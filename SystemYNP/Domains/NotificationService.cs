using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SystemYNP.Data;
using SystemYNP.Models;

namespace SystemYNP.Domains
{
    public class NotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly MailService _mailService;

        public NotificationService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ApplicationDbContext context, MailService mailService)
        {
            _configuration = configuration;
            _context = context;
            _mailService = mailService;
            _httpClient = httpClientFactory.CreateClient();
        }
        
        public async Task Notify()
        {
            var parts = await _context.YNP.ToListAsync();
            var partsCount = (int)Math.Floor(parts.Count / 100.0);
            string message;

            for (var i = 0; i <= partsCount; i++)
            {
                foreach (var unp in parts.Skip(i*100).Take(100))
                {
                    var response = await _httpClient.GetAsync(_configuration["NalogGovBaseUrl"].TrimEnd('/') + $"/grp/getData?unp={unp.Name}&type=json"); //запрос

                    if(response.IsSuccessStatusCode)
                    {
                        
                        var result = JsonConvert.DeserializeObject<NalogGovDataResponse>(await response.Content.ReadAsStringAsync());
                        unp.ExistInExternalApi = true;
                        await _context.SaveChangesAsync();
                        var oldState = unp.ExistInExternalApi;
                        var newState = !string.IsNullOrWhiteSpace(result.Row?.Vunp);

                        if (oldState == newState)
                        {
                            message = $"Уважаемый клиент, {unp.Email} - ваш УНП({unp.Name}) в базе Министерства";
                        }
                        else
                        {
                            message = $"Уважаемый клиент, {unp.Email} - ваш УНП({unp.Name}) в базе заявок на рассмотрение";
                        }
                        
                        unp.ExistInExternalApi = newState;
                        await _context.SaveChangesAsync();
                        _mailService.Send(_configuration["Smtp:Mail"], unp.Email, "Ynp service notification", message);
                    }
                    else
                    {
                        message = $"Уважаемый клиент, {unp.Email} - ваш УНП({unp.Name}) в базе заявок на рассмотрение";
                        await _context.SaveChangesAsync();
                        _mailService.Send(_configuration["Smtp:Mail"], unp.Email, "Ynp service notification", message);
                    }

                }

                await Task.Delay(5000*60);
            }
        }
    }
}
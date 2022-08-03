using System.Threading.Tasks;
using Quartz;

namespace SystemYNP.Domains
{
    public class UserNotificationJob : IJob
    {
        private readonly NotificationService _notificationService;

        public UserNotificationJob(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _notificationService.Notify();
        }
    }
}
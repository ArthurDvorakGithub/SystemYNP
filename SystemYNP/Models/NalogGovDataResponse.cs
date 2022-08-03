using Newtonsoft.Json;

namespace SystemYNP.Models
{
    public class NalogGovDataResponse
    {
        [JsonProperty("ROW")]
        public DataRow Row { get; set; }
    }

    public class DataRow
    {
        [JsonProperty("VUNP")]
        public string Vunp { get; set; }

        [JsonProperty("VNAIMP")]
        public string Vnaimp { get; set; }
    }
}

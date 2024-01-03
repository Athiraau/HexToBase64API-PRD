using Newtonsoft.Json;

namespace DecryptionAPI.Entities
{
    public class ErrorDetails
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

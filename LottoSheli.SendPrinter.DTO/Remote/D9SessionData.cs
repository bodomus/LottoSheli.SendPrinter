using Newtonsoft.Json;

namespace LottoSheli.SendPrinter.DTO.Remote
{
    public class D9SessionData
    {

        [JsonProperty(PropertyName = "csrf_token")]
        public string CsrfToken {get;set;}

        [JsonProperty(PropertyName = "logout_token")]
        public string LogoutToken {get;set;}


        [JsonProperty(PropertyName = "current_user")]
        public D9SessionUser CurrentUser { get; set; }

        [JsonProperty(PropertyName = "session")]
        public CurrentSessionData Session { get; set; }
    }

    public class D9SessionUser
    {
        [JsonProperty(PropertyName = "uid")]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

    public class CurrentSessionData
    {
        [JsonProperty(PropertyName = "id")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}

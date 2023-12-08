using Newtonsoft.Json;

namespace LottoSheli.SendPrinter.Entity
{
    public class PrintTaskResult
    {
        /// <summary>
        /// mapper to json properties received from server
        /// </summary>
        public class ResponseData
        {
            /// <summary>
            /// task status
            /// </summary>
            [JsonProperty("status")]
            public int Status;

            /// <summary>
            /// queue name
            /// </summary>
            [JsonProperty("queue_name")]
            public string QueueName;
        }

        /// <summary>
        /// response data mapped instance
        /// </summary>
        [JsonProperty("data")]
        public readonly ResponseData Data;

        public PrintTaskResult()
        {
            this.Data = new ResponseData();
        }
    }
}

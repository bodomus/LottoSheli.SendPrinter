using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Remote
{
    static class Extensions
    {
        public static bool IsSessionError(this HttpRequestException reqEx) => 
            HttpStatusCode.Unauthorized == reqEx.StatusCode || HttpStatusCode.Forbidden == reqEx.StatusCode;
    }
}

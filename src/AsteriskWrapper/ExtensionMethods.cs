using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AsteriskWrapper
{
    internal static class ExtensionMethods
    {
        public static async Task<Exceptions.AsteriskException> ToExceptionAsync(this HttpResponseMessage response)
        {
            int code = (int)response.StatusCode;
            dynamic responseContent = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            string message = responseContent.message;

            return new Exceptions.AsteriskException(code, message);
        }
    }
}

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsApp.Utils
{
    public class MessageHandler : DelegatingHandler
    {
        public MessageHandler(HttpMessageHandler httpClientHandler) :
            base(httpClientHandler) 
        {  }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Dont use header 'X-Query-Timeout' for CORS policy
            request.Headers.Remove("X-Query-Timeout");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
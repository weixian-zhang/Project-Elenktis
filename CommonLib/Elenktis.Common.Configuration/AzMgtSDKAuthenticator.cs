using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elenktis.Common.Configuration
{
    public class AzMgtSDKAuthenticator : ServiceClientCredentials
    {
        public AzMgtSDKAuthenticator(string tenantId, string clientId, string clientSecret)
        {
            _tenantId = tenantId;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public string AccessToken { get { return AuthenticationToken; } }

        private string AuthenticationToken { get; set; }

        public override void InitializeServiceClient<T>(ServiceClient<T> client)
        {
            var authenticationContext =
                new AuthenticationContext($"https://login.windows.net/{_tenantId}");

            var credential = new ClientCredential(clientId: _clientId, clientSecret: _clientSecret);

            var result = authenticationContext.AcquireTokenAsync
                (resource: "https://management.core.windows.net/",
                    clientCredential: credential).GetAwaiter().GetResult();

            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token");
            }

            AuthenticationToken = result.AccessToken;
        }
        public override async Task ProcessHttpRequestAsync
            (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (AuthenticationToken == null)
            {
                throw new InvalidOperationException("Token Provider Cannot Be Null");
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //request.Version = new Version(apiVersion);
            await base.ProcessHttpRequestAsync(request, cancellationToken);

        }

        private string _tenantId;
        private string _clientId;
        private string _clientSecret;
    }
}

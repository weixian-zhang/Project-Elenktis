using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elenktis.Common.Configuration
{
    public class AzAuthenticator
    {
        public static string GetAccessToken(string tenantId, string clientId, string clientSecret)
        {
            var authenticationContext =
                new AuthenticationContext($"https://login.windows.net/{tenantId}");

            var credential = new ClientCredential(clientId: clientId, clientSecret: clientSecret);

            var result = authenticationContext.AcquireTokenAsync
                (resource: "https://management.core.windows.net/",
                    clientCredential: credential).GetAwaiter().GetResult();

            return result.AccessToken;
        }
    }
}

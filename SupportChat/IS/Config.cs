using IdentityServer4.Models;
using System.Collections.Generic;

namespace IS
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("SignalR", "SignalR Chat")
            };

        public static IEnumerable<ApiResource> ApiResources => 
            new List<ApiResource>
            {
                new ApiResource("SignalR", "SignalR Chat")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                ClientId = "client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "SignalR" }
                }
            };
    }
}

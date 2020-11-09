using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IS.Configs
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                //new IdentityResources.OpenId(),
                //new IdentityResources.Profile()
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

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = {"SignalR"}
                }
            };
    }
}

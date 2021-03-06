﻿using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IS.Configs
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResource("test", "This is one test claim", new string[] {"test"})
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
                    ClientId = "supportChatSpa",
                    ClientName = "Support Chat Spa",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris =           { "https://localhost:5003/callback.html" },
                    // PostLogoutRedirectUris = { "https://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "https://localhost:4200" },

                    AllowedScopes =
                    {
                        "SignalR",
                        "openid",
                        "profile",
                        "phone",
                        "test"
                    }
                },

                new Client
                {
                    ClientId = "postman",

                    ClientSecrets =
                    {
                        new Secret("password".Sha256())
                    },

                    AllowAccessTokensViaBrowser=true,
                    RequireConsent = false,
                    RequirePkce = false,

                    RedirectUris =
                    {
                        "https://localhost:5002/signin-oidc"
                    },

                    AllowedGrantTypes = GrantTypes.Code,

                    AllowedScopes = { //ToDo: implement not as strings
                        "SignalR",
                        "openid",
                        "profile",
                        "phone",
                        "test"
                    }
                },

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

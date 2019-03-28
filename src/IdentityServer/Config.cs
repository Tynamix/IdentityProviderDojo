// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    Description = "Role Description",
                    DisplayName = "Role Display Name",
                    UserClaims = new List<string> { JwtClaimTypes.Role }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            var weatherApi = new ApiResource("weatherapi", "Weather API");
            weatherApi.UserClaims = new List<string> { JwtClaimTypes.Role, "location", "country_code"};

            return new ApiResource[]
            {
                weatherApi
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "demowebapp",
                    ClientName = "Auth Dojo Client",

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "weatherapi", "role"},

                    AlwaysSendClientClaims = true
                },

                new Client
                {
                    ClientId = "postman",
                    ClientName = "Postman",

                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    RedirectUris = { "https://app.getpostman.com/oauth2/callback" },
                    FrontChannelLogoutUri = "https://app.getpostman.com/oauth2/callback",
                    PostLogoutRedirectUris = { "https://app.getpostman.com/oauth2/logout" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "role", "weatherapi" },

                    AlwaysSendClientClaims = true
                },

                // SPA client using implicit flow
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        "http://localhost:5002/index.html",
                        "http://localhost:5002/callback.html",
                        "http://localhost:5002/silent.html",
                        "http://localhost:5002/popup.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5002" },

                    AllowedScopes = { "openid", "profile", "api1" }
                }
            };
        }
    }
}
﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { 
                new ApiScope("order.read"),
                new ApiScope("order.write"),
                new ApiScope("order.delete"),
                new ApiScope("invoice.read"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("order", "My DotNet Core API")
                {
                    Scopes = { "order.read", "order.write", "order.delete" }
                },
                new ApiResource("invoice", "My DotNet 4.5 API")
                {
                    Scopes = { "invoice.read" },
                    ApiSecrets = new Secret[]
                    {
                        new Secret("secret3".Sha256())
                    }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName = "Console app",
                    ClientId = "consoleclient",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret1".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "order.read", "order.write", "order.delete", "invoice.read" }
                },
                new Client
                {
                    ClientName = "MVC website",
                    ClientId = "mvcclient",
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret2".Sha256())
                    },

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    RedirectUris = { "https://localhost:5012/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5012/signout-callback-oidc" },

                    AllowedScopes = {"openid", "profile", "offline_access", "order.read", "order.write", "order.delete", "invoice.read" },

                    AllowOfflineAccess = true,
                },
                new Client
                {
                    ClientId = "jsclient",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =  { "https://localhost:5013/callback.html" },
                    PostLogoutRedirectUris = { "https://localhost:5013/index.html" },
                    AllowedCorsOrigins =
                        {
                            "https://localhost:5013"
                        },

                    AllowedScopes = {"openid", "profile", "offline_access", "order.read", "order.delete", "invoice.read" },
                },
                new Client
                {
                    ClientName = ".NET 4 MVC website",
                    ClientId = "net4mvcclient",
                    ClientSecrets =
                    {
                        new Secret("secret3".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "https://localhost:49816/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:49816" },


                    AllowedScopes = {"openid", "profile", "offline_access", "order.read", "order.delete", "invoice.read" }
                },
                new Client
                {
                    ClientName = ".NET 4 MVC website - external",
                    ClientId = "net4mvcclientexternal",
                    ClientSecrets =
                    {
                        new Secret("secret4".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    AllowOfflineAccess = true,

                    RedirectUris = { "http://localhost:44319/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:44319" },


                    AllowedScopes = {"openid", "profile", "offline_access", "order.read", "order.delete", "invoice.read" }
                },
                new Client
                {
                    ClientId = "razorappclient",
                    ClientName = "Razor Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = { "https://localhost:5015/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5015/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                    }
                },
                new Client
                {
                    ClientId = "password-client",
                    ClientName = "Password Client",
                    ClientSecrets =
                    {
                        new Secret("secret41".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = {"openid", "profile", "offline_access", "email", "order.read", "order.delete", "invoice.read" },

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,
                    IdentityTokenLifetime = 3600,
                    UpdateAccessTokenClaimsOnRefresh = false,
                    SlidingRefreshTokenLifetime = 30,
                   
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,

                    AlwaysSendClientClaims = true,
                    Enabled = true,
                    AllowOfflineAccess = true,
                }
            };
    }
}
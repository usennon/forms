// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using IdentityModel;
using System.Security.Claims;
using System.Text.Json;
using Duende.IdentityServer;
using Duende.IdentityServer.Test;
using IW5.DAL.Initialization;

namespace IW5.IdentityProvider.App;

public static class TestUsers
{
    public static List<TestUser> Users
    {
        get
        {
            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Heidelberg",
                postal_code = "69118",
                country = "Germany"
            };

            var identityUsers = new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "tempUser",
                    Password = "password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "User CookBook"),
                        new Claim(JwtClaimTypes.GivenName, "User"),
                        new Claim(JwtClaimTypes.FamilyName, "CookBook"),
                        new Claim(JwtClaimTypes.Email, "usercookbook@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "admin",
                    Password = "password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Admin CookBook"),
                        new Claim(JwtClaimTypes.GivenName, "Admin"),
                        new Claim(JwtClaimTypes.FamilyName, "CookBook"),
                        new Claim(JwtClaimTypes.Email, "admincookbook@email.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
            var id = 3;
            foreach(var tempUser in SampleData.Users)
            {
                var userToAdd = new TestUser
                {
                    SubjectId = id++.ToString(),
                    Username = tempUser.Name,
                    Password = tempUser.Name + "@password",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, tempUser.Name),
                        new Claim(JwtClaimTypes.GivenName, tempUser.Name),
                        new Claim(JwtClaimTypes.FamilyName, tempUser.Name),
                        new Claim(JwtClaimTypes.Email, tempUser.Email),
                        new Claim(JwtClaimTypes.Role, tempUser.Role.ToString())
                    }
                };
                identityUsers.Add(userToAdd);
            }
            return identityUsers;
        }
    }
}
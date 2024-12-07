using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IW5.IdentityProvider.App
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources
        {
            get
            {
                var profileIdentityResources = new IdentityResources.Profile();
                profileIdentityResources.UserClaims.Add("username");

                return
                [
                    new IdentityResources.OpenId(),
                    profileIdentityResources
                ];
            }
        }

        public static IEnumerable<ApiResource> ApiResources =>
        [
            new ("iw5clientaudience")
        ];

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
            new ApiScope("iw5api", "My API"),
            };

        public static IEnumerable<Client> Clients =>
                [
                new()
                {
                    ClientName = "iw5client",
                    ClientId = "iw5client",
                    AllowOfflineAccess = true,
                    RedirectUris =
                    [
                        "https://oauth.pstmn.io/v1/callback",
                        "https://localhost:5001/authentication/login-callback",
                    ],
                    AllowedGrantTypes =
                    [
                        GrantType.ClientCredentials,
                        GrantType.ResourceOwnerPassword,
                        GrantType.AuthorizationCode
                    ],
                    RequirePkce = true,
                    AllowedScopes =
                    [
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "iw5api"
                    ],
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireClientSecret = false
                }
                ];
    }

}
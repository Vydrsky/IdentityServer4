using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.WebSockets;

namespace Client.Services;

public class TokenService : ITokenService {
    public readonly IOptions<IdentityServerSettings> options;
    public readonly DiscoveryDocumentResponse discoveryDocumentResponse;
    public readonly HttpClient httpClient;

    public TokenService(IOptions<IdentityServerSettings> options) {
        this.options = options;
        httpClient = new HttpClient();
        discoveryDocumentResponse = httpClient.GetDiscoveryDocumentAsync(options.Value.DiscoveryUrl).Result;
        if (discoveryDocumentResponse.IsError) {
            throw new Exception("Unable to get discovery document");
        }
    }

    public async Task<TokenResponse> GetToken(string scope) {
        var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest {
            Address = discoveryDocumentResponse.TokenEndpoint,
            ClientId = options.Value.ClientName,
            ClientSecret = options.Value.ClientPassword,
            Scope = scope
        });

        if (tokenResponse.IsError) {
            throw new Exception("Unable to get token");
        }

        return tokenResponse;
    }
}

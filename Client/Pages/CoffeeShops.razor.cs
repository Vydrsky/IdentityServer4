using API.Models;
using Client.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components;


namespace Client.Pages;
public partial class CoffeeShops {

    private List<CoffeeShopModel> Shops = new();
    [Inject] private HttpClient httpClient { get; set; }
    [Inject] private IConfiguration Configuration { get; set; }
    [Inject] private ITokenService TokenService { get; set; }

    protected override async Task OnInitializedAsync() {

        var tokenResponse = await TokenService.GetToken("CoffeeAPI.read");
        httpClient.SetBearerToken(tokenResponse.AccessToken);

        var result = await httpClient.GetAsync(Configuration["apiUrl"] + "/api/CoffeeShop");

        if (result.IsSuccessStatusCode) {
            Shops = await result.Content.ReadFromJsonAsync<List<CoffeeShopModel>>();
        }
    }
}

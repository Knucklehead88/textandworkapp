using Microsoft.JSInterop;
using TextAndWorkApp.Models;

namespace TextAndWorkApp.Components.Helper;

public static class IJSRuntimeExtensions
{
    public static async Task InitializeStripe(this IJSRuntime jsRuntime, string publicKey)
    {
        await jsRuntime.InvokeVoidAsync("initializeStripe", publicKey);
    }
    
    public static async Task<string> ConfirmCardSetup(this IJSRuntime jsRuntime, string clientSecret)
    {
        return await jsRuntime.InvokeAsync<string>("confirmCardSetup", clientSecret);
    }

    public static async Task ResetStripeElement(this IJSRuntime jsRuntime)
    {
        await jsRuntime.InvokeVoidAsync("resetStripeElement");
    }
}
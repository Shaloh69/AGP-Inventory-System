using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using AppClient.Helpers;
using AppClient.Models;

namespace AppClient.Services;

// The ONLY class that talks to the server. Pages call these methods; they never use HttpClient.
public static class ApiClient
{
    // One HttpClient for the whole app, reused for every request.
    private static readonly HttpClient http = new()
    {
        BaseAddress = new Uri(AppConfig.ApiBaseUrl),
        Timeout = TimeSpan.FromSeconds(90) // Render's free plan can take a while to wake up
    };

    public static string BaseUrl => AppConfig.ApiBaseUrl;

    // ---------- health ----------
    public static async Task<bool> IsServerUpAsync()
    {
        try
        {
            var response = await http.GetAsync("api/health");
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static async Task<bool> IsDatabaseUpAsync()
    {
        try
        {
            var response = await http.GetAsync("api/health/db");
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // ---------- products: read ----------
    public static async Task<List<Product>> GetProductsAsync(string? search = null)
    {
        string url = string.IsNullOrWhiteSpace(search)
            ? "api/products"
            : $"api/products?search={Uri.EscapeDataString(search.Trim())}";
        var response = await http.GetAsync(url);
        await EnsureSuccessAsync(response);
        return await response.Content.ReadFromJsonAsync<List<Product>>() ?? new();
    }

    // ---------- products: store ----------
    public static async Task<Product> CreateProductAsync(Product product)
    {
        var response = await http.PostAsJsonAsync("api/products", product);
        await EnsureSuccessAsync(response);
        return (await response.Content.ReadFromJsonAsync<Product>())!;
    }

    public static async Task<Product> UpdateProductAsync(Product product)
    {
        var response = await http.PutAsJsonAsync($"api/products/{product.Id}", product);
        await EnsureSuccessAsync(response);
        return (await response.Content.ReadFromJsonAsync<Product>())!;
    }

    public static async Task DeleteProductAsync(int id)
    {
        var response = await http.DeleteAsync($"api/products/{id}");
        await EnsureSuccessAsync(response);
    }

    // Turns an error response like {"message":"name is required"} into an ApiException.
    private static async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) return;

        string message = response.ReasonPhrase ?? "Request failed";
        try
        {
            var error = await response.Content.ReadFromJsonAsync<ApiError>();
            if (!string.IsNullOrWhiteSpace(error?.Message)) message = error.Message;
        }
        catch (Exception)
        {
            // the body was not JSON - keep the default message
        }
        throw new ApiException((int)response.StatusCode, message);
    }

    private class ApiError
    {
        [JsonPropertyName("message")] public string? Message { get; set; }
    }
}

// Thrown when the server answers with an error (400, 404, 409, 500 ...).
public class ApiException(int statusCode, string message) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}

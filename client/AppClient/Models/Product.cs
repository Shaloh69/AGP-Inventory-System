using System.ComponentModel;
using System.Text.Json.Serialization;
using AppClient.Helpers;

namespace AppClient.Models;

// Matches one row of the products table, as the API sends it in JSON.
// [JsonPropertyName] maps the database's snake_case names to C# PascalCase names.
public class Product
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("quantity")] public int Quantity { get; set; }
    [JsonPropertyName("price")] public decimal Price { get; set; }

    [JsonPropertyName("created_at"), Browsable(false)] public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updated_at"), Browsable(false)] public DateTime UpdatedAt { get; set; }

    // Calculated in C#, not stored in the database. [JsonIgnore] = never sent to the API.
    [JsonIgnore, DisplayName("Total value")]
    public decimal TotalValue => InventoryMath.CalculateTotal(Price, Quantity);
}

using System.Text.Json;
using System.Text.Json.Serialization;

public static class Foodstuffs
{
	public static List<Ingredient> Ingredients;
	public static Settings Settings;

	public static void LoadIngredients()	
	{
		// Get the ingredients JSON file
		string ingredientsJsonPath = "./assets/food/ingredients.json";
		string ingredientsJson = File.ReadAllText(ingredientsJsonPath);

		// Parse the JSON
		Root jsonData = JsonSerializer.Deserialize<Root>(ingredientsJson);

		// Set all of the ingredients and settings for use in the game
		Ingredients = jsonData.Ingredients;
		Settings = jsonData.Settings;
	}
}



// JSON file schema outline thingy
public class CookStatus
{
	public string name { get; set; }
	public string texture { get; set; }
}

public class Ingredient
{
	[JsonPropertyName("name")] public string Name { get; set; }
	[JsonPropertyName("price")] public float Price { get; set; }
	[JsonPropertyName("spawnPercentage")] public float SpawnPercentage { get; set; }
	[JsonPropertyName("texture")] public string Texture { get; set; }
	[JsonPropertyName("origin")] public float Origin { get; set; }
	[JsonPropertyName("cookTimeSeconds")] public int? CookTimeSeconds { get; set; }
	[JsonPropertyName("cookStatus")] public List<CookStatus> CookStatus { get; set; }
}

public class Root
{
	[JsonPropertyName("settings")] public Settings Settings { get; set; }
	[JsonPropertyName("ingredients")] public List<Ingredient> Ingredients { get; set; }
}

public class Settings
{
	[JsonPropertyName("topBun")] public int TopBun { get; set; }
	[JsonPropertyName("bottomBun")] public int BottomBun { get; set; }
	[JsonPropertyName("requiredIngredients")] public List<int> RequiredIngredients { get; set; }
}
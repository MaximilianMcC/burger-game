using System.Text.Json;

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
		Ingredients = jsonData.ingredients;
		Settings = jsonData.settings;
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
	public string name { get; set; }
	public float price { get; set; }
	public float spawnPercentage { get; set; }
	public string texture { get; set; }
	public float origin { get; set; }
	public int? cookTimeSeconds { get; set; }
	public List<CookStatus> cookStatus { get; set; }
}

public class Root
{
	public Settings settings { get; set; }
	public List<Ingredient> ingredients { get; set; }
}

public class Settings
{
	public int topBun { get; set; }
	public int bottomBun { get; set; }
	public List<int> requiredIngredients { get; set; }
}
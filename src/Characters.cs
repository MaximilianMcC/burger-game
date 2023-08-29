using SFML.Graphics;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class Characters
{
	public static List<Character> Customers { get; set; }

	public static void LoadCharacters()
	{
		// Get the customers JSON file
		string customersJsonPath = "./assets/customers/customers.json";
		string customerJson = File.ReadAllText(customersJsonPath);

		// Parse the JSON and get the customers from it
		Customers = JsonSerializer.Deserialize<List<Character>>(customerJson);
	}
}

public class Character
{
	[JsonPropertyName("name")] public string Name { get; set; }
	[JsonPropertyName("texture")] public string TexturePath { get; set; }
}
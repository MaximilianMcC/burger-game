using SFML.Graphics;

struct Ingredient
{
	public string Name { get; internal set; }
	public float? SpawnProbability { get; internal set; }
	public Sprite Sprite { get; private set; }

	public List<CookState> CookStates { get; internal set; }
	public float CookTimeSeconds { get; internal set; }

	public Ingredient(string texturePath)
	{
		Sprite = new Sprite(new Texture(texturePath));
	}
}

struct CookState
{
	public string Name { get; internal set; }
	public Sprite Sprite { get; private set; }

	public CookState(string texturePath)
	{
		Sprite = new Sprite(new Texture(texturePath));
	}
}



class FoodStuffs
{
	public List<Ingredient> Ingredients = new List<Ingredient>()
	{
		new Ingredient("./assets/food/cheese.png")
		{
			Name = "Cheese",
			SpawnProbability = 0.5f
		},
		new Ingredient("./assets/food/lettuce.png")
		{
			Name = "Lettuce",
			SpawnProbability = 0.4f
		},
		new Ingredient()
		{
			Name = "Beef",
			SpawnProbability = 1f,
			CookTimeSeconds = 30f,
			CookStates = new List<CookState>()
			{
				new CookState("./assets/food/beef-raw.png") { Name = "Raw" },
				new CookState("./assets/food/beef-cooking.png") { Name = "Cooking" },
				new CookState("./assets/food/beef-cooked.png") { Name = "Cooked" },
				new CookState("./assets/food/beef-burnt.png") { Name = "Burnt" }
			}
		},
		new Ingredient()
		{
			Name = "Bacon",
			SpawnProbability = 0.4f,
			CookTimeSeconds = 15f,
			CookStates = new List<CookState>()
			{
				new CookState("./assets/food/bacon-raw.png") { Name = "Raw" },
				new CookState("./assets/food/bacon-cooking.png") { Name = "Cooking" },
				new CookState("./assets/food/bacon-cooked.png") { Name = "Cooked" },
				new CookState("./assets/food/bacon-burnt.png") { Name = "Burnt" }
			}
		}
	};
}
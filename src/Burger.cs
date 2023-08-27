using SFML.Graphics;
using SFML.System;

class Burger
{
	public List<Ingredient> Ingredients { get; private set; }
	private Sprite burgerSprite;

	public Burger(List<Ingredient> ingredients)
	{
		Ingredients = ingredients;
		Create();
	}

	// Create/assemble the burger texture thing
	public void Create()
	{
		Sprite[] burgerSprites = new Sprite[Ingredients.Count];
		RenderTexture burgerTexture = new RenderTexture(Game.Window.Size.X, Game.Window.Size.Y); // TODO: Don't hardcode size
		float y = 0;

		// Loop through all burger ingredients
		for (int i = 0; i < Ingredients.Count; i++)
		{
			// Get the ingredient and make the sprite
			Ingredient ingredient = Ingredients[i];
			Sprite sprite = new Sprite();
 
			// Check for if its something that needs cooking
			if (ingredient.CookTimeSeconds != null)
			{
				// Needs cooking. Get first texture
				// TODO: Store cook status and stuff
				sprite.Texture = new Texture(RelativeToAbsoluteTexture(ingredient.CookStatus[0].texture));
			}
			else sprite.Texture = new Texture(RelativeToAbsoluteTexture(ingredient.Texture));

			// Set the scale
			float scale = 2f;
			sprite.Scale = new Vector2f(scale, scale);

			//TODO: Randomly flip the sprite on X to add some variation
			// Random random = new Random();
			// sprite.Origin = new Vector2f(sprite.Texture.Size.X / 2, sprite.Texture.Size.Y / 2);
			// if (random.NextSingle() > 0.5f) sprite.Scale = new Vector2f(-scale, scale);
			// sprite.Origin = new Vector2f(0, 0);

			// Set the position, then draw it
			sprite.Position = new Vector2f(0, y);
			burgerSprites[(Ingredients.Count - 1) - i] = sprite; // (draws reversed)
			y += ingredient.Origin * scale;
		}

		// Create the texture for drawing the burger
		for (int i = 0; i < burgerSprites.Length; i++)
		{
			burgerTexture.Draw(burgerSprites[i]);
		}
		
		burgerSprite = new Sprite(burgerTexture.Texture);
		burgerTexture.Display();
	}

	public void Render()
	{
		// Render the fully created burger texture
		Game.Window.Draw(burgerSprite);
	}

	public string RelativeToAbsoluteTexture(string relativeTexture)
	{
		return "./assets/food/" + relativeTexture.Split("/")[1];
	}
}
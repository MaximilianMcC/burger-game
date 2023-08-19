using SFML.Graphics;
using SFML.System;

class Burger
{
	public List<Ingredient> Ingredients { get; private set; }
	private RenderTexture burgerTexture;

	public Burger(List<Ingredient> ingredients)
	{
		Ingredients = ingredients;
		Create();
	}

	// Create/assemble the burger texture thing
	public void Create()
	{
		burgerTexture = new RenderTexture(Game.Window.Size.X, Game.Window.Size.Y);
		float y = 0;

		// Loop through all burger ingredients (reversed)
		for (int i = Ingredients.Count - 1; i >= 0 ; i--)
		{
			// Get the ingredient and make the sprite
			Ingredient ingredient = Ingredients[i];
			Sprite sprite = new Sprite();

			// Check for if its something that needs cooking
			if (ingredient.cookTimeSeconds != null)
			{
				// Needs cooking. Get first texture
				// TODO: Store cook status and stuff
				sprite.Texture = new Texture(RelativeToAbsoluteTexture(ingredient.cookStatus[0].texture));
			}
			else sprite.Texture = new Texture(RelativeToAbsoluteTexture(ingredient.texture));

			sprite.Position = new Vector2f(0, y);
			//! sprite.Scale = new Vector2f(1, -1);
			y += 10;
			burgerTexture.Draw(sprite);
		}
	}

	public void Render()
	{
		// TODO: Don't make a new sprite every frame
		Game.Window.Draw(new Sprite(burgerTexture.Texture));
	}

	public string RelativeToAbsoluteTexture(string relativeTexture)
	{
		return "./assets/food/" + relativeTexture.Split("/")[1];
	}
}
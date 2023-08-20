using SFML.Graphics;
using SFML.System;

class Burger
{
	public List<Ingredient> Ingredients { get; private set; }
	private Sprite[] burgerSprites;

	public Burger(List<Ingredient> ingredients)
	{
		Ingredients = ingredients;
		Create();
	}

	// Create/assemble the burger texture thing
	public void Create()
	{
		burgerSprites = new Sprite[Ingredients.Count];
		float y = 0;

		// Loop through all burger ingredients (reversed)
		for (int i = 0; i < Ingredients.Count; i++)
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
			burgerSprites[i] = sprite;

			y += 15f;
		}
	}

	public void Render()
	{
		//TODO: Don't use sprites. Use render texture
		for (int i = 0; i < burgerSprites.Length; i++)
		{
			Game.Window.Draw(burgerSprites[i]);
		}
	}

	public string RelativeToAbsoluteTexture(string relativeTexture)
	{
		return "./assets/food/" + relativeTexture.Split("/")[1];
	}
}
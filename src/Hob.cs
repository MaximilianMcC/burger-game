using System.Numerics;
using Raylib_cs;

class Hob : GameObject
{
	private Texture2D texture;

	public Hob()
	{
		texture = Raylib.LoadTexture("./assets/hob-top-view.png");
		Size = new Vector2(128);
	}

	public bool OnHob(GameObject thing)
	{
		return Raylib.CheckCollisionRecs(
			thing.Rectangle,
			Rectangle
		);
	}

	public override void Draw()
	{
		// Utils.DrawTexture(texture, size, Position);
		Raylib.DrawTextureV(texture, Position, Color.White);
	}
}
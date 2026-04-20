using System.Numerics;
using Raylib_cs;

static class Utils
{
	public static void DrawTexture(Texture2D texture, Vector2 position, Vector2 size)
	{
		Raylib.DrawTexturePro(
			texture,
			new Rectangle(0, 0, texture.Dimensions),
			new Rectangle(position, size),
			Vector2.Zero,
			0f,
			Color.White
		);
	}

	public static void DrawTextureFromMiddle(Texture2D texture, Vector2 position, Vector2 size, float rotation = 0f)
	{
		Raylib.DrawTexturePro(
			texture,
			new Rectangle(0, 0, texture.Dimensions),
			new Rectangle(position, size),
			size / 2f,
			rotation,
			Color.White
		);
	}

	public static void DrawTextureHorizontallyCentred(Texture2D texture, Vector2 position, Vector2 areaSize, Vector2 textureSize)
	{
		Raylib.DrawTexturePro(
			texture,
			new Rectangle(0, 0, texture.Dimensions),
			new Rectangle(
				position + Vector2.UnitX * ((areaSize.X - textureSize.X) / 2f),
				textureSize
			),
			Vector2.Zero,
			0f,
			Color.White
		);	
	}
}
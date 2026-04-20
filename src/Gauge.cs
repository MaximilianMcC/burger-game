using System.Numerics;
using Raylib_cs;

class Gauge : GameObject
{
	private Texture2D gaugeTexture;
	private Texture2D needleTexture;

	private float percentage = 0;

	public Gauge()
	{
		Size = new Vector2(128);
		gaugeTexture = Raylib.LoadTexture("./assets/gauge.png");
		needleTexture = Raylib.LoadTexture("./assets/gauge-needle.png");
	}

	public override void Update()
	{
		if (Raylib.IsKeyDown(KeyboardKey.Space)) percentage += 2 * Raylib.GetFrameTime();
		percentage -= 0.5f * Raylib.GetFrameTime();
		percentage = Math.Clamp(percentage, 0, 1);

		Console.WriteLine(percentage);
	}

	public override void Draw()
	{
		// Turn the percentage into a value
		float rotation = 270 * percentage;

		Utils.DrawTextureFromMiddle(gaugeTexture, Position, Size, 0f);
		Utils.DrawTextureFromMiddle(needleTexture, Position, Size, rotation);
	}
}
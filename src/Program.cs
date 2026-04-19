using System.Numerics;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.InitWindow(800, 600, "rayib now lol");

		Receipt receipt = new Receipt();
		receipt.Position = new Vector2(550, 0);

		Foodstuff patty = new Foodstuff(
			"./assets/food/beef-raw.png",
			"./assets/food/beef-cooking.png",
			"./assets/food/beef-cooked.png",
			"./assets/food/beef-burnt.png"
		);

		while (Raylib.WindowShouldClose() == false)
		{
			patty.Update();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			patty.Draw();
			
			receipt.Draw();
			Raylib.EndDrawing();
		}

		patty.CleanUp();
		receipt.CleanUp();
		Raylib.CloseWindow();
	}
}
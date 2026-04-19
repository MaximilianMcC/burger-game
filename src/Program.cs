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

		Hob hob = new Hob();
		hob.Position = new Vector2(20);
		Scene.GameObjects.Add(hob);

		Foodstuff patty = new Foodstuff(
			"./assets/food/beef-raw.png",
			"./assets/food/beef-cooking.png",
			"./assets/food/beef-cooked.png",
			"./assets/food/beef-burnt.png"
		);
		patty.Position = new Vector2(300);

		while (Raylib.WindowShouldClose() == false)
		{
			hob.Update();
			patty.Update();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			hob.Draw();
			patty.Draw();
			
			receipt.Draw();
			Raylib.EndDrawing();
		}

		patty.CleanUp();
		receipt.CleanUp();
		Raylib.CloseWindow();
	}
}
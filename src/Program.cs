using System.Numerics;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.InitWindow(800, 600, "raylib now lol");

		Receipt receipt = new Receipt();
		receipt.Position = new Vector2(550, 0);
		Scene.GameObjects.Add(receipt);

		Hob hob = new Hob();
		hob.Position = new Vector2(200);
		Scene.GameObjects.Add(hob);

		Gauge gauge = new Gauge();
		gauge.Position = new Vector2(100);
		Scene.GameObjects.Add(gauge);

		Foodstuff patty = new Foodstuff(
			"./assets/food/beef-raw.png",
			"./assets/food/beef-cooking.png",
			"./assets/food/beef-cooked.png",
			"./assets/food/beef-burnt.png"
		);
		patty.Position = new Vector2(300);
		Scene.GameObjects.Add(patty);

		while (Raylib.WindowShouldClose() == false)
		{
			foreach (GameObject gameObject in Scene.GameObjects)
			{
				gameObject.Update();
			}

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			foreach (GameObject gameObject in Scene.GameObjects)
			{
				gameObject.Draw();
			}
			
			Raylib.EndDrawing();
		}

		patty.CleanUp();
		receipt.CleanUp();
		Raylib.CloseWindow();
	}
}
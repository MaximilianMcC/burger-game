using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.InitWindow(800, 600, "rayib now lol");

		Receipt receipt = new Receipt();

		while (Raylib.WindowShouldClose() == false)
		{
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			receipt.Draw();
			Raylib.EndDrawing();
		}

		Raylib.CloseWindow();
	}
}
using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{

	public static RenderWindow Window { get; private set; }
	public static float DeltaTime { get; private set; }

	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 800), "Burger");
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();

		// Clock and ui
		Clock deltaTimeClock = new Clock();

		while (Window.IsOpen)
		{
			// Handle events and whatnot
			Window.DispatchEvents();
			DeltaTime = deltaTimeClock.Restart().AsSeconds();

			// Draw/render everything			
			Window.Clear(Color.Magenta);
			Window.Display();
		}
	}
}
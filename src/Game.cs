using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{

	public static RenderWindow Window { get; private set; }
	public static float DeltaTime { get; private set; }
	private static List<Prop> ingredients; 

	public void Run()
	{
		// Create the SFML window
		Window = new RenderWindow(new VideoMode(800, 600), "Burger"); // 4:3
		Window.SetFramerateLimit(60);
		Window.Closed += (sender, e) => Window.Close();

		// Clock and ui
		Clock deltaTimeClock = new Clock();

		Foodstuffs.LoadIngredients();
		Burger testBurger = new Burger(new Customer().Order);

		// Make stuff
		ingredients = new List<Prop>();
		ingredients.Add(new Prop());

		while (Window.IsOpen)
		{
			// Handle events and whatnot
			Window.DispatchEvents();
			DeltaTime = deltaTimeClock.Restart().AsSeconds();



			for (int i = 0; i < ingredients.Count; i++) ingredients[i].Update();



			// Draw/render everything			
			Window.Clear(Color.Magenta);
			for (int i = 0; i < ingredients.Count; i++) ingredients[i].Render();
			
			testBurger.Render();

			Window.Display();
		}
	}
}
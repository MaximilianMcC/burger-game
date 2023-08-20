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
		// Burger testBurger = new Burger(new Customer().Order);
		List<Ingredient> testOrder = new List<Ingredient>();
		testOrder.Add(Foodstuffs.Ingredients[0]);
		testOrder.Add(Foodstuffs.Ingredients[4]);
		testOrder.Add(Foodstuffs.Ingredients[7]);
		testOrder.Add(Foodstuffs.Ingredients[3]);
		testOrder.Add(Foodstuffs.Ingredients[2]);
		testOrder.Add(Foodstuffs.Ingredients[6]);
		testOrder.Add(Foodstuffs.Ingredients[3]);
		testOrder.Add(Foodstuffs.Ingredients[5]);
		testOrder.Add(Foodstuffs.Ingredients[1]);
		Burger testBurger = new Burger(testOrder);

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
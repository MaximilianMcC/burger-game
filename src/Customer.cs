class Customer
{
	public Customer()
	{
		// Create an order
		GenerateOrder();
	}

	private void GenerateOrder()
	{
		//TODO: Make more advanced order generation algorithm. Add stuff like if there is a beef patty there is a larger chance to get cheese after it

		// Get how many items to add (excluding buns and required items)
		Random random = new Random();
		int orderCount = random.Next(0, 5);

		// Create the order list
		List<Ingredient> order = new List<Ingredient>();

		// Add the random items to the order
		for (int i = 0; i < orderCount; i++)
		{
			// Loop through all ingredients
			foreach (Ingredient ingredient in Foodstuffs.Ingredients)
			{
				// Check for if the current ingredient is selected
				if (random.NextSingle() < ingredient.spawnPercentage)
				{
					// Add the ingredient to the order
					order.Add(ingredient);

					// Only add a single ingredient
					break;
				}
			}
		}

		// Add the required ingredients
		for (int i = 0; i < Foodstuffs.Settings.requiredIngredients.Count; i++)
		{
			// Get a random index to add the ingredient in
			int index = random.Next(0, orderCount);
			order.Insert(index, Foodstuffs.Ingredients[Foodstuffs.Settings.requiredIngredients[i]]);
		}

		// Add the required top and bottom buns
		order.Insert(0, Foodstuffs.Ingredients[Foodstuffs.Settings.topBun]);
		order.Add(Foodstuffs.Ingredients[Foodstuffs.Settings.bottomBun]);
	}

}
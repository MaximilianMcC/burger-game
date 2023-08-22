using SFML.Graphics;
using SFML.System;

class Customer
{
	public List<Ingredient> Order { get; private set; }
	public float OrderPrice { get; private set; }
	private RenderTexture receipt;

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
		// int orderCount = random.Next(0, 5);
		int orderCount = random.Next(0, 15);


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
					OrderPrice += ingredient.price;

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

		// Set the customers order and the price
		Order = order;
		OrderPrice = MathF.Round(OrderPrice, 2);
	}

	// Make a receipt showing the customers order
	public void GenerateOrderReceipt()
	{
		// Get all needed assets
		Font font = new Font("./assets/fonts/MerchantCopy.ttf");
		Font fontWide = new Font("./assets/fonts/MerchantCopyWide.ttf");
		Sprite receiptBackground = new Sprite(new Texture("./assets/receipt-paper.png"));
		uint fontSize = 24;

		// Begin to make the actual receipt
		receipt = new RenderTexture(Game.Window.Size.X, Game.Window.Size.Y);
		receiptBackground.Scale = new Vector2f(0.5f, 0.5f);
		receipt.Draw(receiptBackground);

		// Generate the receipt text about the order
		string itemsTextString = "ORDER SUMMARY -------\n";
		string pricesTextString = "\n";
		foreach (Ingredient ingredient in Order)
		{
			itemsTextString += "> " + ingredient.name + "\n";
			pricesTextString += ingredient.price.ToString("$0.00\n");
		}
		itemsTextString += "---------------------\nTOTAL: " + OrderPrice.ToString("$0.00");
		
		// Make the receipt items text
		Text itemsText = new Text(itemsTextString, font, fontSize);
		itemsText.Position = new Vector2f(10, 30);
		itemsText.FillColor = Color.Black;
		receipt.Draw(itemsText);

		// Make the receipt prices text
		Text pricesText = new Text(pricesTextString, font, fontSize);
		pricesText.Origin = new Vector2f(pricesText.GetGlobalBounds().Width, 0);
		pricesText.Position = new Vector2f(200, 30);
		pricesText.FillColor = Color.Black;
		receipt.Draw(pricesText);

		// Generate the other random receipt stuff. Barcode and whatnot maybe. logo


		// Display the final receipt before rendering to stop it from going upside down
		receipt.Display();
	}



	public void RenderReceipt()
	{
		// TODO: Don't make new sprite every time
		Game.Window.Draw(new Sprite(receipt.Texture));
	}

}
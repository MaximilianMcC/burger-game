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



	public Sprite GenerateBarcode(int numberInput)
	{
		// from https://www.labelsandlabeling.com/sites/labels/lnl/files/Books/figure_2.2_-_how_barcodes_can_be_used_to_represent_the_numbers_from_zero_to_nine.png
		bool[][] numberPatterns = new bool[10][];
		numberPatterns[1] = new bool[] { false, false, true, true, false, false, true };
		numberPatterns[0] = new bool[] { false, false, false, true, true, false, true };
		numberPatterns[2] = new bool[] { false, false, true, false, false, true, true };
		numberPatterns[3] = new bool[] { false, true, true, true, true, false, true };
		numberPatterns[4] = new bool[] { false, true, false, false, false, true, true };
		numberPatterns[5] = new bool[] { false, true, true, false, false, false, true };
		numberPatterns[6] = new bool[] { false, true, false, true, true, true, true };
		numberPatterns[7] = new bool[] { false, true, true, true, false, true, true };
		numberPatterns[8] = new bool[] { false, true, true, false, true, true, true };
		numberPatterns[9] = new bool[] { false, false, false, true, false, true, true };

		int barWidth = 5;
		int barHeight = 200;
		uint width = (uint)(numberInput.ToString().Length * (barWidth * 7));
		RenderTexture barcode = new RenderTexture(width, (uint)(barHeight));
		Console.WriteLine(width);
		Console.WriteLine(numberInput.ToString().Length);

		// TODO: don't cast to string, char, then back to string
		int x = 0;
		foreach (char singleNumber in numberInput.ToString())
		{
			// Check for what number it is
			int number = int.Parse(singleNumber.ToString());
			for (int i = 0; i < numberPatterns.Length; i++)
			{
				if (number != i) continue;

				// Add the current number to the barcode
				for (int j = 0; j < numberPatterns[i].Length; j++)
				{
					// Make the bar
					RectangleShape bar = new RectangleShape(new Vector2f(barWidth, barHeight));
					bar.Position = new Vector2f(x, 0);

					// Fill or no fill
					bar.FillColor = Color.White;
					bar.FillColor = (numberPatterns[i][j] == true) ? Color.Black : Color.White;

					// Draw the bar, then increase the index for the next one
					barcode.Draw(bar);
					x += barWidth;
				}
			}
		}

		// Return the render texture as a sprite
		barcode.Display();
		return new Sprite(barcode.Texture);
	}
}
using SFML.Graphics;
using SFML.System;

class Customer
{
	public List<Ingredient> Order { get; private set; }
	public string OrderString { get; private set; }
	public float OrderPrice { get; private set; } = 0.00f;
	private RenderTexture receipt;

	public Customer()
	{
		// Create an order, and the order asking string
		GenerateOrder();
	}

	private void GenerateOrder()
	{
		//TODO: Make more advanced order generation algorithm. Add stuff like if there is a beef patty there is a larger chance to get cheese after it

		// Get how many items to add (excluding buns and required items)
		Random random = new Random();
		int orderCount = random.Next(0, 7);


		// Create the order list
		List<Ingredient> order = new List<Ingredient>();


		// Create a weighted list so that the probabilities are real
		List<Ingredient> weightedIngredients = new List<Ingredient>();
		foreach (Ingredient ingredient in Foodstuffs.Ingredients)
		{
			// Get how many items to add (the weight/percentage)
			int weight = (int)(ingredient.spawnPercentage * 100);
			for (int i = 0; i < weight; i++) weightedIngredients.Add(ingredient);
		}

		// Add the random items to the order
		for (int i = 0; i < orderCount; i++)
		{
			// Get a random ingredient from the weighted list
			int index = random.Next(0, weightedIngredients.Count);
			Ingredient ingredient = weightedIngredients[index];

			// Add the ingredient to the order
			order.Add(ingredient);
			OrderPrice += ingredient.price;
		}

		// Add the required ingredients
		for (int i = 0; i < Foodstuffs.Settings.requiredIngredients.Count; i++)
		{
			// Get a random index to add the ingredient in
			int index = random.Next(0, orderCount);
			order.Insert(index, Foodstuffs.Ingredients[Foodstuffs.Settings.requiredIngredients[i]]);
			OrderPrice += Foodstuffs.Ingredients[Foodstuffs.Settings.requiredIngredients[i]].price;
		}

		// Add the required top and bottom buns
		order.Insert(0, Foodstuffs.Ingredients[Foodstuffs.Settings.topBun]);
		order.Add(Foodstuffs.Ingredients[Foodstuffs.Settings.bottomBun]);
		OrderPrice += Foodstuffs.Ingredients[Foodstuffs.Settings.topBun].price;
		OrderPrice += Foodstuffs.Ingredients[Foodstuffs.Settings.bottomBun].price;

		// Set the customers order and the price
		Order = order;
		OrderPrice = MathF.Round(OrderPrice, 2);




		// Generate the order dialogue asking thing
		string orderString = "";
		string greeting;
		string want;
		string with;
		string foodType;
		string ingredients;
		string ending;
		
		// Add a random, then way of asking what they want
		greeting = Dialogue.Lines.greetings[random.Next(0, Dialogue.Lines.greetings.Count)] + " ";
		want = Dialogue.Lines.want[random.Next(0, Dialogue.Lines.want.Count)] + " ";

		// Add what type of food they want
		// TODO: Add other food types and whatnot. For example, "burger and a drink"
		foodType = "burger" + " ";

		// Add the "with" part
		with = Dialogue.Lines.with[random.Next(0, Dialogue.Lines.with.Count)] + " ";

		// Add the ingredients
		ingredients = string.Join(", ", Order.Select(ingredient => ingredient.name)) + ". ";

		// Add the ending
		ending = Dialogue.Lines.end[random.Next(0, Dialogue.Lines.end.Count)] + " ";

		// Make the order string
		orderString = Dialogue.Lines.template1Item.Replace("{greeting}", greeting);
		orderString = orderString.Replace("{want}", want);
		orderString = orderString.Replace("{thingName}", foodType);
		orderString = orderString.Replace("{with}", with);
		orderString = orderString.Replace("{ingredients}", ingredients);
		orderString = orderString.Replace("{ending}", ending);

		Console.WriteLine(orderString);
	}

	// Make a receipt showing the customers order
	// TODO: Have one single Y value that is incremented so stuff is dynamic
	public void GenerateOrderReceipt()
	{
		// Dynamic coordinates to fit the receipt content
		float y = 0;
		float x = 10;

		// Get all needed assets
		Font font = new Font("./assets/fonts/MerchantCopy.ttf");
		Font fontWide = new Font("./assets/fonts/MerchantCopyWide.ttf");
		Sprite receiptBackground = new Sprite(new Texture("./assets/receipt-paper.png"));
		uint fontSize = 24;

		// Begin to make the actual receipt
		receipt = new RenderTexture(Game.Window.Size.X, Game.Window.Size.Y);
		receiptBackground.Scale = new Vector2f(0.5f, 0.5f);
		receipt.Draw(receiptBackground);



		// Add the establishments logo
		y += 30;
		Sprite logo = new Sprite(new Texture("./assets/logo-receipt.png"));
		logo.Scale = new Vector2f(3, 2);
		logo.Position = new Vector2f(x, y);
		receipt.Draw(logo);


		// TODO: Don't fill it with newlines and stuff
		// Generate the receipt text about the order
		string itemsTextString = "ORDER SUMMARY =======\n";
		string pricesTextString = "\n";
		foreach (Ingredient ingredient in Order)
		{
			itemsTextString += "> " + ingredient.name + "\n";
			pricesTextString += ingredient.price.ToString("$0.00\n");
		}
		itemsTextString += "=====================\nAMOUNT DUE: " + OrderPrice.ToString("$0.00");

		
		// Make the receipt items text
		y += 130;
		Text itemsText = new Text(itemsTextString, font, fontSize);
		itemsText.Position = new Vector2f(x, y);
		itemsText.FillColor = Color.Black;
		receipt.Draw(itemsText);

		// Make the receipt prices text
		Text pricesText = new Text(pricesTextString, font, fontSize);
		pricesText.Origin = new Vector2f(pricesText.GetGlobalBounds().Width, 0);
		pricesText.Position = new Vector2f(x + 200, y);
		pricesText.FillColor = Color.Black;
		receipt.Draw(pricesText);

		// Make some random stuff at the bottom
		y += itemsText.GetGlobalBounds().Height + 20;
		string bottomTextString = "Thank your for shopping\nat Max Hambur. Next order is\n15% off (not guaranteed)";
		Text bottomText = new Text(bottomTextString, font, 20);
		bottomText.FillColor = Color.Black;
		bottomText.Position = new Vector2f(x, y);
		receipt.Draw(bottomText);

		// Add a barcode with a random number to look cool
		y = receipt.Texture.Size.Y - 120;
		Sprite barcode = GenerateBarcode(new Random().Next(10000, 99999));
		barcode.Position = new Vector2f(x, y);
		receipt.Draw(barcode);

		// Display the final receipt before rendering to stop it from going upside down
		receipt.Display();
	}



	public void RenderReceipt()
	{
		// TODO: Don't make new sprite every time
		Game.Window.Draw(new Sprite(receipt.Texture));
	}


	// Make a barcode
	private Sprite GenerateBarcode(int numberInput)
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
		int barHeight = 50;
		uint width = (uint)(numberInput.ToString().Length * (barWidth * 7));
		RenderTexture barcode = new RenderTexture(width, (uint)(barHeight));

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

					// Color the bars
					bar.FillColor = (numberPatterns[i][j] == true) ? Color.Transparent : Color.Black;

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
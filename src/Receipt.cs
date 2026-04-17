using System.Numerics;
using Raylib_cs;

class Receipt : GameObject
{
	private Font font;
	private Texture2D receiptBackground;
	private Texture2D receiptLogo;

	public Receipt()
	{
		// Load the font
		font = Raylib.LoadFont("./assets/fonts/MerchantCopy.ttf");

		// Load the receipt stuff
		receiptBackground = Raylib.LoadTexture("./assets/receipt-paper.png");
		receiptLogo = Raylib.LoadTexture("./assets/logo-receipt.png");
	}

	public override void Draw()
	{
		Vector2 position = Position;

		Vector2 size = new Vector2(
			Raylib.GetScreenWidth() / 4.5f,
			Raylib.GetScreenHeight()
		);

		// Draw the actual receipt
		Utils.DrawTexture(receiptBackground, position, size);

		// Draw the logo
		position.Y += 20f;
		Vector2 logoSize = new Vector2(
			size.X * 0.9f,
			size.X * 0.6f
		);
		Utils.DrawTextureHorizontallyCentred(
			receiptLogo,
			position,
			size,
			logoSize
		);
		position.Y += logoSize.Y;

		// Make the order summary
		string text = "ORDER SUMMARY ========\n";
		text += "> TOP BUN        $0.10\n";
		text += "> CHEESE         $0.10\n";
		text += "> MEET           $1.20\n";
		text += "> LETTUCE        $0.10\n";
		text += "> TOMATO         $0.20\n";
		text += "> BOTTOM BUN     $0.10\n";
		text += "======================\n";
		text += "AMOUNT DUE: $2.9\n";

		text += "\n\n\n";
		text += "Thanks for shopping at\n";
		text += "Max Hambur, Next order is\n";
		text += "10% off (not guarantee)";

		// Draw the order summary thingy
		position.X += 10f;
		Raylib.DrawTextEx(
			font,
			text,
			position,
			11f,
			11f / 10f,
			Color.Black
		);
		position.Y += 11f * 15f;

		// Draw the barcode
		position.Y += 80;
		logoSize.Y *= 0.3f;
		DrawBarcode(position, logoSize, DateTimeOffset.UtcNow.ToUnixTimeSeconds());
	}

	private void DrawBarcode(Vector2 position, Vector2 size, long number)
	{
		bool[][] patterns = new bool[][]
		{
			new bool[] { false, false, false, true, true, false, true }, // 0
			new bool[] { false, false, true, true, false, false, true }, // 1
			new bool[] { false, false, true, false, false, true, true }, // 2
			new bool[] { false, true, true, true, true, false, true }, // 3
			new bool[] { false, true, false, false, false, true, true }, // 4
			new bool[] { false, true, true, false, false, false, true }, // 5
			new bool[] { false, true, false, true, true, true, true }, // 6
			new bool[] { false, true, true, true, false, true, true }, // 7
			new bool[] { false, true, true, false, true, true, true }, // 8
			new bool[] { false, false, false, true, false, true, true } // 9
		};

		// Stringify the number so we can use each 'digit' as an index
		string numberString = number.ToString();

		// Figure out how large each 'bit' is
		Vector2 bitSize = new Vector2(
			size.X / (numberString.Length * patterns[0].Length),
			size.Y
		);

		// Loop over each digit and draw it
		for (int i = 0; i < numberString.Length; i++)
		{
			// Loop over each 'bit' in the digit
			int patternIndex = int.Parse(numberString[i].ToString());
			bool[] barcodePattern = patterns[patternIndex];
			for (int j = 0; j < barcodePattern.Length; j++)
			{
				// Draw it
				if (barcodePattern[j] == true)
				{
					Raylib.DrawRectangleV(position, bitSize, Color.Black);
				}
				position.X += bitSize.X;
			}
		}
	}

	public override void CleanUp()
	{
		Raylib.UnloadFont(font);
		Raylib.UnloadTexture(receiptBackground);
		Raylib.UnloadTexture(receiptLogo);
	}
}
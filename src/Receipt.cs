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
			Raylib.GetScreenWidth() * 0.25f,
			Raylib.GetScreenHeight() * 0.9f
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
		string[] text = new string[]
		{
			"",
			"ORDER SUMMARY ========",
			"> TOP BUN        $0.10",
			"> CHEESE         $0.10",
			"> MEET           $1.20",
			"> LETTUCE        $0.10",
			"> TOMATO         $0.20",
			"> BOTTOM BUN     $0.10",
			"======================",
			"AMOUNT DUE: $2.9",
			"",
			"",
			"",
			"Thanks for shopping at",
			"Max Hambur.",
			"",
			"nga mihi"
		};

		// Draw the order summary thingy
		position.X += 10f;
		DrawText(ref position, size, text);

		// Draw the barcode
		position.Y += 80;
		logoSize.Y *= 0.3f;
		DrawBarcode(position, logoSize, 1234567894564512312);
	}

	private void DrawText(ref Vector2 position, Vector2 size, string[] text)
	{
		// Figure out how large the font size must be
		float fontSize;
		float fontSpacing;
		{
			// Make a 'default' font to base all our calculations on
			// TODO: Maybe measure with 'M' but I am pretty sure this is monospace anyways
			//? also 22 is just the longest line thing yk 
			// TODO: Don't hardcode 22
			string testText = new String('=', 22);
			const float defaultFontSize = 16f;
			const float defaultFontSpacing = 10f / defaultFontSize;
			float defaultFontWidth = Raylib.MeasureTextEx(font, testText, defaultFontSize, defaultFontSpacing).X;

			// Calculate what the font size should be
			// TODO: Don't do this 0.8 thing
			float scale = size.X / defaultFontWidth;
			fontSize = (defaultFontSize * scale) * 0.8f;
			fontSpacing = 10f / fontSize;
		}

		// Loop over each line and print it
		foreach (string line in text)
		{
			Raylib.DrawTextEx(
				font,
				line,
				position,
				fontSize,
				fontSpacing,
				Color.Black
			);
			position.Y += fontSize + fontSpacing;
		}
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
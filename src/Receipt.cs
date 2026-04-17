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
		position.Y += 50f;
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
	}

	public override void CleanUp()
	{
		Raylib.UnloadFont(font);
		Raylib.UnloadTexture(receiptBackground);
		Raylib.UnloadTexture(receiptLogo);
	}
}
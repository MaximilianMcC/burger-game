using System.Numerics;
using Raylib_cs;

class Foodstuff : GameObject
{
	private bool draggingRn;
	private Vector2 dragOffset;

	private Texture2D rawTexture;
	private Texture2D cookingTexture;
	private Texture2D cookedTexture;
	private Texture2D burntTexture;

	public CookState State = CookState.Raw;
	private float cookTime;

	private Texture2D currentTexture => State switch
	{
		CookState.Raw => rawTexture,
		CookState.Cooking => cookingTexture,
		CookState.Cooked => cookedTexture,
		_ => burntTexture,	
	};

	public Foodstuff(string rawTexturePath, string cookingTexturePath, string cookedTexturePath, string burntTexturePath)
	{
		Size =	new Vector2(64, 32) * 2f;

		rawTexture = Raylib.LoadTexture(rawTexturePath);
		cookingTexture = Raylib.LoadTexture(cookingTexturePath);
		cookedTexture = Raylib.LoadTexture(cookedTexturePath);
		burntTexture = Raylib.LoadTexture(burntTexturePath);
	}

	public override void Update()
	{
		DragAndDrop();

		// Check for if we are on the hob and if so cook
		if (Scene.Get<Hob>().OnHob(this)) Cook();
	}

	private void DragAndDrop()
	{
		// Check for if we are hovering over the thing
		bool hovering = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Rectangle);

		if (draggingRn == false && Raylib.IsMouseButtonPressed(MouseButton.Left) && hovering)
		{
			draggingRn = true;
			dragOffset = Raylib.GetMousePosition() - Position;
		}

		if (draggingRn)
		{
			Position = Raylib.GetMousePosition() - dragOffset;
		}

		if (draggingRn && Raylib.IsMouseButtonReleased(MouseButton.Left))
		{
			draggingRn = false;
			Raylib.SetMouseCursor(MouseCursor.Default);
		}
	}

	private void Cook()
	{
		cookTime += Raylib.GetFrameTime();

		if (cookTime <= 5f) State = CookState.Raw;
		else if (cookTime <= 10f) State = CookState.Cooking;
		else if (cookTime <= 20f) State = CookState.Cooked;
		else if (cookTime <= 30f) State = CookState.Burnt;
	}

	public override void Draw()
	{
		Utils.DrawTexture(currentTexture, Position, Size);
	}

	public override void CleanUp()
	{
		Raylib.UnloadTexture(rawTexture);
		Raylib.UnloadTexture(cookingTexture);
		Raylib.UnloadTexture(cookedTexture);
		Raylib.UnloadTexture(burntTexture);
	}
}

enum CookState
{
	Raw,
	Cooking,
	Cooked,
	Burnt
}
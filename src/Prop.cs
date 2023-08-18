using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Prop
{
	public Vector2f Position { get; set; }
	public RectangleShape sprite;
	private bool dragging;
	private Vector2f dragPosition;

	public Prop()
	{
		sprite = new RectangleShape(new Vector2f(100, 100));
		sprite.FillColor = Color.Red;
	}

	public void Update()
	{
		DragAndDrop();
	}

	public void Render()
	{
		sprite.Position = Position;
		Game.Window.Draw(sprite);
	}




	private void DragAndDrop()
	{
		// Check for if the player's mouse is hovering over the object
		if (sprite.GetGlobalBounds().Contains(Mouse.GetPosition(Game.Window).X, Mouse.GetPosition(Game.Window).Y))
		{
			if (Mouse.IsButtonPressed(Mouse.Button.Left))
			{
				// Toggle dragging
				if (dragging == false)
				{
					dragging = true;
					dragPosition = (Vector2f)Mouse.GetPosition(Game.Window) - Position;
				}
			}
			else dragging = false;
		}

		// Check for if they are dragging
		if (dragging)
		{
			// Calculate the new position
			Position = (Vector2f)Mouse.GetPosition(Game.Window) - dragPosition;
		}
	}

}
using System.Numerics;
using Raylib_cs;

class GameObject
{
	public Vector2 Position;
	public Vector2 Size = new Vector2(16);
	public Rectangle Rectangle => new Rectangle(Position, Size);

	public virtual void Update() { }
	public virtual void Draw() { }
	public virtual void CleanUp() { }
}
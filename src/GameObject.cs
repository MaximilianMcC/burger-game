using System.Numerics;

class GameObject
{
	public Vector2 Position;

	public virtual void Update() { }
	public virtual void Draw() { }
	public virtual void CleanUp() { }
}
static class Scene
{
	public static List<GameObject> GameObjects = [];

	public static T Get<T>() where T : GameObject
	{
		return GameObjects.OfType<T>().FirstOrDefault();
	}

	public static List<T> GetAll<T>() where T : GameObject
	{
		return GameObjects.OfType<T>().ToList();
	}
}
public static class Utils
{
	public static T RandomListElement<T>(List<T> list)
	{
		Random random = new Random();
		return list[random.Next(0, list.Count)];
	}
}
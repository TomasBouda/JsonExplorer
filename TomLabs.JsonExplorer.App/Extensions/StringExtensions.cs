namespace TomLabs.JsonExplorer.App.Extensions
{
	public static class StringExtensions
	{
		public static bool ToBool(this string s)
		{
			bool.TryParse(s, out var res);
			return res;
		}
	}
}
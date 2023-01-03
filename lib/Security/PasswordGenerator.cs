using System.Security.Cryptography;

namespace LibraryAPI.Lib.Security
{
	public static class PasswordGenerator
	{
		private static readonly char[] Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890123456789".ToCharArray();
		//private static readonly char[] Punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();
		private static readonly char[] Punctuations = "".ToCharArray();

		public static string Generate(int length = 8, int numberOfNonAlphanumericCharacters = 0)
		{
			string password = "";
			using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
			{
				do
				{
					Random rnd = new Random();
					int i = rnd.Next(Chars.Length);
					password += Chars[i];
				} while (password.Length < length);
			}
			return password;
		}
	}

}

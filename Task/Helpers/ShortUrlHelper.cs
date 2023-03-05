using System.Text;

namespace TaskI.Helpers
{

    public class ShortUrlHelper
    {

        private static Dictionary<string, string> urlMap = new Dictionary<string, string>();

        public static string Shorten(string originalUrl)
        {

            string key = GenerateKey();
            urlMap.Add(key, originalUrl);
            return key;

        }

        public static string Expand(string shortUrl)
        {

            if (urlMap.ContainsKey(shortUrl))
            {
                return urlMap[shortUrl];
            }
            return null;

        }

        private static string GenerateKey()
        {

            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string key = "";

            for (int i = 0; i < 6; i++)
            {
                int index = random.Next(chars.Length);
                key += chars[index];
            }

            return key;

        }
    }
}
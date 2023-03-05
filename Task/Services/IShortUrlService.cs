using TaskI.Models;

namespace TaskI.Services
{
    public interface IShortUrlService
    {
        ShortUrl GetById(int id);

        ShortUrl GetByPath(string path);

        ShortUrl GetByOriginalUrl(string originalUrl);

        List<ShortUrl> GetAll();

        Users GetUserById(int id);

        void SaveNewUser(Users user);

        bool DeleteUrl(int id);

        Users GetUsersByUsername(string Name);

        int Save(ShortUrl shortUrl);
    }
}

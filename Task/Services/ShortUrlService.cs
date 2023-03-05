using Microsoft.EntityFrameworkCore;
using TaskI.Helpers;
using TaskI.Models;

namespace TaskI.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly TaskDbContext _context;

        public ShortUrlService(TaskDbContext context)
        {
            _context = context;
        }

        public ShortUrl GetById(int id)
        {
            return _context.ShortUrls.Find(id);
        }

        public ShortUrl GetByPath(string path)
        {
            return _context.ShortUrls.Where(x => x.OriginalUrl == ShortUrlHelper.Expand(path.ToString())).First();
        }

        public List<ShortUrl> GetAll()
        {
            return _context.ShortUrls.ToList();
        }

        public ShortUrl GetByOriginalUrl(string originalUrl)
        {
            foreach (var shortUrl in _context.ShortUrls)
            {
                if (shortUrl.OriginalUrl == originalUrl)
                {
                    return shortUrl;
                }
            }

            return null;
        }

        public Users GetUserById(int id)
        {
            Users user = _context.Users.FirstOrDefault(x=>x.UserId==id);

            return user;
        }

        public bool DeleteUrl(int id)
        {
            _context.ShortUrls.Where(x => x.Id == id).ExecuteDelete();

            return true;
        }
        public int Save(ShortUrl shortUrl)
        {
            _context.ShortUrls.Add(shortUrl);
            _context.SaveChanges();

            return shortUrl.Id;
        }

        void IShortUrlService.SaveNewUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        Users IShortUrlService.GetUsersByUsername(string Name)
        {
            var user = _context.Users.Where(x => x.Username == Name);
            try
            {
                return user.First();
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
    }
}

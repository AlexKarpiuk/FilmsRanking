using FilmsRanking.Data;
using FilmsRanking.Data.Enum;
using FilmsRanking.Interfaces;
using FilmsRanking.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmsRanking.Repositories
{
    public class MediaContentRepository : IMediaContentRepository
    {
        private readonly ApplicationDbContext _context;

        public MediaContentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(MediaContent mediaContent)
        {
            _context.Add(mediaContent);
            return Save();
        }

        public bool Delete(MediaContent mediaContent)
        {
            _context.Remove(mediaContent);
            return Save();
        }

        public async Task<IEnumerable<MediaContent>> GetAll()
        {
            return await _context.MediaContent.ToListAsync();
        }

        public async Task<IEnumerable<MediaContent>> GetBySearch(string searchString)
        {
            return await _context.MediaContent.Where(s => s.Name!.Contains(searchString) || s.Director.Contains(searchString)).ToListAsync();
        }

        public async Task<MediaContent> GetByIdAsync(int id)
        {
            return await _context.MediaContent.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<MediaContent> GetByIdNoTrackingAsync(int id)
        {
            return await _context.MediaContent.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(MediaContent mediaContent)
        {
            _context.Update(mediaContent);
            return Save();
        }
    }
}

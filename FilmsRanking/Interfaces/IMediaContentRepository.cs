﻿using FilmsRanking.Data.Enum;
using FilmsRanking.Models;

namespace FilmsRanking.Interfaces
{
    public interface IMediaContentRepository
    {
        Task<IEnumerable<MediaContent>> GetAll();
        Task<MediaContent> GetByIdAsync(int id);
        Task<MediaContent> GetByIdNoTrackingAsync(int id);
        Task<IEnumerable<MediaContent>> GetBySearch(string searchString);

        bool Add(MediaContent mediaContent);
        bool Update(MediaContent mediaContent);
        bool Delete(MediaContent mediaContent);
        bool Save();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadioArchive
{
    public interface IPodcastApiService
    {
        Task<List<PodcastApi>> GetLastShowsAsync();
        Task<List<PodcastApi>> GetTopRatedShowsAsync();
        Task<List<PodcastApi>> GetShowsWithOffsetAsync(string offset);
        Task<List<PodcastApi>> GetShowsWithSpecificDateAsync(int year, int month);
    }
}

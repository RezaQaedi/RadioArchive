using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadioArchive
{
    public interface IPodcastApiService
    {
        Task<List<PodcastURL>> GetLastShowsAsync();
        Task<List<PodcastURL>> GetTopRatedShowsAsync();
        Task<List<PodcastURL>> GetShowsWithOffsetAsync(string offset);
    }
}

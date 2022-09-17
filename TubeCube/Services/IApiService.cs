using TubeCube.Models;

namespace TubeCube.Services;

public interface IApiService
{
    Task<VideoSearchResult?> GetVideos(string querry, string? nextPageToken = null);
}

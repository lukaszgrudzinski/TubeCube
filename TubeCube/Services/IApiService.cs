using TubeCube.Models;

namespace TubeCube.Services;

internal interface IApiService
{
    Task<VideoSearchResult?> GetVideos(string querry, string? nextPageToken = null);
}

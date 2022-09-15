using MonkeyCache;
using System.Net;
using TubeCube.Framework.Services;
using TubeCube.Models;

namespace TubeCube.Services;

internal class YoutubeRestService : RestServiceBase, IApiService
{
    public YoutubeRestService(IConnectivity connectivity, IBarrel cahceBarrel) : base(connectivity, cahceBarrel)
    {
        SetBaseUrl(Constants.BaseApiUrl);
    }

    public async Task<VideoSearchResult?> GetVideos(string querry, string? nextPageToken = null)
    {
        string resourceUri = $"{Constants.BaseApiUrl}search?part=snippet&maxResults=10&q={WebUtility.UrlEncode(querry)}&type=video&key={Constants.ApiKey}";
        if (nextPageToken != null)
            resourceUri += $"&pageToken={nextPageToken}";

        return await GetAsync<VideoSearchResult>(resourceUri, TimeSpan.FromMinutes(30));
    }
}

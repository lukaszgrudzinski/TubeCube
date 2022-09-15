namespace TubeCube.Models
{
    public record Id(
        string Kind,
        string VideoId
    );

    public record YoutubeVideo(
        string Kind,
        string Etag,
        Id Id,
        Snippet Snippet
    );

    public record Thumbnail(
        string Url,
        int Width,
        int Height
    );

    public record PageInfo(
        int TotalResults,
        int ResultsPerPage
    );

    public record VideoSearchResult(
        string Kind,
        string Etag,
        string NextPageToken,
        string RegionCode,
        PageInfo PageInfo,
        IReadOnlyList<YoutubeVideo> Items
    );

    public record Snippet(
        DateTime PublishedAt,
        string ChannelId,
        string Title,
        string Description,
        Thumbnails Thumbnails,
        string ChannelTitle,
        string LiveBroadcastContent,
        DateTime PublishTime
    );

    public record Thumbnails(
        Thumbnail Default,
        Thumbnail Medium,
        Thumbnail High
    );
}

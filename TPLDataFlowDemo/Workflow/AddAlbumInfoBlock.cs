namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;

public static class AddAlbumInfoBlock
{
    public static TransformBlock<Album, AlbumCommercial> Create(CancellationToken cancellationToken) =>
        new((album) => new AlbumCommercial()
        {
            ReleaseYear = album.ReleaseYear,
            Title = album.Title,
            ArtistName = album.ArtistName,
        });
}
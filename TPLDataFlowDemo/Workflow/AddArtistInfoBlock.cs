namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;

public static class AddArtistInfoBlock
{
    public static TransformBlock<AlbumCommercial, AlbumCommercial> Create(CancellationToken cancellationToken) 
        => new((albumCommercial) => albumCommercial);
}
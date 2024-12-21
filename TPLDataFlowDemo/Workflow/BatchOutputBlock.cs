namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;

public static class BatchOutputBlock
{
    public static BatchBlock<AlbumCommercial> Create(CancellationToken cancellationToken, int batchSize) =>
        new BatchBlock<AlbumCommercial>(batchSize, 
            new GroupingDataflowBlockOptions
            {
                CancellationToken = cancellationToken,
                BoundedCapacity = batchSize,
            });

}
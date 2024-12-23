namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;

public static class StartBlock
{
    public static BufferBlock<Organization> Create(CancellationToken cancellationToken) =>
        new(new DataflowBlockOptions()
        {
            CancellationToken = cancellationToken,
            BoundedCapacity = 50,
        });
}
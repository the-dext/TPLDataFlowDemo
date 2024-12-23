using OneOf.Types;
using OneOf;
namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;

public static class BatchOutputBlock
{
	public static BatchBlock<OneOf<Organization, None>> Create(CancellationToken cancellationToken, int batchSize) =>
		new(batchSize,
			new GroupingDataflowBlockOptions
			{
				CancellationToken = cancellationToken,
				BoundedCapacity = batchSize,
			});

}
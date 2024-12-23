using OneOf.Types;
using OneOf;

namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;

public static class BatchOutputBlock
{
	/// <summary>
	/// A demonstration block showing how outputs from the workflow could be batched up before being
	/// dispatched to the final block. For example this could be used to send writes to the database
	/// in batches.
	/// </summary>
	/// <param name="cancellationToken"> </param>
	/// <param name="batchSize"> </param>
	/// <returns> </returns>
	public static BatchBlock<OneOf<Organization, None>> Create(CancellationToken cancellationToken, int batchSize) =>
		new(batchSize,
			new GroupingDataflowBlockOptions
			{
				CancellationToken = cancellationToken,
				BoundedCapacity = batchSize,
			});
}
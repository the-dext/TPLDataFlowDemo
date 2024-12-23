namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;

public static class StartBlock
{
	/// <summary>
	/// A demonstration block that accepts inputs and buffers them ready to be processed by the workflow
	/// </summary>
	/// <param name="cancellationToken"></param>
	public static BufferBlock<Organization> Create(CancellationToken cancellationToken) =>
		new(new DataflowBlockOptions()
		{
			CancellationToken = cancellationToken,
			BoundedCapacity = 50,
		});
}
using System.Threading.Tasks.Dataflow;

namespace Workflow
{
	public class Workflow
	{
		public async Task StartWorkflow(CancellationToken cancellationToken)
		{
			var startBlock = new BufferBlock<int>();
			var transformBlock1 = new TransformBlock<int, int>(n => n + 1);
			var transformBlock2 = new TransformBlock<int, int>(n => n * 2);
			var transformBlock3 = new TransformBlock<int, int>(n => n - 3);
			var finalBlock = new ActionBlock<>()
		}
	}
}

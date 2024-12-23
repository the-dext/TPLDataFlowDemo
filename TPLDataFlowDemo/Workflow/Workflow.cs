using System.Threading.Tasks.Dataflow;
using Domain;
using OneOf;
using OneOf.Types;

namespace Workflow;

public class Workflow
{
	private ActionBlock<IEnumerable<OneOf<Organization, None>>> _outputSelectionBlock;
	private BufferBlock<Organization> _startBlock;

	/// <summary>
	/// Creates a workflow for the demonstration, returning the start block for pushing data into
	/// and the end block to await the end of the workflow processing.
	/// </summary>
	/// <param name="cancellationToken"> </param>
	/// <param name="consoleWriter"> </param>
	/// <returns> </returns>
	public (BufferBlock<Organization> startBlock, ActionBlock<IEnumerable<OneOf<Organization, None>>> endBlock)
		CreateBlocks(CancellationToken cancellationToken, Action<string> consoleWriter)
	{
		_startBlock = StartBlock.Create(cancellationToken);
		var filterByCountry = FilterByIndustryBlock.Create(cancellationToken);
		var applySelectionRulesBlock = ApplySelectionRulesBlock.Create(cancellationToken);
		var bufferResultsBlock = BatchOutputBlock.Create(cancellationToken, 10);
		_outputSelectionBlock = OutputSelectionBlock.Create(cancellationToken, consoleWriter);

		var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
		_startBlock.LinkTo(filterByCountry, linkOptions);
		filterByCountry.LinkTo(applySelectionRulesBlock, linkOptions);
		applySelectionRulesBlock.LinkTo(bufferResultsBlock, linkOptions);
		bufferResultsBlock.LinkTo(_outputSelectionBlock, linkOptions);

		return (_startBlock, _outputSelectionBlock);
	}
}
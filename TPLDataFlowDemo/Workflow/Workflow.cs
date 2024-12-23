
using OneOf.Types;
using OneOf;
namespace Workflow;

using System.Threading.Tasks.Dataflow;
using System.Text;
using Domain;
public class Workflow
{
	private BufferBlock<Organization> _startBlock;
	private ActionBlock<OneOf<Organization, None>> _outputSelectionBlock;

	public (BufferBlock<Organization> startBlock, ActionBlock<OneOf<Organization, None>> endBlock) CreateBlocks(CancellationToken cancellationToken, Action<string> consoleWriter)
	{
		_startBlock = StartBlock.Create(cancellationToken);
		var filterByCountry = FilterByIndustryBlock.Create(cancellationToken);
		var applySelectionRulesBlock = ApplySelectionRulesBlock.Create(cancellationToken); 
		// var bufferResultsBlock = BatchOutputBlock.Create(cancellationToken, batchSize:10);
		_outputSelectionBlock = OutputSelectionBlock.Create(cancellationToken,consoleWriter);

		var linkOptions = new DataflowLinkOptions { PropagateCompletion = true, };
		_startBlock.LinkTo(filterByCountry, linkOptions);
		filterByCountry.LinkTo(applySelectionRulesBlock, linkOptions);
		applySelectionRulesBlock.LinkTo(_outputSelectionBlock, linkOptions);
		//bufferResultsBlock.LinkTo(_outputSelectionBlock, linkOptions);

		return (_startBlock, _outputSelectionBlock);
	}
}
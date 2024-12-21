
namespace Workflow;

using System.Threading.Tasks.Dataflow;
using System.Text;
using Domain;
public class Workflow
{
	private BufferBlock<Album> _startBlock;
	private ActionBlock<AlbumCommercial> _saveCommercialsBlock;

	public (BufferBlock<Album> startBlock, ActionBlock<AlbumCommercial> endBlock) CreateBlocks(CancellationToken cancellationToken, Action<string> consoleWriter)
	{
		_startBlock = StartBlock.Create(cancellationToken);
		var addAlbumInfo = AddAlbumInfoBlock.Create(cancellationToken);
		var addArtistInfo = AddArtistInfoBlock.Create(cancellationToken); 
		var addSocialMediaBlock = AddSocialMediaBlock.Create(cancellationToken);
		// var bufferResultsBlock = BatchOutputBlock.Create(cancellationToken, batchSize:10);
		_saveCommercialsBlock = SaveCommercialsBlock.Create(cancellationToken,consoleWriter);

		var linkOptions = new DataflowLinkOptions { PropagateCompletion = true, };
		_startBlock.LinkTo(addAlbumInfo, linkOptions);
		addAlbumInfo.LinkTo(addArtistInfo, linkOptions);
		addArtistInfo.LinkTo(addSocialMediaBlock, linkOptions);
		addSocialMediaBlock.LinkTo(_saveCommercialsBlock, linkOptions);
		//bufferResultsBlock.LinkTo(_saveCommercialsBlock, linkOptions);

		return (_startBlock, _saveCommercialsBlock);
	}
}
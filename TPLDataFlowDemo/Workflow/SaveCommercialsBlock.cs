namespace Workflow;

using System.Text;
using System.Threading.Tasks.Dataflow;
using Domain;

public static class SaveCommercialsBlock
{
    public static ActionBlock<AlbumCommercial> Create(CancellationToken cancellationToken,
        Action<string> consoleWriter) =>
        new((results) =>
        {
            consoleWriter(results.GetCommercialText());

            // var sb = new StringBuilder();
            // if (!cancellationToken.IsCancellationRequested)
            // {
            //     sb.Clear();
            //     foreach (var albumCommercial in results)
            //     {
            //         sb.AppendLine(albumCommercial.GetCommercialText());
            //     }
            //
            //     consoleWriter(sb.ToString());
            //     Task.Delay(100, cancellationToken).Wait(cancellationToken);
            // }
        });
}
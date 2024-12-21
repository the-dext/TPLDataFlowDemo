// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Threading.Tasks.Dataflow;
using CsvHelper;
using CsvHelper.Configuration;
using Domain;
using Infrastructure.Repositories;

Console.WriteLine("TPL DataFlow Demo");
Console.WriteLine("Press any key to begin");
Console.ReadKey();

var cts = new CancellationTokenSource();
var workflow = new Workflow.Workflow();
var consoleWriter = (string s) => Console.WriteLine(s);
var (startBlock, endBlock) = workflow.CreateBlocks(cts.Token, consoleWriter);

const string filePath = "./albums.csv";
if (!File.Exists(filePath))
    throw new FileNotFoundException(filePath);

var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    HasHeaderRecord = true,
    Delimiter = ","
};
using var streamReader = new StreamReader(filePath);
using var csv = new CsvReader(streamReader, config);

csv.Read();
csv.ReadHeader();
int postCount = 0;
while (csv.Read())
{
    var album = new Album(
        csv.GetField<string>("Title"),
        csv.GetField<string>("Genre"),
        csv.GetField<int>("ReleaseYear"),
        csv.GetField<string>("ArtistName")
    );
    startBlock.Post(album);
    postCount++;
}

Console.WriteLine($"Posted {postCount} albums");
startBlock.Complete();
await endBlock.Completion;
Console.WriteLine("All processed");

Console.WriteLine("Exited");
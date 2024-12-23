// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks.Dataflow;
using CsvHelper;
using CsvHelper.Configuration;
using Infrastructure.Repositories;

Console.WriteLine("TPL DataFlow Demo");
Console.WriteLine("This demo will read a csv file containing organisations are filter them down through two workflow steps, outputting only the results which passed all checks.");
Console.WriteLine("The rules are");
Console.WriteLine("\tThe Organization must be in one of the following industries: Cosmetics, Plastics or Research Industry");
Console.WriteLine("\tThe Organization must be located outside of the United States of America.");
Console.WriteLine("\tThe Organization must be Founded after 1985");
Console.WriteLine("\tThe Organization must be Employ over 5,000 Employees");
Console.WriteLine("Press any key to begin processing the CSV file");
Console.ReadKey();

var cts = new CancellationTokenSource();
var workflow = new Workflow.Workflow();
var consoleWriter = (string s) =>
{
	var defaultColor = Console.ForegroundColor;
	Console.ForegroundColor = ConsoleColor.Cyan;
	Console.WriteLine(s);
	Console.ForegroundColor = defaultColor;
};
var (startBlock, endBlock) = workflow.CreateBlocks(cts.Token, consoleWriter);


// Create a Stopwatch instance
Stopwatch stopwatch = new Stopwatch();

const string filePath = "./organizations.csv";
if (!File.Exists(filePath))
	throw new FileNotFoundException(filePath);

var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
	HasHeaderRecord = true,
	Delimiter = ","
};
using var streamReader = new StreamReader(filePath);
using var csv = new CsvReader(streamReader, config);
var repository = new OrganisationRepository();

int postCount = 0;
stopwatch.Start();
foreach (var organization in repository.ReadAll(csv))
{
	startBlock.Post(organization);
	postCount++;
}

Console.WriteLine($"Posted {postCount} organisations into the workflow.");
startBlock.Complete();
await endBlock.Completion;

// Stop the stopwatch after the block has completed
stopwatch.Stop();

// Output the elapsed time
Console.WriteLine($"Processing complete. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
Console.ReadLine();


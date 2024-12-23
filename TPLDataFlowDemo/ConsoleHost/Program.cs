// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Threading.Tasks.Dataflow;
using ConsoleHost;

Console.WriteLine("TPL DataFlow Demo");
Console.WriteLine(
	"This demo will read a csv file containing organisations are filter them down through two workflow steps, outputting only the results which passed all checks.");
Console.WriteLine("The rules are");
Console.WriteLine(
	"\tThe Organization must be in one of the following industries: Cosmetics, Plastics or Research Industry");
Console.WriteLine("\tThe Organization must be located outside of the United States of America.");
Console.WriteLine("\tThe Organization must be Founded after 1985");
Console.WriteLine("\tThe Organization must be Employ over 5,000 Employees");
Console.WriteLine("int postCount = 0;\r\nstopwatch.Start();");

Console.WriteLine("Press any key to begin processing the CSV file");
Console.ReadKey();

var cts = new CancellationTokenSource();
var workflow = new Workflow.Workflow();

void OnResultCallback(string s)
{
	var defaultColor = Console.ForegroundColor;
	Console.ForegroundColor = ConsoleColor.Cyan;
	Console.WriteLine(s);
	Console.ForegroundColor = defaultColor;
}

var (startBlock, endBlock) = workflow.CreateBlocks(cts.Token, OnResultCallback);

// For a 2 million line csv download it and then uncomment this line, with the correct folder location.
//const string filePath = @"C:\tmp\organizations-2000000.csv";
const string filePath = "./organizations.csv";

// Create a Stopwatch instance
var stopwatch = new Stopwatch();
stopwatch.Start();

// Workflow Processing. Read each organisation line by line and post the organisation into the workflow for processing. After all lines posted mark the start block as completed and
// wait for the end block to complete.
var postCount = 0;
var fileReader = new CsvFileReader(filePath);
foreach (var organization in fileReader.ReadAll())
{
	startBlock.Post(organization);
	postCount++;
}

startBlock.Complete();
await endBlock.Completion;
// End of Workflow

// Output the elapsed time
stopwatch.Stop();
Console.WriteLine($"Posted {postCount} organisations into the workflow.");
Console.WriteLine($"Processing complete. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
Console.ReadLine();
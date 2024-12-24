using System.Diagnostics;
using System.Threading.Tasks.Dataflow;
using Domain;

namespace ConsoleHost;

public class Program
{
	// For a 2 million line csv download it from https://www.datablist.com/learn/csv/download-sample-csv-files#organizations-dataset
	// and then uncomment this line, with the correct folder location.
	// private const string FilePath = @"C:\tmp\organizations-2000000.csv";
	private const string FilePath = "./organizations.csv";

	private static readonly string[] AllowedIndustries = ["Cosmetics", "Plastics", "Research Industry"];

	private static async Task Main(string[] args)
	{
		PrintDemoInformation();

		Console.WriteLine("");
		Console.WriteLine("Press 1 for TPL");
		Console.WriteLine("Press 2 for Traditional");
		var mode = Console.ReadLine();

		Console.WriteLine("");
		Console.WriteLine(
			"How many times to repeat the file (simulate extremely large files by processing more than once)");
		var iterations = int.Parse(Console.ReadLine());

		var postCount = 0;
		// Create a Stopwatch instance
		var stopwatch = new Stopwatch();
		stopwatch.Start();

		if (mode == "1")
		{
			var cts = new CancellationTokenSource();
			var workflow = new Workflow.Workflow();
			var (startBlock, endBlock) = workflow.CreateBlocks(cts.Token, OnResultCallback);

			// WorkflowService Processing. Read each organisation line by line and post the organisation into the workflow for processing.
			// After all lines posted mark the start block as completed and wait for the end block to complete.
			for (var i = 0; i < iterations; i++)
			{
				var fileReader = new CsvFileReader(FilePath);
				foreach (var organization in fileReader.ReadAll())
				{
					startBlock.Post(organization);
					postCount++;
				}
			}

			startBlock.Complete();
			await endBlock.Completion;
			// End of WorkflowService
		}
		else if (mode == "2")
		{
			var cts = new CancellationTokenSource();
			for (var i = 0; i < iterations; i++)
			{
				var fileReader = new CsvFileReader(FilePath);
				foreach (var organization in fileReader.ReadAll())
				{
					ProcessWithoutTPL(organization, OnResultCallback, cts.Token);
					postCount++;
				}
			}
		}

		// Output the elapsed time
		stopwatch.Stop();
		Console.WriteLine($"Posted {postCount} organisations into the workflow.");
		Console.WriteLine($"Processing complete. Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
		Console.ReadLine();
	}

	private static void PrintDemoInformation()
	{
		Console.WriteLine("TPL DataFlow Demo");
		Console.WriteLine(
			"This demo will read a csv file containing organisations are filter them down through two workflow steps, outputting only the results which passed all checks.");
		Console.WriteLine("The rules are");
		Console.WriteLine(
			"\tThe Organization must be in one of the following industries: Cosmetics, Plastics or Research Industry");
		Console.WriteLine("\tThe Organization must be located outside of the United States of America.");
		Console.WriteLine("\tThe Organization must be Founded after 1985");
		Console.WriteLine("\tThe Organization must be Employ over 5,000 Employees");
		Console.WriteLine("");
		Console.WriteLine(
			"For comparison you may run the workflow through TPL.DataFLow or as a traditional for-each loop. At the end of the workflow the processing time will be displayed.");
	}

	private static void OnResultCallback(string s)
	{
		var defaultColor = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine(s);
		Console.ForegroundColor = defaultColor;
	}

	private static void ProcessWithoutTPL(Organization organization, Action<string> selectedOrganisationCallback, CancellationToken ctsToken)
	{
		if (ctsToken.IsCancellationRequested)
			return;

		var passedChecks = AllowedIndustries.Contains(organization.Industry);

		if (organization.Founded <= 1985)
			passedChecks = false;
		else if (organization.Country.Equals("United States of America", StringComparison.InvariantCultureIgnoreCase))
			passedChecks = false;
		else if (organization.NumberOfEmployees <= 5_000)
			passedChecks = false;

		if (passedChecks)
		{
			var outputString =
				$"Found {organization.Name} ({organization.Industry} - {organization.Country}), {Environment.NewLine}\tFounded in: {organization.Founded} and employing: {organization.NumberOfEmployees} people.{Environment.NewLine}";
			selectedOrganisationCallback(outputString);
		}
	}
}
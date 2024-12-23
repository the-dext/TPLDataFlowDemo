using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain;
using Infrastructure.Repositories;

namespace ConsoleHost
{
	internal class CsvFileReader(string filePath)
	{
		public IEnumerable<Organization> ReadAll()
		{
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
			foreach (var organization in repository.ReadAll(csv))
			{
				yield return organization;
			}
		}
	}
}

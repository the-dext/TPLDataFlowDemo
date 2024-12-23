using Domain;
using CsvHelper;

namespace Infrastructure.Repositories;

public class OrganisationRepository
{
	public IEnumerable<Organization> ReadAll(CsvReader csv)
	{
		csv.Read();
		csv.ReadHeader();
		while (csv.Read())
		{
			var organization = new Organization(
				csv.GetField<int>("Index"),
				csv.GetField<string>("Organization Id"),
				csv.GetField<string>("Name"),
				csv.GetField<string>("Website"),
				csv.GetField<string>("Country"),
				csv.GetField<string>("Description"),
				csv.GetField<int>("Founded"),
				csv.GetField<string>("Industry"),
				csv.GetField<int>("Number of employees")
			);
			yield return organization;
		}
	}
}
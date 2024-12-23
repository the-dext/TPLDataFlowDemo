namespace Domain;

public class Organization(
	int Index,
	string OrganizationId,
	string Name,
	string Website,
	string Country,
	string Description,
	int Founded,
	string Industry,
	int NumberOfEmployees)
{
	public int Index { get; } = Index;
	public string OrganizationId { get; } = OrganizationId;
	public string Name { get; } = Name;
	public string Website { get; } = Website;
	public string Country { get; } = Country;
	public string Description { get; } = Description;
	public int Founded { get; } = Founded;
	public string Industry { get; } = Industry;
	public int NumberOfEmployees { get; } = NumberOfEmployees;
	public bool IsSelected { get; set; }
}
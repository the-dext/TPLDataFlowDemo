using OneOf.Types;
using OneOf;
namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;

public static class ApplySelectionRulesBlock
{
	public static TransformBlock<OneOf<Organization, None>, OneOf<Organization,None>> Create(CancellationToken cancellationToken) 
		=> new((organization) =>
		{
			return organization.Match(
				ApplySelectionRules,
				none => organization
			);
		});

	private static OneOf<Organization, None> ApplySelectionRules(Organization organization)
	{
		/* rules being applied are
		   Founded after 1985,
		   Not within the United States of America
		   Greater than 5,000 Employees
		*/
		bool passedChecks = true;

		if (organization.Founded > 1985)
			passedChecks = false;
		else if (organization.Country.Equals("United States of America", StringComparison.InvariantCultureIgnoreCase))
			passedChecks = false;
		else if (organization.NumberOfEmployees <= 5_000)
			passedChecks = false;

		return passedChecks ? organization : new None();

	}
}
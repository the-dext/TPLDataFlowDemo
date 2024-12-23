using System.Threading.Tasks.Dataflow;
using Domain;
using OneOf;
using OneOf.Types;

namespace Workflow;

public static class ApplySelectionRulesBlock
{
	/// <summary>
	///     A Demonstration of how a single block might apply a number of rules to decide if the incoming data is valid to be
	///     passed on to the next block.
	///     Performs an action on the organization if it passed workflow checks.
	///     The callback passed into this method is purely for demo purposes.
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public static TransformBlock<OneOf<Organization, None>, OneOf<Organization, None>> Create(
		CancellationToken cancellationToken)
	{
		return new TransformBlock<OneOf<Organization, None>, OneOf<Organization, None>>(organization =>
			{
				return organization.Match(
					ApplySelectionRules,
					none => organization
				);
			},
			new ExecutionDataflowBlockOptions
			{
				CancellationToken = cancellationToken
			});
	}

	private static OneOf<Organization, None> ApplySelectionRules(Organization organization)
	{
		/* rules being applied are
		   Founded after 1985,
		   Not within the United States of America
		   Greater than 5,000 Employees.
		   TODO: Dependency Inject these.
		*/
		var passedChecks = true;

		if (organization.Founded > 1985)
			passedChecks = false;
		else if (organization.Country.Equals("United States of America", StringComparison.InvariantCultureIgnoreCase))
			passedChecks = false;
		else if (organization.NumberOfEmployees <= 5_000)
			passedChecks = false;

		return passedChecks ? organization : new None();
	}
}
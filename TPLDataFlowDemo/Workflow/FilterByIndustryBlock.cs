using OneOf.Types;

namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;
using OneOf;

public static class FilterByIndustryBlock
{
	private static readonly string[] AllowedIndustries = ["Cosmetics", "Plastics", "Research Industry"];

	/// <summary>
	/// A demonstration block that filters a record by a single rule.
	/// In this example if the rule passes then the record is passed on. If it isn't then None is returned.
	/// Instead of returning None it would be easy to return the data item with different properties set or audit trails added.
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public static TransformBlock<Organization, OneOf<Organization, None>> Create(CancellationToken cancellationToken) =>
		new(organization =>
			AllowedIndustries.Contains(organization.Industry)
			? organization
			: new None(),
			new ExecutionDataflowBlockOptions()
			{
				CancellationToken = cancellationToken
			});
}
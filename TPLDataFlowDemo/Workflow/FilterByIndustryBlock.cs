using OneOf.Types;

namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;
using OneOf;

public static class FilterByIndustryBlock
{
	private static readonly string[] AllowedIndustries = ["Cosmetics", "Plastics", "Research Industry"];

	public static TransformBlock<Organization, OneOf<Organization, None>> Create(CancellationToken cancellationToken) =>
		new(organization => 
			AllowedIndustries.Contains(organization.Industry) 
			? organization
			: new None());
}
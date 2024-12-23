namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;
using OneOf.Types;
using OneOf;

public static class OutputSelectionBlock
{
	// Performs an action on the organization if it passed workflow checks.
	// The callback passed into this method is purely for demo purposes.

	public static ActionBlock<OneOf<Organization, None>> Create(CancellationToken cancellationToken,
		Action<string> selectedOrganisationCallback) =>
		new((organization) =>
		{
			organization.Switch(
				organization =>
				{
					var outputString = $"Found {organization.Name} ({organization.Industry} - {organization.Country}), {System.Environment.NewLine}\tFounded in: {organization.Founded} and employing: {organization.NumberOfEmployees} people.{System.Environment.NewLine}";
					selectedOrganisationCallback(outputString);
				},
				none =>
				{
					// Do nothing, the result has not passed checks earlier in the workflow.
				});
		});
}
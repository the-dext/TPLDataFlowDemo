namespace Workflow;

using System.Threading.Tasks.Dataflow;
using Domain;
using OneOf.Types;
using OneOf;

public static class OutputSelectionBlock
{
	/// <summary>
	/// A demonstration block to accept an organization as the final output of the workflow and perform some action with it.
	/// This could be writing to a database but in this example a callback is used to write the result to the console.
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <param name="selectedOrganisationCallback"></param>
	/// <returns></returns>
	public static ActionBlock<IEnumerable<OneOf<Organization, None>>> Create(CancellationToken cancellationToken,
		Action<string> selectedOrganisationCallback) =>
		new((organizations) =>
		{
			foreach (var organization in organizations)
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
					}
				);
			}
		},
		new ExecutionDataflowBlockOptions()
		{
			CancellationToken = cancellationToken
		});
}
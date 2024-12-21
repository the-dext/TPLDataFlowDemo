using Domain;

namespace Infrastructure.Repositories;

public class ArtistRepository
{
	private readonly List<Artist> _artists =
	[
		new Artist("Pink Floyd", "United Kingdom", new DateTime(1965, 1, 1)),
		new Artist("Michael Jackson", "United States", new DateTime(1958, 8, 29)),
		new Artist("AC/DC", "Australia", new DateTime(1973, 1, 1)),
		new Artist("Fleetwood Mac", "United Kingdom", new DateTime(1967, 1, 1)),
		new Artist("The Beatles", "United Kingdom", new DateTime(1960, 1, 1))
	];

	public IEnumerable<Artist> GetAll() =>
		_artists.AsEnumerable();

	public Artist GetByName(string name) =>
		_artists.Single(x => x.Name == name);

	public IEnumerable<Artist> GetByCountry(string country) =>
		_artists.Where(x => x.Country == country);
}
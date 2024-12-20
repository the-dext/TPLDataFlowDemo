using Domain;

namespace Infrastructure.Repositories
{
	public class ArtistInfoRepository
	{
		private List<ArtistInfo> infos = new()
		{
			new ArtistInfo("Pink Floyd was an English rock band formed in London in 1965.", "https://www.pinkfloyd.com",
				"@pinkfloyd"),
			new ArtistInfo("Michael Jackson was an American singer, songwriter, and dancer.",
				"https://www.michaeljackson.com", "@michaeljackson"),
			new ArtistInfo("AC/DC are an Australian rock band formed in Sydney in 1973.", "https://www.acdc.com",
				"@acdc"),
			new ArtistInfo("Fleetwood Mac is a British-American rock band formed in London in 1967.",
				"https://www.fleetwoodmac.com", "@fleetwoodmac"),
			new ArtistInfo("The Beatles were an English rock band formed in Liverpool in 1960.",
				"https://www.thebeatles.com", "@thebeatles")
		};

		public IEnumerable<ArtistInfo> GetAll() =>
			infos.AsEnumerable();
	}
}

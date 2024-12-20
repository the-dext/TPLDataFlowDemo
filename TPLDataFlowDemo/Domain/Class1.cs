using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	/// <summary>
	/// The is the target output of the workflow
	/// </summary>
	public class AlbumCommercial
	{
		public string Title { get; set;}
		public string Genre { get; set;}
		public int ReleaseYear { get; set;}
		public string ArtistName { get; set;}
		public string ArtistCountry { get; set;}
		public string ArtistBiography { get; set;}
		public string ArtistWebsite { get; set; }

		public string GetCommercialText()
		{
			return $"Introducing '{Title}', the latest {Genre} sensation from {ArtistName}! " +
			       $"Released in {ReleaseYear}, this album is a must-listen for all music lovers. " +
			       $"{ArtistName}, hailing from {ArtistCountry}, has captivated audiences worldwide. " +
			       $"Learn more about the artist: {ArtistBiography}. Visit their official website: {ArtistWebsite}. " +
			       "Don't miss out on this incredible musical journey!";
		}
		}
	}
}

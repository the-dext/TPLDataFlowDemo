using Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Infrastructure.Repositories
{
	public class AlbumRepository
	{
		private const string _filePath = "./albums.csv";

		private List<Album> _albums ;

		public IEnumerable<Album> GetAll() =>
			_albums.AsEnumerable();

		public IEnumerable<Album> TakePage(int page, int pageSize)
		{
			return _albums.Skip(page*pageSize)
				.Take(pageSize);
		}

		public Album GetByTitle(string title) =>
			_albums.Single(x => x.Title == title);

		public IEnumerable<Album> GetByYear(int year) =>
			_albums.Where(x => x.ReleaseYear == year);

		private void LoadData()
		{
			_albums = new List<Album>();

			var config = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				HasHeaderRecord = true,
			};

			using var reader = new StreamReader(_filePath);
			using var csv = new CsvReader(reader, config);
			while (csv.Read())
			{
				var album = new Album(
					csv.GetField<string>("Title"),
					csv.GetField<string>("Genre"),
					csv.GetField<int>("ReleaseYear"),
					csv.GetField<string>("ArtistName")
				);
				_albums.Add(album);
			}
		}
	}
}

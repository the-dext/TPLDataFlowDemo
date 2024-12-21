using Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Infrastructure.Repositories;

public class AlbumRepository
{
    public IEnumerable<Album> ReadItems(CsvReader csv)
    {
        csv.ReadHeader();
        while (csv.Read())
        {
            var album = new Album(
                csv.GetField<string>("Title"),
                csv.GetField<string>("Genre"),
                csv.GetField<int>("ReleaseYear"),
                csv.GetField<string>("ArtistName")
            );
            yield return album;
        }
    }
}
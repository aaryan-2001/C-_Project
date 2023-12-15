using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryApp.Entity;
using VirtualArtGalleryApp.Utility;

namespace VirtualArtGalleryApp.Repository
{
    internal class ArtistRepository : IArtistInterface
    {
        List<Artist> artists = new List<Artist>();
        public string connectionString;
        SqlCommand cmd = null;

        /*public void  AddArtist(Artist artist)
        {
            var ArtistExists = GetArtistById(artist.ArtistID);
            if (ArtistExists == null)
            {
                artists.Add(artist);
                Console.WriteLine($"Artist added: {artist}");
            }

        }

        public void UpdateArtist(Artist artist)
        {
            Artist existingArtist = artists.Find(a => a.ArtistID== artist.ArtistID);

            if (existingArtist != null)
            {
                existingArtist.Name = artist.Name;
                existingArtist.Biography = artist.Biography;
                existingArtist.BirthDate = artist.BirthDate;
                existingArtist.ContactInformation = artist.ContactInformation;
                existingArtist.Nationality = artist.Nationality;
                existingArtist.Website = artist.Website;

                Console.WriteLine($"Artist updated successfully {existingArtist}");
            }
            else
            {
                Console.WriteLine("Artist not found.");
            }
        }

        public void RemoveArtist(int artistId)
        {
            Artist artistToRemove = artists.Find(a => a.ArtistID == artistId);
            if (artistToRemove != null)
            {
                artists.Remove(artistToRemove);
                Console.WriteLine($"Artist removed successfully {artistToRemove}");
            }
            else
            {
                Console.WriteLine("Artist not found.");
                
            }
        }

        public Artist GetArtistById(int artistId)
        {
            return artists.Find(a => a.ArtistID == artistId);
        }*/

        public ArtistRepository() 
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }

        public Artist GetArtistById(int artistId)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Artists WHERE ArtistID = @ArtistID";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@ArtistID", artistId);

                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Artist artist = new Artist
                                {
                                    ArtistID = (int)reader["ArtistID"],
                                    Name = reader["Name"].ToString(),
                                    Biography = reader["Biography"].ToString(),
                                    BirthDate = reader["BirthDate"].ToString(),
                                    Nationality = (string)reader["Nationality"],
                                    Website = (string)reader["Website"],
                                    ContactInformation = (string)reader["ContactInformation"]
                                };

                                return artist;
                            }

                            // Return null if the artist with the specified ID is not found
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while getting artist by ID: {ex.Message}");
                return null; 
            }
        }

    }
}

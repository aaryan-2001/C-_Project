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

        public void AddArtist(Artist artist)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Artists (Name, Biography, BirthDate, Nationality, Website, ContactInformation) " +
                               "VALUES (@Name, @Biography, @BirthDate, @Nationality, @Website, @ContactInformation)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", artist.Name);
                command.Parameters.AddWithValue("@Biography", artist.Biography);
                command.Parameters.AddWithValue("@BirthDate", artist.BirthDate);
                command.Parameters.AddWithValue("@Nationality", artist.Nationality);
                command.Parameters.AddWithValue("@Website", artist.Website);
                command.Parameters.AddWithValue("@ContactInformation", artist.ContactInformation);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateArtist(Artist artist)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Artists SET Name = @Name, Biography = @Biography, BirthDate = @BirthDate, " +
                               "Nationality = @Nationality, Website = @Website, ContactInformation = @ContactInformation " +
                               "WHERE ArtistID = @ArtistID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ArtistID", artist.ArtistID);
                command.Parameters.AddWithValue("@Name", artist.Name);
                command.Parameters.AddWithValue("@Biography", artist.Biography);
                command.Parameters.AddWithValue("@BirthDate", artist.BirthDate);
                command.Parameters.AddWithValue("@Nationality", artist.Nationality);
                command.Parameters.AddWithValue("@Website", artist.Website);
                command.Parameters.AddWithValue("@ContactInformation", artist.ContactInformation);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteArtist(int artistID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Artists WHERE ArtistID = @ArtistID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ArtistID", artistID);

                command.ExecuteNonQuery();
            }
        }
    }
}
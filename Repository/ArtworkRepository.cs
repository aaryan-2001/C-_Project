using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryApp.Entity;
using VirtualArtGalleryApp.exceptions;
using VirtualArtGalleryApp.Utility;

namespace VirtualArtGalleryApp.Repository
{
    internal class ArtworkRepository : IArtworkInterface
    {

        List<Artwork> artworks = new List<Artwork>(); //assume this list stores all artworks
        List<FavoriteArtwork> favoriteArtwork= new List<FavoriteArtwork>(); // for user fav

        public string connectionString;
        SqlCommand cmd = null;

        //constructor
        public ArtworkRepository()
        {
            
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }



        // Showing All Artworks

        public List<Artwork> BrowseArtworks()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Artworks";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Artwork artwork = new Artwork
                                {
                                    ArtworkID = (int)reader["ArtworkID"],
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Medium = reader["Medium"].ToString(),
                                    ImageURL = reader["ImageURL"].ToString()
                                };
                                artworks.Add(artwork);
                            }
                        }
                    }
                }

                return artworks;
            }
            
            catch (ArtWorkNotFoundException ex)
            {
                Console.WriteLine($"An error occurred while browsing artwork: {ex.Message}");
                return null;
            }
        }


        // Getting Artwotk by artworkID

        public Artwork GetArtworkById(int artworkId)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Artworks WHERE ArtworkID = @ArtworkID";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@ArtworkID", artworkId);

                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Artwork artwork = new Artwork
                                {

                                    ArtworkID = (int)reader["ArtworkID"],
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Medium = reader["Medium"].ToString(),
                                    ImageURL = reader["ImageURL"].ToString(),
                                    ArtistID = (int)reader["ArtistID"]
                                };

                                return artwork;
                            }
                            return null;
                        }
                    }
                }
            }
            catch (ArtWorkNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        //Adding Artwork To Favorite
        
        public bool AddArtworkToFavorite(string username, int artworkId)
        {
            int userId = GetUserIdByUsername(username);

            if (userId != 0)
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {

                        string query = "INSERT INTO User_Favorite_Artwork (UserId, ArtworkId) VALUES (@UserId, @ArtworkId)";

                        using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@ArtworkId", artworkId);

                            sqlConnection.Open();

                            int rowsAffected = cmd.ExecuteNonQuery();

                            return rowsAffected > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while adding artwork to favorites: {ex.Message}");
                }
            }

            return false;
        }

        


        //Removing Artwork from Favorites

        public bool RemoveArtworkFromFavorite(string username, int artworkId)
        {
            int userId = GetUserIdByUsername(username);

            if (userId == 0)
            {
                // returning false to User not found
                return false;
            }

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM User_Favorite_Artwork WHERE UserId = @UserId AND ArtworkId = @ArtworkId";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@ArtworkId", artworkId);

                        sqlConnection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing artwork from favorites: {ex.Message}");
                return false;
            }
        }

        public List<Artwork> GetUserFavoriteArtworks(string username)
        {
            int userId = GetUserIdByUsername(username);

            if (userId == 0)
            {
                // User not found
                return new List<Artwork>(); // Returning whole list of user fav artwork
            }

            List<Artwork> favoriteArtworks = new List<Artwork>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "SELECT A.ArtworkId, A.Title, A.Description, A.Medium FROM Artworks A " +
                                   "INNER JOIN User_Favorite_Artwork UFA ON A.ArtworkId = UFA.ArtworkId " +
                                   "WHERE UFA.UserId = @UserId";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Artwork artwork = new Artwork
                                {
                                    ArtworkID = (int)(reader["ArtworkId"]),
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Medium = reader["Medium"].ToString(),
                                   // ImageURL = reader["ImageURL"].ToString()
                                };

                                favoriteArtworks.Add(artwork);
                            }
                           // return favoriteArtworks;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting user's favorite artworks: {ex.Message}");
            }

            return favoriteArtworks;
        }


        private int GetUserIdByUsername(string username)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "SELECT UserId FROM Users WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        sqlConnection.Open();

                        object result = cmd.ExecuteScalar();

                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving user ID: {ex.Message}");
                return 0;
            }
        }

        public void AddArtwork(Artwork artwork)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Artworks (Title, Description, CreationDate, Medium, ImageURL, ArtistID) " +
                               "VALUES (@Title, @Description, @CreationDate, @Medium, @ImageURL, @ArtistID)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
                    cmd.Parameters.AddWithValue("@Title", artwork.Title);
                    cmd.Parameters.AddWithValue("@Description", artwork.Description);
                    cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                    cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                    cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void UpdateArtwork(Artwork artwork)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Artworks SET Title = @Title, Description = @Description, " +
                               "CreationDate = @CreationDate, Medium = @Medium, ImageURL = @ImageURL, " +
                               "ArtistID = @ArtistID WHERE ArtworkID = @ArtworkID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ArtworkID", artwork.ArtworkID);
                    cmd.Parameters.AddWithValue("@Title", artwork.Title);
                    cmd.Parameters.AddWithValue("@Description", artwork.Description);
                    cmd.Parameters.AddWithValue("@CreationDate", artwork.CreationDate);
                    cmd.Parameters.AddWithValue("@Medium", artwork.Medium);
                    cmd.Parameters.AddWithValue("@ImageURL", artwork.ImageURL);
                    cmd.Parameters.AddWithValue("@ArtistID", artwork.ArtistID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveArtwork(int artworkID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Artworks WHERE ArtworkID = @ArtworkID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ArtworkID", artworkID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        
    }
}

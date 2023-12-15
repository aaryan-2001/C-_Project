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
            //sqlconnection = new sqlconnection("server=desktop-0te71rt;database=productappdb;trusted_connection=true");
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
                                    ImageURL = reader["ImageURL"].ToString()

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

        /*
        public bool AddArtwork(Artwork artwork)
        {
            
            if (artworks.Any(a => a.ArtworkID == artwork.ArtworkID))
            {
                return false;
            }
            artworks.Add(artwork);
            return true;  // artwork added successfully
        }

        public bool UpdateArtwork(Artwork artwork)
        {
            Artwork existingArtwork = GetArtworkById(artwork.ArtworkID);

            if (existingArtwork != null)
            {
                
                existingArtwork.Title = artwork.Title;
                existingArtwork.Description = artwork.Description;
                existingArtwork.CreationDate = artwork.CreationDate;
                existingArtwork.Medium = artwork.Medium;
                existingArtwork.ImageURL = artwork.ImageURL;

                return true; // artwork updated successfully
            }
            return false; // artwork for updation not found
        }


        public bool RemoveArtwork(int artworkID)
        {
            int index = artworks.FindIndex(a => a.ArtworkID == artworkID);

            if (index != -1)
            {
                artworks.RemoveAt(index);
                return true; // artwork removed sucessfully
            }
            return false; //no artwork for removal found
        }

        public Artwork GetArtworkById(int artworkID)
        {
            return artworks.Find(a => a.ArtworkID == artworkID);
        }


        public List<Artwork> SearchArtworks(string keyword)
        {
            return artworks
               .Where(a => a.Title.Contains(keyword) || a.Description.Contains(keyword))
               .ToList();
        }



        // User Favourite Artworks 
  
        
        public bool AddArtworkToFavorite(int userId, int artworkId)
        {
            if (!favoriteArtwork.Any(uf => uf.UserID == userId && uf.ArtworkID == artworkId))
            {
                favoriteArtwork.Add(new FavoriteArtwork(userId, artworkId));
                return true;
            }
            return false;
        }

        public bool RemoveArtworkFromFavorite(int userId, int artworkId)
        {
            var favoriteToRemove = favoriteArtwork.First(uf => uf.UserID == userId && uf.ArtworkID == artworkId);
            if (favoriteToRemove != null)
            {
                favoriteArtwork.Remove(favoriteToRemove);
                return true;
            }
            return false;
        }

        public List<Artwork> GetUserFavoriteArtworks(int userId)
        {
            var userFavoritesIds = favoriteArtwork.Where(uf => uf.UserID == userId).Select(uf => uf.ArtworkID);
            return artworks.Where(a => userFavoritesIds.Contains(a.ArtworkID)).ToList();

        }
        */
    }
}

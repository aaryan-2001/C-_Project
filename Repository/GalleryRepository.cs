using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VirtualArtGalleryApp.Entity;
using VirtualArtGalleryApp.Utility;

namespace VirtualArtGalleryApp.Repository
{
    class GalleryRepository : IGalleryInterface
    {
        public List<Gallery> galleries = new List<Gallery>();

        public string connectionString;
        SqlCommand cmd = null;

        //constructor
        public GalleryRepository()
        {

            //sqlconnection = new sqlconnection("server=desktop-0te71rt;database=productappdb;trusted_connection=true");
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }

        #region
        /*public void AddGallery(Gallery gallery)
        {
            var GalleryExists = GetGalleryById(gallery.GalleryID);

            if (GalleryExists == null)
            {
                galleries.Add(gallery);
                Console.WriteLine($"Gallery added: {gallery}");
            }
        }

        /*public void UpdateGallery(Gallery gallery)
        {
            Gallery existingGallery = galleries.Find(g => g.GalleryID == gallery.GalleryID);
            if (existingGallery != null)
            {
                existingGallery.Name = gallery.Name;
                existingGallery.Description = gallery.Description;
                existingGallery.Location = gallery.Location;
                existingGallery.CuratorID = gallery.CuratorID;
                existingGallery.OpeningHours = gallery.OpeningHours;
                Console.WriteLine($"Gallery updated. {existingGallery}");
            }
            else
            {
                Console.WriteLine("Gallery not found for update.");
            }
        }*/
        #endregion
        public Gallery GetGalleryById(int galleryId)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Gallery WHERE GalleryID = @GalleryID";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@GalleryID", galleryId);

                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Gallery gallery = new Gallery
                                {
                                    GalleryID = (int)reader["GalleryID"],
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Location = (string)reader["Location"],
                                    ArtistID = (int)reader["ArtistID"],
                                    OpeningHours = reader["OpeningHours"].ToString()
                                };

                                return gallery;
                            }
                            return null;
                        }
                    }
                }

                // return galleries.Find(g => g.GalleryID == galleryId);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }


        public void DisplayAllGalleries()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Gallery";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Gallery gallery = new Gallery
                                {
                                    GalleryID = (int)reader["GalleryID"],
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Location = reader["Location"].ToString(),
                                    ArtistID = (int)reader["ArtistID"],
                                    OpeningHours = (string)reader["OpeningHours"]
                                };
                                Console.WriteLine($"\nGalleryID: {gallery.GalleryID},\t Name: {gallery.Name},\t Description: {gallery.Description},\t Location: {gallery.Location},\t OpeningHours: {gallery.OpeningHours}");


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while displaying galleries: {ex.Message}");
            }

        }


        public void AddGallery(Gallery gallery)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Gallery (Name, Description, Location, ArtistID, OpeningHours) " +
                                                    "VALUES (@Name, @Description, @Location, @ArtistID, @OpeningHours)", connection);
                command.Parameters.AddWithValue("@Name", gallery.Name);
                command.Parameters.AddWithValue("@Description", gallery.Description);
                command.Parameters.AddWithValue("@Location", gallery.Location);
                command.Parameters.AddWithValue("@ArtistID", gallery.ArtistID);
                command.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateGallery(Gallery gallery)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UPDATE Gallery SET Name = @Name, Description = @Description, " +
                                                    "Location = @Location, ArtistID = @ArtistID, OpeningHours = @OpeningHours " +
                                                    "WHERE GalleryID = @GalleryID", connection);
                command.Parameters.AddWithValue("@Name", gallery.Name);
                command.Parameters.AddWithValue("@Description", gallery.Description);
                command.Parameters.AddWithValue("@Location", gallery.Location);
                command.Parameters.AddWithValue("@ArtistID", gallery.ArtistID);
                command.Parameters.AddWithValue("@OpeningHours", gallery.OpeningHours);
                command.Parameters.AddWithValue("@GalleryID", gallery.GalleryID);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteGallery(int galleryId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM Gallery WHERE GalleryID = @GalleryID", connection);
                command.Parameters.AddWithValue("@GalleryID", galleryId);

                command.ExecuteNonQuery();
            }
        }


    }




}

    


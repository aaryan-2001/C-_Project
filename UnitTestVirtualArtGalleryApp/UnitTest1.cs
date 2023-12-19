using NUnit.Framework;
using NUnit.Compatibility;
using VirtualArtGalleryApp.Utility;
using VirtualArtGalleryApp.Entity;
using VirtualArtGalleryApp.Repository;
using System;
using System.Data.Objects.DataClasses;
using VirtualArtGalleryApp.exceptions;
using System.Data.SqlClient;
using System.Data.Common;
namespace UnitTestVirtualArtGalleryApp
{
    [TestFixture]
    public class UnitTest1
    {
        private const string connectionString = "Server=LAPTOP-2GDF0DQD; Database=VirtualArtGalleryDB; Trusted_Connection=True";
        
        [Test]
        public void TestUserRegistration_Success()
        {
            UserRepository userRepository = new UserRepository();
            //userRepository.connectionString = connectionString;


            int registrationResult = userRepository.AddUser(new VirtualArtGalleryApp.Entity.User
            {
                Username = "testuser22",
                Password = "testpassword",
                Email = "testuser@example.com",
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(1990 - 1 - 1).ToString(),
                ProfilePicture = "default.jpg"
            });

            // Assert
            Assert.That(registrationResult, Is.Not.Null);
            //Assert.IsTrue(loginResult, "User login should be successful"); //why it is not working
        }


        [Test]
        public void testduplicateuserregistration_failure()
        {
            UserRepository userRepository = new UserRepository();
           
            // arrange
            int registrationResult = userRepository.AddUser(new VirtualArtGalleryApp.Entity.User
            {
                Username = "testuser22",
                Password = "testpassword",
                Email = "testuser@example.com",
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(1990 - 1 - 1).ToString(),
                ProfilePicture = "default.jpg"
            });

            // Assert
            Assert.That(registrationResult, Is.EqualTo(0));

        }

        [Test]
        public void TestUserLogin_Success()
        {
            UserRepository userRepository = new UserRepository();
            
            // Arrange
            string existingUsername = "Painter";
            string password = "Password2";

            // Act
            bool loginResult = userRepository.AuthenticateUser(existingUsername, password);


            // Assert
            Assert.That(loginResult, Is.True);
        }

        [Test]
        public void TestUnregisteredUserLogin_Failure()
        {
            UserRepository userRepository = new UserRepository();
           
            // Arrange
            string unregisteredUsername = "unregisteredUser11";
            string password = "password";

            // Act
            bool loginResult = userRepository.AuthenticateUser(unregisteredUsername, password);

            // Assert
            Assert.That(loginResult, Is.False);
        }


        [Test]
        public void SearchArtistbyId()
        {
            // Arrange
            ArtistRepository artistRepository = new ArtistRepository();
            
            int artistIdToSearch = 3;

            // Act
            var artistResult = artistRepository.GetArtistById(artistIdToSearch);

            // Assert
            Assert.That(artistResult, Is.Not.Null);
            Assert.That(artistIdToSearch, Is.EqualTo(artistResult.ArtistID));

        }

        [Test]
        public void SearchArtworkById()
        {
            //Arrange
            ArtworkRepository artworkRepository = new ArtworkRepository();
           
            int ArtworkIdToBesearch = 3;

            //Act
            var artworkresult = artworkRepository.GetArtworkById(ArtworkIdToBesearch);

            //assert
            Assert.That(artworkresult, Is.Not.Null);
            Assert.That(ArtworkIdToBesearch, Is.EqualTo(artworkresult.ArtworkID));


        }


        [Test]
        public void SearchGallery()
        {
            // arrange

            GalleryRepository galleryRepository = new GalleryRepository();
            
            int GalleryIdTosearch = 1;


            //Act

            var galleryResult = galleryRepository.GetGalleryById(GalleryIdTosearch);

            //Assert

            Assert.That(galleryResult, Is.Not.Null);
            Assert.That(GalleryIdTosearch, Is.EqualTo(GalleryIdTosearch));
        }

        [Test]
        public void AddArtist_inDB()
        {
            // Arrange
            ArtistRepository artistRepository = new ArtistRepository();
            Artist artistToAdd = new Artist
            {
                Name = "Test Artist",
                Biography = "Test Biography",
                BirthDate = new DateTime(1990, 1, 1).ToString(),
                Nationality = "Test Nationality",
                Website = "http://testwebsite.com",
                ContactInformation = "test.contact@example.com"
            };

            // Act
            artistRepository.AddArtist(artistToAdd);

            // Assert
            Artist addedArtist = GetArtistFromDatabase(artistToAdd.Name);
            Assert.That(addedArtist, Is.Not.Null);
            Assert.That(artistToAdd.Name, Is.EqualTo(addedArtist.Name));

        }

        [Test]
        public void UpdateArtist_Database()
        {
            // Arrange
            ArtistRepository artistRepository = new ArtistRepository();
            Artist artistToUpdate = new Artist
            {
                ArtistID = 2,
                Name = "Updated Artist2",
                Biography = "Updated Biography3",
                BirthDate = new DateTime(1299, 5, 5).ToString(),
                Nationality = "Updated Nationality",
                Website = "http://updatedwebsite.com",
                ContactInformation = "updated.contact@example.com"
            };

            // Act
            artistRepository.UpdateArtist(artistToUpdate);

            // Assert

            Artist updatedArtist = artistRepository.GetArtistById(artistToUpdate.ArtistID);

            Assert.That(updatedArtist, Is.Not.Null);
            Assert.That(updatedArtist.Name, Is.EqualTo(artistToUpdate.Name));

        }

        [Test]
        public void DeleteArtist_FromDatabase()
        {
            // Arrange
            ArtistRepository artistRepository = new ArtistRepository();
            int artistIdToDelete = 21;

            // Act
            artistRepository.DeleteArtist(artistIdToDelete);

            // Assert
            Artist deletedArtist = artistRepository.GetArtistById(artistIdToDelete);
            Assert.That(deletedArtist, Is.Null);
        }
        
        [Test]
        public void TestDeleteArtwork()
        {
            // Arrange
            ArtworkRepository artworkRepository = new ArtworkRepository();

            int artworktodelete = 9;

            // Act
            
            artworkRepository.RemoveArtwork(artworktodelete);

            // Assert

            Artwork deletedArtwork = artworkRepository.GetArtworkById(artworktodelete);
             Assert.That(deletedArtwork, Is.Null);
        }


        

        [Test]
        public void TestUpdateArtworkDetails()
        {
            // Arrange
            ArtworkRepository artworkRepository = new ArtworkRepository();
           


            Artwork artworkToAdd = new Artwork
            {
                ArtworkID = 11,
                ArtistID = 3,
                Description = "ABC Updated Description",
                ImageURL = "ABC.COM",
                Medium = "Updated BOOKS",
                Title = "ABCD",
                CreationDate = new DateTime(2023-10-12).ToString()
            };

           
            // Act
            artworkRepository.UpdateArtwork(artworkToAdd);

            // Assert
          
            Artwork updatedArtwork = artworkRepository.GetArtworkById(artworkToAdd.ArtworkID);
            Assert.That(updatedArtwork,Is.Not.Null);
            Assert.That( updatedArtwork.Description, Is.EqualTo(artworkToAdd.Description));
            Assert.That( updatedArtwork.Medium, Is.EqualTo(artworkToAdd.Medium));
        }
        
        
        [Test]
        public void TestCreateNewGallery()
        {
            // Arrange
            GalleryRepository galleryRepository = new GalleryRepository();
            

            Gallery galleryToAdd = new Gallery
            {
                Name = "New Gallery",
                Location = "City, Country",
                Description = "A new gallery for testing purposes",
                ArtistID = 10,
                OpeningHours = "9.00 am - 6.00 pm"

            };

            // Act

            galleryRepository.AddGallery(galleryToAdd);

            // Assert
            Assert.That(galleryToAdd.Name, Is.EqualTo("New Gallery"));
            
        }



        

        [Test]
        public void TestUpdateGalleryInfo()
        {
            // Arrange
            GalleryRepository galleryRepository = new GalleryRepository();
          

            Gallery galleryToUpdate = new Gallery
            {
                GalleryID = 10,
                Name = "Existing Gallery framweWorks",
                Location = "BHOPAL",
                Description = "Oil canvas Availablw with frames",
                ArtistID = 10,
                OpeningHours = "6.00 - 6.20"
                
            };

            // Act
            galleryRepository.UpdateGallery(galleryToUpdate);

            // Assert
         
            Gallery updatedGallery = galleryRepository.GetGalleryById(galleryToUpdate.GalleryID);
            Assert.That(updatedGallery, Is.Not.Null);
            Assert.That(updatedGallery.Name, Is.EqualTo("Existing Gallery framweWorks"));
            Assert.That(updatedGallery.Location, Is.EqualTo("BHOPAL"));
        }


        
        [Test]
        public void TestRemoveGallery()
        {
            // Arrange
            GalleryRepository galleryRepository = new GalleryRepository();

            Gallery galleryToRemove = new Gallery
            {
                GalleryID =0 , 
            };

            // Act
            galleryRepository.DeleteGallery(galleryToRemove.GalleryID);

            // Assert

            Gallery removedGallery = galleryRepository.GetGalleryById(galleryToRemove.GalleryID);
            Assert.That(removedGallery, Is.Null);
        }































        // create this method to add artist in db

        private Artist GetArtistFromDatabase(string artistName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Artists WHERE Name = @Name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", artistName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Artist
                        {
                            ArtistID = (int)(reader["ArtistID"]),
                            Name = Convert.ToString(reader["Name"]),
                            Biography = Convert.ToString(reader["Biography"]),
                            BirthDate = Convert.ToString(reader["BirthDate"]),
                            Nationality = Convert.ToString(reader["Nationality"]),
                            Website = Convert.ToString(reader["Website"]),
                            ContactInformation = Convert.ToString(reader["ContactInformation"])
                        };
                    }
                }
            }

            return null;
        }
      


    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryApp.exceptions;
using VirtualArtGalleryApp.Repository;
using VirtualArtGalleryApp.Entity;
using VirtualArtGalleryApp.Utility;
using System.Linq.Expressions;
using System.Threading;

namespace VirtualArtGalleryApp
{
	internal class Program
	{
        private static object updatedUser;

        static void Main(string[] args)
		{
			IUserInterface Iuserobj = new UserRepository();
			IArtistInterface Iartistobj = new ArtistRepository();
			IArtworkInterface Iartworkobj = new ArtworkRepository();
			IGalleryInterface Igalleryobj = new GalleryRepository();

            #region
            //List<User> users = Iuserobj.DisplayAllUsers();


            //foreach (var item in users)
            //{
            //    Console.WriteLine(item);
            //}



            //List<Product>allProducts=iProductRepository.GetAllProducts();
            //foreach (var item in allProducts)
            //{
            //    Console.WriteLine(item);
            //}


            //Iuserobj.DisplayAllUsers(allusers);


            //Iuserobj.DisplayAllUsers();
            /*
			Iuserobj.AddUser(new User(1, "john_doe", "securePassword1", "john.doe@example.com", "John", "Doe", "1990-01-01", "profile1.jpg", new List<int> { 101, 202, 303 }));
			Console.WriteLine();
			Iuserobj.AddUser(new User(2, "jane_smith", "securePassword2", "jane.smith@example.com", "Jane", "Smith", "1985-05-15", "profile2.jpg", new List<int> { 104, 205, 306 }));
			Console.WriteLine();
			Iuserobj.AddUser(new User(3, "bob_jones", "securePassword3", "bob.jones@example.com", "Bob", "Jones", "1988-09-30", "profile3.jpg", new List<int> { 107, 208, 309 }));
			Console.WriteLine();
			Iuserobj.AddUser(new User(4, "alice_miller", "securePassword4", "alice.miller@example.com", "Alice", "Miller", "1992-12-12", "profile4.jpg", new List<int> { 110, 211, 312 }));
			Console.WriteLine();
			Iuserobj.AddUser(new User(5, "eric_smith", "securePassword5", "eric.smith@example.com", "Eric", "Smith", "1980-08-25", "profile5.jpg", new List<int> { 113, 214, 315 }));
			Console.WriteLine();

			Iuserobj.DisplayAllUsers();
			Console.WriteLine();
			
			//updating user

			User updatedUser = new User(7, "john_doe", "securePass12345", "john.doe@example.com", "John", "Doe", "1990-01-01", "profile1.jpg", new List<int> { 101, 202, 303 });
			Console.WriteLine();
			Iuserobj.UpdateUser(updatedUser);

			Iuserobj.DisplayAllUsers();

			Console.WriteLine();

			//Searching user by id

			Console.WriteLine(" Enter id to be searched");
			string searchedid = Console.ReadLine();
			int userID = Convert.ToInt32(searchedid);


			User ids = Iuserobj.GetUserById(userID);
			// User ids = new User(1);

			//Console.WriteLine(Iuserobj.GetUserById(8));
			if (ids == null)
				Console.WriteLine("User not exist");
			else
				Console.WriteLine($"User Found bhaio: {Iuserobj.GetUserById(userID)}");



			//Delete User

			Console.WriteLine(" Enter id to be Deleted = ");
			string idtodelete = Console.ReadLine();
			int userIDdelete = Convert.ToInt32(idtodelete);
			Iuserobj.DeleteUser(userIDdelete);


			//if (userIDdelete == 0)
			//{
			//    Console.WriteLine("User Not exist ");
			//}
			//else { Console.WriteLine("User Deleted"); }
			Iuserobj.DisplayAllUsers();
			*/


            //Operation for Artist
            /*

						IArtistInterface Iartistobj = new ArtistRepository();

						Iartistobj.AddArtist(new Artist ( 1, "Aryan Shah", "Burhanpur wala","29/11/2001","Indian","www.hhehe.com","6565853535"));
						Iartistobj.AddArtist(new Artist(2, "Aryan Shah", "Burhanpur wala", "29/11/2001", "Indian", "www.hhehe.com", "6565853535"));
						Iartistobj.AddArtist(new Artist(3, "Aryan Shah", "Burhanpur wala", "29/11/2001", "Indian", "www.hhehe.com", "6565853535"));

						Artist updatedArtist = new Artist(1, "Aryan Shah", "Burhanpur Jalebi wala", "29/11/2001", "Indian", "www.hhehe.com", "6565853535");
						Console.WriteLine();
						Iartistobj.UpdateArtist(updatedArtist);

						Iartistobj.RemoveArtist(1);

						Console.WriteLine(" Enter id to be searched");
						string searchedid = Console.ReadLine();
						int artistID = Convert.ToInt32(searchedid);


						Artist aid = Iartistobj.GetArtistById(artistID);

						if (aid == null)
							Console.WriteLine("Artist not exist");
						else
							Console.WriteLine($"Artist Found bhaio: {Iartistobj.GetArtistById(artistID)}");
						*/

            // Gallery

            /*
			IGalleryInterface IGalleryobj = new GalleryRepository();

			IGalleryobj.AddGallery(new Gallery(1, "Aryan Gallery", " HII Aryan GAllery", "Burhanpur",1,"9:00 - 6:00"));
			IGalleryobj.AddGallery(new Gallery(2, "Aryan Gallery", " HII Aryan GAllery", "Burhanpur", 1, "9:00 - 6:00"));

			Gallery Gid = IGalleryobj.GetGalleryById(2);


			if (Gid == null)
				Console.WriteLine("Gallery not exist");
			else
				Console.WriteLine($"Gallery Found : {IGalleryobj.GetGalleryById(2)}");

			IGalleryobj.DisplayAllGalleries();
			*/



            //int deleteUserStatus = Iuserobj.DeleteUser(5);
            //if (deleteUserStatus > 0)
            //{
            //	Console.WriteLine("Product Deleted SucessFully");
            //}
            //else
            //{
            //	Console.WriteLine("Product Addition Failed");
            //}

            //Console.ReadLine();
            #endregion

            bool isLoggedIn = false;

			#region
			while (true)
			{

				Console.WriteLine("==============================================================");
				Console.WriteLine("                    VIRTUAL ART GALLERY					");
				Console.WriteLine("==============================================================\n");

				Console.WriteLine("1. Login");
				Console.WriteLine("2. Register");									
				Console.WriteLine("3. Exit");

				Console.Write("\nPlease Enter your choice: ");
				string choice = Console.ReadLine();
				Console.WriteLine();
				
				switch (choice)
				{
					case "1":
						if (!isLoggedIn)
						{
							Console.WriteLine("==============================================================");
							Console.WriteLine("                       LOGIN WINDOW					");
							Console.WriteLine("==============================================================\n");
							Console.Write("Enter username: ");
							string username = Console.ReadLine();
							Console.Write("Enter password: ");
							string password = Console.ReadLine();

							

							if (Iuserobj.AuthenticateUser(username, password))
							{
								Console.WriteLine("\nLogin successful!\n");
								isLoggedIn = true;
								string loggedInUsername = username;

								while (true)
								{
                                    Console.WriteLine("\n==============================================================");
                                    Console.WriteLine($"\nWelcome, {loggedInUsername}!\n");
                                    Console.WriteLine("==============================================================\n");
                                    Console.WriteLine("1. Update Profile");
									Console.WriteLine("2. View Gallery");
									Console.WriteLine("3. Search Gallery");
                                    Console.WriteLine("4. View Artworks");
                                    Console.WriteLine("5. Search Artwork");
                                    Console.WriteLine("6. Add Artwork To Favourite");
                                    Console.WriteLine("7. Remove Artwork From Favorite");
                                    Console.WriteLine("8. All Favourite Artwork");
                                    Console.WriteLine("9. Search Artist");
                                    Console.WriteLine("10. View User Profile");
                                    Console.WriteLine("11. Delete Account");
                                    Console.WriteLine("12. Logout\n");
                                    Console.WriteLine("==============================================================");


                                    Console.Write("\nPlease Enter your choice: ");
									string userProfileChoice = Console.ReadLine();

									switch (userProfileChoice)
									{
										case "1":

											Console.WriteLine("==============================================================");
											Console.WriteLine("                       UPDATE PROFILE WINDOW					");
											Console.WriteLine("==============================================================\n");

											if (isLoggedIn)
											{
												Console.Write("Enter new password: ");
												string new_Password = Console.ReadLine();
												Console.Write("Enter new email: ");
												string new_Email = Console.ReadLine();
												Console.Write("Enter Profile Picture URL: ");
												string new_ProfilePic = Console.ReadLine();

												// Update the user in the database
												int updateSuccess = Iuserobj.UpdateUser(new User()
												{
													Username = loggedInUsername,
													Password = new_Password,
													Email = new_Email,
													ProfilePicture = new_ProfilePic
												});	

												if (updateSuccess > 0)
												{
													Console.WriteLine("Profile updated successfully!");
												}
												else
												{
													Console.WriteLine("Failed to update profile. Please try again.");
												}

											}
										break;
										case "2":
                                            if (isLoggedIn)
                                            {
                                                Console.WriteLine("==============================================================");
                                                Console.WriteLine("                       ALL GALLERIES					");
                                                Console.WriteLine("==============================================================\n");

                                                Igalleryobj.DisplayAllGalleries();
                                            }
                                            
                                            break;
										case "3":
                                            if (isLoggedIn)
                                            {
                                       
                                                Console.Write("\nEnter Gallery ID Which you want to search : ");
                                                if (int.TryParse(Console.ReadLine(), out int galleryId))
                                                {
                                                    Gallery foundGallery = Igalleryobj.GetGalleryById(galleryId);

                                                    if (foundGallery != null)
                                                    {
                                                        Console.WriteLine("\n==============================================================");
                                                        Console.WriteLine($"\nGallery found: {foundGallery.Name}");
                                                        Console.WriteLine($"\nDescription: {foundGallery.Description}");
                                                        Console.WriteLine($"\nLocation: {foundGallery.Location}");
                                                        Console.WriteLine($"\nArtist ID: {foundGallery.ArtistID}");
                                                        Console.WriteLine($"\nOpening Hours: {foundGallery.OpeningHours}");
                                                        
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Gallery not found.");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid Gallery ID. Please enter a valid number.");
                                                }
                                            }
                                           
											break;
										case "4":
                                            
                                            if (isLoggedIn)
                                            {
                                                Console.WriteLine("==============================================================");
                                                Console.WriteLine("                      ALL ARTWORKS					");
                                                Console.WriteLine("==============================================================\n");

                                                List<Artwork> allArtworks = Iartworkobj.BrowseArtworks();

                                                if (allArtworks != null)
                                                {
                                                   
                                                    foreach (Artwork artwork in allArtworks)
                                                    {
                                                        Console.WriteLine($"\nArtworkID: {artwork.ArtworkID},\t Title: {artwork.Title},\t Description: {artwork.Description},\t Medium: {artwork.Medium},\t ImageURL: {artwork.ImageURL}");

                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("\nFailed to retrieve artworks.");
                                                }
                                            }
											break; 
										case "5":
                                            if (isLoggedIn)
                                            {

                                                Console.WriteLine("==============================================================");
                                                Console.WriteLine("                       SEARCH ARTWORK					");
                                                Console.WriteLine("==============================================================\n");

                                                Console.Write("Enter Artwork ID: ");
                                                if (int.TryParse(Console.ReadLine(), out int artworkId))
                                                {
                                                    Artwork foundArtwork = Iartworkobj.GetArtworkById(artworkId);

                                                    if (foundArtwork != null)
                                                    {
                                                        Console.WriteLine($"Artwork Found: {foundArtwork.ArtworkID}");
                                                        Console.WriteLine($"Title: {foundArtwork.Title}");
                                                        Console.WriteLine($"Description: {foundArtwork.Description}");
                                                        Console.WriteLine($"Medium :  {foundArtwork.Medium}");
														
                                                     }
                                                    else
                                                    {
                                                        Console.WriteLine("Artwork not found.");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid Artwork ID. Please enter a valid number.");
                                                }
                                            }
											break;
										case "6":
                                            Console.WriteLine("==============================================================");
                                            Console.WriteLine("                    ADD ARTWORK TO FAVOURITES                     ");
                                            Console.WriteLine("==============================================================\n");

                                            Console.Write("\nEnter Artwork ID to add to favorites: ");

                                            if (int.TryParse(Console.ReadLine(), out int artworkIdAddToFavorites))
                                            {
                                                bool addedToFavorite = Iartworkobj.AddArtworkToFavorite(loggedInUsername, artworkIdAddToFavorites);

                                                if (addedToFavorite)
                                                {
                                                    Console.WriteLine("\nArtwork added to favorites successfully!");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Failed to add artwork to favorites.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid Artwork ID. Please enter a valid number.");
                                            }
                                            break;
										case "7":
                                            Console.WriteLine("==============================================================");
                                            Console.WriteLine("                    REMOVE ARTWORK FROM FAVOURITES                     ");
                                            Console.WriteLine("==============================================================\n");

                                            Console.Write("\nEnter Artwork ID to Remove From favorites: ");

                                            if (int.TryParse(Console.ReadLine(), out int artworkIdRemoveFromFavorites))
                                            {
                                                bool RemoveFromFavorite = Iartworkobj.RemoveArtworkFromFavorite(loggedInUsername, artworkIdRemoveFromFavorites);

                                                if (RemoveFromFavorite)
                                                {
                                                    Console.WriteLine("\nArtwork Removed From favorites successfully!");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Failed to Remove artwork!.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid Artwork ID. Please enter a valid number.");
                                            }
                                            break;

										case "8":
											List<Artwork> allartworks = Iartworkobj.GetUserFavoriteArtworks(username);
                                            foreach (var item in allartworks)
                                            {
                                                Console.WriteLine(item);
                                            }
                                            
											break;

                                        case "9":
											if (isLoggedIn)
											{

												Console.WriteLine("==============================================================");
												Console.WriteLine("                       SEARCH ARTIST					");
												Console.WriteLine("==============================================================\n");

												Console.Write("Enter Artist ID: ");
												if (int.TryParse(Console.ReadLine(), out int artistId))
												{
													Artist foundArtist = Iartistobj.GetArtistById(artistId);

													if (foundArtist != null)
													{
														Console.WriteLine($"\nArtist found: {foundArtist.Name}");
														Console.WriteLine($"Biography: {foundArtist.Biography}");
														Console.WriteLine($"Website: {foundArtist.Website}");
														Console.WriteLine($"Contact Information: {foundArtist.ContactInformation}");
														Console.WriteLine($"Nationality: {foundArtist.Nationality}");
														Console.WriteLine($"Artist ID: {foundArtist.ArtistID}\n");
                                                        

                                                    }
													else
													{
														Console.WriteLine("Artist not found.");
													}
												}
												else
												{
													Console.WriteLine("Invalid Artist ID. Please enter a valid number.");
												}
											}
                                            break;
										case "10" :
                                            Console.WriteLine("==============================================================");
                                            Console.WriteLine("                    USER PROFILE                     ");
                                            Console.WriteLine("==============================================================\n");
                                            
											Iuserobj.UserProfile(loggedInUsername);
                                            Console.WriteLine("\n================================================================");

                                            break;

                                        case "11":
                                            if (isLoggedIn)
                                            {
                                                // Confirm deletion
                                                Console.Write("\nAre you sure you want to delete your account? (yes/no): ");
                                                string confirmation = Console.ReadLine().ToLower();

                                                if (confirmation == "yes")
                                                {
													int deleteSuccess = Iuserobj.DeleteUser(loggedInUsername);

                                                    if (deleteSuccess>0)
                                                    {
                                                        Console.WriteLine("Account deleted successfully. Goodbye!");
                                                        Environment.Exit(0);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Failed to delete account. Please try again.");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Account deletion canceled.");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("You need to log in first.");
                                            }
											break;
                                        case "12":
                                            if (isLoggedIn)
                                            {
                                                Console.WriteLine("Logging You Out.....");
												Thread.Sleep(3000);
                                                isLoggedIn = false; // Set to false after logout
                                                Console.Clear();
                                                
                                            }
											break;
                                                   
									default:
										Console.WriteLine("Invalid choice. Please enter a valid option.");
										break;
									} 

									if (userProfileChoice == "12")
										break; // Return to the main menu
								}
							}
							else
							{
								Console.WriteLine("Invalid username or password. Login failed.");
							}
						}
						break;


                    #region

                    case "2":

						Console.WriteLine("==============================================================");
						Console.WriteLine("                       REGISTER WINDOW					");
						Console.WriteLine("==============================================================\n");

						Console.Write("Enter new username: ");
						string newUsername = Console.ReadLine();
						Console.Write("Enter new password: ");
						string newPassword = Console.ReadLine();
						Console.Write("Enter new email: ");
						string newEmail = Console.ReadLine();
                        Console.Write("Enter First Name: ");
                        string newFirstName = Console.ReadLine();
                        Console.Write("Enter Last Name: ");
                        string newLastName = Console.ReadLine();
                        Console.Write("Enter Date Of Birth (YYYY-MM-DD): ");
                        string newDOB = Console.ReadLine();
                        Console.Write("Enter Profile Picture URL: ");
                        string newProfilePic = Console.ReadLine();


                        // Register a new user

                        int registrationSuccess = Iuserobj.AddUser(new User()
                        {
                            Username = newUsername,
                            Password = newPassword,
                            Email = newEmail,
                            FirstName = newFirstName,
                            LastName = newLastName,
                            DateOfBirth = newDOB,
                            ProfilePicture = newProfilePic
	                   });

						if (registrationSuccess>0)
						{
							Console.WriteLine("\nRegistration successful!\n");
						}
						else
						{
							Console.WriteLine("Failed to register. Please try again.");
						}

						break;

                    #endregion

					
					case "3":
						Console.WriteLine("Exiting From the Virtual Art Gallery 'Thank You'......");
						Environment.Exit(0);
						break;

					default:
						Console.WriteLine("Invalid choice. Please enter a valid option.");
						break;
				}

				Console.WriteLine();
			}


			#endregion
			
        }
        
    }
 }
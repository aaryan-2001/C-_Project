﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryApp.Entity;
using System.Data.SqlClient;
using VirtualArtGalleryApp.Utility;
using VirtualArtGalleryApp.Repository;
using Microsoft.Extensions.Configuration;
using VirtualArtGalleryApp.exceptions;

namespace VirtualArtGalleryApp.Repository
{
    internal class UserRepository : IUserInterface
    {

        public List<User> users = new List<User>();
        public string connectionString;
        SqlCommand cmd = null;

        //constructor
        public UserRepository()
        {
           
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }


        public int AddUser(User user)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Users values(@Username,@Password,@Email,@FirstName,@LastName,@DateOfBirth,@ProfilePicture)";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", user.LastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                        cmd.Parameters.AddWithValue("@ProfilePicture", user.ProfilePicture);

                        sqlConnection.Open();

                        var rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
            }
            catch (SqlException ex)
            {
                // SQL Server exception
                Console.WriteLine($"SQL Error: {ex.Message}");
                return 0;
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Error registering user: { ex.Message}");
                return 0;
            }
            #region
            //using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            //{
            //    cmd.CommandText = "INSERT INTO Users(Username,Password,Email,FirstName,LastName,DateOfBirth,ProfilePicture)VALUES";
            //    cmd.Parameters.AddWithValue("@Username", user.Username);
            //    cmd.Parameters.AddWithValue("@Password", user.Password);
            //    cmd.Parameters.AddWithValue("@Email", user.Email);
            //    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            //    cmd.Parameters.AddWithValue("@LastName", user.LastName);
            //    cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
            //    cmd.Parameters.AddWithValue("@ProfilePicture", user.ProfilePicture);
            //    cmd.Connection = sqlConnection;
            //    sqlConnection.Open();
            //    int addUserStatus = cmd.ExecuteNonQuery();
            //    return addUserStatus;//Console.WriteLine($"{addUserStatus} row(s) affected.");
            //}



            //var userExists = GetUserById(user.UserID);
            //if (userExists == null)
            //{
            //    users.Add(user);
            //    Console.WriteLine($"User added: {user}");
            //}
            #endregion
        }
       
        public int UpdateUser(User updatedUser)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Users SET Password = @Password, Email = @Email,ProfilePicture = @ProfilePicture WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@Username", updatedUser.Username);
                        cmd.Parameters.AddWithValue("@Password", updatedUser.Password);
                        cmd.Parameters.AddWithValue("@Email", updatedUser.Email);
                        cmd.Parameters.AddWithValue("@ProfilePicture", updatedUser.ProfilePicture);
                        sqlConnection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
            }
            catch (SqlException ex)
            {
                // SQL Server exception
                Console.WriteLine($"SQL Error: {ex.Message}");
                return 0;
            }
            catch (UserNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        #region

        //        public void UpdateUser(User updatedUser)
        //        {
        //            User existingUser = GetUserById(updatedUser.UserID);

        //            if (existingUser != null)
        //            {
        //                existingUser.Username = updatedUser.Username;
        //                existingUser.Password = updatedUser.Password;
        //                existingUser.Email = updatedUser.Email;
        //                existingUser.FirstName = updatedUser.FirstName;
        //                existingUser.LastName = updatedUser.LastName;
        //                existingUser.DateOfBirth = updatedUser.DateOfBirth;
        //                existingUser.ProfilePicture = updatedUser.ProfilePicture;
        //                existingUser.FavoriteArtworks = updatedUser.FavoriteArtworks;

        //                Console.WriteLine($"User updated: {existingUser}");
        //            }
        //            else
        //            {
        //                Console.WriteLine("User not found for update.");
        //            }
        //        }
        #endregion
        public int DeleteUser(string username)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Delete from Users WHERE Username = @Username";
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    int deleteUserStatus = cmd.ExecuteNonQuery();
                    return deleteUserStatus;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return 0;   
            }
            catch(UserNotFoundException ex) 
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

           /* //User userToRemove = GetUserById(userID);

            //if (userToRemove != null)
            //{
            //    users.Remove(userToRemove);
            //    Console.WriteLine($"User deleted: {userToRemove}");
            //}
            //else
            //{
            //    Console.WriteLine("User not found for deletion.");
            //}*/
        }
        

        public User UserProfile(string username)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Users WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"Username: {reader["Username"]}");
                                Console.WriteLine($"Email: {reader["Email"]}");
                                Console.WriteLine($"First Name: {reader["FirstName"]}");
                                Console.WriteLine($"Last Name: {reader["LastName"]}");
                                Console.WriteLine($"Date of Birth: {reader["DateOfBirth"]}");
                                Console.WriteLine($"Profile Picture: {reader["ProfilePicture"]}");

                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("User not found in the database.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving user profile: {ex.Message}");
            }
            return null;
        }

        //Here Authenticate user for login 

        public bool AuthenticateUser(string username, string password)
        {
            try
            {
                string connectionString = "Server=LAPTOP-2GDF0DQD; Database=VirtualArtGalleryDB; Trusted_Connection=True";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND Password = @Password", sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        sqlConnection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            return reader.HasRows; // If there are rows, authentication is successful
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Login Failed; {ex.Message}");
                return false;
            }
        }

    }
}

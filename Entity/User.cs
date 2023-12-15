using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGalleryApp.Entity
{
    internal class User
    {
        // Private fields
        private int userID;
        private string username;
        private string password;
        private string email;
        private string firstName;
        private string lastName;
        private string dateOfBirth;
        private string profilePicture;
        private List<int> favoriteArtworks;

        // Auto-implemented properties
        public int UserID { get { return userID; } set { userID = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string DateOfBirth { get { return dateOfBirth; } set { dateOfBirth = value; } }
        public string ProfilePicture { get { return profilePicture; } set { profilePicture = value; } }
        public List<int> FavoriteArtworks { get { return favoriteArtworks; } set { favoriteArtworks = value; } }

        // Default constructor
        public User() { }

        // Parameterized constructor
        public User(int userID, string username, string password, string email, string firstName, string lastName, string dateOfBirth, string profilePicture, List<int> favoriteArtworks)
        {
            UserID = userID;
            Username = username;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            ProfilePicture = profilePicture;
            FavoriteArtworks = favoriteArtworks;
        }

        // ToString() method
        public override string ToString()
        {
            return $"UserID: {UserID}, Username: {Username}, Password: {Password}, Email: {Email}, FirstName: {FirstName}, LastName: {LastName}, DateOfBirth: {DateOfBirth}, ProfilePicture: {ProfilePicture}, FavoriteArtworks: {string.Join(", ", FavoriteArtworks)}";
        }
    }
}

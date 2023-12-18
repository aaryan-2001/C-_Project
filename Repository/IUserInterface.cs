using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryApp.Entity;

namespace VirtualArtGalleryApp.Repository
{
    internal interface IUserInterface
    {
        int AddUser(User user);
        int UpdateUser(User updatedUser);
        int DeleteUser(string username);
        // List<User> DisplayAllUsers();

        User UserProfile(string username);
        //Here Authenticate user for login 
        bool AuthenticateUser(string username, string password);
        

    }
}
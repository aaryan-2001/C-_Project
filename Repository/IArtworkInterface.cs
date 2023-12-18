using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryApp.Entity;

namespace VirtualArtGalleryApp.Repository
{
    internal interface IArtworkInterface
    {
        void AddArtwork(Artwork artwork);
        void UpdateArtwork(Artwork artwork);
        void RemoveArtwork(int artworkID);
        List<Artwork> BrowseArtworks();
        Artwork GetArtworkById(int artworkId);
        //List<Artwork> SearchArtworks(string keyword);



        // User favourites
       bool AddArtworkToFavorite(string Username, int artworkId);
        bool RemoveArtworkFromFavorite(string username, int artworkId);
        List<Artwork> GetUserFavoriteArtworks(string username);
    }
}

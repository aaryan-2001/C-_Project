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
        // Artwork Management
        //bool AddArtwork(Artwork artwork);
        //bool UpdateArtwork(Artwork artwork);
        //bool RemoveArtwork(int artworkId);

        List<Artwork> BrowseArtworks();
        Artwork GetArtworkById(int artworkId);
        //List<Artwork> SearchArtworks(string keyword);



        // User favourites
       bool AddArtworkToFavorite(string Username, int artworkId);
        bool RemoveArtworkFromFavorite(string username, int artworkId);
        List<Artwork> GetUserFavoriteArtworks(string username);
    }
}

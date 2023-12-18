using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryApp.Entity;

namespace VirtualArtGalleryApp.Repository
{
    internal interface IArtistInterface
    {
        void AddArtist(Artist artist);
        void UpdateArtist(Artist artist);
        void DeleteArtist(int artistID);
        Artist GetArtistById(int artistId);
    }
}

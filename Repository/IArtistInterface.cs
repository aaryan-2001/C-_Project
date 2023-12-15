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
        //int AddArtist(Artist artist);
        //int UpdateArtist(Artist artist);
        //int RemoveArtist(int artistId);
        Artist GetArtistById(int artistId);
    }
}

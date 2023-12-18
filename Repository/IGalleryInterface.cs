using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualArtGalleryApp.Entity;

namespace VirtualArtGalleryApp.Repository
{
    internal interface IGalleryInterface
    {  
        Gallery GetGalleryById(int galleryId);
        void DisplayAllGalleries();
        void AddGallery(Gallery gallery);
        void UpdateGallery(Gallery gallery);
        void DeleteGallery(int galleryId);
    }
}

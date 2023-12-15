using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGalleryApp.exceptions
{
    internal class ArtWorkNotFoundException : Exception
    {
        public ArtWorkNotFoundException() : base("Artwork not found.")
        {
        }

        public ArtWorkNotFoundException(string message) : base(message)
        {
        }

        public ArtWorkNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGalleryApp.Entity
{
    internal class Gallery
    {
        // Private fields
        private int galleryID;
        private string name;
        private string description;
        private string location;
        private int artistID;
        private string openingHours;

        // Auto-implemented properties
        public int GalleryID { get { return galleryID; } set { galleryID = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string Location { get { return location; } set { location = value; } }
        public int ArtistID { get { return artistID; } set { artistID = value; } }
        public string OpeningHours { get { return openingHours; } set { openingHours = value; } }

        // Default constructor
        public Gallery() { }

        // Parameterized constructor
        public Gallery(int galleryID, string name, string description, string location, int artistID, string openingHours)
        {
            GalleryID = galleryID;
            Name = name;
            Description = description;
            Location = location;
            ArtistID = artistID;
            OpeningHours = openingHours;
        }

        // ToString() method
        public override string ToString()
        {
            return $"GalleryID: {GalleryID}, Name: {Name}, Description: {Description}, Location: {Location}, artistID: {ArtistID}, OpeningHours: {OpeningHours}";
        }
    }
}

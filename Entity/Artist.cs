using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGalleryApp.Entity
{
    internal class Artist
    {
        private int artistID;
        private string name;
        private string biography;
        private string birthDate;
        private string nationality;
        private string website;
        private string contactInformation;

        // Default constructor
        public Artist() { }

        // Parameterized constructor
        public Artist(int artistID, string name, string biography, string birthDate, string nationality, string website, string contactInformation)
        {
            this.artistID = artistID;
            this.name = name;
            this.biography = biography;
            this.birthDate = birthDate;
            this.nationality = nationality;
            this.website = website;
            this.contactInformation = contactInformation;
        }

        // Getters and setters
        public int ArtistID { get { return artistID; } set { artistID = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Biography { get { return biography; } set { biography = value; } }
        public string BirthDate { get { return birthDate; } set { birthDate = value; } }
        public string Nationality { get { return nationality; } set { nationality = value; } }
        public string Website { get { return website; } set { website = value; } }
        public string ContactInformation { get { return contactInformation; } set { contactInformation = value; } }

        // ToString() method
        public override string ToString()
        {
            return $"ArtistID: {ArtistID}, Name: {Name}, Biography: {Biography}, BirthDate: {BirthDate}, Nationality: {Nationality}, Website: {Website}, ContactInformation: {ContactInformation}";
        }
    }
}




This the main project that follows an object-oriented design and is organized into the following packages:

model: Contains entity classes representing real-world entities 
repository: Provides the data access layer with interfaces and their implementations for database interactions.
exception: Defines custom exceptions for error handling.
utility: Includes utility classes for database connection and property handling.
main: Contains the MainModule class for demonstrating system functionalities in a menu-driven application.

Packages Used
The virtual Art gallery project utilizes the following key packages:

Microsoft.Extensions.Configuration
Microsoft.Extensions.Configuration.Abstractions
Microsoft.Extensions.Configuration.FileExtensions
Microsoft.Extensions.Configuration.Json
System.Data.SqlClient

Running the Application
Compile and run the Program.cs class to start the menu-driven application.


DataBase Schema
Entities and Attributes:
• Artwork
ArtworkID (Primary Key)
Title
Description
CreationDate
Medium
ImageURL (or any reference to the digital representation)
• Artist
ArtistID (Primary Key)
Name
Biography
BirthDate
Nationality
Website
Contact Information
• User
UserID (Primary Key)
Username
Password
Email
First Name
Last Name
Date of Birth
Profile Picture
FavoriteArtworks (a list of references to ArtworkIDs)
• Gallery
GalleryID (Primary Key)
Name
Description
Location
Curator (Reference to ArtistID)
OpeningHours
• Relationships:
• Artwork - Artist (Many-to-One)
An artwork is created by one artist.
Artwork.ArtistID (Foreign Key) references Artist.ArtistID.
• User - Favorite Artwork (Many-to-Many)
A user can have many favorite artworks, and an artwork can be a favorite of
multiple users.
User_Favorite_Artwork (junction table):
UserID (Foreign Key) references User.UserID.
ArtworkID (Foreign Key) references Artwork.ArtworkID.
• Artist - Gallery (One-to-Many)
An artist can be associated with multiple galleries, but a gallery can have
only one curator (artist).
Gallery.ArtistID (Foreign Key) references Artist.ArtistID.
• Artwork - Gallery (Many-to-Many)
An artwork can be displayed in multiple galleries, and a gallery can have
multiple artworks.
Artwork_Gallery (junction table):
ArtworkID (Foreign Key) references Artwork.ArtworkID.
GalleryID (Foreign Key) references Gallery.GalleryID.



# Virtual Art Gallery

## Overview

Welcome to the Virtual Art Gallery project! This application manages and showcases artworks, artists, users, and galleries in a virtual environment. The project follows an object-oriented design and is organized into several packages for a clean and modular codebase.

## Project Structure

### Packages:

1. **model:**
   - Contains entity classes representing real-world entities such as Artwork, Artist, User, and Gallery.

2. **repository:**
   - Provides the data access layer with interfaces and their implementations for database interactions.

3. **exception:**
   - Defines custom exceptions for effective error handling.

4. **utility:**
   - Includes utility classes for database connection and property handling.

5. **main:**
   - Contains the MainModule class for demonstrating system functionalities in a menu-driven application.

### Key Packages Used:

- **Microsoft.Extensions.Configuration:**
  - Configuration framework for handling application settings.

- **Microsoft.Extensions.Configuration.Abstractions:**
  - Abstractions for configuration providers.

- **Microsoft.Extensions.Configuration.FileExtensions:**
  - File-based configuration provider extensions.

- **Microsoft.Extensions.Configuration.Json:**
  - JSON configuration provider.

- **System.Data.SqlClient:**
  - SQL Server database connectivity.

## Running the Application

To run the Virtual Art Gallery application:

1. Compile and run the `Program.cs` class to start the menu-driven application.

## Database Schema

### Entities and Attributes:

#### Artwork

- ArtworkID (Primary Key)
- Title
- Description
- CreationDate
- Medium
- ImageURL (or any reference to the digital representation)

#### Artist

- ArtistID (Primary Key)
- Name
- Biography
- BirthDate
- Nationality
- Website
- Contact Information

#### User

- UserID (Primary Key)
- Username
- Password
- Email
- First Name
- Last Name
- Date of Birth
- Profile Picture
- FavoriteArtworks (a list of references to ArtworkIDs)

#### Gallery

- GalleryID (Primary Key)
- Name
- Description
- Location
- Curator (Reference to ArtistID)
- OpeningHours

### Relationships:

- **Artwork - Artist (Many-to-One):**
  - An artwork is created by one artist.
  - `Artwork.ArtistID` (Foreign Key) references `Artist.ArtistID`.

- **User - Favorite Artwork (Many-to-Many):**
  - A user can have many favorite artworks, and an artwork can be a favorite of multiple users.
  - `User_Favorite_Artwork` (junction table):
    - `UserID` (Foreign Key) references `User.UserID`.
    - `ArtworkID` (Foreign Key) references `Artwork.ArtworkID`.

- **Artist - Gallery (One-to-Many):**
  - An artist can be associated with multiple galleries, but a gallery can have only one curator (artist).
  - `Gallery.ArtistID` (Foreign Key) references `Artist.ArtistID`.

- **Artwork - Gallery (Many-to-Many):**
  - An artwork can be displayed in multiple galleries, and a gallery can have multiple artworks.
  - `Artwork_Gallery` (junction table):
    - `ArtworkID` (Foreign Key) references `Artwork.ArtworkID`.
    - `GalleryID` (Foreign Key) references `Gallery.GalleryID`.

Feel free to explore and enhance the functionality of the Virtual Art Gallery project! If you have any questions or suggestions, please don't hesitate to reach out. Happy coding!

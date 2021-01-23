using OlaFFProj.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using OlaFFProj.Helpers;
using OlaFFProj.Data;

namespace OlaFFProj
{
    class Program
    {
        // These are the various commands that can be performed 
        // in the app. Each command must have a unique string value.
        const string CommandListComicBooks = "l";
        const string CommandListComicBook = "i";
        const string CommandListComicBookProperties = "p";
        const string CommandAddComicBook = "a";
        const string CommandUpdateComicBook = "u";
        const string CommandDeleteComicBook = "d";
        const string CommandSave = "s";
        const string CommandCancel = "c";
        const string CommandQuit = "q";

        //Collection of comic book editable props
        readonly static List<string> EditableProperties = new List<string>()
        {
            "SeriesId",
            "IssueNumber",
            "Description",
            "PublishedOn",
            "AverageRating"
        };

        static void Main(string[] args)
        {
            string command = CommandListComicBooks;
            IList<int> comicBookIds = null;

            // If the current command doesn't = "Quit", evaluate and process the command
            while (command != CommandQuit)
            {
                switch (command) 
                {
                    case CommandListComicBooks:
                        comicBookIds = ListComicBooks();
                        break;
                    case CommandAddComicBook:
                        AddComicBook();
                        command = CommandListComicBooks;
                        continue;
                    default:
                        if (AttemptToDisplayComicBook(command, comicBookIds))
                        {
                            command = CommandListComicBooks;
                            continue;
                        }
                        else
                        {
                            ConsoleHelpers.OutputLine("Sorry, but I didn't understand your request");
                        }
                        break;
                }
                //List the available commands
                ConsoleHelpers.OutputBlankLine();
                ConsoleHelpers.Output("Commands: ");
                int comicBookCount = Repository.GetComicBookCount();
                if (comicBookCount > 0)
                {
                    ConsoleHelpers.Output("Enter a Number 1-{0}, ", comicBookCount);
                }
                ConsoleHelpers.OutputLine("A - Add, Q - Quit", false);

                // Get the next command from the user.
                command = ConsoleHelpers.ReadInput("Enter a Command: ", true);
            }
        }
        // Attempts to parse the provided command as a line number and if successful, 
        //displays the Comic Book Detail screen for the referenced comic book.
        //"command"-Number command. "comicBookIds"- Collection of comic book IDs to be displayed
        private static bool AttemptToDisplayComicBook(string command, IList<int> comicBookIds) 
        {
            var successful = false;
            int? comicBookId = null;
            // Attempt to parse the command to a line number if we have a collection of comic book IDs available
            if (comicBookIds != null)
            {
                // Attempt to parse the command to a line number.
                int lineNumber = 0;
                int.TryParse(command, out lineNumber);

                //If the number is within range then get that comic book ID
                if (lineNumber > 0 && lineNumber <= comicBookIds.Count)
                {
                    comicBookId = comicBookIds[lineNumber - 1];
                    successful = true;
                }
            }
            // If we have a comic book ID, then display the comic book.
            if (comicBookId != null)
            {
                DisplayComicBook(comicBookId.Value);
            }
            return successful;
        }
        //Prompts the user for the comic book values to add and adds the comic book to the database.
        private static void AddComicBook() 
        {
            ConsoleHelpers.ClearOutput();
            ConsoleHelpers.OutputLine("ADD COMIC BOOK");

            // Get the comic book values from the user.
            var comicBook = new ComicBook();
            comicBook.SeriesId = GetSeriesId();
            comicBook.IssueNumber = GetIssueNumber();
            comicBook.Description = GetDescription();
            comicBook.PublishedOn = GetPublishedOnDate();
            comicBook.AverageRating = GetAverageRating();

            var comicBookArtist = new ComicBookArtist();
            comicBookArtist.ArtistId = GetArtistId();
            comicBookArtist.RoleId = GetRoleId();
            comicBook.Artists.Add(comicBookArtist);

            //Add comic book to the Database
            Repository.AddComicBook(comicBook);
        }
        // Gets the series ID from the user- returns an integer for the selected series ID
        private static int GetSeriesId() 
        {
            int? seriesId = null;
            IList<Series> series = Repository.GetSeries();

            //While the series ID is null, prompt the user to select a series ID from the provided list.
            while (seriesId == null)
            {
                ConsoleHelpers.OutputBlankLine();
                foreach (Series s in series)
                {
                    ConsoleHelpers.OutputLine("{0}) {1}", series.IndexOf(s) + 1, s.Title);
                }
                // Get the line number for the selected series
                string lineNumberInput = ConsoleHelpers.ReadInput(
                    "Enter the line number of the series that you want to add a comic book to:");

                // Attempt to parse the user's input to a line number.
                int lineNumber = 0;
                if (int.TryParse(lineNumberInput, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber <= series.Count)
                    {
                        seriesId = series[lineNumber - 1].Id;
                    }
                }
                // If we weren't able to parse the provided line number to a series ID then display an error message
                if (seriesId == null)
                {
                    ConsoleHelpers.OutputLine("Sorry, but that wasn't a valid number");
                }
            }
            return seriesId.Value;
        }
        // Gets the artist ID from the user. Returns an integer for the selected artist ID
        private static int GetArtistId() 
        {
            int? artistId = null;
            IList<Artist> artists = Repository.GetArtists();

            // While the artist ID is null, prompt the user to select a artist ID from the provided list.
            while (artistId == null)
            {
                ConsoleHelpers.OutputBlankLine();
                foreach (Artist a in artists)
                {
                    ConsoleHelpers.OutputLine("{0}) {1}, ", artists.IndexOf(a) + 1, a.Name);
                }
                // Get the line number for the selected series
                string lineNumberInput = ConsoleHelpers.ReadInput(
                    "Enter the line number of the artist that you want to add to this comic book:");

                // Attempt to parse the user's input to a line number.
                int lineNumber = 0;
                if (int.TryParse(lineNumberInput, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber <= artists.Count)
                    {
                        artistId = artists[lineNumber - 1].Id;
                    }
                }
                // If we weren't able to parse the provided line number to a series ID then display an error message
                if (artistId == null)
                {
                    ConsoleHelpers.OutputLine("Sorry, but that was an invalid number");
                }
            }
            return artistId.Value;
        }
        //Gets the role ID from the user. Returns an integer for the selected role ID
        private static int GetRoleId() 
        {
            int? roleId = null;
            IList<Role> roles = Repository.GetRoles();

            // While the role ID is null, prompt the user to select a role ID from the provided list.
            while (roleId == null)
            {
                ConsoleHelpers.OutputBlankLine();
                foreach (Role r in roles)
                {
                    ConsoleHelpers.OutputLine("{0}) {1}, ", roles.IndexOf(r) + 1, r.Name);
                }
                // Get the line number for the selected role.
                string lineNumberInput = ConsoleHelpers.ReadInput(
                    "Enter the line number of the roles that the artist had on this comic book");

                // Attempt to parse the user's input to a line number.
                int lineNumber = 0;
                if (int.TryParse(lineNumberInput, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber <= roles.Count)
                    {
                        roleId = roles[lineNumber - 1].Id;
                    }
                }
                // If we weren't able to parse the provided line number to an role ID then display an error message.

                if (roleId == null)
                {
                    ConsoleHelpers.OutputLine("Sorry, but that was an invalid number.");
                }
            }

            return roleId.Value;
        }
        //Gets the issue number from the user. Returns an integer for the provided issue number.
        private static int GetIssueNumber() 
        {
            int issueNumber = 0;

            // While the issue number is less than or equal to "0", prompt the user to provide an issue number.
            while (issueNumber <= 0)
            {
                // Get the issue number from the user.
                string issueNumberInput = ConsoleHelpers.ReadInput("Enter an Issue Number");

                // Attempt to parse the user's input to an integer.
                int.TryParse(issueNumberInput, out issueNumber);

                // If we weren't able to parse the provided issue number to an integer then display an error message.
                if (issueNumber <= 0)
                {
                    ConsoleHelpers.OutputLine("Sorry, but that was an invalid number");
                }
            }
            return issueNumber;
        }
        // Gets the description from the user. Returns a string for the provided description.
        private static string GetDescription() 
        {
            return ConsoleHelpers.ReadInput("Enter the description: ");
        }

        // Gets published on date from user. Returns a date/time for the provided published on date.
        private static DateTime GetPublishedOnDate()
        {
            DateTime publishedOnDate = DateTime.MinValue;

            // While the published on date equals the minimum date/time value, prompt the user to provide a published on date.
            while (publishedOnDate == DateTime.MinValue)
            {
                // Get the published on date from the user.
                string publishedOnDateInput = ConsoleHelpers.ReadInput("Enter the date this comic book was published on");

                // Attempt to parse the user's input to a date/time.
                DateTime.TryParse(publishedOnDateInput, out publishedOnDate);

                // If we weren't able to parse the provided published on date to a date/time then display an error message
                if (publishedOnDate == DateTime.MinValue)
                {
                    ConsoleHelpers.OutputLine("Sorry, but that was an invalid date");
                }
            }
            return publishedOnDate;
        }
        //Gets the average rating from the user. Returns a decimal for the provided average rating
        private static decimal? GetAverageRating()
        {
            decimal? averageRating = null;
            var promptUser = true;
            //Continue to prompt the user for an average rating until we get a valid value or an empty value.
            while (promptUser)
            {
                // Get the average rating from the user.
                string averageRatingInput = ConsoleHelpers.ReadInput("Enter the average rating for this comic book: ");

                // Did we get a value from the user?
                if (!string.IsNullOrWhiteSpace(averageRatingInput))
                {
                    // Attempt to parse the user's input to a decimal
                    decimal averageRatingValue;
                    if (decimal.TryParse(averageRatingInput, out averageRatingValue)) 
                    {
                        averageRating = averageRatingValue;
                        // If we were able to parse the provided average rating then set the prompt 
                        //user flag to "false" so we break out of the while loop.
                        promptUser = false;
                    }
                    else
                    {
                        // If we weren't able to parse the provided average rating 
                        // to a decimal then display an error message.
                        ConsoleHelpers.OutputLine("Sorry, but that was an invalid rating");
                    }
                }
                else
                {
                    // If we didn't get a value from the user then set the prompt user flag to "false" 
                    // so we break out of the while loop.
                    promptUser = false;
                }
            }
            return averageRating;
        }
        //Retrieves the comic books from the database and lists them to the screen.
        //Returns a collection of the comic book IDs in the same order as the comic books were listed to 
        //facilitate selecting a comic book by its line number.
        private static IList<int> ListComicBooks() 
        {
            var comicBookIds = new List<int>();
            IList<ComicBook> comicBooks = Repository.GetComicBooks();

            ConsoleHelpers.ClearOutput();
            ConsoleHelpers.OutputLine("COMIC BOOKS");
            ConsoleHelpers.OutputBlankLine();

            foreach (ComicBook comicBook in comicBooks)
            {
                comicBookIds.Add(comicBook.Id);
                ConsoleHelpers.OutputLine("{0}) {1}, ", comicBooks.IndexOf(comicBook) + 1, comicBook.DisplayText);
            }
            return comicBookIds;
        }
        // Displays the comic book detail for the provided comic book ID. "comicBookId">The comic book ID to display
        private static void DisplayComicBook(int comicBookId) 
        {
            string command = CommandListComicBook;

            // If the current command doesn't equal the "Cancel" command then evaluate and process the command.
            while (command != CommandCancel)
            {
                switch (command) 
                {
                    case CommandListComicBook:
                        ListComicBook(comicBookId);
                        break;
                    case CommandUpdateComicBook:
                        UpdateComicBook(comicBookId);
                        command = CommandListComicBook;
                        continue;
                    case CommandDeleteComicBook:
                        if (DeleteComicBook(comicBookId))
                        {
                            command = CommandCancel;
                        }
                        else
                        {
                            command = CommandListComicBook;
                        }
                        continue;
                    default:
                        ConsoleHelpers.OutputLine("Sorry, but I didn't understand that command");
                        break;
                }
                // List the available commands.
                ConsoleHelpers.OutputBlankLine();
                ConsoleHelpers.Output("Commands: ");
                ConsoleHelpers.OutputLine("U - Update, D - Delete, C - Cancel", false);

                // Get the next command from the user.
                command = ConsoleHelpers.ReadInput("Enter a Command: ", true);
            }
        }
        // Confirms that the user wants to delete the comic book for the provided comic book ID and 
        //if so, deletes the comic book from the database. "comicBookId". The comic book ID to delete
        //Returns "true" if the comic book was deleted, otherwise "false".
        private static bool DeleteComicBook(int comicBookId) 
        {
            var successful = false;

            // Prompt the user if they want to continue with deleting this comic book.
            string input = ConsoleHelpers.ReadInput(
                "Are you sure that you want to delete this comic book? (Y/N)", true);

            // If the user entered "y", then delete the comic book.
            if (input == "y")
            {
                Repository.DeleteComicBook(comicBookId);
                successful = true;
            }
            return successful;
        }
        // Lists the detail for the provided comic book ID. "comicBookId">The comic book ID to list 
        private static void ListComicBook(int comicBookId) 
        {
            ComicBook comicBook = Repository.GetComicBook(comicBookId);

            ConsoleHelpers.ClearOutput();
            ConsoleHelpers.OutputLine("COMIC BOOK DETAILS");
            ConsoleHelpers.OutputLine(comicBook.DisplayText);

            if (!string.IsNullOrWhiteSpace(comicBook.Description))
            {
                ConsoleHelpers.OutputLine(comicBook.Description);
            }
            ConsoleHelpers.OutputBlankLine();
            ConsoleHelpers.OutputLine("Published On: {0}", comicBook.PublishedOn.ToShortDateString());
            ConsoleHelpers.OutputLine("Average Rating: {0}", 
                comicBook.AverageRating != null ? comicBook.AverageRating.Value.ToString("n2") : "N/A");

            ConsoleHelpers.OutputLine("Artists");
            foreach (ComicBookArtist artist in comicBook.Artists)
            {
                ConsoleHelpers.OutputLine("{0} - {1}", artist.Artist.Name, artist.Role.Name);
            }
        }
        // Lists the editable properties for the provided comic book ID and prompts the user to select a property to edit.
        //"comicBookId"-The comic book ID to update.
        private static void UpdateComicBook(int comicBookId) 
        {
            ComicBook comicBook = Repository.GetComicBook(comicBookId);

            string command = CommandListComicBookProperties;
            // If the current command doesn't equal the "Cancel" then evaluate and process the command.
            while (command != CommandCancel)
            {
                switch (command) 
                {
                    case CommandListComicBookProperties:
                        ListComicBookProperties(comicBook);
                        break;
                    case CommandSave:
                        Repository.UpdateComicBook(comicBook);
                        command = CommandCancel;
                        continue;
                    default:
                        if (AttemptUpdateComicBookProperty(command, comicBook))
                        {
                            command = CommandListComicBookProperties;
                            continue;
                        }
                        else
                        {
                            ConsoleHelpers.OutputLine("Sorry, but I didn't understand that command. ");
                        }
                        break;
                }
                // List the available commands
                ConsoleHelpers.OutputBlankLine();
                ConsoleHelpers.Output("Commands: ");
                if (EditableProperties.Count > 0)
                {
                    ConsoleHelpers.Output("Enter a Number 1-{0}, ", EditableProperties.Count);
                }
                ConsoleHelpers.Output("S - Save, C - Cancel", false);

                // Get the next command from the user.
                command = ConsoleHelpers.ReadInput("Enter a Command: ", true);
            }
            ConsoleHelpers.ClearOutput();
        }
        // Attempts to parse the provided command as a line number and if successful, calls the appropriate
        // user input method for the selected comic book property.
        //"command" The line number command. Returns "true" if the comic book property was successfully updated, otherwise "false".
        private static bool AttemptUpdateComicBookProperty(string command, ComicBook comicBook) 
        {
            var successful = false;

            // Attempt to parse the command to a line number.
            int lineNumber = 0;
            int.TryParse(command, out lineNumber);

            // If the number is within range then get that comic book ID.
            if (lineNumber > 0 && lineNumber <= EditableProperties.Count)
            {
                // Retrieve the property name for the provided line number.
                string propertyName = EditableProperties[lineNumber - 1];

                // Switch on the provided property name and call the 
                // associated user input method for that property name.
                switch (propertyName) 
                {
                    case "SeriesId":
                        comicBook.SeriesId = GetSeriesId();
                        comicBook.Series = Repository.GetSeries(comicBook.SeriesId);
                        break;
                    case "IssueNumber":
                        comicBook.IssueNumber = GetIssueNumber();
                        break;
                    case "Description":
                        comicBook.Description = GetDescription();
                        break;
                    case "PublishedOn":
                        comicBook.PublishedOn = GetPublishedOnDate();
                        break;
                    case "AverageRating":
                        comicBook.AverageRating = GetAverageRating();
                        break;
                    default:
                        break;
                }
                successful = true;
            }
            return successful;
        }
        // Lists the editable comic book properties to the screen. "comicBook"-The comic book property values to list
        private static void ListComicBookProperties(ComicBook comicBook) 
        {
            ConsoleHelpers.ClearOutput();
            ConsoleHelpers.OutputLine("UPDATE COMIC BOOK");

            //List of comic book property values  needs to match the collection of editable properties 
            // declared at the top of this file
            ConsoleHelpers.OutputBlankLine();
            ConsoleHelpers.OutputLine("1) Series: {0}", comicBook.Series.Title);
            ConsoleHelpers.OutputLine("2) Description: {0}", comicBook.Description);
            ConsoleHelpers.OutputLine("3) Issue Number: {0}", comicBook.IssueNumber);
            ConsoleHelpers.OutputLine("4) Published On: {0}", comicBook.PublishedOn.ToShortDateString());
            ConsoleHelpers.OutputLine("5) Average Rating: {0}", comicBook.AverageRating);
        }
    }
}

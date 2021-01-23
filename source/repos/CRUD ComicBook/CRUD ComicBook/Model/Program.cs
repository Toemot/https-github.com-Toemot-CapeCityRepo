using CRUD_ComicBook.Data;
using CRUD_ComicBook.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_ComicBook
{
    class Program
    {
        const string CommandListComicBooks = "l";
        const string CommandListComicBook = "i";
        const string CommandListComicBookProperties = "p";
        const string CommandAddComicBook = "a";
        const string CommandUpdateComicBook = "u";
        const string CommandDeleteComicBook = "d";
        const string CommandSave = "s";
        const string CommandCancel = "c";
        const string CommandQuit = "q";

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
                        if (AttemptDisplayComicBook(command, comicBookIds))
                        {
                            command = CommandListComicBooks;
                            continue;
                        }
                        else
                        {
                            ConsoleHelper.OutputLine("Sorry, I don't get you!");
                        }
                        break;
                }
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");

                var comicBookCount = Repository.GetComicBookCount();
                if (comicBookCount > 0)
                    ConsoleHelper.Output("Enter a number 1-{0}", comicBookCount);

                ConsoleHelper.OutputLine(" Enter: Add- A, Quit- Q", false);
                command = ConsoleHelper.ReadInput("Enter a commmand", true);
            }
        }
        private static bool AttemptDisplayComicBook(string command, IList<int> comicBookIds)
        {
            var successful = false;
            int? comicBookId = null;

            if (comicBookIds != null)
            {
                int lineNumber = 0;
                int.TryParse(command, out lineNumber);
                if (lineNumber > 0 && lineNumber <= comicBookIds.Count)
                {
                    comicBookId = comicBookIds[lineNumber - 1];
                    successful = true;
                }
            }
            if (comicBookId != null)
            {
                DisplayComicBook(comicBookId.Value);
            }
            return successful;
        }
        private static void AddComicBook()
        {
            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("ADD COMIC BOOK");

            var comicBook = new ComicBook();
            comicBook.SeriesId = GetSeriesId();
            comicBook.IssueNumber = GetIssueNumber();
            comicBook.Description = GetDescription();
            comicBook.PublishedOn = GetPublishedOn();
            comicBook.AverageRating = GetAverageRating();

            var comicBookArtist = new ComicBookArtist();
            comicBookArtist.ArtistId = GetArtistId();
            comicBookArtist.RoleId = GetRoleId();
            comicBook.Artists.Add(comicBookArtist);

            Repository.AddComicBook(comicBook);
        }
        private static int GetSeriesId()
        {
            int? seriesId = null;
            IList<Series> series = Repository.GetSeries();

            while (seriesId == null)
            {
                ConsoleHelper.Output("Enter a Series ID: ");
                ConsoleHelper.OutputBlankLine();

                foreach (Series s in series)
                {
                    ConsoleHelper.OutputLine("{0}) {1}",
                        series.IndexOf(s) + 1, s.Title);
                }

                string lineNumberInput = ConsoleHelper.ReadInput
                    ("Enter the line number of the series you want to add: ");
                int lineNumber;
                if (int.TryParse(lineNumberInput, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber < series.Count)
                    {
                        seriesId = series[lineNumber - 1].Id;
                    }
                }
                if (seriesId == null)
                {
                    ConsoleHelper.OutputLine("Sorry, you entered a wrong value");
                }
            }
            return seriesId.Value;
        }
        private static int GetArtistId()
        {
            int? artistId = null;
            IList<Artist> artists = Repository.GetArtists();

            while (artistId == null)
            {
                ConsoleHelper.Output("Enter the Artist ID: ");
                ConsoleHelper.OutputBlankLine();

                foreach (Artist a in artists)
                {
                    ConsoleHelper.OutputLine("{0}) {1}",
                        artists.IndexOf(a) + 1, a.Name);
                }
                string lineNumberInput = ConsoleHelper.ReadInput
                    ("Enter the name of the artist you want to update.");
                int lineNumber;
                if (int.TryParse(lineNumberInput, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber < artists.Count)
                    {
                        artistId = artists[lineNumber - 1].Id;
                    }
                }
                if (artistId == null)
                {
                    ConsoleHelper.OutputLine("Sorry you entered a wrong value");
                }
            }
            return artistId.Value;
        }
        private static int GetRoleId()
        {
            int? roleId = null;
            IList<Role> roles = Repository.GetRoles();

            while (roleId == null)
            {
                ConsoleHelper.Output("Enter the Role ID: ");
                ConsoleHelper.OutputBlankLine();

                foreach (Role r in roles)
                {
                    ConsoleHelper.OutputLine("{0}) {1}",
                        roles.IndexOf(r) + 1, r.Name);
                }
                string lineNumberInput = ConsoleHelper.ReadInput
                    ("Enter the name of the role to update");
                int lineNumber;
                if (int.TryParse(lineNumberInput, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber < roles.Count)
                    {
                        roleId = roles[lineNumber - 1].Id;
                    }
                }
                if (roleId == null)
                {
                    ConsoleHelper.OutputLine("Sorry you entered a wrong value");
                }
            }
            return roleId.Value;
        }
        private static int GetIssueNumber()
        {
            int issueNumber = 0;

            while (issueNumber <= 0)
            {
                string issueNumberInput = ConsoleHelper.ReadInput("Enter an issue number");

                int.TryParse(issueNumberInput, out issueNumber);

                if (issueNumber <= 0)
                {
                    ConsoleHelper.Output("Sorry, but that was an invalid issue number");
                }
            }
            return issueNumber;
        }
        private static string GetDescription()
        {
            return ConsoleHelper.ReadInput("Enter a Description");
        }
        private static DateTime GetPublishedOn()
        {
            DateTime publishedOnDate = DateTime.MinValue;

            while (publishedOnDate == DateTime.MinValue)
            {
                string publishedOnDateInput = ConsoleHelper.ReadInput
                    ("Enter a Published on Date: ");
                DateTime.TryParse(publishedOnDateInput, out publishedOnDate);

                if (publishedOnDate == null)
                {
                    ConsoleHelper.OutputLine("You entered a wrong date value");
                }
            }
            return publishedOnDate;
        }
        private static decimal GetAverageRating()
        {
            var promptUser = true;
            decimal? averageRating = null;

            while (promptUser)
            {
                string averageRatingValue = ConsoleHelper.ReadInput
                    ("Enter the Average Rating: ");

                if (!String.IsNullOrWhiteSpace(averageRatingValue))
                {
                    decimal averageRatingInput;
                    if (decimal.TryParse(averageRatingValue, out averageRatingInput))
                    {
                        averageRating = averageRatingInput;
                        promptUser = false;
                    }
                    else
                    {
                        ConsoleHelper.OutputLine("Sorry, you entered a wrong value");
                    }
                }
                else
                {
                    promptUser = false;
                }
            }
            return averageRating.Value;
        }
        private static IList<int> ListComicBooks()
        {
            var comicBookIds = new List<int>();
            IList<ComicBook> comicBooks = Repository.GetComicBooks();

            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("COMIC BOOKS");
            ConsoleHelper.OutputBlankLine();

            foreach (ComicBook comicBook in comicBooks)
            {
                comicBookIds.Add(comicBook.Id);

                ConsoleHelper.OutputLine("{0}) {1}",
                    comicBooks.IndexOf(comicBook) + 1, comicBook.DisplayText);
            }
            return comicBookIds;
        }
        private static void DisplayComicBook(int comicBookId)
        {
            string command = CommandListComicBook;

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
                        ConsoleHelper.OutputLine
                            ("Sorry, but I did not understand that command.");
                        break;
                }
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                ConsoleHelper.OutputLine("Update U, Delete D, Cancel C", false);

                command = ConsoleHelper.ReadInput("Enter a command: ", true);
            }
        }
        private static bool DeleteComicBook(int comicBookId)
        {
            var successful = false;

            string input = ConsoleHelper.ReadInput
                ("Are you sure that you want to delete (Y/N?)", true);

            if (input == "Y")
            {
                Repository.DeleteComicBook(comicBookId);
                successful = true;
            }
            return successful;
        }
        private static void ListComicBook(int comicBookId)
        {
            ComicBook comicBook = Repository.GetComicBook(comicBookId);

            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("COMIC BOOK DETAILS");
            ConsoleHelper.OutputLine(comicBook.DisplayText);

            if (!String.IsNullOrWhiteSpace(comicBook.Description))
            {
                ConsoleHelper.OutputLine(comicBook.Description);
            }

            ConsoleHelper.OutputBlankLine();
            ConsoleHelper.OutputLine("Published on: {0}", comicBook.PublishedOn.ToShortDateString());
            ConsoleHelper.OutputLine("Average Rating: {0}", comicBook.AverageRating != null ?
                comicBook.AverageRating.Value.ToString("n2") : "N/A");

            ConsoleHelper.OutputLine("Artist:");

            foreach (ComicBookArtist artist in comicBook.Artists)
            {
                ConsoleHelper.OutputLine("{0}-{1}", artist.Artist.Name, artist.Role.Name);
            }
        }
        private static void UpdateComicBook(int comicBookId) 
        {
            ComicBook comicBook = Repository.GetComicBook(comicBookId);
            
            string command = CommandListComicBookProperties;
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
                            ConsoleHelper.OutputLine("Sorry, but I do not undertsand");
                        }
                        break;
                }
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");

                if (EditableProperties.Count > 0)
                {
                    ConsoleHelper.Output("Enter a number 1-{0}, ", EditableProperties.Count);
                }
                ConsoleHelper.OutputLine("Save - S, Cancel - C", false);
                command = ConsoleHelper.ReadInput("Enter a Command: ", true);
            }
            ConsoleHelper.ClearOutput();
        }
        private static bool AttemptUpdateComicBookProperty(string command, ComicBook comicBook)
        {
            var successful = false;
            int lineNumber;

            int.TryParse(command, out lineNumber);
            if (lineNumber > 0 && lineNumber <= EditableProperties.Count)
            {
                string propertyName = EditableProperties[lineNumber - 1];

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
                        comicBook.PublishedOn = GetPublishedOn();
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
        private static void ListComicBookProperties(ComicBook comicBook) 
        {
            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("UPDATE COMIC BOOK");
            ConsoleHelper.OutputBlankLine();
            ConsoleHelper.OutputLine("1) Series: {0}", comicBook.Series.Title);
            ConsoleHelper.OutputLine("2) Issue Number: {0}", comicBook.IssueNumber);
            ConsoleHelper.OutputLine("3) Description: {0}", comicBook.Description);
            ConsoleHelper.OutputLine("4) Published On: {0}", comicBook.PublishedOn.ToShortDateString());
            ConsoleHelper.OutputLine("5) Average Rating: {0}", comicBook.AverageRating);
        }
    }
}

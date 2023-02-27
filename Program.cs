using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");

string file = "movies.csv";
// make sure movie file exists
if (!File.Exists(file))
{
  logger.Error("File does not exist: {File}", file);
}
else
{
  // create parallel lists of movie details
  // lists are used since we do not know number of lines of data
  List<UInt64> MovieIds = new List<UInt64>();
  List<string> MovieTitles = new List<string>();
  List<string> MovieGenres = new List<string>();

  string? choice;
  do
  {
    // display choices to user
    Console.WriteLine("1) Add Movie");
    Console.WriteLine("2) Display All Movies");
    Console.WriteLine("Enter to quit");

    // input selection
    choice = Console.ReadLine();
    logger.Info("User choice: {Choice}", choice);

    if (choice == "1")
    {
      // Add Movie
    }
    else if (choice == "2")
    {
      // Display All Movies
    }
  } while (choice == "1" || choice == "2");
}

logger.Info("Program ended");

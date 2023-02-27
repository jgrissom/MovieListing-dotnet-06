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

  // to populate the lists with data, read from the data file
  try
  {
    StreamReader sr = new StreamReader(file);
    // first line contains column headers
    sr.ReadLine();
    while (!sr.EndOfStream)
    {
      string? line = sr.ReadLine();
      if (line is not null)
      {
        // first look for quote(") in string
        // this indicates a comma(,) in movie title
        int idx = line.IndexOf('"');
        if (idx == -1)
        {
          // no quote = no comma in movie title
          // movie details are separated with comma(,)
          string[] movieDetails = line.Split(',');
          // 1st array element contains movie id
          MovieIds.Add(UInt64.Parse(movieDetails[0]));
          // 2nd array element contains movie title
          MovieTitles.Add(movieDetails[1]);
          // 3rd array element contains movie genre(s)
          // replace "|" with ", "
          MovieGenres.Add(movieDetails[2].Replace("|", ", "));
        }
        else
        {
          // quote = comma in movie title
          // extract the movieId
          MovieIds.Add(UInt64.Parse(line.Substring(0, idx - 1)));
          // remove movieId and first quote from string
          line = line.Substring(idx + 1);
          // find the next quote
          idx = line.IndexOf('"');
          // extract the movieTitle
          MovieTitles.Add(line.Substring(0, idx));
          // remove title and last comma from the string
          line = line.Substring(idx + 2);
          // replace the "|" with ", "
          MovieGenres.Add(line.Replace("|", ", "));
        }
      }
    } 
    sr.Close();
  }
  catch (Exception ex)
  {
    logger.Error(ex.Message);
  }

  string? choice;
  do
  {
    logger.Info("Movies in file {Count}", MovieIds.Count);
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
      // loop thru Movie Lists
      for (int i = 0; i < MovieIds.Count; i++)
      {
        // display movie details
        Console.WriteLine($"Id: {MovieIds[i]}");
        Console.WriteLine($"Title: {MovieTitles[i]}");
        Console.WriteLine($"Genre(s): {MovieGenres[i]}");
        Console.WriteLine();
      }
    }
  } while (choice == "1" || choice == "2");
}

logger.Info("Program ended");

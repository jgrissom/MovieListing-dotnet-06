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
  // TODO: create user menu
}

logger.Info("Program ended");

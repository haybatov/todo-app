const string backupExtension = ".bak";

string inputFile = null!;
var cmdParams = Environment.GetCommandLineArgs();
if(cmdParams.Length > 1 && !File.Exists(inputFile = cmdParams[1]))
{
    Console.WriteLine($"File does not exist: {inputFile}. Exiting.");
    return;
}

var fileName = inputFile ?? "todo.txt";
Console.Write($"Processing {fileName}...");

var lines = File.ReadAllLines(fileName);

List<string> completedLines = new(lines.Length);
List<string> ongoingLines = new(lines.Length);

for (int i = 0; i < lines.Length; i++)
     (IsLineCompleted(lines[i]) ? completedLines : ongoingLines).Add(lines[i]);

File.Move(fileName, fileName + backupExtension, true);

File.WriteAllLines(fileName, ongoingLines);
File.AppendAllLines(fileName, completedLines);

Console.WriteLine("Done.");

static bool IsLineCompleted(string line) =>
    line.StartsWith("***") || 
    line.StartsWith("  ***") || 
    line.StartsWith(" ***") || 
    line.StartsWith("**-") ||
    line.StartsWith("---") ||
    line.StartsWith("*--");
// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

const string moveFrom = @"/Users/ken/Developer/zig/ziglings/exercises";
const string moveTo = @"/Users/ken/Developer/zig/ziglings-answers";

Directory.SetCurrentDirectory(moveTo);
foreach (var file in Directory.EnumerateFiles(moveFrom, "0*_*.zig").OrderBy(x => x))
{
    var fi = new FileInfo(file);
    // if (fi.Name.StartsWith("031_")) break;
    var newName = file.Replace(moveFrom, moveTo);
    if (File.Exists(newName)) continue;
    Console.WriteLine($"copying {file} to {newName}");
    File.Copy(file, newName, true);
    
    var date = fi.LastWriteTime;
    var dateStr = date.ToString("MMM dd HH:mm:ss yyyy");
    var commitFile = Path.GetFileNameWithoutExtension(fi.Name);
    commitFile = commitFile.Substring(commitFile.IndexOf('_')+1);
    var commitMessage = $"add {commitFile}";
    Console.WriteLine(dateStr);
    // git add 001_hello.zig 
    // git commit --date "Sep 14 19:25:34 2022" -m "add hello"
    var p = Process.Start("git", $"add {fi.Name}");
    p.WaitForExit();
    p = Process.Start("git", $"commit --date \"{dateStr}\" -m \"{commitMessage}\"");
    p.WaitForExit();
    //Thread.Sleep(1);
}
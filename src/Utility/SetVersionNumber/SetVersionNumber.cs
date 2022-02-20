using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

public class SetVersionNumber : Microsoft.Build.Utilities.Task
{
    public int BuildNumber { get; set; }
    public string VersionFile { get; set; }

    public override bool Execute()
    {
        if (!File.Exists(VersionFile))
        {
            Console.WriteLine($"The file '{VersionFile}' could not be found");
            return false;
        }

        var content = File.ReadAllText(VersionFile);
        if (String.IsNullOrEmpty(content))
        {
            Console.WriteLine($"The file {VersionFile} is empty. Please specify an AssemblyVersion attribute");
            return false;
        }

        if( Regex.IsMatch(content, $"\"\\d+\\.\\d+\\.{BuildNumber}"))
        {
            Console.WriteLine($"{VersionFile} already contains {BuildNumber}, nothing to do");
            return true;
        }
        
        var pattern = @"(?<Major>\d+)\.(?<Minor>\d+)\.(?<BuildNumber>\d+)";
        content = Regex.Replace(content, pattern, "${Major}.${Minor}." + BuildNumber);
        var attempts = 0;
        while (attempts++ < 5)
        {
            try
            {
                File.WriteAllText(VersionFile, content, System.Text.Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception {ex.Message} caught");
                Thread.Sleep(100);
            }
        }
        
        return false;
    }
}
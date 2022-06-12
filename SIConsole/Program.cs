using CommandLine;
using Newtonsoft.Json;
using SIConsole;
using SIConsole.Jobs;

class Options
{
    [Option('d', "data-path", Default = null, Required = false)]
    public string? DataPath { get; set; }
}

internal static class Program
{
    public static void Main(string[] args)
    {
        const string path = "data";

        SIData dat;
        var loader = new SIJSONLoader(typeof(CopyFileJob));
        using (var sr = new StreamReader(Path.Combine(path, "data.json")))
            dat = loader.Load(new JsonTextReader(sr));

        var executor = new SIJobExecutor(dat.Jobs)
        {
            WorkingDirectory = "C:\\test",
            Tags = new string[]{"c"}
        };
        executor.ExecuteAll();
        Console.WriteLine("Done!");
    }
}

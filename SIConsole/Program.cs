using CommandLine;
using Newtonsoft.Json;
using SIConsole;
using SIConsole.Jobs;

class Options
{
    [Option('d', "data-path", Default = null, Required = false)]
    public string? DataPath { get; set; }

    [Option("data-file", Default = null, Required = false)]
    public string? DataFilename { get; set; }

    [Option('a', "args", Default = new string[] { }, Separator = '|')]
    public IEnumerable<string> Args { get; set; }

    [Option('t', "tags", Default = new string[] { }, Separator = '|')]
    public IEnumerable<string> Tags { get; set; }
}

internal static class Program
{
    private const string DefaultDataPath = "";
    private const string DefaultDataFilename = "data.json";
    private static readonly Type[] JobTypes =
    {
        typeof(CopyFileJob), 
        typeof(DeleteFileJob),
        typeof(DeleteDirectoryJob)
    };

    public static void Main(string[] args)
    {
        Options? options = null;
        Parser.Default.ParseArguments<Options>(args).WithParsed(x => { options = x; });
        if (options == null)
        {
            Console.WriteLine("ERR Parsing error.");
            return;
        }

        if (options.Args.Count() % 2 != 0)
        {
            Console.WriteLine("ERR Check args input count.");
            return;
        }

        var dataPath = options.DataPath ?? DefaultDataPath;
        var dataFilename = options.DataFilename ?? DefaultDataFilename;

        SIData dat;
        var loader = new SIJSONLoader(JobTypes);
        using (var sr = new StreamReader(Path.Combine(dataPath, dataFilename)))
            dat = loader.Load(new JsonTextReader(sr));

        var argDictionary = new Dictionary<string, string>();
        for (var i = 0; i < options.Args.Count(); i += 2)
        {
            argDictionary.Add(options.Args.ElementAt(i), options.Args.ElementAt(i + 1));
        }

        if (!dat.Args.SequenceEqual(argDictionary.Keys))
        {
            Console.WriteLine("ERR Match args with required args.");
            return;
        }

        var executor = new SIJobExecutor(dat.Jobs)
        {
            WorkingDirectory = dataPath,
            Tags = options.Tags.ToArray(),
            Args = argDictionary
        };
        executor.ProgressChanged += Executor_ProgressChanged;
        executor.ExecuteAll();
        Console.WriteLine("DONE");
    }

    private static void Executor_ProgressChanged(object sender, SIExecutorProgressEventArgs arg)
    {
        Console.WriteLine($"PRG {arg.CurrentJob.Name} {arg.Progress} {arg.TotalProgress}");
    }
}
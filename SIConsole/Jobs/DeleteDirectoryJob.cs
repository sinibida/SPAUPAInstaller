namespace SIConsole.Jobs;

[SIJobInfo(Code = "delete_dir")]
public class DeleteDirectoryJob : SIJob
{
    public string Dir { get; set; }

    public override void Execute()
    {
        var expDir = Environment.ExpandEnvironmentVariables(Dir);
        if (Directory.Exists(expDir))
        {
            var files = Directory.GetFiles(expDir, "*", SearchOption.AllDirectories);
            var dirs = Directory.GetDirectories(expDir, "*", SearchOption.AllDirectories);
            int total = files.Length + dirs.Length + 1;
            int i = 0;
            foreach (var path in files)
            {
                File.Delete(path);
                i++;
                Progress = (double)i / total;
            }
            foreach (var path in dirs)
            {
                Directory.Delete(path);
                i++;
                Progress = (double)i / total;
            }
            Directory.Delete(expDir);
        }
        
        Progress = 1;
    }
}
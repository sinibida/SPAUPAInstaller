namespace SIConsole;

public class SIJobCollection : List<SIJob>
{
    public SIJobCollection()
    {
    }

    public SIJobCollection(IEnumerable<SIJob> list)
    {
        foreach (var job in list)
        {
            Add(job);
        }
    }

    public SIJobCollection FilterTag(params string[] tags)
    {
        var col = new SIJobCollection();
        foreach (var job in this)
        {
            if (job.TagFilters.Length == 0)
            {
                col.Add(job);
                continue;
            }

            var pass = true;
            foreach (var tagFilter in job.TagFilters)
            {
                var passIfFound = !tagFilter.StartsWith('-');
                var rawFilter = passIfFound ? tagFilter : tagFilter.Substring(1);
                var found = tags.Contains(rawFilter);
                if (passIfFound != found)
                {
                    pass = false;
                    break;
                }
            }

            if (pass)
                col.Add(job);
        }

        return col;
    }
}
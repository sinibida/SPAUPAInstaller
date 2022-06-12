namespace SIConsole;

[AttributeUsage(AttributeTargets.Class)]
public class SIJobInfoAttribute : Attribute
{
    public string? Code { get; set; }
}
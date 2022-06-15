using System;

namespace SPAUPAInstaller;

[AttributeUsage(AttributeTargets.Class)]
public class InstallerElementInfoAttribute : Attribute
{
    public string? Code { get; set; }
}
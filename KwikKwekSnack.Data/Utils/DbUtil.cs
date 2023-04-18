using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace KwikKwekSnack.Data.Utils;

public class DbUtil
{
    private const string ConnectionStringName = "KwikKwekSnack"; 
    
    public static string GetConnectionString()
    {
        var slnDir = TryGetSolutionDirectoryInfo();
        if (slnDir == null) throw new Exception("Solution directory not found");
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .SetBasePath(slnDir.FullName)
            .Build();
        return config.GetConnectionString(ConnectionStringName);
    }
    
    private static DirectoryInfo? TryGetSolutionDirectoryInfo()
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }
        return directory;
    }
}
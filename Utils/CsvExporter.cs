using System.Text;
using UserData.Models;

namespace UserData.Utils;

public static class CsvExporter
{
    public static void ExportToCsv(string filePath, List<User> users)
    {
        var sb = new StringBuilder();
        sb.AppendLine("FirstName,LastName,Email,SourceId");

        foreach (var user in users)
        {
            sb.AppendLine($"{user.FirstName},{user.LastName},{user.Email},{user.SourceId}");
        }

        File.WriteAllText(filePath, sb.ToString());
    }
}

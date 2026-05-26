namespace NotebookApp.Utils;

public static class OsChecker
{
    public static string GetOperatingSystem()
    {
        string userAgent = System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier;
        
        if (userAgent.Contains("ios") || 
            userAgent.Contains("mac"))
        {
            return "ios";
        }
        return "win";
    }
}
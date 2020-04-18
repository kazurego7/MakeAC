using System.IO;
using static DirectoryInfoEx;

public static class DirectoryEx
{
    /// <summary>
    /// Copy directories recursively.
    /// </summary>
    /// <param name="sourceDirPath"></param>
    /// <param name="targetDirPath"></param>
    public static void Copy(string sourceDirPath, string targetDirPath)
    {
        var sourceDirInfo = new DirectoryInfo(sourceDirPath);
        var targetDirInfo = new DirectoryInfo(targetDirPath);
        if (!sourceDirInfo.Exists)
        {
            throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirPath);
        }
        if (targetDirInfo.Exists)
        {
            throw new IOException("Target directory already exists: " + targetDirPath);
        }

        targetDirInfo.Create();
        foreach (var sourceFileInfo in sourceDirInfo.GetFiles())
        {
            sourceFileInfo.CopyTo(Path.Combine(targetDirInfo.FullName, sourceFileInfo.Name));
        }

        foreach (var sourceSubDirInfo in sourceDirInfo.GetDirectories())
        {
            var targetSubDirPath = Path.Combine(targetDirInfo.FullName, sourceSubDirInfo.Name);
            Copy(sourceSubDirInfo.FullName, targetSubDirPath);
        }
    }
}

public static class DirectoryInfoEx
{
    /// <summary>
    /// Copy direcotries recursively
    /// </summary>
    /// <param name="sourceDirInfo"></param>
    /// <param name="targetDirPath"></param>
    public static void Copy(this DirectoryInfo sourceDirInfo, string targetDirPath)
    {
        DirectoryEx.Copy(sourceDirInfo.FullName, targetDirPath);
        return;
    }
}
using System.Collections.Generic;
using System.IO;
using Extend;

/// <summary>
/// A helper class consisting of helper methods for directories.
/// </summary>
internal static class LocalDirectoryUtils
{

    /// <summary>
    /// Return a list of the names of all files located at the provided directory.
    /// </summary>
    /// <param name="directory">The directory whose files' names should be returned.</param>
    /// <returns>A list of the names of all files located at the provided directory.</returns>
    public static IEnumerable<string> GetFileNames(string directory)
    {
        string[] filePaths = Directory.GetFiles(directory);
        return filePaths.SelectAsList(fullPath => Path.GetFileName(fullPath));
    }

}
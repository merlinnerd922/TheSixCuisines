﻿#region

using System;
using System.IO;

#endregion

/// <summary>A location within a file directory.</summary>
[Serializable]
public class PathInst
{

    /// <summary>An array of "/"-delimited Strings collectively representing this Path.</summary>
    internal readonly string[] _pathComponents;

    /// <summary>Construct a brand new PathInst instance.</summary>
    /// <param name="pathComponents">The String components of the Path that will be created.</param>
    public PathInst(params string[] pathComponents)
    {
        // Store this path's components, but make sure to trim any leading slashes off all Path subcomponents beyond the first.
        this._pathComponents = pathComponents;
        this.TrimLeadingSlashesFromLeadingComponents();
    }

    /// <summary>
    ///     Trim all leading slashes off of all of this Path instance's relative sub-paths (i.e. all Path components
    ///     except for the first).
    /// </summary>
    private void TrimLeadingSlashesFromLeadingComponents()
    {
        for (int i = 1; i < this._pathComponents.Length; i++)
            this._pathComponents[i] = this._pathComponents[i].TrimStart('/', '\\');
    }

    /// <summary>
    ///     Given a PathInstance, create a FileStream object from that path and return the newly created FileStream
    ///     object. If the file already exists, overwrite it.
    /// </summary>
    /// <returns>The newly created FileStream object.</returns>
    public FileStream CreateOverwriteFileStream()
    {
        return File.Create(this.AsString());
    }

    /// <summary>Return true iff there is a file at this path.</summary>
    /// <returns>true iff there is a file at this path.</returns>
    public bool Exists()
    {
        return File.Exists(this.AsString());
    }

    /// <summary>Return the absolute path representation of this String.</summary>
    /// <returns></returns>
    private string AsString()
    {
        return Path.Combine(this._pathComponents);
    }

    /// <summary>Return a String representation of this path.</summary>
    /// <remarks>
    ///     This method distinguishes itself from AsString(), as this method is specifically designated as a String
    ///     representation, which may change in the future, whereas AsString() always returns an absolute path string
    ///     representation..
    /// </remarks>
    /// <returns>A String representation of this path.</returns>
    public override string ToString()
    {
        return this.AsString();
    }

}
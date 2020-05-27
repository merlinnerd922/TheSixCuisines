﻿#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Extend;
using UnityEngine;

#endregion

namespace BDT
{

    /// <summary>A script designed to save games.</summary>
    [Serializable]
    public class SaveGameManager
    {

        /// <summary>A formatter to convert data into a save file.</summary>
        protected internal static BinaryFormatter _bf = new BinaryFormatter();

        /// <summary>Save the provided game state <paramref name="savedGame" /> at the path specified by <paramref name="path" />.</summary>
        /// <param name="savedGame">The game state to save.</param>
        /// <param name="path">The path to save <paramref name="savedGame" /> at.</param>
        public static void SaveGame(object savedGame, PathInst path)
        {
            FileStream fileStream = path.CreateOverwriteFileStream();
            _bf.Serialize(fileStream, savedGame);
            fileStream.Close();
        }

        /// <summary>
        ///     Given that we want to save a save file with the file name <paramref name="saveFileName" />, return the path
        ///     that such a save file would have, using the standard Unity save file directory
        ///     <see cref="Application.persistentDataPath" />.
        /// </summary>
        /// <param name="saveFileName">The name of the save file to save.</param>
        /// <returns>The Path value of a BDT save file with the provided name.</returns>
        public static PathInst InitializeSaveFilePathFromFileName(string saveFileName)
        {
            return new PathInst(Application.persistentDataPath, saveFileName);
        }

        /// <summary>
        /// Return the names of all save files stored in the game save directory location.
        /// </summary>
        /// <returns> the names of all save files stored in the game save directory location.</returns>
        public static IEnumerable<string> GetSaveFileNames()
        {
            return LocalDirectoryUtils.GetFileNames(Application.persistentDataPath);
        }

        /// <summary>
        /// Load the game whose file name is provided below.
        /// </summary>
        /// <param name="filename">The name of the file to load.</param>
        /// <returns>The save file object corresponding to the provided file name.</returns>
        public static object LoadGame(string filename)
        {
            PathInst gamePath = InitializeSaveFilePathFromFileName(filename);
            return _bf.Deserialize(gamePath.OpenExistingFileStream());
        }

    }

}
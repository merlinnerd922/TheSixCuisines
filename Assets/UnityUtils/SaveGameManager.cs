﻿#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

#endregion

namespace BDT
{

    /// <summary>A script designed to save games.</summary>
    [Serializable]
    public class SaveGameManager
    {

        /// <summary>A formatter to convert data into a save file.</summary>
        private protected static BinaryFormatter _bf = new BinaryFormatter();

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
        /// TODO
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSaveFileNames()
        {
            return new List<string>(Directory.GetFiles(Application.persistentDataPath));
        }

    }

}
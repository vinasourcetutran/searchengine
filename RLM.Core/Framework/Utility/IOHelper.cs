using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RLM.Core.Framework.Log;
using System.IO;

namespace RLM.Core.Framework.Utility
{
    public class IOHelper
    {
        public static bool SaveFileToFolder(HttpPostedFile postesFile, string filePath)
        {
            try
            {
                postesFile.SaveAs(filePath);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }
        public static void DeleteFile(string[] filenames)
        {
            try
            {
                foreach (string item in filenames)
                {
                    try
                    {
                        System.IO.File.Delete(item);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static void DeleteFile(string filename)
        {
            try
            {
                System.IO.File.Delete(filename);
            }
            catch (Exception ex)
            {
            }
        }
        public static void EmptyFolder(string folderPath,bool createIfNotExist)
        {
            try
            {
                if (createIfNotExist && !System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                    return;
                }
                string[] files = IOHelper.GetFiles(folderPath);
                IOHelper.DeleteFile(files);
            }
            catch (Exception ex)
            {
            }
        }

        public static string[] GetFiles(string folderPath)
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(folderPath);
                return files;
            }
            catch (Exception ex)
            {
                return new string[0];
                throw;
            }
        }

        public static string[] GetFolders(string folderPath)
        {
            try
            {
                string[] folders = System.IO.Directory.GetDirectories(folderPath);
                return folders;
            }
            catch (Exception ex)
            {
                return new string[0];
                throw;
            }
        }

        public static void DeleteFolder(string folderPath)
        {
            try
            {
                EmptyFolder(folderPath,false);
                System.IO.Directory.Delete(folderPath);
            }
            catch (Exception ex)
            {
            }
        }

        #region CLone folder
        public static void Clone(string fromFolder, string toFolder, bool isOverrideIfExist)
        {
            Clone(fromFolder, toFolder, isOverrideIfExist,string.Empty);
        }
        /// <summary>
        /// Move all file from fromFolder to toFolder
        /// </summary>
        /// <param name="fromFolder">source folder</param>
        /// <param name="toFolder">destination folder</param>
        /// <param name="isOverrideIfExist">true to override if destination folder already exist</param>
        /// 
        public static void Clone(string fromFolder, string toFolder, bool isOverrideIfExist, string regexIgnore)
        {
            if (!Directory.Exists(fromFolder)) { return; }
            if (Directory.Exists(toFolder) && !isOverrideIfExist) { return; }
            if (!Directory.Exists(toFolder)) { IOHelper.EmptyFolder(toFolder,true); }
            string[] files = IOHelper.GetFiles(fromFolder);
            foreach (string file in files)
            {
                if (!string.IsNullOrEmpty(regexIgnore) &&  StringHelper.IsMatch(regexIgnore, file)) { continue; }
                string desFile = Path.Combine(toFolder, Path.GetFileName(file));
                try
                {
                    File.Copy(file, desFile,isOverrideIfExist);
                    Logger.InfoWithParam("Move file '{0}' to '{1}' successful.", file, desFile);
                }
                catch (Exception ex)
                {
                    Logger.Error("Exception while move file '{0}' to '{1}'.", file, desFile);
                }
            }

            string[] folders = IOHelper.GetFolders(fromFolder);
            foreach (string folder in folders)
            {
                if (!string.IsNullOrEmpty(regexIgnore) && StringHelper.IsMatch(regexIgnore, folder)) { continue; }
                string folderName = Path.GetFileName(folder);
                if (folderName.StartsWith(".")) { continue; }
                string desFolder = Path.Combine(toFolder, folderName);
                Clone(folder, desFolder, true);
            }

        }
        #endregion
        public static void WriteToFile(string file, string newContent, bool isAppend)
        {
            StreamWriter stream = new StreamWriter(file,isAppend);
            stream.Write(newContent);
            stream.Close();
        }
    }
}

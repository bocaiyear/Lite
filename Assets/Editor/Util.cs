using System;
using System.IO;
using UnityEngine;

namespace Editor
{
    public class Util
    {
        public static void ClearDirectory(string path)
        {
            var dir = new DirectoryInfo(Path.GetFullPath(path));
            if (!dir.Exists)
            {
                return;
            }

            try
            {
                var fileInfo = dir.GetFileSystemInfos();
                foreach (var i in fileInfo)
                {
                    if (i is DirectoryInfo)
                    {
                        var subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);
                    }
                    else
                    {
                        File.Delete(i.FullName);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to clear folder '{path}'.\n{e.StackTrace}");
            }
        }
        
        public static string GetPrjRootPath()
        {
            string currentDir = Application.dataPath;
            int index = currentDir.LastIndexOf("/", System.StringComparison.Ordinal);
            currentDir = currentDir.Remove(index);
            return currentDir;
        }
        
        public static string GetBuildDir()
        {
            var buildDir = $"{GetPrjRootPath()}/Build/";

            if (!Directory.Exists(buildDir))
                Directory.CreateDirectory(buildDir);

            return buildDir;
        }
    }
}
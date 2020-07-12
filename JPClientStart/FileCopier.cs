using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JPClientStart
{
    public class FileCopier
    {
        private static string sourcePath;

        private static string targetPath;

        private static string gamePath = @"Game\DATA\FINAL";

        private static string pluginsPath = @"Plugins";

        public static void Run()
        {
            Console.WriteLine("Enter source path (\"...\\Riot Games\\Leaguage of Legends\" as usual):");
            sourcePath = Console.ReadLine();
            Console.WriteLine("Enter target path (\"...Garena\\Games\\32787\" as usual):");
            targetPath = Console.ReadLine();

            try
            {
                //Copy Champions directory
                FileSystem.CopyDirectory(Path.Combine(sourcePath, gamePath), Path.Combine(targetPath, gamePath), UIOption.AllDialogs);

                //Copy Plugins directory
                FileSystem.CopyDirectory(Path.Combine(sourcePath, pluginsPath, "rcp-be-lol-game-data"), Path.Combine(targetPath, "LeagueClient", pluginsPath, "rcp-be-lol-game-data"), UIOption.AllDialogs);

                FileSystem.CopyDirectory(Path.Combine(sourcePath, pluginsPath, "rcp-fe-lol-typekit"), Path.Combine(targetPath, "LeagueClient", pluginsPath, "rcp-fe-lol-typekit"), UIOption.AllDialogs);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

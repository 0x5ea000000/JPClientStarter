using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace JPClientStart
{
    public class Program
    {
        public static string Path
        {
            get
            {
                return Regex.Match(commandLine, pathPattern).Value;
            }
        }

        public static string Args
        {
            get
            {
                return commandLine.Substring(Path.Length + 1);
            }
        }

        private static string localePattern = @"--locale=[a-z]{2}_[A-Z]{2} ";

        private static string pathPattern = @""".*RiotClientServices\.exe""";

        private static string commandLine;

        private static bool gotClientcommandLine = false;

        private static void Main()
        {

            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    if (process.ProcessName == "RiotClientServices")
                    {
                        commandLine = process.GetCommandLine();
                        gotClientcommandLine = true;
                    }
                    if (process.ProcessName == "LeagueClient")
                    {
                        Console.WriteLine("Client process found!");
                        process.Kill();
                        Console.WriteLine("Client process has been killed!");
                        break;
                    }
                }
                catch (Win32Exception ex) when ((uint)ex.ErrorCode == 0x80004005)
                {
                    // Intentionally empty - no security access to the process.
                }
                catch (InvalidOperationException)
                {
                    // Intentionally empty - the process exited before getting details.
                }
            }
            if (!gotClientcommandLine)
            {
                Console.WriteLine("Client process not found! Press any key to exit...");
                Console.ReadLine();
                return;
            }

            bool quit = false;
            while (!quit)
            {
                Console.WriteLine("Select Client locale: ");
                Console.WriteLine("1. vn_VN");
                Console.WriteLine("2. en_US");
                Console.WriteLine("3. ja_JP");
                Console.WriteLine("4. ko_KR");

                int option = int.Parse(Console.ReadLine());

                Console.WriteLine($"Your Option: {option}");

                switch (option)
                {
                    case 1:
                        commandLine = Regex.Replace(commandLine, localePattern, "--locale=vn_VN ");
                        quit = true;
                        break;
                    case 2:
                        commandLine = Regex.Replace(commandLine, localePattern, "--locale=en_US ");
                        quit = true;
                        break;
                    case 3:
                        commandLine = Regex.Replace(commandLine, localePattern, "--locale=ja_JP ");
                        quit = true;
                        break;
                    case 4:
                        commandLine = Regex.Replace(commandLine, localePattern, "--locale=ko_KR ");
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Dont Understand?");
                        break;
                }
            }
            Console.WriteLine(Path);
            Console.WriteLine(Args);
            var client = Process.Start(Path, Args);
            if (client.Id != 0)
            {
                Console.WriteLine("Client Started successfully! Press any key to exit...");
                Console.ReadLine();
            }
        }
    }

}



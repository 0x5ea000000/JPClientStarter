using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace JPClientStart
{
    public class Program
    {
        private static string CommandLine;

        public static string Path
        {
            get
            {
                return CommandLine.Substring(1, 56);
            }
        }

        public static string Args
        {
            get
            {
                return CommandLine.Substring(59);
            }
        }

        private static bool GotClientCommandLine = false;

        private static void Main()
        {
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    if (process.ProcessName == "RiotClientServices")
                    {
                        CommandLine = process.GetCommandLine();
                        GotClientCommandLine = true;
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
            if (!GotClientCommandLine)
            {
                Console.WriteLine("Client process not found! Press any key to exit!");
                Console.ReadLine();
                return;
            }

            bool quit = false;
            while (!quit)
            {
                Console.WriteLine("Select Client locale: ");
                Console.WriteLine("1. vi-VN");
                Console.WriteLine("2. en-US");
                Console.WriteLine("3. ja-JP");
                Console.WriteLine("4. ko-KR");

                int option = int.Parse(Console.ReadLine());

                Console.WriteLine($"Your Option: {option}");

                switch (option)
                {
                    case 1:
                        CommandLine = CommandLine.Replace("--locale=en_US", "--locale=vi_VN");
                        CommandLine = CommandLine.Replace("--locale=vi_VN", "--locale=vi_VN");
                        quit = true;
                        break;
                    case 2:
                        CommandLine = CommandLine.Replace("--locale=en-US", "--locale=en_US");
                        CommandLine = CommandLine.Replace("--locale=vi_VN", "--locale=en_US");
                        quit = true;
                        break;
                    case 3:
                        CommandLine = CommandLine.Replace("--locale=en_US", "--locale=ja_JP");
                        CommandLine = CommandLine.Replace("--locale=vi_VN", "--locale=ja_JP");
                        quit = true;
                        break;
                    case 4:
                        CommandLine = CommandLine.Replace("--locale=en-US", "--locale=ko_KR");
                        CommandLine = CommandLine.Replace("--locale=vi_VN", "--locale=ko_KR");
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Dont Understand?");
                        break;
                }
            }
            Console.WriteLine(Args);
            var client = Process.Start(Path, Args);
            if (client.Id != 0)
            {
                Console.WriteLine("Client Started successfully! Press any key to exit!");
            }
        }
    }

}



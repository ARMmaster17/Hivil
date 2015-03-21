#define NOHEAD
#undef VER_ENTERPRISE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime;
using System.IO;

using hivil;
using HIVEcsl;

namespace hivil
{
    class Program
    {
        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">Args passed from the command line</param>
        static void Main(string[] args)
        {
            /////////////////////////////////////////////
            string GLOBAL_VERSION_NAME = "DEVELOPMENT";
            int GLOBAL_VERSION_MAJOR = 1;
            int GLOBAL_VERSION_MINOR = 0;
            int GLOBAL_VERSION_PATCH = 0;
            int GLOBAL_VERSION_BUILD = 26;
            string STARTUP_FILEPATH;
            string UNIT_ID;
            /////////////////////////////////////////////
            // Display startup messages
            pline("HIVIL \u00a9 2015 Joshua Zenn");
            pline("Version: " + GLOBAL_VERSION_NAME + " " + GLOBAL_VERSION_MAJOR.ToString() + "." + GLOBAL_VERSION_MINOR.ToString() + "." + GLOBAL_VERSION_PATCH.ToString());
            pline("Build " + GLOBAL_VERSION_BUILD);
            pline("Starting up...");
            // Insert startup commands and variable initializations here
#if NOHEAD
            UNIT_ID = "TESTUNIT001";
#else
            // TODO: Insert UNITID lookup code
            UNIT_ID = "UNKNOWN";
#endif
            STARTUP_FILEPATH = Directory.GetCurrentDirectory();
            pline("Setting up filesystem...");
            if(Directory.Exists(STARTUP_FILEPATH + @"/filestore"))
            {
                // The directory does exist, delete it just to be safe
                pline("Clearing filestore");
                Directory.Delete(STARTUP_FILEPATH + @"/filestore", true);
            }
            // Create the /appstore directory
            pline("Restoring filestore");
            Directory.CreateDirectory(STARTUP_FILEPATH + @"/appstore");
            /////////////////////////////////////////////
            while (true)
            {
                commandLoop(UNIT_ID);
            }
            /////////////////////////////////////////////
        }
        
        public static /*async*/ void commandLoop(string UNITID)
        {
            Console.Write("@{0}: ", UNITID);
            string inputRAW = Console.ReadLine();
            string[] cmds = inputRAW.Split(' ');
            #region cmd::quit
            if (cmds[0].ToLower() == "quit")
            {
                if (cmds.Length > 1)
                {
                    if (cmds[1].Contains("f") && cmds[1].Contains("i"))
                    {
                        // Force a shutdown
                        pline("Forcing a shutdown...");
                        //TODO: Kill all threads
                        //TODO: show interactive messages on shutdown of each thread
                        termination.Terminate("User forced a shutdown", 0);
                    }
                    else if (cmds[1].Contains("f"))
                    {
                        // Force a shutdown
                        //TODO: Kill all threads
                        termination.Terminate("User forced a shutdown", 0);
                    }
                    else
                    {
                        pline("Unrecognized option: " + cmds[1].ToLower().ToString());
                        // Procced with normal shutdown
                        termination.quit();
                    }
                }
                else
                {
                    // Shutdown
                    termination.quit();
                }
            }
            #endregion
            #region cmd::compile
            else if (cmds[0].ToLower() == "compile")
            {
                if (cmds.Length == 2)
                {
                    // Compile using path given in cmds[1]
                    CompileAsync CA = new CompileAsync(cmds[1]);
                    Task.Factory.StartNew(() => CA.StartAsync());
                }
                else if (cmds.Length == 3)
                {
                    // Compile using path given in cmds[1] with the options given in cmds[2]
                }
                else
                {
                    pline("Bad option count");
                }
            }
            #endregion
            #region cmd::UNKNOWN
            else
            {
                pline("Unrecognized command");
            }
            #endregion
        }

        /// <summary>
        /// Returns name of the script
        /// </summary>
        /// <param name="line">The line of source containing #NAME parameter</param>
        /// <returns>Name of script</returns>
        static string getScriptName(string line)
        {
            if (!line.StartsWith("#NAME:"))
            {
                termination.Terminate("Header file is missing properties", 3);
            }
            line = line.Replace("#NAME:", "");
            return line;
        }

        /// <summary>
        /// Checks required version of HIVE required to run source script
        /// </summary>
        /// <param name="version">Line of source containing #HIVE parameter</param>
        /// <param name="major">Major version number installed</param>
        /// <param name="minor">Minor version number installed</param>
        static void versionCheck(string version, int major, int minor)
        {
            if (!version.StartsWith("#HIVE:"))
            {
                termination.Terminate("Header file is missing properties", 3);
            }
            version = version.Replace("#HIVE:", "");
            string[] versions = version.Split('.');
            if(Convert.ToInt32(versions[0]) > major)
            {
                // Error, to big of a version change
                termination.Terminate("Inadaquate version of HIVIL", 4);
            }
            if(Convert.ToInt32(versions[1]) > minor)
            {
                // Error, to big of a version change
                termination.Terminate("Inadaquate version of HIVIL", 4);
            }
        }

        /// <summary>
        /// Checks required version (Major only) of HIVE required to run source script
        /// </summary>
        /// <param name="version">Line of source containing #HIVE parameter</param>
        /// <param name="major">Major version number installed</param>
        static void versionCheck(string version, int major)
        {
            if (!version.StartsWith("#HIVE:"))
            {
                termination.Terminate("Header file is missing properties", 3);
            }
            version = version.Replace("#HIVE:", "");
            string[] versions = version.Split('.');
            if (Convert.ToInt32(versions[0]) > major)
            {
                // Error, to big of a version change
                termination.Terminate("Inadaquate version of HIVIL", 4);
            }
        }
        static void pline(string line)
        {
            //Console.WriteLine("[{0}:{1}:{2}][HIVE CORE] {3}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Millisecond.ToString(), line.Trim());
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Millisecond.ToString());
            Console.ResetColor();
            Console.Write("][");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("HIVE CORE");
            Console.ResetColor();
            Console.Write("] ");
            Console.Write(line.Trim());
            Console.Write(Environment.NewLine);
        }
    }
}

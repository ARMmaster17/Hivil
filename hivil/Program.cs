using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.IO;

using hivil;
using hiveenv;

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
            string STARTUP_FILEPATH;
            /////////////////////////////////////////////
            Console.WriteLine("HIVIL \u00a9 2015 Joshua Zenn");
            Console.WriteLine("Version: {0} {1}.{2}", GLOBAL_VERSION_NAME, GLOBAL_VERSION_MAJOR.ToString(), GLOBAL_VERSION_MINOR.ToString());
            STARTUP_FILEPATH = args[0];
            if(STARTUP_FILEPATH == null || STARTUP_FILEPATH == "")
            {
                termination.Terminate("No .hive script is defined", 1);
            }
            if(!STARTUP_FILEPATH.EndsWith(".hive") || !STARTUP_FILEPATH.EndsWith(".HIVE"))
            {
                termination.Terminate("Filepath " + STARTUP_FILEPATH + " is not a valid HIVE script", 2);
            }
            // Everything outside file checks out, start reading the file
            StreamReader SR = new StreamReader(STARTUP_FILEPATH);
            versionCheck(SR.ReadLine().Trim(), GLOBAL_VERSION_MAJOR, GLOBAL_VERSION_MINOR);
            Console.WriteLine("Setting up HIVE cluster enviroment...");
            HiveManager hiveManager = new HiveManager();
            hiveManager.init();

            Console.WriteLine("Executing script: {0} on Node 0", getScriptName(SR.ReadLine().Trim()));
            termination.quit();
        }
        static string getScriptName(string line)
        {
            if (!line.StartsWith("#NAME:"))
            {
                termination.Terminate("Header file is missing properties", 3);
            }
            line = line.Replace("#NAME:", "");
            return line;
        }
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
    }
}

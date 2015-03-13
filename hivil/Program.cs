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
            // Display startup messages
            Console.WriteLine("HIVIL \u00a9 2015 Joshua Zenn");
            Console.WriteLine("Version: {0} {1}.{2}", GLOBAL_VERSION_NAME, GLOBAL_VERSION_MAJOR.ToString(), GLOBAL_VERSION_MINOR.ToString());
            // Get the path of the script to run
            STARTUP_FILEPATH = args[0];
            // Make sure it isn't empty
            if(STARTUP_FILEPATH == null || STARTUP_FILEPATH == "")
            {
                termination.Terminate("No .hive script is defined", 1);
            }
            // Make sure it is a valid HIVE file
            if(!STARTUP_FILEPATH.EndsWith(".hive") || !STARTUP_FILEPATH.EndsWith(".HIVE"))
            {
                termination.Terminate("Filepath " + STARTUP_FILEPATH + " is not a valid HIVE script", 2);
            }
            // Create a new StreamReader object and bind it to the file stream
            StreamReader SR = new StreamReader(STARTUP_FILEPATH);
            // Read the first line and check the version number
            versionCheck(SR.ReadLine().Trim(), GLOBAL_VERSION_MAJOR, GLOBAL_VERSION_MINOR);
            // Begin setting up the HIVE enviroment
            Console.WriteLine("Setting up HIVE cluster enviroment...");
            HiveManager hiveManager = new HiveManager();
            hiveManager.init();
            // All done setting up
            Console.WriteLine("Executing script: {0} on Node 0", getScriptName(SR.ReadLine().Trim()));

            /////////////////////
            // EXECUTION START //
            /////////////////////

            // Parse the file
            // TODO: Parse the file
            // Start running the jobs listed on task list
            // TODO: Run jobs on task list
            // At end of execution, tell all machines to go into NodeState.Standby
            // TODO: Order all machines to go into standby

            ////////////////////
            // EXECUTION STOP //
            ////////////////////

            // Tell the user the program has exited
            termination.quit();
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
    }
}

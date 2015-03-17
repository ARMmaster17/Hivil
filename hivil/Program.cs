using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.IO;

using hivil;

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
            /////////////////////////////////////////////
            // Display startup messages
            pline("HIVIL \u00a9 2015 Joshua Zenn");
            pline("Version: " + GLOBAL_VERSION_NAME + " " + GLOBAL_VERSION_MAJOR.ToString() + "." + GLOBAL_VERSION_MINOR.ToString() + "." + GLOBAL_VERSION_PATCH.ToString());
            pline("Build " + GLOBAL_VERSION_BUILD);
            pline("Starting up...");
            // Insert startup commands and variable initializations here
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
        static void pline(string line)
        {
            Console.WriteLine("[{0}:{1}:{2}] {3}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Millisecond.ToString(), line.Trim());
        }
    }
}

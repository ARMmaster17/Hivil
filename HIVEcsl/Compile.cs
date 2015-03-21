using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HIVEcsl
{
    public class CompileAsync
    {
        string filepath;
        CompileOptions CO;

        Dictionary<string, string> ProgramOptions = new Dictionary<string, string>();

        [Flags]
        enum CompileOptions
        {
            interactive = 1
        }

        public CompileAsync(string fp)
        {
            filepath = fp;
            CO = new CompileOptions();
        }
        public CompileAsync(string fp, string opt)
        {
            filepath = fp;
            CO = new CompileOptions();
            if(opt.Contains('i'))
            {
                // Interactive mode is enabled
                CO = CO | CompileOptions.interactive;
            }
            
        }

        public /*async*/ void StartAsync()
        {
            #region Init Setup
            cpln("Starting up");
            string activeDirectory = Path.GetDirectoryName(filepath);
            List<string> cmdlist = new List<string>();
            //Dictionary<string, string> dirList = new Dictionary<string, string>();
            Dictionary<string, List<string>> jobList = new Dictionary<string, List<string>>();
            //Dictionary<string, Type> varlist = new Dictionary<string, Type>();
            Dictionary<string, Dictionary<string, Type>> varlist = new Dictionary<string, Dictionary<string, Type>>();
            string currentJob;
            bool fatalErrorFound = false;
            bool injob = false;
            programData pd = new programData();
            int errorCount = 0;
            int warningCount = 0;
            int lineCount = 0;
            #endregion
            #region File Setup
            cpln("Opening source file");
            StreamReader STREAM_source;
            try
            {
                STREAM_source = new StreamReader(filepath);
            }
            catch
            {
                pln("ERROR: FILE NOT FOUND", ConsoleColor.Red);
                return;
            }
            #endregion
            #region Read Loop
            while(!fatalErrorFound && !STREAM_source.EndOfStream)
            {
                lineCount++;
                string cmdraw = STREAM_source.ReadLine().Trim();
                #region DIRECTIVES
                if (cmdraw.StartsWith("#"))
                {
                    if(cmdraw.StartsWith("#HIVE:"))
                    {
                        pd.version = cmdraw.Replace("#HIVE:", "").Trim();
                    }
                    else if(cmdraw.StartsWith("#NAME:"))
                    {
                        pd.name = cmdraw.Replace("#NAME:", "").Trim();
                    }
                    else if (cmdraw.StartsWith("#START:"))
                    {
                        pd.mainJob = cmdraw.Replace("#START:", "").Trim();
                    }
                    else if (cmdraw.StartsWith("#DESC:"))
                    {
                        pd.description = cmdraw.Replace("#DESC:", "").Trim();
                    }
                    else
                    {
                        errorCount++;
                        errpln("Unknown directive: " + cmdraw);
                    }
                    cpln("Added directive: " + cmdraw);
                }
                #endregion
                #region Job Descriptors
                else if(cmdraw.StartsWith("JOB"))
                {
                    injob = true;
                    currentJob = cmdraw.Split(' ')[1];
                    jobList.Add(cmdraw.Split(' ')[1], new List<string>());
                    varlist.Add(cmdraw.Split(' ')[1], new Dictionary<string, Type>());
                }
                else if(cmdraw.StartsWith("ENDJOB"))
                {
                    injob = false;
                    currentJob = null;
                }
                #endregion
                #region Basic Math
                #region Add

                #endregion
                #region Subtract

                #endregion
                #endregion
                #region COMMENTS
                else if(cmdraw.StartsWith("//"))
                {
                    // Do nothing, this is a comment
                }
                #endregion
                else
                {
                    errorCount++;
                    errpln("Line " + lineCount.ToString() + ": Unknown command");
                }
            }
            #endregion
            pln("Compile complete");
            pln("Errors found: " + errorCount.ToString());
            pln("Warnings found: " + warningCount.ToString());
        }
        public struct programData
        {
            public string name;
            public string version;
            public string mainJob;
            public string description;
        }
        public static void pln(string line)
        {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Millisecond.ToString());
            Console.ResetColor();
            Console.Write("][");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("COMPILE");
            Console.ResetColor();
            Console.Write("] ");
            Console.Write(line.Trim());
            Console.Write(Environment.NewLine);
        }
        public static void pln(string line, ConsoleColor CC)
        {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Millisecond.ToString());
            Console.ResetColor();
            Console.Write("][");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("COMPILE");
            Console.ResetColor();
            Console.Write("] ");
            Console.ForegroundColor = CC;
            Console.Write(line.Trim());
            Console.ResetColor();
            Console.Write(Environment.NewLine);
        }
        public static void errpln(string line)
        {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Millisecond.ToString());
            Console.ResetColor();
            Console.Write("][");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("COMPILE");
            Console.ResetColor();
            Console.Write("][");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ERROR");
            Console.ResetColor();
            Console.Write("] ");
            Console.Write(line.Trim());
            Console.Write(Environment.NewLine);
        }
        public static void warnpln(string line)
        {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Millisecond.ToString());
            Console.ResetColor();
            Console.Write("][");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("COMPILE");
            Console.ResetColor();
            Console.Write("][");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("ERROR");
            Console.ResetColor();
            Console.Write("] ");
            Console.Write(line.Trim());
            Console.Write(Environment.NewLine);
        }
        public void cpln(string line)
        {
            if(CO.HasFlag(CompileOptions.interactive))
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Millisecond.ToString());
                Console.ResetColor();
                Console.Write("][");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("COMPILE");
                Console.ResetColor();
                Console.Write("] ");
                Console.Write(line.Trim());
                Console.Write(Environment.NewLine);
            }
        }
        public void cpln(string line, ConsoleColor CC)
        {
            if (CO.HasFlag(CompileOptions.interactive))
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}:{1}:{2}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Millisecond.ToString());
                Console.ResetColor();
                Console.Write("][");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("COMPILE");
                Console.ResetColor();
                Console.Write("] ");
                Console.ForegroundColor = CC;
                Console.Write(line.Trim());
                Console.ResetColor();
                Console.Write(Environment.NewLine);
            }
        }
    }
}

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
            cpln("Starting up...");
            #endregion
            #region File Setup
            try
            {
                TextReader STREAM_source = new StreamReader(filepath);
            }
            catch
            {
                pln("ERROR: FILE NOT FOUND", ConsoleColor.Red);
                return;
            }
            #endregion

            pln("Compile complete");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace hivil
{
    namespace parse
    {
        public class Parser
        {
            public static ParseResult parseFile(ref StreamReader SR)
            {
                return new ParseResult();
            }
        }
        public class ParseResult
        {
            List<string> jobList;
            List<List<string>> jobInstructions;
            List<JobFlags> jobFlags;
            public ParseResult()
            {
                
            }
        }
        [Flags]
        public enum JobFlags
        {
            None = 0,
            LocalJob = 1,
            ReturnsValue = 2
        }
    }
}

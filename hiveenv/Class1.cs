using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hiveenv.Nodes;

namespace hiveenv
{
    public class HiveManager
    {
        List<SlaveNode> slaveNodeList = new List<SlaveNode>();
        public HiveManager()
        {

        }
        public int init()
        {
            return 0;
        }
        public SlaveNode[] getNodes()
        {

        }
        public static string getVersion()
        {
            return "HIVEenv Version DEVELOPMENT 1.0 \n Copyright 2015 Joshua Zenn";
        }
    }
}

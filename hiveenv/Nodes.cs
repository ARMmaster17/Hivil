using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace hiveenv
{
    namespace Nodes
    {
        class Node
        {
            NodeState nodeState;
            string nodeName;
            DateTime upStart;
            IPAddress ipAddress;

            protected internal void setUpStartTime()
            {
                // Get current time
                upStart = DateTime.Now;
            }
            protected internal void setUpStartTime(DateTime DT)
            {
                upStart = DT;
            }
        }
        class SlaveNode : Node
        {
            public SlaveNode()
            {
                // Set up a TCP connection to HIVE node
            }
        }
        class MasterNode : Node
        {
            public MasterNode()
            {
                // Set up a TCP connection to HIVE master node
            }
        }

        enum NodeState
        {
            Offline,
            Starting,
            Online,
            Working,
            Waiting,
            Reconfiguring
        }
    }
}

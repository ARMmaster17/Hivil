using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace hiveapi
{
    public class APIobject
    {
        //HIVEvar<int> hv = new HIVEvar<int>();
        int api_PROCESSID;
        IPAddress api_HOSTIP;
        int api_HOSTPORT;
        public APIobject(string hostIP, int hostPort, int processID)
        {
            api_HOSTIP = IPAddress.Parse(hostIP);
            api_HOSTPORT = hostPort;
            api_PROCESSID = processID;
        }
    }
    public class HIVEvar<T>
    {
        public string name;
        public T value
        {
            get
            {
                return value;
            }
            set
            {
                IPEndPoint endPoint = new IPEndPoint(Ipaddress.Parse("127.0.0.1"), 8000);
                Socket serverConn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverConn.Connect(endPoint);
                serverConn.send(Encoding.ASCII.GetBytes("SET|" + name + "|" /*value*/));
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
        }
        public HIVEvar(string vname)
        {
            name = vname;
        }
    }
}

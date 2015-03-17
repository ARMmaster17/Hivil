using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hiveapi
{
    public class APIobject
    {
        HIVEvar<int> hv = new HIVEvar<int>();
        public APIobject()
        {
            hv.value = 5;
            List<int> i;
        }
    }
    public class HIVEvar<T>
    {
        public T value
        {
            get
            {
                return value;
            }
            set
            {

            }
        }
        public HIVEvar()
        {

        }
    }
}

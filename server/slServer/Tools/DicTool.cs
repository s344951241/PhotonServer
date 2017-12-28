using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slServer.Tools
{
    public class DicTool
    {
        public static T2 GetValue<T1, T2>(Dictionary<T1, T2> dic, T1 key)
        {
            T2 value;
            bool result = dic.TryGetValue(key, out value);
            if (result)
            {
                return value;
            }
            else
            {
                return default(T2);
            }
        }
    }
}

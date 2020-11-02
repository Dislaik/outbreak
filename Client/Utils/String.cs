using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Utils
{

    public class String
    {
        public static string DictionaryToString(Dictionary<string, dynamic> dictionary)
        {
            string dictionaryString = "{";
            foreach (KeyValuePair<string, dynamic> keyValues in dictionary)
            {
                dictionaryString += "\"" +keyValues.Key + "\":" + keyValues.Value + ",";
            }
            return dictionaryString.TrimEnd(',', ' ') + "}";
        }
    }
}

namespace Outbreak
{
    public class Console
    {
        public static void Debug(string Data) => CitizenFX.Core.Debug.WriteLine(Data);
        public static void Info(string Data) => CitizenFX.Core.Debug.WriteLine($"^5[Info]^7" + Data);
        public static void Warning(string Data) => CitizenFX.Core.Debug.WriteLine($"^3[Warning]^7" + Data);
        public static void Error(string Data) => CitizenFX.Core.Debug.WriteLine($"^1[Error]^7" + Data);
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Outbreak.Utils
{
    public class String
    {
        public static string DictionaryStringToInt(Dictionary<string, int> dictionary)
        {
            string dictionaryString = "{";
            foreach (KeyValuePair<string, int> keyValues in dictionary)
            {
                dictionaryString += "\"" + keyValues.Key + "\":" + keyValues.Value + ", ";
            }
            return dictionaryString.TrimEnd(',', ' ') + "}";
        }
    }
}
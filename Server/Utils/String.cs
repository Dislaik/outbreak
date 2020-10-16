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
        public static Dictionary<string, string> StringToDictionary(string Json)
        {
            string json = @"{" + Json + "}";
            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return values;
        }
    }
}
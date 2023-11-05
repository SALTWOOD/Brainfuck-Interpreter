using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Brainfuck
{
    internal class Regex
    {
        public static string ReplaceSlash(string directory)
        {
            return directory.Replace("\\\\", "\\").Replace("\\", "/");
        }

        public static string RegexReplace(string input, string replacement, string regex, RegexOptions options = RegexOptions.None) { return System.Text.RegularExpressions.Regex.Replace(input, regex, replacement, options); }
        public static string RegexMatch(string input, string reg, RegexOptions options = RegexOptions.None)
        {
            try
            {
                string result = System.Text.RegularExpressions.Regex.Match(input, reg, options).Value;
                return result;
            }
            finally { }
        }
    }
}

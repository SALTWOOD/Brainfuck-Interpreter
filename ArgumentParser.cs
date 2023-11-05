using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Brainfuck.Regex;

namespace Brainfuck
{
    public class ArgumentParser
    {
        public string? filename = null;
        public string workingDir = ReplaceSlash(AppDomain.CurrentDomain.BaseDirectory);

        public bool RunAsInterpreter
        {
            get
            {
                return this.filename == null;
            }
        }

        public ArgumentParser(string[] args)
        {
            string argHead = "";
            foreach (string i in args)
            {
                string current = RegexMatch(i, "[^-].*");
                bool isArgHead = !(current == i);
                switch (argHead)
                {
                    case "":
                        {
                            if (isArgHead)
                            {
                                argHead = current;
                            }
                            else
                            {
                                filename = i;
                            }
                            break;
                        }
                    case "w" when !isArgHead:
                    case "working-directory" when !isArgHead:
                        {
                            workingDir = i;
                            break;
                        }
                }
            }
        }
    }
}

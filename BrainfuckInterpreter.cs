using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainfuck
{
    public class BrainfuckInterpreter
    {
        byte[] bytes;
        int pointer = 0;
        public readonly Version version = new Version(0, 0, 2, 0);

        public BrainfuckInterpreter()
        {
            bytes = new byte[30000];//创建一个30000长度的byte数组
        }

        public void Main(string[] args)
        {
            ArgumentParser argp = new ArgumentParser(args);
            if (argp.RunAsInterpreter)
            {
                Console.WriteLine($"Brainfuck v{this.version} ({DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")}) at SALTWOOD/Brainfuck");
                Console.WriteLine("Type any Brainfuck command and enter to execute");
                while (true)
                {
                    try
                    {
                        Console.Write("\r>>> ");
                        string? command = Console.ReadLine();
                        if (command != null)
                        {
                            if (command != "")
                            {
                                Execute(command);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            throw new Exception("KeyboardInterrupt");
                        }
                    }
                    catch (Exception e)
                    {
                        WriteException(e);
                    }
                }
            }
            else if (argp.filename != null)
            {
                using (StreamReader sr = new StreamReader(argp.filename))
                {
                    string command = sr.ReadToEnd();
                    Execute(command);
                }
            }
        }

        private void WriteException(Exception e)
        {
            Console.WriteLine($"{e.ToString()}: {e.Message}\r\n{e.StackTrace}");
            if (e.InnerException != null)
            {
                Console.WriteLine("\r\nDuring handling of the above exception, another exception occurred.\r\n");
                WriteException(e.InnerException);
            }
        }

        public void Execute(string bfCommand)
        {
            string loopCommand = "";
            bool isInLoop = false;
            foreach (char c in bfCommand)
            {
                if (!isInLoop)
                {
                    switch (c)
                    {
                        case '>':
                            this.pointer++;
                            if (this.pointer > 30000)
                            {
                                throw new ArgumentOutOfRangeException($"Pointer should be between 0 and 30000, but found {this.pointer}");
                            }
                            break;
                        case '<':
                            this.pointer--;
                            if (this.pointer < 0)
                            {
                                throw new ArgumentOutOfRangeException($"Pointer should be between 0 and 30000, but found {this.pointer}");
                            }
                            break;
                        case '+':
                            this.bytes[this.pointer] += 1;
                            break;
                        case '-':
                            this.bytes[this.pointer] -= 1;
                            break;
                        case '.':
                            Console.Write(Encoding.UTF8.GetString(this.bytes.Skip(this.pointer).Take(1).ToArray()));
                            break;
                        case ',':
                            this.bytes[this.pointer] = Convert.ToByte(Console.Read());
                            break;
                        case '[':
                            isInLoop = true;
                            break;
                        case ']':
                            throw new ArgumentException("Unexpected identifier \"]\"");
                    }
                }
                else
                {
                    if (c != ']')
                    {
                        loopCommand += c;
                    }
                    else
                    {
                        isInLoop = false;
                        while (this.bytes[this.pointer] != 0)
                        {
                            Execute(loopCommand);
                        }
                    }
                }
            }
        }
    }
}

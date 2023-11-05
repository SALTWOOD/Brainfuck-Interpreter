namespace Brainfuck
{
    public static class Program
    {
        static void Main(string[] args)
        {
            BrainfuckInterpreter i = new BrainfuckInterpreter();
            i.Main(args);
        }
    }
}
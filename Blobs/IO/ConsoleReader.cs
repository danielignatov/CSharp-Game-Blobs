namespace Blobs.IO
{
    using Blobs.Interfaces;
    using System;

    class ConsoleReader : IInputReader
    {
        public string ReadLine()
        {
            string input;
            input = Console.ReadLine();
            return input;
        }
    }
}
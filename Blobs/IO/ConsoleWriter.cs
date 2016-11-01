namespace Blobs.IO
{
    using Blobs.Interfaces;
    using System;

    class ConsoleWriter : IOutputWriter
    {
        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }
    }
}
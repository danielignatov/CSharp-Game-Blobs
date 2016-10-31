namespace Blobs
{
    using Blobs.Models;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            IBlob dani = new Blob("Dani", 100, 10, Enums.BehaviorType.Aggressive, Enums.AttackType.Blobplode);
            Console.WriteLine(dani.Name);
        }
    }
}

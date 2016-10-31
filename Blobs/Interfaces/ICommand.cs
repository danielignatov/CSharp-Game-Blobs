namespace Blobs.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface ICommand
    {
        /// <summary>
        /// Send command to the game engine
        /// </summary>
        void ExecuteCommand(string command);
    }
}
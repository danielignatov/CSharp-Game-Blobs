using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blobs.IO
{
    public static class Message
    {
        public const string IncorrectInputArguments = "Invalid command arguments, type \"help\" to see valid command examples.";

        public const string HelpLineCreateCommand = "create <name> <health> <damage> <behavior> <attack>";

        public const string HelpLineCreateExplain = "Creates new blob with the specified behavior and attack.";

        public const string HelpLineCreateExample = "Example: create Preslava 100 10 Inflated PutridFart";

        public const string HelpLineAttackCommand = "attack <attacker> <target>";

        public const string HelpLineAttackExplain = "Forces a blob to perform an attack on another blob.";

        public const string HelpLineAttackExample = "Example: attack Preslava Fiki";

        public const string HelpLinePassCommand = "pass";

        public const string HelpLinePassExplain = "Does nothing, skips the turn and progresses the game.";

        public const string HelpLineStatusCommand = "status";

        public const string HelpLineStatusExplain = "Prints data about the current state of the game.";

        public const string HelpLineDropCommand = "drop";

        public const string HelpLineDropExplain = "Ends the program.";

        public const string HelpLineDivider = "###################################";
    }
}

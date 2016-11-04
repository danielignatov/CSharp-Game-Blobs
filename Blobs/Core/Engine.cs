namespace Blobs.Core
{
    using Blobs.Interfaces;
    using IO;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Engine : IEngine
    {
        // Fields
        private IInputReader inputReader;
        private IOutputWriter outputWriter;
        private IAttackTypeFactory attackFactory;
        private IBehaviorTypeFactory behaviorFactory;

        private Dictionary<string, IBlob> blobDatabase = new Dictionary<string, IBlob>();

        // Constructor
        public Engine(
            IInputReader inputReader,
            IOutputWriter outputWriter,
            IAttackTypeFactory attackFactory,
            IBehaviorTypeFactory behaviorFactory)
        {
            this.InputReader = inputReader;
            this.OutputWriter = outputWriter;
            this.AttackFactory = attackFactory;
            this.BehaviorFactory = behaviorFactory;
        }

        // Properties
        public IInputReader InputReader
        {
            get
            {
                return this.inputReader;
            }
            set
            {
                this.inputReader = value;
            }
        }

        public IOutputWriter OutputWriter
        {
            get
            {
                return this.outputWriter;
            }
            set
            {
                this.outputWriter = value;
            }
        }

        public IBehaviorTypeFactory BehaviorFactory
        {
            get
            {
                return this.behaviorFactory;
            }
            private set
            {
                this.behaviorFactory = value;
            }
        }

        public IAttackTypeFactory AttackFactory
        {
            get
            {
                return this.attackFactory;
            }
            private set
            {
                this.attackFactory = value;
            }
        }

        // Methods
        /// <summary>
        /// Keep the engine running by constantly expecting commands
        /// </summary>
        public void Run()
        {
            //string gameTitle = $"####################################################{Environment.NewLine}" +
            //                   $"#         ####   #       ###   ####    ####        #{Environment.NewLine}" +
            //                   $"#         #   #  #      #   #  #   #  #            #{Environment.NewLine}" +
            //                   $"#         ####   #      #   #  ####    ###         #{Environment.NewLine}" +
            //                   $"#         #   #  #      #   #  #   #      #        #{Environment.NewLine}" +
            //                   $"#         ####   #####   ###   ####   ####         #{Environment.NewLine}" +
            //                   $"####################################################";
            //
            //this.OutputWriter.WriteLine(gameTitle);

            while (true)
            {
                string[] commandArgs = InputReader.ReadLine().Trim().Split();
                this.ProcessCommand(commandArgs);
                this.Update();
            }
        }

        /// <summary>
        /// Make each Blob in database check if behavior is triggered and do changes in stats
        /// </summary>
        private void Update()
        {
            foreach (IBlob blob in blobDatabase.Values)
            {
                if (blob.IsBlobDead == false)
                {
                    blob.Update();
                }
            }
        }

        /// <summary>
        /// Receive string array from Run() method and direct commands to corresponding methods
        /// </summary>
        /// <param name="commandArgs"></param>
        private void ProcessCommand(string[] commandArgs)
        {
            switch (commandArgs[0])
            {
                case "create":
                    this.CreateBlob(commandArgs);
                    break;
                case "attack":
                    this.BlobAttackAnotherBlob(commandArgs);
                    break;
                case "pass":
                    break;
                case "status":
                    this.OutputStatusOfBlobs();
                    break;
                case "drop":
                    System.Environment.Exit(0);
                    break;
                case "help":
                    this.Help();
                    break;
                default:
                    //this.OutputWriter.WriteLine("Invalid command arguments, type \"help\" to see valid command examples.");
                    break;
            }
        }

        /// <summary>
        /// Print out help information about different commands.
        /// </summary>
        private void Help()
        {
            string helpInfo = $"####################### HELP #######################{Environment.NewLine}" +
                              $"create <name> <health> <damage> <behavior> <attack> {Environment.NewLine}" +
                              $"Create blob with the specified behavior and attack. {Environment.NewLine}" +
                              $"Example: create Preslava 100 10 Inflated PutridFart {Environment.NewLine}" +
                              $"####################################################{Environment.NewLine}" +
                              $"attack <attacker> <target>                          {Environment.NewLine}" +
                              $"Forces a blob to perform an attack on another blob. {Environment.NewLine}" +
                              $"Example: attack Preslava Fiki                       {Environment.NewLine}" +
                              $"####################################################{Environment.NewLine}" +
                              $"pass                                                {Environment.NewLine}" +
                              $"Does nothing, skips the turn and progresses the game{Environment.NewLine}" +
                              $"####################################################{Environment.NewLine}" +
                              $"status                                              {Environment.NewLine}" +
                              $"Prints data about the current state of the game.    {Environment.NewLine}" +
                              $"####################################################{Environment.NewLine}" +
                              $"drop                                                {Environment.NewLine}" +
                              $"Ends the program.                                   {Environment.NewLine}" +
                              $"####################################################";
            this.OutputWriter.WriteLine(helpInfo);
        }

        /// <summary>
        /// Print out status of blobs in Database
        /// </summary>
        private void OutputStatusOfBlobs()
        {
            foreach (var blob in this.blobDatabase.Values)
            {
                if (blob.IsBlobDead == true)
                {
                    this.OutputWriter.WriteLine($"Blob {blob.Name} KILLED");
                }
                else
                {
                    this.OutputWriter.WriteLine($"Blob {blob.Name}: {blob.Health} HP, {blob.AttackDamage} Damage");
                }
            }
        }

        /// <summary>
        /// Attacker damage value removes health value from target
        /// </summary>
        private void BlobAttackAnotherBlob(string[] commandArgs)
        {
            string attackerBlobName = commandArgs[1];
            string targetBlobName = commandArgs[2];

            if (!blobDatabase.ContainsKey(attackerBlobName))
            {
                this.OutputWriter.WriteLine("Attacker Blob does not exist.");
                return;
            }
            else if (!blobDatabase.ContainsKey(targetBlobName))
            {
                this.OutputWriter.WriteLine("Target Blob does not exist.");
                return;
            }

            IBlob attackerBlob = blobDatabase[attackerBlobName];
            IBlob targetBlob = blobDatabase[targetBlobName];

            if (attackerBlob.IsBlobDead == true)
            {
                this.OutputWriter.WriteLine("Attacker Blob is dead.");
                return;
            }
            else if (targetBlob.IsBlobDead == true)
            {
                this.OutputWriter.WriteLine("Target Blob is dead.");
                return;
            }

            attackerBlob.ProduceAttack();
            targetBlob.Health -= attackerBlob.AttackDamage;
            attackerBlob.AfterAttack();
        }

        /// <summary>
        /// Creates new Blob and add him in database.
        /// </summary>
        private void CreateBlob(string[] commandArgs)
        {
            IBlob newBlob = new Blob(
                commandArgs[1], // Name
                int.Parse(commandArgs[2]), // Health
                int.Parse(commandArgs[3]), // Attack Damage
                this.BehaviorFactory.CreateBehaviorType(commandArgs[4]), // Assign Behavior Type
                this.AttackFactory.CreateAttackType(commandArgs[5])); // Assign Attack Type
            blobDatabase.Add(newBlob.Name, newBlob);
        }
    }
}
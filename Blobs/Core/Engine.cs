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

        private List<IBlob> blobDatabase = new List<IBlob>();

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

        public IOutputWriter OutputWriter { get; set; }
        public IBehaviorTypeFactory BehaviorFactory { get; private set; }
        public IAttackTypeFactory AttackFactory { get; private set; }

        // Methods
        public void Run()
        {
            while (true)
            {
                string[] commandArgs = InputReader.ReadLine().Trim().Split();
                this.ProcessCommand(commandArgs);
            }
        }

        private void ProcessCommand(string[] commandArgs)
        {
            switch (commandArgs[0])
            {
                case "create":
                    this.CreateBlob(commandArgs); break;
                case "attack":
                    this.BlobAttackAnotherBlob(commandArgs); break;
                case "pass":
                    break;
                case "status":
                    this.OutputStatusOfBlobs(); break;
                case "drop":
                    System.Environment.Exit(0); break;
                case "help":
                    this.Help(); break;
                default:
                    this.OutputWriter.WriteLine(Message.IncorrectInputArguments); break;

            }
        }

        /// <summary>
        /// Print out help information about different commands.
        /// </summary>
        private void Help()
        {
            this.OutputWriter.WriteLine(Message.HelpLineCreateCommand);
            this.OutputWriter.WriteLine(Message.HelpLineCreateExplain);
            this.OutputWriter.WriteLine(Message.HelpLineCreateExample);
            this.OutputWriter.WriteLine(Message.HelpLineDivider);
            this.OutputWriter.WriteLine(Message.HelpLineAttackCommand);
            this.OutputWriter.WriteLine(Message.HelpLineAttackExplain);
            this.OutputWriter.WriteLine(Message.HelpLineAttackExample);
            this.OutputWriter.WriteLine(Message.HelpLineDivider);
            this.OutputWriter.WriteLine(Message.HelpLinePassCommand);
            this.OutputWriter.WriteLine(Message.HelpLinePassExplain);
            this.OutputWriter.WriteLine(Message.HelpLineDivider);
            this.OutputWriter.WriteLine(Message.HelpLineStatusCommand);
            this.OutputWriter.WriteLine(Message.HelpLineStatusExplain);
            this.OutputWriter.WriteLine(Message.HelpLineDivider);
            this.OutputWriter.WriteLine(Message.HelpLineDropCommand);
            this.OutputWriter.WriteLine(Message.HelpLineDropExplain);
        }

        private void OutputStatusOfBlobs()
        {
            foreach (var blob in this.blobDatabase)
            {
                if (blob.IsBlobDead == true)
                {

                }
                else
                {
                    this.OutputWriter.WriteLine("");
                }
            }
        }

        private void BlobAttackAnotherBlob(string[] commandArgs)
        {
            throw new NotImplementedException();
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
            blobDatabase.Add(newBlob);
        }
    }
}
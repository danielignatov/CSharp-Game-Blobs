namespace Blobs
{
    using Core;
    using Core.Factories;
    using Interfaces;
    using IO;

    class Program
    {
        static void Main(string[] args)
        {
            IInputReader inputReader = new ConsoleReader();
            IOutputWriter outputWriter = new ConsoleWriter();
            IAttackTypeFactory attackFactory = new AttackTypeFactory();
            IBehaviorTypeFactory behaviorFactory = new BehaviorTypeFactory();

            IEngine engine = new Engine(inputReader, outputWriter, attackFactory, behaviorFactory);
            engine.Run();
        }
    }
}
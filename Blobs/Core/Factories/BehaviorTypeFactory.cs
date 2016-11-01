namespace Blobs.Core.Factories
{
    using Blobs.Enums;
    using Blobs.Interfaces;
    using System;

    class BehaviorTypeFactory : IBehaviorTypeFactory
    {
        public BehaviorType CreateBehaviorType(string behaviorType)
        {
            BehaviorType type = (BehaviorType)Enum.Parse(typeof(BehaviorType), behaviorType);

            return type;
        }
    }
}
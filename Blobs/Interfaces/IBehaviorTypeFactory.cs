namespace Blobs.Interfaces
{
    using Blobs.Enums;

    public interface IBehaviorTypeFactory
    {
        BehaviorType CreateBehaviorType(string behaviorType);
    }
}
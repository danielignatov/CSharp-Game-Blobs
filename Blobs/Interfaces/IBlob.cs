namespace Blobs.Interfaces
{
    using Blobs.Enums;

    interface IBlob
    {
        string Name { get; set; }
        int Health { get; set; }
        int AttackDamage { get; set; }
        BehaviorType? CurrentBehaviorType { get; set; }
        AttackType CurrentAttackType { get; set; }
        bool IsBlobDead { get; }
    }
}
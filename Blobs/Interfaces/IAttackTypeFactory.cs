namespace Blobs.Interfaces
{
    using Blobs.Enums;

    public interface IAttackTypeFactory
    {
        AttackType CreateAttackType(string attackType);
    }
}
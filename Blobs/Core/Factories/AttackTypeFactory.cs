namespace Blobs.Core.Factories
{
    using Blobs.Enums;
    using Blobs.Interfaces;
    using System;

    class AttackTypeFactory : IAttackTypeFactory
    {
        public AttackType CreateAttackType(string attackType)
        {
            AttackType type = (AttackType)Enum.Parse(typeof(AttackType), attackType);

            return type;
        }
    }
}
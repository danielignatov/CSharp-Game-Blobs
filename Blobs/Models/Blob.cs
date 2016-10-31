namespace Blobs.Models
{
    using Enums;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Blob : IBlob
    {
        // Fields
        private string name;

        private int health;

        private int attackDamage;

        private BehaviorType? currentBehaviorType;

        private AttackType currentAttackType;

        private bool isBlobDead;

        // Constructor
        public Blob(string name, int health, int attackDamage, BehaviorType behaviorType, AttackType currentAttackType)
        {
            this.Name = name;
            this.Health = health;
            this.AttackDamage = attackDamage;
            this.CurrentBehaviorType = behaviorType;
            this.CurrentAttackType = currentAttackType;
            this.isBlobDead = false;
        }

        // Properties
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid Blob Name.");
                }

                this.name = value;
            }
        }

        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                this.health = value;

                if (this.health <= 0)
                {
                    this.isBlobDead = true;
                }
            }
        }

        public int AttackDamage
        {
            get
            {
                return this.attackDamage;
            }
            set
            {
                this.attackDamage = value;
            }
        }

        public BehaviorType? CurrentBehaviorType
        {
            get
            {
                return this.currentBehaviorType;
            }
            set
            {
                if (this.currentBehaviorType == null)
                {
                    this.currentBehaviorType = value;
                }
            }
        }

        public AttackType CurrentAttackType
        {
            get
            {
                return this.currentAttackType;
            }
            set
            {
                this.currentAttackType = value;
            }
        }

        public bool IsBlobDead
        {
            get
            {
                return this.isBlobDead;
            }
        }
    }
}
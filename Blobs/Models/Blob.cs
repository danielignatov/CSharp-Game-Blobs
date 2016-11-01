namespace Blobs.Models
{
    using Enums;
    using Interfaces;
    using System;

    public class Blob : IBlob
    {
        // Fields
        private string name;

        private int health;

        private int attackDamage;

        private BehaviorType? currentBehaviorType;

        private AttackType currentAttackType;

        private bool isBlobDead;

        private bool isBehaviorActivated;

        private int initialHealth;

        // Constructor
        public Blob(string name, int health, int attackDamage, BehaviorType behaviorType, AttackType currentAttackType)
        {
            this.Name = name;
            this.Health = health;
            this.AttackDamage = attackDamage;
            this.CurrentBehaviorType = behaviorType;
            this.CurrentAttackType = currentAttackType;
            if (this.CurrentAttackType == AttackType.Blobplode)
            {
                this.AttackDamage *= 2;
            }
            this.isBlobDead = false;
            this.isBehaviorActivated = false;
            this.initialHealth = health;
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
                else if (this.health <= (this.initialHealth / 2))
                {
                    // Activate behavior
                    if (this.isBehaviorActivated == false)
                    {
                        if (this.CurrentBehaviorType == BehaviorType.Aggressive)
                        {
                            this.AttackDamage *= 2;
                        }
                        else if (this.CurrentBehaviorType == BehaviorType.Inflated)
                        {
                            this.health += 50;
                        }

                        this.isBehaviorActivated = true;
                    }
                    else
                    {
                        if (this.CurrentBehaviorType == BehaviorType.Aggressive)
                        {
                            this.health -= 5;
                        }
                        else if (this.CurrentBehaviorType == BehaviorType.Inflated)
                        {
                            this.health -= 10;
                        }

                        if (this.CurrentAttackType == AttackType.Blobplode)
                        {
                            this.health /= 2;
                        }

                        if (this.health <= 0)
                        {
                            this.isBlobDead = true;
                        }
                    }
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

        public void Update()
        {

        }
    }
}
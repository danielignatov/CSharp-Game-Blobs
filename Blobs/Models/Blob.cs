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

        private float initialHealth;

        private int initialAttackDamage;

        // Constructor
        public Blob(string name, int health, int attackDamage, BehaviorType behaviorType, AttackType currentAttackType)
        {
            this.Name = name;
            this.Health = health;
            this.AttackDamage = attackDamage;
            this.CurrentBehaviorType = behaviorType;
            this.CurrentAttackType = currentAttackType;
            this.isBlobDead = false;
            this.isBehaviorActivated = false;
            this.initialHealth = health;
            this.initialAttackDamage = attackDamage;
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
            float exactHalfOfInitialHealth = this.initialHealth / 2f;
            int halfOfInitialHealth = (int)Math.Ceiling(exactHalfOfInitialHealth);

            if (this.Health <= 0)
            {
                if ((this.isBehaviorActivated == false) && (this.CurrentBehaviorType == BehaviorType.Inflated))
                {
                    this.Health = 50;
                    this.isBehaviorActivated = true;
                    return;
                }
                else
                {
                    this.isBlobDead = true;
                    return;
                }
            }

            // Activate behavior if not activated
            if ((this.Health <= halfOfInitialHealth) && (this.isBehaviorActivated == false))
            {
                if (this.CurrentBehaviorType == BehaviorType.Aggressive)
                {
                    this.AttackDamage *= 2;
                }
                else if (this.CurrentBehaviorType == BehaviorType.Inflated)
                {
                    this.Health += 50;
                }

                this.isBehaviorActivated = true;
            }
            else if (this.isBehaviorActivated == true)
            {
                if (this.CurrentBehaviorType == BehaviorType.Aggressive)
                {
                    if ((this.AttackDamage - 5) >= this.initialAttackDamage)
                    {
                        this.AttackDamage -= 5;
                    }
                    else
                    {
                        this.AttackDamage = this.initialAttackDamage;
                    }
                }
                else if (this.CurrentBehaviorType == BehaviorType.Inflated)
                {
                    this.health -= 10;

                    if (this.Health <= 0)
                    {
                        this.isBlobDead = true;
                    }
                }
            }
        }

        public void ProduceAttack()
        {
            if (this.CurrentAttackType == AttackType.Blobplode)
            {
                this.Health /= 2;
                this.AttackDamage *= 2;
            }

            if (this.Health <= 0)
            {
                this.isBlobDead = true;
            }
        }

        public void AfterAttack()
        {
            if (this.CurrentAttackType == AttackType.Blobplode)
            {
                this.AttackDamage /= 2;
            }
        }
    }
}
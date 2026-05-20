using System;
using System.Collections.Generic;
using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class Stats : IDisposable
    {
        #region Variables

        public List<Property> List;

        public float HP;
        public float MP;

        public float Health;
        public float Mana;
        public float Energy;
        public float Stamina;

        public float Strength;
        public float Constitution;
        public float Dexterity;
        public float Intelligence;
        public float Wisdom;
        public float Charisma;

        public float Perception;
        public float Endurance;
        public float Agility;
        public float Speed;
        public float Luck;

        public float Toughness;
        public float Vitality;
        public float Intuition;
        public float Willpower;

        public float Hunger;
        public float Thirst;
        public float Comfort;
        public float Bladder;
        public float Hygiene;
        public float Grime;

        public float Blood;
        public float Pain;
        public float Consciousness;
        public float Awareness;
        public float Alertness;
        public float Fatigue;

        public float Happiness;
        public float Boredom;
        public float Depression;
        public float Sanity;
        public float Paranoia;
        public float Disposition;

        public float Initiative;
        public float Evasion;
        public float Accuracy;
        public float Fortitude;
        public float Reflex;
        public float Will;

        public float Body;
        public float Mind;
        public float Spirit;

        public float Physical;
        public float Mental;
        public float Social;

        public float Damage;
        public float Magic;
        public float Armor;

        public float Attack;
        public float Defense;
        public float MagicAttack;
        public float MagicDefense;
        public float CriticalChance;
        public float CriticalDamage;

        #endregion

        #region Constructors

        public Stats()
        {
            List = new List<Property>();
        }

        #endregion

        #region Methods

        public virtual Property GetStat(string name)
        {
            int count = List.Count;
            for (int i = 0; i < count; i++)
            {
                Property existing = List[i];
                if (existing != null)
                {
                    if (existing.Name == name)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public void Dispose()
        {
            foreach (Property stat in List)
            {
                stat.Dispose();
            }
        }

        #endregion
    }
}

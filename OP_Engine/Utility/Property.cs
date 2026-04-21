using System;

namespace OP_Engine.Utility
{
    public class Property : IDisposable
    {
        #region Variables

        public string Name;
        public string Description;
        public float Value;
        public float Max_Value;
        public float Rate;

        #endregion

        #region Constructors

        public Property()
        {
            
        }

        public Property(string name, float value)
        {
            Name = name;
            Value = value;
        }

        public Property(string name, float value, float max_value)
        {
            Name = name;
            Value = value;
            Max_Value = max_value;
        }

        public Property(string name, string description, float value, float max_value)
        {
            Name = name;
            Description = description;
            Value = value;
            Max_Value = max_value;
        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {
            
        }

        #endregion
    }
}

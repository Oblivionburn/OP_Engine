using System.Collections.Generic;

using OP_Engine.Utility;

namespace OP_Engine.Spells
{
    public class Spell : Something
    {
        #region Variables

        public List<Something> Properties = new List<Something>();

        #endregion

        #region Constructor

        public Spell()
        {
            
        }

        #endregion

        #region Methods

        public override void Dispose()
        {
            foreach (Something something in Properties)
            {
                something.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}

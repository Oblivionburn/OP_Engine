using System;
using System.Reflection;

namespace OP_Engine.Mods
{
    public class ModAssembly
    {
        #region Variables

        private Guid ID;
        public string Name;
        public string AssemblyPath;
        public Assembly Assembly;

        #endregion

        #region Constructors

        public ModAssembly(string name, string file_path)
        {
            ID = Guid.NewGuid();
            Name = name;
            AssemblyPath = file_path;
            Assembly = Assembly.LoadFrom(file_path);
        }

        #endregion

        #region Methods
        


        #endregion
    }
}

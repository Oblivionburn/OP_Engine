using System;

using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Mods
{
    public class Mod : IDisposable
    {
        #region Variables

        public Guid ID;
        public string Name;
        public string Description;
        public string Directory;
        public string Author;
        public string Version;
        public Texture2D Image;
        public ModAssembly Assembly;

        #endregion

        #region Constructors

        public Mod(string name, string directory, string description, string author, string version)
        {
            ID = Guid.NewGuid();
            Name = name;
            Directory = directory;
            Description = description;
            Author = author;
            Version = version;
        }

        public Mod(string name, string directory, string description, string author, string version, Texture2D image)
        {
            ID = Guid.NewGuid();
            Name = name;
            Directory = directory;
            Description = description;
            Author = author;
            Version = version;
            Image = image;
        }

        public Mod(string name, string directory, string description, string author, string version, ModAssembly assembly)
        {
            ID = Guid.NewGuid();
            Name = name;
            Directory = directory;
            Description = description;
            Author = author;
            Version = version;
            Assembly = assembly;
        }

        public Mod(string name, string directory, string description, string author, string version, Texture2D image, ModAssembly assembly)
        {
            ID = Guid.NewGuid();
            Name = name;
            Directory = directory;
            Description = description;
            Author = author;
            Version = version;
            Image = image;
            Assembly = assembly;
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            if (Image != null)
            {
                Image.Dispose();
            }
        }

        #endregion
    }
}

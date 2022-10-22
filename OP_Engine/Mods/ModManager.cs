using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Logging;

namespace OP_Engine.Mods
{
    public class ModManager : GameComponent
    {
        #region Variables

        public static string ModsDirectory;
        public static List<Mod> Mods = new List<Mod>();

        #endregion

        #region Constructors

        public ModManager(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public static Mod GetMod(Guid id)
        {
            foreach (Mod mod in Mods)
            {
                if (mod.ID == id)
                {
                    return mod;
                }
            }

            return null;
        }

        public static Mod GetMod(string name)
        {
            foreach (Mod mod in Mods)
            {
                if (mod.Name == name)
                {
                    return mod;
                }
            }

            return null;
        }

        public static List<Mod> GetMods_ByAuthor(string author)
        {
            List<Mod> mods = new List<Mod>();
            foreach (Mod mod in Mods)
            {
                if (mod.Author == author)
                {
                    mods.Add(mod);
                }
            }

            return mods;
        }

        public static void LoadMods(GraphicsDevice graphicsDevice)
        {
            if (!string.IsNullOrEmpty(ModsDirectory))
            {
                string[] directories = Directory.GetDirectories(ModsDirectory);
                for (int d = 0; d < directories.Length; d++)
                {
                    string dir_path = directories[d];

                    DirectoryInfo dir = new DirectoryInfo(dir_path);
                    string mod_info = Path.Combine(dir.FullName, "ModInfo.xml");

                    if (File.Exists(mod_info))
                    {
                        FileInfo[] AssemblyPaths = dir.GetFiles("*.dll");
                        FileInfo[] Images = dir.GetFiles("*.png");
                        string Directory = dir_path;
                        string Name = "";
                        string Description = "";
                        string Author = "";
                        string Version = "";

                        try
                        {
                            using (XmlTextReader reader = new XmlTextReader(File.OpenRead(mod_info)))
                            {
                                while (reader.Read())
                                {
                                    switch (reader.Name)
                                    {
                                        case "Mod":
                                            while (reader.Read())
                                            {
                                                if (reader.Name == "Mod" &&
                                                    reader.NodeType == XmlNodeType.EndElement)
                                                {
                                                    break;
                                                }

                                                switch (reader.Name)
                                                {
                                                    case "Properties":
                                                        while (reader.MoveToNextAttribute())
                                                        {
                                                            switch (reader.Name)
                                                            {
                                                                case "Name":
                                                                    Name = reader.Value;
                                                                    break;

                                                                case "Description":
                                                                    Description = reader.Value;
                                                                    break;

                                                                case "Author":
                                                                    Author = reader.Value;
                                                                    break;

                                                                case "Version":
                                                                    Version = reader.Value;
                                                                    break;
                                                            }
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }

                            Texture2D image = null;
                            if (Images.Length > 0)
                            {
                                string path = Images[0].FullName;
                                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                                {
                                    image = Texture2D.FromStream(graphicsDevice, fileStream);
                                }
                            }

                            ModAssembly assembly = null;
                            if (AssemblyPaths.Length > 0)
                            {
                                string path = AssemblyPaths[0].FullName;
                                assembly = new ModAssembly(Path.GetFileNameWithoutExtension(path), path);
                            }

                            Mod mod = new Mod(Name, Directory, Description, Author, Version, image, assembly);
                            Mods.Add(mod);
                        }
                        catch (Exception e)
                        {
                            Logger.Logs.Add(new Log(e.Source, e.Message, e.StackTrace));
                        }
                    }
                }
            }
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (Mod mod in Mods)
            {
                mod.Dispose();
            }
        }

        #endregion
    }
}

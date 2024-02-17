using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Sounds;

namespace OP_Engine.Utility
{
    public class AssetManager : GameComponent
    {
        #region Variables

        public static ContentManager Content;
        public static Dictionary<string, string> Directories = new Dictionary<string, string>();
        public static Dictionary<string, string> Files = new Dictionary<string, string>();
        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Effect> Shaders = new Dictionary<string, Effect>();
        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();
        public static Dictionary<string, Dictionary<string, Sound>> Sounds = new Dictionary<string, Dictionary<string, Sound>>();
        public static Dictionary<string, Dictionary<string, Sound>> Music = new Dictionary<string, Dictionary<string, Sound>>();
        public static Dictionary<string, Dictionary<string, Sound>> Ambient = new Dictionary<string, Dictionary<string, Sound>>();

        #endregion

        #region Constructor

        public AssetManager(Game game) : base(game)
        {
            
        }

        #endregion

        #region Methods

        public static void Init(Game game, string content_dir)
        {
            if (!Directories.ContainsKey("Content"))
            {
                Directories.Add("Content", content_dir);

                Content = new ContentManager(game.Services, content_dir)
                {
                    RootDirectory = content_dir
                };
            }

            if (!Directories.ContainsKey("Ambient"))
            {
                Directories.Add("Ambient", string.Concat(Directories["Content"], @"\Ambient"));
            }

            if (!Directories.ContainsKey("Fonts"))
            {
                Directories.Add("Fonts", string.Concat(Directories["Content"], @"\Fonts"));
            }

            if (!Directories.ContainsKey("Music"))
            {
                Directories.Add("Music", string.Concat(Directories["Content"], @"\Music"));
            }

            if (!Directories.ContainsKey("Shaders"))
            {
                Directories.Add("Shaders", string.Concat(Directories["Content"], @"\Shaders"));
            }

            if (!Directories.ContainsKey("Sounds"))
            {
                Directories.Add("Sounds", string.Concat(Directories["Content"], @"\Sounds"));
            }

            if (!Directories.ContainsKey("Textures"))
            {
                Directories.Add("Textures", string.Concat(Directories["Content"], @"\Textures"));
            }            
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (var texture in Textures)
            {
                texture.Value.Dispose();
            }

            foreach (var shader in Shaders)
            {
                shader.Value.Dispose();
            }
        }

        #region Load Stuff

        public static void LoadTextures(GraphicsDevice graphicsDevice)
        {
            DirectoryInfo dir = new DirectoryInfo(Directories["Textures"]);
            foreach (var file in dir.GetFiles("*.png"))
            {
                var name = Path.GetFileNameWithoutExtension(file.Name);
                if (!Textures.ContainsKey(name))
                {
                    using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open))
                    {
                        Texture2D texture = Texture2D.FromStream(graphicsDevice, fileStream);
                        texture.Name = name;
                        Textures.Add(name, texture);
                    }
                }
            }
        }

        public static void LoadTextures(GraphicsDevice graphicsDevice, string directory)
        {
            DirectoryInfo dir = new DirectoryInfo(Directories["Textures"]);
            foreach (var sub_dir in dir.GetDirectories())
            {
                if (sub_dir.Name == directory)
                {
                    foreach (var file in sub_dir.GetFiles("*.png"))
                    {
                        var name = Path.GetFileNameWithoutExtension(file.Name);
                        if (!Textures.ContainsKey(name))
                        {
                            using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open))
                            {
                                Texture2D texture = Texture2D.FromStream(graphicsDevice, fileStream);
                                texture.Name = name;
                                Textures.Add(name, texture);
                            }
                        }
                    }

                    break;
                }
            }
        }

        public static void LoadFonts()
        {
            DirectoryInfo dir = new DirectoryInfo(Directories["Fonts"]);
            foreach (var file in dir.GetFiles("*.xnb"))
            {
                var name = Path.GetFileNameWithoutExtension(file.Name);
                if (!Fonts.ContainsKey(name))
                {
                    SpriteFont font = Content.Load<SpriteFont>(@"Fonts\" + name);
                    Fonts.Add(name, font);
                }
            }
        }

        public static void LoadShaders(GraphicsDevice graphicsDevice)
        {
            DirectoryInfo dir = new DirectoryInfo(Directories["Shaders"]);
            foreach (var file in dir.GetFiles("*.FxDX"))
            {
                var name = Path.GetFileNameWithoutExtension(file.Name);
                if (!Shaders.ContainsKey(name))
                {
                    Effect effect = new Effect(graphicsDevice, File.ReadAllBytes(file.FullName));
                    effect.CurrentTechnique = effect.Techniques[name];
                    Shaders.Add(name, effect);
                }
            }
        }

        public static void LoadSounds()
        {
            DirectoryInfo dir = new DirectoryInfo(Directories["Sounds"]);
            foreach (var file in dir.GetFiles())
            {
                Sound sound = new Sound();
                sound.Extension = file.Extension;
                sound.Name = Path.GetFileNameWithoutExtension(file.FullName);
                sound.Directory = Path.GetDirectoryName(file.FullName);
                sound.Type = sound.Name;

                if (!Sounds.ContainsKey("Sounds"))
                {
                    Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();
                    sounds.Add(sound.Name, sound);

                    Sounds.Add("Sounds", sounds);
                }
                else if (!Sounds["Sounds"].ContainsKey(sound.Name))
                {
                    Sounds["Sounds"].Add(sound.Name, sound);
                }
            }
        }

        public static void LoadSounds(string directory)
        {
            DirectoryInfo dir = new DirectoryInfo(Directories["Sounds"]);
            foreach (var sub_dir in dir.GetDirectories())
            {
                if (sub_dir.Name == directory)
                {
                    foreach (var file in sub_dir.GetFiles())
                    {
                        Sound sound = new Sound();
                        sound.Extension = file.Extension;
                        sound.Name = Path.GetFileNameWithoutExtension(file.FullName);
                        sound.Directory = Path.GetDirectoryName(file.FullName);

                        //Assumes your sound files end with numbers for variations of each sound
                        //because nobody likes hearing the same sound repeatedly for hours
                        //Example: Hit1.mp3, Hit2.mp3, Hit3.mp3,etc
                        sound.Type = sound.Name.Substring(0, sound.Name.Length - 1);

                        if (!Sounds.ContainsKey(directory))
                        {
                            Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();
                            sounds.Add(sound.Name, sound);

                            Sounds.Add(directory, sounds);
                        }
                        else if (!Sounds[directory].ContainsKey(sound.Name))
                        {
                            Sounds[directory].Add(sound.Name, sound);
                        }
                    }

                    break;
                }
            }  
        }

        public static void LoadMusic()
        {
            DirectoryInfo dir = new DirectoryInfo(Directories["Music"]);
            foreach (var file in dir.GetFiles())
            {
                Sound sound = new Sound();
                sound.Extension = file.Extension;
                sound.Name = Path.GetFileNameWithoutExtension(file.FullName);
                sound.Directory = Path.GetDirectoryName(file.FullName);
                sound.Type = sound.Name;

                if (!Music.ContainsKey("Music"))
                {
                    Dictionary<string, Sound> music = new Dictionary<string, Sound>();
                    music.Add(sound.Name, sound);

                    Music.Add("Music", music);
                }
                else if (!Music["Music"].ContainsKey(sound.Name))
                {
                    Music["Music"].Add(sound.Name, sound);
                }
            }
        }

        public static void LoadMusic(string directory)
        {
            DirectoryInfo dir = new DirectoryInfo(Directories["Music"]);
            foreach (var dir_music in dir.GetDirectories())
            {
                if (dir_music.Name == directory)
                {
                    foreach (var file in dir_music.GetFiles())
                    {
                        Sound sound = new Sound();
                        sound.Extension = file.Extension;
                        sound.Name = Path.GetFileNameWithoutExtension(file.FullName);
                        sound.Directory = Path.GetDirectoryName(file.FullName);
                        sound.Type = sound.Name;

                        if (!Music.ContainsKey(directory))
                        {
                            Dictionary<string, Sound> music = new Dictionary<string, Sound>();
                            music.Add(sound.Name, sound);

                            Music.Add(directory, music);
                        }
                        else if (!Music[directory].ContainsKey(sound.Name))
                        {
                            Music[directory].Add(sound.Name, sound);
                        }
                    }
                }
            }
        }

        public static void LoadAmbient()
        {
            DirectoryInfo dir = new DirectoryInfo(Directories["Ambient"]);
            foreach (var file in dir.GetFiles())
            {
                Sound sound = new Sound();
                sound.Extension = file.Extension;
                sound.Name = Path.GetFileNameWithoutExtension(file.FullName);
                sound.Directory = Path.GetDirectoryName(file.FullName);
                sound.Type = sound.Name;

                if (!Ambient.ContainsKey("Ambient"))
                {
                    Dictionary<string, Sound> ambient = new Dictionary<string, Sound>();
                    ambient.Add(sound.Name, sound);

                    Ambient.Add("Ambient", ambient);
                }
                else if (!Ambient["Ambient"].ContainsKey(sound.Name))
                {
                    Ambient["Ambient"].Add(sound.Name, sound);
                }
            }
        }

        public static void LoadAmbient(string directory)
        {
            DirectoryInfo d = new DirectoryInfo(Directories["Ambient"]);
            foreach (var dir in d.GetDirectories())
            {
                if (dir.Name == directory)
                {
                    foreach (var file in dir.GetFiles())
                    {
                        Sound sound = new Sound();
                        sound.Extension = file.Extension;
                        sound.Name = Path.GetFileNameWithoutExtension(file.FullName);
                        sound.Directory = Path.GetDirectoryName(file.FullName);
                        sound.Type = sound.Name;

                        if (!Ambient.ContainsKey(directory))
                        {
                            Dictionary<string, Sound> ambient = new Dictionary<string, Sound>();
                            ambient.Add(sound.Name, sound);

                            Ambient.Add(directory, ambient);
                        }
                        else if (!Ambient[directory].ContainsKey(sound.Name))
                        {
                            Ambient[directory].Add(sound.Name, sound);
                        }
                    }

                    break;
                }
            }
        }

        #endregion

        #region Play Stuff

        public static void PlaySound(string name)
        {
            SoundManager.PlaySound(Sounds["Sounds"][name]);
        }

        public static void PlaySound(string type, string name)
        {
            SoundManager.PlaySound(Sounds[type][name]);
        }

        public static void PlaySound_Random(string type)
        {
            CryptoRandom rand = new CryptoRandom();
            int choice = rand.Next(0, Sounds[type].Count);

            Sound sound = Sounds[type].ToList().ElementAt(choice).Value;
            SoundManager.PlaySound(sound);
        }

        public static void PlaySound_AtDistance(string name, Vector2 location, Vector2 source, int max_distance)
        {
            if (Sounds.ContainsKey(name))
            {
                SoundManager.PlaySound_Distance(Sounds["Sounds"][name], location, source, max_distance);
            }
        }

        public static void PlaySound_AtDistance(string type, string name, Vector2 location, Vector2 source, int max_distance)
        {
            if (Sounds.ContainsKey(name))
            {
                SoundManager.PlaySound_Distance(Sounds[type][name], location, source, max_distance);
            }
        }

        public static void PlaySound_Random_AtDistance(string type, Vector2 location, Vector2 source, int max_distance)
        {
            CryptoRandom rand = new CryptoRandom();
            int choice = rand.Next(0, Sounds[type].Count);

            Sound sound = Sounds[type].ToList().ElementAt(choice).Value;
            SoundManager.PlaySound_Distance(sound, location, source, max_distance);
        }

        public static void PlayMusic(string name, bool looping)
        {
            SoundManager.PlayMusic(Music["Music"][name], looping);
            SoundManager.NeedMusic = false;
        }

        public static void PlayMusic(string type, string name, bool looping)
        {
            SoundManager.PlayMusic(Music[type][name], looping);
            SoundManager.NeedMusic = false;
        }

        public static void PlayMusic_Random(string type, bool looping)
        {
            CryptoRandom rand = new CryptoRandom();
            int choice = rand.Next(0, Music[type].Count);

            SoundManager.PlayMusic(Music[type].ToList().ElementAt(choice).Value, looping);
            SoundManager.NeedMusic = false;
        }

        public static void PlayAmbient(string name, bool looping)
        {
            SoundManager.PlayMusic(Ambient["Ambient"][name], looping);
        }

        public static void PlayAmbient(string type, string name, bool looping)
        {
            SoundManager.PlayMusic(Ambient[type][name], looping);
        }

        public static void PlayAmbient_Random(string type, bool looping)
        {
            CryptoRandom rand = new CryptoRandom();
            int choice = rand.Next(0, Ambient[type].Count);

            SoundManager.PlayMusic(Ambient[type].ToList().ElementAt(choice).Value, looping);
        }

        #endregion

        #region Get Stuff

        public static Texture2D GetTexture(string name)
        {
            return Textures[name];
        }

        public static Texture2D GetTextureCopy(GraphicsDevice graphicsDevice, string name)
        {
            Texture2D texture = Textures[name];
            Color[] colors = new Color[texture.Width * texture.Height];
            texture.GetData(colors);

            Texture2D newTexture = new Texture2D(graphicsDevice, texture.Width, texture.Height);
            newTexture.Name = texture.Name;
            newTexture.SetData(colors);

            return newTexture;
        }

        public static Effect GetShader(string name)
        {
            return Shaders[name];
        }

        public static SpriteFont GetFont(string name)
        {
            return Fonts[name];
        }

        public static Sound GetSound(string name)
        {
            return Sounds["Sounds"][name];
        }

        public static Sound GetSound(string type, string name)
        {
            return Sounds[type][name];
        }

        public static Sound GetMusic(string name)
        {
            return Music["Music"][name];
        }

        public static Sound GetMusic(string type, string name)
        {
            return Music[type][name];
        }

        public static Sound GetAmbient(string name)
        {
            return Ambient["Ambient"][name];
        }

        public static Sound GetAmbient(string type, string name)
        {
            return Ambient[type][name];
        }

        #endregion

        #endregion
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

using OP_Engine.Sounds;

namespace OP_Engine.Utility
{
    public class AssetManager(Game game) : GameComponent(game)
    {
        #region Variables

        public static ContentManager? Content;
        public static string[] SoundExtensions = [".mp3", ".wma", ".wav", ".ogg"];
        public static Dictionary<string, string> Directories = [];
        public static Dictionary<string, string> Files = [];
        public static Dictionary<string, Texture2D> Textures = [];
        public static Dictionary<string, Effect> Shaders = [];
        public static Dictionary<string, SpriteFont> Fonts = [];
        public static Dictionary<string, Dictionary<string, Sound>> Sounds = [];
        public static Dictionary<string, Dictionary<string, Sound>> Music = [];
        public static Dictionary<string, Dictionary<string, Sound>> Ambient = [];

        #endregion

        #region Methods

        public static void Init(Game game, string content_dir)
        {
            if (Directories.TryAdd("Content", content_dir))
            {
                Content = new ContentManager(game.Services, content_dir)
                {
                    RootDirectory = content_dir
                };
            }

            Directories.TryAdd("Ambient", string.Concat(Directories["Content"], @"\Ambient"));
            Directories.TryAdd("Fonts", string.Concat(Directories["Content"], @"\Fonts"));
            Directories.TryAdd("Music", string.Concat(Directories["Content"], @"\Music"));
            Directories.TryAdd("Shaders", string.Concat(Directories["Content"], @"\Shaders"));
            Directories.TryAdd("Sounds", string.Concat(Directories["Content"], @"\Sounds"));
            Directories.TryAdd("Textures", string.Concat(Directories["Content"], @"\Textures"));
        }

        private void Game_Exiting(object? sender, EventArgs e)
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
            DirectoryInfo dir = new(Directories["Textures"]);
            foreach (var file in dir.GetFiles("*.png"))
            {
                var name = Path.GetFileNameWithoutExtension(file.Name);
                if (!Textures.ContainsKey(name))
                {
                    using (FileStream fileStream = new(file.FullName, FileMode.Open))
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
            DirectoryInfo dir = new(Directories["Textures"]);
            foreach (var sub_dir in dir.GetDirectories())
            {
                if (sub_dir.Name == directory)
                {
                    foreach (var file in sub_dir.GetFiles("*.png"))
                    {
                        var name = Path.GetFileNameWithoutExtension(file.Name);
                        if (!Textures.ContainsKey(name))
                        {
                            using (FileStream fileStream = new(file.FullName, FileMode.Open))
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

        public static Texture2D? LoadTexture(GraphicsDevice graphicsDevice, DirectoryInfo directory, string fileName)
        {
            Texture2D? texture = null;

            FileInfo[] files = directory.GetFiles("*.png");

            int count = files.Length;
            for (int i = 0; i < count; i++)
            {
                FileInfo file = files[i];

                string name = Path.GetFileNameWithoutExtension(file.Name);
                if (fileName == name)
                {
                    try
                    {
                        using (FileStream fileStream = new(file.FullName, FileMode.Open))
                        {
                            texture = Texture2D.FromStream(graphicsDevice, fileStream);
                        }
                    }
                    catch
                    {
                        //Ignore potential errors for lazy-loading
                    }

                    if (texture != null)
                    {
                        texture.Name = name;

                        Textures.TryAdd(name, texture);
                        return texture;
                    }
                }
            }

            if (texture == null)
            {
                //Search sub-dirs recursively

                DirectoryInfo[] sub_directories = directory.GetDirectories();

                int dir_count = sub_directories.Length;
                for (int i = 0; i < dir_count; i++)
                {
                    DirectoryInfo dir = sub_directories[i];
                    texture = LoadTexture(graphicsDevice, dir, fileName);
                    if (texture != null)
                    {
                        return texture;
                    }
                }
            }

            return texture;
        }

        public static void LoadFonts()
        {
            DirectoryInfo dir = new(Directories["Fonts"]);
            foreach (var file in dir.GetFiles("*.xnb"))
            {
                var name = Path.GetFileNameWithoutExtension(file.Name);

                if (Content != null)
                {
                    Fonts.TryAdd(name, Content.Load<SpriteFont>(@"Fonts\" + name));
                }
            }
        }

        public static void LoadShaders(GraphicsDevice graphicsDevice)
        {
            DirectoryInfo dir = new(Directories["Shaders"]);
            foreach (var file in dir.GetFiles("*.FxDX"))
            {
                var name = Path.GetFileNameWithoutExtension(file.Name);

                Effect effect = new(graphicsDevice, File.ReadAllBytes(file.FullName));
                effect.CurrentTechnique = effect.Techniques[name];

                Shaders.TryAdd(name, effect);
            }
        }

        public static void LoadSounds()
        {
            DirectoryInfo dir = new(Directories["Sounds"]);
            foreach (var file in dir.GetFiles())
            {
                if (SoundExtensions.Contains(file.Extension))
                {
                    string name = Path.GetFileNameWithoutExtension(file.Name);

                    Sound sound = new()
                    {
                        Extension = file.Extension,
                        Name = name,
                        Type = name,
                        Directory = Path.GetDirectoryName(file.FullName)
                    };

                    if (!Sounds.TryGetValue("Sounds", out Dictionary<string, Sound>? value))
                    {
                        Sounds.Add("Sounds", new()
                        {
                            { sound.Name, sound }
                        });
                    }
                    else
                    {
                        value.TryAdd(sound.Name, sound);
                    }
                }
            }
        }

        public static void LoadSounds(string directory)
        {
            DirectoryInfo dir = new(Directories["Sounds"]);
            foreach (var sub_dir in dir.GetDirectories())
            {
                if (sub_dir.Name == directory)
                {
                    foreach (var file in sub_dir.GetFiles())
                    {
                        if (SoundExtensions.Contains(file.Extension))
                        {
                            string name = Path.GetFileNameWithoutExtension(file.FullName);

                            Sound sound = new()
                            {
                                Extension = file.Extension,
                                Name = name,

                                //Assumes your sound files end with numbers for variations of each sound
                                //because nobody likes hearing the same sound repeatedly for hours
                                //Example: Hit1.mp3, Hit2.mp3, Hit3.mp3,etc
                                Type = name.Substring(0, name.Length - 1),

                                Directory = Path.GetDirectoryName(file.FullName)
                            };

                            if (!Sounds.TryGetValue(directory, out Dictionary<string, Sound>? value))
                            {
                                Sounds.Add(directory, new()
                                {
                                    { sound.Name, sound }
                                });
                            }
                            else
                            {
                                value.TryAdd(sound.Name, sound);
                            }
                        }
                    }

                    break;
                }
            }
        }

        public static void LoadMusic()
        {
            DirectoryInfo dir = new(Directories["Music"]);
            foreach (var file in dir.GetFiles())
            {
                if (SoundExtensions.Contains(file.Extension))
                {
                    string name = Path.GetFileNameWithoutExtension(file.FullName);

                    Sound sound = new()
                    {
                        Extension = file.Extension,
                        Name = name,
                        Type = name,
                        Directory = Path.GetDirectoryName(file.FullName)
                    };

                    if (!Music.TryGetValue("Music", out Dictionary<string, Sound>? value))
                    {
                        Music.Add("Music", new()
                        {
                            { sound.Name, sound }
                        });
                    }
                    else
                    {
                        value.TryAdd(sound.Name, sound);
                    }
                }
            }
        }

        public static void LoadMusic(string directory)
        {
            DirectoryInfo dir = new(Directories["Music"]);
            foreach (var dir_music in dir.GetDirectories())
            {
                if (dir_music.Name == directory)
                {
                    foreach (var file in dir_music.GetFiles())
                    {
                        if (SoundExtensions.Contains(file.Extension))
                        {
                            string name = Path.GetFileNameWithoutExtension(file.FullName);

                            Sound sound = new()
                            {
                                Extension = file.Extension,
                                Name = name,
                                Type = name,
                                Directory = Path.GetDirectoryName(file.FullName)
                            };

                            if (!Music.TryGetValue(directory, out Dictionary<string, Sound>? value))
                            {
                                Music.Add(directory, new()
                                {
                                    { sound.Name, sound }
                                });
                            }
                            else
                            {
                                value.TryAdd(sound.Name, sound);
                            }
                        }
                    }
                }
            }
        }

        public static void LoadAmbient()
        {
            DirectoryInfo dir = new(Directories["Ambient"]);
            foreach (var file in dir.GetFiles())
            {
                if (SoundExtensions.Contains(file.Extension))
                {
                    string name = Path.GetFileNameWithoutExtension(file.FullName);

                    Sound sound = new()
                    {
                        Extension = file.Extension,
                        Name = name,
                        Type = name,
                        Directory = Path.GetDirectoryName(file.FullName)
                    };

                    if (!Ambient.TryGetValue("Ambient", out Dictionary<string, Sound>? value))
                    {
                        Ambient.Add("Ambient", new()
                        {
                            { sound.Name, sound }
                        });
                    }
                    else
                    {
                        value.TryAdd(sound.Name, sound);
                    }
                }
            }
        }

        public static void LoadAmbient(string directory)
        {
            DirectoryInfo dir = new(Directories["Ambient"]);
            foreach (var dir_ambient in dir.GetDirectories())
            {
                if (dir_ambient.Name == directory)
                {
                    foreach (var file in dir_ambient.GetFiles())
                    {
                        if (SoundExtensions.Contains(file.Extension))
                        {
                            string name = Path.GetFileNameWithoutExtension(file.FullName);

                            Sound sound = new()
                            {
                                Extension = file.Extension,
                                Name = name,
                                Type = name,
                                Directory = Path.GetDirectoryName(file.FullName)
                            };

                            if (!Ambient.TryGetValue(directory, out Dictionary<string, Sound>? value))
                            {
                                Ambient.Add(directory, new()
                                {
                                    { sound.Name, sound }
                                });
                            }
                            else
                            {
                                value.TryAdd(sound.Name, sound);
                            }
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
            if (Sounds.TryGetValue("Sounds", out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                SoundManager.PlaySound(sound);
            }
        }

        public static void PlaySound(string type, string name)
        {
            if (Sounds.TryGetValue(type, out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                SoundManager.PlaySound(sound);
            }
        }

        public static void PlaySound_Random(string type)
        {
            if (Sounds.TryGetValue(type, out Dictionary<string, Sound>? list))
            {
                CryptoRandom rand = new();
                int choice = rand.Next(0, list.Count);

                Sound sound = list.ToList().ElementAt(choice).Value;
                SoundManager.PlaySound(sound);
            }
        }

        public static void PlaySound_AtDistance(string name, Vector2 location, Vector2 source, int max_distance)
        {
            if (Sounds.TryGetValue("Sounds", out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                SoundManager.PlaySound_Distance(sound, location, source, max_distance);
            }
        }

        public static void PlaySound_AtDistance(string type, string name, Vector2 location, Vector2 source, int max_distance)
        {
            if (Sounds.TryGetValue(type, out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                SoundManager.PlaySound_Distance(sound, location, source, max_distance);
            }
        }

        public static void PlaySound_Random_AtDistance(string type, Vector2 location, Vector2 source, int max_distance)
        {
            if (Sounds.TryGetValue(type, out Dictionary<string, Sound>? list))
            {
                CryptoRandom rand = new();
                int choice = rand.Next(0, list.Count);

                Sound sound = list.ToList().ElementAt(choice).Value;
                SoundManager.PlaySound_Distance(sound, location, source, max_distance);
            }
        }

        public static void PlayMusic(string name, bool looping)
        {
            if (Music.TryGetValue("Music", out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                SoundManager.PlayMusic(sound, looping);
                SoundManager.NeedMusic = false;
            }
        }

        public static void PlayMusic(string type, string name, bool looping)
        {
            if (Music.TryGetValue(type, out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                SoundManager.PlayMusic(sound, looping);
                SoundManager.NeedMusic = false;
            }
        }

        public static void PlayMusic_Random(string type, bool looping)
        {
            if (Music.TryGetValue(type, out Dictionary<string, Sound>? list))
            {
                CryptoRandom rand = new();
                int choice = rand.Next(0, list.Count);

                Sound sound = list.ToList().ElementAt(choice).Value;

                SoundManager.PlayMusic(sound, looping);
                SoundManager.NeedMusic = false;
            }
        }

        public static void PlayAmbient(string name, bool looping)
        {
            if (Ambient.TryGetValue("Ambient", out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                SoundManager.PlayAmbient(sound, looping);
            }
        }

        public static void PlayAmbient(string type, string name, bool looping)
        {
            if (Ambient.TryGetValue(type, out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                SoundManager.PlayAmbient(sound, looping);
            }
        }

        public static void PlayAmbient_Random(string type, bool looping)
        {
            if (Ambient.TryGetValue(type, out Dictionary<string, Sound>? list))
            {
                CryptoRandom rand = new();
                int choice = rand.Next(0, list.Count);

                Sound sound = list.ToList().ElementAt(choice).Value;

                SoundManager.PlayAmbient(sound, looping);
            }
        }

        #endregion

        #region Get Stuff

        public static Texture2D? GetTexture(string name)
        {
            return Textures.TryGetValue(name, out Texture2D? value) ? value : null;
        }

        public static Texture2D? GetTexture_LazyLoad(GraphicsDevice graphicsDevice, string name)
        {
            if (Textures.TryGetValue(name, out Texture2D? value))
            {
                return value;
            }
            else
            {
                return LoadTexture(graphicsDevice, new DirectoryInfo(Directories["Textures"]), name);
            }
        }

        public static Texture2D? GetTextureCopy(GraphicsDevice graphicsDevice, string name)
        {
            if (Textures.TryGetValue(name, out Texture2D? texture))
            {
                if (texture != null)
                {
                    Color[] colors = new Color[texture.Width * texture.Height];
                    texture.GetData(colors);

                    Texture2D newTexture = new(graphicsDevice, texture.Width, texture.Height)
                    {
                        Name = texture.Name
                    };
                    newTexture.SetData(colors);

                    return newTexture;
                }
            }

            return null;
        }

        public static Effect? GetShader(string name)
        {
            return Shaders.TryGetValue(name, out Effect? value) ? value : null;
        }

        public static SpriteFont? GetFont(string name)
        {
            return Fonts.TryGetValue(name, out SpriteFont? value) ? value : null;
        }

        public static Sound? GetSound(string name)
        {
            if (Sounds.TryGetValue("Sounds", out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                return sound;
            }

            return null;
        }

        public static Sound? GetSound(string type, string name)
        {
            if (Sounds.TryGetValue(type, out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                return sound;
            }

            return null;
        }

        public static Sound? GetMusic(string name)
        {
            if (Music.TryGetValue("Music", out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                return sound;
            }

            return null;
        }

        public static Sound? GetMusic(string type, string name)
        {
            if (Music.TryGetValue(type, out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                return sound;
            }

            return null;
        }

        public static Sound? GetAmbient(string name)
        {
            if (Ambient.TryGetValue("Ambient", out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                return sound;
            }

            return null;
        }

        public static Sound? GetAmbient(string type, string name)
        {
            if (Ambient.TryGetValue(type, out Dictionary<string, Sound>? list) &&
                list.TryGetValue(name, out Sound? sound))
            {
                return sound;
            }

            return null;
        }

        #endregion

        #endregion
    }
}

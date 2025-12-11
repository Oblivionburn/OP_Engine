using FMOD;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OP_Engine.Sounds
{
    public class SoundManager : GameComponent
    {
        #region Variables

        public static FMOD.System FMODSystem;

        public static bool MusicEnabled;
        public static FMOD.Sound MusicOut;
        public static FMOD.Channel MusicChannel;
        public static FMOD.ChannelGroup MusicGroup;
        public static float MusicVolume = 1;
        public static bool MusicLooping;
        public static bool MusicPaused;
        public static bool MusicPlaying;
        public static string MusicPlaying_Name;

        public static bool AmbientEnabled;
        public static List<FMOD.Sound> AmbientOuts = new List<FMOD.Sound>();
        public static List<FMOD.Channel> AmbientChannels = new List<FMOD.Channel>();
        public static FMOD.ChannelGroup AmbientGroup;
        public static float AmbientVolume = 1;
        public static Dictionary<string, float> AmbientFade = new Dictionary<string, float>();
        public static string[] AmbientTypes = { "Rain", "Storm", "Snow", "Fog" };

        public static bool SoundEnabled;
        public static List<FMOD.Sound> SoundOuts = new List<FMOD.Sound>();
        public static List<FMOD.Channel> SoundChannels = new List<FMOD.Channel>();
        public static FMOD.ChannelGroup SoundGroup;
        public static float SoundVolume = 1;
        public static bool SoundPaused;

        public static bool GameStarted;
        public static bool NeedMusic;
        public static bool Paused;

        #endregion

        #region Constructor

        public SoundManager(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;

            FMOD.Factory.System_Create(out FMODSystem);
            FMODSystem.setDSPBufferSize(512, 2);
            FMODSystem.init(16, FMOD.INITFLAGS.NORMAL, (IntPtr)0);

            SoundEnabled = true;
            AmbientEnabled = true;
            MusicEnabled = true;
        }

        #endregion

        #region Methods

        public static void Update()
        {
            UpdateMusic();
            UpdateAmbient();
            UpdateSound();

            FMODSystem.update();
        }

        public static void UpdateMusic()
        {
            if (MusicEnabled)
            {
                FMOD.RESULT set_music_volume = MusicChannel.setVolume(MusicVolume);

                bool isPaused = false;
                FMOD.RESULT get_paused = MusicChannel.getPaused(out isPaused);

                uint music_length;
                FMOD.RESULT get_music_length = MusicOut.getLength(out music_length, FMOD.TIMEUNIT.MS);

                uint music_position;
                FMOD.RESULT get_music_position = MusicChannel.getPosition(out music_position, FMOD.TIMEUNIT.MS);

                if (!MusicLooping &&
                    music_length > 0 &&
                    music_position > 0)
                {
                    if (music_position >= music_length)
                    {
                        StopMusic();
                        NeedMusic = true;
                    }
                    else
                    {
                        MusicPlaying = true;
                    }
                }

                if (!isPaused &&
                    (Paused || MusicPaused))
                {
                    FMOD.RESULT set_paused = MusicChannel.setPaused(true);
                }
                else if (isPaused &&
                         !Paused &&
                         !MusicPaused)
                {
                    FMOD.RESULT set_paused = MusicChannel.setPaused(false);
                }
            }
        }

        public static void UpdateAmbient()
        {
            if (AmbientEnabled)
            {
                if (AmbientChannels.Count > 0)
                {
                    for (int c = 0; c < AmbientChannels.Count; c++)
                    {
                        FMOD.Channel channel = AmbientChannels[c];
                        FMOD.Sound sound = AmbientOuts[c];

                        string name = "";
                        for (int n = 2; n < 7; n++)
                        {
                            sound.getName(out name, n);
                            if (AmbientTypes.Contains(name))
                            {
                                break;
                            }
                        }

                        float volume = AmbientVolume;

                        if (AmbientFade.ContainsKey(name))
                        {
                            volume = (((AmbientVolume * 100) * (100 - (AmbientFade[name] * 100))) / 100) / 100;
                        }
                        
                        if (volume < 0)
                        {
                            volume = 0;
                        }
                        FMOD.RESULT set_ambient_volume = channel.setVolume(volume);

                        bool isPaused = false;
                        FMOD.RESULT get_paused = channel.getPaused(out isPaused);

                        bool isPlaying = false;
                        FMOD.RESULT get_playing = channel.isPlaying(out isPlaying);

                        FMOD.MODE mode = FMOD.MODE.DEFAULT;
                        FMOD.RESULT get_mode = sound.getMode(out mode);

                        uint ambient_length;
                        FMOD.RESULT get_ambient_length = sound.getLength(out ambient_length, FMOD.TIMEUNIT.MS);

                        uint ambient_position;
                        FMOD.RESULT get_ambient_position = channel.getPosition(out ambient_position, FMOD.TIMEUNIT.MS);

                        if (!isPaused &&
                            Paused)
                        {
                            FMOD.RESULT set_paused = channel.setPaused(true);
                        }
                        else if (isPaused &&
                                 !Paused)
                        {
                            FMOD.RESULT set_paused = channel.setPaused(false);
                        }
                    }
                }
            }
            else
            {
                if (AmbientChannels.Count > 0)
                {
                    int count = AmbientChannels.Count;
                    for (int c = 0; c < count; c++)
                    {
                        FMOD.Channel channel = AmbientChannels[c];
                        FMOD.RESULT set_sound_volume = channel.setVolume(0);
                    }
                }
            }
        }

        public static void UpdateSound()
        {
            if (SoundEnabled)
            {
                if (SoundChannels.Count > 0)
                {
                    for (int c = 0; c < SoundChannels.Count; c++)
                    {
                        FMOD.Channel channel = SoundChannels[c];
                        FMOD.Sound sound = SoundOuts[c];

                        FMOD.RESULT set_sound_volume = channel.setVolume(SoundVolume);

                        bool isPaused = false;
                        FMOD.RESULT get_paused = channel.getPaused(out isPaused);

                        bool isPlaying = false;
                        FMOD.RESULT get_playing = channel.isPlaying(out isPlaying);

                        uint sound_length;
                        FMOD.RESULT get_sound_length = sound.getLength(out sound_length, FMOD.TIMEUNIT.MS);

                        uint sound_position;
                        FMOD.RESULT get_sound_position = channel.getPosition(out sound_position, FMOD.TIMEUNIT.MS);

                        if (sound_length > 0 &&
                            sound_position > 0)
                        {
                            if (sound_position >= sound_length &&
                                !isPlaying)
                            {
                                channel.stop();
                                SoundChannels.Remove(channel);
                                SoundOuts.Remove(sound);

                                c--;
                            }
                            else if (!isPaused &&
                                    (Paused || SoundPaused))
                            {
                                FMOD.RESULT set_paused = channel.setPaused(true);
                            }
                            else if (isPaused &&
                                     !Paused &&
                                     !SoundPaused)
                            {
                                FMOD.RESULT set_paused = channel.setPaused(false);
                            }
                        }
                    }
                }
            }
            else
            {
                if (SoundChannels.Count > 0)
                {
                    int count = SoundChannels.Count;
                    for (int c = 0; c < count; c++)
                    {
                        FMOD.Channel channel = SoundChannels[c];
                        FMOD.RESULT set_sound_volume = channel.setVolume(0);
                    }
                }
            }
        }

        public static void PlayMusic(Sound sound, bool looping)
        {
            if (MusicEnabled)
            {
                string file = sound.Directory + @"\" + sound.Name + sound.Extension;

                bool isPlaying = false;
                FMOD.RESULT get_music_playing = MusicChannel.isPlaying(out isPlaying);

                uint music_length;
                FMOD.RESULT get_music_length = MusicOut.getLength(out music_length, FMOD.TIMEUNIT.MS);

                uint music_position;
                FMOD.RESULT get_music_position = MusicChannel.getPosition(out music_position, FMOD.TIMEUNIT.MS);

                if (isPlaying &&
                    music_length > 0 &&
                    music_position > 0)
                {
                    StopMusic();
                }

                MusicLooping = looping;
                if (MusicLooping)
                {
                    FMODSystem.createStream(file, FMOD.MODE.LOOP_NORMAL, out MusicOut);
                }
                else
                {
                    FMODSystem.createStream(file, FMOD.MODE.DEFAULT, out MusicOut);
                }
                
                FMODSystem.playSound(MusicOut, MusicGroup, false, out MusicChannel);
                MusicChannel.setVolume(MusicVolume);

                MusicPlaying = true;
                MusicPlaying_Name = sound.Name;
            }
        }

        public static void PlayAmbient(Sound sound, bool looping)
        {
            if (AmbientEnabled)
            {
                string file = sound.Directory + @"\" + sound.Name + sound.Extension;

                FMOD.Channel channel = new FMOD.Channel();

                if (!AmbientFade.ContainsKey(sound.Name))
                {
                    AmbientFade.Add(sound.Name, 1f);
                }
                else
                {
                    AmbientFade[sound.Name] = 1f;
                }

                float volume = AmbientVolume - AmbientFade[sound.Name];
                if (volume < 0)
                {
                    volume = 0;
                }
                
                if (looping)
                {
                    FMOD.RESULT stream_result = FMODSystem.createStream(file, FMOD.MODE.LOOP_NORMAL, out sound.SoundOut);
                }
                else
                {
                    FMOD.RESULT stream_result = FMODSystem.createStream(file, FMOD.MODE.DEFAULT, out sound.SoundOut);
                }

                FMOD.RESULT play_result = FMODSystem.playSound(sound.SoundOut, AmbientGroup, false, out channel);
                FMOD.RESULT volume_result = channel.setVolume(volume);

                AmbientOuts?.Add(sound.SoundOut);
                AmbientChannels?.Add(channel);

                FMOD.RESULT updated = FMODSystem.update();
            }
        }

        public static void PlaySound(Sound sound)
        {
            if (SoundEnabled)
            {
                string file = sound.Directory + @"\" + sound.Name + sound.Extension;

                FMOD.Channel channel = new FMOD.Channel();
                FMOD.RESULT stream_result = FMODSystem.createStream(file, FMOD.MODE.DEFAULT, out sound.SoundOut);
                FMOD.RESULT play_result = FMODSystem.playSound(sound.SoundOut, SoundGroup, false, out channel);
                FMOD.RESULT volume_result = channel.setVolume(SoundVolume);
                
                SoundOuts?.Add(sound.SoundOut);
                SoundChannels?.Add(channel);

                FMOD.RESULT updated = FMODSystem.update();
            }
        }

        public static void PlaySound_Distance(Sound sound, Vector2 location, Vector2 source, int max_distance)
        {
            if (SoundEnabled)
            {
                string fileName = sound.Directory + @"\" + sound.Name + sound.Extension;

                float x_diff = location.X - source.X;
                if (x_diff < 0)
                {
                    x_diff *= -1;
                }

                float y_diff = location.Y - source.Y;
                if (y_diff < 0)
                {
                    y_diff *= -1;
                }

                float distance = x_diff + y_diff;
                float volume_percent = ((max_distance - distance) * 100) / max_distance;
                if (volume_percent < 0)
                {
                    volume_percent = 0;
                }

                if (volume_percent > 0)
                {
                    float volume = (SoundVolume * volume_percent) / 100;

                    if (volume < 0)
                    {
                        volume = 0;
                    }
                    else if (volume > 1)
                    {
                        volume = 1;
                    }

                    if (volume > 0)
                    {
                        FMOD.Channel channel = new FMOD.Channel();
                        FMOD.RESULT stream_result = FMODSystem.createStream(fileName, FMOD.MODE.DEFAULT, out sound.SoundOut);
                        FMOD.RESULT play_result = FMODSystem.playSound(sound.SoundOut, SoundGroup, false, out channel);
                        FMOD.RESULT volume_result = channel.setVolume(volume);

                        SoundOuts.Add(sound.SoundOut);
                        SoundChannels.Add(channel);
                    }
                }
            }
        }

        public static void StopMusic()
        {
            if (MusicEnabled)
            {
                MusicChannel.stop();
                MusicPaused = false;
                MusicPlaying = false;
                MusicPlaying_Name = null;
            }
        }

        public static void StopAmbient()
        {
            if (AmbientEnabled)
            {
                if (AmbientChannels.Count > 0)
                {
                    for (int c = 0; c < AmbientChannels.Count; c++)
                    {
                        FMOD.Channel channel = AmbientChannels[c];
                        FMOD.Sound ambient = AmbientOuts[c];

                        string name = "";
                        for (int n = 2; n < 7; n++)
                        {
                            ambient.getName(out name, n);
                            if (AmbientTypes.Contains(name))
                            {
                                break;
                            }
                        }

                        channel.stop();
                        AmbientChannels.Remove(channel);
                        AmbientOuts.Remove(ambient);
                        AmbientFade.Remove(name);

                        FMOD.RESULT updated = FMODSystem.update();

                        c--;
                    }
                }
            }
        }

        public static void StopAmbient(string name)
        {
            int name_length = name.Length + 1;

            if (AmbientEnabled)
            {
                if (AmbientChannels != null &&
                    AmbientChannels.Count > 0)
                {
                    for (int c = 0; c < AmbientChannels.Count; c++)
                    {
                        FMOD.Channel channel = AmbientChannels[c];
                        FMOD.Sound ambient = AmbientOuts[c];

                        string name_output;
                        ambient.getName(out name_output, name_length);

                        if (!string.IsNullOrEmpty(name_output) &&
                            name_output == name)
                        {
                            channel.stop();
                            AmbientChannels.Remove(channel);
                            AmbientOuts.Remove(ambient);
                            AmbientFade.Remove(name);

                            FMODSystem.update();

                            break;
                        }
                    }
                }
            }
        }

        public static void StopSound()
        {
            if (SoundChannels != null &&
                SoundChannels.Count > 0)
            {
                for (int c = 0; c < SoundChannels.Count; c++)
                {
                    FMOD.Channel channel = SoundChannels[c];
                    FMOD.Sound sound = SoundOuts[c];

                    channel.stop();
                    SoundChannels.Remove(channel);
                    SoundOuts.Remove(sound);

                    FMOD.RESULT updated = FMODSystem.update();

                    c--;
                }
            }
        }

        public static void StopSound(string name)
        {
            int name_length = name.Length + 1;

            if (SoundChannels.Count > 0)
            {
                for (int c = 0; c < SoundChannels.Count; c++)
                {
                    FMOD.Channel channel = SoundChannels[c];
                    FMOD.Sound sound = SoundOuts[c];

                    string name_output;

                    for (int i = 0; i < 10; i++)
                    {
                        string name_check = name;

                        if (i == 0)
                        {
                            FMOD.RESULT name_result = sound.getName(out name_output, name_length);
                        }
                        else
                        {
                            FMOD.RESULT name_result = sound.getName(out name_output, name_length + 1);
                            name_check += i.ToString();
                        }
                        
                        if (!string.IsNullOrEmpty(name_output) &&
                            name_output == name_check)
                        {
                            channel.stop();
                            SoundChannels.Remove(channel);
                            SoundOuts.Remove(sound);
                            c--;

                            FMOD.RESULT updated = FMODSystem.update();

                            break;
                        }
                    }
                }
            }
        }

        public static void StopAll()
        {
            StopMusic();
            StopAmbient();
            StopSound();
        }

        public static bool IsPlaying_Ambient(string name)
        {
            int name_length = name.Length + 1;

            if (AmbientEnabled)
            {
                if (AmbientChannels != null &&
                    AmbientChannels.Count > 0)
                {
                    for (int c = 0; c < AmbientChannels.Count; c++)
                    {
                        FMOD.Channel channel = AmbientChannels[c];
                        FMOD.Sound ambient = AmbientOuts[c];

                        string name_output;
                        ambient.getName(out name_output, name_length);

                        if (!string.IsNullOrEmpty(name_output) &&
                            name_output == name)
                        {
                            bool isPlaying = false;
                            FMOD.RESULT get_playing = channel.isPlaying(out isPlaying);
                            return isPlaying;
                        }
                    }
                }
            }

            return false;
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            StopAll();

            AmbientOuts = null;
            AmbientChannels = null;

            SoundOuts = null;
            SoundChannels = null;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

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
        public static FMOD.Sound AmbientOut;
        public static FMOD.Channel AmbientChannel;
        public static FMOD.ChannelGroup AmbientGroup;
        public static float AmbientVolume = 1;
        public static float AmbientFade = 0;
        public static bool AmbientLooping;
        public static bool AmbientPaused;
        public static bool AmbientPlaying;
        public static string AmbientPlaying_Name;

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

                if (Paused &&
                    !isPaused)
                {
                    FMOD.RESULT set_paused = MusicChannel.setPaused(true);
                }
                else if (!Paused &&
                         isPaused)
                {
                    FMOD.RESULT set_paused = MusicChannel.setPaused(false);
                }
            }

            if (AmbientEnabled)
            {
                float volume = (((AmbientVolume * 100) * (100 - (AmbientFade * 100))) / 100) / 100;
                if (volume < 0)
                {
                    volume = 0;
                }
                FMOD.RESULT set_ambient_volume = AmbientChannel.setVolume(volume);

                bool isPaused = false;
                FMOD.RESULT get_paused = AmbientChannel.getPaused(out isPaused);

                uint ambient_length;
                FMOD.RESULT get_ambient_length = AmbientOut.getLength(out ambient_length, FMOD.TIMEUNIT.MS);

                uint ambient_position;
                FMOD.RESULT get_ambient_position = AmbientChannel.getPosition(out ambient_position, FMOD.TIMEUNIT.MS);

                if (!AmbientLooping &&
                    ambient_length > 0 &&
                    ambient_position > 0 &&
                    ambient_position >= ambient_length)
                {
                    StopAmbient();
                }

                if (Paused &&
                    !isPaused)
                {
                    FMOD.RESULT set_paused = AmbientChannel.setPaused(true);
                }
                else if (!Paused &&
                         isPaused)
                {
                    FMOD.RESULT set_paused = AmbientChannel.setPaused(false);
                }
            }

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
                            else if (Paused &&
                                    !isPaused)
                            {
                                FMOD.RESULT set_paused = channel.setPaused(true);
                            }
                            else if (!Paused &&
                                     isPaused)
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

            FMODSystem.update();
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

                float volume = AmbientVolume - AmbientFade;
                if (volume < 0)
                {
                    volume = 0;
                }

                bool isPlaying = false;
                FMOD.RESULT get_ambient_playing = AmbientChannel.isPlaying(out isPlaying);

                uint ambient_length;
                FMOD.RESULT get_ambient_length = AmbientOut.getLength(out ambient_length, FMOD.TIMEUNIT.MS);

                uint ambient_position;
                FMOD.RESULT get_ambient_position = AmbientChannel.getPosition(out ambient_position, FMOD.TIMEUNIT.MS);

                if (isPlaying &&
                    ambient_length > 0 &&
                    ambient_position > 0)
                {
                    StopAmbient();
                }

                AmbientLooping = looping;
                if (AmbientLooping)
                {
                    FMODSystem.createStream(file, FMOD.MODE.LOOP_NORMAL, out AmbientOut);
                }
                else
                {
                    FMODSystem.createStream(file, FMOD.MODE.DEFAULT, out AmbientOut);
                }
                
                FMODSystem.playSound(AmbientOut, AmbientGroup, false, out AmbientChannel);
                AmbientChannel.setVolume(volume);

                AmbientPlaying = true;
                AmbientPlaying_Name = sound.Name;
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
                
                SoundOuts.Add(sound.SoundOut);
                SoundChannels.Add(channel);
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
                AmbientChannel.stop();
                AmbientPaused = false;
                AmbientPlaying = false;
                AmbientPlaying_Name = null;
            }
        }

        public static void StopSound()
        {
            if (SoundChannels.Count > 0)
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

        private void Game_Exiting(object sender, EventArgs e)
        {
            StopAll();
        }

        #endregion
    }
}

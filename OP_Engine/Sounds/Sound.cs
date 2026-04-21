using System;

namespace OP_Engine.Sounds
{
    public class Sound : IDisposable
    {
        #region Variables

        public string Name;
        public string Type;

        public FMOD.Sound SoundOut;
        public string Extension;
        public string Directory;
        public byte[] data;

        #endregion

        #region Constructor

        public Sound()
        {

        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {

        }

        #endregion
    }
}

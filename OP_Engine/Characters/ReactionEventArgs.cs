using System;
using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class ReactionEventArgs : EventArgs
    {
        public Direction Direction { get; private set; }
        public int Distance { get; private set; }
        public float LightLevel { get; private set; }
        public int Loudness { get; private set; }
        public int OdorStrength { get; private set; }

        public ReactionEventArgs(Direction direction, int distance, int loudness)
        {
            Direction = direction;
            Distance = distance;
            Loudness = loudness;
        }

        public ReactionEventArgs(Direction direction, int distance, float light_level)
        {
            Direction = direction;
            Distance = distance;
            LightLevel = light_level;
        }

        public ReactionEventArgs(Direction direction, int odor_strength)
        {
            Direction = direction;
            OdorStrength = odor_strength;
        }
    }
}

using System;
using OP_Engine.Enums;

namespace OP_Engine.Characters
{
    public class ReactionEventArgs : EventArgs
    {
        public Direction Direction { get; private set; } //What direction was it in?
        public int Distance { get; private set; } //How far away was it?
        public float LightLevel { get; private set; } //How dark was it?
        public int Strength { get; private set; } //How loud was the noise, strong the smell, potent the taste, etc?
        public int Scale { get; private set; } //How positive or negative was the experience? (e.g. scale of -10 to +10 with -10 being the worst imaginable)
        public string Adjective { get; private set; } //What would describe the thing being experienced? (e.g. horrible, sharp, sour, cold, etc)
        public BodyPart BodyPart { get; private set; } //What part of the body did it happen to?

        //Heard Something
        public ReactionEventArgs(Direction direction, int distance, string adjective, int strength, int scale)
        {
            Direction = direction;
            Distance = distance;
            Adjective = adjective;
            Strength = strength;
            Scale = scale;

            /*
            Example:
            Direction = Direction.Backward; //was behind me
            Distance = 1; //was next to me
            Adjective = "whisper";
            Strength = 1; //very soft noise
            Scale = -10; //was the worst

            Translation: I heard a terrifying whisper just behind me.
            */
        }

        //Saw Something
        public ReactionEventArgs(Direction direction, int distance, string adjective, float light_level, int scale)
        {
            Direction = direction;
            Distance = distance;
            Adjective = adjective;
            LightLevel = light_level;
            Scale = scale;

            /*
            Example:
            Direction = Direction.Forward; //was in front of me
            Distance = 12; //was not nearby
            Adjective = "slithering";
            LightLevel = 0.2; //was barely visible
            Scale = -10; //was the worst

            Translation: I could barely see something nightmarish slithing in the distance ahead of me.
            */
        }

        //Smelled Something
        public ReactionEventArgs(Direction direction, string adjective, int strength, int scale)
        {
            Direction = direction;
            Adjective = adjective;
            Strength = strength;
            Scale = scale;

            /*
            Example:
            Direction = Direction.East;
            Adjective = "death";
            Strength = 4; //was nearby
            Scale = -8; //was awful

            Translation: Something close by to the East was emanating a putrid smell of death.
            */
        }

        //Tasted Something
        public ReactionEventArgs(string adjective, int strength, int scale)
        {
            Adjective = adjective;
            Strength = strength;
            Scale = scale;

            /*
            Example:
            Adjective = "sweet";
            Strength = 2; //was minor
            Scale = 8; //was very good

            Translation: There was a deliciously sweet taste that was not too strong.
            */
        }

        //Felt Something
        public ReactionEventArgs(string adjective, int strength, int scale, BodyPart bodyPart)
        {
            Adjective = adjective;
            Strength = strength;
            Scale = scale;
            BodyPart = bodyPart;

            /*
            Example:
            Adjective = "sharp";
            Strength = 10; //was overwhelming
            Scale = -10; //was the worst
            BodyPart = Right Foot object;

            Translation: I felt extreme pain as something sharp punctured my right foot.
            */
        }
    }
}

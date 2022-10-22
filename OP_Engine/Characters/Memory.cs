using System.Collections.Generic;

using Microsoft.Xna.Framework;

using OP_Engine.Time;
using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class Memory
    {
        #region Variables

        public long ID;
        public TimeHandler DateTime; //When did this happen?
        public long CausedBy_Memory_ID; //Was this event caused by something else?
        public string Event_Type; //What happened? (e.g. witnessed a murder, car crash, broke a window)
        public string Emotion_Response; //Was the character happy it happened, or sad, or terrified?
        public int Memory_Strength; //How long will it be before this memory is forgotten? Was it fleeting or traumatic?
        public int Memory_Scale; //Was it a really good memory or a bad one? (e.g. scale of -10 to +10)

        public long Owner_ID; //Was this character the owner of this memory, or did they hear it as a story from someone else?
        public Vector3 Owner_Location; //Where was the character when this happened?
        public Direction Owner_Direction; //What direction was the character facing in? (e.g. Were they facing in the direction of where it happened?)

        public long Instigator_ID; //Who did it?
        public List<string> Instigator_Description = new List<string>(); //What did they look like?
        public Vector3 Instigator_Location; //Where did they do it?
        public Direction Instigator_Direction; //What direction were they facing in?
        public int Instigator_DistanceFrom_Owner; //How far away did it happen?
        public List<string> Instigator_Item_Description = new List<string>(); //Were they using an object to do it?

        public long Victim_ID; //Who or what was it done to?
        public List<string> Victim_Description = new List<string>(); //What did the victim look like?
        public Vector3 Victim_Location; //Where was the victim?
        public Direction Victim_Direction; //What direction was the victim facing?
        public int Victim_DistanceFrom_Instigator; //Was the instigator next to the victim or was it done from a distance?
        public List<string> Victim_Item_Description = new List<string>(); //Was the victim using something at the time?

        //Note: descriptions are saved in case they had changed between now and the DateTime it happened
        #endregion

        #region Constructors

        public Memory()
        {

        }

        #endregion

        #region Methods



        #endregion
    }
}

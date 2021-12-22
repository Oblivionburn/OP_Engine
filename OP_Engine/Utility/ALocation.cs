namespace OP_Engine.Utility
{
    public class ALocation
    {
        #region Variables

        public int X;
        public int Y;
        public int Priority;
        public int Distance_ToStart;
        public int Distance_ToDestination;
        public ALocation Parent;

        #endregion

        #region Constructor

        public ALocation(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

    }
}
namespace Tanks.Components.StateComponents
{
    class Idle
    {
        public int wayForMove;
        public int maxCellsForMove = 7;

        public Idle(int maxCellsForMove)
        {
            this.maxCellsForMove = maxCellsForMove;
        }
    }
}

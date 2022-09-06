using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace UntitledProject
{
    class PositionTranslator
    {
        //screen sense as in withing the play area window. Does not function properly if position is within the right screen (<resolution/2)
        public Point ScreenToSubBiomePos(Container cell, Point position)
        {
            return new Point((position.X - position.X % 5) / 5 + cell.x, (position.Y - position.Y % 5) / 5 + cell.y);
        }
        public Point SubBiomeToBiome(Container cell, Point position)
        {
            return new Point((position.X - position.X % 5) / 5 + cell.x, (position.Y - position.Y % 5) / 5 + cell.y);
        }
        public Point ScreenToBiomePos(Container cell, Point position)
        {
            return new Point((position.X - position.X % 25) / 25 + cell.x, (position.Y - position.Y % 25) / 25 + cell.y);
        }
        public Point SubBiomeToScreenPos(Container cell, Point position)
        {
            if (cell != null) return new Point(position.X*5, position.Y *5);
            else return new Point(-1, -1);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace UntitledProject
{
    class Building
    {
        Point position;
        public Building()
        {

        }

        public void Update()
        {

        }
        public void Render()
        {

        }
    }
    class Settlement
    {
        Point position;
        public int pops, housedPops, level;
        double radius;
        Rectangle rect;
        bool render;
        Container cell;
        
        public Settlement(Point position, Container cell)
        {
            level = 0;
            this.cell = cell;
            this.position = position;
            render = true;
            pops = 100;
            housedPops = 0;
            radius = 30;
            rect = new Rectangle(new Point(750+position.X - cell.x * 5-5, position.Y - cell.y * 5-5), new Point(10, 10));
        }
        public void Update(Container cell)
        {
            if (cell!=null)
            {
                if (this.cell.x == cell.x && this.cell.y == cell.y)
                {
                    render = true;
                }
                else
                {
                    render = false;
                }
            }
            else
            {
                render = false;
            }
            
        }
        public void Render(Texture2D texture, SpriteBatch sb)
        {
            if (render)
            {
                sb.Draw(texture, rect, Color.White);
            }
        }
        public Point GetScreenPos()
        {
            return new Point(position.X - cell.x * 5, position.Y - cell.y * 5);
        }
        public double GetFood()
        {
            double upkeep = pops * -0.1;
            return 0.0;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace UntitledProject
{

    class Faction
    {
        /*
         resources= Food, Wood, Stone, Metal, Mana
         */
        public double[] resources;
        double[] efficencies;
        int population;
        bool chosen = false;
        List<Settlement> settlements;
        public bool buildmode;

        public Faction()
        {
            
            buildmode = false;
            population = 0;
            resources = new double[5];
            efficencies = new double[5];
            settlements = new List<Settlement>();
           
        }

        public void FoundSettlement(Point position, Container cell)
        {
            if (chosen && resources[0] >= 150 && resources[1] >= 100)
            {
                resources[0] -= 150;
                resources[1] -= 100;
                settlements.Add(new Settlement(position, cell));
            }
            
            buildmode = false;
        }
        public void Update(Container cell)
        {
            int popsum = 0;
            foreach (Settlement temp in settlements)
            {
                temp.Update(cell);
                popsum += temp.pops;
            }
            if (popsum != population) population = popsum;
        }
        public void Render(Texture2D texture, SpriteBatch sb)
        {
            foreach (Settlement temp in settlements)
            {
                temp.Render(texture, sb);
            }
        }
        public void SetBuildMode()
        {
            buildmode = true;
        }
        public void Initialize(int race)
        {
            chosen = true;
            if (race == 0)
            {
                resources[0] = 200;
                resources[1] = 150;
                for (int i = 0; i < 5; i++)
                {
                    efficencies[i] = 1.0;
                }
            }
        }

        public void Tick()
        {

        }
    }
}

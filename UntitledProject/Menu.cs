using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace UntitledProject
{

    class Sidebar
    {
        List<Rectangle> rects, bigRects;
        List<bool> hoover, bigHoover;
        Texture2D small, big;
        List<TextBox> titles;
        List<Button> buttons;
        public Sidebar(Point origin, Point resolution, GraphicsDevice graphicsDevice, SpriteFont sf)
        {
            titles = new List<TextBox>();
            buttons = new List<Button>();

            hoover = new List<bool>();
            rects = new List<Rectangle>();

            bigHoover = new List<bool>();
            bigRects = new List<Rectangle>();


            rects.Add(new Rectangle(origin ,new Point(10, 30)));
            bigRects.Add(new Rectangle(origin, new Point(100, 300)));

            titles.Add(new TextBox(sf, "BUILD",new Point(800,0),graphicsDevice));
            buttons.Add(new Button(sf,"Settlement", new Point(800, 35), graphicsDevice));

            hoover.Add(false);
            bigHoover.Add(false);

            SetSmallTexture(graphicsDevice, new Point(resolution.X / 75, resolution.Y / 25));
            SetBigTexture(graphicsDevice, new Point(100, 300));

        }
        public void SetSmallTexture(GraphicsDevice graphicsDevice, Point resolution)
        {
            small = new Texture2D(graphicsDevice,resolution.X,resolution.Y);
            Color[] color = new Color[resolution.X * resolution.Y];
            for (int i = 0; i < resolution.X; i++)
            {
                for (int j = 0; j < resolution.Y; j++)
                {

                    color[i + j * resolution.X] = Color.Black*0.25f;

                }
            }
            small.SetData(color);
        }
        public void SetBigTexture(GraphicsDevice graphicsDevice, Point resolution)
        {
            big = new Texture2D(graphicsDevice, resolution.X, resolution.Y);
            Color[] color = new Color[resolution.X * resolution.Y];
            for (int i = 0; i < resolution.X; i++)
            {
                for (int j = 0; j < resolution.Y; j++)
                {

                    color[i + j * resolution.X] = Color.Black * 0.25f;

                }
            }
            big.SetData(color);
        }
        public void Update(MouseState old, Faction player)
        {
            int i = 0;
            MouseState mouse = Mouse.GetState();
            Point point = new Point(mouse.X, mouse.Y);
            foreach (Rectangle temp in rects)
            {
                

                hoover[i] = temp.Contains(point);
                if (hoover[i] || bigHoover[i])
                {
                    bigHoover[i] = bigRects[i].Contains(point);
                    if (buttons[i].IsClicked(old))
                    {
                        player.SetBuildMode();
                    }
                }
                i++;
            }
        }
        public void Render(SpriteBatch sb, SpriteFont font)
        {
            int i = 0;
            foreach (Rectangle temp in rects)
            {
                
                if (bigHoover[i])
                {
                    sb.Draw(big, bigRects[i], Color.White);
                    titles[i].Render(sb, font);
                    buttons[i].Render(sb, font);
                }
                else
                {
                    sb.Draw(small, temp, Color.White);
                }
                i++;
            }
            
        }
    }
    

    class GUI
    {
        Texture2D texture;
        Rectangle boundingRect;
        Random rgen;
        double[] resources = new double[5];

        public GUI(Point origin, Point resolution, GraphicsDevice graphicsDevice)
        {
            
            rgen = new Random();

            boundingRect = new Rectangle(origin, resolution);
            texture = new Texture2D(graphicsDevice, resolution.X, resolution.Y);
            texture.SetData(ResourceOverlay(resolution));
        }
        public Color[] ResourceOverlay(Point resolution)
        {
            Color[] color = new Color[resolution.X * resolution.Y];
            for (int i = 0; i < resolution.X; i++)
            {
                for (int j = 0; j < resolution.Y; j++)
                {

                    if (j < 30 && i < 750)
                    {
                        if (i < 1 || j < 1 || i > 748 || j > 28)
                        {
                            color[i + j * resolution.X] = Color.Lerp(Color.Black, Color.SlateGray, 0.65f);
                            continue;
                        }
                        else if (i < 2 || j < 2 || i > 747 || j > 27)
                        {
                            color[i + j * resolution.X] = Color.Lerp(Color.Black, Color.SlateGray, 0.75f);
                            continue;
                        }
                        else if (i < 3 || j < 3 || i > 746 || j > 26)
                        {
                            color[i + j * resolution.X] = Color.Lerp(Color.Black, Color.SlateGray, 0.95f);
                            continue;
                        }
                        double number = rgen.NextDouble();
                        if (number > 0.8) color[i + j * resolution.X] = Color.Lerp(Color.SlateGray, Color.White, 0.01f);
                        else if (number > 0.6) color[i + j * resolution.X] = Color.Lerp(Color.SlateGray, Color.White, 0.075f);
                        else if (number > 0.4) color[i + j * resolution.X] = Color.Lerp(Color.SlateGray, Color.White, 0.05f);
                        else if (number > 0.2) color[i + j * resolution.X] = Color.Lerp(Color.SlateGray, Color.White, 0.025f);
                        else color[i + j * resolution.X] = Color.SlateGray;
                    }
                    else
                    {
                        color[i + j * resolution.X] = Color.Transparent;
                    }
                }
            }
            return color;
        }

        public Point MouseToWorldPos(Container cell,Point resolution)
        {
            MouseState ms = Mouse.GetState();
            if (ms.X>=resolution.X/2 && cell!=null)
            {
                Point worldPosition = new Point(cell.x * 5 + ms.X-(resolution.X / 2), cell.y * 5 + ms.Y);
                return worldPosition;
            }
            return new Point(-1,-1);
        }

        public bool Click(MouseState old)
        {
            MouseState mouseState = Mouse.GetState();
            if (boundingRect.Contains(new Point(mouseState.X, mouseState.Y)) && (old.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed))
            {
                return true;
            }
            return false;
        }
        /*resources = player.resources;
            if (player.buildmode && Click(old))
            {
                Point pos = MouseToWorldPos(cell, resolution);
                if(pos!=new Point(-1,-1))
                player.FoundSettlement(pos,cell);
            }*/
        double time = 0.0;
        public void Update(Faction player, MouseState old, Container cell, Point resolution, MapCloseup close, Biome[,] biomes, GameTime gt)
        {
            ///highlightTexture.SetData(HighLight(new Point(old.X, old.Y), resolution, 15));
            resources = player.resources;
            /*if (player.buildmode && Click(old))
            {
                Point pos = MouseToWorldPos(cell, resolution);
                if(pos!=new Point(-1,-1))
                player.FoundSettlement(pos,cell);
            }*/
            time+= gt.ElapsedGameTime.TotalSeconds;
            MouseState mouseState = Mouse.GetState();
            Point mouse = new Point(mouseState.X, mouseState.Y);
            Point dif = new Point(Math.Abs(mouseState.X- old.X), Math.Abs(mouseState.Y - old.Y));
            /*if (boundingRect.Contains(mouse) && (mouseState.LeftButton == ButtonState.Pressed))
            {
                if (mouse.X < resolution.X / 2 && time>=1.0/30.0)
                {
                    close.ChangePos(mouse);
                    time = 0.0;
                }
            }
            else*/ if (boundingRect.Contains(mouse) && (old.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed))
            {
                if (mouse.X < resolution.X / 2)
                {
                    close.ChangePos(mouse);
                }
                else if(close.render)
                {
                    Point mouse2 = new Point(mouseState.X-750, mouseState.Y);
                    close.Zoom(mouse2);
                    
                }
            }
            /*if (mouse.X > resolution.X / 2 && time >= 1.0 / 30.0)
            {
                close.Pan(new Point(mouseState.X - 750, mouseState.Y));
                time = 0.0;
            }
            //close.Pan(new Point(mouseState.X - 750, mouseState.Y));*/

        }
        public void Render(SpriteBatch sb, SpriteFont font)
        {
            sb.Draw(texture, boundingRect, Color.White);
            
            sb.DrawString(font, resources[0].ToString(), new Vector2(75 + 7, 5), Color.White);
            sb.DrawString(font, resources[1].ToString(), new Vector2(225 + 7, 5), Color.White);
            sb.DrawString(font, resources[2].ToString(), new Vector2(375 + 7, 5), Color.White);
            sb.DrawString(font, resources[3].ToString(), new Vector2(525 + 7, 5), Color.White);
            sb.DrawString(font, resources[4].ToString(), new Vector2(675 + 7, 5), Color.White);

            sb.DrawString(font, "F", new Vector2(60, 5), Color.Green);
            sb.DrawString(font, "W", new Vector2(210, 5), Color.SaddleBrown);
            sb.DrawString(font, "S", new Vector2(360, 5), Color.LightGray);
            sb.DrawString(font, "I", new Vector2(510, 5), Color.Black);
            sb.DrawString(font, "M", new Vector2(660, 5), Color.Blue);
        }
    }
    class Menu
    {
        List<TextBox>[] textBoxes = new List<TextBox>[2];
        List<Button>[] buttons = new List<Button>[2];
        int choice = 0;
        TextBox title, text1;
        Button race1, reroll;
        String totalPop;

        public Menu(SpriteFont spriteFont, GraphicsDevice graphicsDevice)
        {
            race1 = new Button(spriteFont, "HUMAN", new Point(750 + 375, 140), graphicsDevice);
            reroll = new Button(spriteFont, "Reroll map", new Point(750 + 375, 540), graphicsDevice);

            textBoxes[0] = new List<TextBox>();
            textBoxes[0].Add(new TextBox(spriteFont, "WELCOME TO THE SEA OF THESYS", new Point(750 + 375, 40), graphicsDevice));
            textBoxes[0].Add(new TextBox(spriteFont, "CHOOSE YOUR RACE TO BEGIN ", new Point(750 + 375, 70), graphicsDevice));

            textBoxes[1] = new List<TextBox>();
            textBoxes[1].Add(new TextBox(spriteFont, "SATISTICS:", new Point(750 + 375, 40), graphicsDevice));

            buttons[0] = new List<Button>();
            buttons[0].Add(race1);
            buttons[0].Add(reroll);

            buttons[1] = new List<Button>();
        }
        public void Update(MouseState mouseState,Faction player, PMap map, GraphicsDevice graphicsDevice,MapCloseup close)
        {
            if (!close.render)
            {
                if (choice == 0) 
                {
                    if (race1.IsClicked(mouseState))
                    {
                        player.Initialize(0);
                        choice++;
                    }
                    if (reroll.IsClicked(mouseState))
                    { 
                        map.ReRoll(5, 750, graphicsDevice, 25);
                        close.updateWrld(map.biomes);
                    }
                }
            }
        }
        public void Render(SpriteBatch sb, SpriteFont font)
        {
            foreach (TextBox temp in textBoxes[choice])
            {
                temp.Render(sb, font);
                
            }
            foreach (Button temp in buttons[choice])
            {
                temp.Render(sb, font);

            }

        }
    }
    
    class Button
    {
        Point origin;
        Point dimensions;
        Rectangle boundingRect;
        Texture2D texture;
        String text;
        public Button(SpriteFont spriteFont, String text, Point origin, GraphicsDevice graphicsDevice)
        {
            
            Vector2 temp=spriteFont.MeasureString(text);
            dimensions = new Point((int)temp.X+20,(int)temp.Y+10);
            this.origin = new Point(origin.X - (int)((temp.X + 20.0f)/ 2.0f), origin.Y);
            
            boundingRect = new Rectangle(this.origin, dimensions);
            this.text = text;
            texture = new Texture2D(graphicsDevice,dimensions.X, dimensions.Y);
            Color[] color = new Color[dimensions.X * dimensions.Y];
            for(int i =0; i < dimensions.X; i++)
            {
                for (int j = 0; j < dimensions.Y; j++)
                {
                    color[dimensions.Y * i + j] = Color.DimGray;
                }
            }
            texture.SetData(color);
        }
        public bool IsClicked(MouseState old)
        {
            MouseState mouseState = Mouse.GetState();
            if(boundingRect.Contains(new Point(mouseState.X, mouseState.Y)) && (old.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed))
            {
                return true;
            }
            return false;
        }
        public void Render(SpriteBatch sb, SpriteFont font)
        {
            sb.Draw(texture, boundingRect, Color.White);
            sb.DrawString(font, text, new Vector2(origin.X+10, origin.Y+5), Color.White);
        }
    }
    class TextBox
    {
        Point origin;
        Point dimensions;
        Rectangle boundingRect;
        Texture2D texture;
        String text;
        public TextBox(SpriteFont spriteFont, String text, Point origin, GraphicsDevice graphicsDevice)
        {
            Vector2 temp = spriteFont.MeasureString(text);
            dimensions = new Point((int)temp.X + 20, (int)temp.Y + 10);
            this.origin = new Point(origin.X - (int)((temp.X + 20.0f) / 2.0f), origin.Y);

            boundingRect = new Rectangle(this.origin, dimensions);
            this.text = text;
            texture = new Texture2D(graphicsDevice, dimensions.X, dimensions.Y);
            Color[] color = new Color[dimensions.X * dimensions.Y];
            for (int i = 0; i < dimensions.X; i++)
            {
                for (int j = 0; j < dimensions.Y; j++)
                {
                    color[dimensions.Y * i + j] = Color.DimGray;
                }
            }
            texture.SetData(color);
        }
        public void Render(SpriteBatch sb, SpriteFont font)
        {
            sb.Draw(texture, boundingRect, Color.White);
            sb.DrawString(font, text, new Vector2(origin.X + 10, origin.Y + 5), Color.White);
        }
    }
}

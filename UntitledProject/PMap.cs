using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UntitledProject
{
    
    class Vector2D
    {
        double x, y;
        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double dot(Vector2D compare)
        {
            return this.x * compare.x + this.y * compare.y;
        }
    }
    

    

    class Container
    {
        double[] biomePercentage;
        public Rectangle boundingRect;
        public bool hoover = false;
        public int x, y, size;

        //x,y in container, not x,y on screen
        public Container(int side, int x, int y)
        {
           
            this.x = x*side;
            this.y = y*side;
            size = side;
            boundingRect = new Rectangle(this.x,this.y,side,side);
            
            
        }
        
        /*x,y on screen/
        public Container(Container origin, int scale, int x, int y, GraphicsDevice graphicsDevice, int[,,] biomeref)
        {
            biomePercentage = origin.biomePercentage;
            int side = (origin.size * scale);//-scale;
            size = side;
            boundingRect = new Rectangle(x, y, side, side);
            cells = new Biome[side, side];
            for (int i = 0; i < origin.size; i++)
            {
                for (int j = 0; j < origin.size; j++)
                {
                    
                    for(int a= 0; a < scale; a++)
                    {
                        for (int b = 0; b < scale; b++)
                        {
                            cells[i * scale + a, j * scale + b] = origin.cells[i, j];
                            /*
                            double heightTemp, humidityTemp, temperatureTemp;
                            heightTemp = Lerp((double)b*(1.0/scale),Lerp((double)a * (1.0 / scale), origin.cells[i, j].height, origin.cells[i + 1, j].height),Lerp((double)a * (1.0 / scale), origin.cells[i, j+1].height, origin.cells[i + 1, j+1].height));
                            humidityTemp = Lerp((double)b * (1.0 / scale), Lerp((double)a * (1.0 / scale), origin.cells[i, j].humidity, origin.cells[i + 1, j].humidity), Lerp((double)a * (1.0 / scale), origin.cells[i, j + 1].humidity, origin.cells[i + 1, j + 1].humidity));
                            temperatureTemp = Lerp((double)b * (1.0 / scale), Lerp((double)a * (1.0 / scale), origin.cells[i, j].temperature, origin.cells[i + 1, j].temperature), Lerp((double)a * (1.0 / scale), origin.cells[i, j + 1].temperature, origin.cells[i + 1, j + 1].temperature));
                            cells[i * scale+a, j * scale+b] = new Biome(heightTemp,humidityTemp,temperatureTemp,biomeref);
                        }
                    }
                    //cells[i * scale, j * scale] = origin.cells[i , j ];
                }
            }
            //zoom = getTextureZoom(graphicsDevice);
        }*/
        
        public String toString()
        {
            String returnValue = "";
            for (int i = 0; i < 36; i++)
            {
                
                if (biomePercentage[i] == 0.0) continue;
                //System.Diagnostics.Debug.WriteLine(biomePercentage[i] + "\n");
                returnValue += (double)i + ": " + biomePercentage[i]+"\n";
            }
            return returnValue;
        }
        /*public Texture2D getTextureZoom(GraphicsDevice graphicsDevice)
        {
            Color[] data = new Color[size * size];
            Texture2D texture = new Texture2D(graphicsDevice, size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i < 3 || j < 3 || i > size - 4 || j > size - 4) data[i + size * j] = Color.DarkRed;
                    else data[i + size * j] = cells[i,j].color;
                    //data[i + size * j] = 
                }
            }
            texture.SetData(data);
            return texture;
        }*/
        public Texture2D getTexture(GraphicsDevice graphicsDevice)
        {
            Color[] data = new Color[size * size];
            Texture2D texture = new Texture2D(graphicsDevice, size, size);
            for (int i=0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i < 1 || j < 1 || i > size - 2 || j > size - 2) data[i + size * j] = Color.White;
                    else data[i + size * j] = Color.Transparent;
                    //data[i + size * j] = 
                }
            }
            texture.SetData(data);
            return texture;
        }

        

    }
    class Biome
    {
        public double localFertility, temperatureCelsius, height, humidity, temperature, magic;
        public Color color,temperatureColor, humidityColor, upperColor;
        public int biome;
        public double metal;

        public Biome(double height, double humidity, double temperature, int[,,] biomeRef, double magic, Random rgen)
        {
            this.height = height;
            this.magic = magic;
            this.humidity = humidity;
            this.temperature = temperature;
            int heightZone, bioZone, tempZone;
            
            /* heightzone:      biozone:                tempzone:
             * 0=Mountain       0=barren                0=freezing
             * 1=Highlands      1=Low biomass           1=cold
             * 2=Lowlands       2=Medium biomass        2=temperate
             * 3=Coastal        3=High biomass          3=hot
             * 4=shallow Sea    4=Very high biomass     4=scorching
             * 5=Sea
             * 6=Ocean
             */
            heightZone = -1;
            bioZone = -1;
            tempZone = -1;
            localFertility = (humidity % 0.2) / 0.2;
            temperatureCelsius = temperature * 50 - 10;
            if (height >= 0.80)
            {
                heightZone = 0;
            }
            else if (height >= 0.65)
            {
                heightZone = 1;
            }
            else if (height >= 0.52)
            {
                heightZone = 2;
            }
            else if (height >= 0.50)
            {
                heightZone = 3;
            }
            else if (height >= 0.45)
            {
                heightZone = 4;
            }
            else if (height >= 0.25)
            {
                heightZone = 5;
            }
            else
            {
                heightZone = 6;
            }

            if (humidity >= 0.80)
            {
                bioZone = 4;
            }
            else if (humidity >= 0.60)
            {
                bioZone = 3;
            }
            else if (humidity >= 0.40)
            {
                bioZone = 2;
            }
            else if (humidity >= 0.20)
            {
                bioZone = 1;
            }
            else
            {
                bioZone = 0;
            }

            if (temperature >= 0.80)
            {
                tempZone = 4;
            }
            else if (temperature >= 0.60)
            {
                tempZone = 3;
            }
            else if (temperature >= 0.40)
            {
                tempZone = 2;
            }
            else if (temperature >= 0.20)
            {
                tempZone = 1;
            }
            else
            {
                tempZone = 0;
            }
            biome = biomeRef[heightZone, bioZone, tempZone];
            color= AssignBiomeColor(height, biome);
            temperatureColor = AssignTemperatureColor(temperature,height);
            humidityColor = AssignHumidityColor(humidity,height);
            
            metal = rgen.NextDouble();

        }

        public Biome(double height, double humidity, double temperature, int[,,] biomeRef, double magic, double metal)
        {
            this.metal = metal;
            this.height = height;
            this.magic = magic;
            this.humidity = humidity;
            this.temperature = temperature;
            int heightZone, bioZone, tempZone;

            /* heightzone:      biozone:                tempzone:
             * 0=Mountain       0=barren                0=freezing
             * 1=Highlands      1=Low biomass           1=cold
             * 2=Lowlands       2=Medium biomass        2=temperate
             * 3=Coastal        3=High biomass          3=hot
             * 4=shallow Sea    4=Very high biomass     4=scorching
             * 5=Sea
             * 6=Ocean
             */
            heightZone = -1;
            bioZone = -1;
            tempZone = -1;
            localFertility = (humidity % 0.2) / 0.2;
            temperatureCelsius = temperature * 50 - 10;
            if (height >= 0.80)
            {
                heightZone = 0;
            }
            else if (height >= 0.65)
            {
                heightZone = 1;
            }
            else if (height >= 0.52)
            {
                heightZone = 2;
            }
            else if (height >= 0.50)
            {
                heightZone = 3;
            }
            else if (height >= 0.45)
            {
                heightZone = 4;
            }
            else if (height >= 0.25)
            {
                heightZone = 5;
            }
            else
            {
                heightZone = 6;
            }

            if (humidity >= 0.80)
            {
                bioZone = 0;
            }
            else if (humidity >= 0.60)
            {
                bioZone = 1;
            }
            else if (humidity >= 0.40)
            {
                bioZone = 2;
            }
            else if (humidity >= 0.20)
            {
                bioZone = 3;
            }
            else
            {
                bioZone = 4;
            }

            if (temperature >= 0.80)
            {
                tempZone = 4;
            }
            else if (temperature >= 0.60)
            {
                tempZone = 3;
            }
            else if (temperature >= 0.40)
            {
                tempZone = 2;
            }
            else if (temperature >= 0.20)
            {
                tempZone = 1;
            }
            else
            {
                tempZone = 0;
            }
            biome = biomeRef[heightZone, bioZone, tempZone];
            color = AssignBiomeColor(height, biome);
            temperatureColor = AssignTemperatureColor(temperature, height);
            humidityColor = AssignHumidityColor(humidity, height);


        }
        public Color GetColor()
        {
            if(height<0.5) return Color.Lerp(color, upperColor, (float)height);
            return color;//Color.Lerp(color, upperColor, (float)(height-0.5)/0.5f);
        }
        public Biome Lerp(Biome goal, double t, int[,,] biomeRef)
        {
            double magic = Lerp(t, this.magic,goal.magic);
            double height = Lerp(t, this.height, goal.height);
            double temperature = Lerp(t, this.temperature, goal.temperature);
            double humidity = Lerp(t, this.humidity, goal.humidity);
            double metal = Lerp(t, this.metal, goal.metal);
            return new Biome(height,humidity,temperature,biomeRef,magic,metal);
        }
        private double Lerp(double t, double a1, double a2)
        {
            return a1 + t * (a2 - a1);
        }
        public Color AssignTemperatureColor(double temp, double height)
        {
            if (height < 0.5) return Color.Black;
            if (temp < 0.2) return Color.Lerp(Color.Blue, Color.Cyan, (float)((temp) / 0.2));
            else if(temp <0.4) return Color.Lerp(Color.Cyan, Color.Yellow, (float)((temp - 0.2) / 0.2));
            else if (temp < 0.6) return Color.Lerp(Color.Yellow, Color.Orange, (float)((temp - 0.4) / 0.2));
            else if (temp < 0.8) return Color.Lerp(Color.Orange, Color.Red, (float)((temp - 0.6) / 0.2));
            return Color.Lerp(Color.Red, Color.DarkRed, (float)((temp - 0.8) / 0.2));
        }
        public Color AssignHumidityColor(double humidity, double height)
        {
            if (height < 0.5) return Color.Black;
            if (humidity < 0.2) return Color.Lerp(Color.Red, Color.DarkOrange, (float)((humidity) / 0.2));
            else if (humidity < 0.4) return Color.Lerp(Color.DarkOrange, Color.Yellow, (float)((humidity - 0.2) / 0.2));
            else if (humidity < 0.6) return Color.Lerp(Color.Yellow, Color.Chartreuse, (float)((humidity - 0.4) / 0.2));
            else if (humidity < 0.8) return Color.Lerp(Color.Chartreuse, Color.ForestGreen, (float)((humidity - 0.6) / 0.2));
            return Color.Lerp(Color.ForestGreen, Color.DarkGreen, (float)((humidity - 0.8) / 0.2));
        }
        public Color AssignBiomeColor(double height, int biomenr)
        {

            Color hstart, hend, FColor;  
            if (biomenr == 0)//snow n ice mountain
            {
                color = Color.DarkGray;
                upperColor = Color.White;
                return Color.Lerp(Color.DarkGray, Color.White, (float)((height - 0.8) / 0.2));
            }
            else if (biomenr == 1)//rocky mountain
            {
                color = Color.DarkGray;
                upperColor = Color.Gray;
                return Color.DarkGray;//Color.Lerp(Color.DarkGray, Color.Gray, (float)((height - 0.8) / 0.2));
            }
            else if (biomenr == 2)//rocky artic desert
            {
                color = Color.Gray;
                upperColor = Color.White;
                FColor = Color.AliceBlue;
                hstart= Color.Lerp(Color.Black, FColor, 0.5f);
                hend= Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 3)//rocky temperate desert
            {
                color = Color.DarkOrange;
                upperColor = Color.Bisque;
                FColor = Color.BurlyWood;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 4)//rocky desert
            {
                color = Color.Gold;
                upperColor = Color.DarkKhaki;
                FColor = Color.Orange;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 5)//Highland Tundra
            {
                color = Color.Olive;
                upperColor = Color.DarkSeaGreen;
                FColor = Color.PaleGreen;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 6)//Highland Steppe
            {
                color = Color.Lerp(Color.Goldenrod, Color.DarkOliveGreen, 0.5f); ;
                upperColor = Color.YellowGreen;
                FColor = Color.Olive;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 7)//Highland savannah
            {
                color = Color.Lerp(Color.Goldenrod, Color.Chocolate, 0.5f); ;
                upperColor = Color.GreenYellow;
                FColor = Color.GreenYellow;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 8)//sparse taiga
            {
                color = Color.SeaGreen;
                upperColor = Color.MediumSeaGreen;
                FColor = Color.SeaGreen;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 9)//sparse forest
            {
                color = Color.Green;
                upperColor = Color.ForestGreen;
                FColor = Color.Green;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 10)//sparse tropical
            {
                color = Color.LimeGreen;

                upperColor = Color.LawnGreen;
                FColor = Color.Lerp(Color.LimeGreen, Color.DarkGreen, 0.35f);
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 11)//taiga
            {
                color = Color.Lerp(Color.SeaGreen, Color.DarkGreen, 0.2f);
                upperColor = Color.MediumSeaGreen;
                FColor = Color.Lerp(Color.SeaGreen, Color.DarkGreen, 0.5f);
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 12)//forest
            {
                color = Color.Lerp(Color.Green, Color.DarkGreen, 0.3f);
                upperColor = Color.ForestGreen;
                FColor = Color.Lerp(Color.Green, Color.Lerp(Color.DarkGreen, Color.Black, 0.25f), 0.5f);
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 13)//Tropical
            {
                color = Color.Lerp(Color.LimeGreen, Color.DarkGreen, 0.4f);
                upperColor = Color.LimeGreen;
                FColor = Color.ForestGreen;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 14)//Ancient Taiga
            {
                color = Color.Lerp(Color.SeaGreen, Color.DarkGreen, 0.8f);
                upperColor = Color.MediumSeaGreen;
                FColor = Color.Lerp(Color.SeaGreen, Color.Black, 0.45f);
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 15)//Deep Forest
            {
                color = Color.Lerp(Color.Green, Color.DarkGreen, 0.6f);
                upperColor = Color.ForestGreen;
                FColor = Color.Lerp(Color.DarkGreen, Color.Black, 0.25f);
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 16)//Rainforest
            {
                color = Color.Lerp(Color.LimeGreen, Color.DarkGreen, 0.8f);
                upperColor = Color.Green;
                FColor = Color.Lerp(Color.LimeGreen, Color.Black, 0.4f);
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 17)//Artic desert
            {
                color = Color.PowderBlue;
                upperColor = Color.White;
                FColor = Color.AliceBlue;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor; //Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 18)//temperate desert
            {
                color = Color.DarkOrange;
                upperColor = Color.Bisque;
                FColor = Color.BurlyWood;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height-0.4)/0.6));
            }
            else if (biomenr == 19)//desert
            {
                color = Color.Gold;
                upperColor = Color.DarkKhaki;
                FColor = Color.Orange;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 20)//Tundra
            {
                color = Color.Olive;
                upperColor = Color.DarkSeaGreen;
                FColor = Color.PaleGreen;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 21)//Steppe
            {
                color = Color.Lerp(Color.Goldenrod, Color.DarkGreen, 0.5f); 
                upperColor = Color.YellowGreen;
                FColor = Color.Olive;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 22)//Savannah
            {
                color = Color.Lerp(Color.Goldenrod, Color.Chocolate, 0.5f); 
                upperColor = Color.GreenYellow;
                FColor = Color.GreenYellow;
                hstart = Color.Lerp(Color.Black, FColor, 0.5f);
                hend = Color.Lerp(FColor, Color.White, 0.5f);
                return FColor;//Color.Lerp(hstart, hend, (float)((height - 0.4) / 0.6));
            }
            else if (biomenr == 23)//Frozen Beach
            {
                color = Color.PowderBlue;
                upperColor = Color.Khaki;
                return Color.LightGray;
            }
            else if (biomenr == 24)//icy Beach
            {
                color = Color.PowderBlue;
                upperColor = Color.Khaki;
                return Color.LemonChiffon;
            }
            else if (biomenr == 25)//Beach
            {
                color = Color.Linen;
                upperColor = Color.Khaki;
                return Color.Linen;
            }
            else if (biomenr == 26)//Marsh
            {
                color = Color.OliveDrab;
                upperColor = Color.Olive;
                return Color.Lerp(Color.DarkGreen, Color.OliveDrab, 0.5f);
            }
            else if (biomenr == 27)//Mangrove
            {
                color = Color.LimeGreen;
                upperColor = Color.Lime;
                return Color.Lerp(Color.DarkGreen, Color.Chartreuse, 0.5f);
            }
            else if (biomenr == 28)//Icy shallows
            {
                color = Color.DarkBlue;
                upperColor = Color.Lerp(Color.DarkCyan, Color.Blue, 0.5f);
                return Color.AliceBlue;
            }
            else if (biomenr == 29)//cold shallows
            {
                color = Color.DarkBlue;
                upperColor = Color.Lerp(Color.DarkCyan, Color.Blue, 0.5f);
                return Color.Lerp(Color.DarkBlue, Color.Blue, (float)((height) / 0.5));
            }
            else if (biomenr == 30)//shallows
            {
                color = Color.DarkBlue;
                upperColor = Color.Lerp(Color.DarkCyan, Color.Blue, 0.5f);
                return Color.Lerp(Color.DarkBlue, Color.Blue, (float)((height) / 0.5));
            }
            else if (biomenr == 31)//reefs
            {
                color = Color.DarkBlue;
                upperColor = Color.Lerp(Color.DarkCyan, Color.Blue, 0.5f);
                return Color.Lerp(Color.DarkBlue, Color.Blue, (float)((height) / 0.5));
            }
            else if (biomenr == 32)//Sea Ice
            {
                color = Color.AliceBlue;
                upperColor = Color.AliceBlue;
                return Color.AliceBlue;
            }
            else if (biomenr == 33)//cold seas
            {
                color = Color.DarkBlue;
                upperColor = Color.Lerp(Color.DarkCyan, Color.Blue, 0.5f);
                return Color.Lerp(Color.DarkBlue, Color.Blue, (float)((height) / 0.5));
            }
            else if (biomenr == 34)//Open seas
            {
                color = Color.DarkBlue;
                upperColor = Color.Lerp(Color.DarkCyan, Color.Blue, 0.5f);
                return Color.Lerp(Color.DarkBlue, Color.Blue, (float)((height) / 0.5));
            }
            else if (biomenr == 35)//Ocean
            {
                color = Color.DarkBlue;
                upperColor = Color.Lerp(Color.DarkCyan, Color.Blue, 0.5f);
                return Color.Lerp(Color.DarkBlue, Color.Blue, (float)((height)/0.5 ));
            }
            else return Color.Black;
        } 
        
        public double GetHeightAngle(int direction,Biome[,] biomes, Point pos)
        {
            if (direction == 0)
            {
                if (pos.Y == 0) return 0.0;
                return Math.Atan((biomes[pos.X, pos.Y - 1].height - height) * 150.0) * (180 / Math.PI);
            }
            else if(direction == 1)
            {
                if (pos.X == biomes.GetLength(0) - 1 || pos.Y == 0) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X + 1, pos.Y - 1].height - height) / Math.Sqrt(2)) * (180 / Math.PI);
            }
            else if (direction == 2)
            {
                if (pos.X == biomes.GetLength(0)-1) return 0.0;
                return Math.Atan((biomes[pos.X+1, pos.Y].height - height) * 150.0) * (180 / Math.PI);
            }
            else if (direction == 3)
            {
                if (pos.X == biomes.GetLength(0) - 1 || pos.Y == biomes.GetLength(1) - 1) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X + 1, pos.Y + 1].height - height) / Math.Sqrt(2)) * (180 / Math.PI);
            }
            else if (direction == 4)
            {
                if (pos.Y == biomes.GetLength(1) - 1) return 0.0;
                return Math.Atan((biomes[pos.X, pos.Y+1].height - height) * 150.0) * (180 / Math.PI);
            }
            else if (direction == 5)
            {
                if (pos.X == 0 || pos.Y == biomes.GetLength(1) - 1) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X - 1, pos.Y + 1].height - height) / Math.Sqrt(2)) * (180 / Math.PI);
            }
            else if (direction == 6)
            {
                if (pos.X == 0) return 0.0;
                return Math.Atan((biomes[pos.X - 1, pos.Y].height - height) * 150.0) * (180 / Math.PI);
            }
            else
            {
                if (pos.X == 0 || pos.Y == 0) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X - 1, pos.Y - 1].height - height) / Math.Sqrt(2)) * (180 / Math.PI);
            }
        }

        public double GetFertilityAngle(int direction, Biome[,] biomes, Point pos)
        {
            if (direction == 0)
            {
                if (pos.Y == 0) return 0.0;
                return Math.Atan((biomes[pos.X, pos.Y - 1].humidity - humidity) * 150.0) * (180 / Math.PI);
            }
            else if (direction == 1)
            {
                if (pos.X == biomes.GetLength(0) - 1 || pos.Y == 0) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X + 1, pos.Y - 1].humidity - humidity) / Math.Sqrt(2)) * (180 / Math.PI);
            }
            else if (direction == 2)
            {
                if (pos.X == biomes.GetLength(0) - 1) return 0.0;
                return Math.Atan((biomes[pos.X + 1, pos.Y].humidity - humidity) * 150.0) * (180 / Math.PI);
            }
            else if (direction == 3)
            {
                if (pos.X == biomes.GetLength(0) - 1 || pos.Y == biomes.GetLength(1) - 1) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X + 1, pos.Y + 1].humidity - humidity) / Math.Sqrt(2)) * (180 / Math.PI);
            }
            else if (direction == 4)
            {
                if (pos.Y == biomes.GetLength(1) - 1) return 0.0;
                return Math.Atan((biomes[pos.X, pos.Y + 1].humidity - humidity) * 150.0) * (180 / Math.PI);
            }
            else if (direction == 5)
            {
                if (pos.X == 0 || pos.Y == biomes.GetLength(1) - 1) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X - 1, pos.Y + 1].humidity - humidity) / Math.Sqrt(2)) * (180 / Math.PI);
            }
            else if (direction == 6)
            {
                if (pos.X == 0) return 0.0;
                return Math.Atan((biomes[pos.X - 1, pos.Y].humidity - humidity) * 150.0) * (180 / Math.PI);
            }
            else
            {
                if (pos.X == 0 || pos.Y == 0) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X - 1, pos.Y - 1].humidity - humidity) / Math.Sqrt(2)) * (180 / Math.PI);
            }
        }

        public double GetTemperatureAngle(int direction, Biome[,] biomes, Point pos)
        {
            if (direction == 0)
            {
                if (pos.Y == 0) return 0.0;
                return Math.Atan((biomes[pos.X, pos.Y - 1].temperature - temperature) * 150.0) * (180 / Math.PI);
            }
            else if (direction == 1)
            {
                if (pos.X == biomes.GetLength(0) - 1 || pos.Y == 0) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X + 1, pos.Y - 1].temperature - temperature) / Math.Sqrt(2)) * (180 / Math.PI);
            }
            else if (direction == 2)
            {
                if (pos.X == biomes.GetLength(0) - 1) return 0.0;
                return Math.Atan((biomes[pos.X + 1, pos.Y].temperature - temperature) * 150.0) * (180 / Math.PI);
            }
            else if (direction == 3)
            {
                if (pos.X == biomes.GetLength(0) - 1 || pos.Y == biomes.GetLength(1) - 1) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X + 1, pos.Y + 1].temperature - temperature) / Math.Sqrt(2)) * (180 / Math.PI);
            }
            else if (direction == 4)
            {
                if (pos.Y == biomes.GetLength(1) - 1) return 0.0;
                return Math.Atan((biomes[pos.X, pos.Y + 1].temperature - temperature) * 150.0) * (180 / Math.PI);
            }
            else if (direction == 5)
            {
                if (pos.X == 0 || pos.Y == biomes.GetLength(1) - 1) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X - 1, pos.Y + 1].temperature - temperature) / Math.Sqrt(2)) * (180 / Math.PI);
            }
            else if (direction == 6)
            {
                if (pos.X == 0) return 0.0;
                return Math.Atan((biomes[pos.X - 1, pos.Y].temperature - temperature) * 150.0) * (180 / Math.PI);
            }
            else
            {
                if (pos.X == 0 || pos.Y == 0) return 0.0;
                return Math.Atan(150.0 * (biomes[pos.X - 1, pos.Y - 1].temperature - temperature) / Math.Sqrt(2)) * (180 / Math.PI);
            }
        }
    }

    class PMap
    {
        Random rgen = new Random();
        
        double size;
        
        public Biome[,] biomes;
        int res, gridAmount;
        public Container[,] grid;
        Texture2D mapTexture, gridTexture;
        Rectangle boundingRect;
        public Container selectedContainer=null;
        public PlayArea area = null;
        
        public int[,,] biomeRef = new int[7, 5, 5]
        {
            //biomes[heightZone, biozone, tempzone]
            {
                {0,0,1,1,1 /*tempzone*/},//biozone
                {0,0,1,1,1},
                {0,0,1,1,1},
                {0,0,1,1,1},
                {0,0,1,1,1}
            },//0 heightzone
            {
                {2,2,3,4,4 /*tempzone*/},//biozone
                {5,5,6,7,4},
                {8,8,9,10,10},
                {11,11,12,13,13},
                {14,14,15,16,16}
            },//1
            {
                {17,17,18,19,19 /*tempzone*/},//biozone
                {20,20,21,22,19},
                {8,8,9,10,10},
                {11,11,12,13,13},
                {14,14,15,16,16}
            },//2
            {
                {23,24,25,25,25 /*tempzone*/},//biozone
                {23,24,25,25,25},
                {23,24,25,25,25},
                {23,24,26,27,27},
                {23,24,26,27,27}
            },//3
            {
                {28,29,30,30,30 /*tempzone*/},//biozone
                {28,29,30,30,30},
                {28,29,30,30,30},
                {28,29,30,30,31},
                {28,29,30,31,31}
            },//4
            {
                {32,33,34,34,34 /*tempzone*/},//biozone
                {32,33,34,34,34},
                {32,33,34,34,34},
                {32,33,34,34,34},
                {32,33,34,34,34}
            },//5
            {
                {32,35,35,35,35 /*tempzone*/},//biozone
                {32,35,35,35,35},
                {32,35,35,35,35},
                {32,35,35,35,35},
                {32,35,35,35,35}
            }//6
        };

        public PMap(int size, int resolution, GraphicsDevice graphicsDevice, int gridAmount)
        {
            Vector2D[,] height, magic1, temperature, humidity;
            this.gridAmount = gridAmount;
            double highest = 0.0;
            double lowest = 1.0;
            double fhighest = 0.0;
            double flowest = 1.0;
            double thighest = 0.0;
            double tlowest = 1.0;
            double t2highest = 0.0;
            double t2lowest = 1.0;
            double mhighest = 0.0;
            double mlowest = 1.0;
            double value, magic;
            this.size = size+2;
            res = resolution;
            boundingRect = new Rectangle(0, 0, resolution, resolution);
            double[,] heightPoints = new double[resolution, resolution];
            double[,] temperaturePoints = new double[resolution, resolution];
            double[,] humidityPoints = new double[resolution, resolution];
            double[,] magicPoints = new double[resolution, resolution];
            height = GenerateConstants(size+2);
            magic1 = GenerateConstants(size+2);
            temperature = GenerateConstants(size+2);
            humidity = GenerateConstants(size+2);
            double[,] filter = CircleGradient(resolution);
            double[,] filter2 = VecticalGradient(resolution, 4*resolution/5);
            biomes = new Biome[resolution, resolution];
            grid = new Container[25, 25];
            
            for (int i=0; i < resolution; i++)
            {
                for(int j = 0; j < resolution; j++)
                {
                    value = GetValueOfPoint(i, j, 6, resolution, height);//*filter[i,j];
                    if (value > highest) highest = value;
                    if (value < lowest) lowest = value;
                    heightPoints[i, j] = value;

                    magic = GetValueOfPoint(i, j, 6, resolution, magic1);
                    if (magic > mhighest) mhighest = magic;
                    if (magic < mlowest) mlowest = magic;
                    magicPoints[i, j] = magic;

                    double temp = GetValueOfPoint(i, j, 6, resolution, temperature) * 0.5 + filter2[i, j];
                    if (temp > thighest) thighest = temp;
                    if (temp < tlowest) tlowest = temp;
                    temperaturePoints[i, j] = temp;
                }
            }
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    value = Normalize(heightPoints[i, j], lowest, highest)*filter[i,j];
                    heightPoints[i, j] = value;// * filter[i, j];
                    if (value > fhighest) fhighest = value;
                    if (value < flowest) flowest = value;

                    magic = Normalize(magicPoints[i, j], mlowest, mhighest);
                    magicPoints[i, j] = magic;

                    double temp = Normalize(temperaturePoints[i, j], tlowest, thighest);
                    if (temp < 0) temp = 0;
                    if (temp > t2highest) t2highest = temp;
                    if (temp < t2lowest) t2lowest = temp;
                    temperaturePoints[i, j] = temp;
                    //heightPoints[i, j] = filter[i, j];
                }
            }
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    
                    value = Normalize(heightPoints[i, j], flowest, fhighest);
                    double elevationEffect;
                    if (value > 0.5) elevationEffect = Math.Pow((value - 0.5) / 0.5, 2);
                    else elevationEffect = 0.0;
                    double temp = Normalize(temperaturePoints[i, j], t2lowest, t2highest)-elevationEffect;
                    if (temp < 0.0) temp = 0;
                    temperaturePoints[i, j] = temp;
                    heightPoints[i, j] = value;
                }
            }
            double[,] filter3 = SeaGradient(resolution, heightPoints);
            highest = 0.0;
            lowest = 1.0;
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    value = GetValueOfPoint(i, j, 6, resolution, humidity);
                    if (value > highest) highest = value;
                    if (value < lowest) lowest = value;
                    humidityPoints[i, j] = value;
                }
            }
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    value = Normalize(humidityPoints[i, j], lowest, highest)*(1-filter3[i, j]);
                    if (value < 0.0) value = 0.0;
                    if (value > fhighest) fhighest = value;
                    if (value < flowest) flowest = value;
                    humidityPoints[i, j] = value;
                }
            }
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    value = Normalize(humidityPoints[i, j], flowest, fhighest);
                    humidityPoints[i, j] = value;
                    biomes[i, j] = new Biome(heightPoints[i,j], humidityPoints[i,j],temperaturePoints[i,j],biomeRef,magicPoints[i,j],rgen);
                }
            }

            for (int i = 0; i < gridAmount; i++)
            {
                for (int j = 0; j < gridAmount; j++)
                {
                    grid[i, j] = new Container(resolution / gridAmount, i, j);
                }
            }
            mapTexture = getTexture(graphicsDevice);
            gridTexture = grid[0, 0].getTexture(graphicsDevice);
            
        }
        public void ReRoll(int size, int resolution, GraphicsDevice graphicsDevice, int gridAmount)
        {
            Vector2D[,] height, magic1, temperature, humidity;
            this.gridAmount = gridAmount;
            double highest = 0.0;
            double lowest = 1.0;
            double fhighest = 0.0;
            double flowest = 1.0;
            double thighest = 0.0;
            double tlowest = 1.0;
            double t2highest = 0.0;
            double t2lowest = 1.0;
            double mhighest = 0.0;
            double mlowest = 1.0;
            double value, magic;
            this.size = size + 2;
            res = resolution;
            boundingRect = new Rectangle(0, 0, resolution, resolution);
            double[,] heightPoints = new double[resolution, resolution];
            double[,] temperaturePoints = new double[resolution, resolution];
            double[,] humidityPoints = new double[resolution, resolution];
            double[,] magicPoints = new double[resolution, resolution];
            height = GenerateConstants(size + 2);
            magic1 = GenerateConstants(size + 2);
            temperature = GenerateConstants(size + 2);
            humidity = GenerateConstants(size + 2);
            double[,] filter = CircleGradient(resolution);
            double[,] filter2 = VecticalGradient(resolution, 4 * resolution / 5);
            biomes = new Biome[resolution, resolution];
            grid = new Container[25, 25];

            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    value = GetValueOfPoint(i, j, 6, resolution, height);//*filter[i,j];
                    if (value > highest) highest = value;
                    if (value < lowest) lowest = value;
                    heightPoints[i, j] = value;

                    magic = GetValueOfPoint(i, j, 6, resolution, magic1);
                    if (magic > mhighest) mhighest = magic;
                    if (magic < mlowest) mlowest = magic;
                    magicPoints[i, j] = magic;

                    double temp = GetValueOfPoint(i, j, 6, resolution, temperature) * 0.5 + filter2[i, j];
                    if (temp > thighest) thighest = temp;
                    if (temp < tlowest) tlowest = temp;
                    temperaturePoints[i, j] = temp;

                }
            }
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    value = Normalize(heightPoints[i, j], lowest, highest) * filter[i, j];
                    heightPoints[i, j] = value;// * filter[i, j];
                    if (value > fhighest) fhighest = value;
                    if (value < flowest) flowest = value;

                    magic = Normalize(magicPoints[i, j], mlowest, mhighest);
                    magicPoints[i, j] = magic;

                    double temp = Normalize(temperaturePoints[i, j], tlowest, thighest);
                    if (temp < 0) temp = 0;
                    if (temp > t2highest) t2highest = temp;
                    if (temp < t2lowest) t2lowest = temp;
                    temperaturePoints[i, j] = temp;
                    //heightPoints[i, j] = filter[i, j];
                }
            }
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {

                    value = Normalize(heightPoints[i, j], flowest, fhighest);
                    double elevationEffect;
                    if (value > 0.5) elevationEffect = Math.Pow((value - 0.5) / 0.5, 2);
                    else elevationEffect = 0.0;
                    double temp = Normalize(temperaturePoints[i, j], t2lowest, t2highest) - elevationEffect;
                    if (temp < 0.0) temp = 0;
                    temperaturePoints[i, j] = temp;
                    heightPoints[i, j] = value;// * filter[i, j];

                    //heightPoints[i, j] = filter3[i, j];
                }
            }
            double[,] filter3 = SeaGradient(resolution, heightPoints);
            highest = 0.0;
            lowest = 1.0;
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    value = GetValueOfPoint(i, j, 6, resolution, humidity);//*filter[i,j];
                    if (value > highest) highest = value;
                    if (value < lowest) lowest = value;
                    humidityPoints[i, j] = value;
                }
            }
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    value = Normalize(humidityPoints[i, j], lowest, highest) * (1 - filter3[i, j]);// * (1-filter3[i, j]);// * filter3[i, j];
                    if (value < 0.0) value = 0.0;
                    if (value > fhighest) fhighest = value;
                    if (value < flowest) flowest = value;
                    humidityPoints[i, j] = value;
                }
            }
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    value = Normalize(humidityPoints[i, j], flowest, fhighest);
                    humidityPoints[i, j] = value;
                    biomes[i, j] = new Biome(heightPoints[i, j], humidityPoints[i, j], temperaturePoints[i, j], biomeRef, magicPoints[i, j],rgen);
                }
            }

            for (int i = 0; i < gridAmount; i++)
            {
                for (int j = 0; j < gridAmount; j++)
                {
                    grid[i, j] = new Container(resolution / gridAmount, i, j);
                }
            }
            mapTexture = getTexture(graphicsDevice);
            gridTexture = grid[0, 0].getTexture(graphicsDevice);

        }

        public void Update(MouseState old, GraphicsDevice graphicsDevice)
        {
            /*
            MouseState mouseState= Mouse.GetState();
            for (int i = 0; i < gridAmount; i++)
            {
                for (int j = 1; j < gridAmount; j++)
                {
                    if(grid[i, j].boundingRect.Contains(new Point(mouseState.X, mouseState.Y)))
                    {
                        grid[i, j].hoover = true;

                        if (old.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (selectedContainer == grid[i, j])
                            {
                                selectedContainer = null;
                                
                            }
                            else
                            {
                                selectedContainer = grid[i, j];
                                if (area == null)
                                {
                                    area = new PlayArea(25, 750, 0, grid[i, j], biomes, graphicsDevice);
                                }
                                else
                                {
                                    area.ChangeCell(grid[i, j], biomes);
                                }
                                //if(enlarged[i, j]==null && ( (i>=13) && (j < 12))) enlarged[i, j] = new Container(grid[i, j], 12, 13 * selectedContainer.size, 13 * selectedContainer.size, graphicsDevice, biomeRef);
                                //else if (enlarged[i, j] == null) enlarged[i,j] = new Container(grid[i, j], 12, 13*selectedContainer.size, 0, graphicsDevice,biomeRef);
                                
                            }
                        }

                    }
                    else
                    {
                        grid[i, j].hoover = false;
                    }
                }
            }*/
        }
        public void Render(SpriteBatch sb, SpriteFont font)
        {
            sb.Draw(mapTexture,boundingRect,Color.White);
            /*
            for (int i = 0; i < gridAmount; i++)
            {
                for (int j = 1; j < gridAmount; j++)
                {

                    if(grid[i, j].hoover) sb.Draw(gridTexture, grid[i, j].boundingRect, Color.Black * 0.5f);
                    else sb.Draw(gridTexture, grid[i,j].boundingRect, Color.Black*0.15f);
                }
            }
            if (selectedContainer != null) 
            {
                area.Render(sb);
                //sb.Draw(enlarged[selectedContainer.x/ selectedContainer.size, selectedContainer.y /selectedContainer.size].zoom, enlarged[selectedContainer.x / selectedContainer.size, selectedContainer.y / selectedContainer.size].boundingRect, Color.White);
                sb.Draw(gridTexture, selectedContainer.boundingRect, Color.Red);
                //sb.DrawString(font, selectedContainer.toString(), new Vector2(0, 0), Color.Red);
            }*/
        }
        public Texture2D getTexture(GraphicsDevice graphicsDevice)
        {
            Color[] data = new Color[res * res];
            Texture2D texture = new Texture2D(graphicsDevice, res, res);
            for (int i = 0; i < res; i++)
            {
                for (int j = 0; j < res; j++)
                {
                    data[i + res * j] = biomes[i,j].GetColor();
                    //data[i + size * j] = 
                }
            }
            texture.SetData(data);
            return texture;
        }
        

        /*private double DistanceToWater(int x, int y, double waterlevel, int resolution)
        {
            int cX = x;
            int cY = y;
            bool[,] visited = new bool[resolution, resolution];
            bool[,] queued = new bool[resolution, resolution];
            Queue<int> xQueue = new Queue<int>();
            Queue<int> yQueue = new Queue<int>();
            
            while (true)
            {
                visited[cX, cY] = true;
                for(int i = cX - 1; i < cX + 2; i++)
                {
                    for (int j = cY - 1; j < cY + 2; j++)
                    {
                        if (i >= resolution || i < 0 || j >= resolution || j < 0 || visited[i, j] || queued[i, j]) continue;
                        if(heightPoints[i,j]<=0.5) return Distance2D(x, cX, y, cY);
                        xQueue.Enqueue(i);
                        yQueue.Enqueue(j);
                        queued[i, j] = true;
                    }
                }
                cX = xQueue.Dequeue();
                cY = yQueue.Dequeue();

            }
            
        }*/
        public double Distance2D(int x1, int x2,int y1, int y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
        private double[,] SeaGradient(int resolution, double[,] heightPoints)
        {
            double[,] filter = new double[resolution, resolution];
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    double value;
                    if (heightPoints[i, j] > 0.5) value = Math.Pow((heightPoints[i, j]-0.5)/0.5,2);// DistanceToWater(i, j, 0.5, resolution);
                    else value = 0.0;
                    filter[i, j] = value;
                }
            }
            return filter;
        }

        private double[,] VecticalGradient(int resolution, int midpoint)
        {
            double[,] filter = new double[resolution, resolution];
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    double distance = Math.Abs(midpoint-j);
                    if (distance <= 0.80 * resolution)
                    {
                        filter[i, j] = 1.0 - (distance / (0.80 * resolution));
                    }
                    else
                    {
                        filter[i, j] = 0.0;
                    }
                }
            }
            return filter;
        }
        private double[,] CircleGradient(int resolution)
        {
            double[,] filter = new double[resolution, resolution];
            for (int i = 0; i < resolution; i++)
            {
                for (int j = 0; j < resolution; j++)
                {
                    double distance = Math.Abs(Math.Sqrt(Math.Pow(resolution/2- 1 - i, 2) + Math.Pow(resolution/2 - 1 - j, 2)));
                    if (distance <=0.9*resolution)
                    {
                        filter[i, j] = 1.0-(distance / (0.9*resolution));
                    }
                    else
                    {
                        filter[i, j] = 0.0;
                    }
                }
            }
            return filter;
        }

        public double Normalize(double value, double lowest, double highest)
        {
            return (value - lowest) / (highest - lowest);
        }

        public Vector2D GetConstantVector(int v)
        {
            int h = v & 3;
            if (h == 0)
                return new Vector2D(1.0, 1.0);
            else if (h == 1)
                return new Vector2D(-1.0, 1.0);
            else if (h == 2)
                return new Vector2D(-1.0, -1.0);
            else
                return new Vector2D(1.0, -1.0);
        }
        public double GetValueOfPoint(int x, int y, int octave, int resolution, Vector2D[,] constants)
        {
            double total = 0.0;
            double amplitude = 1.0;
            double frequency = ((size-2) / resolution);
            double xPos;
            double yPos;
            double maxValue=0;
            for (int i=0; i < octave; i++)
            {
                /*xPos = (frequency / 2.0) + x * frequency;
                yPos = (frequency / 2.0) + y * frequency;*/
                xPos =  (x+1) * frequency;
                yPos =  (y+1) * frequency;
                total += (amplitude * PerlinNoise(xPos, yPos, constants));
                maxValue += amplitude;
                amplitude *= 0.5;
                frequency *= 2.0;
            }
            
            return total/maxValue;
        }
        
        public Vector2D[,] GenerateConstants(int size)
        {
            double value;
            Vector2D[,] constants = new Vector2D[size+1, size+1];
            for (int i = 0; i < size+1; i++)
            {
                for (int j = 0; j < size+1; j++)
                {

                    value = rgen.Next(3);
                    //value = rgen.NextDouble() * 2 * Math.PI;

                    constants[i, j] = GetConstantVector((int)value);// new Vector2(Math.Cos(value) , Math.Sin(value)); //* (rgen.NextDouble() * 10));//GetConstantVector((int)value);
                    
                    
                }
            }
            for(int i =0; i<size; i++)
            {
                constants[size, i] = constants[0, i];
            }
            for (int i = 0; i < size+1; i++)
            {
                constants[i,size] = constants[i, 0];
            }
            return constants;
        }
        private double Fade(double t)
        {
            return ((6 * t - 15) * t + 10) * t * t * t;
        }
        private double Lerp(double t, double a1, double a2)
        {
            return a1 + t * (a2 - a1);
        }
        public double PerlinNoise(double x, double y, Vector2D[,] constants)
        {
            x = x % size;
            y = y % size;
            double X = Math.Floor(x);
            double Y = Math.Floor(y);
            double xf, yf, dotTopLeft, dotTopRight, dotBotLeft, dotBotRight, u, v;

            xf = x - X;
            yf = y - Y;
            Vector2D topLeft = new Vector2D(xf, yf - 1.0);
            Vector2D topRight = new Vector2D(xf - 1.0, yf - 1.0);
            Vector2D botLeft = new Vector2D(xf, yf);
            Vector2D botRight = new Vector2D(xf - 1.0, yf);

            dotTopLeft = topLeft.dot(constants[(int)X, (int)Y+1]);
            dotTopRight = topRight.dot(constants[((int)X)+1, (int)Y+1]);
            dotBotLeft = botLeft.dot(constants[(int)X, ((int)Y)]);
            dotBotRight = botRight.dot(constants[((int)X)+1, ((int)Y)]);
            u = Fade(xf);
            v = Fade(yf);

            double noiseValue= Lerp(u, Lerp(v, dotBotLeft, dotTopLeft), Lerp(v, dotBotRight, dotTopRight));
            return (noiseValue+1)/2;
            
            
        }
    }
}

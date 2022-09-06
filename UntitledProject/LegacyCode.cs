using Microsoft.Xna.Framework;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace UntitledProject
{

    /*
     * 
     * 
     * 
        public Texture2D test(GraphicsDevice gd)
        {
            
            Point tempres = new Point(resolution.X, resolution.Y);
            heightLocal = TheNewWorld.GeneratePerlinValuesV2(mapSection, tempres, height, 6, 0.6f, heightMinMax[0], heightMinMax[1],topology.getFilter(mapSection, tempres));
            float[,] temporaryfilter = topology.getFilter(heightLocal, 0.5, 3, true);
            fertilityLocal = TheNewWorld.GeneratePerlinValuesV2(mapSection, tempres, fertility, 2, 1f, fertMinMax[0], fertMinMax[1], temporaryfilter);
            temporaryfilter = topology.getFilter(mapSection,heightLocal, 0.5, 2, true,equator);
            temperatureLocal = TheNewWorld.GeneratePerlinValuesV2(mapSection, tempres, temperature, 4, 0.75f, tempMinMax[0], tempMinMax[1], temporaryfilter);

            Color[] data = scaleColor(resolution, tempres);
            Texture2D texture = new Texture2D(gd,resolution.X,resolution.Y);
            texture.SetData(data);
            return texture;
        }
     * public static Texture2D SupplyVisualisation(byte[,] values, GraphicsDevice gd)
        {
            Texture2D t = new Texture2D(gd, values.GetLength(0), values.GetLength(1));
            Color[] data = new Color[values.GetLength(0) * values.GetLength(1)];
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    if(values[i, j] <= 128)
                    {
                        data[i + values.GetLength(0) * j] = Color.Blue;
                        continue;
                    }
                    data[i + values.GetLength(0) * j] = Color.Lerp(Color.Black,Color.White,(float)values[i,j]/255.0f);
                    
                }
            }
            t.SetData(data);
            return t;
        }
     *         private static byte[,] Normalize(byte[,] values)
        {
            byte highest = 0;
            byte lowest = 255;
            for (int x = 0; x < values.GetLength(0); x++)
            {
                for (int y = 0; y < values.GetLength(1); y++)
                {
                    if (values[x, y] > highest) highest = values[x, y];
                    if (values[x, y] < lowest) lowest = values[x, y];
                }
            }
            for (int x = 0; x < values.GetLength(0); x++)
            {
                for (int y = 0; y < values.GetLength(1); y++)
                {

                    values[x, y] = (byte)Math.Round(((values[x, y] - lowest) / (double)(highest - lowest)) * 255);
                }
            }
            return values;
        }
     * 
     * 
     * public static byte[,] OctavesV2(byte[,] data, double persistance, int octaves)
        {
            double total, maxValue, frequency;
            int X, Y;
            byte[,] values = new byte[data.GetLength(0), data.GetLength(1)];
            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    total = data[x, y];
                    frequency = 2.0;
                    maxValue = 255.0;
                    for (int i = 1; i < octaves; i++)
                    {
                        X = (int)Math.Round((x * frequency) % data.GetLength(0));
                        Y = (int)Math.Round((y * frequency) % data.GetLength(1));
                        total += data[X, Y] * Math.Pow(persistance, i);
                        maxValue += 255.0 * Math.Pow(persistance, i);
                        frequency *= 2.0;
                    }
                    values[x, y] = (byte)Math.Round((total / maxValue) * 255.0);
                }
            }

            return Normalize(values);
        }
     *         public static byte[,] GeneratePerlinValues(Point size,float[,][] constants )
        {
            float x, y, xf, yf, u, v, floatvalue;
            int X, Y;
            byte[,] values = new byte[size.X, size.Y];
            float[] topLeft, botLeft , topRight, botRight;
            for (int i = 0; i < size.X; i++)
            {
                x=((constants.GetLength(0) - 1) / (float)size.X) * i;
                X = (int)Math.Floor(x - x % 1.0f);
                xf = x - X;
                u = Fade(xf);
                for (int j = 0; j < size.Y; j++)
                {
                    y = ((constants.GetLength(1) - 1) / (float)size.Y) * j;
                    Y = (int)Math.Floor(y - y % 1.0f);
                    yf = y - Y;
                    v = Fade(yf);

                    topLeft =new float[]{ xf, yf };
                    botLeft = new float[] { xf, yf - 1.0f };
                    topRight = new float[] { xf - 1.0f, yf };
                    botRight = new float[] { xf - 1.0f, yf - 1.0f };
                    
                    floatvalue=Lerp(u, Lerp(v, dot(topLeft, constants[X,Y]), dot(botLeft, constants[X, Y + 1])), Lerp(v, dot(topRight, constants[X + 1, Y]), dot(botRight, constants[X + 1, Y + 1])));

                    values[i,j]=(byte)Math.Round(((floatvalue + 1.0f) / 2.0f)*255.0f);
                }
            }
            return values;
        }
        public static byte[,] GeneratePerlinValues(Rectangle size, float[,][] constants)
        {
            float x, y, xf, yf, u, v, floatvalue;
            int X, Y;
            byte[,] values = new byte[size.Width, size.Height];
            float[] topLeft, botLeft, topRight, botRight;
            for (int i = 0; i < size.Width; i++)
            {
                x = ((constants.GetLength(0) - 1) / (float)size.X) * i;
                X = (int)Math.Floor(x - x % 1.0f);
                xf = x - X;
                u = Fade(xf);
                for (int j = 0; j < size.Height; j++)
                {
                    y = ((constants.GetLength(1) - 1) / (float)size.Y) * j;
                    Y = (int)Math.Floor(y - y % 1.0f);
                    yf = y - Y;
                    v = Fade(yf);

                    topLeft = new float[] { xf, yf };
                    botLeft = new float[] { xf, yf - 1.0f };
                    topRight = new float[] { xf - 1.0f, yf };
                    botRight = new float[] { xf - 1.0f, yf - 1.0f };

                    floatvalue = Lerp(u, Lerp(v, dot(topLeft, constants[X, Y]), dot(botLeft, constants[X, Y + 1])), Lerp(v, dot(topRight, constants[X + 1, Y]), dot(botRight, constants[X + 1, Y + 1])));

                    values[i, j] = (byte)Math.Round(((floatvalue + 1.0f) / 2.0f) * 255.0f);
                }
            }
            return values;
        }
     * 
     * public static void help()
        {
            //double[] temp = new double[5] { 6967.7, 6996.0, 6987.2, 5740.1, 5763.3}; //1
            //double[] temp = new double[5] {13228.6,13997.1,11475.1,11471.0,11467.9 }; //2
            //double[] temp = new double[5] {23909.4, 22802.1, 23381.3, 22779.7, 22772.7 }; //4
            //double[] temp = new double[5] { 36909.5,37087.0,37522.4,37470.1,37480.5}; //8
            //double[] temp = new double[5] { 40436.8, 40672.0,40517.3,40300.8,40995.0 }; //12
            //double[] temp = new double[5] { 40763.4, 40975.0, 41015.1, 41300.3, 41130.7 };//16
            //double[] temp = new double[5] { 41290.1, 41067.8, 41120.6, 41163.5, 41163.5 };//20
            //double[] temp = new double[5] {41755.1, 43338.0, 41679.9, 42676.5, 43919.4};//24
            //double[] temp = new double[5] { 42977.2,42714.6,41374.1,41484.1,41333.4 }; //28
            //double[] temp = new double[5] { 45767.5,41140.8,44930.9,50663.5,45389.8 };//32
            //double[] temp = new double[5] { 54573.4,40910.1,40940.0,45185.1,40975.0, };//guided
            //double[] temp = new double[5] { 236.3,222.8,266.0,246.1,215.9 };//dynamic
            double[] temp = new double[5] { 40475.8,42955.2,40721.4,45121.3,52066.8 };//static
            double placeholder = 0;
            double placeholder2 = 0;
            foreach (double temp1 in temp)
            {
                placeholder += temp1;
            }
            placeholder= placeholder /5;
            foreach (double temp1 in temp)
            {
                placeholder2 += Math.Abs(temp1-placeholder);
            }
            placeholder2 = placeholder2 / 5;

        }
     * 
     *         public Biome[,] Scale(Biome[,] biomes, double originalDist, double interpolationDist, int[,,] biomeRef)
        {
            Biome[,] scaledVers = new Biome[(int)(1.0 / interpolationDist) + 1, (int)(1.0 / interpolationDist) + 1];
            for(double i = 0.0; i <= 1.0; i += interpolationDist)
            {
                for (double j = 0.0; j <= 1.0; j += interpolationDist)
                {
                    int x = (int) ((i - i % originalDist) / originalDist);
                    int y = (int) ((j - j % originalDist) / originalDist);
                    Biome tempx1 = biomes[x, y].Lerp(biomes[x+1, y], (i % originalDist) / originalDist, biomeRef);
                    Biome tempx2 = biomes[x, y+1].Lerp(biomes[x + 1, y+1], (i % originalDist) / originalDist, biomeRef);
                    
                    scaledVers[(int)Math.Round(i / interpolationDist), (int)Math.Round(j / interpolationDist)] = tempx1.Lerp(tempx2, (j % originalDist) / originalDist, biomeRef);
                }
            }
            return scaledVers;
        }
    private Color[] scaleColor(Biome[,] biomes,int targetSize)
        {
            Color[] color = new Color[targetSize*targetSize];
            int size = biomes.GetLength(0);
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int a = 0; a < targetSize/size; a++)
                    {
                        for (int b = 0; b < targetSize / size; b++)
                        {
                            color[(i * (targetSize / size) + a) + targetSize * (j * (targetSize / size) + b)] = biomes[i, j].GetColor();
                        }
                    }
                }
            }
            return color;
        }
     * public void AssignGradients(Biome[,] biomes)
        {
     int heightZone, bioZone, tempZone;

            


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
            if (heightZone >= 4) return Color.Blue;
            if (tree) return Color.Sienna;
            if (IsCliff()) return Color.Gray;
            if (tempZone==0)
            {
                return Color.White;
            }
            if (bioZone == 0)
            {
                return Color.Orange;
            }
            else
            {
                if (tempZone == 1)
                {
                    if (bioZone >= 2) return Color.SeaGreen;
                    return Color.LightGreen;
                }
                else if (tempZone == 2)
                {
                    if(bioZone>=2) return Color.ForestGreen;
                    return Color.DarkKhaki;
                }
                else if(tempZone == 3)
                {
                    if(bioZone>=2) return Color.DarkGreen;
                    return Color.GreenYellow;
                }
                else 
                {
                    if (bioZone >= 3) return Color.DarkGreen;
                    return Color.Goldenrod;
                }
            }
            gradients = new double[1,4];
            //angles = new double[1, 8];
            angles = new double[3, 3];
            gradients[0, 0]=height-Lerp(0.5, biomes[0,0].height, biomes[2, 2].height);
            gradients[0, 1] = height - Lerp(0.5, biomes[0, 2].height, biomes[2, 0].height);
            gradients[0, 2] = height - Lerp(0.5, biomes[0, 1].height, biomes[2, 1].height);
            gradients[0, 3] = height - Lerp(0.5, biomes[1, 0].height, biomes[1, 2].height);

            angles[1, 0] = Math.Atan((biomes[1, 0].height - height) * 150.0) * (180 / Math.PI);
            angles[2, 0] = Math.Atan(150.0 * (biomes[2, 0].height - height) / Math.Sqrt(2)) * (180 / Math.PI);
            angles[2, 1] = Math.Atan((biomes[2, 1].height - height) * 150.0) * (180 / Math.PI);
            angles[2, 2] = Math.Atan(150.0 * (biomes[2, 2].height - height) / Math.Sqrt(2)) * (180 / Math.PI);
            angles[1, 2] = Math.Atan((biomes[1, 2].height - height) * 150.0) * (180 / Math.PI);
            angles[0, 2] = Math.Atan(150.0 * (biomes[0, 2].height - height) / Math.Sqrt(2)) * (180 / Math.PI);
            angles[0, 1] = Math.Atan((biomes[0, 1].height - height) * 150.0) * (180 / Math.PI);
            angles[0, 0] = Math.Atan(150.0 * (biomes[0, 0].height - height) / Math.Sqrt(2)) * (180 / Math.PI);
            /*
            angles[0, 0] = Math.Atan((biomes[1,0].height-height) * 150.0) * (180 / Math.PI);
            angles[0, 1] = Math.Atan(150.0 * (biomes[2, 0].height - height)/Math.Sqrt(2)) * (180 / Math.PI);
            angles[0, 2] = Math.Atan((biomes[2, 1].height - height) * 150.0) * (180 / Math.PI);
            angles[0, 3] = Math.Atan(150.0 * (biomes[2, 2].height - height) / Math.Sqrt(2)) * (180 / Math.PI);
            angles[0, 4] = Math.Atan((biomes[1, 2].height - height) * 150.0) * (180 / Math.PI);
            angles[0, 5] = Math.Atan(150.0 * (biomes[0, 2].height - height) / Math.Sqrt(2)) * (180 / Math.PI);
            angles[0, 6] = Math.Atan((biomes[0, 1].height - height) * 150.0) * (180 / Math.PI);
            angles[0, 7] = Math.Atan(150.0 * (biomes[0, 0].height - height) / Math.Sqrt(2)) * (180 / Math.PI);
            /*
            gradients[1, 0] = humidity - Lerp(0.5, biomes[0, 0].humidity, biomes[2, 2].humidity);
            gradients[1, 1] = humidity - Lerp(0.5, biomes[0, 2].humidity, biomes[2, 0].humidity);
            gradients[1, 2] = humidity - Lerp(0.5, biomes[0, 1].humidity, biomes[2, 1].humidity);
            gradients[1, 3] = humidity - Lerp(0.5, biomes[1, 0].humidity, biomes[1, 2].humidity);

            gradients[2, 0] = temperature - Lerp(0.5, biomes[0, 0].temperature, biomes[2, 2].temperature);
            gradients[2, 1] = temperature - Lerp(0.5, biomes[0, 2].temperature, biomes[2, 0].temperature);
            gradients[2, 2] = temperature - Lerp(0.5, biomes[0, 1].temperature, biomes[2, 1].temperature);
            gradients[2, 3] = temperature - Lerp(0.5, biomes[1, 0].temperature, biomes[1, 2].temperature);

            gradients[3, 0] = magic - Lerp(0.5, biomes[0, 0].magic, biomes[2, 2].magic);
            gradients[3, 1] = magic - Lerp(0.5, biomes[0, 2].magic, biomes[2, 0].magic);
            gradients[3, 2] = magic - Lerp(0.5, biomes[0, 1].magic, biomes[2, 1].magic);
            gradients[3, 3] = magic - Lerp(0.5, biomes[1, 0].magic, biomes[1, 2].magic);
            
}
private Color[] DisplayTiles(int targetSize)
        {
            Color[] color = new Color[targetSize * targetSize];
            int size = tiles.GetLength(0);
            Color[,] colorTemp = new Color[size*3,size*3];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int a = 0; a < 3; a++)
                    {
                        for (int b = 0; b < 3; b++)
                        {
                            colorTemp[i*3+a,j*3+b] = tiles[i, j].colorTerrain[a, b];
                        }
                    }
                }
            }
            color= scaleColor2(colorTemp, targetSize);
            return color;
        }

        private Color[] scaleColor2(Color[,] color2, int targetSize)
        {
            Color[] color = new Color[targetSize * targetSize];
            int size = color2.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int a = 0; a < targetSize / size; a++)
                    {
                        for (int b = 0; b < targetSize / size; b++)
                        {
                            color[(i * (targetSize / size) + a) + targetSize * (j * (targetSize / size) + b)] = color2[i, j];
                        }
                    }
                }
            }
            return color;
        }*/
    class Hex
    {
        double s = 1 / Math.Sqrt(3);
        double h = 1.0 - (1.0 / Math.Sqrt(3.0));
        double r = 0.5;


        double TriArea(double x1, double y1, double x2,
                       double y2, double x3, double y3)
        {
            return Math.Abs((x1 * (y2 - y3) +
                             x2 * (y3 - y1) +
                             x3 * (y1 - y2)) / 2.0);
        }
        bool IsInsideTri(double x1, double y1, double x2,
                         double y2, double x3, double y3,
                         double x, double y)
        {
            /* Calculate area of triangle ABC */
            double A = TriArea(x1, y1, x2, y2, x3, y3);

            /* Calculate area of triangle PBC */
            double A1 = TriArea(x, y, x2, y2, x3, y3);

            /* Calculate area of triangle PAC */
            double A2 = TriArea(x1, y1, x, y, x3, y3);

            /* Calculate area of triangle PAB */
            double A3 = TriArea(x1, y1, x2, y2, x, y);

            /* Check if sum of A1, A2 and A3 is same as A */
            return (A == A1 + A2 + A3);
        }
        //takes index pos of hex and returns all 6 corner points (starts at top and goes clockwise)
        double[,] HexCornerns(int hexX, int hexY)
        {
            double[,] corners = new double[6, 2];
            if (hexY % 2 == 0)
            {
                corners[0, 0] = (double)hexX + 0.5;
                corners[0, 1] = (double)hexY;

                corners[1, 0] = (double)hexX + 1.0;
                corners[1, 1] = (double)hexY + h;

                corners[2, 0] = (double)hexX + 1.0;
                corners[2, 1] = (double)hexY + 1.0;

                corners[3, 0] = (double)hexX + 0.5;
                corners[3, 1] = (double)hexY + 1.0 + h;

                corners[4, 0] = (double)hexX;
                corners[4, 1] = (double)hexY + 1.0;

                corners[5, 0] = (double)hexX;
                corners[5, 1] = (double)hexY + h;
            }
            else
            {
                corners[0, 0] = (double)hexX + 1.0;
                corners[0, 1] = (double)hexY;

                corners[1, 0] = (double)hexX + 1.5;
                corners[1, 1] = (double)hexY + h;

                corners[2, 0] = (double)hexX + 1.5;
                corners[2, 1] = (double)hexY + 1.0;

                corners[3, 0] = (double)hexX + 1.0;
                corners[3, 1] = (double)hexY + 1.0 + h;

                corners[4, 0] = (double)hexX + 0.5;
                corners[4, 1] = (double)hexY + 1.0;

                corners[5, 0] = (double)hexX + 0.5;
                corners[5, 1] = (double)hexY + h;
            }
            return corners;
        }

        //given a float point, returns index of hex
        int[] CoordToHexPos(double x, double y)
        {
            double X = Math.Floor(x);
            double Y = Math.Floor(y);
            if (Y % 2 == 0)
            {

                if (IsInsideTri(X, Y, X + 0.5, Y, X, Y + h, x, y))
                {
                    return new int[] { (int)X - 1, (int)Y - 1 };
                }
                else if (IsInsideTri(X + 0.5, Y, X + 1, Y, X + 1, Y + h, x, y))
                {
                    return new int[] { (int)X, (int)Y - 1 };
                }
                else
                {
                    return new int[] { (int)X, (int)Y };
                }
            }
            else
            {
                if (IsInsideTri(X, Y, X + 0.5, Y + h, X + 1.0, Y, x, y))
                {
                    return new int[] { (int)X, (int)Y - 1 };
                }
                else if (IsInsideTri(X, Y, X, Y + h, X + 0.5, Y + h, x, y) || ((x >= X && x <= (X + 0.5)) && (y >= Y + h && y < Y + 1)))
                {
                    return new int[] { (int)X - 1, (int)Y };
                }
                else
                {
                    return new int[] { (int)X, (int)Y };
                }
            }

        }
    }
    struct OverCell
    {
        int biome, height;
        Color color;

        //biome:[0=sea, 1=land, 2=river]
        public OverCell(int biome, int height)
        {
            this.biome = biome;
            this.height = height;
            if (biome == 0) color = Color.DarkBlue;
            else if (biome == 1) color = Color.Black;
            else if (biome == 2) color = Color.Blue;
            else color = Color.White;
        }
    }
    class Map
    {
        public int[,] map;
        Random rgen = new Random();
        int waterlevel=2;
        public List<int[]> sea;
        int highestlvl;
        public List<int[]> path;
        public Map()
        {
            generateMap();
            sea=generateRivers();
            //sea = aStar(0, 0, 99, 89);
            //List<int[]>[] layers = allLayers();
            
            /*path = findPath(99,99,0,0);
            path=riverRouteFind(layers[highestlvl][0]);
            System.Diagnostics.Debug.WriteLine(path[0][0] + ", " + path[0][1]);
            System.Diagnostics.Debug.WriteLine(path[path.Count - 1][0] + ", " + path[path.Count - 1][1]);
            Stack<int[]> test = findPath(layers[highestlvl][0][0], layers[highestlvl][0][1],bfs(0, layers[highestlvl][0])[0], bfs(0, layers[highestlvl][0])[1]);
            //sea.Clear();
            foreach (int[] coord in test)
            {
                sea.Add(coord);
                System.Diagnostics.Debug.WriteLine(coord[0] + ", " + coord[1]);
                System.Diagnostics.Debug.WriteLine(map[coord[0], coord[1]]);
            }*/
            //sea = riverRouteFind(layers[highestlvl][0]);
            /*path=riverRouteFind(layers[highestlvl][0]);
            foreach (int[] coord in path)
            {
                System.Diagnostics.Debug.WriteLine(coord[0] + ", " + coord[1]);
                System.Diagnostics.Debug.WriteLine(map[coord[0], coord[1]]);
            }
            System.Diagnostics.Debug.WriteLine("ding");
            path = riverPath(path[0], path[path.Count-1]);
            foreach (int[] coord in path)
            {
                System.Diagnostics.Debug.WriteLine(coord[0] + ", " + coord[1]);
                System.Diagnostics.Debug.WriteLine(map[coord[0], coord[1]]);
            }
            System.Diagnostics.Debug.WriteLine("dong");*/

        }

        public bool isIn(int[] target, List<int[]> list)
        {
            foreach(int[] possible in list)
            {
                if (possible[0] == target[0] && possible[1] == target[1]) return true;
            }
            return false;
        }

        public List<int[]>[] allLayers()
        {
            List<int[]>[] layers = new List<int[]>[11];
            highestlvl=10;
            for (int i=0;i<11;i++)
            {
                layers[i] = new List<int[]>();
            }
            for (int col = 0; col < map.GetLength(0); col++)
            {
                for (int row = 0; row < map.GetLength(1); row++)
                {
                    layers[map[col, row]].Add(new int[] { col, row });
                }
            }
            for (int i = 10; i >= 0; i--)
            {
                if (!layers[i].Any()) highestlvl -= 1;
                else
                {
                    break;
                }
            }
            //System.Diagnostics.Debug.WriteLine(highestlvl);
            return layers;
        }


        public bool passable(int type,int[] start, int[] goal)
        {
            //type of entity:river=0, mountaintop=1
            if (type == 0)
            {
                if (map[start[0], start[1]] < map[goal[0], goal[1]] )
                    return false;
            }
            else if (type == 1)
            {
                if (map[start[0], start[1]] < map[goal[0], goal[1]] || map[start[0], start[1]] > map[goal[0], goal[1]])
                    return false;
            }
            else if (type == 2)
            {
                if (map[start[0], start[1]] - map[goal[0], goal[1]]>1 || map[start[0], start[1]] - map[goal[0], goal[1]] < -1)
                    return false;
            }
            return true; 
        }
        /*public struct node
        {
            int x, y,parentX, parentY,fCost,gCost,hCost;
            //public int[];
            return null;
        }*/
        private int lowestFindex(List<int[]> list)
        {
            int index = -1;
            int lowestValue=int.MaxValue;
            for (int i =0;i<list.Count;i++)
            {
                if (list[i][2] < lowestValue) 
                {
                    lowestValue = list[i][2];
                    index = i;
                }

            }
            return index;
        }
        private bool lowerGValue(int[] current, List<int[]> list)
        {
            foreach(int[] temp in list)
            {
                if (current[0] == temp[0] && current[1] == temp[1] && current[3] <= temp[3]) return true;
            }
            return false;
        }
        public int indexIn(int[] target, List<int[]> list)
        {
            int i = 0;
            foreach (int[] possible in list)
            {
                if (possible[0] == target[0] && possible[1] == target[1]) return i;
                i++;
            }
            return -1;
        }
        public List<int[]> aStar(int sx, int sy, int gx, int gy)
        {

            List<int[]> neighbors;
            //every entry in open and closed like this [0x, 1y, 2f, 3g, 4h, 5px, 6py]
            List<int[]> open = new List<int[]>();
            List<int[]> closed = new List<int[]>();
            int currentIndex, fcost, gcost, hcost, Oindex, Cindex;
            int[] current;
            open.Add(new int[] { sx, sy, 0, 0, cost(new int[] { sx, sy }, new int[] { gx, gy }), 0, 0 });
            //cost(new int[] { path.Peek()[0], path.Peek()[1] }, new int[] { current[0], current[1] });
            while (open.Count != 0)
            {
                //int[] temp2 = new int[] { neighbor[0], neighbor[1], cost(current, neighbor) + current[3] + cost(neighbor, new int[] { gx, gy }), cost(current, neighbor) + current[3], cost(neighbor, new int[] { gx, gy }), current[0] ,current[1] };
                currentIndex = lowestFindex(open);
                current = open[currentIndex];
                open.RemoveAt(currentIndex);
                if(current[0]==gx && current[1] == gy)
                {
                    closed.Add(current);
                    break;
                }
                neighbors = neighborSearch(current[0], current[1]);
                foreach(int[] neighbor in neighbors)
                {
                    if (!passable(0, current, neighbor)) continue;
                    gcost = current[3] + cost(current, neighbor);
                    Oindex = indexIn(neighbor, open);
                    Cindex = indexIn(neighbor, closed);
                    if (Oindex!=-1)
                    {
                        if (open[Oindex][3] <= gcost) continue;
                    }
                    else if (Cindex != -1)
                    {
                        if (closed[Cindex][3] <= gcost) continue;
                        open.Add(closed[Cindex]);
                        closed.RemoveAt(Cindex);
                    }
                    else
                    {
                        hcost = cost(neighbor, new int[] { gx, gy });
                        fcost = hcost + gcost;
                        open.Add(new int[] {neighbor[0], neighbor[1], fcost, gcost, hcost, current[0], current[1]});
                    }
                }
                closed.Add(current);

            }
            List<int[]> path = new List<int[]>();
            current = closed[closed.Count - 1];
            while (current[0]!=sx && current[1]!=sy)
            {
                path.Add(new int[] {current[0], current[1]});
                current = closed[indexIn(new int[] { current[5], current[6] },closed)];
            }
            return path;
        }
        public Stack<int[]> findPath(int sx, int sy, int gx, int gy)
        {
            int[] current = new int[] {sx, sy};
            List<int[]> neighbors;
            List<int[]> visited = new List<int[]>();
            Stack<int[]> path = new Stack<int[]>();
            int[] lowestCostCoord = new int[] { 0, 0 };
            int pathCost, lowestCost, lowestGCost, gCost, hCost,previousCost;
            pathCost = 0;
            
            
            while(true)
            {
                if (current[0] == gx && current[1] == gy) 
                {
                    path.Push(new int[] { current[0], current[1] });
                    break; 
                }
                

                lowestCost = int.MaxValue;
                lowestGCost = int.MaxValue;
                neighbors = neighborSearch(current[0], current[1]);
                foreach (int[] neighbor in neighbors)
                {
                    if (!passable(0, current, neighbor)) continue;

                    else if (isIn(neighbor, visited)) continue;

                    gCost = pathCost + cost(current, neighbor);
                    hCost = cost(neighbor, new int[] { gx, gy });


                    if (gCost + hCost == lowestCost)
                    {
                        if (gCost<lowestGCost)
                        {
                            lowestGCost = gCost;
                            lowestCostCoord = new int[] { neighbor[0], neighbor[1] };
                        }
                        else
                        {
                            continue;
                        }
                    }

                    else if (gCost + hCost < lowestCost)
                    {
                        lowestCost = gCost + hCost;
                        lowestGCost = gCost;
                        lowestCostCoord = new int[] {neighbor[0], neighbor[1]};
                    }
                    
                }
                if (lowestCost == int.MaxValue)
                {

                    visited.Add(new int[] { current[0], current[1] });
                    pathCost -= cost(new int[] { path.Peek()[0], path.Peek()[1] }, new int[] { current[0], current[1] });
                    current = new int[] { path.Peek()[0], path.Peek()[1] };
                    path.Pop();
                }

                else
                {
                    pathCost = lowestGCost;
                    visited.Add(new int[] { current[0], current[1] });
                    path.Push(new int[] { current[0], current[1] });
                    current = new int[] { lowestCostCoord[0], lowestCostCoord[1] };
                }
            }
            return path;
        }

        private int cost(int[] start, int[] goal)
        {
            return (int) Math.Sqrt(Math.Pow((double)start[0] - (double)goal[0], 2) + Math.Pow((double)start[1] - (double)goal[1], 2))*10;
        }
        /*public List<int[]> neighborLevel(int xCoord, int yCoord)
        {
            List<int[]> list = new List<int[]>();

            for (int col = xCoord - 1; col <= xCoord + 1; col++)
            {
                for (int row = yCoord - 1; row <= yCoord + 1; row++)
                {
                    if (col < 0 || col > 99) continue;
                    if (row < 0 || row > 99) continue;
                    if (col == xCoord && row == yCoord) continue;
                    list.Add(new int[2] { col, row });
                }
            }
            return false;
        }*/
        public List<int[]> fillLayer(int[] start)
        {

            Queue<int[]> queue = new Queue<int[]>();
            List<int[]> visited = new List<int[]>();
            List<int[]> list;
            int[] tracker;
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                tracker = new int[] { queue.Peek()[0], queue.Peek()[1] };
                queue.Dequeue();

                if (isIn(tracker, visited))
                {
                    continue;
                }
                else
                {

                    visited.Add(new int[] { tracker[0], tracker[1] });
                    list = neighborSearch(tracker[0], tracker[1]);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (isIn(list[i], visited) || !passable(1, tracker, list[i]))
                        {
                            continue;
                        }
                        else
                        {
                            queue.Enqueue(list[i]);
                        }
                    }
                }
            }
            return visited;
        }

        public List<int[]> fillLake(int[] start)
        {
            
            Queue<int[]> queue = new Queue<int[]>();
            List<int[]> visited = new List<int[]>();
            List<int[]> list;
            int[] tracker;
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                tracker = new int[] { queue.Peek()[0], queue.Peek()[1] };
                queue.Dequeue();

                if (isIn(tracker, visited) )
                {
                    continue;
                }
                else
                {

                    visited.Add(new int[] { tracker[0], tracker[1] });
                    list = neighborSearch(tracker[0], tracker[1]);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (isIn(list[i], visited)|| !passable(0,tracker, list[i]))
                        {
                            continue;
                        }
                        else
                        {
                            queue.Enqueue(list[i]);
                        }
                    }
                }
            }
            return visited;
        }

        public bool approvedLake(List<int[]> lake, int[] riversSource, int cost1)
        {
            if (lake.Count < 5) return false;
            foreach(int[] coord in lake)
            {
                if (cost(new int[] {coord[0], coord[1]}, new int[] { riversSource[0], riversSource[1] }) < cost1) return false;
            }
            return true;
        }
        public int[] bfs(int level, int[] start)
        {
            List<int[]> visited = new List<int[]>();
            Queue<int[]> queue = new Queue<int[]>();
            List<int[]> list, lake;
            int[] tracker;
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                tracker = new int[] { queue.Peek()[0], queue.Peek()[1] };
                queue.Dequeue();

                if (map[tracker[0], tracker[1]] == level)
                {
                    lake = fillLake(tracker);
                    if (approvedLake(lake, start,50))
                    {
                        return tracker;
                    }
                    else
                    {
                        visited.AddRange(lake);
                    }
                    //return tracker;

                }
                
                if (isIn(tracker, visited))
                {
                    continue;
                }
                else
                {
                    
                    visited.Add(new int[] { tracker[0], tracker[1] });
                    list = neighborSearch(tracker[0], tracker[1]);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (isIn(list[i], visited) || !passable(0, tracker, list[i]))
                        {
                            continue;
                        }
                        else if (isIn(list[i], sea)) continue;
                        else
                        {
                            queue.Enqueue(list[i]);
                        }
                    }
                }
            }
            return null;
        }

        public int[] bfsSea(int level, int[] start)
        {
            List<int[]> visited = new List<int[]>();
            Queue<int[]> queue = new Queue<int[]>();
            List<int[]> list, lake;
            int[] tracker;
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                tracker = new int[] { queue.Peek()[0], queue.Peek()[1] };
                queue.Dequeue();

                if (isIn(tracker, sea))
                {
                    /*lake = fillLake(tracker);
                    if (isIn(tracker, sea))
                    {
                        return tracker;
                    }*/
                    return tracker;

                }

                if (isIn(tracker, visited))
                {
                    continue;
                }
                else
                {

                    visited.Add(new int[] { tracker[0], tracker[1] });
                    list = neighborSearch(tracker[0], tracker[1]);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (isIn(list[i], visited) || !passable(0, tracker, list[i]))
                        {
                            continue;
                        }
                        //else if (isIn(list[i], sea)) continue;
                        else
                        {
                            queue.Enqueue(list[i]);
                        }
                    }
                }
            }
            return null;
        }



        public List<int[]> generateRivers()
        {
            List<int[]> rivers, top;
            List<int[]> visited = new List<int[]>();
            List<int[]> tempPath;
            List<int[]>[] layers = allLayers();
            List<List<int[]>> sourceTop = new List<List<int[]>>();
            rivers = new List<int[]>();
            int temp = 0;
            int[] goal;
            foreach (int[] coord in layers[7])
            {
                if (isIn(coord, visited)) continue;
                top = fillLayer(coord);
                sourceTop.Add(top);
                visited.AddRange(top);
                temp += 1;
            }
            

            foreach (List<int[]> source in sourceTop)
            {
                
                foreach (int[] coord in source)
                {
                    if (generateRandomNumber(50) == 1)
                    {

                        goal = bfs(0, coord);
                        if (goal == null) goal = bfsSea(0, coord);
                        tempPath = aStar(coord[0], coord[1], goal[0], goal[1]);
                        foreach (int[] i in tempPath)
                        {
                            rivers.Add(i);
                        }
                        break;
                    }
                }
            }

            return rivers;
        }
        public List<int[]> generateSea()
        {
            List<int[]> list;
            List<int[]> allConnected= new List<int[]>();
            Queue<int[]> queue = new Queue<int[]>();
            //System.Diagnostics.Debug.WriteLine("Hej hej");
            queue.Enqueue(new int[] { 0, 0 });
            //allConnected.Add(new int[] { 0, 0 });
            int[] tracker;
            while (queue.Count>0)
            {
                tracker=new int[] {queue.Peek()[0], queue.Peek()[1] };
                queue.Dequeue();
                //System.Diagnostics.Debug.WriteLine(tracker[0] + ", " + tracker[1]);
                if (isIn(tracker, allConnected))
                {
                    continue;
                }
                else
                {
                    allConnected.Add(new int[] { tracker[0], tracker[1] });
                    list = neighborSearch(tracker[0], tracker[1]);
                    for(int i =0; i < list.Count; i++)
                    {
                        if (isIn(list[i], allConnected) || map[list[i][0], list[i][1]]>waterlevel)
                        {
                            continue;
                        }
                        else
                        {
                            queue.Enqueue(list[i]);
                        }
                    }
                }
            }
            
            return allConnected;
        } 
        public void generateMap()
        {
            map = new int[100, 100];
            for (int i = 1; i < 11; i++)
            {
                generatelvl(i,45);
                if (i > 5) smoothLvl(i, 1);
                else if (i > 4) smoothLvl(i, 2);
                else if (i > 2) smoothLvl(i, 3);
                else smoothLvl(i, 4);
            }
            sea = generateSea();
        }
        private int generateRandomNumber(int chance)
        {
            if (rgen.Next(100)<=chance)
            {
                return 1;
            }
            return 0;
        }

        public void generatelvl(int level, int chance) {
            for (int col = 1; col < map.GetLength(0) - 1; col++)
            {
                for (int row = 1; row < map.GetLength(1) - 1; row++)
                {
                    if(map[col, row]==level-1)
                    map[col, row] += generateRandomNumber(chance);
                    //System.Diagnostics.Debug.WriteLine(rgen.Next(2));
                }

            }
        }
        public void smoothLvl(int level, int iterations)
        {
            int[,] tmpMap;
            for(int i= 0; i < iterations; i++)
            {
                tmpMap = (int[,])map.Clone();
                for (int col = 1; col < map.GetLength(0) - 1; col++)
                {
                    for (int row = 1; row < map.GetLength(1) - 1; row++)
                    {
                        if (map[col, row] == level)
                        {
                            if (n3(col, row, level,3))
                            {
                                tmpMap[col, row] = level;
                                
                            }
                            else
                            {
                                tmpMap[col, row] = level - 1;
                            }
                        }
                        else if(map[col, row] == level-1)
                        {
                            if (n3(col, row, level, 4))
                            {
                                tmpMap[col, row] = level;
                            }
                            else
                            {
                                tmpMap[col, row] = level - 1;
                            }
                        }

                    }

                }
                map = (int[,])tmpMap.Clone();
            }
            
        }

        public List<int[]> neighborSearch(int xCoord, int yCoord)
        {
            List<int[]> list = new List<int[]>();
            
            for (int col = xCoord - 1; col <= xCoord + 1; col++)
            {
                for (int row = yCoord - 1; row <= yCoord + 1; row++)
                {
                    if (col<0||col>99) continue;
                    if (row < 0 || row > 99) continue;
                    if (col == xCoord && row == yCoord) continue;
                    list.Add(new int[2] { col, row });
                }
            }
            return list;
        }


        public bool n3(int xCoord, int yCoord, int currentlvl, int comparison)
        {
            int type = 0;
            for (int col = xCoord - 1; col <= xCoord + 1; col++)
            {
                for (int row = yCoord - 1; row <= yCoord + 1; row++)
                {
                    if (col == xCoord && row == yCoord) continue;
                    if (map[col, row] == currentlvl)
                    {
                        type++;
                    }
                }
            }
            if (type >= comparison) return true;
            return false;
        }

    }
}

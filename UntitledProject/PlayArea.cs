
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace UntitledProject
{
   
    class World
    {
        //cell and pos are to orient around subbiomes in the biome only world aka pos.x and y are between 0-150

        
        public double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }
        public static SubBiome[,] GenerateSubBiomes(Container cell, Biome[,] biomes)
        {
            SubBiome[,] subBiomes = new SubBiome[cell.size * 5, cell.size * 5];
            for (int i = 0; i < cell.size; i++)
            {

                for (int j = 0; j < cell.size; j++)
                {
                    if (i + cell.x<0 || i + cell.x>biomes.GetLength(0) || j + cell.y < 0 || j + cell.y > biomes.GetLength(1))
                    {
                        continue;
                    }
                    Point position = new Point(i + cell.x, j + cell.y);

                    double avarage = 0.0;
                    double havarage = 0.0;
                    double tavarage = 0.0;
                    for (int a = 0; a < 8; a++)
                    {
                        avarage += Math.Abs(biomes[i + cell.x, j + cell.y].GetHeightAngle(a, biomes, position));
                        havarage += Math.Abs(biomes[i + cell.x, j + cell.y].GetFertilityAngle(a, biomes, position));
                        tavarage += Math.Abs(biomes[i + cell.x, j + cell.y].GetTemperatureAngle(a, biomes, position));
                    }

                    subBiomes[i * 5 + 2, j * 5 + 2] = new SubBiome(biomes, position, avarage / 8.0, havarage / 8.0, tavarage / 8.0);

                    subBiomes[i * 5, j * 5 + 1] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.x, j + cell.y].GetHeightAngle(6, biomes, position), biomes[i + cell.x, j + cell.y].GetHeightAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetFertilityAngle(6, biomes, position), biomes[i + cell.x, j + cell.y].GetFertilityAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetTemperatureAngle(6, biomes, position), biomes[i + cell.x, j + cell.y].GetTemperatureAngle(7, biomes, position)));

                    subBiomes[i * 5, j * 5 + 3] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.x, j + cell.y].GetHeightAngle(6, biomes, position), biomes[i + cell.x, j + cell.y].GetHeightAngle(5, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetFertilityAngle(6, biomes, position), biomes[i + cell.x, j + cell.y].GetFertilityAngle(5, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetTemperatureAngle(6, biomes, position), biomes[i + cell.x, j + cell.y].GetTemperatureAngle(5, biomes, position)));

                    subBiomes[i * 5 + 4, j * 5 + 1] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.x, j + cell.y].GetHeightAngle(2, biomes, position), biomes[i + cell.x, j + cell.y].GetHeightAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetFertilityAngle(2, biomes, position), biomes[i + cell.x, j + cell.y].GetFertilityAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetTemperatureAngle(2, biomes, position), biomes[i + cell.x, j + cell.y].GetTemperatureAngle(1, biomes, position)));

                    subBiomes[i * 5 + 4, j * 5 + 3] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.x, j + cell.y].GetHeightAngle(2, biomes, position), biomes[i + cell.x, j + cell.y].GetHeightAngle(3, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetFertilityAngle(6, biomes, position), biomes[i + cell.x, j + cell.y].GetFertilityAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetTemperatureAngle(6, biomes, position), biomes[i + cell.x, j + cell.y].GetTemperatureAngle(7, biomes, position)));

                    subBiomes[i * 5 + 1, j * 5] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.x, j + cell.y].GetHeightAngle(0, biomes, position), biomes[i + cell.x, j + cell.y].GetHeightAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetFertilityAngle(0, biomes, position), biomes[i + cell.x, j + cell.y].GetFertilityAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetTemperatureAngle(0, biomes, position), biomes[i + cell.x, j + cell.y].GetTemperatureAngle(7, biomes, position)));

                    subBiomes[i * 5 + 3, j * 5] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.x, j + cell.y].GetHeightAngle(0, biomes, position), biomes[i + cell.x, j + cell.y].GetHeightAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetFertilityAngle(0, biomes, position), biomes[i + cell.x, j + cell.y].GetFertilityAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetTemperatureAngle(0, biomes, position), biomes[i + cell.x, j + cell.y].GetTemperatureAngle(1, biomes, position)));

                    subBiomes[i * 5 + 1, j * 5 + 4] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.x, j + cell.y].GetHeightAngle(5, biomes, position), biomes[i + cell.x, j + cell.y].GetHeightAngle(4, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetFertilityAngle(5, biomes, position), biomes[i + cell.x, j + cell.y].GetFertilityAngle(4, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetTemperatureAngle(5, biomes, position), biomes[i + cell.x, j + cell.y].GetTemperatureAngle(4, biomes, position)));

                    subBiomes[i * 5 + 3, j * 5 + 4] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.x, j + cell.y].GetHeightAngle(4, biomes, position), biomes[i + cell.x, j + cell.y].GetHeightAngle(3, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetFertilityAngle(4, biomes, position), biomes[i + cell.x, j + cell.y].GetFertilityAngle(3, biomes, position)),
                        Lerp(0.5, biomes[i + cell.x, j + cell.y].GetTemperatureAngle(4, biomes, position), biomes[i + cell.x, j + cell.y].GetTemperatureAngle(3, biomes, position)));

                    for (int a = 0; a < 2; a++)
                    {
                        subBiomes[i * 5 + a, j * 5 + 2] = new SubBiome(biomes, position, biomes[i + cell.x, j + cell.y].GetHeightAngle(6, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetFertilityAngle(6, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetTemperatureAngle(6, biomes, position));
                        subBiomes[i * 5 + 4 - a, j * 5 + 2] = new SubBiome(biomes, position, biomes[i + cell.x, j + cell.y].GetHeightAngle(2, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetFertilityAngle(2, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetTemperatureAngle(2, biomes, position));

                        subBiomes[i * 5 + 2, j * 5 + a] = new SubBiome(biomes, position, biomes[i + cell.x, j + cell.y].GetHeightAngle(0, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetFertilityAngle(0, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetTemperatureAngle(0, biomes, position));
                        subBiomes[i * 5 + 2, j * 5 + 4 - a] = new SubBiome(biomes, position, biomes[i + cell.x, j + cell.y].GetHeightAngle(4, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetFertilityAngle(4, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetTemperatureAngle(4, biomes, position));

                        subBiomes[i * 5 + a, j * 5 + a] = new SubBiome(biomes, position, biomes[i + cell.x, j + cell.y].GetHeightAngle(7, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetFertilityAngle(7, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetTemperatureAngle(7, biomes, position));
                        subBiomes[i * 5 + 4 - a, j * 5 + 4 - a] = new SubBiome(biomes, position, biomes[i + cell.x, j + cell.y].GetHeightAngle(3, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetFertilityAngle(3, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetTemperatureAngle(3, biomes, position));

                        subBiomes[i * 5 + a, j * 5 + 4 - a] = new SubBiome(biomes, position, biomes[i + cell.x, j + cell.y].GetHeightAngle(5, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetFertilityAngle(5, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetTemperatureAngle(5, biomes, position));
                        subBiomes[i * 5 + 4 - a, j * 5 + a] = new SubBiome(biomes, position, biomes[i + cell.x, j + cell.y].GetHeightAngle(1, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetFertilityAngle(1, biomes, position),
                            biomes[i + cell.x, j + cell.y].GetTemperatureAngle(1, biomes, position));

                    }
                }
            }
            for (int i = 0; i < cell.size * 5; i++)
            {

                for (int j = 0; j < cell.size * 5; j++)
                {
                    if (subBiomes[i, j] == null)
                    {
                        continue;
                    }
                    subBiomes[i, j].SetData(biomes, new Point(i - (i % 5) + 2, j - (j % 5) + 2), new Point(i, j));

                }
            }
            Trees(cell, subBiomes, biomes);
            return subBiomes;
        }
        private static double Lerp(double t, double a1, double a2)
        {
            return a1 + t * (a2 - a1);
        }
        private static void Trees(Container cell, SubBiome[,] subBiomes, Biome[,] biomes)
        {
            int size = subBiomes.GetLength(0);
            int trees = 0;
            int highestIndex = 0;
            Point position;
            bool[] banlist;
            banlist = new bool[8];

            for (int i = 0; i < cell.size; i++)
            {

                for (int j = 0; j < cell.size; j++)
                {
                    if (i + cell.x < 0 || i + cell.x > biomes.GetLength(0) || j + cell.y < 0 || j + cell.y > biomes.GetLength(1))
                    {
                        continue;
                    }
                    position = new Point(i + cell.x, j + cell.y);

                    if (biomes[i + cell.x, j + cell.y].humidity < 0.4 || biomes[i + cell.x, j + cell.y].biome == 0 || biomes[i + cell.x, j + cell.y].biome == 1)
                    {
                        trees = 0;
                        //trees = (int)Math.Round(8.0*(biomes[i + cell.x, j + cell.y].humidity-0.2)/0.8);
                    }

                    else
                    {
                        trees = (int)Math.Round(6.0 * (biomes[i + cell.x, j + cell.y].humidity - 0.35) / 0.65);
                    }

                    if (trees == 0) continue;



                    while (trees != 0)
                    {
                        for (int a = 0; a < 8; a++)
                        {
                            if (!banlist[a]) break;
                            if (a == 7) banlist = new bool[8];
                        }
                        highestIndex = GetHighestBioAngle(banlist, biomes, position);
                        banlist[highestIndex] = true;
                        if (biomes[i + cell.x, j + cell.y].GetHeightAngle(highestIndex, biomes, position) > 50.0)
                        {

                            continue;
                        }
                        if (highestIndex == 0)
                        {
                            //if (subBiomes[i * 5 + 2, j * 5 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 2, j * 5 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 4)
                        {
                            //if (subBiomes[i * 5 + 2, j * 5 + 3 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 2, j * 5 + 3 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 2)
                        {
                            //if (subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 2].IsCliff()) continue;
                            subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 2].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 6)
                        {
                            //if (subBiomes[i * 5 + (trees % 2), j * 5 + 2].IsCliff()) continue;
                            subBiomes[i * 5 + (trees % 2), j * 5 + 2].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 7)
                        {
                            //if (subBiomes[i * 5 + (trees % 2), j * 5 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + (trees % 2), j * 5 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 3)
                        {
                            //if (subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 3 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 3 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 5)
                        {
                            //if (subBiomes[i * 5 + 4 - (trees % 2), j * 5 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 4 - (trees % 2), j * 5 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 1)
                        {
                            //if (subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 1 - (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 1 - (trees % 2)].SetTree();
                            trees--;
                        }
                    }

                }
            }
        }
        private static int GetHighestBioAngle(bool[] banlist, Biome[,] biomes, Point pos)
        {
            double highestValue = -90.0;
            int highestDir = -1;
            for (int i = 0; i < 8; i++)
            {
                if (banlist[i]) continue;
                if (biomes[pos.X, pos.Y].GetFertilityAngle(i, biomes, pos) > highestValue)
                {
                    highestDir = i;
                }
            }
            return highestDir;
        }


        public static SubBiome[,] GenerateSubBiomes(Rectangle cell, Biome[,] biomes)
        {
            SubBiome[,] subBiomes = new SubBiome[cell.Width * 5, cell.Height * 5];
            for (int i = 0; i < cell.Width; i++)
            {

                for (int j = 0; j < cell.Height; j++)
                {
                    if (i + cell.X < 0 || i + cell.X > biomes.GetLength(0)-1 || j + cell.Y < 0 || j + cell.Y > biomes.GetLength(1)-1)
                    {
                        continue;
                    }
                    Point position = new Point(i + cell.X, j + cell.Y);

                    double avarage = 0.0;
                    double havarage = 0.0;
                    double tavarage = 0.0;
                    for (int a = 0; a < 8; a++)
                    {
                        avarage += Math.Abs(biomes[i + cell.X, j + cell.Y].GetHeightAngle(a, biomes, position));
                        havarage += Math.Abs(biomes[i + cell.X, j + cell.Y].GetFertilityAngle(a, biomes, position));
                        tavarage += Math.Abs(biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(a, biomes, position));
                    }

                    subBiomes[i * 5 + 2, j * 5 + 2] = new SubBiome(biomes, position, avarage / 8.0, havarage / 8.0, tavarage / 8.0);

                    subBiomes[i * 5, j * 5 + 1] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(7, biomes, position)));

                    subBiomes[i * 5, j * 5 + 3] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(5, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(5, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(5, biomes, position)));

                    subBiomes[i * 5 + 4, j * 5 + 1] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(2, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(2, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(2, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(1, biomes, position)));

                    subBiomes[i * 5 + 4, j * 5 + 3] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(2, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(3, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(7, biomes, position)));

                    subBiomes[i * 5 + 1, j * 5] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(7, biomes, position)));

                    subBiomes[i * 5 + 3, j * 5] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(1, biomes, position)));

                    subBiomes[i * 5 + 1, j * 5 + 4] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(5, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(4, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(5, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(4, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(5, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(4, biomes, position)));

                    subBiomes[i * 5 + 3, j * 5 + 4] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(4, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(3, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(4, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(3, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(4, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(3, biomes, position)));

                    for (int a = 0; a < 2; a++)
                    {
                        subBiomes[i * 5 + a, j * 5 + 2] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(6, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(6, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(6, biomes, position));
                        subBiomes[i * 5 + 4 - a, j * 5 + 2] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(2, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(2, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(2, biomes, position));

                        subBiomes[i * 5 + 2, j * 5 + a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(0, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(0, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(0, biomes, position));
                        subBiomes[i * 5 + 2, j * 5 + 4 - a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(4, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(4, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(4, biomes, position));

                        subBiomes[i * 5 + a, j * 5 + a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(7, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(7, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(7, biomes, position));
                        subBiomes[i * 5 + 4 - a, j * 5 + 4 - a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(3, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(3, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(3, biomes, position));

                        subBiomes[i * 5 + a, j * 5 + 4 - a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(5, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(5, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(5, biomes, position));
                        subBiomes[i * 5 + 4 - a, j * 5 + a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(1, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(1, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(1, biomes, position));

                    }
                }
            }
            for (int i = 0; i < cell.Width * 5; i++)
            {

                for (int j = 0; j < cell.Height * 5; j++)
                {
                    if (subBiomes[i, j] == null)
                    {
                        continue;
                    }
                    subBiomes[i, j].SetData(biomes, new Point(i - (i % 5) + 2, j - (j % 5) + 2), new Point(i, j));

                }
            }
            Trees(cell, subBiomes, biomes);
            return subBiomes;
        }
        public static SubBiome[,] GenerateSubBiomes(Rectangle cell, Rectangle previous, Biome[,] biomes, SubBiome[,] previousSub)
        {
            Rectangle include = Rectangle.Intersect(cell, previous);
            SubBiome[,] subBiomes = new SubBiome[cell.Width * 5, cell.Height * 5];
            for (int i = 0; i < cell.Width; i++)
            {

                for (int j = 0; j < cell.Height; j++)
                {
                    if (i + cell.X < 0 || i + cell.X > biomes.GetLength(0) - 1 || j + cell.Y < 0 || j + cell.Y > biomes.GetLength(1) - 1)
                    {
                        continue;
                    }
                    
                    Point position = new Point(i + cell.X, j + cell.Y);
                    if (include.Contains(position))
                    {
                        for (int a = 0; a < 5; a++)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                subBiomes[i * 5 + a, j * 5 + b] = previousSub[(position.X-previous.X)*5+a, (position.Y - previous.Y) * 5 + b];
                            }
                        }
                        continue;
                    }
                    double avarage = 0.0;
                    double havarage = 0.0;
                    double tavarage = 0.0;
                    for (int a = 0; a < 8; a++)
                    {
                        avarage += Math.Abs(biomes[i + cell.X, j + cell.Y].GetHeightAngle(a, biomes, position));
                        havarage += Math.Abs(biomes[i + cell.X, j + cell.Y].GetFertilityAngle(a, biomes, position));
                        tavarage += Math.Abs(biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(a, biomes, position));
                    }

                    subBiomes[i * 5 + 2, j * 5 + 2] = new SubBiome(biomes, position, avarage / 8.0, havarage / 8.0, tavarage / 8.0);

                    subBiomes[i * 5, j * 5 + 1] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(7, biomes, position)));

                    subBiomes[i * 5, j * 5 + 3] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(5, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(5, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(5, biomes, position)));

                    subBiomes[i * 5 + 4, j * 5 + 1] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(2, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(2, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(2, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(1, biomes, position)));

                    subBiomes[i * 5 + 4, j * 5 + 3] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(2, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(3, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(6, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(7, biomes, position)));

                    subBiomes[i * 5 + 1, j * 5] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(7, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(7, biomes, position)));

                    subBiomes[i * 5 + 3, j * 5] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(1, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(0, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(1, biomes, position)));

                    subBiomes[i * 5 + 1, j * 5 + 4] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(5, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(4, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(5, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(4, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(5, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(4, biomes, position)));

                    subBiomes[i * 5 + 3, j * 5 + 4] = new SubBiome(biomes, position, Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetHeightAngle(4, biomes, position), biomes[i + cell.X, j + cell.Y].GetHeightAngle(3, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetFertilityAngle(4, biomes, position), biomes[i + cell.X, j + cell.Y].GetFertilityAngle(3, biomes, position)),
                        Lerp(0.5, biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(4, biomes, position), biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(3, biomes, position)));

                    for (int a = 0; a < 2; a++)
                    {
                        subBiomes[i * 5 + a, j * 5 + 2] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(6, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(6, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(6, biomes, position));
                        subBiomes[i * 5 + 4 - a, j * 5 + 2] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(2, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(2, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(2, biomes, position));

                        subBiomes[i * 5 + 2, j * 5 + a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(0, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(0, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(0, biomes, position));
                        subBiomes[i * 5 + 2, j * 5 + 4 - a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(4, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(4, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(4, biomes, position));

                        subBiomes[i * 5 + a, j * 5 + a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(7, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(7, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(7, biomes, position));
                        subBiomes[i * 5 + 4 - a, j * 5 + 4 - a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(3, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(3, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(3, biomes, position));

                        subBiomes[i * 5 + a, j * 5 + 4 - a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(5, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(5, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(5, biomes, position));
                        subBiomes[i * 5 + 4 - a, j * 5 + a] = new SubBiome(biomes, position, biomes[i + cell.X, j + cell.Y].GetHeightAngle(1, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetFertilityAngle(1, biomes, position),
                            biomes[i + cell.X, j + cell.Y].GetTemperatureAngle(1, biomes, position));

                    }
                }
            }
            for (int i = 0; i < cell.Width * 5; i++)
            {

                for (int j = 0; j < cell.Height * 5; j++)
                {
                    Point position = new Point(i/5 + cell.X, j/5 + cell.Y);
                    if (subBiomes[i, j] == null || include.Contains(position))
                    {
                        continue;
                    }
                    subBiomes[i, j].SetData(biomes, new Point(i - (i % 5) + 2, j - (j % 5) + 2), new Point(i, j));

                }
            }
            Trees(cell,include, subBiomes, biomes);
            return subBiomes;
        }
        private static void Trees(Rectangle cell, Rectangle overlap, SubBiome[,] subBiomes, Biome[,] biomes)
        {
            int size = subBiomes.GetLength(0);
            int trees = 0;
            int highestIndex = 0;
            Point position;
            bool[] banlist;
            banlist = new bool[8];

            for (int i = 0; i < cell.Width; i++)
            {

                for (int j = 0; j < cell.Height; j++)
                {
                    if (i + cell.X < 0 || i + cell.X > biomes.GetLength(0) - 1 || j + cell.Y < 0 || j + cell.Y > biomes.GetLength(1) - 1)
                    {
                        continue;
                    }

                    position = new Point(i + cell.X, j + cell.Y);
                    if (overlap.Contains(position)) continue;

                    if (biomes[i + cell.X, j + cell.Y].humidity < 0.4 || biomes[i + cell.X, j + cell.Y].biome == 0 || biomes[i + cell.X, j + cell.Y].biome == 1)
                    {
                        trees = 0;
                        //trees = (int)Math.Round(8.0*(biomes[i + cell.x, j + cell.y].humidity-0.2)/0.8);
                    }

                    else
                    {
                        trees = (int)Math.Round(6.0 * (biomes[i + cell.X, j + cell.Y].humidity - 0.35) / 0.65);
                    }

                    if (trees == 0) continue;



                    while (trees != 0)
                    {
                        for (int a = 0; a < 8; a++)
                        {
                            if (!banlist[a]) break;
                            if (a == 7) banlist = new bool[8];
                        }
                        highestIndex = GetHighestBioAngle(banlist, biomes, position);
                        banlist[highestIndex] = true;
                        if (biomes[i + cell.X, j + cell.Y].GetHeightAngle(highestIndex, biomes, position) > 50.0)
                        {

                            continue;
                        }
                        if (highestIndex == 0)
                        {
                            //if (subBiomes[i * 5 + 2, j * 5 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 2, j * 5 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 4)
                        {
                            //if (subBiomes[i * 5 + 2, j * 5 + 3 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 2, j * 5 + 3 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 2)
                        {
                            //if (subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 2].IsCliff()) continue;
                            subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 2].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 6)
                        {
                            //if (subBiomes[i * 5 + (trees % 2), j * 5 + 2].IsCliff()) continue;
                            subBiomes[i * 5 + (trees % 2), j * 5 + 2].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 7)
                        {
                            //if (subBiomes[i * 5 + (trees % 2), j * 5 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + (trees % 2), j * 5 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 3)
                        {
                            //if (subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 3 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 3 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 5)
                        {
                            //if (subBiomes[i * 5 + 4 - (trees % 2), j * 5 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 4 - (trees % 2), j * 5 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 1)
                        {
                            //if (subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 1 - (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 1 - (trees % 2)].SetTree();
                            trees--;
                        }
                    }

                }
            }
        }
        private static void Trees(Rectangle cell, SubBiome[,] subBiomes, Biome[,] biomes)
        {
            int size = subBiomes.GetLength(0);
            int trees = 0;
            int highestIndex = 0;
            Point position;
            bool[] banlist;
            banlist = new bool[8];

            for (int i = 0; i < cell.Width; i++)
            {

                for (int j = 0; j < cell.Height; j++)
                {
                    if (i + cell.X < 0 || i + cell.X > biomes.GetLength(0)-1 || j + cell.Y < 0 || j + cell.Y > biomes.GetLength(1)-1)
                    {
                        continue;
                    }

                    position = new Point(i + cell.X, j + cell.Y);

                    if (biomes[i + cell.X, j + cell.Y].humidity < 0.4 || biomes[i + cell.X, j + cell.Y].biome == 0 || biomes[i + cell.X, j + cell.Y].biome == 1)
                    {
                        trees = 0;
                        //trees = (int)Math.Round(8.0*(biomes[i + cell.x, j + cell.y].humidity-0.2)/0.8);
                    }

                    else
                    {
                        trees = (int)Math.Round(6.0 * (biomes[i + cell.X, j + cell.Y].humidity - 0.35) / 0.65);
                    }

                    if (trees == 0) continue;



                    while (trees != 0)
                    {
                        for (int a = 0; a < 8; a++)
                        {
                            if (!banlist[a]) break;
                            if (a == 7) banlist = new bool[8];
                        }
                        highestIndex = GetHighestBioAngle(banlist, biomes, position);
                        banlist[highestIndex] = true;
                        if (biomes[i + cell.X, j + cell.Y].GetHeightAngle(highestIndex, biomes, position) > 50.0)
                        {

                            continue;
                        }
                        if (highestIndex == 0)
                        {
                            //if (subBiomes[i * 5 + 2, j * 5 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 2, j * 5 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 4)
                        {
                            //if (subBiomes[i * 5 + 2, j * 5 + 3 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 2, j * 5 + 3 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 2)
                        {
                            //if (subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 2].IsCliff()) continue;
                            subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 2].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 6)
                        {
                            //if (subBiomes[i * 5 + (trees % 2), j * 5 + 2].IsCliff()) continue;
                            subBiomes[i * 5 + (trees % 2), j * 5 + 2].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 7)
                        {
                            //if (subBiomes[i * 5 + (trees % 2), j * 5 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + (trees % 2), j * 5 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 3)
                        {
                            //if (subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 3 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 3 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 5)
                        {
                            //if (subBiomes[i * 5 + 4 - (trees % 2), j * 5 + (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 4 - (trees % 2), j * 5 + (trees % 2)].SetTree();
                            trees--;
                        }
                        else if (highestIndex == 1)
                        {
                            //if (subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 1 - (trees % 2)].IsCliff()) continue;
                            subBiomes[i * 5 + 3 + (trees % 2), j * 5 + 1 - (trees % 2)].SetTree();
                            trees--;
                        }
                    }

                }
            }
        }
        /*public static SubBiome[,] GetLocal(Rectangle large, Rectangle small, SubBiome[,] subBiomes)
        {
            SubBiome[,] returnList = new SubBiome[small.Width*5, small.Height*5];

            for (int i=0;i< small.Width * 5;i++)
            {
                for (int j = 0; j < small.Height * 5; j++)
                {
                    if (large.X * 5 - small.X * 5 < 0 || large.Y * 5 - small.Y * 5 < 0) continue;
                    returnList[i, j] = subBiomes[small.X*5-large.X*5+,];
                }
            }
            return returnList;
        }*/
        public static DeconstructedSubBiome[,] GenerateWorld(Biome[,] biomes)
        {
            DeconstructedSubBiome[,] temp1 = new DeconstructedSubBiome[biomes.GetLength(0)*5, biomes.GetLength(1)*5];
            //SubBiome[,] temp = GenerateSubBiomes(new Rectangle(0, 0, biomes.GetLength(0), biomes.GetLength(1)), biomes);
            for(int i = 0; i < biomes.GetLength(0) * 5; i++)
            {
                for (int j = 0; j < biomes.GetLength(0) * 5; j++)
                {
                    temp1[i, j] = new DeconstructedSubBiome();
                    temp1[i, j].data = new byte[4];
                    temp1[i, j].parent = new short[3];
                    temp1[i, j].tree = true;
                }
            }
            //temp = null;
            return temp1;
        }
    }
    struct DeconstructedSubBiome
    {
        public byte[] data;
        public bool tree;
        public short[] parent;
        /*public SubBiome Construct()
        {
            return new SubBiome(data, parent, tree);
        }*/
    }

    class SubBiome
    {
        /*
         0=plains (between -5 degrees and 5 degrees)
         1=slight hill (5-25)
         2=hill (25-40)
         3=steep hill(40-50)
         4=cliff(50+)
         */
        double angle,tAngle,hAngle;
        Point parent;
        public double  height,humidity, temperature;
        public bool tree = false;
        double grass = 0.0;
        
        public SubBiome(Biome[,] biomes,Point parent, double angle, double hAngle,double tAngle)
        {
            this.parent = parent;
            this.angle = angle;
            this.hAngle = hAngle;
            this.tAngle = tAngle;
        }

        public SubBiome(byte[] data,short[] parent, bool tree)
        {
            angle = parent[0] / 255.0;
            height = data[0] / 255.0;
            humidity = data[1] / 255.0;
            temperature = data[2] / 255.0;
            grass = data[3] / 255.0;
            this.tree = tree;
            this.parent = new Point(parent[1],parent[2]);
        }
        public DeconstructedSubBiome Deconstruct()
        {
            byte[] data = new byte[4];
            data[0]= (byte)Math.Round(height*255.0);
            data[1] = (byte)Math.Round(humidity * 255.0);
            data[2] = (byte)Math.Round(temperature * 255.0);
            data[3] = (byte)Math.Round(grass * 255.0);
            short[] parent = new short[3];
            parent[0] = (short)Math.Round(angle*90);
            parent[1] = (short)this.parent.X;
            parent[2] = (short)this.parent.Y;
            DeconstructedSubBiome A;
            A.data = data;
            A.parent = parent;
            A.tree = tree;
            return A;
        }
        

        public void GetGrass()
        {
            if (humidity<0.2 || height<0.51)
            {
                grass = 0.0;
            }
            else if (humidity < 0.30)
            {
                grass = (humidity-0.2)/0.1;
            }
            else
            {
                grass = 1.0-(humidity - 0.3) / 0.7;
            }
        }
        public Color GetHumidityBase()
        {
            Color colorLower, colorUpper;

            colorLower = Color.Transparent;
            colorUpper = Color.Transparent;
            if (height>0.8) return Color.Lerp(Color.Gray, Color.White, (float)(((height - 0.8) / 0.2) - ((height - 0.8) / 0.2) % 0.05));
            else if (height > 0.65)
            {
                if (humidity < 0.20)
                {
                    if (temperature > 0.4)
                    {
                        colorUpper = Color.Bisque;
                        colorLower = Color.Orange;
                    }
                    else
                    {
                        colorUpper = Color.Bisque;
                        colorLower = Color.MediumAquamarine;
                    }
                    
                }
                else if (humidity < 0.40)
                {
                    if (temperature > 0.6)
                    {
                        colorUpper = Color.Lerp(Color.LawnGreen, Color.White, 0.85f);
                        colorLower = Color.Lerp(Color.Goldenrod, Color.LawnGreen, 0.45f);
                    }
                    else if (temperature > 0.4)
                    {
                        colorUpper = Color.Lerp(Color.OliveDrab, Color.White, 0.85f);
                        colorLower = Color.Lerp(Color.Goldenrod, Color.OliveDrab, 0.75f);
                    }
                    else
                    {
                        colorUpper = Color.Lerp(Color.PaleGreen, Color.White, 0.45f);
                        colorLower = Color.Lerp(Color.Olive, Color.PaleGreen, 0.35f);
                    }
                    
                }
                else if (humidity < 0.60)
                {
                    if (temperature > 0.6)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Lerp(Color.Goldenrod, Color.LawnGreen, 0.45f), Color.ForestGreen, 0.4f), Color.White, 0.75f);
                        colorLower = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.LawnGreen, 0.45f), Color.ForestGreen, 0.4f);
                    }
                    else if (temperature > 0.4)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Lerp(Color.Goldenrod, Color.OliveDrab, 0.75f), Color.Green, 0.45f), Color.White, 0.75f);
                        colorLower = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.OliveDrab, 0.75f), Color.Green, 0.40f);
                    }
                    else
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Lerp(Color.Olive, Color.PaleGreen, 0.35f), Color.SeaGreen, 0.6f), Color.White, 0.75f);
                        colorLower = Color.Lerp(Color.Lerp(Color.Olive, Color.PaleGreen, 0.35f), Color.SeaGreen, 0.4f);
                    }

                }
                else if (humidity < 0.80)
                {
                    if (temperature > 0.6)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Lerp(Color.Goldenrod, Color.LawnGreen, 0.45f), Color.ForestGreen, 0.4f), Color.White, 0.75f);
                        colorLower = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.LawnGreen, 0.5f), Color.Green, 0.6f);
                    }
                    else if (temperature > 0.4)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Lerp(Color.Goldenrod, Color.OliveDrab, 0.75f), Color.Green, 0.45f), Color.White, 0.75f);
                        colorLower = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.OliveDrab, 0.5f), Color.Green, 0.65f);
                    }
                    else
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Lerp(Color.Olive, Color.PaleGreen, 0.35f), Color.SeaGreen, 0.6f), Color.White, 0.75f);
                        colorLower = Color.Lerp(Color.Lerp(Color.Olive, Color.PaleGreen, 0.45f), Color.SeaGreen, 0.65f);
                    }

                }
                else
                {
                    if (temperature > 0.6)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Lerp(Color.Goldenrod, Color.LawnGreen, 0.45f), Color.ForestGreen, 0.4f), Color.White, 0.75f);
                        colorLower = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.LawnGreen, 0.5f), Color.Green, 0.85f);
                    }
                    else if (temperature > 0.4)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Lerp(Color.Goldenrod, Color.OliveDrab, 0.75f), Color.Green, 0.45f), Color.White, 0.75f);
                        colorLower = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.OliveDrab, 0.35f), Color.Green, 0.95f);
                    }
                    else
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Lerp(Color.Olive, Color.PaleGreen, 0.35f), Color.SeaGreen, 0.6f), Color.White, 0.75f); 
                        colorLower = Color.Lerp(Color.Lerp(Color.Olive, Color.PaleGreen, 0.45f), Color.SeaGreen, 0.95f);
                    }
                }
                return Color.Lerp(colorLower, colorUpper, (float)(((height - 0.65) / 0.15) - ((height - 0.65) / 0.15) % 0.1333));
            }
            else if (height > 0.51)
            {
                if (humidity < 0.20)
                {
                    if (temperature > 0.6)
                    {
                        colorUpper = Color.Orange;
                        colorLower = Color.OrangeRed;
                    }
                    else if (temperature > 0.4)
                    {
                        colorUpper = Color.Orange;
                        colorLower = Color.Lerp(Color.SaddleBrown, Color.OrangeRed, 0.25f);
                    }
                    else
                    {
                        colorUpper = Color.MediumAquamarine;
                        colorLower = Color.Lerp(Color.DarkCyan,Color.SlateGray,0.5f);
                    }
                }
                
                else if (humidity < 0.40)
                {
                    if (temperature > 0.8)
                    {
                        colorUpper = Color.Orange;
                        colorLower = Color.OrangeRed;
                    }
                    else if (temperature > 0.6)
                    {
                        colorUpper = Color.Lerp(Color.Goldenrod, Color.LawnGreen, 0.45f);
                        colorLower = Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.LawnGreen, 0.35f);
                    }
                    else if (temperature > 0.4)
                    {
                        colorUpper = Color.Lerp(Color.Goldenrod, Color.OliveDrab, 0.75f);
                        colorLower = Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.OliveDrab, 0.35f);
                    }
                    else
                    {

                        colorUpper = Color.Lerp(Color.Olive, Color.PaleGreen, 0.35f);
                        colorLower = Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.PaleGreen, 0.25f);
                    }
                }
                else if (humidity < 0.60)
                {
                    if (temperature > 0.6)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.LawnGreen, 0.45f), Color.ForestGreen, 0.35f);
                        colorLower = Color.Lerp(Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.LawnGreen, 0.15f), Color.ForestGreen, 0.25f);
                    }
                    else if (temperature > 0.4)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.OliveDrab, 0.75f), Color.Green, 0.30f);
                        colorLower = Color.Lerp(Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.OliveDrab, 0.35f), Color.Green, 0.15f);
                    }
                    else
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Olive, Color.PaleGreen, 0.35f), Color.SeaGreen, 0.35f);
                        colorLower = Color.Lerp(Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.PaleGreen, 0.15f), Color.SeaGreen, 0.25f);
                    }

                }
                else if (humidity < 0.80)
                {
                    if (temperature > 0.6)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.LawnGreen, 0.5f), Color.Green, 0.5f);
                        colorLower = Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.Green, 0.35f);
                    }
                    else if (temperature > 0.4)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.OliveDrab, 0.35f), Color.Green, 0.65f);
                        colorLower = Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.OliveDrab, 0.25f);
                        /*colorUpper = Color.Lerp(Color.Lerp(Color.Olive, Color.Chartreuse, 0.15f), Color.Green, 0.25f);
                        colorLower = Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.Olive, 0.3f);*/
                    }
                    else
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Olive, Color.PaleGreen, 0.35f), Color.SeaGreen, 0.65f);
                        colorLower = Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.DarkCyan, 0.25f);
                    }

                }

                
                else
                {
                    if (temperature > 0.6)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.LawnGreen, 0.5f), Color.Green, 0.75f);
                        colorLower = Color.Lerp(Color.DarkGreen, Color.Black, 0.25f);
                    }
                    else if (temperature > 0.4)
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Goldenrod, Color.OliveDrab, 0.35f), Color.Green, 0.85f);
                        colorLower = Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.45f), Color.OliveDrab, 0.1f);
                    }
                    else
                    {
                        colorUpper = Color.Lerp(Color.Lerp(Color.Olive, Color.PaleGreen, 0.45f), Color.SeaGreen, 0.85f);
                        colorLower = Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.65f), Color.DarkCyan, 0.35f);
                    }
                }
                return Color.Lerp(colorLower, colorUpper, (float)(((height - 0.51) / 0.14) - ((height - 0.51) / 0.14) % 0.142));
            }
            return Color.Lerp(colorLower,colorUpper, (float) (((height-0.52)/ 0.28)- ((height - 0.52) / 0.28)%0.0357));
        }
        public Color GetRealColor()
        {
            /* heightzone:      biozone:                tempzone:
             * 0=Mountain       0=barren                0=freezing
             * 1=Highlands      1=Low biomass           1=cold
             * 2=Lowlands       2=Medium biomass        2=temperate
             * 3=Coastal        3=High biomass          3=hot
             * 4=shallow Sea    4=Very high biomass     4=scorching
             * 5=Sea
             * 6=Ocean
             */
            
            if (IsCliff())
            {
                if (height < 0.5)
                {
                    if (height < 0.35)
                    {
                        return Color.Lerp(Color.DarkBlue, Color.Lerp(Color.Blue, Color.Gray, 0.05f), (float)((height / 0.35) - (height / 0.35) % 0.0571));
                    }
                    
                    return Color.Lerp(Color.Lerp(Color.Blue, Color.Lerp(Color.MediumSeaGreen, Color.Blue, 0.5f), (float)(((height - 0.35) / 0.15) - ((height - 0.35) / 0.15) % 0.1333)),Color.Gray,0.1f);
                    
                }
                else if (height<0.65)
                {
                    return Color.Lerp(Color.Lerp(Color.Black, Color.SlateGray, 0.5f), Color.Gray, (float)(((height - 0.5) / 0.15) - ((height - 0.5) / 0.15) % 0.1333));
                }
                else if (height < 0.8)
                {
                    return Color.Lerp(Color.Gray, Color.Lerp(Color.Gray, Color.White, 0.75f), (float)(((height - 0.65) / 0.15) - ((height - 0.65) / 0.15) % 0.1333));
                }
                return Color.Lerp(Color.Lerp(Color.Gray, Color.White, 0.85f), Color.White, (float)(((height - 0.8) / 0.2) - ((height - 0.8) / 0.2) % 0.1));
            }
            else if (height < 0.5) 
            {
                if (height < 0.35)
                {
                    return Color.Lerp(Color.DarkBlue, Color.Blue, (float)((height / 0.35) - (height / 0.35) % 0.0571));
                }
                return Color.Lerp(Color.Blue, Color.Lerp(Color.MediumSeaGreen, Color.Blue, 0.5f), (float)(((height-0.35) / 0.15)- ((height - 0.35) / 0.15)%0.1333));
            }
            
            else if (height < 0.51)
            {
                
                if (humidity > 0.6 && temperature > 0.4) 
                {
                    return Color.Lerp(Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.45f), Color.OliveDrab, 0.25f), Color.DarkGreen, (float)(((height-0.5)/0.01))); 
                }
                return Color.Lerp(Color.BurlyWood, Color.Bisque, (float)(((height - 0.5) / 0.01)));
            }
            return GetHumidityBase();
            
        }
        public double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p2.X-p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }
        public void SetData(Biome[,] biomes,Point parentPos, Point pos)
        {
            double distance = Distance(parentPos, pos);
            height = biomes[parent.X, parent.Y].height +(Math.Tan(angle / (180.0 / Math.PI)) / 150.0) * (distance / 4.0);
            humidity = biomes[parent.X, parent.Y].humidity + (Math.Tan(hAngle / (180.0 / Math.PI)) / 150.0) * (distance / 4.0);
            temperature = biomes[parent.X, parent.Y].temperature + (Math.Tan(tAngle / (180.0 / Math.PI)) / 150.0) * (distance / 4.0);
            GetGrass();
        }
        public void SetTree()
        {
            if(height>0.52&&!IsCliff())
            tree = true;
        }
        private double Lerp(double t, double a1, double a2)
        {
            return a1 + t * (a2 - a1);
        }
        public bool IsCliff()
        {
            if (Math.Abs(angle) > 50.0) return true;
            return false;
        }


        public Color GetColor()
        {

            /*return Color.Lerp(Color.Black, Color.White, (float)(height-0.5)/0.5f);
            if (tree) 
            {
                if (height < 0.52) return GetLightScale(biomes);
                return Color.Lerp(Color.Lerp(Color.Sienna, Color.Black, 0.5f), Color.Sienna, (float)(localheight));
            }
            
            if (IsCliff())
            {
                if (height < 0.5) return GetLightScale(biomes);
                return Color.Lerp(Color.Lerp(Color.DarkGray,Color.Black,0.5f), Color.LightGray, (float)(localheight));
            }
            if (height < 0.5) return Color.Blue;
            */
            return GetRealColor();//GetLightScale(biomes);
        }
    }
    
    class MapCloseup
    {
        Rectangle boundingRect, mapPosRect, zoom1, panRect;
        Texture2D texture, miniTexture, trees;
        SubBiome[,] current;
        Biome[,] wrld;
        int zoom = 0;
        public bool render;
        //Rectangle[,] grid;
        //SubBiome[,][,] active;
        public void updateWrld(Biome[,] wrld)
        {
            this.wrld = wrld;
        }
        public MapCloseup(GraphicsDevice graphicsDevice, Biome[,] biomes, Point originScreen, Point resolutionScreen, Point initialResolution)
        {
            render = false;
            //active = new SubBiome[15,15][,];
            //grid = new Rectangle[15, 15];
            boundingRect = new Rectangle(originScreen, resolutionScreen);
            panRect = new Rectangle(new Point(50, 50), new Point(resolutionScreen.X - 100, resolutionScreen.Y - 100));
            mapPosRect = new Rectangle(new Point(0, 0), initialResolution);
            zoom1 = new Rectangle(new Point(12, 12), new Point(initialResolution.X/2, initialResolution.Y/2));
            texture = new Texture2D(graphicsDevice, resolutionScreen.X, resolutionScreen.Y);
            trees= new Texture2D(graphicsDevice, resolutionScreen.X, resolutionScreen.Y);
            miniTexture = new Texture2D(graphicsDevice, initialResolution.X, initialResolution.Y);
            current = World.GenerateSubBiomes(mapPosRect, biomes);
            
            miniTexture.SetData(Outline(mapPosRect));
            wrld = biomes;
            /*for(int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    grid[i, j] = new Rectangle(new Point(i*50,j*50), new Point(50, 50));
                    if(grid[i, j].Intersects(mapPosRect))
                    {
                        active[i,j]= World.GenerateSubBiomes(grid[i, j], biomes);
                    }
                    
                }
            }*/
            texture.SetData(scaleColor(current, boundingRect.Width));
            trees.SetData(TreeOverlay(current, boundingRect.Width));
        }
        /*
        public Color[] Display()
        {
            Color[] color = new Color[mapPosRect.Width * 5 * mapPosRect.Height * 5];
            for (int i = 0; i < mapPosRect.Width; i++)
            {

                for (int j = 0; j < mapPosRect.Height; j++)
                {
                    Point position = new Point(i + mapPosRect.X, j + mapPosRect.Y);
                    for(int x = 0; x < 15; x++)
                    {
                        for (int y = 0; y < 15; y++)
                        {
                            if (grid[x, y].Intersects(mapPosRect))
                            {
                                for (int a = 0; a < 5; a++)
                                {
                                    for (int b = 0; b < 5; b++)
                                    {
                                        color[i * 5 + a+ (j * 5 + b)* mapPosRect.Width * 5] = active[x,y][(position.X - grid[x, y].X) * 5 + a, (position.Y - grid[x, y].Y) * 5 + b].GetColor();
                                    }
                                }
                            }
                        }
                    }
                    

                }
            }
            return color;
        }
        public SubBiome[,] Coolio()
        {
            SubBiome[,] subBiomes = new SubBiome[mapPosRect.Width*5, mapPosRect.Height*5];
            for (int i = 0; i < mapPosRect.Width; i++)
            {

                for (int j = 0; j < mapPosRect.Height; j++)
                {
                    Point position = new Point(i + mapPosRect.X, j + mapPosRect.Y);
                    for (int x = 0; x < 15; x++)
                    {
                        for (int y = 0; y < 15; y++)
                        {
                            if (grid[x, y].Intersects(mapPosRect) && grid[x, y].Contains(position))
                            {
                                for (int a = 0; a < 5; a++)
                                {
                                    for (int b = 0; b < 5; b++)
                                    {
                                        int temp = (position.X - grid[x, y].X) * 5 + a;
                                        int tempo = (position.Y - grid[x, y].Y) * 5 + b;
                                        subBiomes[i * 5 + a, j * 5 + b]= active[x, y][(position.X - grid[x, y].X) * 5 + a, (position.Y - grid[x, y].Y) * 5 + b];
                                    }
                                }
                            }
                        }
                    }


                }
            }
            return subBiomes;
        }
        public void UpdatePos(Point newPos)
        {

            mapPosRect.X = newPos.X - mapPosRect.Width / 2;
            mapPosRect.Y = newPos.Y - mapPosRect.Height / 2;
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    if (grid[x, y].Intersects(mapPosRect))
                    {
                        if(active[x, y] == null)
                        active[x, y] = World.GenerateSubBiomes(grid[x, y], wrld);
                    }
                    else
                    {
                        active[x, y] = null;
                    }
                }
            }
            texture.SetData(scaleColor(Coolio(), boundingRect.Width));
        }*/
        public void ChangePos(Point newPos)
        {
            Rectangle old = mapPosRect;
            if (old.Contains(newPos) && render)
            {
                render = false;
                
                return;
            }
            zoom = 0;
            mapPosRect.X = newPos.X - mapPosRect.Width / 2;
            mapPosRect.Y = newPos.Y - mapPosRect.Height / 2;
            current = World.GenerateSubBiomes(mapPosRect,old, wrld, current);
            texture.SetData(scaleColor(current, boundingRect.Width));
            trees.SetData(TreeOverlay(current, boundingRect.Width));
            render = true;
        }
        //needs a lot of work bro! skip it fucker

        public void Pan(Point mouse)
        {
            if (zoom==1 && !panRect.Contains(mouse))
            {
                if (mouse.X > 325  && zoom1.X<mapPosRect.X+mapPosRect.Width)
                {
                    zoom1.X += 1;
                    
                }
                else if(zoom1.X+zoom1.Width > mapPosRect.X)
                {
                    zoom1.X -= 1;
                }
                
                SubBiome[,] temp = World.GenerateSubBiomes(zoom1, mapPosRect, wrld, current);
                texture.SetData(scaleColor(temp, boundingRect.Width));
                trees.SetData(TreeOverlay(temp, boundingRect.Width));
            }
        }
        //needs work!
        public void Zoom(Point newPos)
        {
            if (zoom == 0)
            {
                zoom = 1;
                /*if((mapPosRect.X + (newPos.X / 15) - zoom1.Width / 2< mapPosRect.X) && (mapPosRect.Y + (newPos.Y / 15) - zoom1.Height / 2 < mapPosRect.Y))
                {

                }*/
                zoom1.X = mapPosRect.X + (newPos.X / 15) - zoom1.Width / 2;
                zoom1.Y = mapPosRect.Y + (newPos.Y / 15) - zoom1.Height / 2;

                SubBiome[,] temp = World.GenerateSubBiomes(zoom1, mapPosRect, wrld, current);
                texture.SetData(scaleColor(temp, boundingRect.Width));
                trees.SetData(TreeOverlay(temp, boundingRect.Width));
            }
            else if (zoom == 1)
            {
                zoom = 0;

                texture.SetData(scaleColor(current, boundingRect.Width));
                trees.SetData(TreeOverlay(current, boundingRect.Width));
            }
            

        }
        private Color[] scaleColor(SubBiome[,] subBiomes, int targetSize)
        {
            Color[] color = new Color[targetSize * targetSize];
            int size = subBiomes.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int a = 0; a < targetSize / size; a++)
                    {
                        for (int b = 0; b < targetSize / size; b++)
                        {
                            if (subBiomes[i, j] == null) color[(i * (targetSize / size) + a) + targetSize * (j * (targetSize / size) + b)] = Color.Black;
                            else color[(i * (targetSize / size) + a) + targetSize * (j * (targetSize / size) + b)] = subBiomes[i, j].GetColor();
                        }
                    }
                }
            }
            return color;
        }
        private Color[] Outline(Rectangle rect)
        {
            Color[] color = new Color[rect.Width * rect.Height];
            for (int i = 0; i < rect.Width; i++)
            {
                for (int j = 0; j < rect.Height; j++)
                {
                    if (i < 1 || j < 1 || i > rect.Width - 2 || j > rect.Height - 2) color[i + rect.Width * j] = Color.Black;
                    else color[i + rect.Width * j] = Color.Transparent;
                }
            }
            return color;
        }
        public Color[] TreeOverlay(SubBiome[,] subBiomes, int size)
        {
            Color[] treeOverlay = new Color[size * size];
            Color[,] temp = new Color[size, size];
            int sizeO = subBiomes.GetLength(0);
            int repeat = size / sizeO;
            for (int i = 0; i < sizeO; i++)
            {
                for (int j = 0; j < sizeO; j++)
                {
                    if (subBiomes[i, j].tree)
                    {
                        for (int a = 0; a < repeat; a++)
                        {
                            for (int b = 0; b < repeat; b++)
                            {
                                if (zoom == 1)
                                {
                                    /*if (subBiomes[i, j].humidity > 0.95)
                                    {

                                        if ((a == 0 && b == 0) || (a == repeat - 1 && b == 0) || (a == 0 && b == repeat - 1) || (a == repeat - 1 && b == repeat - 1))
                                        {
                                            temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                        }
                                        else
                                        {
                                            if (subBiomes[i, j].height < 0.65)
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                            }
                                            else
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                            }
                                        }


                                    }
                                    else */if (subBiomes[i, j].humidity > 0.65)
                                    {
                                        if (subBiomes[i, j].humidity % 0.0005 > 0.00025)
                                        {
                                            if (a == repeat - 1 || b == repeat - 1 || (a == 0 && b == 0) || (a == repeat - 2 && b == 0) || (a == 0 && b == repeat - 2) || (a == repeat - 2 && b == repeat - 2))
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                            }
                                            else
                                            {
                                                if (subBiomes[i, j].height < 0.65)
                                                {
                                                    temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                                }
                                                else
                                                {
                                                    temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (a == 0 || b == 0 || (a == repeat - 1 && b == repeat - 1) || (a == 1 && b == repeat - 1) || (a == repeat - 1 && b == 1) || (a == 1 && b == 1))
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                            }
                                            else
                                            {
                                                if (subBiomes[i, j].height < 0.65)
                                                {
                                                    temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                                }
                                                else
                                                {
                                                    temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (subBiomes[i, j].humidity % 0.0005 > 0.00025)
                                        {
                                            if (a == 0 || b == 0 || a == repeat - 1 || b == repeat - 1 || (a == repeat - 2 && b == repeat - 2))
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                            }
                                            else
                                            {
                                                if (subBiomes[i, j].height < 0.65)
                                                {
                                                    temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                                }
                                                else
                                                {
                                                    temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (a == 0 || b == 0 || a == repeat - 1 || b == repeat - 1 || (a == 1 && b == 1))
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                            }
                                            else
                                            {
                                                if (subBiomes[i, j].height < 0.65)
                                                {
                                                    temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                                }
                                                else
                                                {
                                                    temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                                }
                                            }
                                        }

                                    }


                                }
                                else if (zoom == 0)
                                {
                                    if(subBiomes[i, j].humidity > 0.65)
                                    {
                                        if ((a == 0 && b == 0) || (a == repeat - 1 && b == 0) || (a == 0 && b == repeat - 1) || (a == repeat - 1 && b == repeat - 1))
                                        {
                                            temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                        }
                                        else
                                        {
                                            if (subBiomes[i, j].height < 0.65)
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                            }
                                            else
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if ((a == 0 || b == 0) ||(a==1&&b==1))
                                        {
                                            temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                        }
                                        else
                                        {
                                            if (subBiomes[i, j].height < 0.65)
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                            }
                                            else
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                            }
                                        }
                                    }
                                    
                                }
                                //temp[i * repeat + a, j * repeat + b] = Color.Sienna;
                                



                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < repeat; a++)
                        {
                            for (int b = 0; b < repeat; b++)
                            {
                                temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    treeOverlay[i + j * size] = temp[i, j];
                }
            }
            return treeOverlay;
        }
        public void Render(SpriteBatch sb)
        {
            if (render)
            {
                sb.Draw(texture, boundingRect, Color.White);
                sb.Draw(miniTexture, mapPosRect, Color.White);
                if (zoom == 1)
                {
                    sb.Draw(miniTexture, zoom1, Color.Red);
                }
                sb.Draw(trees, boundingRect, Color.White);
            }
        }
        
    }
    class PlayArea
    {
        Rectangle boundingRect;
        Texture2D texture,trees;
        int scale;
        
        Color[] color;



        private Color[] scaleColor2(SubBiome[,] subBiomes, int targetSize)
        {
            Color[] color = new Color[targetSize * targetSize];
            int size = subBiomes.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int a = 0; a < targetSize / size; a++)
                    {
                        for (int b = 0; b < targetSize / size; b++)
                        {
                            color[(i * (targetSize / size) + a) + targetSize * (j * (targetSize / size) + b)] = subBiomes[i, j].GetColor();
                        }
                    }
                }
            }
            return color;
        }

        public Color[] TreeOverlay(SubBiome[,] subBiomes, int size)
        {
            Color [] treeOverlay = new Color[size*size];
            Color[,] temp = new Color[size, size];
            int sizeO = subBiomes.GetLength(0);
            int repeat = size / sizeO;
            for(int i=0; i < sizeO; i++)
            {
                for (int j = 0; j < sizeO; j++)
                {
                    if (subBiomes[i,j].tree )
                    {
                        for (int a = 0; a < repeat; a++)
                        {
                            for (int b = 0; b < repeat; b++)
                            {
                                //temp[i * repeat + a, j * repeat + b] = Color.Sienna;
                                if (subBiomes[i, j].humidity > 0.95)
                                {
                                    
                                        if ((a == 0 && b == 0) || (a == repeat - 1 && b == 0) || (a == 0 && b == repeat - 1) || (a == repeat - 1 && b == repeat - 1))
                                        {
                                            temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                        }
                                        else
                                        {
                                            if (subBiomes[i, j].height < 0.65)
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                            }
                                            else
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                            }
                                        }
                                    

                                }
                                else if (subBiomes[i, j].humidity > 0.65)
                                {
                                    if (subBiomes[i, j].humidity % 0.0005 > 0.00025)
                                    {
                                        if (a == repeat - 1 || b == repeat - 1 || (a == 0 && b == 0) || (a == repeat - 2 && b == 0) || (a == 0 && b == repeat - 2) || (a == repeat - 2 && b == repeat - 2))
                                        {
                                            temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                        }
                                        else
                                        {
                                            if (subBiomes[i, j].height < 0.65)
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                            }
                                            else
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (a == 0 || b == 0 || (a == repeat - 1 && b == repeat - 1) || (a == 1 && b == repeat - 1) || (a == repeat - 1 && b == 1) || (a == 1 && b == 1))
                                        {
                                            temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                        }
                                        else
                                        {
                                            if (subBiomes[i, j].height < 0.65)
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                            }
                                            else
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                            }
                                        }
                                    }
                                    
                                }
                                else
                                {
                                    if (subBiomes[i, j].humidity % 0.0005 > 0.00025)
                                    {
                                        if (a == 0 || b == 0 || a == repeat - 1 || b == repeat - 1 || (a == repeat - 2 && b == repeat - 2))
                                        {
                                            temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                        }
                                        else
                                        {
                                            if (subBiomes[i, j].height < 0.65)
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                            }
                                            else
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (a == 0 || b == 0 || a == repeat - 1 || b == repeat - 1 || (a == 1 && b == 1))
                                        {
                                            temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                                        }
                                        else
                                        {
                                            if (subBiomes[i, j].height < 0.65)
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Lerp(Color.Firebrick, Color.SaddleBrown, 0.3f), Color.SaddleBrown, (float)(((subBiomes[i, j].height - 0.52) / 0.13) - ((subBiomes[i, j].height - 0.52) / 0.13) % 0.154));
                                            }
                                            else
                                            {
                                                temp[i * repeat + a, j * repeat + b] = Color.Lerp(Color.Sienna, Color.BurlyWood, (float)(((subBiomes[i, j].height - 0.65) / 0.15) - ((subBiomes[i, j].height - 0.65) / 0.15) % 0.1333));
                                            }
                                        }
                                    }
                                    
                                }
                                


                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < repeat; a++)
                        {
                            for (int b = 0; b < repeat; b++)
                            {
                                temp[i * repeat + a, j * repeat + b] = Color.Transparent;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    treeOverlay[i  + j*size] = temp[i, j];
                }
            }
            return treeOverlay;
        }
        public PlayArea(int scale,int x,int y,Container cell, Biome[,] biomes, GraphicsDevice graphicsDevice)
        {
            this.scale = scale;
            texture = new Texture2D(graphicsDevice, cell.size * scale, cell.size * scale);
            trees = new Texture2D(graphicsDevice, cell.size * scale, cell.size * scale);
            boundingRect = new Rectangle(x, y, cell.size * scale, cell.size * scale);
            SubBiome[,] subBiomes = World.GenerateSubBiomes(cell, biomes);
            
            trees.SetData(TreeOverlay(subBiomes, cell.size * scale));
            color =scaleColor2(subBiomes, cell.size * scale);
            
            texture.SetData(color);

        }
        public void ChangeCell(Container cell, Biome[,] biomes)
        {
            SubBiome[,] subBiomes = World.GenerateSubBiomes(cell, biomes);
            
            
            trees.SetData(TreeOverlay(subBiomes, cell.size * scale));
            //color = scaleColor(temp, cell.size * scale);
            color = scaleColor2(subBiomes, cell.size * scale);
            texture.SetData(color);
        }
        public void Render(SpriteBatch sb)
        {
            sb.Draw(texture, boundingRect, Color.White);
            sb.Draw(trees, boundingRect, Color.White);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;

namespace UntitledProject
{
    struct Filter
    {
        //Rectangle world;
        List<double[]> circles;
        public Filter(bool useless)
        {
            //this.world = new Rectangle(0, 0, constants.GetLength(0) * 100, constants.GetLength(0) * 100);
            circles = new List<double[]>();
        }

        public void addCircle(double[] data)
        {
            circles.Add(data);
        }

        public float[,] getFilter(byte[,] data, double floor, int ease, bool inverse)
        {
            int xLen = data.GetLength(0);
            int yLen = data.GetLength(1);
            float[,] values = new float[xLen, yLen];
            double temp = 0;
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    temp = (data[i, j] / 255.0);
                    if (temp < floor)
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = (temp - floor) / (1 - floor);
                    }
                    if (ease == 2)
                    {
                        temp = temp * temp;
                    }
                    else if (ease == 3)
                    {
                        temp = temp * temp * temp;
                    }
                    else if (ease == 4)
                    {
                        temp = temp * temp * temp * temp;
                    }
                    else if (ease == 5)
                    {
                        temp = 1 - Math.Sqrt(1 - Math.Pow(temp, 2));
                    }

                    if (inverse) values[i, j] = (float)(1 - temp);
                    else values[i, j] = (float)temp;
                }
            }
            return values;
        }
        public float[,] getFilterS(Rectangle part, byte[,] data, double floor, int ease, bool inverse)
        {
            int xLen = data.GetLength(0);
            int yLen = data.GetLength(1);
            float[,] values = new float[xLen, yLen];
            double temp = 0;
            double y, help;
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    y = part.Y + j;
                    temp = (data[i, j] / 255.0);
                    if (temp < floor)
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = (temp - floor) / (1 - floor);
                    }
                    if (ease == 2)
                    {
                        temp = temp * temp;
                    }
                    else if (ease == 3)
                    {
                        temp = temp * temp * temp;
                    }
                    else if (ease == 4)
                    {
                        temp = temp * temp * temp * temp;
                    }
                    else if (ease == 5)
                    {
                        temp = 1 - Math.Sqrt(1 - Math.Pow(temp, 2));
                    }
                    if (inverse) values[i, j] = (float)((1 - temp));
                    else values[i, j] = (float)(temp);
                }
            }
            return values;
        }
        public float[,] getFilterS(Rectangle part, byte[,] data, double floor, int ease, bool inverse, Point equator)
        {
            int xLen = data.GetLength(0);
            int yLen = data.GetLength(1);
            float[,] values = new float[xLen, yLen];
            double temp = 0;
            double y, help;
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    y = part.Y + j;
                    temp = (data[i, j] / 255.0);
                    if (temp < floor)
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = (temp - floor) / (1 - floor);
                    }
                    if (ease == 2)
                    {
                        temp = temp * temp;
                    }
                    else if (ease == 3)
                    {
                        temp = temp * temp * temp;
                    }
                    else if (ease == 4)
                    {
                        temp = temp * temp * temp * temp;
                    }
                    else if (ease == 5)
                    {
                        temp = 1 - Math.Sqrt(1 - Math.Pow(temp, 2));
                    }
                    help = (1 - Math.Abs(y - equator.X) / equator.Y);
                    if (help < 0) help = 0;
                    if (inverse) values[i, j] = (float)((1 - temp) * help);
                    else values[i, j] = (float)(temp * help);
                }
            }
            return values;
        }
        public float[,] getFilter(Rectangle part, byte[,] data, double floor, int ease, bool inverse, Point equator)
        {
            int xLen = data.GetLength(0);
            int yLen = data.GetLength(1);
            double stepY = (part.Height / (double)yLen);
            float[,] values = new float[xLen, yLen];
            double temp = 0;
            double y, help;
            for (int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    y = part.Y + stepY * j;
                    temp = (data[i, j] / 255.0);
                    if (temp < floor)
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = (temp - floor) / (1 - floor);
                    }
                    if (ease == 2)
                    {
                        temp = temp * temp;
                    }
                    else if (ease == 3)
                    {
                        temp = temp * temp * temp;
                    }
                    else if (ease == 4)
                    {
                        temp = temp * temp * temp * temp;
                    }
                    else if (ease == 5)
                    {
                        temp = 1 - Math.Sqrt(1 - Math.Pow(temp, 2));
                    }
                    help = (1 - Math.Abs(y - equator.X) / equator.Y);
                    if (help < 0) help = 0;
                    if (inverse) values[i, j] = (float)((1 - temp) * help);
                    else values[i, j] = (float)(temp * help);
                }
            }
            return values;
        }

        public float[,] getFilter(Rectangle part, Point resolution)
        {
            float[,] values = new float[resolution.X, resolution.Y];
            double stepX = (part.Width / (double)resolution.X);
            double stepY = (part.Height / (double)resolution.Y);
            double max = 0;
            double temp = 0;
            double x, y;
            for (int i = 0; i < resolution.X; i++)
            {
                x = part.X + stepX * i;
                for (int j = 0; j < resolution.Y; j++)
                {
                    max = 0;
                    y = part.Y + stepY * j;
                    foreach (double[] data in circles)
                    {
                        temp = (1.0 - (Math.Sqrt(Math.Pow((x - data[0]), 2) + Math.Pow((y - data[1]), 2)) / data[2]));

                        if (data[3] == 2)
                        {
                            temp = (1 - (1 - temp) * (1 - temp));
                        }
                        else if (data[3] == 3)
                        {
                            temp = (((temp - 1) * (temp - 1) * (temp - 1) + 1));
                        }
                        else if (data[3] == 4)
                        {
                            temp = (-1 * ((temp - 1) * (temp - 1) * (temp - 1) * (temp - 1) - 1));
                        }
                        else if (data[3] == 5)
                        {
                            temp = Math.Sqrt(1 - (temp - 1) * (temp - 1));
                        }

                        if (temp * data[4] > max)
                        {
                            max = temp * data[4];
                        }
                    }
                    values[i, j] = (float)max;
                    //quartic out ease
                    //values[i, j] = (float)(-1*((max-1)* (max - 1) * (max - 1) * (max-1)-1));
                    //cubic out ease
                    //values[i, j] = (float)(((max-1) * (max - 1) * (max-1)+1));
                    //quadradic out ease
                    //values[i, j] = (float)(1 - (1 - max) * (1 - max));
                    //ease in out quad
                    //values[i, j] = (float)(max < 0.5 ? 2 * max * max : 1 - Math.Pow(-2 * max + 2, 2) / 2);
                    //easeinoutCubic
                    //values[i, j] = (float)(max < 0.5 ? 4 * max * max * max : 1 - Math.Pow(-2 * max + 2, 3) / 2);
                    //circle out ease
                    //values[i, j] = (float)Math.Sqrt(1- (max - 1)* (max - 1));
                    //values[i, j] = (float)max;
                    //values[i, j] = Fade((float)max);
                }
            }
            return values;
        }
        public float[,] getSFilter(Rectangle part, Point equator)
        {
            float[,] values = new float[part.Width, part.Height];
            double max = 0, temp = 0;
            double x, y, help;
            for (int i = 0; i < part.Width; i++)
            {
                x = part.X + i;
                for (int j = 0; j < part.Height; j++)
                {
                    max = 0;
                    y = part.Y + j;
                    foreach (double[] data in circles)
                    {
                        temp = (1.0 - (Math.Sqrt(Math.Pow((x - data[0]), 2) + Math.Pow((y - data[1]), 2)) / data[2]));

                        if (data[3] == 2)
                        {
                            temp = (1 - (1 - temp) * (1 - temp));
                        }
                        else if (data[3] == 3)
                        {
                            temp = (((temp - 1) * (temp - 1) * (temp - 1) + 1));
                        }
                        else if (data[3] == 4)
                        {
                            temp = (-1 * ((temp - 1) * (temp - 1) * (temp - 1) * (temp - 1) - 1));
                        }
                        else if (data[3] == 5)
                        {
                            temp = Math.Sqrt(1 - (temp - 1) * (temp - 1));
                        }

                        if (temp * data[4] > max)
                        {
                            max = temp * data[4];
                        }
                    }
                    help = (1 - Math.Abs(y - equator.X) / equator.Y);
                    if (help < 0) help = 0;
                    values[i, j] = (float)(max * help);

                }
            }
            return values;
        }

        public float[,] getSFilter(Rectangle part)
        {
            float[,] values = new float[part.Width, part.Height];
            double max = 0, temp = 0;
            double x, y, help;
            for (int i = 0; i < part.Width; i++)
            {
                x = part.X + i;
                for (int j = 0; j < part.Height; j++)
                {
                    max = 0;
                    y = part.Y + j;
                    foreach (double[] data in circles)
                    {
                        temp = (1.0 - (Math.Sqrt(Math.Pow((x - data[0]), 2) + Math.Pow((y - data[1]), 2)) / data[2]));

                        if (data[3] == 2)
                        {
                            temp = (1 - (1 - temp) * (1 - temp));
                        }
                        else if (data[3] == 3)
                        {
                            temp = (((temp - 1) * (temp - 1) * (temp - 1) + 1));
                        }
                        else if (data[3] == 4)
                        {
                            temp = (-1 * ((temp - 1) * (temp - 1) * (temp - 1) * (temp - 1) - 1));
                        }
                        else if (data[3] == 5)
                        {
                            temp = Math.Sqrt(1 - (temp - 1) * (temp - 1));
                        }

                        if (temp * data[4] > max)
                        {
                            max = temp * data[4];
                        }
                    }
                    values[i, j] = (float)(max);

                }
            }
            return values;
        }
        public float[,] getFilter(Rectangle part, Point resolution, Point equator)
        {
            float[,] values = new float[resolution.X, resolution.Y];
            double stepX = (part.Width / (double)resolution.X);
            double stepY = (part.Height / (double)resolution.Y);
            double max = 0;
            double temp = 0;
            double x, y, help;
            for (int i = 0; i < resolution.X; i++)
            {
                x = part.X + stepX * i;
                for (int j = 0; j < resolution.Y; j++)
                {
                    max = 0;
                    y = part.Y + stepY * j;
                    foreach (double[] data in circles)
                    {
                        temp = (1.0 - (Math.Sqrt(Math.Pow((x - data[0]), 2) + Math.Pow((y - data[1]), 2)) / data[2]));

                        if (data[3] == 2)
                        {
                            temp = (1 - (1 - temp) * (1 - temp));
                        }
                        else if (data[3] == 3)
                        {
                            temp = (((temp - 1) * (temp - 1) * (temp - 1) + 1));
                        }
                        else if (data[3] == 4)
                        {
                            temp = (-1 * ((temp - 1) * (temp - 1) * (temp - 1) * (temp - 1) - 1));
                        }
                        else if (data[3] == 5)
                        {
                            temp = Math.Sqrt(1 - (temp - 1) * (temp - 1));
                        }

                        if (temp * data[4] > max)
                        {
                            max = temp * data[4];
                        }
                    }
                    help = (1 - Math.Abs(y - equator.X) / equator.Y);
                    if (help < 0) help = 0;
                    values[i, j] = (float)(max * help);

                }
            }
            return values;
        }

        public float[,] getInverseFilter(Rectangle part, Point resolution)
        {
            float[,] values = new float[resolution.X, resolution.Y];
            double stepX = (part.Width / (double)resolution.X);
            double stepY = (part.Height / (double)resolution.Y);
            double max = 0;
            double temp = 0;
            double x, y;
            for (int i = 0; i < resolution.X; i++)
            {
                x = part.X + stepX * i;
                for (int j = 0; j < resolution.Y; j++)
                {
                    max = 0;
                    y = part.Y + stepY * j;
                    foreach (double[] data in circles)
                    {
                        temp = (1.0 - (Math.Sqrt(Math.Pow((x - data[0]), 2) + Math.Pow((y - data[1]), 2)) / data[2]));

                        if (data[3] == 2)
                        {
                            temp = (1 - (1 - temp) * (1 - temp));
                        }
                        else if (data[3] == 3)
                        {
                            temp = (((temp - 1) * (temp - 1) * (temp - 1) + 1));
                        }
                        else if (data[3] == 4)
                        {
                            temp = (-1 * ((temp - 1) * (temp - 1) * (temp - 1) * (temp - 1) - 1));
                        }
                        else if (data[3] == 5)
                        {
                            temp = Math.Sqrt(1 - (temp - 1) * (temp - 1));
                        }

                        if (temp * data[4] > max)
                        {
                            max = temp * data[4];
                        }
                    }
                    values[i, j] = 1 - (float)max;

                }
            }
            return values;
        }
        public float[,] getInverseFilter(Rectangle part, Point resolution, Point equator)
        {
            float[,] values = new float[resolution.X, resolution.Y];
            double stepX = (part.Width / (double)resolution.X);
            double stepY = (part.Height / (double)resolution.Y);
            double max = 0;
            double temp = 0;
            double x, y, help;
            for (int i = 0; i < resolution.X; i++)
            {
                x = part.X + stepX * i;
                for (int j = 0; j < resolution.Y; j++)
                {
                    max = 0;
                    y = part.Y + stepY * j;
                    foreach (double[] data in circles)
                    {
                        temp = (1.0 - (Math.Sqrt(Math.Pow((x - data[0]), 2) + Math.Pow((y - data[1]), 2)) / data[2]));

                        if (data[3] == 2)
                        {
                            temp = (1 - (1 - temp) * (1 - temp));
                        }
                        else if (data[3] == 3)
                        {
                            temp = (((temp - 1) * (temp - 1) * (temp - 1) + 1));
                        }
                        else if (data[3] == 4)
                        {
                            temp = (-1 * ((temp - 1) * (temp - 1) * (temp - 1) * (temp - 1) - 1));
                        }
                        else if (data[3] == 5)
                        {
                            temp = Math.Sqrt(1 - (temp - 1) * (temp - 1));
                        }

                        if (temp * data[4] > max)
                        {
                            max = temp * data[4];
                        }
                    }
                    help = (1 - Math.Abs(y - equator.X) / equator.Y);
                    if (help < 0) help = 0;
                    values[i, j] = (float)((1 - max) * help);

                }
            }
            return values;
        }
    }
    class TheNewWorld
    {
        public static byte[,] DSPN_mix(Rectangle part, Point resolution, float[,][] constants, int baseOctaves, float perlinPersitance, byte maxV, byte minV, float[,] filter, float dsWeight)
        {
            int seed=new Random().Next(int.MaxValue);
            byte[,] currentP = GeneratePerlinValuesV2(part, resolution, constants, baseOctaves, perlinPersitance);//,maxV,minV,filter);
            byte[,] currentDS = Diamond_square(resolution,seed,currentP[0,0], currentP[0, currentP.GetLength(1)-1], currentP[currentP.GetLength(0)-1, 0], currentP[currentP.GetLength(0)-1, currentP.GetLength(1)-1]);
            int tempval=0;
            for(int i = 0;i< currentP.GetLength(0); i++)
            {
                for (int j= 0;j< currentP.GetLength(1); j++)
                {
                    tempval =(int) (currentP[i, j] + dsWeight * currentDS[i, j]);
                    if (tempval > 255)
                    {
                        tempval = 255;
                    }
                    else if (tempval<0)
                    {
                        tempval = 0;
                    }
                    currentP[i, j] = (byte)tempval;
                }
            }
            return currentP;
        }

        public static void TexturizeDS(ref byte[,] p, float dsWeight)
        {
            byte[,] currentDS = Diamond_square(new Point(p.GetLength(0), p.GetLength(1)), new Random().Next(int.MaxValue), p[0, 0], p[0, p.GetLength(1) - 1], p[p.GetLength(0) - 1, 0], p[p.GetLength(0) - 1, p.GetLength(1) - 1]);
            int tempval = 0;
            for (int i = 0; i < p.GetLength(0); i++)
            {
                for (int j = 0; j < p.GetLength(1); j++)
                {
                    tempval = (int)(p[i, j] + dsWeight * currentDS[i, j]);
                    if (tempval > 255)
                    {
                        tempval = 255;
                    }
                    else if (tempval < 0)
                    {
                        tempval = 0;
                    }
                    p[i, j] = (byte)tempval;
                }
            }
        }
        private static byte Average(byte f1, byte f2, byte f3, byte f4)
        {
            return (byte)((f1 + f2 + f3 + f4) /4);
        }
        private static byte Average(byte f1, byte f2, byte f3)
        {
            return (byte)((f1 + f2 + f3 ) / 3);
        }
        public static byte[,] Diamond_square(Point psize, int seed, int s1, int s2, int s3, int s4)
        {
            Random random = new Random(seed);
            int size;
            if (psize.X != psize.Y)
            {
                size = (psize.X > psize.Y) ? psize.X : psize.Y;
            }
            else
            {
                size = psize.X;
            }
            if (size % 2 == 0)
            {
                size++;
            }
            int iterations = 1;
            int temp1 = 3;
            while (temp1 < size)
            {
                temp1 = temp1 * 2 - 1;
                iterations++;
            }
            size = temp1;
            byte[,] builder = new byte[size, size];
            byte[,] result = new byte[psize.X, psize.Y];
            builder[0, 0] = (byte)s1;
            builder[0, size - 1] = (byte)s2;
            builder[size - 1, 0] = (byte)s3;
            builder[size - 1, size - 1] = (byte)s4;
            double fixedDistance = size - 1;
            double distance = fixedDistance;
            int nrOfRects, roof, tempd, a;
            double b = 0;
            Rectangle current;
            roof =128;
            a= (int)Math.Round(128.0 / (double)iterations);
            while (distance > 1)
            {

                nrOfRects = (int)Math.Round(fixedDistance / distance);
                distance = Math.Round(distance);
                tempd = (int)distance;
                if (nrOfRects == 1)
                {
                    current = new Rectangle(0, 0, size - 1, size - 1);
                    Diamond(current, ref builder, random.Next(roof * 2) - roof);
                    Sqaure(current, ref builder, random.Next(roof * 2) - roof);

                }
                else
                {
                    for (int i = 0; i < nrOfRects; i++)
                    {
                        for (int j = 0; j < nrOfRects; j++)
                        {
                            if (i == 0 && j == 0)
                            {
                                current = new Rectangle(0, 0, (int)distance, (int)distance);
                                Diamond(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else if (i == 0)
                            {
                                current = new Rectangle(0, (int)distance * j, (int)distance, (int)distance);
                                Diamond(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else if (j == 0)
                            {
                                current = new Rectangle((int)distance * i, 0, (int)distance, (int)distance);
                                Diamond(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else
                            {
                                current = new Rectangle((int)distance * i, (int)distance * j, (int)distance, (int)distance);
                                Diamond(current, ref builder, random.Next(roof * 2) - roof);
                            }

                        }
                    }
                    for (int i = 0; i < nrOfRects; i++)
                    {
                        for (int j = 0; j < nrOfRects; j++)
                        {
                            if (i == 0 && j == 0)
                            {
                                current = new Rectangle(0, 0, (int)distance, (int)distance);
                                Sqaure(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else if (i == 0)
                            {
                                current = new Rectangle(0, (int)distance * j, (int)distance, (int)distance);
                                Sqaure(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else if (j == 0)
                            {
                                current = new Rectangle((int)distance * i, 0, (int)distance, (int)distance);
                                Sqaure(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else
                            {
                                current = new Rectangle((int)distance * i, (int)distance * j, (int)distance, (int)distance);
                                Sqaure(current, ref builder, random.Next(roof * 2) - roof);
                            }
                        }
                    }
                }

                roof-=a;//=(int)(roof*0.9);
                distance /= 2;
                //distance = Math.Floor(distance);
            }
            
            for (int i = 0; i < psize.X; i++)
            {
                for (int j = 0; j < psize.Y; j++)
                {
                    result[i, j] = builder[i, j];
                }
            }

            return result;


        }
        public static byte[,] Diamond_square(Point psize, int seed)
        {
            Random random = new Random(seed);
            int size;
            if (psize.X !=psize.Y)
            {
                size = (psize.X > psize.Y) ? psize.X : psize.Y;
            }
            else
            {
                size = psize.X;
            }
            if (size % 2 == 0)
            {
                size++;
            }
            int temp1 = 3;
            while (temp1 < size)
            {
                temp1 = temp1 * 2 - 1;
            }
            size = temp1;
            List<Rectangle> workload = new List<Rectangle>();
            List<int[]> variances = new List<int[]>();
            byte[,] builder = new byte[size, size];
            byte[,] result = new byte[psize.X, psize.Y];
            builder[0, 0] = (byte) random.Next(255);
            builder[0, size-1] = (byte)random.Next(255);
            builder[size-1, 0] = (byte)random.Next(255);
            builder[size-1, size-1] =(byte)random.Next(255);
            double fixedDistance = size-1;
            double distance= fixedDistance;
            int nrOfRects, roof, tempd, a;
            Rectangle current;
            roof = 35;
            while (distance >1)
            {
                
                nrOfRects = (int)Math.Round(fixedDistance / distance);
                distance=Math.Round(distance);
                tempd =(int) distance;
                if (nrOfRects == 1)
                {
                    current = new Rectangle(0, 0, size-1, size-1);
                    Diamond(current, ref builder, random.Next(roof * 2) - roof);
                    Sqaure(current, ref builder, random.Next(roof * 2) - roof);
                    
                }
                else
                {
                    for (int i = 0; i < nrOfRects; i++)
                    {
                        for (int j = 0; j < nrOfRects; j++)
                        {
                            if(i==0 && j == 0)
                            {
                                current = new Rectangle(0, 0, (int)distance, (int)distance);
                                Diamond(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else if (i == 0)
                            {
                                current = new Rectangle(0, (int)distance * j, (int)distance, (int)distance);
                                Diamond(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else if (j == 0)
                            {
                                current = new Rectangle((int)distance * i, 0, (int)distance, (int)distance);
                                Diamond(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else
                            {
                                current = new Rectangle((int)distance * i, (int)distance * j, (int)distance, (int)distance);
                                Diamond(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            
                            /*workload.Add(new Rectangle(tempd*i, tempd * j, tempd, tempd));
                            variances.Add(new int[2] { random.Next(roof * 2) - roof, random.Next(roof * 2) - roof });*/
                        }
                    }
                    for (int i = 0; i < nrOfRects; i++)
                    {
                        for (int j = 0; j < nrOfRects; j++)
                        {
                            if (i == 0 && j == 0)
                            {
                                current = new Rectangle(0, 0, (int)distance, (int)distance);
                                Sqaure(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else if (i == 0)
                            {
                                current = new Rectangle(0, (int)distance * j, (int)distance, (int)distance);
                                Sqaure(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else if (j == 0)
                            {
                                current = new Rectangle((int)distance * i , 0, (int)distance, (int)distance);
                                Sqaure(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            else
                            {
                                current = new Rectangle((int)distance * i , (int)distance * j, (int)distance, (int)distance);
                                Sqaure(current, ref builder, random.Next(roof * 2) - roof);
                            }
                            /*workload.Add(new Rectangle(tempd*i, tempd * j, tempd, tempd));
                            variances.Add(new int[2] { random.Next(roof * 2) - roof, random.Next(roof * 2) - roof });*/
                        }
                    }
                }

                roof--;//=(int)(roof*0.9);
                distance /= 2;
                //distance = Math.Floor(distance);
            }
            /*roof = 0;
            int ö=0;
            for (int i = 0; i < workload.Count; i += roof)
            {
                roof = (int)(Math.Pow(2, ö)* Math.Pow(2, ö));
                for (a = 0; a < roof; a++)
                {
                    Diamond(workload[i+a], ref builder, variances[i+a][0]);
                }
                for (a = 0; a < roof; a++)
                {
                    Sqaure(workload[i + a], ref builder, variances[i + a][1]);
                }
                ö++;
            }
            */
            for (int i = 0; i < psize.X; i++)
            {
                for (int j = 0; j < psize.Y; j++)
                {
                    result[i, j] = builder[i, j];
                }
            }
            
            return result;
            
            
        }

        private static void Diamond(Rectangle rect, ref byte[,] builder, int varience)
        {

            int data = Average(builder[rect.Left, rect.Top], builder[rect.Left, rect.Bottom], builder[rect.Right, rect.Top], builder[rect.Right, rect.Bottom])+varience;
            if (data < 0)
            {
                builder[rect.Center.X, rect.Center.Y] = 0;
            }
            else if (data>255)
            {
                builder[rect.Center.X, rect.Center.Y] = 255;
            }
            else
            {
                builder[rect.Center.X, rect.Center.Y] = (byte)data;
            }
            
        }
        private static void Sqaure(Rectangle rect, ref byte[,] builder, int varience)
        {
            //data0=N,data1=E,data2=S, data3=W
            int data, data1, data2, data3, distance;
            distance = rect.Center.X - rect.X;

            data=(rect.Top - distance < 0) ?
                Average(builder[rect.Left, rect.Top], builder[rect.Center.X, rect.Center.Y], builder[rect.Right, rect.Top]) + varience :
                Average(builder[rect.Left, rect.Top], builder[rect.Center.X, rect.Center.Y], builder[rect.Right, rect.Top], builder[rect.Center.X, rect.Top-distance]) + varience;
            if (data < 0)
            {
                builder[rect.Center.X, rect.Top] = 0;
            }
            else if (data > 255)
            {
                builder[rect.Center.X, rect.Top] = 255;
            }
            else
            {
                builder[rect.Center.X, rect.Top] = (byte)data;
            }

            data = (rect.Right + distance>=builder.GetLength(0)) ?
                Average(builder[rect.Right, rect.Bottom], builder[rect.Center.X, rect.Center.Y], builder[rect.Right, rect.Top]) + varience :
                Average(builder[rect.Right, rect.Bottom], builder[rect.Center.X, rect.Center.Y], builder[rect.Right, rect.Top], builder[rect.Right+distance, rect.Center.Y]) + varience;
            if (data < 0)
            {
                builder[rect.Right, rect.Center.Y] = 0;
            }
            else if (data > 255)
            {
                builder[rect.Right, rect.Center.Y] = 255;
            }
            else
            {
                builder[rect.Right, rect.Center.Y] = (byte)data;
            }

            data = (rect.Bottom + distance >= builder.GetLength(0)) ?
                Average(builder[rect.Right, rect.Bottom], builder[rect.Center.X, rect.Center.Y], builder[rect.Left, rect.Bottom]) + varience :
                Average(builder[rect.Right, rect.Bottom], builder[rect.Center.X, rect.Center.Y], builder[rect.Left, rect.Bottom], builder[rect.Center.X, rect.Bottom + distance]) + varience;
            if (data < 0)
            {
                builder[rect.Center.X, rect.Bottom] = 0;
            }
            else if (data > 255)
            {
                builder[rect.Center.X, rect.Bottom] = 255;
            }
            else
            {
                builder[rect.Center.X, rect.Bottom] = (byte)data;
            }

            data = (rect.Left - distance < 0) ?
                Average(builder[rect.Left, rect.Top], builder[rect.Center.X, rect.Center.Y], builder[rect.Left, rect.Bottom]) + varience :
                Average(builder[rect.Left, rect.Top], builder[rect.Center.X, rect.Center.Y], builder[rect.Left, rect.Bottom], builder[rect.Left - distance, rect.Center.Y]) + varience;
            if (data < 0)
            {
                builder[rect.Left, rect.Center.Y] = 0;
            }
            else if (data > 255)
            {
                builder[rect.Left, rect.Center.Y] = 255;
            }
            else
            {
                builder[rect.Left, rect.Center.Y] = (byte)data;
            }


        }
        private static float CalculatePerlin(int X, int Y, float xf, float yf, float u, float v, float[,][] constants)
        {
            xf += 0.00000000000001f;
            yf += 0.00000000000001f;
            float[] topLeft = new float[] { xf, yf };
            float[] botLeft = new float[] { xf, yf - 1.0f };
            float[] topRight = new float[] { xf - 1.0f, yf };
            float[] botRight = new float[] { xf - 1.0f, yf - 1.0f };

            float floatvalue = Lerp(u, Lerp(v, dot(topLeft, constants[X, Y]), dot(botLeft, constants[X, Y + 1])), Lerp(v, dot(topRight, constants[X + 1, Y]), dot(botRight, constants[X + 1, Y + 1])));
            //might have to change position of X and Y ^
            return (floatvalue + 1.0f) / 2.0f;
        }
        private static int fastFloor(double x)
        {
            int xi = (int)x;
            return x < xi ? xi - 1 : xi;
        }
        private static float Ease(float val, int ease)
        {
            float temp = val;
            if (ease == 2)
            {
                temp = temp * temp;
            }
            else if (ease == 3)
            {
                temp = temp * temp * temp;
            }
            else if (ease == 4)
            {
                temp = temp * temp * temp * temp;
            }
            else if (ease == 5)
            {
                temp = 1f - MathF.Sqrt(1f - MathF.Pow(temp, 2f));
            }
            return temp;
        }
        public static byte[,] GenerateSimplexNoiseW(Rectangle part, Rectangle boundingWorld, float[,][] gradients, int octave, float persistance, float[,] filter)
        {
            byte[,] returnVal = new byte[boundingWorld.Width, boundingWorld.Height];
            float[] a = GetSimplexSize(gradients);
            float stepX = a[0] / boundingWorld.Width, stepY = a[1] / boundingWorld.Height;
            float x, y, total, amplitude, frequency, max;
            for (int i = 0; i < part.Width; i++)
            {
                x = stepX * (i + part.X);
                for (int j = 0; j < part.Height; j++)
                {
                    if (filter[i, j] == 0.0f) continue;
                    y = stepY * (j + part.Y);
                    frequency = 1.0f;
                    amplitude = 1.0f;
                    total = 0.0f;
                    max = 0.0f;
                    for (int b = 0; b < octave; b++)
                    {
                        total += CalculateSimplex(x * frequency, y * frequency, gradients) * amplitude;
                        max += 1.0f * amplitude;
                        amplitude *= persistance;
                        frequency *= 2.0f;
                    }
                    total = (total / max) * filter[i, j];
                    returnVal[i, j] = (byte)MathF.Round(total * 255.0f);
                }
            }
            return returnVal;
        }
        public static byte[,] GenerateSimplexNoiseW(Rectangle part, ref byte[,] values, float[,][] gradients, int octave, float persistance, float[,] filter)
        {
            byte[,] returnVal = new byte[values.GetLength(0), values.GetLength(1)];
            float[] a = GetSimplexSize(gradients);
            float stepX = a[0] / values.GetLength(0), stepY = a[1] / values.GetLength(1);
            float x, y, total, amplitude, frequency, max;
            for (int i = 0; i < part.Width; i++)
            {
                x = stepX * (i + part.X);
                for (int j = 0; j < part.Height; j++)
                {
                    if (filter[i, j] == 0.0f) continue;
                    y = stepY * (j + part.Y);
                    frequency = 1.0f;
                    amplitude = 1.0f;
                    total = 0.0f;
                    max = 0.0f;
                    for (int b = 0; b < octave; b++)
                    {
                        total += CalculateSimplex(x * frequency+b, y * frequency+b, gradients) * amplitude;
                        max += 1.0f * amplitude;
                        amplitude *= persistance;
                        frequency *= 2.0f;
                    }
                    total = (total / max)* filter[i, j];
                    values[i + part.X, j + part.Y] = (byte)MathF.Round(total * 255.0f);
                    returnVal[i, j] = values[i + part.X, j + part.Y];
                }
            }
            return returnVal;
        }
        public static byte[,] GenerateSimplexNoiseW(Rectangle part, ref byte[,] values, float[,][] gradients, int octave, float persistance)
        {
            byte[,] returnVal = new byte[values.GetLength(0), values.GetLength(1)];
            float[] a = GetSimplexSize(gradients);
            float stepX = a[0] / values.GetLength(0), stepY = a[1] / values.GetLength(1);
            float x, y, total, amplitude, frequency, max;
            for (int i = 0; i < part.Width; i++)
            {
                x = stepX * (i + part.X);
                for (int j = 0; j < part.Height; j++)
                {
                    y = stepY * (j + part.Y);
                    frequency = 1.0f;
                    amplitude = 1.0f;
                    total = 0.0f;
                    max = 0.0f;
                    for (int b = 0; b < octave; b++)
                    {
                        total += CalculateSimplex(x * frequency, y * frequency, gradients) * amplitude;
                        max += 1.0f * amplitude;
                        amplitude *= persistance;
                        frequency *= 2.0f;
                    }
                    total = total / max;
                    values[i + part.X, j + part.Y] = (byte)MathF.Round(total * 255.0f);
                    returnVal[i, j] = values[i + part.X, j + part.Y];
                }
            }
            return returnVal;
        }
        public static void GenerateSimplexNoise(Rectangle part, ref byte[,] values, float[,][] gradients, int octave, float persistance, float[,] filter)
        {
            
            float[] a = GetSimplexSize(gradients);
            float stepX = a[0] / values.GetLength(0), stepY = a[1] / values.GetLength(1);
            float x, y, total, amplitude, frequency, max;
            for (int i = 0; i < part.Width; i++)
            {
                x = stepX * (i + part.X);
                for (int j = 0; j < part.Height; j++)
                {
                    if (filter[i, j] == 0.0f) continue;
                    y = stepY * (j + part.Y);
                    frequency = 1.0f;
                    amplitude = 1.0f;
                    total = 0.0f;
                    max = 0.0f;
                    for (int b = 0; b < octave; b++)
                    {
                        total += CalculateSimplex(x * frequency+b, y * frequency+b, gradients) * amplitude;
                        max += 1.0f * amplitude;
                        amplitude *= persistance;
                        frequency *= 2.0f;
                    }
                    total = (total / max) * filter[i, j];
                    values[i + part.X, j + part.Y] = (byte)MathF.Round(total * 255.0f);

                }
            }
            
        }
        public static void GenerateSimplexNoise(Rectangle part, ref byte[,] values, float[,][] gradients, int octave, float persistance)
        {
            float[] a = GetSimplexSize(gradients);
            float stepX = a[0] / values.GetLength(0), stepY = a[1] / values.GetLength(1);
            float x, y, total, amplitude, frequency, max;
            for (int i = 0; i < part.Width; i++)
            {
                x = stepX * (i + part.X);
                for (int j = 0; j < part.Height; j++)
                {
                    y = stepY * (j + part.Y);
                    frequency = 1.0f;
                    amplitude = 1.0f;
                    total = 0.0f;
                    max = 0.0f;
                    for (int b = 0; b < octave; b++)
                    {
                        total += CalculateSimplex(x * frequency, y * frequency, gradients) * amplitude;
                        max += 1.0f * amplitude;
                        amplitude *= persistance;
                        frequency *= 2.0f;
                    }
                    total = total / max;
                    values[i + part.X, j + part.Y] = (byte)MathF.Round(total * 255.0f);
                }
            }
        }
        public static void GenerateSimplexNoise(Rectangle part, ref byte[,] values, float[,][] gradients, int octave, float persistance, byte[,] filter)
        {
            float[] a = GetSimplexSize(gradients);
            float stepX = a[0] / values.GetLength(0), stepY = a[1] / values.GetLength(1);
            float x, y, total, amplitude, frequency, max;
            for (int i = 0; i < part.Width; i++)
            {
                x = stepX * (i + part.X);
                for (int j = 0; j < part.Height; j++)
                {
                    y = stepY * (j + part.Y);
                    frequency = 1.0f;
                    amplitude = 1.0f;
                    total = 0.0f;
                    max = 0.0f;
                    for (int b = 0; b < octave; b++)
                    {
                        total += CalculateSimplex(x * frequency, y * frequency, gradients) * amplitude;
                        max += 1.0f * amplitude;
                        amplitude *= persistance;
                        frequency *= 2.0f;
                    }
                    total = total / max;
                    values[i + part.X, j + part.Y] = (byte)MathF.Round(total * (filter[i + part.X, j + part.Y] * 0.00392156862f) * 255.0f);
                }
            }
        }
        public static byte[,] GenerateSimplexNoise(Rectangle part, Rectangle boundingWorld, float[,][] gradients, int octave, float persistance, byte[,] filter)
        {
            byte[,] values = new byte[part.Width, part.Height];
            float[] a = GetSimplexSize(gradients);
            float stepX = a[0] / boundingWorld.Width, stepY = a[1] / boundingWorld.Height;
            float x, y, total, amplitude, frequency, max;
            for (int i = 0; i < part.Width; i++)
            {
                x = stepX * (i + part.X);
                for (int j = 0; j < part.Height; j++)
                {
                    y = stepY * (j + part.Y);
                    frequency = 1.0f;
                    amplitude = 1.0f;
                    total = 0.0f;
                    max = 0.0f;
                    for (int b = 0; b < octave; b++)
                    {
                        total += CalculateSimplex(x * frequency, y * frequency, gradients) * amplitude;
                        max += 1.0f * amplitude;
                        amplitude *= persistance;
                        frequency *= 2.0f;
                    }
                    total = total / max;
                    values[i, j] = (byte)MathF.Round(total * (filter[i + part.X, j + part.Y]* 0.00392156862f) * 255.0f);
                }
            }

            return values;
        }
        public static byte[,] GenerateSimplexNoise(Rectangle part, Rectangle boundingWorld, float[,][] gradients, int octave, float persistance, float[,] filter)
        {
            byte[,] values = new byte[part.Width, part.Height];
            float[] a = GetSimplexSize(gradients);
            float stepX = a[0] / boundingWorld.Width, stepY = a[1] / boundingWorld.Height;
            float x, y, total, amplitude, frequency, max;
            for (int i = 0; i < part.Width; i++)
            {
                x = stepX * (i + part.X);
                for (int j = 0; j < part.Height; j++)
                {
                    y = stepY * (j + part.Y);
                    frequency = 1.0f;
                    amplitude = 1.0f;
                    total = 0.0f;
                    max = 0.0f;
                    for (int b = 0; b < octave; b++)
                    {
                        total += CalculateSimplex(x * frequency, y * frequency, gradients) * amplitude;
                        max += 1.0f * amplitude;
                        amplitude *= persistance;
                        frequency *= 2.0f;
                    }
                    total = (total / max)* filter[i, j];
                    values[i, j] = (byte)MathF.Round(total * 255.0f);
                }
            }

            return values;
        }

        //part describes the part of the screen
        public static byte[,] GenerateSimplexNoise(Rectangle part,Rectangle boundingWorld, float[,][] gradients, int octave, float persistance)
        {
            byte[,] values = new byte[part.Width, part.Height];
            float[] a = GetSimplexSize(gradients);
            float stepX = a[0] / boundingWorld.Width, stepY = a[1] / boundingWorld.Height;
            float x, y, total, amplitude, frequency, max;
            for (int i = 0; i < part.Width; i++)
            {
                x = stepX * (i+part.X);
                for (int j = 0; j < part.Height; j++)
                {
                    y = stepY * (j+part.Y);
                    frequency = 1.0f;
                    amplitude = 1.0f;
                    total = 0.0f;
                    max = 0.0f;
                    for (int b = 0; b < octave; b++)
                    {
                        total += CalculateSimplex(x * frequency, y * frequency, gradients) * amplitude;
                        max += 1.0f * amplitude;
                        amplitude *= persistance;
                        frequency *= 2.0f;
                    }
                    total = total / max;
                    values[i, j] = (byte)MathF.Round(total * 255.0f);
                }
            }

            return values;
        }
        public static byte[,] GenerateSimplexNoise(Point resolution, float[,][] gradients, int octave, float persistance)
        {
            byte[,] values = new byte[resolution.X, resolution.Y];
            float[] a = GetSimplexSize(gradients);
            float sizeX = a[0], sizeY = a[1];
            float stepX = sizeX / resolution.X, stepY = sizeY / resolution.Y;
            float x, y, total, amplitude, frequency, max;
            for (int i = 0; i < resolution.X; i++)
            {
                x = stepX * i;
                for (int j = 0; j < resolution.X; j++)
                {
                    y = stepY * j;
                    frequency = 1.0f;
                    amplitude = 1.0f;
                    total = 0.0f;
                    max = 0.0f;
                    for (int b=0; b<octave; b++)
                    {
                        total += CalculateSimplex(x*frequency, y*frequency, gradients)*amplitude;
                        max += 1.0f * amplitude;
                        amplitude *= persistance;
                        frequency *= 2.0f;
                    }
                    total = total / max;
                    values[i, j] = (byte)MathF.Round(total * 255.0f);
                }
            }

            return values;
        }
        public static byte[,] GenerateSimplexNoise(Point resolution, float[,][] gradients)
        {
            byte[,] values = new byte[resolution.X, resolution.Y];
            float[] a = GetSimplexSize(gradients);
            float sizeX=a[0], sizeY=a[1];
            float stepX = sizeX / resolution.X, stepY = sizeY / resolution.Y;
            float x, y;
            for(int i = 0; i < resolution.X; i++)
            {
                x = stepX * i;
                for (int j = 0; j < resolution.X; j++)
                {
                    y = stepY * j;
                    values[i, j] = (byte)MathF.Round(CalculateSimplex(x, y, gradients)*255.0f);
                }
            }

            return values;
        }
        private static float[] GetSimplexSize(float[,][] gradients)
        {
            float displacement = (gradients.GetLength(0) + gradients.GetLength(1)) * -0.211324865405187f;
            return new float[2] { gradients.GetLength(0)+ displacement, gradients.GetLength(1)+displacement };
        }
        private static float CalculateSimplex(float x, float y, float[,][] gradients)
        {
            //skew input to simplex
            float s = 0.366025403784439f * (x + y);
            float xs = (x + s) % (gradients.GetLength(0) - 1), ys = (y + s) % (gradients.GetLength(1) - 1);
            //find cell origin
            int X = fastFloor(xs), Y = fastFloor(ys);
            //unskew cell origin to X/Y space
            float xsi = xs - X, ysi = ys - Y;
            float ssi = (xsi + ysi) * -0.211324865405187f;//my t
            //calculate x,y distances from the cell origin
            float x0 = xsi + ssi, y0 = ysi + ssi;//x0 and y0
            float t0, t1, t2, w0, w1, w2, x1, x2, y1, y2;
            
            if (x0>y0)
            {
                x1 = x0 - 1.0f + 0.211324865405187f;
                x2 = x0 - 1.0f + 2.0f * 0.211324865405187f;
                y1 = y0 + 0.211324865405187f;
                y2 = y0 - 1.0f + 2.0f * 0.211324865405187f;
                t1 = 0.5f - x1 * x1 - y1 * y1;

                if (t1 < 0.0f) w1 = 0f;
                else
                {
                    t1 *= t1;
                    w1 = t1 * t1 * dot(gradients[X+1, Y], new float[2] { x1, y1 });
                }


            }
            else
            {
                x1 = x0 + 0.211324865405187f;
                x2 = x0 - 1.0f + 2.0f * 0.211324865405187f;
                y1 = y0 - 1.0f + 0.211324865405187f;
                y2 = y0 - 1.0f + 2.0f * 0.211324865405187f;
                t1 = 0.5f - x1 * x1 - y1 * y1;

                if (t1 < 0.0f) w1 = 0f;
                else
                {
                    t1 *= t1;
                    w1 = t1 * t1 * dot(gradients[X, Y+1], new float[2] { x1, y1 });
                }
            }
            t0 = 0.5f - x0 * x0 - y0 * y0;

            if (t0 < 0.0f) w0 = 0f;
            else
            {
                t0 *= t0;
                w0 = t0 * t0 * dot(gradients[X, Y], new float[2] { x0, y0 });
            }


            t2 = 0.5f - x2 * x2 - y2 * y2;

            if (t2 < 0.0f) w2 = 0f;
            else
            {
                t2 *= t2;
                w2 = t2 * t2 * dot(gradients[X+1, Y+1], new float[2] { x2, y2 });
            }

            return (70.0f * (w0 + w1 + w2)+1.0f)/2.0f;
        }



        public static byte[] GetTopValue(float[,][] constants, int octaves, float persitance)
        {
            byte[] values = new byte[2] { 0, 255 };
            float stepX = 1 / 20.0f;
            float stepY = 1 / 20.0f;
            float frequency = 1.0f;
            float total, max, amplitude;
            max = 0;
            amplitude = 1;
            int helpX = (constants.GetLength(0) - 1);
            int helpY = (constants.GetLength(1) - 1);
            float[] x, y, xf, yf, u, v;
            x = new float[octaves];
            y = new float[octaves];
            xf = new float[octaves];
            yf = new float[octaves];
            u = new float[octaves];
            v = new float[octaves];
            for (int a = 0; a < octaves; a++)
            {
                max += 1.0f * amplitude;
                amplitude *= persitance;
            }
            int[] X, Y;
            X = new int[octaves];
            Y = new int[octaves];
            for (int i = 0; i < (constants.GetLength(0) - 1) * 20; i++)
            {
                for (int a = 0; a < octaves; a++)
                {
                    x[a] = ((stepX * i) * frequency) % helpX;
                    X[a] = (int)(x[a] - (x[a] % 1.0f));
                    xf[a] = x[a] - X[a];
                    u[a] = Fade(xf[a]);
                    frequency *= 2.0f;
                }
                frequency = 1.0f;
                for (int j = 0; j < (constants.GetLength(0) - 1) * 20; j++)
                {
                    total = 0f;
                    amplitude = 1f;
                    for (int a = 0; a < octaves; a++)
                    {
                        y[a] = ((stepY * j) * frequency) % helpY;
                        Y[a] = (int)(y[a] - (y[a] % 1.0f));
                        yf[a] = y[a] - Y[a];
                        v[a] = Fade(yf[a]);
                        frequency *= 2.0f;
                        total += CalculatePerlin(X[a], Y[a], xf[a], yf[a], u[a], v[a], constants) * amplitude;
                        amplitude *= persitance;
                    }
                    frequency = 1.0f;
                    total = (float)Math.Round((total / max) * 255f);
                    if (total >= values[0])
                    {
                        values[0] = (byte)total;
                    }
                    else if (total <= values[1])
                    {
                        values[1] = (byte)total;
                    }

                }
            }
            return values;
        }

        public static byte[] GetTopValue(float[,][] constants, int octaves, float persitance, float[,] filter)
        {
            byte[] values = new byte[2] { 0, 255 };
            float stepX = 1 / 20.0f;
            float stepY = 1 / 20.0f;
            float frequency = 1.0f;
            float total, max, amplitude;
            max = 0;
            amplitude = 1;
            int helpX = (constants.GetLength(0) - 1);
            int helpY = (constants.GetLength(1) - 1);
            float[] x, y, xf, yf, u, v;
            x = new float[octaves];
            y = new float[octaves];
            xf = new float[octaves];
            yf = new float[octaves];
            u = new float[octaves];
            v = new float[octaves];
            for (int a = 0; a < octaves; a++)
            {
                max += 1.0f * amplitude;
                amplitude *= persitance;
            }
            int[] X, Y;
            X = new int[octaves];
            Y = new int[octaves];
            for (int i = 0; i < (constants.GetLength(0) - 1) * 20; i++)
            {
                for (int a = 0; a < octaves; a++)
                {
                    x[a] = ((stepX * i) * frequency) % helpX;
                    X[a] = (int)(x[a] - (x[a] % 1.0f));
                    xf[a] = x[a] - X[a];
                    u[a] = Fade(xf[a]);
                    frequency *= 2.0f;
                }
                frequency = 1.0f;
                for (int j = 0; j < (constants.GetLength(1) - 1) * 20; j++)
                {
                    total = 0f;
                    amplitude = 1f;
                    for (int a = 0; a < octaves; a++)
                    {
                        y[a] = ((stepY * j) * frequency) % helpY;
                        Y[a] = (int)(y[a] - (y[a] % 1.0f));
                        yf[a] = y[a] - Y[a];
                        v[a] = Fade(yf[a]);
                        frequency *= 2.0f;
                        total += CalculatePerlin(X[a], Y[a], xf[a], yf[a], u[a], v[a], constants) * amplitude;
                        amplitude *= persitance;
                    }
                    frequency = 1.0f;
                    total = (float)Math.Round((total / max) * (filter[i, j]) * 255f);
                    if (total >= values[0])
                    {
                        values[0] = (byte)total;
                    }
                    else if (total <= values[1])
                    {
                        values[1] = (byte)total;
                    }

                }
            }
            return values;
        }

        public static byte[] GetTopValue(float[,][] constants, int octaves, float persitance, float[,] filter, float[] octaveRotation)
        {
            byte[] values = new byte[2] { 0, 255 };
            float stepX = 1 / 20.0f;
            float stepY = 1 / 20.0f;
            float frequency = 1.0f;
            float total, max, amplitude, tempX, tempY;
            max = 0;
            amplitude = 1;
            int helpX = (constants.GetLength(0) - 1);
            int helpY = (constants.GetLength(1) - 1);
            float[] x, y, xf, yf, u, v;
            x = new float[octaves];
            y = new float[octaves];
            xf = new float[octaves];
            yf = new float[octaves];
            u = new float[octaves];
            v = new float[octaves];
            for (int a = 0; a < octaves; a++)
            {
                max += 1.0f * amplitude;
                amplitude *= persitance;
            }
            int[] X, Y;
            X = new int[octaves];
            Y = new int[octaves];
            for (int i = 0; i < (constants.GetLength(0) - 1) * 20; i++)
            {
                for (int a = 0; a < octaves; a++)
                {
                    tempX = (stepX * i) * (frequency);
                    tempX -= MathF.Cos(octaveRotation[a]);
                    tempX = tempX % helpX;
                    if (tempX < 0.0f)
                    {
                        tempX += (float)helpX;
                    }
                    x[a] = tempX;
                    X[a] = (int)(x[a] - (x[a] % 1.0f));
                    xf[a] = x[a] - X[a];
                    u[a] = Fade(xf[a]);
                    frequency *= 2.0f;
                }
                frequency = 1.0f;
                for (int j = 0; j < (constants.GetLength(1) - 1) * 20; j++)
                {
                    total = 0f;
                    amplitude = 1f;
                    for (int a = 0; a < octaves; a++)
                    {
                        tempY = (stepY * j) * (frequency);
                        tempY -= MathF.Sin(octaveRotation[a]);
                        tempY = tempY % helpY;
                        if (tempY < 0.0f)
                        {
                            tempY += (float)helpY;
                        }
                        y[a] = tempY;
                        Y[a] = (int)(y[a] - (y[a] % 1.0f));
                        yf[a] = y[a] - Y[a];
                        v[a] = Fade(yf[a]);
                        frequency *= 2.0f;
                        total += CalculatePerlin(X[a], Y[a], xf[a], yf[a], u[a], v[a], constants) * amplitude;
                        amplitude *= persitance;
                    }
                    frequency = 1.0f;
                    total = (float)Math.Round((total / max) * (filter[i, j]) * 255f);
                    if (total >= values[0])
                    {
                        values[0] = (byte)total;
                    }
                    else if (total <= values[1])
                    {
                        values[1] = (byte)total;
                    }

                }
            }
            return values;
        }

        public static byte[,] GenerateNormalizedOctave(Rectangle part, Point resolution, float[,][] constants, int octaves, float persitance)
        {
            Filter filter = new Filter(true);
            //filter.addCircle(new double[4] { 500, 400, 400, 1});
            //filter.addCircle(new double[4] { 1100, 400, 400, 4 });

            /*filter.addCircle(new double[5] { 200, 200, 200, 1 ,1});
            filter.addCircle(new double[5] { 600, 200, 200, 1 ,0.9});
            filter.addCircle(new double[5] { 1000, 200, 200, 1,0.8 });
            filter.addCircle(new double[5] { 1400, 200, 200, 1,0.7 });

            filter.addCircle(new double[5] { 200, 600, 200, 5 ,0.25});
            filter.addCircle(new double[5] { 600, 600, 200, 5, 0.5});
            filter.addCircle(new double[5] { 1000, 600, 200, 5, 0.75 });
            filter.addCircle(new double[5] { 1400, 600, 200, 5 ,1});*/
            //new world with seperating sea and southern mountains
            filter.addCircle(new double[5] { 800, 400, 100, 1, 1 });
            filter.addCircle(new double[5] { 850, 450, 150, 4, 0.75 });
            filter.addCircle(new double[5] { 1000, 400, 400, 5, 0.7 });
            filter.addCircle(new double[5] { 1200, 400, 400, 5, 0.7 });
            //start continent
            filter.addCircle(new double[5] { 400, 400, 400, 5, 0.7 });
            filter.addCircle(new double[5] { 300, 500, 300, 5, 0.7 });
            //southern islands
            filter.addCircle(new double[5] { 600, 700, 100, 1, 0.8 });
            filter.addCircle(new double[5] { 700, 750, 50, 4, 0.6 });
            filter.addCircle(new double[5] { 700, 650, 110, 5, 0.6 });

            //filter.addCircle(new double[3] { 800, 300, 300 });
            //filter.addCircle(new double[3] { 800, 500, 300 });
            //filter.addCircle(new double[3] { 400, 200, 200 });
            byte[] minMax = GetTopValue(constants, octaves, persitance, filter.getFilter(new Rectangle(0, 0, 1600, 800), new Point((constants.GetLength(0) - 1) * 10, (constants.GetLength(1) - 1) * 10)));//,filter.getFilter(new Rectangle(0,0,1600,800), new Point( (constants.GetLength(0)-1)*10, (constants.GetLength(1) - 1) * 10))

            //byte[] minMax = GetTopValue(constants, octaves, persitance);
            //return filter.getFilter(part, resolution);

            return GeneratePerlinValuesV2(part, resolution, constants, octaves, persitance, minMax[0], minMax[1], filter.getFilter(part, resolution));
        }

        //rectangle ratio to constant distance= 100:1 i.e 50 rect pos = 0.5 constant dist
        //part.Width/resolution.X
        public static byte[,] GeneratePerlinValuesV2(Rectangle part, Point resolution, float[,][] constants, int octaves, float persitance)
        {
            byte[,] values = new byte[resolution.X, resolution.Y];

            float stepX = (part.Width / (float)resolution.X) / 100.0f;
            float stepY = (part.Height / (float)resolution.Y) / 100.0f;
            float frequency = 1.0f;
            float total, max, amplitude;
            max = 0;
            amplitude = 1;
            int helpX = (constants.GetLength(0) - 1);
            int helpY = (constants.GetLength(1) - 1);
            float[] x, y, xf, yf, u, v;
            x = new float[octaves];
            y = new float[octaves];
            xf = new float[octaves];
            yf = new float[octaves];
            u = new float[octaves];
            v = new float[octaves];
            for (int a = 0; a < octaves; a++)
            {
                max += 1.0f * amplitude;
                amplitude *= persitance;
            }
            int[] X, Y;
            X = new int[octaves];
            Y = new int[octaves];
            for (int i = 0; i < resolution.X; i++)
            {
                for (int a = 0; a < octaves; a++)
                {
                    x[a] = (((part.X / 100f) + stepX * i) * frequency) % helpX;
                    X[a] = (int)(x[a] - (x[a] % 1.0f));
                    xf[a] = x[a] - X[a];
                    u[a] = Fade(xf[a]);
                    frequency *= 2.0f;
                }
                frequency = 1.0f;
                for (int j = 0; j < resolution.Y; j++)
                {
                    total = 0f;
                    amplitude = 1f;
                    for (int a = 0; a < octaves; a++)
                    {
                        y[a] = (((part.Y / 100f) + stepY * j) * frequency) % helpY;
                        Y[a] = (int)(y[a] - (y[a] % 1.0f));
                        yf[a] = y[a] - Y[a];
                        v[a] = Fade(yf[a]);
                        frequency *= 2.0f;
                        total += CalculatePerlin(X[a], Y[a], xf[a], yf[a], u[a], v[a], constants) * amplitude;
                        amplitude *= persitance;
                    }
                    frequency = 1.0f;
                    values[i, j] = (byte)Math.Round((total / max) * 255f);
                }
            }

            return values;
        }

        public static byte[,] GeneratePerlinValuesV2(Rectangle part, Point resolution, float[,][] constants, int octaves, float persitance, byte maxV, byte minV)
        {
            byte[,] values = new byte[resolution.X, resolution.Y];

            float stepX = (part.Width / (float)resolution.X) / 100.0f;
            float stepY = (part.Height / (float)resolution.Y) / 100.0f;
            float frequency = 1.0f;
            float total, max, amplitude, temp;
            max = 0;
            amplitude = 1;
            int helpX = (constants.GetLength(0) - 1);
            int helpY = (constants.GetLength(1) - 1);
            float[] x, y, xf, yf, u, v;
            x = new float[octaves];
            y = new float[octaves];
            xf = new float[octaves];
            yf = new float[octaves];
            u = new float[octaves];
            v = new float[octaves];
            for (int a = 0; a < octaves; a++)
            {
                max += 1.0f * amplitude;
                amplitude *= persitance;
            }
            int[] X, Y;
            X = new int[octaves];
            Y = new int[octaves];
            for (int i = 0; i < resolution.X; i++)
            {
                for (int a = 0; a < octaves; a++)
                {
                    x[a] = (((part.X / 100f) + stepX * i) * frequency) % helpX;
                    X[a] = (int)(x[a] - (x[a] % 1.0f));
                    xf[a] = x[a] - X[a];
                    u[a] = Fade(xf[a]);
                    frequency *= 2.0f;
                }
                frequency = 1.0f;
                for (int j = 0; j < resolution.Y; j++)
                {
                    total = 0f;
                    amplitude = 1f;
                    for (int a = 0; a < octaves; a++)
                    {
                        y[a] = (((part.Y / 100f) + stepY * j) * frequency) % helpY;
                        Y[a] = (int)(y[a] - (y[a] % 1.0f));
                        yf[a] = y[a] - Y[a];
                        v[a] = Fade(yf[a]);
                        frequency *= 2.0f;
                        total += CalculatePerlin(X[a], Y[a], xf[a], yf[a], u[a], v[a], constants) * amplitude;
                        amplitude *= persitance;
                    }
                    frequency = 1.0f;
                    temp = ((total / max) * 255f);
                    if (temp > maxV) temp = maxV;
                    if (temp < minV) temp = minV;
                    values[i, j] = (byte)Math.Round(((temp - minV) / (float)(maxV - minV)) * 255f);
                }
            }

            return values;
        }

        public static byte[,] GeneratePerlinValuesV2(Rectangle part, Point resolution, float[,][] constants, int octaves, float persitance, byte maxV, byte minV, float[,] filter)
        {
            byte[,] values = new byte[resolution.X, resolution.Y];
            Random rgen = new Random();
            float stepX = (part.Width / (float)resolution.X) / 100.0f;
            float stepY = (part.Height / (float)resolution.Y) / 100.0f;
            float frequency = 1.0f;
            float total, max, amplitude, temp;
            max = 0;
            amplitude = 1;
            int helpX = (constants.GetLength(0) - 1);
            int helpY = (constants.GetLength(1) - 1);
            float[] x, y, xf, yf, u, v;
            x = new float[octaves];
            y = new float[octaves];
            xf = new float[octaves];
            yf = new float[octaves];
            u = new float[octaves];
            v = new float[octaves];
            float tempY, tempX;
            for (int a = 0; a < octaves; a++)
            {
                max += 1.0f * amplitude;
                amplitude *= persitance;
            }
            int[] X, Y;
            X = new int[octaves];
            Y = new int[octaves];
            for (int i = 0; i < resolution.X; i++)
            {
                for (int a = 0; a < octaves; a++)
                {
                    x[a] = (((part.X / 100f) + stepX * i) * (frequency)) % helpX;
                    X[a] = (int)(x[a] - (x[a] % 1.0f));
                    xf[a] = x[a] - X[a];
                    u[a] = Fade(xf[a]);
                    frequency *= 2.0f;
                }
                frequency = 1.0f;
                for (int j = 0; j < resolution.Y; j++)
                {
                    if (filter[i, j] == 0) continue;
                    total = 0f;
                    amplitude = 1f;
                    for (int a = 0; a < octaves; a++)
                    {
                        y[a] = (((part.Y / 100f) + stepY * j) * (frequency)) % helpY;
                        Y[a] = (int)(y[a] - (y[a] % 1.0f));
                        yf[a] = y[a] - Y[a];
                        v[a] = Fade(yf[a]);
                        frequency *= 2.0f;
                        total += CalculatePerlin(X[a], Y[a], xf[a], yf[a], u[a], v[a], constants) * amplitude;
                        amplitude *= persitance;
                    }
                    frequency = 1.0f;
                    temp = ((total / max) * filter[i, j] * 255f);
                    if (temp > maxV) temp = maxV;
                    if (temp < minV) temp = minV;
                    values[i, j] = (byte)Math.Round(((temp - minV) / (float)(maxV - minV)) * 255f);
                }
            }

            return values;
        }

        public static byte[,] GeneratePerlinValuesV2(Rectangle part, Point resolution, float[,][] constants, int octaves, float persitance, byte maxV, byte minV, float[,] filter, float[] octaveRotation)
        {
            byte[,] values = new byte[resolution.X, resolution.Y];
            Random rgen = new Random();
            float stepX = (part.Width / (float)resolution.X) / 100.0f;
            float stepY = (part.Height / (float)resolution.Y) / 100.0f;
            float frequency = 1.0f;
            float total, max, amplitude, temp;
            max = 0;
            amplitude = 1;
            int helpX = (constants.GetLength(0) - 1);
            int helpY = (constants.GetLength(1) - 1);
            float[] x, y, xf, yf, u, v;
            x = new float[octaves];
            y = new float[octaves];
            xf = new float[octaves];
            yf = new float[octaves];
            u = new float[octaves];
            v = new float[octaves];
            float tempY, tempX;
            for (int a = 0; a < octaves; a++)
            {
                max += 1.0f * amplitude;
                amplitude *= persitance;
            }
            int[] X, Y;
            X = new int[octaves];
            Y = new int[octaves];
            for (int i = 0; i < resolution.X; i++)
            {
                for (int a = 0; a < octaves; a++)
                {
                    tempX = ((part.X / 100f) + stepX * i) * (frequency);
                    
                    tempX -= MathF.Cos(octaveRotation[a]);
                    tempX = tempX % helpX;
                    if (tempX < 0.0f)
                    {
                        tempX += (float)helpX;
                    }
                    x[a] = tempX;
                    X[a] = (int)(x[a] - (x[a] % 1.0f));
                    xf[a] = x[a] - X[a];
                    u[a] = Fade(xf[a]);
                    frequency *= 2.0f;
                }
                frequency = 1.0f;
                for (int j = 0; j < resolution.Y; j++)
                {
                    if (filter[i, j] == 0) continue;
                    total = 0f;
                    amplitude = 1f;
                    for (int a = 0; a < octaves; a++)
                    {
                        tempY = ((part.Y / 100f) + stepY * j) * (frequency);
                        tempY -= MathF.Sin(octaveRotation[a]);
                        tempY = tempY % helpY;
                        if (tempY < 0.0f)
                        {
                            tempY += (float)helpY;
                        }
                        y[a] = tempY;
                        Y[a] = (int)(y[a] - (y[a] % 1.0f));
                        yf[a] = y[a] - Y[a];
                        v[a] = Fade(yf[a]);
                        frequency *= 2.0f;
                        total += CalculatePerlin(X[a], Y[a], xf[a], yf[a], u[a], v[a], constants) * amplitude;
                        amplitude *= persitance;
                    }
                    frequency = 1.0f;
                    temp = ((total / max) * filter[i, j] * 255f);
                    if (temp > maxV) temp = maxV;
                    if (temp < minV) temp = minV;
                    values[i, j] = (byte)Math.Round(((temp - minV) / (float)(maxV - minV)) * 255f);
                }
            }

            return values;
        }

        public static byte[,] GeneratePerlinValuesV2(Rectangle part, Point resolution, float[,][] constants, int octaves, float persitance, float[,] filter)
        {
            byte[,] values = new byte[resolution.X, resolution.Y];

            float stepX = (part.Width / (float)resolution.X) / 100.0f;
            float stepY = (part.Height / (float)resolution.Y) / 100.0f;
            float frequency = 1.0f;
            float total, max, amplitude, temp;
            max = 0;
            amplitude = 1;
            int helpX = (constants.GetLength(0) - 1);
            int helpY = (constants.GetLength(1) - 1);
            float[] x, y, xf, yf, u, v;
            x = new float[octaves];
            y = new float[octaves];
            xf = new float[octaves];
            yf = new float[octaves];
            u = new float[octaves];
            v = new float[octaves];
            for (int a = 0; a < octaves; a++)
            {
                max += 1.0f * amplitude;
                amplitude *= persitance;
            }
            int[] X, Y;
            X = new int[octaves];
            Y = new int[octaves];
            for (int i = 0; i < resolution.X; i++)
            {
                for (int a = 0; a < octaves; a++)
                {
                    x[a] = (((part.X / 100f) + stepX * i) * frequency) % helpX;
                    X[a] = (int)(x[a] - (x[a] % 1.0f));
                    xf[a] = x[a] - X[a];
                    u[a] = Fade(xf[a]);
                    frequency *= 2.0f;
                }
                frequency = 1.0f;
                for (int j = 0; j < resolution.Y; j++)
                {
                    if (filter[i, j] == 0) continue;
                    total = 0f;
                    amplitude = 1f;
                    for (int a = 0; a < octaves; a++)
                    {
                        y[a] = (((part.Y / 100f) + stepY * j) * frequency) % helpY;
                        Y[a] = (int)(y[a] - (y[a] % 1.0f));
                        yf[a] = y[a] - Y[a];
                        v[a] = Fade(yf[a]);
                        frequency *= 2.0f;
                        total += CalculatePerlin(X[a], Y[a], xf[a], yf[a], u[a], v[a], constants) * amplitude;
                        amplitude *= persitance;
                    }
                    frequency = 1.0f;
                    temp = ((total / max) * filter[i, j] * 255f);
                    values[i, j] = (byte)temp;
                }
            }

            return values;
        }

        private static float CalculateBCN(int X, int Y, float xf, float yf, float[,][] constants)
        {
            float[] v0, v1, v2;
            float w0,w1,w2;
            float d = (xf) * (-1.0f) - (yf-1.0f) * (1);
            if (d < 0)
            {
                v0 = new float[2] { X + 1, Y };//constants[X + 1, Y];
                v1 = new float[2] { X + 1, Y + 1 };//constants[X+1, Y+1];
                v2 = new float[2] { X, Y + 1 };//constants[X, Y+1];
            }
            else
            {
                v0 = new float[2] { X, Y };//constants[X, Y];
                v1 = new float[2] { X + 1, Y };//constants[X + 1, Y];
                v2 = new float[2] { X, Y + 1 };//constants[X, Y + 1];
            }
            float wDiv = 1/((v1[1]-v2[1])*(v0[0]-v2[1])+(v2[0]-v1[0])*(v0[1]-v2[0]));//(p1.y - p2.y) * (p0.x - p2.x) + (p2.x - p1.x) * (p0.y - p2.y);
            w0 = ((v1[1]-v2[1]) * (xf-v2[0]) + (v2[0]-v1[0]) * (yf-v2[1])) * wDiv;//((p1.y - p2.y) * (x - p2.x) + (p2.x - p1.x)* (y - p2.y)) * wDiv;
            w1 = ((v2[1]-v0[1]) * (xf-v2[0]) + (v0[0]-v2[0]) * (yf - v2[1])) * wDiv;//((p2.y - p0.y) * (x - p2.x) + (p0.x - p2.x) * (y - p2.y)) * wDiv;
            w2 =1.0f-w0-w1;
            w0 = Fade(w0);
            w1 = Fade(w1);
            w2 = Fade(w2);
            return w0 * dot(constants[(int)v0[0], (int)v0[1]], new float[2] {xf- (int)v0[0] , yf- v0[1]}) + w1 * dot(constants[(int)v1[0], (int)v1[1]], new float[2] { xf - (int)v1[0], yf - v1[1] }) + w2 * dot(constants[(int)v2[0], (int)v2[1]], new float[2] { xf - (int)v2[0], yf - v2[1] });
            
        }

        public static byte[,] GenerateBCNValues(Rectangle part, Point resolution, float[,][] constants, int octaves, float persitance, byte maxV, byte minV, float[,] filter)
        {
            byte[,] values = new byte[resolution.X, resolution.Y];

            float stepX = (part.Width / (float)resolution.X) / 100.0f;
            float stepY = (part.Height / (float)resolution.Y) / 100.0f;
            float frequency = 1.0f;
            float total, max, amplitude, temp;
            max = 0;
            amplitude = 1;
            int helpX = (constants.GetLength(0) - 1);
            int helpY = (constants.GetLength(1) - 1);
            float[] x, y, xf, yf, u, v;
            x = new float[octaves];
            y = new float[octaves];
            xf = new float[octaves];
            yf = new float[octaves];
            for (int a = 0; a < octaves; a++)
            {
                max += 1.0f * amplitude;
                amplitude *= persitance;
            }
            int[] X, Y;
            X = new int[octaves];
            Y = new int[octaves];
            for (int i = 0; i < resolution.X; i++)
            {
                for (int a = 0; a < octaves; a++)
                {
                    x[a] = (((part.X / 100f) + stepX * i) * frequency) % helpX;
                    X[a] = (int)(x[a] - (x[a] % 1.0f));
                    xf[a] = x[a] - X[a];
                    frequency *= 2.0f;
                }
                frequency = 1.0f;
                for (int j = 0; j < resolution.Y; j++)
                {
                    if (filter[i, j] == 0) continue;
                    total = 0f;
                    amplitude = 1f;
                    for (int a = 0; a < octaves; a++)
                    {
                        y[a] = (((part.Y / 100f) + stepY * j) * frequency) % helpY;
                        Y[a] = (int)(y[a] - (y[a] % 1.0f));
                        yf[a] = y[a] - Y[a];
                        frequency *= 2.0f;
                        total += CalculateBCN(X[a], Y[a], xf[a], yf[a], constants) * amplitude;
                        amplitude *= persitance;
                    }
                    frequency = 1.0f;
                    temp = ((total / max) * filter[i, j] * 255f);
                    if (temp > maxV) temp = maxV;
                    if (temp < minV) temp = minV;
                    values[i, j] = (byte)Math.Round(((temp - minV) / (float)(maxV - minV)) * 255f);
                }
            }

            return values;
        }

        public static float[,][] GenerateConstants(Point size)
        {
            float[,][] constants = new float[size.X, size.Y][];
            Random rgen = new Random();
            double temp;
            for (int i = 0; i < size.X - 1; i++)
            {
                for (int j = 0; j < size.Y - 1; j++)
                {
                    temp = rgen.NextDouble()*2*MathF.PI;
                    constants[i, j] = new float[] { MathF.Cos((float)temp),MathF.Sin((float)temp) };
                    /*if (temp == 0)
                        constants[i, j] = new float[] { 1, 1 };
                    else if (temp == 1)
                        constants[i, j] = new float[] { 1, -1 };
                    else if (temp == 2)
                        constants[i, j] = new float[] { -1, 1 };
                    else
                        constants[i, j] = new float[] { -1, -1 };*/

                }
            }
            for (int i = 0; i < size.Y - 1; i++)
            {
                constants[size.X - 1, i] = constants[0, i];
            }
            for (int i = 0; i < size.X; i++)
            {
                constants[i, size.Y - 1] = constants[i, 0];
            }
            return constants;
        }

        private static float dot(float[] vector1, float[] vector2)
        {
            return vector1[0] * vector2[0] + vector1[1] * vector2[1];
        }
        private static float Fade(float t)
        {
            return ((6 * t - 15) * t + 10) * t * t * t;
        }
        private static float Lerp(float t, float a1, float a2)
        {
            return a1 + t * (a2 - a1);
        }
        public static Color GetColor(byte h, byte t, byte f)
        {
            double height, temperature, fertility;
            height = h / 255.0;
            temperature = t / 255.0;
            fertility = f / 255.0;
            Color colorLower, colorUpper;

            colorLower = Color.Transparent;
            colorUpper = Color.Transparent;
            if (height < 0.5)
            {
                if (height < 0.35)
                {
                    return Color.Lerp(Color.DarkBlue, Color.Blue, (float)((height / 0.35) - (height / 0.35) % 0.0571));
                }
                return Color.Lerp(Color.Blue, Color.Lerp(Color.MediumSeaGreen, Color.Blue, 0.5f), (float)(((height - 0.35) / 0.15) - ((height - 0.35) / 0.15) % 0.1333));
            }

            else if (height < 0.51)
            {

                if (fertility > 0.6 && temperature > 0.4)
                {
                    return Color.Lerp(Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.45f), Color.OliveDrab, 0.25f), Color.DarkGreen, (float)(((height - 0.5) / 0.01)));
                }
                return Color.Lerp(Color.BurlyWood, Color.Bisque, (float)(((height - 0.5) / 0.01)));
            }

            if (height > 0.8) return Color.Lerp(Color.Gray, Color.White, (float)(((height - 0.8) / 0.2)));// - ((height - 0.8) / 0.2) % 0.05));
            else if (height > 0.65)
            {
                if (fertility < 0.20)
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
                else if (fertility < 0.40)
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
                else if (fertility < 0.60)
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
                else if (fertility < 0.80)
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
                if (fertility < 0.20)
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
                        colorLower = Color.Lerp(Color.DarkCyan, Color.SlateGray, 0.5f);
                    }
                }

                else if (fertility < 0.40)
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
                else if (fertility < 0.60)
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
                else if (fertility < 0.80)
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
            return Color.Lerp(colorLower, colorUpper, (float)(((height - 0.52) / 0.28) - ((height - 0.52) / 0.28) % 0.0357));
        }

    }
    class City
    {
        Rectangle bounds;
        bool[] range;
    }
    class Race
    {
        float food = 0, wood = 0, foodRate = 1, woodRate = 1;
    }
    class Highlight
    {
        Rectangle rect;
        Texture2D texture;
        bool render;
        int range=70;
        const int maxRange= 70;
        MouseState mouseState;
        public Highlight(GraphicsDevice gd)
        {
            texture = new Texture2D(gd, 201, 201);
            rect = new Rectangle(0, 0, 201, 201);
        }
        public void Activate(int range)
        {
            render = true;
            if (range < maxRange)
                this.range = range;
            else
                this.range = maxRange;
        }
        public void Update(SimplexWorld world, MouseState old)
        {
            mouseState = Mouse.GetState();
            if ((old.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed))
            {
                //if(render)
                    render = !render;
            }
            if (render)
            {
                
                world.Highlight(old, range, texture);
            }
            if (world.changed)
            {
                rect.Width = (int)(201f * world.zoom);
                rect.Height = (int)(201f * world.zoom);
            }
            rect.X = (int)Math.Floor(Mouse.GetState().X - (Mouse.GetState().X % world.zoom) - 100f * world.zoom);
            rect.Y = (int)Math.Floor(Mouse.GetState().Y - (Mouse.GetState().Y % world.zoom) - 100f * world.zoom);
        }
        public void Render(SpriteBatch sb)
        {
            if (render)
                sb.Draw(texture, rect, Color.White * 0.35f);
        }
    }
    class GameInterface
    {

        Highlight highlight;
        public GameInterface(GraphicsDevice gd)
        {
            highlight = new Highlight(gd);
        }

        public void Update(SimplexWorld world, MouseState old)
        {

            highlight.Update(world, old);
        }
        public void Render(SpriteBatch sb)
        {
            highlight.Render(sb);
        }
    }
    class SimplexWorld
    {
        int hOctave = 7, tOctave = 2, fOctave = 1, displacementX, displacementY;
        public float hPersistance = 0.5f, tPersistance = 0.75f, fPersistance = 0.75f, zoom=1.0f;
        public bool changed = true, finishedLoading=false;

        byte[,] height, temperature, fertility;
        float[,][] heightGradients, temperatureGradients, fertilityGradients;
        bool[,] tRenderMap;

        Rectangle boundingWorld, screen, world = new Rectangle(), viewPort = new Rectangle();
        public Texture2D texture, treetexture;
        Filter topology;
        Point equator;
        Color[] reset;
        public SimplexWorld(int sizeX, int sizeY, int gX, int gY, int screenX, int screenY, GraphicsDevice graphicsDevice)
        {
            //set up the size of the world
            Point resolution= new Point(gX, gY);
            equator = new Point(sizeY/2,2*sizeY/3);
            boundingWorld = new Rectangle(0, 0, sizeX, sizeY);
            screen = new Rectangle(0, 0, screenX, screenY);

            heightGradients = TheNewWorld.GenerateConstants(resolution);
            temperatureGradients= TheNewWorld.GenerateConstants(new Point(gX / 2, gY / 2));
            fertilityGradients = TheNewWorld.GenerateConstants(resolution);

            topology = new Filter(true);
            topology.addCircle(new double[5] { (sizeX / 2), (sizeY / 2), (sizeX*2/3), 2,1});


            height = new byte[sizeX, sizeY];
            temperature = new byte[sizeX, sizeY];
            fertility = new byte[sizeX, sizeY];

            texture = new Texture2D(graphicsDevice, sizeX, sizeY);
            treetexture = new Texture2D(graphicsDevice, sizeX, sizeY);
            reset = new Color[201*201];
            for (int i = 0; i < 201 * 201; i++) reset[i] = Color.Transparent;
            SetData(graphicsDevice);
            
            viewPort = screen;
        }
        
        public void Highlight(MouseState old, int range, Texture2D texture)
        {
            if (!finishedLoading) return;
            MouseState mouseState = Mouse.GetState();

            //translate mouse to data pos
            int dataX = viewPort.X + (int)Math.Floor((mouseState.X - (mouseState.X % zoom)) / zoom),
                dataY = viewPort.Y + (int)Math.Floor((mouseState.Y - (mouseState.Y % zoom)) / zoom); 
            if ((dataX == viewPort.X + (int)Math.Floor((old.X - (old.X % zoom)) / zoom) &&
                dataY == viewPort.Y + (int)Math.Floor((old.Y - (old.Y % zoom)) / zoom)) && !changed) return;
            texture.SetData(reset);
            int side = range * 2 + 1;
            Rectangle area = new Rectangle(dataX - range, dataY - range, side, side), textureArea = new Rectangle(100 - range, 100 - range, side, side);
            if (area.X < 0) area.X = 0;
            else if (area.Right >= height.GetLength(0)) area.X = height.GetLength(0) - side;
            if (area.Y < 0) area.Y = 0;
            else if (area.Bottom >= height.GetLength(1)) area.Y = height.GetLength(1) - side;
            Color[] colors = RangeFindAcessability(area, range);
            texture.SetData(0, textureArea, colors, 0, side * side);

        }
        
        public async Task SetTextureThreaded(Texture2D texture, Rectangle part, Color[] allColors)
        {
            Color[] colors = new Color[part.Width * part.Height];
            for (int i = 0; i < part.Width; i++)
            {
                for (int j = 0; j < part.Height; j++)
                {
                    
                }
            }
            texture.SetData(0, part, colors, 0, part.Width * part.Height);
        }

        private float Cost(Point p1, Point p2)
        {
            float cost = 1f;
            float bridgeTemp, bridgeHeight, bridgeFert;
            bridgeTemp = (temperature[p1.X, p1.Y] + temperature[p2.X, p2.Y]) * 0.5f * 0.00392156862f;
            float dink = MathF.Abs((height[p1.X, p1.Y] - height[p1.X, p1.Y] % 2f)- (height[p2.X, p2.Y] - height[p2.X, p2.Y] % 2f))* 0.5f;

            if (height[p2.X, p2.Y] >= 204) cost += 1.5f;
            if (height[p2.X, p2.Y] <128) cost += 7f;
            if (bridgeTemp > 0.8f || bridgeTemp < 0.2f) cost += 0.5f;

            if (tRenderMap[p2.X, p2.Y]) cost += 105f;
            cost += 4f * dink;
            return cost;
        }
        private Color[] RangeFindAcessability(Rectangle area, int range)
        {
            Color[] data = new Color[area.Width * area.Height];
            Point current, temp;
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(area.Center);
            bool[,] banlist = new bool[area.Width, area.Height];
            float[,] values = new float[area.Width, area.Height];
            
            for(int i =0; i < area.Width; i++)
            {
                for (int j = 0; j < area.Height; j++)
                {
                    if (i == area.Center.X - area.X && j == area.Center.Y - area.Y) continue;
                    values[i, j] = float.MaxValue;
                }
            }
            float cost;
            while (queue.Count != 0)
            {
                current = queue.Dequeue();
                
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i == 1 && i == j) continue;
                        temp = new Point(current.X - 1 + i, current.Y - 1 + j);
                        if (area.Contains(temp))
                        {
                            cost = values[current.X - area.X, current.Y - area.Y] + MathF.Sqrt(MathF.Pow(temp.X-current.X,2) + MathF.Pow(temp.Y - current.Y,2)) * Cost(current, temp);
                            if (cost > range) continue;
                            if (cost < values[temp.X - area.X, temp.Y - area.Y])
                            {
                                values[temp.X - area.X, temp.Y - area.Y] = cost;
                            }
                            if (!banlist[temp.X - area.X, temp.Y - area.Y])
                            {
                                queue.Enqueue(temp);
                                banlist[temp.X - area.X, temp.Y - area.Y] = true;
                            }
                        }
                    }
                }
                


            }
            float t, b = 1/(float)range;
            for (int i = 0; i < area.Width; i++)
            {
                for (int j = 0; j < area.Height; j++)
                {
                    t = values[i, j]*b;
                    if (t > 1.0f)
                    {
                        continue;
                    }
                    if(t < 0.25f) data[i + area.Width * j] = Color.Yellow;
                    else if(t < 0.5f) data[i + area.Width * j] = Color.Orange;
                    else if(t < 0.75f) data[i + area.Width * j] = Color.Red;
                    else data[i + area.Width * j] = Color.DarkRed ;
                }
            }
            return data;

        }
        private bool Click(MouseState old, ref Point p)
        {
            MouseState mouseState = Mouse.GetState();
            if ((old.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed))
            {
                p = new Point(mouseState.X + screen.X, mouseState.Y + screen.Y);
                return true;
            }
            return false;
        }
        
        private Color GetColor(int x, int y)
        {
            return TheNewWorld.GetColor(height[x, y], temperature[x, y], fertility[x, y]);
        }
        private void Normalize(ref byte[,] data)
        {
            int sizeX = data.GetLength(0), sizeY = data.GetLength(1);
            float max = 0f;
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (data[i, j] > max) max = data[i, j];
                }
            }
            max = 1.0f / max;
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (data[i, j] == 0) continue;
                    data[i, j] = (byte)MathF.Round(data[i, j] * max*255f);
                }
            }
        }
        private void SetForests(GraphicsDevice gd)
        {
            Random random = new Random();
            bool[,] renderlist = new bool[height.GetLength(0), height.GetLength(1)];
            Color[] colors = new Color[height.GetLength(0)* height.GetLength(1)];
            double chance = 0;
            for (int i = 0; i < height.GetLength(0); i++)
            {
                for (int j = 0; j < height.GetLength(1); j++)
                {
                    if (!(height[i, j] > 131 && height[i, j] < 204 && fertility[i, j] >= 102)) continue;
                    else
                    {
                        if (fertility[i, j ] < 153) chance = 0.05;
                        else if (fertility[i, j ] < 204) chance = 0.15;
                        else chance = 0.45;
                        if (random.NextDouble() < chance)
                        {
                            colors[i + height.GetLength(0) * j] = Color.Lerp(Color.Firebrick, Color.SaddleBrown,(float)((height[i,j]-131f)/73f));
                            renderlist[i, j] = true;
                            
                        }
                    }
                }
            }
            tRenderMap = renderlist;
            
            treetexture.SetData(colors);
        }
        private void SetTexture()
        {
            Color[] colors = new Color[boundingWorld.Width * boundingWorld.Height];
            for (int i = 0; i < boundingWorld.Width; i++)
            {
                for (int j = 0; j < boundingWorld.Height; j++)
                {
                    colors[i + boundingWorld.Width * j] = GetColor(i, j);
                }
            }
            texture.SetData(colors);
        }
        private async Task SetData(GraphicsDevice gd)
        {
            Action<object> action1 = (o) => SetHeight((Rectangle)o);
            Action<object> action2 = (o) => SetTFData((Rectangle)o);
            Rectangle current;
            int a = 2, b = 2;
            while (boundingWorld.Width % a != 0) a++;
            while (boundingWorld.Height % b != 0) b++;
            int stepX = boundingWorld.Width / a, stepY = boundingWorld.Height / b;
            Task[] tasks = new Task[a * b];
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    current = new Rectangle(i*stepX,j*stepY, stepX, stepY);
                    tasks[i + a * j] = new Task(action1, current);
                    tasks[i + a * j].Start();
                }
            }
            await Task.WhenAll(tasks);
            //
            Normalize(ref height);
            TheNewWorld.TexturizeDS(ref height, 0.09f);

            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < b; j++)
                {
                    current = new Rectangle(i * stepX, j * stepY, stepX, stepY);
                    tasks[i + a * j] = new Task(action2, current);
                    tasks[i + a * j].Start();
                }
            }
            await Task.WhenAll(tasks);
            Normalize(ref temperature);
            Normalize(ref fertility);
            SetTexture();
            SetForests(gd);
            finishedLoading = true;
        }
        private async Task SetHeight(Rectangle section)
        {
            TheNewWorld.GenerateSimplexNoise(section, ref height, heightGradients, hOctave, hPersistance, topology.getSFilter(section));
        }
        private async Task SetTFData(Rectangle section)
        {
            byte[,] h = new byte[section.Width, section.Height];
            for (int i = 0; i < section.Width; i++)
            {
                for (int j = 0; j < section.Height; j++)
                {
                    h[i, j] = height[i + section.X, j + section.Y];
                }
            }
            float[,] filter = topology.getFilterS(section, h, 0.5f, 2, true, equator);
            TheNewWorld.GenerateSimplexNoise(section, ref temperature, temperatureGradients, tOctave, tPersistance, filter);
            filter = topology.getFilterS(section, h, 0.5f, 2, true);
            TheNewWorld.GenerateSimplexNoise(section, ref fertility, fertilityGradients, fOctave, fPersistance, filter);
        }
        
        public void Update(MouseState old)
        {
            MouseState mouseState = Mouse.GetState();
           
            if (mouseState.ScrollWheelValue < old.ScrollWheelValue)
            {
                if (zoom != 1f)
                {
                    zoom -= 1f;
                    displacementX = (int)(screen.Width / 2f - screen.Width / (zoom * 2f));
                    displacementY = (int)(screen.Height / 2f - screen.Height / (zoom * 2f));
                    changed = true;
                }
                
            }
            if (mouseState.ScrollWheelValue > old.ScrollWheelValue)
            {
                if (zoom != 20f)
                {
                    zoom += 1f;
                    displacementX = (int)(screen.Width / 2f - screen.Width / (zoom * 2f));
                    displacementY = (int)(screen.Height / 2f - screen.Height / (zoom * 2f));
                    changed = true;
                }
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                screen.Y += 4;
                if(Keyboard.GetState().IsKeyDown(Keys.LeftShift)) screen.Y += 4;
                changed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                screen.Y -= 4;
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) screen.Y -= 4; 
                changed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                screen.X += 4;
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) screen.X += 4;
                changed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                screen.X -= 4;
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) screen.X -= 4;
                changed = true;
            }
            if (changed)
            {
                world.X = (int)((-screen.X - displacementX) * zoom);
                world.Y = (int)((-screen.Y - displacementY) * zoom);
                world.Width = (int)(boundingWorld.Width * zoom);
                world.Height = (int)(boundingWorld.Height * zoom);
                viewPort.X = screen.X + displacementX;
                viewPort.Y = screen.Y + displacementY;
                viewPort.Width = screen.Width - 2 * displacementX;
                viewPort.Height = screen.Height - 2 * displacementY;
            }
        }
        
        public void Render(SpriteBatch sb)
        {


            

            
            sb.Draw(texture, world, Color.White);
            
            sb.Draw(treetexture, world, Color.White *0.45f);



        }

    }

}
    

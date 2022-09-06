using System;
using System.Collections.Generic;
using System.Text;

namespace UntitledProject
{
    class Class2
    {
    }
}
/*
 OLD GAIA
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
            float[,] values = new float[xLen,yLen];
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
                        temp = (temp - floor) / (1-floor);
                    }
                    if (ease == 2)
                    {
                        temp = temp*temp;
                    }
                    else if (ease == 3)
                    {
                        temp = temp*temp*temp;
                    }
                    else if (ease == 4)
                    {
                        temp = temp * temp * temp * temp;
                    }
                    else if (ease == 5)
                    {
                        temp = 1 - Math.Sqrt(1 - Math.Pow(temp, 2));
                    }
                    
                    if (inverse) values[i, j] = (float)(1-temp);
                    else values[i, j] = (float)temp;
                }
            }
            return values;
        }
        public float[,] getFilter(Rectangle part, byte[,] data, double floor, int ease, bool inverse,Point equator)
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
                    else values[i, j] = (float)(temp*help);
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
                        temp = (1.0 - (Math.Sqrt(Math.Pow((x - data[0]), 2) + Math.Pow((y - data[1]), 2)) / data[2])) ;

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
                    values[i, j] = (float)(max*help);

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
                    values[i, j] = 1-(float)max;

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
                    values[i, j] = (float)((1 - max) *help);

                }
            }
            return values;
        }
    }
    class TheNewWorld
    {
        

        private static float CalculatePerlin(int X, int Y, float xf, float yf, float u, float v, float[,][] constants)
        {
            float[] topLeft = new float[] { xf, yf };
            float[] botLeft = new float[] { xf, yf - 1.0f };
            float[] topRight = new float[] { xf - 1.0f, yf };
            float[] botRight = new float[] { xf - 1.0f, yf - 1.0f };

            float floatvalue = Lerp(u, Lerp(v, dot(topLeft, constants[X, Y]), dot(botLeft, constants[X, Y + 1])), Lerp(v, dot(topRight, constants[X + 1, Y]), dot(botRight, constants[X + 1, Y + 1])));
            //might have to change position of X and Y ^
            return (floatvalue + 1.0f) / 2.0f;
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
            for (int i = 0; i < (constants.GetLength(0)-1)*20; i++)
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

        public static byte[] GetTopValue(float[,][] constants, int octaves, float persitance,float[,] filter)
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
                    total = (float)Math.Round((total / max) * (filter[i,j]) * 255f);
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
            filter.addCircle(new double[5] { 1400, 600, 200, 5 ,1});
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
        max += 255 * amplitude;
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
    int temp;
    for (int i = 0; i < size.X - 1; i++)
    {
        for (int j = 0; j < size.Y - 1; j++)
        {
            temp = rgen.Next(4);
            if (temp == 0)
                constants[i, j] = new float[] { 1, 1 };
            else if (temp == 1)
                constants[i, j] = new float[] { 1, -1 };
            else if (temp == 2)
                constants[i, j] = new float[] { -1, 1 };
            else
                constants[i, j] = new float[] { -1, -1 };

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
        
        
    }

    struct Tile
{
    public Rectangle position;
    public Texture2D texture;
    public Tile(Rectangle rect)
    {
        position = rect;
        texture = null;
    }
    public Tile(Rectangle rect, Texture2D texture)
    {
        position = rect;
        this.texture = texture;
    }
    public bool Empty()
    {
        return texture == null;
    }
}

class Gaia
{
    float[,][] height, fertility, temperature, metal;
    Rectangle mapSection, camera, prevCamera;
    Point resolution, minMaxSample, equator;
    Filter topology;
    byte[] heightMinMax, tempMinMax, fertMinMax, metaltMinMax;
    List<Tile>[] worldMap;
    Tile[,] worldTiles;
    DataBlock[,] worldBlocks;
    double loading;
    Texture2D texture, texture2;
    Color[] screenColors;
    Rectangle world, boundingWorld;

    public Gaia(Point screenResolution, Point size, GraphicsDevice gd)
    {
        world = new Rectangle(0, 0, size.X * 100, size.Y * 100);
        boundingWorld = new Rectangle(0, 0, size.X * 500, size.Y * 500);
        mapSection = new Rectangle(0, 0, size.X * 100, size.Y * 100);
        texture = new Texture2D(gd, screenResolution.X, screenResolution.Y);
        texture2 = new Texture2D(gd, size.X * 500, size.Y * 500);
        screenColors = new Color[screenResolution.X * screenResolution.Y];
        loading = 0.0;
        worldBlocks = new DataBlock[size.X * 10, size.Y * 10];
        worldMap = new List<Tile>[4];
        topology = new Filter(true);
        worldTiles = new Tile[size.X * 2, size.Y * 2];
        for (int i = 0; i < 4; i++)
        {
            worldMap[i] = new List<Tile>();
        }

        resolution = screenResolution;
        minMaxSample = new Point((size.X - 1) * 40, (size.Y - 1) * 40);
        equator = new Point((2 * size.Y / 3) * 100, (size.Y / 2) * 100);

        height = TheNewWorld.GenerateConstants(size);
        metal = TheNewWorld.GenerateConstants(size);

        fertility = TheNewWorld.GenerateConstants(size);
        temperature = TheNewWorld.GenerateConstants(size);

        if (size.X != size.Y)
        {
            if (size.X > size.Y)
            {
                if (size.X >= size.Y * 2)
                {
                    topology.addCircle(new double[5] { (size.X / 4) * 100, (size.Y / 2) * 100, (size.Y / 2) * 100, 1, 1 });
                    topology.addCircle(new double[5] { (3 * size.X / 4) * 100, (size.Y / 2) * 100, (size.Y / 2) * 100, 5, 0.7 });
                }
                else
                {
                    topology.addCircle(new double[5] { (size.X / 2) * 100, (size.Y / 2) * 100, (size.Y / 2) * 100, 1, 1 });
                    topology.addCircle(new double[5] { (size.X / 2) * 100, (size.Y / 2) * 100, (size.Y / 2) * 100, 5, 0.7 });
                }


            }
            else
            {
                topology.addCircle(new double[5] { (size.X / 2) * 100, (size.Y / 2) * 100, (size.X / 2) * 100, 1, 1 });

            }
        }
        else
        {
            topology.addCircle(new double[5] { (size.X / 2) * 100, (size.Y / 2) * 100, (size.X / 2) * 100, 1, 1 });

        }
        float[,] temporaryfilter = topology.getFilter(world, minMaxSample);
        heightMinMax = TheNewWorld.GetTopValue(height, 6, 0.6f, temporaryfilter);
        byte[,] temporaryByteFilter = TheNewWorld.GeneratePerlinValuesV2(world, minMaxSample, height, 6, 0.6f, heightMinMax[0], heightMinMax[1], temporaryfilter);
        temporaryfilter = topology.getFilter(temporaryByteFilter, 0.6, 2, true);
        fertMinMax = TheNewWorld.GetTopValue(fertility, 3, 0.75f, temporaryfilter);
        temporaryfilter = topology.getFilter(world, temporaryByteFilter, 0.5, 2, true, equator);
        tempMinMax = TheNewWorld.GetTopValue(temperature, 4, 0.75f, temporaryfilter);
        //DataBlockGeneration(world, 4);
        DataBlockGeneration();
        camera = new Rectangle(400 - 160, 400 - 80, 320, 160);
    }


    public byte[][,] getLocalData(Rectangle mapSection)
    {
        int x, y, lenX, lenY, dataX, dataY;
        byte[][,] data = new byte[3][,];
        lenX = mapSection.Width;
        lenY = mapSection.Height;
        data[0] = new byte[lenX * 5, lenY * 5];
        data[1] = new byte[lenX * 5, lenY * 5];
        data[2] = new byte[lenX * 5, lenY * 5];

        for (int i = 0; i < lenX; i++)
        {
            x = ((mapSection.X + i) - (mapSection.X + i) % 10) / 10;
            for (int j = 0; j < lenY; j++)
            {
                y = ((mapSection.Y + j) - (mapSection.Y + j) % 10) / 10;
                dataX = (mapSection.X + i) - worldBlocks[x, y].block.X;
                dataY = (mapSection.Y + j) - worldBlocks[x, y].block.Y;
                for (int a = 0; a < 5; a++)
                {
                    for (int b = 0; b < 5; b++)
                    {
                        data[0][i * 5 + a, j * 5 + b] = worldBlocks[x, y].data[0][dataX * 5 + a, dataY * 5 + b];
                        data[1][i * 5 + a, j * 5 + b] = worldBlocks[x, y].data[1][dataX * 5 + a, dataY * 5 + b];
                        data[2][i * 5 + a, j * 5 + b] = worldBlocks[x, y].data[2][dataX * 5 + a, dataY * 5 + b];
                    }
                }
            }
        }

        return data;
    }
    public void ThreadedSetTexture(int threadCount, Rectangle camera, Rectangle prevCamera)
    {
        if (camera == prevCamera) return;
        Rectangle temporary = Rectangle.Intersect(camera, prevCamera);

        Thread[] threads = new Thread[threadCount];
        List<DataBlock>[] workQueues = new List<DataBlock>[threadCount];
        int a = 0;
        for (int i = 0; i < threadCount; i++)
        {
            workQueues[i] = new List<DataBlock>();
        }
        for (int i = 0; i < worldBlocks.GetLength(0); i++)
        {
            for (int j = 0; j < worldBlocks.GetLength(1); j++)
            {
                if (worldBlocks[i, j] == null) continue;
                workQueues[a % threadCount].Add(worldBlocks[i, j]);
                a++;
            }
        }
        int[] temp = new int[threadCount];
        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread((object i) => SetTextures(workQueues[(int)i], camera));
            threads[i].Start(i);
        }

    }
    private void SetTextures(List<DataBlock> queue, Rectangle camera)
    {
        foreach (DataBlock block in queue)
        {
            SetTextureV2(camera, block);

        }

    }

    public void SetTextureV2(Rectangle camera, DataBlock block)
    {
        if (!block.block.Intersects(camera)) return;
        Rectangle area = Rectangle.Intersect(camera, block.block);
        Rectangle dataArea = new Rectangle((area.X - block.block.X) * 5, (area.Y - block.block.Y) * 5, area.Width * 5, area.Height * 5);
        Rectangle textureArea = new Rectangle((area.X - camera.X) * 5, (area.Y - camera.Y) * 5, area.Width * 5, area.Height * 5);
        Color[] colors = block.GetColorsInArea(dataArea);
        texture.SetData(0, textureArea, colors, 0, dataArea.Width * dataArea.Height);
    }
    public void SetTextureV3(Rectangle camera, DataBlock block, ref Texture2D texture)
    {
        if (!block.block.Intersects(camera)) return;
        Rectangle area = Rectangle.Intersect(camera, block.block);
        Rectangle dataArea = new Rectangle((area.X - block.block.X) * 5, (area.Y - block.block.Y) * 5, area.Width * 5, area.Height * 5);
        Rectangle textureArea = new Rectangle((area.X - camera.X) * 5, (area.Y - camera.Y) * 5, area.Width * 5, area.Height * 5);
        Color[] colors = block.GetColorsInArea(dataArea);
        texture.SetData(0, textureArea, colors, 0, dataArea.Width * dataArea.Height);
    }

    public void ThreadedSetScreenColorsV2(Rectangle camera, Rectangle prevCamera)
    {
        if (camera == prevCamera) return;
        Task[] tasks = new Task[worldBlocks.GetLength(0) * worldBlocks.GetLength(1)];
        DataBlock block;
        Action<object> action = (o) => SetColors(camera, (DataBlock)o);
        for (int i = 0; i < worldBlocks.GetLength(0); i++)
        {
            for (int j = 0; j < worldBlocks.GetLength(1); j++)
            {
                block = worldBlocks[i, j];
                tasks[i + worldBlocks.GetLength(0) * j] = new Task(action, block);
                tasks[i + worldBlocks.GetLength(0) * j].Start();
            }
        }
        Task.WaitAll(tasks);
        texture.SetData(screenColors);

    }
    public void ThreadedSetScreenColorsV3(Rectangle camera, Rectangle prevCamera)
    {
        if (camera == prevCamera) return;
        Rectangle intersection = Rectangle.Intersect(camera, prevCamera);
        Rectangle leftOver;
        Color[] localCopy = new Color[(camera.Width * 5) * (camera.Height * 5)];
        if (intersection.Width == camera.Width)
        {
            int globalX, globalY, oldX, oldY, newX, newY;
            for (int i = 0; i < intersection.Width; i++)
            {
                globalX = i + intersection.X;
                oldX = globalX - prevCamera.X;
                newX = globalX - camera.X;
                for (int j = 0; j < intersection.Height; j++)
                {
                    globalY = j + intersection.Y;
                    oldY = globalY - prevCamera.Y;
                    newY = globalY - camera.Y;
                    for (int a = 0; a < 5; a++)
                    {
                        for (int b = 0; b < 5; b++)
                        {
                            localCopy[(newX * 5 + a) + (intersection.Width * 5) * (newY * 5 + b)] = screenColors[(oldX * 5 + a) + (intersection.Width * 5) * (oldY * 5 + b)];
                        }
                    }
                }
            }
            for (int i = 0; i < localCopy.Length; i++)
            {
                screenColors[i] = localCopy[i];
            }

        }
        if (camera.Y == intersection.Y)
        {
            leftOver = new Rectangle(camera.X, camera.Y + intersection.Height, camera.Width, camera.Height - intersection.Height);
        }
        else
        {
            leftOver = new Rectangle(camera.X, camera.Y, camera.Width, camera.Height - intersection.Height);
        }

        for (int i = 0; i < worldBlocks.GetLength(0); i++)
        {
            for (int j = 0; j < worldBlocks.GetLength(1); j++)
            {
                SetColorsv2(camera, leftOver, worldBlocks[i, j]);
            }
        }
        /*Task[] tasks = new Task[worldBlocks.GetLength(0)* worldBlocks.GetLength(1)];
        DataBlock block;
        Action<object> action = (o) => SetColorsv2(camera, leftOver, (DataBlock)o);
        for (int i = 0; i < worldBlocks.GetLength(0); i++)
        {
            for (int j = 0; j < worldBlocks.GetLength(1); j++)
            {
                block = worldBlocks[i, j];
                tasks[i + worldBlocks.GetLength(0) * j] = new Task(action,block);
                tasks[i + worldBlocks.GetLength(0) * j].Start();
            }
        }
        Task.WaitAll(tasks);
        texture.SetData(screenColors);

    }
    public void ThreadedSetScreenColors(int threadCount, Rectangle camera, Rectangle prevCamera)
    {


        Thread[] threads = new Thread[threadCount];
        List<DataBlock>[] workQueues = new List<DataBlock>[threadCount];
        int a = 0;
        for (int i = 0; i < threadCount; i++)
        {
            workQueues[i] = new List<DataBlock>();
        }
        for (int i = 0; i < worldBlocks.GetLength(0); i++)
        {
            for (int j = 0; j < worldBlocks.GetLength(1); j++)
            {
                if (worldBlocks[i, j] == null) continue;
                if (!(worldBlocks[i, j].block.Intersects(camera))) continue;
                workQueues[a % threadCount].Add(worldBlocks[i, j]);
                a++;
            }
        }
        int[] temp = new int[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread((object i) => SetScreenColors(workQueues[(int)i], camera));
            threads[i].Start(i);
        }
        for (int i = 0; i < threadCount; i++)
        {
            threads[i].Join();
        }

    }
    private void SetScreenColors(List<DataBlock> queue, Rectangle camera)
    {
        foreach (DataBlock block in queue)
        {
            SetColors(camera, block);
        }

    }
    public void SetColors(Rectangle camera, DataBlock block)
    {
        if (block == null) return;
        if (!(block.block.Intersects(camera))) return;
        Rectangle area = Rectangle.Intersect(camera, block.block);
        Rectangle dataArea = new Rectangle((area.X - block.block.X) * 5, (area.Y - block.block.Y) * 5, area.Width * 5, area.Height * 5);
        Rectangle textureArea = new Rectangle((area.X - camera.X) * 5, (area.Y - camera.Y) * 5, area.Width * 5, area.Height * 5);
        for (int i = 0; i < dataArea.Width; i++)
        {
            for (int j = 0; j < dataArea.Height; j++)
            {
                screenColors[i + textureArea.X + 5 * camera.Width * (j + textureArea.Y)] = block.colors[i + dataArea.X + block.data[0].GetLength(0) * (j + dataArea.Y)];
            }
        }
    }
    public void SetColorsv2(Rectangle camera, Rectangle realArea, DataBlock block)
    {
        if (block == null) return;
        if (!(block.block.Intersects(realArea))) return;
        Rectangle area = Rectangle.Intersect(realArea, block.block);
        Rectangle dataArea = new Rectangle((area.X - block.block.X) * 5, (area.Y - block.block.Y) * 5, area.Width * 5, area.Height * 5);
        Rectangle textureArea = new Rectangle((area.X - camera.X) * 5, (area.Y - camera.Y) * 5, area.Width * 5, area.Height * 5);
        for (int i = 0; i < dataArea.Width; i++)
        {
            for (int j = 0; j < dataArea.Height; j++)
            {
                screenColors[i + textureArea.X + 5 * camera.Width * (j + textureArea.Y)] = block.colors[i + dataArea.X + block.data[0].GetLength(0) * (j + dataArea.Y)];
            }
        }
    }
    public void setTexture(Rectangle mapSection)
    {
        int x, y, lenX, lenY, dataX, dataY;
        byte[][,] data = new byte[3][,];
        lenX = mapSection.Width;
        lenY = mapSection.Height;
        data[0] = new byte[lenX * 5, lenY * 5];
        data[1] = new byte[lenX * 5, lenY * 5];
        data[2] = new byte[lenX * 5, lenY * 5];

        Color[] colors = new Color[(lenX * 5) * (lenY * 5)];

        for (int i = 0; i < lenX; i++)
        {
            x = ((mapSection.X + i) - (mapSection.X + i) % 10) / 10;
            for (int j = 0; j < lenY; j++)
            {
                y = ((mapSection.Y + j) - (mapSection.Y + j) % 10) / 10;
                dataX = (mapSection.X + i) - worldBlocks[x, y].block.X;
                dataY = (mapSection.Y + j) - worldBlocks[x, y].block.Y;
                for (int a = 0; a < 5; a++)
                {
                    for (int b = 0; b < 5; b++)
                    {
                        colors[(i * (5) + a) + (lenX * 5) * (j * (5) + b)] = GetColor(worldBlocks[x, y].data[0][dataX * 5 + a, dataY * 5 + b], worldBlocks[x, y].data[1][dataX * 5 + a, dataY * 5 + b], worldBlocks[x, y].data[2][dataX * 5 + a, dataY * 5 + b]);
                    }
                }
            }
        }
        texture.SetData(colors);
    }



    public void GenerateWorldTiles(GraphicsDevice gd, int threadCount, Point resolution)
    {
        Thread[] threads = new Thread[threadCount];
        Queue<int[]>[] workQueues = new Queue<int[]>[threadCount];
        int a = 0;
        for (int i = 0; i < threadCount; i++)
        {
            workQueues[i] = new Queue<int[]>();
        }
        for (int i = 0; i < height.GetLength(0); i++)
        {
            for (int j = 0; j < height.GetLength(1); j++)
            {
                workQueues[a % threadCount].Enqueue(new int[2] { i, j });
                a++;
            }
        }
        int[] temp = new int[threadCount];
        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread((object i) => GenerateTiles(workQueues[(int)i], gd, resolution));
            threads[i].Start(i);
        }

    }
    public void GenerateWorldTilesV2(GraphicsDevice gd, int threadCount, Point resolution, Point rectSize)
    {
        Thread[] threads = new Thread[threadCount];
        Queue<Rectangle>[] workQueues = new Queue<Rectangle>[threadCount];
        int a = 0;
        for (int i = 0; i < threadCount; i++)
        {
            workQueues[i] = new Queue<Rectangle>();
        }
        for (int i = 0; i < height.GetLength(0) * (100 / rectSize.X); i++)
        {
            for (int j = 0; j < height.GetLength(1) * (100 / rectSize.Y); j++)
            {
                workQueues[a % threadCount].Enqueue(new Rectangle(i * rectSize.X, j * rectSize.Y, rectSize.X, rectSize.Y));
                a++;
            }
        }
        int[] temp = new int[threadCount];
        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread((object i) => GenerateTiles(workQueues[(int)i], gd, resolution, rectSize));
            threads[i].Start(i);
        }

    }
    public void GenerateWorldTilesV3(GraphicsDevice gd, int threadCount, Point resolution, Point rectSize, Rectangle camera)
    {
        Thread[] threads = new Thread[threadCount];
        Queue<Rectangle>[] workQueues = new Queue<Rectangle>[threadCount];
        Rectangle temp1;
        int a = 0;
        for (int i = 0; i < threadCount; i++)
        {
            workQueues[i] = new Queue<Rectangle>();
        }
        for (int i = 0; i < height.GetLength(0) * (100 / rectSize.X); i++)
        {
            for (int j = 0; j < height.GetLength(1) * (100 / rectSize.Y); j++)
            {
                temp1 = new Rectangle(i * rectSize.X, j * rectSize.Y, rectSize.X, rectSize.Y);
                if (temp1.Intersects(camera))
                {
                    workQueues[a % threadCount].Enqueue(new Rectangle(i * rectSize.X, j * rectSize.Y, rectSize.X, rectSize.Y));
                    a++;
                }
            }
        }
        int[] temp = new int[threadCount];
        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread((object i) => GenerateTiles(workQueues[(int)i], gd, resolution, rectSize));
            threads[i].Start(i);
        }

    }
    private void GenerateTiles(Queue<int[]> queue, GraphicsDevice gd, Point resolution)
    {
        foreach (int[] coords in queue)
        {
            GenerateTile(coords[0], coords[1], gd, resolution);
        }

    }
    private void GenerateTiles(Queue<Rectangle> queue, GraphicsDevice gd, Point resolution, Point rectSize)
    {
        foreach (Rectangle part in queue)
        {
            worldTiles[part.X / rectSize.X, part.Y / rectSize.Y] = GenerateTile(gd, resolution, part);
        }
    }
    private Tile GenerateTile(GraphicsDevice gd, Point resolution, Rectangle part)
    {
        Point temporary = new Point((int)((resolution.X / (double)part.Width) * part.X), (int)((resolution.Y / (double)part.Height) * part.Y));
        Rectangle rect = new Rectangle(temporary, resolution);
        byte[,] h, f, t;
        h = GetHeight(part, resolution);
        f = GetFertility(part, resolution, h);
        t = GetTemperature(part, resolution, h);
        Color[] data = scaleColor(h, f, t, resolution, resolution);
        Texture2D texture = new Texture2D(gd, resolution.X, resolution.Y);
        texture.SetData(data);
        Tile temp = new Tile(rect, texture);
        return temp;
    }
    private void GenerateTile(int x, int y, GraphicsDevice gd, Point resolution)
    {
        Rectangle tile = new Rectangle(x * 100, y * 100, 100, 100);
        Point tem = new Point(100, 100);
        byte[,] h, f, t;
        h = GetHeight(tile, resolution);
        f = GetFertility(tile, resolution, h);
        t = GetTemperature(tile, resolution, h);
        Color[] data = scaleColor(h, f, t, tem, resolution);
        Texture2D texture = new Texture2D(gd, tem.X, tem.Y);
        texture.SetData(data);
        Tile temp = new Tile(tile, texture);
        worldTiles[x, y] = temp;

    }


    public void DataBlockGeneration()
    {
        Rectangle current;
        Task[] tasks = new Task[worldBlocks.GetLength(0) * worldBlocks.GetLength(1)];
        Action<object> action = (o) => GenerateBlockV2((Rectangle)o);
        for (int i = 0; i < worldBlocks.GetLength(0); i++)
        {
            for (int j = 0; j < worldBlocks.GetLength(1); j++)
            {
                current = new Rectangle(i * 10, j * 10, 10, 10);

                tasks[i + worldBlocks.GetLength(0) * j] = new Task(action, current);
                tasks[i + worldBlocks.GetLength(0) * j].Start();
            }
        }

    }
    public void DataBlockGeneration(Rectangle camera, int threadCount)
    {
        Rectangle current;
        int lenX, lenY;
        lenX = worldBlocks.GetLength(0);
        lenY = worldBlocks.GetLength(1);
        int a = 0;
        Queue<Rectangle>[] workQueues = new Queue<Rectangle>[threadCount];
        Thread[] threads = new Thread[threadCount];

        for (int i = 0; i < threadCount; i++)
        {
            workQueues[i] = new Queue<Rectangle>();
        }

        for (int i = 0; i < lenX; i++)
        {
            for (int j = 0; j < lenY; j++)
            {
                current = new Rectangle(i * 10, j * 10, 10, 10);
                if (worldBlocks != null && camera.Intersects(current))
                {
                    workQueues[a % threadCount].Enqueue(current);
                    a++;
                }
            }
        }

        for (int i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread((object i) => GenerateDataBlock(workQueues[(int)i]));
            threads[i].Start(i);
        }

    }
    private void GenerateDataBlock(Queue<Rectangle> queue)
    {
        foreach (Rectangle part in queue)
        {
            worldBlocks[part.X / 10, part.Y / 10] = GenerateBlock(part);
        }
    }
    private DataBlock GenerateBlock(Rectangle part)
    {
        Point size = new Point(50, 50);
        byte[,] h, t, f;
        h = GetHeight(part, size);
        t = GetTemperature(part, size, h);
        f = GetFertility(part, size, h);
        return new DataBlock(part, h, t, f);
    }
    private void GenerateBlockV2(Rectangle part)
    {
        Point size = new Point(50, 50);
        byte[,] h, t, f;
        h = GetHeight(part, size);
        t = GetTemperature(part, size, h);
        f = GetFertility(part, size, h);
        worldBlocks[part.X / 10, part.Y / 10] = new DataBlock(part, h, t, f);
        SetTextureV3(world, worldBlocks[part.X / 10, part.Y / 10], ref texture2);
    }
    private void GenerateDataSection(ref byte[,] hp, ref byte[,] tp, ref byte[,] fp, Rectangle part, Point resolution, ref Texture2D texture)
    {
        int startX = part.X * 5;
        int startY = part.Y * 5;
        byte[,] h, t, f;
        h = GetHeight(part, resolution);
        t = GetTemperature(part, resolution, h);
        f = GetFertility(part, resolution, h);
        Color[] colors = new Color[h.GetLength(0) * h.GetLength(1)];
        for (int i = 0; i < h.GetLength(0); i++)
        {
            for (int j = 0; j < h.GetLength(1); j++)
            {
                hp[startX + i, startY + j] = h[i, j];
            }
        }

    }



    private byte[,] GetHeight(Rectangle section, Point size)
    {
        return TheNewWorld.GeneratePerlinValuesV2(section, size, height, 6, 0.6f, heightMinMax[0], heightMinMax[1], topology.getFilter(section, size));
    }
    private byte[,] GetTemperature(Rectangle section, Point size, byte[,] height)
    {
        float[,] temporaryfilter = topology.getFilter(section, height, 0.5, 2, true, equator);
        return TheNewWorld.GeneratePerlinValuesV2(section, size, temperature, 4, 0.75f, tempMinMax[0], tempMinMax[1], temporaryfilter);
    }
    private byte[,] GetFertility(Rectangle section, Point size, byte[,] height)
    {
        float[,] temporaryfilter = topology.getFilter(height, 0.5, 1, true);
        return TheNewWorld.GeneratePerlinValuesV2(section, size, fertility, 2, 1f, fertMinMax[0], fertMinMax[1], temporaryfilter);
    }



    private Color[] scaleColor(byte[,] h, byte[,] f, byte[,] t, Point targetSize, Point size)
    {
        Color[] color = new Color[targetSize.X * targetSize.Y];

        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                for (int a = 0; a < targetSize.X / size.X; a++)
                {
                    for (int b = 0; b < targetSize.Y / size.Y; b++)
                    {
                        color[(i * (targetSize.Y / size.Y) + a) + targetSize.X * (j * (targetSize.X / size.X) + b)] = GetColor(h[i, j], t[i, j], f[i, j]);
                    }
                }
            }
        }
        return color;
    }
    private Color GetColor(byte h, byte t, byte f)
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

        if (height > 0.8) return Color.Lerp(Color.Gray, Color.White, (float)(((height - 0.8) / 0.2) - ((height - 0.8) / 0.2) % 0.05));
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
                    colorLower = Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.Olive, 0.3f);
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


    bool imp = false;
    int timer = 0;
    public void Update(GameTime gameTime)
    {
        int lenX, lenY;
        double max, temp;

        lenX = worldBlocks.GetLength(0);
        lenY = worldBlocks.GetLength(1);
        max = lenX * lenY;
        temp = 0.0;

        for (int i = 0; i < lenX; i++)
        {
            for (int j = 0; j < lenY; j++)
            {
                if (worldBlocks[i, j] == null) continue;
                temp += 1.0;
            }
        }
        if (temp == max && !imp)
        {
            loading = 100;
            for (int i = 0; i < lenX; i++)
            {
                for (int j = 0; j < lenY; j++)
                {
                    //SetTextureV2(camera, worldBlocks[i, j]);
                    //SetColors(camera, worldBlocks[i, j]);
                }
            }
            ThreadedSetScreenColorsV2(camera, prevCamera);
            imp = true;
            //texture.SetData(screenColors);
            return;
        }
        loading = (int)(100 * temp / max);
        if (imp)
        {
            for (int i = 0; i < lenX; i++)
            {
                for (int j = 0; j < lenY; j++)
                {
                    //SetTextureV2(camera, worldBlocks[i, j]);
                    //SetColors(camera, worldBlocks[i, j]);
                }
            }
            //ThreadedSetScreenColors(2, camera, prevCamera);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {

                boundingWorld.Y -= 2;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                boundingWorld.Y += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                boundingWorld.X -= 2;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                boundingWorld.X += 2;
            }
            //ThreadedSetTexture(16, camera, prevCamera);
            ThreadedSetScreenColorsV3(camera, prevCamera);

            prevCamera = camera;
        }
    }
    public void Render(SpriteBatch sb, SpriteFont font)
    {
        /*for(int i =0; i< worldTiles.GetLength(0); i++)
        {
            for (int j = 0; j < worldTiles.GetLength(1); j++)
            {
                if (worldTiles[i, j].Empty()) continue;
                sb.Draw(worldTiles[i, j].texture, worldTiles[i, j].position, Color.White);
            }
        }
        //texture.SetData(screenColors);
        sb.Draw(texture, mapSection, Color.White);
        sb.DrawString(font, loading.ToString(), new Vector2(0, 0), Color.Red);
        if (imp)
        {
            sb.Draw(texture2, boundingWorld, Color.White);
        }
    }
}
class Tree
{
    int x, y, age;
    public Tree(int x, int y)
    {
        this.x = x;
        this.y = y;
        age = 0;
    }
}
class DataBlock
{
    public byte[][,] data;
    public Rectangle block;
    public Color[] colors;
    public DataBlock(Rectangle block, byte[,] h, byte[,] t, byte[,] f)
    {
        this.block = block;
        data = new byte[3][,];
        data[0] = h;
        data[1] = t;
        data[2] = f;
        colors = SetColors();
    }

    public Rectangle Translate(int level)
    {
        return new Rectangle(block.X * level, block.Y * level, block.Width * level, block.Height * level);
    }
    private Color GetColor(byte h, byte t, byte f)
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

        if (height > 0.8) return Color.Lerp(Color.Gray, Color.White, (float)(((height - 0.8) / 0.2) - ((height - 0.8) / 0.2) % 0.05));
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
                    colorLower = Color.Lerp(Color.Lerp(Color.DarkGreen, Color.Black, 0.35f), Color.Olive, 0.3f);
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
    public Color[] GetColorsInArea(Rectangle area)
    {
        Color[] color = new Color[area.Width * area.Height];

        for (int i = 0; i < area.Width; i++)
        {
            for (int j = 0; j < area.Height; j++)
            {
                color[i + area.Width * j] = colors[i + area.X + data[0].GetLength(0) * (j + area.Y)];
            }
        }

        return color;
    }
    private Color[] SetColors()
    {
        Color[] color = new Color[data[0].GetLength(0) * data[0].GetLength(1)];

        for (int i = 0; i < data[0].GetLength(0); i++)
        {
            for (int j = 0; j < data[0].GetLength(1); j++)
            {
                color[i + data[0].GetLength(0) * j] = GetColor(data[0][i, j], data[1][i, j], data[2][i, j]);
            }
        }
        return color;
    }
    public Color[] scaleColor(Point targetSize)
    {
        Color[] color = new Color[targetSize.X * targetSize.Y];

        for (int i = 0; i < data[0].GetLength(0); i++)
        {
            for (int j = 0; j < data[0].GetLength(1); j++)
            {
                for (int a = 0; a < targetSize.X / data[0].GetLength(0); a++)
                {
                    for (int b = 0; b < targetSize.Y / data[0].GetLength(1); b++)
                    {
                        color[(i * (targetSize.Y / data[0].GetLength(1)) + a) + targetSize.X * (j * (targetSize.X / data[0].GetLength(1)) + b)] = GetColor(data[0][i, j], data[1][i, j], data[2][i, j]);
                    }
                }
            }
        }
        return color;
    }
}
}

 
 */
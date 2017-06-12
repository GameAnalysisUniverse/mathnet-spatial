using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace MathNet.Spatial.Functions
{
    public class BresenhamAlgorithm
    {
        /// <summary>
        /// Bresenham line stepping:
        /// Collect points on a line from start to end in a certain stepsize.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="maplevel"></param>
        /// <returns></returns>
        public static Point2D[] BresenhamLineStepping2D(Point2D start, Point2D end, Point2D coord_origin, float cellwidth, float cellheight, float stepsize)
        {
            List<Point2D> brespoints = new List<Point2D>();
            int stepcount = 0;
            var agpx = GridFunctions.GetGridPosX(start.X, coord_origin.X, cellwidth);
            var agpy = GridFunctions.GetGridPosY(start.Y, coord_origin.Y, cellheight);
            var rgpx = GridFunctions.GetGridPosX(end.X, coord_origin.X, cellwidth);
            var rgpy = GridFunctions.GetGridPosY(end.Y, coord_origin.Y, cellheight);

            var dx = end.X - start.X;
            var dy = end.Y - start.Y;

            float adx = (float)Math.Abs(dx);
            float ady = (float)Math.Abs(dy);

            var sdx = Math.Sign(dx) * stepsize;
            var sdy = Math.Sign(dy) * stepsize;

            float pdx, pdy, ddx, ddy, es, el;
            if (adx > ady)
            {
                pdx = sdy; pdy = 0;
                ddx = sdx; ddy = sdy;
                es = ady; el = adx;
            }
            else
            {
                pdx = 0; pdy = sdy;
                ddx = sdx; ddy = sdy;
                es = adx; el = ady;
            }

            var x = start.X;
            var y = start.Y;

            var error = el / 2;

            for (int i = 1; i < el; i++) // Iterate every point
            {
                error -= es;
                if (error < 0)
                {
                    error += el;
                    x += ddx; y += ddy;
                }
                else
                {
                    x += pdx; y += pdy;
                }
                stepcount++;
                brespoints.Add(new Point2D(x, y));
            }

            return brespoints.ToArray();
        }
    }
}

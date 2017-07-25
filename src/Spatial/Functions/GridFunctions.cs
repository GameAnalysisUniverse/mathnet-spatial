using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace MathNet.Spatial.Functions
{
    public class GridFunctions
    {
        /// <summary>
        /// Return the X index of a cell in a grid with cellwidth and x origin(upperleft) of the grid as well as the width of the grid
        /// </summary>
        /// <param name="x"></param>
        /// <param name="originx"></param>
        /// <param name="cellwidth"></param>
        /// <returns></returns>
        public static int GetGridPosX(double x, double originx, float cellwidth)
        {
            var width = Math.Abs(originx - x);
            return (int)(width / cellwidth);
        }

        /// <summary>
        /// Return the Y index of a cell in a grid with cellheight and y origin(upperleft) of the grid as well as the height of the grid
        /// </summary>
        /// <param name="y"></param>
        /// <param name="originy"></param>
        /// <param name="cellheight"></param>
        /// <returns></returns>
        public static int GetGridPosY(double y, double originy, float cellheight)
        {
            var height = Math.Abs(originy - y);
            return (int)(height / cellheight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="coord_origin"></param>
        /// <param name="cellwidth"></param>
        /// <param name="cellheight"></param>
        /// <param name="stepsize"></param>
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
    

    /// <summary>
    /// Get the bounding rectangle of a pointset
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Rectangle2D GetPointCloudBoundings(List<Point2D> data)
        {
            var min_x = data.Min(point => point.X);
            var min_y = data.Min(point => point.Y);
            var max_x = data.Max(point => point.X);
            var max_y = data.Max(point => point.Y);
            var dx = max_x - min_x;
            var dy = max_y - min_y;
            return new Rectangle2D(new Point2D(min_x, max_y), dx, dy); // Why max_y? not applicable for all games
        }

        /// <summary>
        /// Get the bounding rectangle between to positions.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Rectangle2D GetRectFromPoints(Point3D v1, Point3D v2)
        {
            var min_x = Math.Min(v1.X, v2.X);
            var max_x = Math.Max(v1.X, v2.X);
            var min_y = Math.Min(v1.Y, v2.Y);
            var max_y = Math.Max(v1.Y, v2.Y);
            var dx = max_x - min_x;
            var dy = max_y - min_y;
            if (dx == 0) dx = 0.1; //TODO: prevent empty rectangles for tree box queries
            if (dy == 0) dy = 0.1;
            return new Rectangle2D(new Point2D(min_x, min_y), dx, dy);
        }

    }
}

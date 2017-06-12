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
        /// Get the bounding rectangle of a pointset
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Rectangle2D getPointCloudBoundings(List<Point3D> data)
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
            return new Rectangle2D(new Point2D(min_x, min_y), dx, dy);
        }

    }
}

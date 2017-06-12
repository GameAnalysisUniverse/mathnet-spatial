using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Spatial.Functions;

namespace MathNet.Spatial.Euclidean
{
    public struct Rectangle2D : IEquatable<Rectangle2D>
    {

        /// <summary>
        /// Upper left point of the rectangle
        /// </summary>
        public readonly Point2D UpperLeftPoint;

        private readonly double Height;

        private readonly double Width;

        /// <summary>
        /// Constructor for the Rectangle2D
        /// </summary>
        /// <param name="startPoint">the Upper left point of the line</param>

        public Rectangle2D(Point2D upperLeftPoint, double width, double height)
        {
            this.UpperLeftPoint = upperLeftPoint;
            this.Width = width;
            this.Height = height;

            if (this.Width == 0 || this.Height == 0)
            {
                throw new ArgumentException("A rectangle cannot be empty (a point)");
            }
        }

        public System.Drawing.Rectangle getAsQuadTreeRect()
        {
            return new System.Drawing.Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }

        /// <summary>
        /// Intersection of a line with a rect. -> perform 4 line tests.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="r"></param>
        /// <returns></returnsV
        public Point3D? Rect2DIntersectsLine(Point2D start, Point2D end)
        {
            var r = this;
            // Build all lines of the Rectangle and test a line-line collision
            var los = new Line2D(start, end); //TODO: Ugly code
            var l1 = new Line2D(new Point2D((float)r.X, (float)r.Y), new Point2D((float)(r.X + r.Width), (float)r.Y));
            var l2 = new Line2D(new Point2D((float)(r.X + r.Width), (float)r.Y), new Point2D((float)(r.X + r.Width), (float)(r.Y + r.Height)));
            var l3 = new Line2D(new Point2D((float)(r.X + r.Width), (float)(r.Y + r.Height)), new Point2D((float)r.X, (float)(r.Y + r.Height)));
            var l4 = new Line2D(new Point2D((float)r.X, (float)(r.Y + r.Height)), new Point2D((float)r.X, (float)r.Y));

            var i1 = (Point2D)l1.IntersectWith(los);
            var i2 = (Point2D)l2.IntersectWith(los);
            var i3 = (Point2D)l3.IntersectWith(los);
            var i4 = (Point2D)l4.IntersectWith(los);
            List<Point2D> ps = new List<Point2D>();
            if (i1 != null) ps.Add(i1);
            if (l2 != null) ps.Add(i2);
            if (l3 != null) ps.Add(i3);
            if (l4 != null) ps.Add(i4);
            if (ps.Count == 0) return null;
            if (ps.Count > 2) throw new Exception("Too many collisions");
            return ps.OrderBy(point => DistanceFunctions.GetEuclidDistance2D(start, point)).First().ToPoint3D(); // Return point with lowest distance to p1
        }

        public bool Contains(Point2D p)
        {
            return true;
        }

        /// <summary>
        /// Intersection of two Rectangles
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Intersects(Rectangle2D a, Rectangle2D b)
        {
            if (b.X < a.X+a.Width && a.X < b.X+b.Width && b.Y < a.Y+a.Height)
                return a.Y < b.Y+b.Height;
            return false;
        }

        public bool Equals(Rectangle2D other)
        {
            return this.UpperLeftPoint.Equals(other.UpperLeftPoint) && (this.Width == other.Width) && (this.Height == other.Height);

        }

        public double X => UpperLeftPoint.X;
        public double Y => UpperLeftPoint.Y;

        public Point2D Center
        {
            get
            {
                return
                    new Point2D(UpperLeftPoint.X + Width / 2, UpperLeftPoint.Y + Height / 2);

            }
        }
    }
}

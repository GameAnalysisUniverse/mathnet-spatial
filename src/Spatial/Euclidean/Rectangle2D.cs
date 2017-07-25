using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Spatial.Functions;
using Newtonsoft.Json;

namespace MathNet.Spatial.Euclidean
{
    public class Rectangle2D : IEquatable<Rectangle2D>
    {

        /// <summary>
        /// Upper left point of the rectangle
        /// </summary>
        [JsonProperty]
        public readonly Point2D UpperLeftPoint;

        public double X => UpperLeftPoint.X;
        public double Y => UpperLeftPoint.Y;

        [JsonProperty]
        public readonly double Height;

        [JsonProperty]
        public readonly double Width;

        public float Right
        {
            get { return (float)(X + Width); }
        }

        public float Left
        {
            get { return (float)(X); }
        }

        public float Top
        {
            get { return (float)(Y); }
        }
        

        public float Bottom
        {
            get { return (float)(Y + Height); }
        }

        public Line2D Diagonal
        {
            get { return new Line2D(UpperLeftPoint, new Point2D(X + Width, Y + Height)); }
        }

        public Point2D Center
        {
            get { return new Point2D(UpperLeftPoint.X + Width / 2, UpperLeftPoint.Y + Height / 2); }
        }

        public float Size
        {
            get { return (float)(Width*Height); }
        }

        public Rectangle2D Copy()
        {
            return new Rectangle2D(UpperLeftPoint, Width, Height);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rectangle2D(double x, double y, double width, double height)
        {
            this.UpperLeftPoint = new Point2D(x, y);
            this.Width = width;
            this.Height = height;

            if (this.Width == 0 || this.Height == 0)
            {
                throw new ArgumentException("A rectangle cannot be empty (a point)");
            }
        }

        public Rectangle2D(double maxx, double maxy, double minx, double miny, bool orientation)
        {
            this.UpperLeftPoint = new Point2D(minx, miny);
            this.Width = maxx-minx;
            this.Height = maxy-miny;

            if (this.Width == 0 || this.Height == 0)
            {
                throw new ArgumentException("A rectangle cannot be empty (a point)");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="upperLeftPoint"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rectangle2D(Point2D upperLeftPoint, double width, double height)
        {
            this.UpperLeftPoint = upperLeftPoint;
            this.Width = width;
            this.Height = height;

            if (this.Width == 0 || this.Height == 0)
            {
                throw new ArgumentException("A rectangle dimension cannot be empty (a point)");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Point2D? Rect2DIntersectsLine(Point2D start, Point2D end)
        {
            var r = this;
            // Build all lines of the Rectangle and test a line-line collision
            var los = new Line2D(start, end); //TODO: Ugly code
            var l1 = new Line2D(new Point2D(r.X, r.Y), new Point2D(r.X + r.Width, r.Y));
            var l2 = new Line2D(new Point2D((r.X + r.Width), r.Y), new Point2D(r.X + r.Width, r.Y + r.Height));
            var l3 = new Line2D(new Point2D((r.X + r.Width), (r.Y + r.Height)), new Point2D( r.X, r.Y + r.Height));
            var l4 = new Line2D(new Point2D(r.X, r.Y + r.Height), new Point2D(r.X, r.Y));

            var i1 = (Point2D)l1.IntersectWith(los);
            var i2 = (Point2D)l2.IntersectWith(los);
            var i3 = (Point2D)l3.IntersectWith(los);
            var i4 = (Point2D)l4.IntersectWith(los);
            List<Point2D> ps = new List<Point2D>();
            if (i1 != null) ps.Add(i1);
            if (i2 != null) ps.Add(i2);
            if (i3 != null) ps.Add(i3);
            if (i4 != null) ps.Add(i4);
            if (ps.Count == 0) return null;
            if (ps.Count > 2) throw new Exception("Too many collisions. Collisionpoints: " + String.Join(", ", ps));
            return ps.OrderBy(point => DistanceFunctions.GetEuclidDistance2D(start, point)).First(); // Return point with shortest distance to start
        }

        /// <summary>
        /// Test if a rectangle contains a point
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool Contains(Point2D p)
        {
            if (p.X >= X && p.X <= X + Width && p.Y >= Y && p.Y <= Y + Height)
                return true;
            else return false;
        }

        /// <summary>
        /// Intersection of two Rectangles
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool Intersects(Rectangle2D b)
        {
            if (b.X < X + Width && X < b.X + b.Width && b.Y < Y + Height)
                return Y < b.Y + b.Height;
            return false;
        }

        public bool Equals(Rectangle2D other)
        {
            return this.UpperLeftPoint.Equals(other.UpperLeftPoint) && (this.Width == other.Width) && (this.Height == other.Height);

        }

        /// <summary>
        /// Rect is entirely contained in the rectangle 
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool Contains(Rectangle2D rect)
        {
            if (rect.Size > Size) return false; // A bigger rectangle cannot entirely be contained in this one
            //If we have a rectangle at the same reference point and it has smaller dimension
            if (UpperLeftPoint.Equals(rect.UpperLeftPoint) && rect.Width <= Width && rect.Height <= Height) return true;
            //Only if all four edge points are within the rectangle - rect is in the rectangle entirely
            if (Contains(UpperLeftPoint) && Contains(new Point2D(X + Width, Y)) && Contains(new Point2D(X + Width, Y+Height)) && Contains(new Point2D(X , Y+Height))) return true;

            return false;
        }

        public void Union(Rectangle2D r)
        {

        }

        /**
 * Determine whether an edge of this rectangle overlies the equivalent 
 * edge of the passed rectangle
 */
        public bool edgeOverlaps(Rectangle2D r)
        {
            return false;
        }
    }
}

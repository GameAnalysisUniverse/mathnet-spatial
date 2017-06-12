using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using MathNet.Spatial.Interfaces;

namespace MathNet.Spatial.Functions
{
    public class DistanceFunctions
    {
        /// <summary>
        /// Returns euclid distance between point p1 and p2
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetEuclidDistance2D(Point2D p1, Point2D p2)
        {
            if (p2 == null || p1 == null) throw new Exception("Vector cant be null");
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
        public static double GetEuclidDistance2D(Vector2D p1, Vector2D p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        /// <summary>
        /// Returns euclid distance in 3D space
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetEuclidDistance3D(Point3D p1, Point3D p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
        }
        public static double GetEuclidDistance3D(Vector3D p1, Vector3D p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
        }

        /// <summary>
        /// Return the median of a list of float values
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static float GetMedian(IEnumerable<float> source)
        {
            float[] temp = source.ToArray();
            Array.Sort(temp);

            int count = temp.Length;
            if (count == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }
            else if (count % 2 == 0)
            {
                float a = temp[count / 2 - 1];
                float b = temp[count / 2];
                return (a + b) / 2.0f;
            }
            else
            {
                return temp[count / 2];
            }
        }

        public static double GetManhattanDistance2D(Point2D p1, Point2D p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }

        //
        //
        // GENERIC VERSIONS
        //
        //

        public static T GetMedian<T>(IEnumerable<T> source) where T : IArithmetic
        {
            // Create a copy of the input, and sort the copy
            T[] temp = source.ToArray();
            Array.Sort(temp); // @TODO: T needs to implement IComparable !!!

            int count = temp.Length;
            if (count == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }
            else if (count % 2 == 0)
            {
                // count is even, average two middle elements
                T a = temp[count / 2 - 1];
                T b = temp[count / 2];
                return a.Add(b).Bisect<T>();
            }
            else
            {
                // count is odd, return the middle element
                return temp[count / 2];
            }
        }
    }
}
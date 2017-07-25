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
    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetEuclidDistance2D(Vector2D p1, Vector2D p2)
        {
            if (p2 == null || p1 == null) throw new Exception("Vector cant be null");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetEuclidDistance3D(Vector3D p1, Vector3D p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetManhattanDistance2D(Point2D p1, Point2D p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetMaximumsDistance2D(Point2D p1, Point2D p2)
        {
            return Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
        }

        //
        //
        // GENERIC VERSIONS
        //
        //

        /// <summary>
        /// Returns euclid distance between p1 and p2
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetEuclidDistance<T>(T p1, T p2)
        {
            if (p2 == null || p1 == null) throw new Exception("Vector cant be null");
            return 0;
        }

        /// <summary>
        /// Returns manhattan distance between p1 and p2
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetManhattanDistance<T>(T p1, T p2)
        {
            if (p2 == null || p1 == null) throw new Exception("Vector cant be null");
            return 0;
        }

        
    }
}
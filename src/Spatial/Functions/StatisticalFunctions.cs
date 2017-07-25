using MathNet.Spatial.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathNet.Spatial.Functions
{
    public class StatisticalFunctions
    {
        public static T ArithmeticMean<T>(T[] source) where T : IArithmetic
        {
            T sum = source[0];
            for (int i = 1; i < source.Length; i++)
                sum.Add(source[i]);

            return sum.Divide<T>(source.Length);

        }

        public static float ArithmeticMean(float[] source)
        {
            float sum = source[0];
            for (int i = 1; i < source.Length; i++)
                sum += source[i];

            return sum/source.Length;

        }

        /// <summary>
        /// Return the median of a list of float values
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static float Median(float[] source)
        {
            float[] temp = source;
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Median<T>(IEnumerable<T> source) where T : IArithmetic
        {
            // Create a copy of the input, and sort the copy
            T[] temp = source.ToArray();
            Array.Sort(temp); // @TODO: T needs to implement IComparable !!!

            int count = temp.Length;
            if (count == 0)
                throw new InvalidOperationException("Empty collection");

            else if (count % 2 == 0)
            {
                // count is even, average two middle elements
                T a = temp[count / 2 - 1];
                T b = temp[count / 2];
                return a.Add(b).Bisect<T>(); //Half the value as T should be halfed
            }
            else
                return temp[count / 2]; // count is odd, return the middle element
        }
    }
}

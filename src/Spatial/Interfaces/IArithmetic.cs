using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathNet.Spatial.Interfaces
{
    /// <summary>
    /// Interface for a datatype to determine basic calculation operations
    /// </summary>
    public interface IArithmetic
    {
        /// <summary>
        /// Define how two elements of type T add up
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="b"></param>
        /// <returns></returns>
        T Add<T>(T b) where T : IArithmetic;


        T Substract<T>(T b) where T : IArithmetic;

        T Multiply<T>(T b) where T : IArithmetic;

        T Divide<T>(T b) where T : IArithmetic;

        T Divide<T>(int a) where T : IArithmetic;

        /// <summary>
        /// How do you bisect a value of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Bisect<T>() where T : IArithmetic;
    }
}

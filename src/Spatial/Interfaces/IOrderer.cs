using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clustering
{
    public interface IOrderer
    {
        /// <summary>
        /// Define a method on how to order a list of this object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        T[] OrderData<T>(IEnumerable<T> list);
    }
}

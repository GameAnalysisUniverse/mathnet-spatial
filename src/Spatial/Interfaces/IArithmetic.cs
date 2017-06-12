using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathNet.Spatial.Interfaces
{
    public interface IArithmetic
    {
        T Add<T>(T b) where T : IArithmetic;
        T Bisect<T>() where T : IArithmetic;
    }
}

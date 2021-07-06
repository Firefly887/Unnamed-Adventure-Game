using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvGameLib
{
    interface IActions
    {
        void Use(byte noun, byte verb);
        void Go(byte noun);
        void Talk(byte noun);
        void Shoot(byte noun);
    }
}

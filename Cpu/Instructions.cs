using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    public class Instructions
    {
        private Cpu Cpu;
        public Instructions(Cpu c)
        {
            Cpu = c;
        }

        public void Execute(byte opcode)
        {

        }
    }
}

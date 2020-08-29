using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    public class Gameboy
    {
        public MemoryBus Bus;
        public Cpu Cpu;
        public Gpu Gpu;

        // TODO: Sound

        private bool isPaused;

        private int timerCounter;


        public Gameboy()
        {
          
        }
    }
}

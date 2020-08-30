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
        public Ppu Ppu;

        // TODO: Sound

        private bool isPaused;

        private int timerCounter;


        public Gameboy(string romPath)
        {
            Bus = new MemoryBus();
            Cpu = new Cpu(Bus);
            Ppu = new Ppu();

            // TODO: APU / sound

        }

        public void Loop()
        {
            // TODO
            while(true)
            {
                Update();
            }
        }

        // Update the state of the Gameboy by a single frame
        private int Update()
        {
            if (isPaused)
            {
                return 0;
            }

            var cyclesOp = Cpu.ExecuteNextOpcode();


            return 1; // TODO: cycles
        }
    }
}

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
            switch (opcode)
            {
                case 0x00:
                    // NOP
                    break;

                case 0xC3:
                    // JMP nn
                    jump(Cpu.PopPC16());
                    break;

                default:
                    Console.WriteLine("UNKNOWN OPCODE: 0x{0}", opcode.ToString("X2"));
                    break;
            }
        }

        public void jump(UInt16 address)
        {
            Cpu.PC = address;
        }
    }
}

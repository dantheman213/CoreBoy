using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    // Emulate Gameboy 8-bit CPU 
    // This CPU is known as Sharp LR35902 and is similar to Intel 8080 and Zilog Z80.
    public class Cpu
    {
        // Registers are grouped together. There are instructions that allow game to read and write 16 bits (2 bytes) of data.
        // Use Hi or Lo to access specific register data.
        // F - Flags Register. 7 = zero, 6 = subtract, 5 = half carry, 4 = carry
        public Register AF;
        public Register BC;
        public Register DE;
        public Register HL;

        public Register SP; // Stack Pointer - Gameboy CPU has built in support for stack-like data structure in memory. 'SP' points to where top of stack is.
        public UInt16 PC; // Program Counter - location of what byte is currently being executed

        public int Divider;

        public Cpu()
        {
            AF = new Register();
            BC = new Register();
            DE = new Register();
            HL = new Register();
            SP = new Register();

            PC = 0x100;

            BC.Set(0x0000);
            DE.Set(0xFF56);
            HL.Set(0x000D);
            SP.Set(0xFFFE);

            AF.SetMask(0xFFF0);
        }

        private void setFlag(byte index, bool on)
        {
            if (on)
            {
                AF.SetLo(BitHelper.Set(AF.Lo(), index));
            }
            else
            {
                AF.SetLo(BitHelper.Reset(AF.Lo(), index));
            }
        }

        public bool Z()
        {
            return (AF.HiLo() >> 7 & 1) == 1;
        }
        
        public void SetZ(bool on)
        {
            setFlag(7, on);
        }

        public bool N()
        {
            return (AF.HiLo() >> 6 & 1) == 1;
        }

        public void SetN(bool on)
        {
            setFlag(6, on);
        }

        public bool H()
        {
            return (AF.HiLo() >> 5 & 1) == 1;
        }

        public void SetH(bool on)
        {
            setFlag(5, on);
        }

        public bool C()
        {
            return (AF.HiLo() >> 4 & 1) == 1;
        }

        public void SetC(bool on)
        {
            setFlag(4, on);
        }

        private UInt16 popStack()
        {
            var sp = SP.HiLo();
            // TODO

            return 0;
        }
    }
}

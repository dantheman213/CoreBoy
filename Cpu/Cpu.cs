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
        // Registers are super fast memory cells located within the CPU. Each register (A, B, C, D, E, F, H, L) is 8-bit. 
        // Because CPU instructions often groups registers together we'll group them together by default.
        // These grouped registers allow instructions to read and write 16 bits (2 bytes) of data.
        // Use Hi or Lo to access specific register data from a grouped register pair.
        // F - Flags Register. 7 = zero, 6 = subtract, 5 = half carry, 4 = carry
        public Register AF;
        public Register BC;
        public Register DE;
        public Register HL;

        // Special 16-bit native registers
        public Register SP; // Stack Pointer - Gameboy CPU has built in support for stack-like data structure in memory. 'SP' points to where top of stack is.
        public UInt16 PC; // Program Counter - location of what byte is currently being executed

        public int Divider;

        private MemoryBus Memory;
        private Instructions Instructions;

        public Cpu(MemoryBus m)
        {
            Memory = m;
            Instructions = new Instructions(this);

            AF = new Register();
            BC = new Register();
            DE = new Register();
            HL = new Register();
            SP = new Register();

            // set the initial state of the CPU
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

        // Push a 16 bit value onto the stack and decrement SP.
        private void pushStack(UInt16 address)
        {
            var sp = SP.HiLo();
            Memory.Write((ushort)(sp - 1), (byte)((address & 0xFF00) >> 8));
            Memory.Write((ushort)(sp - 2), (byte)(address & 0xFF));
            SP.Set((ushort)(SP.HiLo() - 2));
        }

        // Pop the next 16 bit value off the stack and increment SP.
        private UInt16 popStack()
        {
            var sp = SP.HiLo();
            var byte1 = Memory.Read(sp);
            var byte2 = Memory.Read((ushort)(sp + 1 << 8));
            SP.Set((ushort)(SP.HiLo() + 2));

            return (ushort)(byte1 | byte2);
        }

        private byte popPC()
        {
            var opcode = Memory.Read(PC);
            PC += 1;
            return opcode;
        }

        public int ExecuteNextOpcode()
        {
            var opcode = popPC();
            Instructions.Execute(opcode);
            return 1; // TODO: CPU ticks
        }
    }
}

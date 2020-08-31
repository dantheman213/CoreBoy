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
        // Registers are super fast memory cells located within the CPU. Each register (A, B, C, D, E, F, H, L) is 8-bit (contain 1 byte).
        // Because CPU instructions often groups registers together we'll group them together by default.
        // These grouped registers allow instructions to read and write 16 bits (2 bytes) of data.
        // Use Hi, Lo, HiLo to access specific register data from a grouped register pair.
        //

        // Register Pair AF
        // Arithmitec operations take place on Register A and is arguably the most important register.
        // Register F is paired with A and is named Flags Register. 
        // 7 = zero, 6 = subtract, 5 = half carry, 4 = carry
        public Register AF;

        // Register pair BC registers are generally used as counters during repetitive blocks of code such as moving data from one location to another.
        public Register BC;

        // Registers pair DE are generally used together as a 16-bit register for holding a destination address in moving data from one address to another. 
        // They can be used for other operations though, as much as the instructions will permit.
        public Register DE;

        // Register pair HL
        // Similar to BC and DE. Extensively used for indirect addressing.
        // Only register pair that can be used indirectly in the instrutions ADC, ADD, AND, CP, DEC, INC, OR, SUB, and XOR.
        public Register HL;

        // Special 16-bit native registers
        //

        // Stack Pointer - Gameboy CPU has built in support for stack-like data structure in memory. 'SP' points to where top of stack is.
        // Values from instructions PUSH, POP, CALL, and RET are placed or taken.
        public Register SP;

        // Program Counter - location of what byte is currently being executed
        public UInt16 PC;

        // Current CPU multiplier (1x or 2x)
        public byte currentSpeed;
        public bool prepareSpeed;

        public bool InterruptsEnabling;
        public bool InterruptsOn;
        public bool Halted;

        public int Divider;

        public int ticks;

        private MemoryBus Memory;
        private Instructions Instructions;

        public Cpu(ref MemoryBus m)
        {
            Memory = m;
        
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

        public void SetInstructions(ref Instructions i)
        {
            Instructions = i;
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

        // Get Zero flag
        public bool Z()
        {
            return (AF.HiLo() >> 7 & 1) == 1;
        }
       
        public void SetZ(bool on)
        {
            setFlag(7, on);
        }

        // Get subtract flag
        public bool N()
        {
            return (AF.HiLo() >> 6 & 1) == 1;
        }

        public void SetN(bool on)
        {
            setFlag(6, on);
        }

        // Get half carry flag
        public bool H()
        {
            return (AF.HiLo() >> 5 & 1) == 1;
        }

        public void SetH(bool on)
        {
            setFlag(5, on);
        }

        // Get carry flag
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

        public byte PopPC8()
        {
            var opcode = Memory.Read(PC);
            PC += 1;
            return opcode;
        }

        public UInt16 PopPC16()
        {
            var b1 = (UInt16)PopPC8();
            var b2 = (UInt16)PopPC8();
            return ((ushort)(b2 << 8 | b1));
        }

        public int ExecuteNextOpcode()
        {
            var opcode = PopPC8();
            return Instructions.Execute(opcode);
        }

        public int getSpeed()
        {
            return currentSpeed + 1;
        }

        public void instXor(byte value1, byte value2)
        {
            var total = (byte)(value1 & value2);
            AF.SetHi(total);
            SetZ(total == 0);
            SetN(false);
            SetH(false);
            SetC(false);
        }
    }
}

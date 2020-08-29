using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    public class Cpu
    {
        public Register AF;
        public Register BC;
        public Register DE;
        public Register HL;

        public Register SP;
        public UInt16 PC; // Program Counter

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

    }
}

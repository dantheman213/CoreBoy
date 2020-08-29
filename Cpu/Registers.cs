using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    // CPU Register
    // A Register is responsible for holding on to a little piece of data that the overall CPU can manipulate as it executes instructions.
    public class Register
    {
        private UInt16 value;
        private UInt16 mask; // Only used for F

        public byte Hi()
        {
            return (byte)(value >> 8);
        }

        public byte Lo()
        {
            return (byte)(value & 0xFF);
        }

        public UInt16 HiLo()
        {
            return value;
        }

        public void SetHi(byte val)
        {
            value = (UInt16)(val << 8 | value & 0xFF);
            updateMask();
        }

        public void SetLo(byte val)
        {
            value = (UInt16)(val | value & 0xFF00);
            updateMask();
        }

        public void Set(UInt16 val)
        {
            value = val;
            updateMask();
        }

        public void SetMask(UInt16 val)
        {
            mask = val;
        }

        private void updateMask()
        {
            if (mask != 0) {
                value &= mask;
            }
        }
    }
}

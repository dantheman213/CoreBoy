using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    public class BitHelper
    {
        public static bool Test(byte value, byte bit)
        {
            return ((value >> bit) & 1) == 1;
        }

        public static byte Set(byte value, byte bit)
        {
            return (byte)(value | (1 << bit));
        }

        public static byte Val(byte value, byte bit)
        {
            return (byte)((value >> bit) & 1);
        }

        public static byte Reset(byte value, byte bit)
        {
            byte b = value;
            b &= b;
            b ^= (byte)(1 << bit);
            return b;
        }
    }
}

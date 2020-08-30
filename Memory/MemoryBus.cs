using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    public class MemoryBus
    {
        public static UInt16 DIV = 0xFF04;
        public static UInt16 TIMA = 0xFF05;
        public static UInt16 TMA = 0xFF06;
        public static UInt16 TAC = 0xFF07;

        public byte Read(UInt16 address)
        {
            if (address < 0x8000)
            {
                // Cartridge ROM
                // 0 - 7FFF
                // 32kb

                // TODO
                return 0;
            }
            else if (address < 0xA000)
            {
                // Video Ram (Banking)
                // 8000 - 9FFFF
                // 8kb

                // TODO
                return 0;
            }
            else if (address < 0xC000)
            {
                // External RAM (Cartridge RAM / contains game save data, etc)
                // A000 - BFFF
                // 8kb

                // TODO
                return 0;
            }
            else if (address < 0xD000)
            {
                // Internal Work RAM (Bank 0)
                // C000 - DFFF
                // 8kb
                // TODO
                return 0;
            }
            else if (address < 0xE000)
            {
                // Internal Work RAM (Bank 1-7)
                
                // TODO
                return 0;
            }
            else if(address < 0xFE00)
            {
                // Echo RAM
                // TODO?
                return 0xFF;
            }
            else if(address < 0xFEA0)
            {
                // Object Attribute Memory (OAM) / Sprite Attribute Table
                // FE00 - FE9F
                // 160 bytes
                // Each sprite takes up 4 bytes which leaves just enough room for exactly 40 sprites

                // TODO
                return 0;
            }
            else if (address < 0xFF00)
            {
                // Talk directly to screen controller, sound generator, buttons, link cable, and internal timers
                // Unusuable memory
                // 127 bytes
                return 0xFF;
            }
            else
            {
                // High RAM (in Gameboy hardware this RAM sits inside the CPU and is faster than Work RAM)
                // FF80 - FFFE
                // 127 bytes

                // TODO
                return 0;
            }

            // FFFF
            // Interrupt switch
            // Unusable memory
            // 1 byte
        }

        public void Write(UInt16 address, byte value)
        {
            if (address < 0x8000)
            {
                // Cartridge ROM
                // 0 - 7FFF
                // 32kb
                // TODO
            }
            else if (address < 0xA000)
            {
                // Video Ram (Banking)
                // TODO
            }
            else if (address < 0xC000)
            {
                // Cartridge ROM
                // TODO
            }
            else if (address < 0xD000)
            {
                // Internal RAM Banks (1-7)
            }
            else if (address < 0xFE00)
            {
                // Echo RAM
                // TODO?
            }
            else if (address < 0xFEA0)
            {
                // Object Attribute Memory (OAM) Table
                // TODO
            }
            else if (address < 0xFF00)
            {
                // Unusuable memory
            }
            else
            {
                // If get here assume reading from High Memory
                // TODO
            }
        }
    }
}

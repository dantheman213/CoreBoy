using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    // General Memory Map (extra details in 'Read' sub-routine below)
    //
    // 0000-3FFF     16KB ROM Bank 00     (in cartridge, fixed at bank 00)
    // 4000 - 7FFF   16KB ROM Bank 01..NN(in cartridge, switchable bank number)
    // 8000 - 9FFF   8KB Video RAM(VRAM)(switchable bank 0 - 1 in CGB Mode)
    // A000 - BFFF   8KB External RAM(in cartridge, switchable bank, if any)
    // C000 - CFFF   4KB Work RAM Bank 0(WRAM)
    // D000 - DFFF   4KB Work RAM Bank 1(WRAM)(switchable bank 1 - 7 in CGB Mode)
    // E000 - FDFF   Same as C000 - DDFF(ECHO)(typically not used)
    // FE00 - FE9F   Sprite Attribute Table(OAM)
    // FEA0 - FEFF   Not Usable
    // FF00 - FF7F   I / O Ports
    // FF80 - FFFE   High RAM(HRAM)
    // FFFF          Interrupt Enable Register
    public class MemoryBus
    {
        public static UInt16 DIV = 0xFF04;
        public static UInt16 TIMA = 0xFF05;
        public static UInt16 TMA = 0xFF06;
        public static UInt16 TAC = 0xFF07;

        private Cartridge cartridge;

        public MemoryBus(ref Cartridge c)
        {
            cartridge = c;
        }

        public byte Read(UInt16 address)
        {
            if (address < 0x8000)
            {
                // Cartridge ROM
                // 0 - 7FFF
                // 32kb

                return cartridge.Read(address);
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

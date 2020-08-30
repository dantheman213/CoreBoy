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
                // TODO
                return 0;
            }
            else if (address < 0xA000)
            {
                // VRAM Banking
                // TODO
                return 0;
            }
            else if (address < 0xC000)
            {
                // Cartridge ROM
                // TODO
                return 0;
            }
            else if (address < 0xD000)
            {
                // Internal RAM Banks (1-7)
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
                // Object Attribute Memory (OAM) Table
                // TODO
                return 0;
            }
            else if (address < 0xFF00)
            {
                // Unusuable memory
                return 0xFF;
            }
            else
            {
                // If get here assume reading from High Memory
                // TODO
                return 0;
            }
        }

        public void Write(UInt16 address, byte value)
        {
            if (address < 0x8000)
            {
                // Cartridge ROM (banking)
                // TODO
              
            }
            else if (address < 0xA000)
            {
                // VRAM Banking
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
                // TODO
              
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

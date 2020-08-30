using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    public class Cartridge
    {
        private byte[] rom;
        private string title;
        private int mode;

        public Cartridge(byte[] dat)
        {
            rom = dat;

            switch(rom[0x0143])
            {
                case 0x80:
                    mode = 0; // DMG
                    break;
                case 0xC0:
                    mode = 1; // CGB
                    break;
                default:
                    mode = 0; // DMG
                    break;
            }

            setTitleFromRomData();
            Console.WriteLine("Loaded ROM Title: {0}", title);
        }

        public byte Read(UInt16 address)
        {
            return rom[address];
        }

        private void setTitleFromRomData()
        {
            for (var i = 0x134; i < 0x142; i++)
            {
                title += (char)Read((UInt16)i);
            }
        }

        public byte[] getRom()
        {
            return rom;
        }

        public int getMode()
        {
            return mode;
        }

        public string getTitle()
        {
            return title;
        }
    }
}

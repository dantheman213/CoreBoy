using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private byte[] HighRam = new byte[0x100];

        private byte[] VideoRam = new byte[0x4000];
        private byte CurrentVideoRamBank = 1;

        private byte[] WorkRam = new byte[0x9000];
        private byte CurrentWorkRamBank = 1;

        private byte[] Oam = new byte[0x100];

        public int timerCounter;

        private byte hdmaLength;
        private bool hdmaActive;

        private Cartridge cartridge;
        private Cpu Cpu;

        public MemoryBus(ref Cartridge c)
        {
            cartridge = c;
            bootstrapHighRam();
        }

        public void SetCpu(ref Cpu c)
        {
            Cpu = c;
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

                return ReadHighRam(address);
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

        // Set the High RAM to expected values after Gameboy boot up screen
        private void bootstrapHighRam()
        {
            HighRam[0x04] = 0x1E;

            HighRam[0x05] = 0x00;

            HighRam[0x06] = 0x00;

            HighRam[0x07] = 0xF8;

            HighRam[0x0F] = 0xE1;

            HighRam[0x10] = 0x80;

            HighRam[0x11] = 0xBF;

            HighRam[0x12] = 0xF3;

            HighRam[0x14] = 0xBF;

            HighRam[0x16] = 0x3F;

            HighRam[0x17] = 0x00;

            HighRam[0x19] = 0xBF;

            HighRam[0x1A] = 0x7F;

            HighRam[0x1B] = 0xFF;

            HighRam[0x1C] = 0x9F;

            HighRam[0x1E] = 0xBF;

            HighRam[0x20] = 0xFF;

            HighRam[0x21] = 0x00;

            HighRam[0x22] = 0x00;

            HighRam[0x23] = 0xBF;

            HighRam[0x24] = 0x77;

            HighRam[0x25] = 0xF3;

            HighRam[0x26] = 0xF1;

            HighRam[0x40] = 0x91;

            HighRam[0x41] = 0x85;

            HighRam[0x42] = 0x00;

            HighRam[0x43] = 0x00;

            HighRam[0x45] = 0x00;

            HighRam[0x47] = 0xFC;

            HighRam[0x48] = 0xFF;

            HighRam[0x49] = 0xFF;

            HighRam[0x4A] = 0x00;

            HighRam[0x4B] = 0x00;

            HighRam[0xFF] = 0x00;
        }

        public byte ReadHighRam(UInt16 address)
        {
            if (address == 0xFF00)
            {
                // Joypad
                // TODO
            }
            else if (address >= 0xFF10 && address <= 0xFF26)
            {
                // Sound
                // TODO
            }
            else if (address >= 0xFF30 && address <= 0xFF3F)
            {
                // Waveform RAM
                // TODO
            }
            else if (address == 0xFF0F)
            {
                return (byte)(HighRam[0x0F] | 0xE0);
            }
            else if (address == 0xFF72 && address <= 0xFF77)
            {
                // TODO?
                return 0;
            }
            else if (address == 0xFF68)
            {
                // BG Palette index
                // TODO
            }
            else if (address == 0xFF69)
            {
                // BG Palette data
                // TODO
            }
            else if (address == 0xFF6A)
            {
                // Sprite palette index
                // TODO
            }
            else if (address == 0xFF6B)
            {
                // Sprite Palette data
                // TODO
            }
            else if(address == 0xFF4D)
            {
                return (byte)(Cpu.currentSpeed << 7 | BitHelper.BoolToByte(Cpu.prepareSpeed));
            }
            else if (address == 0xFF4F)
            {
                return CurrentVideoRamBank;
            }
            else if (address == 0xFF70)
            {
                return CurrentWorkRamBank;
            }
            else
            {
                return HighRam[address - 0xFF00];
            }

            return 0;
        }

        public void WriteHighRam(UInt16 address, byte value)
        {
            if (address >= 0xFEA0 && address <= 0xFF26)
            {
                // Restricted
                return;
            }
            else if (address >= 0xFF10 && address <= 0xFF26)
            {
                // TODO: Sound
            }
            else if (address >= 0xFF30 && address <= 0xFF3F)
            {
                // TODO: Sound
            }
            else if (address == 0xFF02)
            {
                // TODO: Serial?
            }
            else if (address == DIV)
            {
                // Trap divider register
                SetClockFrequency();
                Cpu.Divider = 0;
                HighRam[DIV - 0xFF00] = 0;
            }
            else if (address == TIMA)
            {
                HighRam[TIMA - 0xFF00] = value;
            }
            else if (address == TMA)
            {
                HighRam[TMA - 0xFF00] = value;
            }
            else if (address == TAC)
            {
                // Timer control
                var currentFrequency = GetClockFrequency();
                HighRam[TAC - 0xFF00] = (byte)(value | 0xF8);
                var newFrequency = GetClockFrequency();

                if (currentFrequency != newFrequency)
                {
                    SetClockFrequency();
                }
            }
            else if (address == 0xFF41)
            {
                HighRam[0x41] = (byte)(value | 0x80);
            }
            else if (address == 0xFF44)
            {
                // Scanline
                HighRam[0x44] = 0;
            }
            else if (address == 0xFF46)
            {
                // DMA transfer
                // TODO
            }
            else if (address == 0xFF4D)
            {
                if (cartridge.getMode() == GameBoyTypeEnum.GAMEBOY_COLOR)
                {
                    Cpu.prepareSpeed = BitHelper.Test(value, 0);
                }
            }
            else if (address == 0xFF4F)
            {
                // Video RAM (Gameboy Color only), blocked when HDMA is active
                if (cartridge.getMode() == GameBoyTypeEnum.GAMEBOY_COLOR && !hdmaActive)
                {
                    CurrentVideoRamBank = (byte)(value & 0x1);
                }
            }
            else if (address == 0xFF55)
            {
                // Gameboy Color DMA transfer
                if (cartridge.getMode() == GameBoyTypeEnum.GAMEBOY_COLOR)
                {
                    // TODO
                }
            }
            else if (address == 0xFF68)
            {
                // BG Palette Index
                if (cartridge.getMode() == GameBoyTypeEnum.GAMEBOY_COLOR)
                {
                    // TODO
                }
            }
            else if (address == 0xFF69)
            {
                // TODO
            }
            else if (address == 0xFF6A)
            {
                // Sprite palette index
                // TODO
            }
            else if (address == 0xFF6B)
            {
                // TODO
            }
            else if (address == 0xFF70)
            {
                if (cartridge.getMode() == GameBoyTypeEnum.GAMEBOY_COLOR)
                {
                    CurrentWorkRamBank = (byte)(value & 0x7);
                    if (CurrentWorkRamBank == 0)
                    {
                        CurrentWorkRamBank = 1;
                    }
                }
            }
            else if (address >= 0xFF72 && address <= 0xFF77)
            {
                // TODO
            }
            else
            {
                HighRam[address - 0xFF00] = value;
            }
        }


        public void SetClockFrequency()
        {
            timerCounter = 0;
        }

        public byte GetClockFrequency()
        {
            return HighRam[0x07];
        }

        public int GetClockFrequencyCount()
        {
            switch (GetClockFrequency())
            {
                case 0:
                    return 1024;
                case 1:
                    return 16;
                case 2:
                    return 64;
                default:
                    return 256;
            }
        }
    }
}

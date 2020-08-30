using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    // Gameboy hardware specifications:
    // CPU          - 8-bit (Similar to the Z80 processor)
    // Clock Speed  - 4.194304MHz(4.295454MHz for SGB, max. 8.4MHz for CGB)
    // Work RAM     - 8K Byte(32K Byte for CGB)
    // Video RAM    - 8K Byte(16K Byte for CGB)
    // Screen Size  - 2.6"
    // Resolution   - 160x144(20x18 tiles)
    // Max sprites  - Max 40 per screen, 10 per line
    // Sprite sizes - 8x8 or 8x16
    // Palettes     - 1x4 BG, 2x3 OBJ(for CGB: 8x4 BG, 8x3 OBJ)
    // Colors       - 4 grayshades(32768 colors for CGB)
    // Horiz Sync   - 9198 KHz(9420 KHz for SGB)
    // Vert Sync    - 59.73 Hz(61.17 Hz for SGB)
    // Sound        - 4 channels with stereo sound
    // Power        - DC6V 0.7W(DC3V 0.7W for GB Pocket, DC3V 0.6W for CGB)

    public class Gameboy
    {
        // The number of cycles the Gameboy CPU is capable of processing each second.
        public const int ClockSpeed = 4194304;

        // Frames per second for the display
        public const int FramesPerSecond = 60;

        // The number of CPU cycles in each frame
        public const int CyclesPerFrame = ClockSpeed / FramesPerSecond;

        public Cartridge Cartridge;
        public MemoryBus Bus;
        public Cpu Cpu;
        public Ppu Ppu;

        // TODO: Sound

        private bool isPaused;

        private int timerCounter;

        public Gameboy(byte[] rom)
        {
            Cartridge = new Cartridge(rom);
            Bus = new MemoryBus(Cartridge);
            Cpu = new Cpu(Bus);
            Ppu = new Ppu();

            // TODO: APU / sound

        }

        public void Loop()
        {
            // TODO
            while(true)
            {
                // TODO: Calculate FPS?
                // TODO: Get button state?
                // TODO: Process button input states?

                Update();
                // TODO: Render display data to native screen

            }
        }

        // Update the state of the Gameboy by a single frame
        private int Update()
        {
            if (isPaused)
            {
                return 0;
            }

            var cycles = 0;
            while(cycles < CyclesPerFrame * Cpu.getSpeed())
            {
                var cyclesOp = 4;
                // TODO: halted?

                cyclesOp = Cpu.ExecuteNextOpcode();

                cycles += cyclesOp;
                
                // TODO: Update GFX
                // TODO: Update Timers

                // TODO: Interrupts?
                // TODO: Sound?

            }

            return cycles;
        }
    }
}

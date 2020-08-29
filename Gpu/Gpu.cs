using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    public class Gpu
    {
        public const int ScreenWidth = 160;
        public const int ScreenHeight = 144;

        public const UInt16 LCDC = 0xFF40;

        private const int lcdMode2Bounds = 456 - 80;
        private const int lcdMode3Bounds = lcdMode2Bounds - 172;

        // Matrix of pixel data which is used while the screen is rendering. When a
        // frame has been completed, this data is copied into the PreparedData matrix
        private byte[,,] screenData;
        private byte[,,] PreparedData;
        private bool[,] bgPriority;

        public Gpu()
        {
            screenData = new byte[Gpu.ScreenWidth, Gpu.ScreenHeight, 3];
            screenData = new byte[Gpu.ScreenWidth, Gpu.ScreenHeight, 3];
            bgPriority = new bool[Gpu.ScreenWidth, Gpu.ScreenHeight];
        }
    }
}

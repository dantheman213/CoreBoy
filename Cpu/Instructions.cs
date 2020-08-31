using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CoreBoy
{
    public class Instructions
    {
        private int[] OpcodeCycles = new int[] {
            1, 3, 2, 2, 1, 1, 2, 1, 5, 2, 2, 2, 1, 1, 2, 1, // 0
	        0, 3, 2, 2, 1, 1, 2, 1, 3, 2, 2, 2, 1, 1, 2, 1, // 1
	        2, 3, 2, 2, 1, 1, 2, 1, 2, 2, 2, 2, 1, 1, 2, 1, // 2
	        2, 3, 2, 2, 3, 3, 3, 1, 2, 2, 2, 2, 1, 1, 2, 1, // 3
	        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, // 4
	        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, // 5
	        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, // 6
	        2, 2, 2, 2, 2, 2, 0, 2, 1, 1, 1, 1, 1, 1, 2, 1, // 7
	        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, // 8
	        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, // 9
	        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, // a
	        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, // b
	        2, 3, 3, 4, 3, 4, 2, 4, 2, 4, 3, 0, 3, 6, 2, 4, // c
	        2, 3, 3, 0, 3, 4, 2, 4, 2, 4, 3, 0, 3, 0, 2, 4, // d
	        3, 3, 2, 0, 0, 4, 2, 4, 4, 1, 4, 0, 0, 0, 2, 4, // e
	        3, 3, 2, 1, 0, 4, 2, 4, 3, 2, 4, 1, 0, 0, 2, 4, // f
        }; //0  1  2  3  4  5  6  7  8  9  a  b  c  d  e  f

        // CBOpcodeCycles is the number of cpu cycles for each CB opcode.
        private int[] CBOpcodeCycles = new int[] {
            2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // 0
	        2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // 1
	        2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // 2
	        2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // 3
	        2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3, 2, // 4
	        2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3, 2, // 5
	        2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3, 2, // 6
	        2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 2, 2, 2, 2, 3, 2, // 7
	        2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // 8
	        2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // 9
	        2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // A
	        2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // B
	        2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // C
	        2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // D
	        2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // E
	        2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, // F
        }; //0  1  2  3  4  5  6  7  8  9  a  b  c  d  e  f

		private Dictionary<UInt16, string> instructionNameSet = new Dictionary<UInt16, string>() {
			{ 0x06, "LD B,n" },
			{ 0x0E, "LD C,n" },
			{ 0x16, "LD D,n" },
			{ 0x1E, "LD E,n" },
			{ 0x26, "LD H,n" },
			{ 0x2E, "LD L,n" },
			{ 0x7F, "LD A,A" },
			{ 0x78, "LD A,B" },
			{ 0x79, "LD A,C" },
			{ 0x7A, "LD A,D" },
			{ 0x7B, "LD A,E" },
			{ 0x7C, "LD A,H" },
			{ 0x7D, "LD A,L" },
			{ 0x0A, "LD A,(BC)" },
			{ 0x1A, "LD A,(DE)" },
			{ 0x7E, "LD A,(HL)" },
			{ 0xFA, "LD A,(nn)" },
			{ 0x3E, "LD A,(nn)" },
			{ 0x47, "LD B,A" },
			{ 0x40, "LD B,B" },
			{ 0x41, "LD B,C" },
			{ 0x42, "LD B,D" },
			{ 0x43, "LD B,E" },
			{ 0x44, "LD B,H" },
			{ 0x45, "LD B,L" },
			{ 0x46, "LD B,(HL)" },
			{ 0x4F, "LD C,A" },
			{ 0x48, "LD C,B" },
			{ 0x49, "LD C,C" },
			{ 0x4A, "LD C,D" },
			{ 0x4B, "LD C,E" },
			{ 0x4C, "LD C,H" },
			{ 0x4D, "LD C,L" },
			{ 0x4E, "LD C,(HL)" },
			{ 0x57, "LD D,A" },
			{ 0x50, "LD D,B" },
			{ 0x51, "LD D,C" },
			{ 0x52, "LD D,D" },
			{ 0x53, "LD D,E" },
			{ 0x54, "LD D,H" },
			{ 0x55, "LD D,L" },
			{ 0x56, "LD D,(HL)" },
			{ 0x5F, "LD E,A" },
			{ 0x58, "LD E,B" },
			{ 0x59, "LD E,C" },
			{ 0x5A, "LD E,D" },
			{ 0x5B, "LD E,E" },
			{ 0x5C, "LD E,H" },
			{ 0x5D, "LD E,L" },
			{ 0x5E, "LD E,(HL)" },
			{ 0x67, "LD H,A" },
			{ 0x60, "LD H,B" },
			{ 0x61, "LD H,C" },
			{ 0x62, "LD H,D" },
			{ 0x63, "LD H,E" },
			{ 0x64, "LD H,H" },
			{ 0x65, "LD H,L" },
			{ 0x66, "LD H,(HL)" },
			{ 0x6F, "LD L,A" },
			{ 0x68, "LD L,B" },
			{ 0x69, "LD L,C" },
			{ 0x6A, "LD L,D" },
			{ 0x6B, "LD L,E" },
			{ 0x6C, "LD L,H" },
			{ 0x6D, "LD L,L" },
			{ 0x6E, "LD L,(HL)" },
			{ 0x77, "LD (HL),A" },
			{ 0x70, "LD (HL),B" },
			{ 0x71, "LD (HL),C" },
			{ 0x72, "LD (HL),D" },
			{ 0x73, "LD (HL),E" },
			{ 0x74, "LD (HL),H" },
			{ 0x75, "LD (HL),L" },
			{ 0x36, "LD (HL),n 36" },
			{ 0x02, "LD (BC),A" },
			{ 0x12, "LD (DE),A" },
			{ 0xEA, "LD (nn),A" },
			{ 0xF2, "LD A,(C)" },
			{ 0xE2, "LD (C),A" },
			{ 0x3A, "LDD A,(HL)" },
			{ 0x32, "LDD (HL),A" },
			{ 0x2A, "LDI A,(HL)" },
			{ 0x22, "LDI (HL),A" },
			{ 0xE0, "LD (0xFF00+n),A" },
			{ 0xF0, "LD A,(0xFF00+n)" },
			{ 0x01, "LD BC,nn" },
			{ 0x11, "LD DE,nn" },
			{ 0x21, "LD HL,nn" },
			{ 0x31, "LD SP,nn" },
			{ 0xF9, "LD SP,HL" },
			{ 0xF8, "LD HL,SP+n" },
			{ 0x08, "LD (nn),SP" },
			{ 0xF5, "PUSH AF" },
			{ 0xC5, "PUSH BC" },
			{ 0xD5, "PUSH DE" },
			{ 0xE5, "PUSH HL" },
			{ 0xF1, "POP AF" },
			{ 0xC1, "POP BC" },
			{ 0xD1, "POP DE" },
			{ 0xE1, "POP HL" },
			{ 0x87, "ADD A,A" },
			{ 0x80, "ADD A,B" },
			{ 0x81, "ADD A,C" },
			{ 0x82, "ADD A,D" },
			{ 0x83, "ADD A,E" },
			{ 0x84, "ADD A,H" },
			{ 0x85, "ADD A,L" },
			{ 0x86, "ADD A,(HL)" },
			{ 0xC6, "ADD A,#" },
			{ 0x8F, "ADC A,A" },
			{ 0x88, "ADC A,B" },
			{ 0x89, "ADC A,C" },
			{ 0x8A, "ADC A,D" },
			{ 0x8B, "ADC A,E" },
			{ 0x8C, "ADC A,H" },
			{ 0x8D, "ADC A,L" },
			{ 0x8E, "ADC A,(HL)" },
			{ 0xCE, "ADC A,#" },
			{ 0x97, "SUB A,A" },
			{ 0x90, "SUB A,B" },
			{ 0x91, "SUB A,C" },
			{ 0x92, "SUB A,D" },
			{ 0x93, "SUB A,E" },
			{ 0x94, "SUB A,H" },
			{ 0x95, "SUB A,L" },
			{ 0x96, "SUB A,(HL)" },
			{ 0xD6, "SUB A,#" },
			{ 0x9F, "SBC A,A" },
			{ 0x98, "SBC A,B" },
			{ 0x99, "SBC A,C" },
			{ 0x9A, "SBC A,D" },
			{ 0x9B, "SBC A,E" },
			{ 0x9C, "SBC A,H" },
			{ 0x9D, "SBC A,L" },
			{ 0x9E, "SBC A,(HL)" },
			{ 0xDE, "SBC A,#" },
			{ 0xA7, "AND A,A" },
			{ 0xA0, "AND A,B" },
			{ 0xA1, "AND A,C" },
			{ 0xA2, "AND A,D" },
			{ 0xA3, "AND A,E" },
			{ 0xA4, "AND A,H" },
			{ 0xA5, "AND A,L" },
			{ 0xA6, "AND A,(HL)" },
			{ 0xE6, "AND A,#" },
			{ 0xB7, "OR A,A" },
			{ 0xB0, "OR A,B" },
			{ 0xB1, "OR A,C" },
			{ 0xB2, "OR A,D" },
			{ 0xB3, "OR A,E" },
			{ 0xB4, "OR A,H" },
			{ 0xB5, "OR A,L" },
			{ 0xB6, "OR A,(HL)" },
			{ 0xF6, "OR A,#" },
			{ 0xAF, "XOR A,A" },
			{ 0xA8, "XOR A,B" },
			{ 0xA9, "XOR A,C" },
			{ 0xAA, "XOR A,D" },
			{ 0xAB, "XOR A,E" },
			{ 0xAC, "XOR A,H" },
			{ 0xAD, "XOR A,L" },
			{ 0xAE, "XOR A,(HL)" },
			{ 0xEE, "XOR A,#" },
			{ 0xBF, "CP A,A" },
			{ 0xB8, "CP A,B" },
			{ 0xB9, "CP A,C" },
			{ 0xBA, "CP A,D" },
			{ 0xBB, "CP A,E" },
			{ 0xBC, "CP A,H" },
			{ 0xBD, "CP A,L" },
			{ 0xBE, "CP A,(HL)" },
			{ 0xFE, "CP A,#" },
			{ 0x3C, "INC A" },
			{ 0x04, "INC B" },
			{ 0x0C, "INC C" },
			{ 0x14, "INC D" },
			{ 0x1C, "INC E" },
			{ 0x24, "INC H" },
			{ 0x2C, "INC L" },
			{ 0x34, "INC (HL)" },
			{ 0x3D, "DEC A" },
			{ 0x05, "DEC B" },
			{ 0x0D, "DEC C" },
			{ 0x15, "DEC D" },
			{ 0x1D, "DEC E" },
			{ 0x25, "DEC H" },
			{ 0x2D, "DEC L" },
			{ 0x35, "DEC (HL)" },
			{ 0x09, "ADD HL,BC" },
			{ 0x19, "ADD HL,DE" },
			{ 0x29, "ADD HL,HL" },
			{ 0x39, "ADD HL,SP" },
			{ 0xE8, "ADD SP,n" },
			{ 0x03, "INC BC" },
			{ 0x13, "INC DE" },
			{ 0x23, "INC HL" },
			{ 0x33, "INC SP" },
			{ 0x0B, "DEC BC" },
			{ 0x1B, "DEC DE" },
			{ 0x2B, "DEC HL" },
			{ 0x3B, "DEC SP" },
			{ 0x27, "DAA" },
			{ 0x2F, "CPL" },
			{ 0x3F, "CCF" },
			{ 0x37, "SCF" },
			{ 0x00, "NOP" },
			{ 0x76, "HALT" },
			{ 0x10, "STOP" },
			{ 0xF3, "DI" },
			{ 0xFB, "EI" },
			{ 0x07, "RLCA" },
			{ 0x17, "RLA" },
			{ 0x0F, "RRCA" },
			{ 0x1F, "RRA" },
			{ 0xC3, "JP nn" },
			{ 0xC2, "JP NZ,nn" },
			{ 0xCA, "JP Z,nn" },
			{ 0xD2, "JP NC,nn" },
			{ 0xDA, "JP C,nn" },
			{ 0xE9, "JP HL" },
			{ 0x18, "JR n" },
			{ 0x20, "JR NZ,n" },
			{ 0x28, "JR Z,n" },
			{ 0x30, "JR NC,n" },
			{ 0x38, "JR C,n" },
			{ 0xCD, "CALL nn" },
			{ 0xC4, "CALL NZ,nn" },
			{ 0xCC, "CALL Z,nn" },
			{ 0xD4, "CALL NC,nn" },
			{ 0xDC, "CALL C,nn" },
			{ 0xC7, "RST 0x00" },
			{ 0xCF, "RST 0x08" },
			{ 0xD7, "RST 0x10" },
			{ 0xDF, "RST 0x18" },
			{ 0xE7, "RST 0x20" },
			{ 0xEF, "RST 0x28" },
			{ 0xF7, "RST 0x30" },
			{ 0xFF, "RST 0x38" },
			{ 0xC9, "RET" },
			{ 0xC0, "RET NZ" },
			{ 0xC8, "RET Z" },
			{ 0xD0, "RET NC" },
			{ 0xD8, "RET C" },
			{ 0xD9, "RETI" },
			{ 0xCB, "CB!" },
		};

		private Cpu Cpu;
		private MemoryBus Memory;
        public Instructions(ref Cpu c, ref MemoryBus m)
        {
            Cpu = c;
			Memory = m;
        }

        public int Execute(byte opcode)
        {
			if (opcode == 0x00)
            {
				// NOP
            }
			else if (opcode == 0x06)
            {
				// LD B,n
				Cpu.BC.SetHi(Cpu.PopPC8());
            }
			else if (opcode == 0x0E)
            {
				// LD C,n
				Cpu.BC.SetLo(Cpu.PopPC8());
            }
			else if (opcode == 0x20)
            {
				// JR NZ,n
				var next = (sbyte)Cpu.PopPC8();
				if (Cpu.Z())
                {
					var addr = (Int32)Cpu.PC + (Int32)next;
					Cpu.instJump((UInt16)addr);
                }
            }
			else if (opcode == 0x21)
            {
				// LD HL,nn
				var val = Cpu.PopPC16();
				Cpu.HL.Set(val);
            }
			else if (opcode == 0x31)
            {
				// LD SP,nn
				var val = Cpu.PopPC16();
				Cpu.SP.Set(val);
            }
			else if (opcode == 0x32)
            {
				// LD (HL),A
				var val = Cpu.HL.HiLo();
				Memory.Write(val, Cpu.AF.Hi());
				Cpu.HL.Set((UInt16)(Cpu.HL.HiLo() - 1));
            }
			else if (opcode == 0x3E)
            {
				// LD A,(nn)
				var val = Cpu.PopPC8();
				Cpu.AF.SetHi(val);
			}
			else if (opcode == 0xAF)
            {
				// XOR A,A
				Cpu.instXor(Cpu.AF.Hi(), Cpu.AF.Hi());
            }
			else if (opcode == 0xC3)
			{
				// JMP nn
				jump(Cpu.PopPC16());
			}
			else if (opcode == 0xE0)
			{
				var val = (UInt16)(0xFF00 + Cpu.PopPC8());
				Memory.Write(val, Cpu.AF.Hi());
			}
			else if (opcode == 0xEA)
            {
				// LD (nn),A
				var val = Cpu.AF.Hi();
				Memory.Write(Cpu.PopPC16(), val);
			}
			else if (opcode == 0xF0)
            {
				// LD A,(0xFF00+n)
				var val = Memory.ReadHighRam((UInt16)(0xFF00 + (UInt16)Cpu.PopPC8()));
				Cpu.AF.SetHi(val);
            }
			else if (opcode == 0xF3)
            {
				Cpu.InterruptsOn = false;
            }
			else if (opcode == 0xFE)
            {
				// CP A,#
				Cpu.instCp(Cpu.PopPC8(), Cpu.AF.Hi());
            }
			else
            {
				Console.WriteLine("UNKNOWN OPCODE: 0x{0} - {1}", opcode.ToString("X2"), instructionNameSet[opcode]);
				throw new Exception();
			}
			
            return OpcodeCycles[opcode] * 4;
        }

        public void jump(UInt16 address)
        {
            Cpu.PC = address;
        }
    }
}

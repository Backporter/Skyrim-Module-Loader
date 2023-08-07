using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyrim_Module_Loader
{
    class Program
    {
        static Int32 GetTargetVersion()
        {
            Console.WriteLine("Waht is the Patch you want to target?");
            Console.WriteLine("00: 1.00");
            Console.WriteLine("01: 1.01");
            Console.WriteLine("02: 1.02");
            Console.WriteLine("03: 1.03");
            Console.WriteLine("04: 1.04");
            Console.WriteLine("05: 1.05");
            Console.WriteLine("06: 1.06");
            Console.WriteLine("07: 1.07");
            Console.WriteLine("08: 1.08");
            Console.WriteLine("09: 1.09");
            Console.WriteLine("10: 1.10");
            Console.WriteLine("11: 1.11");
            Console.WriteLine("12: 1.12");
            Console.WriteLine("13: 1.13");
            Console.WriteLine("14: 1.14");
            Console.WriteLine("15: 1.15");
            Console.WriteLine("16: 1.16");
            Console.WriteLine("17: 1.17");
            Console.WriteLine("18: 1.18");
            Console.WriteLine("19: 1.19");
            Console.WriteLine("20: 1.20");
            Console.WriteLine("21: 1.21");
            Console.WriteLine("22: 1.22");
            Console.WriteLine("23: 1.23");
            Console.WriteLine("24: 1.24");
            Console.WriteLine("25: 1.25");
            Console.WriteLine("26: 1.26");

            int v = Int32.Parse(Console.ReadLine());
            switch (v)
            {
                case 00:
                    return 0;
                case 01:
                    return 1;
                case 02:
                    return 2;
                case 03:
                    return 3;
                case 04:
                    return 4;
                case 05:
                    return 5;
                case 06:
                    return 6;
                case 07:
                    return 7;
                case 08:
                    return 8;
                case 09:
                    return 9;
                case 10:
                    return 10;
                case 11:
                    return 11;
                case 12:
                    return 12;
                case 13:
                    return 13;
                case 14:
                    return 14;
                case 15:
                    return 15;
                case 16:
                    return 16;
                case 17:
                    return 17;
                case 18:
                    return 18;
                case 19:
                    return 19;
                case 20:
                    return 20;
                case 21:
                    return 21;
                case 22:
                    return 22;
                case 23:
                    return 23;
                case 24:
                    return 24;
                case 25:
                    return 25;
                case 26:
                    return 26;
                default:
                    return -1;
            }
        }

        static Int32 ProcessDifference(int targetVersion)
        {
            Console.WriteLine("What Firmware Will this binary be ran on?");
            Console.WriteLine("-1: User Specified(Hex)");
            Console.WriteLine("0: 1.76");
            Console.WriteLine("1: 3.55");
            Console.WriteLine("2: 4.05");
            Console.WriteLine("3: 4.55");
            Console.WriteLine("4: 5.05");
            Console.WriteLine("5: 6.72");
            Console.WriteLine("6: 7.02");
            Console.WriteLine("7: 7.55");
            Console.WriteLine("8: 9.00");

            // 1.00 -> 1.19, after 1.19 the function used for the patch got removed, therefore we need to use a different function.
            if (targetVersion <= 19)
            {
                // sceKernelGetModuleList: 

                Int32 value = Int32.Parse(Console.ReadLine());
                switch (value)
                {
                    // Unkown, passed to the app via the user
                    case -1:
                        return Int32.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
                    // 1.76 [unused]
                    case 0:
                        return 0;
                    // 3.55 [unused]
                    case 1:
                        return 0;
                    // 4.05
                    case 2:
                        return 0xA10;
                    // 4.55
                    case 3:
                        return 0x9D0;
                    // 5.05
                    case 4:
                        return 0xB70;
                    // 6.72/7.02
                    case 5:
                    case 6:
                        return 0xBB0;
                    // 7.55
                    case 7:
                        return 0xC00;
                    // 9.00
                    case 8:
                        return 0xCB0;
                    // ?.?? [unused]
                    default:
                        return 0;
                }
            }
            else
            {
                // 
                Int32 value = Int32.Parse(Console.ReadLine());
                switch (value)
                {
                    // Unkown, passed to the app via the user
                    case -1:
                        return Int32.Parse(Console.ReadLine(), System.Globalization.NumberStyles.HexNumber);
                    // 1.76 [unused]
                    case 0:
                        return 0;
                    // 3.55 [unused]
                    case 1:
                        return 0;
                    // 4.05
                    case 2:
                        return 0x4B60;
                    // 4.55
                    case 3:
                        return 0x4830;
                    // 5.05
                    case 4:
                        return 0x4CC0;
                    // 6.72
                    case 5:
                        return 0x5140;
                    // 7.02
                    case 6:
                        return 0x51A0;
                    // 7.55
                    case 7:
                        return 0x5270;
                    // 9.00
                    case 8:
                        return 0x51E0;
                    // ?.?? [unused]
                    default:
                        return 0;
                }
            }
        }

        static void PatchBinary(int targetVersion, BinaryWriter bw, byte[] dispbytes)
        {
            switch (targetVersion)
            {
                case 00:
                    Patches.PatchBinary100(bw, dispbytes);
                    break;
                case 01:
                    throw new NotImplementedException("Target Version 1.01 not Implemented");
                case 02:
                    throw new NotImplementedException("Target Version 1.02 not Implemented");
                case 03:
                    throw new NotImplementedException("Target Version 1.03 not Implemented");
                case 04:
                    Patches.PatchBinary104(bw, dispbytes);
                    break;
                case 05:
                    Patches.PatchBinary105(bw, dispbytes);
                    break;
                case 06:
                    Patches.PatchBinary106(bw, dispbytes);
                    break;
                case 07:
                    throw new NotImplementedException("Target Version 1.07 not Implemented");
                case 08:
                    Patches.PatchBinary108(bw, dispbytes);
                    break;
                case 09:
                    Patches.PatchBinary109(bw, dispbytes);
                    break;
                case 10:
                    Patches.PatchBinary110(bw, dispbytes);
                    break;
                case 11:
                    Patches.PatchBinary111(bw, dispbytes);
                    break;
                case 12:
                    Patches.PatchBinary112(bw, dispbytes);
                    break;
                case 13:
                    Patches.PatchBinary113(bw, dispbytes);
                    break;
                case 14:
                    Patches.PatchBinary114(bw, dispbytes);
                    break;
                case 15:
                    throw new NotImplementedException("Target Version 1.15 not Implemented");
                case 16:
                    Patches.PatchBinary116_117_119(bw, dispbytes);
                    break;
                case 17:
                    Patches.PatchBinary116_117_119(bw, dispbytes);
                    break;
                case 18:
                    throw new NotImplementedException("Target Version 1.18 not Implemented");
                case 19:
                    Patches.PatchBinary116_117_119(bw, dispbytes);
                    break;
                case 20:
                    Patches.PatchBinary120(bw, dispbytes);
                    break;
                case 21:
                    Patches.PatchBinary121(bw, dispbytes);
                    break;
                case 22:
                    Patches.PatchBinary122(bw, dispbytes);
                    break;
                case 23:
                    throw new NotImplementedException("Target Version 1.23 not Implemented");
                case 24:
                    throw new NotImplementedException("Target Version 1.24 not Implemented");
                case 25:
                    Patches.PatchBinary125(bw, dispbytes);
                    break;
                case 26:
                    Patches.PatchBinary126(bw, dispbytes);
                    break;
                default:
                    break;
            }
        }

        static void Main(string[] args)
        {
#if DEBUG
            for (int i = 0; i < args.Count(); i++)
            {
                Console.WriteLine($"Argument {i}: {args[i]}");
            }
#endif

#if !DEBUG
            if (args.Count() != 1)
            {
                Console.WriteLine("invalid arguments, first argument must be the path to the eboot.");
                Console.ReadLine();
            }
#endif
            Console.WriteLine("Welcome to Skyrim Module Loader Patcher");
            var targertVersion = GetTargetVersion();
            var disp = ProcessDifference(targertVersion);
            var dispbytes = BitConverter.GetBytes(disp);

            //
            using (BinaryReader br = new BinaryReader(File.Open(args[0], FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)))
            {
#if !DEBUG
                var header = br.ReadUInt32();
                if (header != 0x464C457F)
                {
                    Console.WriteLine($"Invaild File Header [expected 0x464C457F, got 0x{header}");
                    Console.ReadKey();
                }
#endif
                using (BinaryWriter bw = new BinaryWriter(File.Open(args[0], FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)))
                {
                    PatchBinary(targertVersion, bw, dispbytes);
                }
            }

            Console.WriteLine($"Finished!, patched [{args[0]}]\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}

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
        static Int32 ProcessDifference(int a_sys)
        {
            switch (a_sys)
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

        static void Main(string[] args)
        {
#if DEBUG
            foreach (string arg in args)
                Console.WriteLine(arg);
#endif

#if !DEBUG
            if (args.Count() != 1)
            {
                Console.WriteLine("invalid arguments, first argument must be the path to the eboot.");
                Console.ReadLine();
            }
#endif
            Console.WriteLine("Welcome to Skyrim Module Loader Patcher\nNote: 1.09 only as of [5/8/2023]\nWhat Firmware?\n-1:Custom\n0:1.76\n1:3.55\n2:4.05\n3:4.55\n4:5.05\n5:6.72\n6:7.02\n7:7.55\n8:9.00");
            var disp = ProcessDifference(int.Parse(Console.ReadLine()));
            var dispbytes = BitConverter.GetBytes(disp);

            //
            using (BinaryReader br = new BinaryReader(File.Open(args[0], FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)))
            {
#if !DEBUG
                var header = br.ReadUInt32();
                if (header != 0x464C457F)
                {
                    Console.WriteLine($"Invaild File Header [expected 0x464C457F, got 0x{header}");
                    Console.ReadLine();
                }
#endif
                using (BinaryWriter bw = new BinaryWriter(File.Open(args[0], FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)))
                {
                    byte[] _asm1 = new byte[]
                    {
                        // lea rdi, [aApp0]
                        0x48, 0x8D, 0x3D, 0x70, 0xFA, 0x93, 0x00,

                        // call sub_F8C970
                        0xE8, 0xD4, 0xBD, 0x14, 0x00,

                        // call sub_F8C990
                        0xE8, 0xEF, 0xBD, 0x14, 0x00,

                        // ret
                        0xC3
                    };

                    byte[] _asm2 = new byte[]
                    {
                        // push    rbp
                        0x55,

                        // mov     rbp, rsp
                        0x48, 0x89, 0xE5,

                        // mov     rsi, 0
                        0x48, 0xC7, 0xC6, 0x00, 0x00, 0x00, 0x00,

                        // mov     rdx, 0
                        0x48, 0xC7, 0xC2, 0x00, 0x00, 0x00, 0x00,

                        // mov     ecx, 0
                        0xB9, 0x00, 0x00, 0x00, 0x00,

                        // mov     r8, 0
                        0x49, 0xC7, 0xC0, 0x00, 0x00, 0x00, 0x00,

                        // mov     r9, 0
                        0x49, 0xC7, 0xC1, 0x00, 0x00, 0x00, 0x00,

                        // lea     rax, _PGsceKernelGetModuleList
                        0x48, 0x8D, 0x05, 0x42, 0x2A, 0x03, 0x01,

                        // mov     rax, [rax]
                        0x48, 0x8B, 0x00,

                        // sub     rax, XXXX
                        0x48, 0x2D, dispbytes[0], dispbytes[1], dispbytes[2], dispbytes[3],
                        
                        // call    rax
                        0xFF, 0xD0,
                        
                        // pop     rbp
                        0x5D,
                        
                        // retn
                        0xC3,
                    };

                    // [unused]
                    byte[] _asm3 = new byte[]
                    {
                        // mov     edi, 6
                        0xBF, 0x06, 0x00, 0x00, 0x00,

                        // call    sceKernelSleep
                        0xE8, 0x53, 0x5B, 0x8D, 0x00,

                        // retn
                        0xC3,
                    };

                    byte[] _asm4 = new byte[]
                    {
                        // lea rdi, qword ptr [a11tesidleform]
                        0x48, 0x8D, 0x3D, 0xF3, 0x0A, 0x0A, 0x01,

                        // call    sub_E40BA2
                        0xE8, 0xEA, 0xF4, 0x79, 0x00,

                        // call    sub_E40B90
                        0xE8, 0xD3, 0xF4, 0x79, 0x00,
                    };


                    byte[] _path = new byte[]    
                    {
                        // /app0/prx.prx
                        0x2F, 0x61, 0x70, 0x70, 0x30, 0x2F, 0x70, 0x72, 0x78, 0x2E, 0x70, 0x72, 0x78,
                    };

                    //
                    bw.Seek(0xE40B90 + 0x4000, SeekOrigin.Begin);

                    //
                    for (int i = 0; i < 0xA0; i++)
                    {
                        bw.Write((byte)0x90);
                    }

                    //
                    bw.Seek(0xE40B90 + 0x4000, SeekOrigin.Begin);
                    bw.Write(_asm1);
                    bw.Write(_asm2);
                    bw.Write(_asm3);

                    //
                    bw.Seek(0x6A16A6 + 0x4000, SeekOrigin.Begin);

                    for (int i = 0; i < 0x17; i++)
                    {
                        bw.Write((byte)0x90);
                    }

                    bw.Seek(0x6A16A6 + 0x4000, SeekOrigin.Begin);

                    for (int i = 0; i < 6; i++)
                    {
                        bw.Write((byte)0x90);
                    }

                    bw.Write(_asm4);
                    

                    //
                    bw.Seek(0x17421A6 + 0x4000, SeekOrigin.Begin);
                    bw.Write(_path);

                }
            }

            Console.WriteLine($"Finished!, patched [{args[0]}]\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}

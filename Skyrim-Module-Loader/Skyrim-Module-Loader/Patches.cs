using System;
using System.IO;
using System.Diagnostics;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyrim_Module_Loader
{
    class Patches
    {
        // 0xDEADBEEF + 0x4000

        // 1.00
        public static void PatchBinary100(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0x8F, 0xC2, 0x8D, 0x00,

                // call sub_F4BA00
                0xE8, 0x14, 0xBE, 0x14, 0x00,

                // call sub_F4BA20
                0xE8, 0x2F, 0xBE, 0x14, 0x00,

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

                // lea     rax, [_PGsceKernelGetModuleList]
                0x48, 0x8D, 0x05, 0xAA, 0xC8, 0xFA, 0x00,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0x93, 0x69, 0x87, 0x00,

                // retn
                0xC3,
            };
            
            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0x93, 0x29, 0x03, 0x01,

                // call    sub_DFFBF2
                0xE8, 0x7A, 0x11, 0x79, 0x00,

                // call    sub_DFFBE0
                0xE8, 0x63, 0x11, 0x79, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xDFFBE0 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0xA0; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x66EA66 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xDFFBE0 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x66EA66 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x16A1406 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.04
        public static void PatchBinary104(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0xB0, 0x1E, 0x91, 0x00,

                // call sub_F611D0
                0xE8, 0xF4, 0xC3, 0x14, 0x00,

                // call sub_F611F0
                0xE8, 0x0F, 0xC4, 0x14, 0x00,

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

                // lea     rax, [_PGsceKernelGetModuleList]
                0x48, 0x8D, 0x05, 0xBA, 0xD1, 0xFD, 0x00,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0xE3, 0xA5, 0x8A, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0x53, 0xAD, 0x06, 0x01,

                // call    sub_E14DE2
                0xE8, 0xEA, 0x51, 0x79, 0x00,

                // call    sub_E14DD0
                0xE8, 0xD3, 0x51, 0x79, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE14DD0 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0xA0; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x67FBE6 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE14DD0 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x67FBE6 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x16EA946 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.05
        public static void PatchBinary105(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0x10, 0x38, 0x91, 0x00,

                // call sub_F61BB0
                0xE8, 0xF4, 0xC3, 0x14, 0x00,

                // call sub_F61BD0
                0xE8, 0x0F, 0xC4, 0x14, 0x00,

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

                // lea     rax, [_PGsceKernelGetModuleList]
                0x48, 0x8D, 0x05, 0x3A, 0x09, 0xFE, 0x00,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0x03, 0xBF, 0x8A, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0x73, 0xCB, 0x06, 0x01,

                // call    sub_E157C2
                0xE8, 0xEA, 0x56, 0x79, 0x00,

                // call    sub_E157B0
                0xE8, 0xD3, 0x56, 0x79, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE157B0 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0xA0; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6800C6 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE157B0 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6800C6 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x16ECC46 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.06
        public static void PatchBinary106(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0x80, 0xDB, 0x91, 0x00,

                // call sub_F66170
                0xE8, 0x64, 0xC6, 0x14, 0x00,

                // call sub_F66190
                0xE8, 0x7F, 0xC6, 0x14, 0x00,

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

                // lea     rax, [_PGsceKernelGetModuleList]
                0x48, 0x8D, 0x05, 0x92, 0x5C, 0xFF, 0x00,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0x43, 0x5B, 0x8B, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0xC3, 0x66, 0x07, 0x01,

                // call    sub_E14DE2
                0xE8, 0xEA, 0x54, 0x79, 0x00,

                // call    sub_E14DD0
                0xE8, 0xD3, 0x54, 0x79, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE19B00 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0xA0; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x684616 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE19B00 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x684616 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x16FACE6 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.08
        public static void PatchBinary108(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0xD0, 0xF6, 0x93, 0x00,

                // call sub_F8C950
                0xE8, 0xD4, 0xBD, 0x14, 0x00,

                // call sub_F8C970
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

                // lea     rax, [_PGsceKernelGetModuleList]
                0x48, 0x8D, 0x05, 0x62, 0x2A, 0x03, 0x01,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0xB3, 0x57, 0x8D, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0x53, 0x07, 0x0A, 0x01,

                // call    sub_E40B82
                0xE8, 0xEA, 0xF4, 0x79, 0x00,

                // call    sub_E40B70
                0xE8, 0xD3, 0xF4, 0x79, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE40B70 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0xA0; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6A1686 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE40B70 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6A1686 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x1741DE6 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.09
        public static void PatchBinary109(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
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


            byte[] _path = modulePath;

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

        // 1.10
        public static void PatchBinary110(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0x70, 0xFA, 0x93, 0x00,

                // call sub_F8D8D0
                0xE8, 0xD4, 0xBD, 0x14, 0x00,

                // call sub_F8D8F0
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

                // lea     rax, [_PGsceKernelGetModuleList]
                0x48, 0x8D, 0x05, 0x02, 0x1C, 0x03, 0x01,

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
                0x48, 0x8D, 0x3D, 0x33, 0x13, 0x0A, 0x01,

                // call    sub_E41B02
                0xE8, 0x2A, 0xFD, 0x79, 0x00,

                // call    sub_E41AF0
                0xE8, 0x13, 0xFD, 0x79, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE41AF0 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0xA0; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6A1DC6 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE41AF0 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6A1DC6 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x1743106 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.11
        public static void PatchBinary111(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0x30, 0xFB, 0x93, 0x00,

                // call sub_F8D3D0
                0xE8, 0xD4, 0xBD, 0x14, 0x00,

                // call sub_F8D3F0
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

                // lea     rax, [_PGsceKernelGetModuleList]
                0x48, 0x8D, 0x05, 0x02, 0x21, 0x03, 0x01,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0x13, 0x5C, 0x8D, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0x63, 0x13, 0x0A, 0x01,

                // call    sub_E41602
                0xE8, 0x9A, 0xFC, 0x79, 0x00,

                // call    sub_E415F0
                0xE8, 0x83, 0xFC, 0x79, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE415F0 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0xA0; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6A1956 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE415F0 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6A1956 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x1742CC6 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.12
        public static void PatchBinary112(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
{
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0x30, 0xFB, 0x93, 0x00,

                // call sub_F8D3D0
                0xE8, 0xD4, 0xBD, 0x14, 0x00,

                // call sub_F8D3F0
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

                // lea     rax, [_PGsceKernelGetModuleList]
                0x48, 0x8D, 0x05, 0x02, 0x21, 0x03, 0x01,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0x13, 0x5C, 0x8D, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0x63, 0x13, 0x0A, 0x01,

                // call    sub_E41602
                0xE8, 0x9A, 0xFC, 0x79, 0x00,

                // call    sub_E415F0
                0xE8, 0x83, 0xFC, 0x79, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE415F0 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0xA0; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6A1956 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE415F0 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6A1956 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x1742CC6 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.13
        public static void PatchBinary113(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0x30, 0xFB, 0x93, 0x00,

                // call sub_F8D3D0
                0xE8, 0xD4, 0xBD, 0x14, 0x00,

                // call sub_F8D3F0
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

                // lea     rax, [_PGsceKernelGetModuleList]
                0x48, 0x8D, 0x05, 0x02, 0x21, 0x03, 0x01,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0x13, 0x5C, 0x8D, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0x63, 0x13, 0x0A, 0x01,

                // call    sub_E41602
                0xE8, 0x9A, 0xFC, 0x79, 0x00,

                // call    sub_E415F0
                0xE8, 0x83, 0xFC, 0x79, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE415F0 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0xA0; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6A1956 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE415F0 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6A1956 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x1742CC6 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.14
        public static void PatchBinary114(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0xB0, 0xE3, 0x93, 0x00,

                // call sub_F8D3D0
                0xE8, 0xD4, 0xBD, 0x14, 0x00,

                // call sub_F8D3F0
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

                // lea     rax, [_PGsceKernelGetModuleList]
                0x48, 0x8D, 0x05, 0xC2, 0xE4, 0x03, 0x01,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0xF3, 0x44, 0x8D, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0xE3, 0xFB, 0x09, 0x01,

                // call    sub_E41602
                0xE8, 0x9A, 0xFC, 0x79, 0x00,

                // call    sub_E415F0
                0xE8, 0x83, 0xFC, 0x79, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE415F0 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0xA0; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6A1956 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE415F0 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6A1956 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x1741546 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.16/1.17/1.19
        public static void PatchBinary116_117_119(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0xB0, 0xE3, 0x93, 0x00,

                // call sub_F8D3D0
                0xE8, 0xD4, 0xBD, 0x14, 0x00,

                // call sub_F8D3F0
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

                // lea     rax, [_PGsceKernelGetModuleList]
                0x48, 0x8D, 0x05, 0xC2, 0xE4, 0x03, 0x01,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0xF3, 0x44, 0x8D, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0xE3, 0xFB, 0x09, 0x01,

                // call    sub_E41602
                0xE8, 0x9A, 0xFC, 0x79, 0x00,

                // call    sub_E415F0
                0xE8, 0x83, 0xFC, 0x79, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE415F0 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0xA0; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6A1956 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE415F0 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6A1956 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x1741546 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }
        
        // <! past this version sceKernelGetModuleList no longer exists inside the eboot so we need to use somthing else. !>

        // 1.20
        public static void PatchBinary120(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            // sub_E8C900
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0xE8, 0x27, 0xAD, 0x00,

                // call    sub_FD18F0
                0xE8, 0xE4, 0x4F, 0x14, 0x00,

                // call    sub_FD1910
                0xE8, 0xFF, 0x4F, 0x14, 0x00,

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

                // lea     rax, [_PGpthread_mutex_destroy]
                0x48, 0x8D, 0x05, 0xE2, 0x15, 0x4E, 0x01,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0x63, 0x72, 0xA6, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0x03, 0x81, 0x23, 0x01,

                // call    sub_E8C912
                0xE8, 0x1A, 0x3B, 0x7A, 0x00,

                // call    sub_E8C900
                0xE8, 0x03, 0x3B, 0x7A, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE8C900 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x80; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6E8DE6 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE8C900 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6E8DE6 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x1920EF6 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.21
        public static void PatchBinary121(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0x78, 0x6C, 0xB0, 0x00,

                // call sub_FD2010
                0xE8, 0xE4, 0x4F, 0x14, 0x00,

                // call sub_FD2030
                0xE8, 0xFF, 0x4F, 0x14, 0x00,

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

                // lea     rax, [_PGpthread_mutex_destroy]
                0x48, 0x8D, 0x05, 0x72, 0x13, 0x4E, 0x01,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0x43, 0xB8, 0xA9, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0x63, 0xC8, 0x26, 0x01,

                // call    sub_E8D032
                0xE8, 0x9A, 0x3B, 0x7A, 0x00,

                // call    sub_E8D020
                0xE8, 0x83, 0x3B, 0x7A, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE8D020 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x80; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6E9486 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE8D020 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6E9486 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x1955CF6 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.22
        public static void PatchBinary122(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0x48, 0x8A, 0xB0, 0x00,

                // call sub_FD5660
                0xE8, 0x24, 0x6E, 0x14, 0x00,

                // call sub_FD5680
                0xE8, 0x3F, 0x6E, 0x14, 0x00,

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

                // lea     rax, [_PGpthread_mutex_destroy]
                0x48, 0x8D, 0x05, 0xFA, 0xFB, 0x4D, 0x01,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0x03, 0xD5, 0xA9, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0x43, 0xE4, 0x26, 0x01,

                // call    sub_E8E842
                0xE8, 0xCA, 0x3A, 0x7A, 0x00,

                // call    sub_E8E830
                0xE8, 0xB3, 0x3A, 0x7A, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE8E830 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x80; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6EAD66 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE8E830 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6EAD66 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x19591B6 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }
        
        // 1.25
        public static void PatchBinary125(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0xA8, 0xA3, 0xAF, 0x00,

                // call sub_FE6AC0
                0xE8, 0x24, 0x6E, 0x14, 0x00,

                // call sub_FE6AE0
                0xE8, 0x3F, 0x6E, 0x14, 0x00,

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

                // lea     rax, [_PGpthread_mutex_destroy]
                0x48, 0x8D, 0x05, 0xEA, 0xEB, 0x4C, 0x01,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0x23, 0xDA, 0xA8, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0xC3, 0xF9, 0x25, 0x01,

                // call    sub_E9FCA2
                0xE8, 0x8A, 0x4A, 0x7A, 0x00,

                // call    sub_E9FC90
                0xE8, 0x73, 0x4A, 0x7A, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE9FC90 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x80; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6FB206 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE9FC90 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6FB206 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x195ABD6 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }

        // 1.26
        public static void PatchBinary126(BinaryWriter bw, byte[] modulePath, byte[] dispbytes)
        {
            byte[] _asm1 = new byte[]
            {
                // lea rdi, [aApp0]
                0x48, 0x8D, 0x3D, 0xA8, 0xA3, 0xAF, 0x00,

                // call sub_FE69C0
                0xE8, 0x24, 0x6E, 0x14, 0x00,

                // call sub_FE69E0
                0xE8, 0x3F, 0x6E, 0x14, 0x00,

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

                // lea     rax, [_PGpthread_mutex_destroy]
                0x48, 0x8D, 0x05, 0xEA, 0xEC, 0x4C, 0x01,

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

            byte[] _asm3 = new byte[]
            {
                // mov     edi, 6
                0xBF, 0x06, 0x00, 0x00, 0x00,

                // call    sceKernelSleep
                0xE8, 0x23, 0xDA, 0xA8, 0x00,

                // retn
                0xC3,
            };

            byte[] _asm4 = new byte[]
            {
                // lea rdi, qword ptr [a11tesidleform]
                0x48, 0x8D, 0x3D, 0xC3, 0xF9, 0x25, 0x01,

                // call    sub_E9FBA2
                0xE8, 0x8A, 0x4A, 0x7A, 0x00,

                // call    sub_E9FB90
                0xE8, 0x73, 0x4A, 0x7A, 0x00,
            };

            byte[] _path = modulePath;

            bw.Seek(0xE9FB90 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x80; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0x6FB106 + 0x4000, SeekOrigin.Begin);

            for (int i = 0; i < 0x17; i++)
            {
                bw.Write((byte)0x90);
            }

            bw.Seek(0xE9FB90 + 0x4000, SeekOrigin.Begin);

            bw.Write(_asm1);
            bw.Write(_asm2);
            bw.Write(_asm3);

            bw.Seek(0x6FB106 + 0x4000 + 6, SeekOrigin.Begin);

            bw.Write(_asm4);

            bw.Seek(0x195AAD6 + 0x4000, SeekOrigin.Begin);

            bw.Write(_path);
        }
    }
}

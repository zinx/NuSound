using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MethodReplacer
{
    public class Replacer
    {
        static public IntPtr GetFunctionPointerRef(RuntimeMethodHandle handle)
        {
            RuntimeHelpers.PrepareMethod(handle);
            return handle.Value + 8; // Valid for x86 and x64.
        }

        static public IntPtr GetFunctionPointer(RuntimeMethodHandle handle)
        {
            RuntimeHelpers.PrepareMethod(handle);
            return handle.GetFunctionPointer();
        }

        const uint PAGE_EXECUTE_READWRITE = 0x40;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualProtect(IntPtr lpAddress, uint dwSize,
           uint flNewProtect, out uint lpflOldProtect);

        static public unsafe void InsertJumpToFunction(IntPtr jumpPtr, IntPtr addr, out byte[] saved)
        {
            Trace.Assert(IntPtr.Size == 4);
            uint oldProt, dummy;
            saved = new byte[5];
            for (int i = 0; i < saved.Length; ++i) { saved[i] = *(byte*)(jumpPtr + i).ToPointer(); }
            VirtualProtect(jumpPtr, 5, PAGE_EXECUTE_READWRITE, out oldProt);
            *(byte*)jumpPtr.ToPointer() = 0xE9;
            *(int*)(jumpPtr + 1).ToPointer() = addr.ToInt32() - jumpPtr.ToInt32() - 5;
            VirtualProtect(jumpPtr, 5, oldProt, out dummy);
        }

        static public unsafe void RestoreFunction(IntPtr jumpPtr, byte[] saved)
        {
            Trace.Assert(IntPtr.Size == 4);
            uint oldProt, dummy;
            VirtualProtect(jumpPtr, 5, PAGE_EXECUTE_READWRITE, out oldProt);
            for (int i = 0; i < saved.Length; ++i) { *(byte*)(jumpPtr + i).ToPointer() = saved[i]; }
            VirtualProtect(jumpPtr, 5, oldProt, out dummy);
        }

        // Doesn't always work - some calls are by method number, which is elsewhere
        static public unsafe void SetFunctionPointer(IntPtr destRef, IntPtr sourceRef)
        {
            *(void**)destRef.ToPointer() = *(void**)sourceRef.ToPointer();
        }

        static public unsafe IntPtr Deref(IntPtr reference)
        {
            return new IntPtr(*(void**)reference.ToPointer());
        }
    }
}

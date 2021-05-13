using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SDL2.Net5.Internal
{
    public static class Util
    {
        public static unsafe int Strlen(byte* str)
        {
            var result = 0;
            while (*str != 0)
            {
                result++;
                str++;
            }
            return result;
        }   

        public static unsafe string PtrToStringUTF8(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero) return null;
            var p = (byte*)ptr;
            var length = Strlen(p);
            return Encoding.UTF8.GetString(p, length);
        }

        public static unsafe IntPtr StringToHGlobalUTF8(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var ptr = Marshal.AllocHGlobal(bytes.Length + 1);
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            *((byte*)ptr + bytes.Length) = 0;
            return ptr;
        }
    }
}
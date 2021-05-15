using System;

using SDL_bool = System.Int32;
using size_t = System.IntPtr;

namespace SDL2.Net5
{
    public static class SdlRwops
    {
        public const int SDL_RWOPS_UNKNOWN = 0;
        public const int SDL_RWOPS_WINFILE = 1;
        public const int SDL_RWOPS_STDFILE = 2;
        public const int SDL_RWOPS_JNIFILE = 3;
        public const int SDL_RWOPS_MEMORY = 4;
        public const int SDL_RWOPS_MEMORY_RO = 5;
        public const int RW_SEEK_SET = 0;
        public const int RW_SEEK_CUR = 1;
        public const int RW_SEEK_END = 2;

        private delegate IntPtr SdlRwFromFileIntPtrIntPtrT(IntPtr file, IntPtr mode);
        private static readonly SdlRwFromFileIntPtrIntPtrT SSdlRwFromFileIntPtrIntPtrT = __LoadFunction<SdlRwFromFileIntPtrIntPtrT>("SDL_RWFromFile");
        public static IntPtr SDL_RWFromFile(IntPtr file, IntPtr mode) => SSdlRwFromFileIntPtrIntPtrT(file, mode);

        private delegate IntPtr SdlRwFromFpFileSdlBoolT(IntPtr fp, SDL_bool autoclose);
        private static readonly SdlRwFromFpFileSdlBoolT SSdlRwFromFpFileSdlBoolT = __LoadFunction<SdlRwFromFpFileSdlBoolT>("SDL_RWFromFP");
        public static IntPtr SDL_RWFromFP(IntPtr fp, SDL_bool autoclose) => SSdlRwFromFpFileSdlBoolT(fp, autoclose);
        
        private delegate IntPtr SdlRwFromMemVoidIntT(IntPtr mem, int size);
        private static readonly SdlRwFromMemVoidIntT SSdlRwFromMemVoidIntT = __LoadFunction<SdlRwFromMemVoidIntT>("SDL_RWFromMem");
        public static IntPtr SDL_RWFromMem(IntPtr mem, int size) => SSdlRwFromMemVoidIntT(mem, size);

        private delegate IntPtr SdlRwFromConstMemVoidIntT(IntPtr mem, int size);
        private static readonly SdlRwFromConstMemVoidIntT SSdlRwFromConstMemVoidIntT = __LoadFunction<SdlRwFromConstMemVoidIntT>("SDL_RWFromConstMem");
        public static IntPtr SDL_RWFromConstMem(IntPtr mem, int size) => SSdlRwFromConstMemVoidIntT(mem, size);

        private delegate IntPtr SdlAllocRwT();
        private static readonly SdlAllocRwT SSdlAllocRwT = __LoadFunction<SdlAllocRwT>("SDL_AllocRW");
        public static IntPtr SDL_AllocRW() => SSdlAllocRwT();

        private delegate void SdlFreeRwSdlRWopsT(IntPtr area);
        private static readonly SdlFreeRwSdlRWopsT SSdlFreeRwSdlRWopsT = __LoadFunction<SdlFreeRwSdlRWopsT>("SDL_FreeRW");
        public static void SDL_FreeRW(IntPtr area) => SSdlFreeRwSdlRWopsT(area);

        private delegate byte SdlReadU8SdlRWopsT(IntPtr src);
        private static readonly SdlReadU8SdlRWopsT SSdlReadU8SdlRWopsT = __LoadFunction<SdlReadU8SdlRWopsT>("SDL_ReadU8");
        public static byte SDL_ReadU8(IntPtr src) => SSdlReadU8SdlRWopsT(src);

        private delegate ushort SdlReadLe16SdlRWopsT(IntPtr src);
        private static readonly SdlReadLe16SdlRWopsT SSdlReadLe16SdlRWopsT = __LoadFunction<SdlReadLe16SdlRWopsT>("SDL_ReadLE16");
        public static ushort SDL_ReadLE16(IntPtr src) => SSdlReadLe16SdlRWopsT(src);

        private delegate ushort SdlReadBe16SdlRWopsT(IntPtr src);
        private static readonly SdlReadBe16SdlRWopsT SSdlReadBe16SdlRWopsT = __LoadFunction<SdlReadBe16SdlRWopsT>("SDL_ReadBE16");
        public static ushort SDL_ReadBE16(IntPtr src) => SSdlReadBe16SdlRWopsT(src);

        private delegate uint SdlReadLe32SdlRWopsT(IntPtr src);
        private static readonly SdlReadLe32SdlRWopsT SSdlReadLe32SdlRWopsT = __LoadFunction<SdlReadLe32SdlRWopsT>("SDL_ReadLE32");
        public static uint SDL_ReadLE32(IntPtr src) => SSdlReadLe32SdlRWopsT(src);

        private delegate uint SdlReadBe32SdlRWopsT(IntPtr src);
        private static readonly SdlReadBe32SdlRWopsT SSdlReadBe32SdlRWopsT = __LoadFunction<SdlReadBe32SdlRWopsT>("SDL_ReadBE32");
        public static uint SDL_ReadBE32(IntPtr src) => SSdlReadBe32SdlRWopsT(src);

        private delegate ulong SdlReadLe64SdlRWopsT(IntPtr src);
        private static readonly SdlReadLe64SdlRWopsT SSdlReadLe64SdlRWopsT = __LoadFunction<SdlReadLe64SdlRWopsT>("SDL_ReadLE64");
        public static ulong SDL_ReadLE64(IntPtr src) => SSdlReadLe64SdlRWopsT(src);

        private delegate ulong SdlReadBe64SdlRWopsT(IntPtr src);
        private static readonly SdlReadBe64SdlRWopsT SSdlReadBe64SdlRWopsT = __LoadFunction<SdlReadBe64SdlRWopsT>("SDL_ReadBE64");
        public static ulong SDL_ReadBE64(IntPtr src) => SSdlReadBe64SdlRWopsT(src);

        private delegate size_t SdlWriteU8SdlRWopsByteT(IntPtr dst, byte value);
        private static readonly SdlWriteU8SdlRWopsByteT SSdlWriteU8SdlRWopsByteT = __LoadFunction<SdlWriteU8SdlRWopsByteT>("SDL_WriteU8");
        public static size_t SDL_WriteU8(IntPtr dst, byte value) => SSdlWriteU8SdlRWopsByteT(dst, value);

        private delegate size_t SdlWriteLe16SdlRWopsUInt16T(IntPtr dst, UInt16 value);
        private static readonly SdlWriteLe16SdlRWopsUInt16T SSdlWriteLe16SdlRWopsUInt16T = __LoadFunction<SdlWriteLe16SdlRWopsUInt16T>("SDL_WriteLE16");
        public static size_t SDL_WriteLE16(IntPtr dst, UInt16 value) => SSdlWriteLe16SdlRWopsUInt16T(dst, value);

        private delegate size_t SdlWriteBe16SdlRWopsUInt16T(IntPtr dst, UInt16 value);
        private static readonly SdlWriteBe16SdlRWopsUInt16T SSdlWriteBe16SdlRWopsUInt16T = __LoadFunction<SdlWriteBe16SdlRWopsUInt16T>("SDL_WriteBE16");
        public static size_t SDL_WriteBE16(IntPtr dst, UInt16 value) => SSdlWriteBe16SdlRWopsUInt16T(dst, value);

        private delegate size_t SdlWriteLe32SdlRWopsUInt32T(IntPtr dst, UInt32 value);
        private static readonly SdlWriteLe32SdlRWopsUInt32T SSdlWriteLe32SdlRWopsUInt32T = __LoadFunction<SdlWriteLe32SdlRWopsUInt32T>("SDL_WriteLE32");
        public static size_t SDL_WriteLE32(IntPtr dst, UInt32 value) => SSdlWriteLe32SdlRWopsUInt32T(dst, value);

        private delegate size_t SdlWriteBe32SdlRWopsUInt32T(IntPtr dst, UInt32 value);
        private static readonly SdlWriteBe32SdlRWopsUInt32T SSdlWriteBe32SdlRWopsUInt32T = __LoadFunction<SdlWriteBe32SdlRWopsUInt32T>("SDL_WriteBE32");
        public static size_t SDL_WriteBE32(IntPtr dst, UInt32 value) => SSdlWriteBe32SdlRWopsUInt32T(dst, value);

        private delegate size_t SdlWriteLe64SdlRWopsUInt64T(IntPtr dst, UInt64 value);
        private static readonly SdlWriteLe64SdlRWopsUInt64T SSdlWriteLe64SdlRWopsUInt64T = __LoadFunction<SdlWriteLe64SdlRWopsUInt64T>("SDL_WriteLE64");
        public static size_t SDL_WriteLE64(IntPtr dst, UInt64 value) => SSdlWriteLe64SdlRWopsUInt64T(dst, value);

        private delegate size_t SdlWriteBe64SdlRWopsUInt64T(IntPtr dst, UInt64 value);
        private static readonly SdlWriteBe64SdlRWopsUInt64T SSdlWriteBe64SdlRWopsUInt64T = __LoadFunction<SdlWriteBe64SdlRWopsUInt64T>("SDL_WriteBE64");
        public static size_t SDL_WriteBE64(IntPtr dst, UInt64 value) => SSdlWriteBe64SdlRWopsUInt64T(dst, value);
        
        private static T __LoadFunction<T>(string name) { return Internal.LoaderSdl2.LoadFunction<T>(name); }
    }
}
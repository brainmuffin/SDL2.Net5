using System;
using System.Runtime.InteropServices;

using SDL_bool = System.Int32;

namespace SDL2.Net5
{
    public static class SdlPixels
    {
        public const int SDL_ALPHA_OPAQUE = 255;
        public const int SDL_ALPHA_TRANSPARENT = 0;

        /** Pixel formats */
        public const uint SDL_PIXELFORMAT_UNKNOWN = 0x0;
        public const uint SDL_PIXELFORMAT_INDEX1LSB = 0x11100100;
        public const uint SDL_PIXELFORMAT_INDEX1MSB = 0x11200100;
        public const uint SDL_PIXELFORMAT_INDEX4LSB = 0x12100400;
        public const uint SDL_PIXELFORMAT_INDEX4MSB = 0x12200400;
        public const uint SDL_PIXELFORMAT_INDEX8 = 0x13000801;
        public const uint SDL_PIXELFORMAT_RGB332 = 0x14110801;
        public const uint SDL_PIXELFORMAT_RGB444 = 0x15120c02;
        public const uint SDL_PIXELFORMAT_RGB555 = 0x15130f02;
        public const uint SDL_PIXELFORMAT_BGR555 = 0x15530f02;
        public const uint SDL_PIXELFORMAT_ARGB4444 = 0x15321002;
        public const uint SDL_PIXELFORMAT_RGBA4444 = 0x15421002;
        public const uint SDL_PIXELFORMAT_ABGR4444 = 0x15721002;
        public const uint SDL_PIXELFORMAT_BGRA4444 = 0x15821002;
        public const uint SDL_PIXELFORMAT_ARGB1555 = 0x15331002;
        public const uint SDL_PIXELFORMAT_RGBA5551 = 0x15441002;
        public const uint SDL_PIXELFORMAT_ABGR1555 = 0x15731002;
        public const uint SDL_PIXELFORMAT_BGRA5551 = 0x15841002;
        public const uint SDL_PIXELFORMAT_RGB565 = 0x15151002;
        public const uint SDL_PIXELFORMAT_BGR565 = 0x15551002;
        public const uint SDL_PIXELFORMAT_RGB24 = 0x17101803;
        public const uint SDL_PIXELFORMAT_BGR24 = 0x17401803;
        public const uint SDL_PIXELFORMAT_RGB888 = 0x16161804;
        public const uint SDL_PIXELFORMAT_RGBX8888 = 0x16261804;
        public const uint SDL_PIXELFORMAT_BGR888 = 0x16561804;
        public const uint SDL_PIXELFORMAT_BGRX8888 = 0x16661804;
        public const uint SDL_PIXELFORMAT_ARGB8888 = 0x16362004;
        public const uint SDL_PIXELFORMAT_RGBA8888 = 0x16462004;
        public const uint SDL_PIXELFORMAT_ABGR8888 = 0x16762004;
        public const uint SDL_PIXELFORMAT_BGRA8888 = 0x16862004;
        public const uint SDL_PIXELFORMAT_ARGB2101010 = 0x16372004;
        public const uint SDL_PIXELFORMAT_YV12 = 0x32315659;
        public const uint SDL_PIXELFORMAT_IYUV = 0x56555949;
        public const uint SDL_PIXELFORMAT_YUY2 = 0x32595559;
        public const uint SDL_PIXELFORMAT_UYVY = 0x59565955;
        public const uint SDL_PIXELFORMAT_YVYU = 0x55595659;

        /** Pixel type. */
        public enum SdlPixelType
        {
            SDL_PIXELTYPE_UNKNOWN,
            SDL_PIXELTYPE_INDEX1,
            SDL_PIXELTYPE_INDEX4,
            SDL_PIXELTYPE_INDEX8,
            SDL_PIXELTYPE_PACKED8,
            SDL_PIXELTYPE_PACKED16,
            SDL_PIXELTYPE_PACKED32,
            SDL_PIXELTYPE_ARRAYU8,
            SDL_PIXELTYPE_ARRAYU16,
            SDL_PIXELTYPE_ARRAYU32,
            SDL_PIXELTYPE_ARRAYF16,
            SDL_PIXELTYPE_ARRAYF32
        };

        /** Bitmap pixel order, high bit -> low bit. */
        public enum SdlBitmapOrder
        {
            SDL_BITMAPORDER_NONE,
            SDL_BITMAPORDER_4321,
            SDL_BITMAPORDER_1234
        };

        /** Packed component order, high bit -> low bit. */
        public enum SdlPackedOrder
        {
            SDL_PACKEDORDER_NONE,
            SDL_PACKEDORDER_XRGB,
            SDL_PACKEDORDER_RGBX,
            SDL_PACKEDORDER_ARGB,
            SDL_PACKEDORDER_RGBA,
            SDL_PACKEDORDER_XBGR,
            SDL_PACKEDORDER_BGRX,
            SDL_PACKEDORDER_ABGR,
            SDL_PACKEDORDER_BGRA
        };

        /** Array component order, low byte -> high byte. */
        public enum SdlArrayOrder
        {
            SDL_ARRAYORDER_NONE,
            SDL_ARRAYORDER_RGB,
            SDL_ARRAYORDER_RGBA,
            SDL_ARRAYORDER_ARGB,
            SDL_ARRAYORDER_BGR,
            SDL_ARRAYORDER_BGRA,
            SDL_ARRAYORDER_ABGR
        };

        /** Packed component layout. */
        public enum SdlPackedLayout
        {
            SDL_PACKEDLAYOUT_NONE,
            SDL_PACKEDLAYOUT_332,
            SDL_PACKEDLAYOUT_4444,
            SDL_PACKEDLAYOUT_1555,
            SDL_PACKEDLAYOUT_5551,
            SDL_PACKEDLAYOUT_565,
            SDL_PACKEDLAYOUT_8888,
            SDL_PACKEDLAYOUT_2101010,
            SDL_PACKEDLAYOUT_1010102
        };
        
        public static uint SDL_PIXELFLAG(uint format) => ((format >> 28) & 0x0F);
        public static SdlPixelType SDL_PIXELTYPE(uint format) => (SdlPixelType)((format >> 24) & 0x0F);
        // SDL_BitmapOrder, SDL_PackedOrder or SDL_ArrayOrder
        public static int SDL_PIXELORDER(uint format) => (int)((format >> 20) & 0x0F);
        public static SdlPackedLayout SDL_PIXELLAYOUT(uint format) => (SdlPackedLayout)((format >> 16) & 0x0F);
        public static int SDL_BITSPERPIXEL(uint format) => (int)((format >> 8) & 0xFF);
        public static int SDL_BYTESPERPIXEL(uint format)
        {
            if (SDL_ISPIXELFORMAT_FOURCC(format))
            {
                switch (format)
                {
                    case SDL_PIXELFORMAT_YUY2:
                    case SDL_PIXELFORMAT_UYVY:
                    case SDL_PIXELFORMAT_YVYU:
                        return 2;
                    default:
                        return 1;
                }
            }
            return (int)(format & 0xFF);
        }
        public static bool SDL_ISPIXELFORMAT_FOURCC(uint format) => ((format) & (SDL_PIXELFLAG(format) != 1 ? 1U : 0U)) > 0;
        public static bool SDL_ISPIXELFORMAT_ALPHA(uint format)
        {
            if (SDL_ISPIXELFORMAT_FOURCC(format)) return false;
            if (SDL_ISPIXELFORMAT_PACKED(format))
            {
                switch ((SdlPackedOrder)SDL_PIXELORDER(format))
                {
                    case SdlPackedOrder.SDL_PACKEDORDER_ARGB:
                    case SdlPackedOrder.SDL_PACKEDORDER_ABGR:
                    case SdlPackedOrder.SDL_PACKEDORDER_BGRA:
                    case SdlPackedOrder.SDL_PACKEDORDER_RGBA:
                        return true;
                    default:
                        return false;
                }
            }

            if (SDL_ISPIXELFORMAT_ARRAY(format))
            {
                switch ((SdlArrayOrder)SDL_PIXELORDER(format))
                {
                    case SdlArrayOrder.SDL_ARRAYORDER_ABGR:
                    case SdlArrayOrder.SDL_ARRAYORDER_ARGB:
                    case SdlArrayOrder.SDL_ARRAYORDER_BGRA:
                    case SdlArrayOrder.SDL_ARRAYORDER_RGBA:
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }

        public static bool SDL_ISPIXELFORMAT_INDEXED(uint format)
        {
            if (SDL_ISPIXELFORMAT_FOURCC(format)) return false;
            switch (SDL_PIXELTYPE(format))
            {
                case SdlPixelType.SDL_PIXELTYPE_INDEX1:
                case SdlPixelType.SDL_PIXELTYPE_INDEX4:
                case SdlPixelType.SDL_PIXELTYPE_INDEX8:
                    return true;
                default:
                    return false;
            }
        }

        public static bool SDL_ISPIXELFORMAT_PACKED(uint format)
        {
            if (SDL_ISPIXELFORMAT_FOURCC(format)) return false;
            switch (SDL_PIXELTYPE(format))
            {
                case SdlPixelType.SDL_PIXELTYPE_PACKED8:
                case SdlPixelType.SDL_PIXELTYPE_PACKED16:
                case SdlPixelType.SDL_PIXELTYPE_PACKED32:
                    return true;
                default:
                    return false;
            }
        }

        public static bool SDL_ISPIXELFORMAT_ARRAY(uint format)
        {
            if (SDL_ISPIXELFORMAT_FOURCC(format)) return false;
            switch (SDL_PIXELTYPE(format))
            {
                case SdlPixelType.SDL_PIXELTYPE_ARRAYU8:
                case SdlPixelType.SDL_PIXELTYPE_ARRAYU16:
                case SdlPixelType.SDL_PIXELTYPE_ARRAYU32:
                case SdlPixelType.SDL_PIXELTYPE_ARRAYF16:
                case SdlPixelType.SDL_PIXELTYPE_ARRAYF32:
                    return true;
                default:
                    return false;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SdlColor
        {
            public byte r;
            public byte g;
            public byte b;
            public byte a;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SdlPalette
        {
            public int ncolors;
            public IntPtr colors;
            public UInt32 version;
            public int refcount;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SdlPixelFormat
        {
            public UInt32 format;
            public IntPtr palette;
            public byte BitsPerPixel;
            public byte BytesPerPixel;
            public byte padding_1;
            public byte padding_2;
            public UInt32 Rmask;
            public UInt32 Gmask;
            public UInt32 Bmask;
            public UInt32 Amask;
            public byte Rloss;
            public byte Gloss;
            public byte Bloss;
            public byte Aloss;
            public byte Rshift;
            public byte Gshift;
            public byte Bshift;
            public byte Ashift;
            public int refcount;
            public IntPtr next;
        }

        private delegate IntPtr SdlGetPixelFormatNameUInt32T(UInt32 format);
        private static readonly SdlGetPixelFormatNameUInt32T SSdlGetPixelFormatNameUInt32T = __LoadFunction<SdlGetPixelFormatNameUInt32T>("SDL_GetPixelFormatName");
        public static IntPtr SDL_GetPixelFormatName(UInt32 format) => SSdlGetPixelFormatNameUInt32T(format);

        private delegate SDL_bool SdlPixelFormatEnumToMasksUInt32IntUintUintUintUintT(UInt32 format, ref int bpp, ref uint Rmask, ref uint Gmask, ref uint Bmask, ref uint Amask);
        private static readonly SdlPixelFormatEnumToMasksUInt32IntUintUintUintUintT SSdlPixelFormatEnumToMasksUInt32IntUintUintUintUintT = __LoadFunction<SdlPixelFormatEnumToMasksUInt32IntUintUintUintUintT>("SDL_PixelFormatEnumToMasks");
        public static SDL_bool SDL_PixelFormatEnumToMasks(UInt32 format, ref int bpp, ref uint Rmask, ref uint Gmask, ref uint Bmask, ref uint Amask) => SSdlPixelFormatEnumToMasksUInt32IntUintUintUintUintT(format, ref bpp, ref Rmask, ref Gmask, ref Bmask, ref Amask);

        private delegate uint SdlMasksToPixelFormatEnumIntUInt32UInt32UInt32UInt32T(int bpp, UInt32 Rmask, UInt32 Gmask, UInt32 Bmask, UInt32 Amask);
        private static readonly SdlMasksToPixelFormatEnumIntUInt32UInt32UInt32UInt32T SSdlMasksToPixelFormatEnumIntUInt32UInt32UInt32UInt32T = __LoadFunction<SdlMasksToPixelFormatEnumIntUInt32UInt32UInt32UInt32T>("SDL_MasksToPixelFormatEnum");
        public static uint SDL_MasksToPixelFormatEnum(int bpp, UInt32 Rmask, UInt32 Gmask, UInt32 Bmask, UInt32 Amask) => SSdlMasksToPixelFormatEnumIntUInt32UInt32UInt32UInt32T(bpp, Rmask, Gmask, Bmask, Amask);

        private delegate IntPtr SdlAllocFormatUInt32T(UInt32 pixelFormat);
        private static readonly SdlAllocFormatUInt32T SSdlAllocFormatUInt32T = __LoadFunction<SdlAllocFormatUInt32T>("SDL_AllocFormat");
        public static IntPtr SDL_AllocFormat(UInt32 pixel_format) => SSdlAllocFormatUInt32T(pixel_format);

        private delegate void SdlFreeFormatSdlPixelFormatT(IntPtr format);
        private static readonly SdlFreeFormatSdlPixelFormatT SSdlFreeFormatSdlPixelFormatT = __LoadFunction<SdlFreeFormatSdlPixelFormatT>("SDL_FreeFormat");
        public static void SDL_FreeFormat(IntPtr format) => SSdlFreeFormatSdlPixelFormatT(format);

        private delegate IntPtr SdlAllocPaletteIntT(int ncolors);
        private static readonly SdlAllocPaletteIntT SSdlAllocPaletteIntT = __LoadFunction<SdlAllocPaletteIntT>("SDL_AllocPalette");
        public static IntPtr SDL_AllocPalette(int ncolors) => SSdlAllocPaletteIntT(ncolors);

        private delegate int SdlSetPixelFormatPaletteSdlPixelFormatSdlPaletteT(IntPtr format, IntPtr palette);
        private static readonly SdlSetPixelFormatPaletteSdlPixelFormatSdlPaletteT SSdlSetPixelFormatPaletteSdlPixelFormatSdlPaletteT = __LoadFunction<SdlSetPixelFormatPaletteSdlPixelFormatSdlPaletteT>("SDL_SetPixelFormatPalette");
        public static int SDL_SetPixelFormatPalette(IntPtr format, IntPtr palette) => SSdlSetPixelFormatPaletteSdlPixelFormatSdlPaletteT(format, palette);

        private delegate int SdlSetPaletteColorsSdlPaletteSdlColorIntIntT(IntPtr palette, IntPtr colors, int firstcolor, int ncolors);
        private static readonly SdlSetPaletteColorsSdlPaletteSdlColorIntIntT SSdlSetPaletteColorsSdlPaletteSdlColorIntIntT = __LoadFunction<SdlSetPaletteColorsSdlPaletteSdlColorIntIntT>("SDL_SetPaletteColors");
        public static int SDL_SetPaletteColors(IntPtr palette, IntPtr colors, int firstcolor, int ncolors) => SSdlSetPaletteColorsSdlPaletteSdlColorIntIntT(palette, colors, firstcolor, ncolors);

        private delegate void SdlFreePaletteSdlPaletteT(IntPtr palette);
        private static readonly SdlFreePaletteSdlPaletteT SSdlFreePaletteSdlPaletteT = __LoadFunction<SdlFreePaletteSdlPaletteT>("SDL_FreePalette");
        public static void SDL_FreePalette(IntPtr palette) => SSdlFreePaletteSdlPaletteT(palette);

        private delegate uint SdlMapRgbSdlPixelFormatByteByteByteT(IntPtr format, byte r, byte g, byte b);
        private static readonly SdlMapRgbSdlPixelFormatByteByteByteT SSdlMapRgbSdlPixelFormatByteByteByteT = __LoadFunction<SdlMapRgbSdlPixelFormatByteByteByteT>("SDL_MapRGB");
        public static uint SDL_MapRGB(IntPtr format, byte r, byte g, byte b) => SSdlMapRgbSdlPixelFormatByteByteByteT(format, r, g, b);

        private delegate uint SdlMapRgbaSdlPixelFormatByteByteByteByteT(IntPtr format, byte r, byte g, byte b, byte a);
        private static readonly SdlMapRgbaSdlPixelFormatByteByteByteByteT SSdlMapRgbaSdlPixelFormatByteByteByteByteT = __LoadFunction<SdlMapRgbaSdlPixelFormatByteByteByteByteT>("SDL_MapRGBA");
        public static uint SDL_MapRGBA(IntPtr format, byte r, byte g, byte b, byte a) => SSdlMapRgbaSdlPixelFormatByteByteByteByteT(format, r, g, b, a);

        private delegate void SdlGetRgbUInt32SdlPixelFormatByteByteByteT(UInt32 pixel, IntPtr format, ref byte r, ref byte g, ref byte b);
        private static readonly SdlGetRgbUInt32SdlPixelFormatByteByteByteT SSdlGetRgbUInt32SdlPixelFormatByteByteByteT = __LoadFunction<SdlGetRgbUInt32SdlPixelFormatByteByteByteT>("SDL_GetRGB");
        public static void SDL_GetRGB(UInt32 pixel, IntPtr format, ref byte r, ref byte g, ref byte b) => SSdlGetRgbUInt32SdlPixelFormatByteByteByteT(pixel, format, ref r, ref g, ref b);

        private delegate void SdlGetRgbaUInt32SdlPixelFormatByteByteByteByteT(UInt32 pixel, IntPtr format, ref byte r, ref byte g, ref byte b, ref byte a);
        private static readonly SdlGetRgbaUInt32SdlPixelFormatByteByteByteByteT SSdlGetRgbaUInt32SdlPixelFormatByteByteByteByteT = __LoadFunction<SdlGetRgbaUInt32SdlPixelFormatByteByteByteByteT>("SDL_GetRGBA");
        public static void SDL_GetRGBA(UInt32 pixel, IntPtr format, ref byte r, ref byte g, ref byte b, ref byte a) => SSdlGetRgbaUInt32SdlPixelFormatByteByteByteByteT(pixel, format, ref r, ref g, ref b, ref a);

        private delegate void SdlCalculateGammaRampFloatUshortT(float gamma, ref ushort ramp);
        private static readonly SdlCalculateGammaRampFloatUshortT SSdlCalculateGammaRampFloatUshortT = __LoadFunction<SdlCalculateGammaRampFloatUshortT>("SDL_CalculateGammaRamp");
        public static void SDL_CalculateGammaRamp(float gamma, ref ushort ramp) => SSdlCalculateGammaRampFloatUshortT(gamma, ref ramp);
        
        private static T __LoadFunction<T>(string name) { return Internal.LoaderSdl2.LoadFunction<T>(name); }
    }
}
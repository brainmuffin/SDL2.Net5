using System;
using System.Runtime.InteropServices;
using SDL2.Net5.Internal;
using static SDL2.Net5.SdlBlendmode;
using static SDL2.Net5.SdlPixels;
using static SDL2.Net5.SdlPointRect;
using static SDL2.Net5.SdlRwops;

using SDL_RWops = System.IntPtr;
using SDL_bool = System.Int32;

namespace SDL2.Net5
{
    public static class SdlSurfaceBlit
    {
        public const int SDL_SWSURFACE = 0;
        public const int SDL_PREALLOC = 0x00000001;
        public const int SDL_RLEACCEL = 0x00000002;
        public const int SDL_DONTFREE = 0x00000004;
        public static bool SDL_MUSTLOCK(SdlSurface S) => (S.flags & SDL_RLEACCEL) != 0;

        [StructLayout(LayoutKind.Sequential)]
        unsafe struct SdlBlitInfo
        {
            byte* src;
            int src_w, src_h;
            int src_pitch;
            int src_skip;
            byte* dst;
            int dst_w, dst_h;
            int dst_pitch;
            int dst_skip;
            SdlPixelFormat* src_fmt;
            SdlPixelFormat* dst_fmt;
            byte* table;
            int flags;
            UInt32 colorkey;
            byte r, g, b, a;
        }

        [StructLayout(LayoutKind.Sequential)]
        unsafe struct SdlBlitMap
        {
            SdlSurface* dst;
            int identity;
            IntPtr blit;
            void* data;
            SdlBlitInfo info;

            /* the version count matches the destination; mismatch indicates
               an invalid mapping */
            UInt32 dst_palette_version;
            UInt32 src_palette_version;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SdlSurface
        {
            public UInt32 flags;               /**< Read-only */
            public IntPtr format;              /**< Read-only */
            public int w, h;                   /**< Read-only */
            public int pitch;                  /**< Read-only */
            public IntPtr pixels;              /**< Read-write */

            /** Application data associated with the surface */
            public IntPtr userdata;             /**< Read-write */

            /** information needed for surfaces requiring locks */
            public int locked;                 /**< Read-only */
            public IntPtr lock_data;           /**< Read-only */

            /** clipping information */
            public SdlRect clip_rect;         /**< Read-only */

            /** info for fast blit mapping to other surfaces */
            // struct SDL_BlitMap map;    /**< Private */
            SdlBlitMap map;

            /** Reference count -- used when freeing surface */
            int refcount;               /**< Read-mostly */
        }

        private delegate IntPtr SdlCreateRgbSurfaceUintIntIntIntUintUintUintUintT(uint flags, int width, int height, int depth, uint rmask, uint gmask, uint bmask, uint amask);
        private static readonly SdlCreateRgbSurfaceUintIntIntIntUintUintUintUintT SSdlCreateRgbSurfaceUintIntIntIntUintUintUintUintT = __LoadFunction<SdlCreateRgbSurfaceUintIntIntIntUintUintUintUintT>("SDL_CreateRGBSurface");
        public static IntPtr SDL_CreateRGBSurface(uint flags, int width, int height, int depth, uint rmask, uint gmask, uint bmask, uint amask) => SSdlCreateRgbSurfaceUintIntIntIntUintUintUintUintT(flags, width, height, depth, rmask, gmask, bmask, amask);

        private delegate IntPtr SdlCreateRgbSurfaceFromIntPtrIntIntIntIntUInt32UInt32UInt32UInt32T(IntPtr pixels, int width, int height, int depth, int pitch, UInt32 Rmask, UInt32 Gmask, UInt32 Bmask, UInt32 Amask);
        private static readonly SdlCreateRgbSurfaceFromIntPtrIntIntIntIntUInt32UInt32UInt32UInt32T SSdlCreateRgbSurfaceFromIntPtrIntIntIntIntUInt32UInt32UInt32UInt32T = __LoadFunction<SdlCreateRgbSurfaceFromIntPtrIntIntIntIntUInt32UInt32UInt32UInt32T>("SDL_CreateRGBSurfaceFrom");
        public static IntPtr SDL_CreateRGBSurfaceFrom(IntPtr pixels, int width, int height, int depth, int pitch, UInt32 Rmask, UInt32 Gmask, UInt32 Bmask, UInt32 Amask) => SSdlCreateRgbSurfaceFromIntPtrIntIntIntIntUInt32UInt32UInt32UInt32T(pixels, width, height, depth, pitch, Rmask, Gmask, Bmask, Amask);

        private delegate void SdlFreeSurfaceSdlSurfaceT(IntPtr surface);
        private static readonly SdlFreeSurfaceSdlSurfaceT SSdlFreeSurfaceSdlSurfaceT = __LoadFunction<SdlFreeSurfaceSdlSurfaceT>("SDL_FreeSurface");
        public static void SdlFreeSurface(IntPtr surface) => SSdlFreeSurfaceSdlSurfaceT(surface);

        private delegate int SdlSetSurfacePaletteSdlSurfaceSdlPaletteT(IntPtr surface, ref SdlPalette palette);
        private static readonly SdlSetSurfacePaletteSdlSurfaceSdlPaletteT SSdlSetSurfacePaletteSdlSurfaceSdlPaletteT = __LoadFunction<SdlSetSurfacePaletteSdlSurfaceSdlPaletteT>("SDL_SetSurfacePalette");
        public static int SDL_SetSurfacePalette(IntPtr surface, ref SdlPalette palette) => SSdlSetSurfacePaletteSdlSurfaceSdlPaletteT(surface, ref palette);

        private delegate int SdlLockSurfaceSdlSurfaceT(IntPtr surface);
        private static readonly SdlLockSurfaceSdlSurfaceT SSdlLockSurfaceSdlSurfaceT = __LoadFunction<SdlLockSurfaceSdlSurfaceT>("SDL_LockSurface");
        public static int SDL_LockSurface(IntPtr surface) => SSdlLockSurfaceSdlSurfaceT(surface);

        private delegate void SdlUnlockSurfaceSdlSurfaceT(IntPtr surface);
        private static readonly SdlUnlockSurfaceSdlSurfaceT SSdlUnlockSurfaceSdlSurfaceT = __LoadFunction<SdlUnlockSurfaceSdlSurfaceT>("SDL_UnlockSurface");
        public static void SDL_UnlockSurface(IntPtr surface) => SSdlUnlockSurfaceSdlSurfaceT(surface);

        private delegate IntPtr SdlLoadBmpRwSdlRWopsIntT(IntPtr src, int freesrc);
        private static readonly SdlLoadBmpRwSdlRWopsIntT SSdlLoadBmpRwSdlRWopsIntT = __LoadFunction<SdlLoadBmpRwSdlRWopsIntT>("SDL_LoadBMP_RW");
        public static IntPtr SDL_LoadBMP_RW(IntPtr src, int freesrc) => SSdlLoadBmpRwSdlRWopsIntT(src, freesrc);
        
        public static IntPtr SDL_LoadBMP(string filename) => SDL_LoadBMP_RW(SDL_RWFromFile(Util.StringToHGlobalUTF8(filename), Util.StringToHGlobalUTF8("rb")), 1);

        private delegate int SdlSetSurfaceRleSdlSurfaceIntT(IntPtr surface, int flag);
        private static readonly SdlSetSurfaceRleSdlSurfaceIntT SSdlSetSurfaceRleSdlSurfaceIntT = __LoadFunction<SdlSetSurfaceRleSdlSurfaceIntT>("SDL_SetSurfaceRLE");
        public static int SDL_SetSurfaceRLE(IntPtr surface, int flag) => SSdlSetSurfaceRleSdlSurfaceIntT(surface, flag);

        private delegate int SdlSetColorKeySdlSurfaceIntUInt32T(IntPtr surface, int flag, UInt32 key);
        private static readonly SdlSetColorKeySdlSurfaceIntUInt32T SSdlSetColorKeySdlSurfaceIntUInt32T = __LoadFunction<SdlSetColorKeySdlSurfaceIntUInt32T>("SDL_SetColorKey");
        public static int SDL_SetColorKey(IntPtr surface, int flag, UInt32 key) => SSdlSetColorKeySdlSurfaceIntUInt32T(surface, flag, key);

        private delegate int SdlGetColorKeySdlSurfaceUintT(IntPtr surface, ref uint key);
        private static readonly SdlGetColorKeySdlSurfaceUintT SSdlGetColorKeySdlSurfaceUintT = __LoadFunction<SdlGetColorKeySdlSurfaceUintT>("SDL_GetColorKey");
        public static int SDL_GetColorKey(IntPtr surface, ref uint key) => SSdlGetColorKeySdlSurfaceUintT(surface, ref key);

        private delegate int SdlSetSurfaceColorModSdlSurfaceByteByteByteT(IntPtr surface, byte r, byte g, byte b);
        private static readonly SdlSetSurfaceColorModSdlSurfaceByteByteByteT SSdlSetSurfaceColorModSdlSurfaceByteByteByteT = __LoadFunction<SdlSetSurfaceColorModSdlSurfaceByteByteByteT>("SDL_SetSurfaceColorMod");
        public static int SDL_SetSurfaceColorMod(IntPtr surface, byte r, byte g, byte b) => SSdlSetSurfaceColorModSdlSurfaceByteByteByteT(surface, r, g, b);

        private delegate int SdlGetSurfaceColorModSdlSurfaceByteByteByteT(IntPtr surface, ref byte r, ref byte g, ref byte b);
        private static readonly SdlGetSurfaceColorModSdlSurfaceByteByteByteT SSdlGetSurfaceColorModSdlSurfaceByteByteByteT = __LoadFunction<SdlGetSurfaceColorModSdlSurfaceByteByteByteT>("SDL_GetSurfaceColorMod");
        public static int SDL_GetSurfaceColorMod(IntPtr surface, ref byte r, ref byte g, ref byte b) => SSdlGetSurfaceColorModSdlSurfaceByteByteByteT(surface, ref r, ref g, ref b);

        private delegate int SdlSetSurfaceAlphaModSdlSurfaceByteT(IntPtr surface, byte alpha);
        private static readonly SdlSetSurfaceAlphaModSdlSurfaceByteT SSdlSetSurfaceAlphaModSdlSurfaceByteT = __LoadFunction<SdlSetSurfaceAlphaModSdlSurfaceByteT>("SDL_SetSurfaceAlphaMod");
        public static int SDL_SetSurfaceAlphaMod(IntPtr surface, byte alpha) => SSdlSetSurfaceAlphaModSdlSurfaceByteT(surface, alpha);

        private delegate int SdlGetSurfaceAlphaModSdlSurfaceByteT(IntPtr surface, ref byte alpha);
        private static readonly SdlGetSurfaceAlphaModSdlSurfaceByteT SSdlGetSurfaceAlphaModSdlSurfaceByteT = __LoadFunction<SdlGetSurfaceAlphaModSdlSurfaceByteT>("SDL_GetSurfaceAlphaMod");
        public static int SDL_GetSurfaceAlphaMod(IntPtr surface, ref byte alpha) => SSdlGetSurfaceAlphaModSdlSurfaceByteT(surface, ref alpha);

        private delegate int SdlSetSurfaceBlendModeSdlSurfaceSdlBlendModeT(IntPtr surface, SdlBlendMode blendMode);
        private static readonly SdlSetSurfaceBlendModeSdlSurfaceSdlBlendModeT SSdlSetSurfaceBlendModeSdlSurfaceSdlBlendModeT = __LoadFunction<SdlSetSurfaceBlendModeSdlSurfaceSdlBlendModeT>("SDL_SetSurfaceBlendMode");
        public static int SDL_SetSurfaceBlendMode(IntPtr surface, SdlBlendMode blendMode) => SSdlSetSurfaceBlendModeSdlSurfaceSdlBlendModeT(surface, blendMode);

        private delegate int SdlGetSurfaceBlendModeSdlSurfaceSdlBlendModeT(IntPtr surface, ref SdlBlendMode blendMode);
        private static readonly SdlGetSurfaceBlendModeSdlSurfaceSdlBlendModeT SSdlGetSurfaceBlendModeSdlSurfaceSdlBlendModeT = __LoadFunction<SdlGetSurfaceBlendModeSdlSurfaceSdlBlendModeT>("SDL_GetSurfaceBlendMode");
        public static int SDL_GetSurfaceBlendMode(IntPtr surface, ref SdlBlendMode blendMode) => SSdlGetSurfaceBlendModeSdlSurfaceSdlBlendModeT(surface, ref blendMode);

        private unsafe delegate SDL_bool SdlSetClipRectSdlSurfaceSdlRectT(IntPtr surface, SdlRect* rect);
        private static readonly SdlSetClipRectSdlSurfaceSdlRectT SSdlSetClipRectSdlSurfaceSdlRectT = __LoadFunction<SdlSetClipRectSdlSurfaceSdlRectT>("SDL_SetClipRect");
        public static unsafe SDL_bool SDL_SetClipRect(IntPtr surface, SdlRect* rect) => SSdlSetClipRectSdlSurfaceSdlRectT(surface, rect);

        private unsafe delegate void SdlGetClipRectSdlSurfaceSdlRectT(IntPtr surface, SdlRect* rect);
        private static readonly SdlGetClipRectSdlSurfaceSdlRectT SSdlGetClipRectSdlSurfaceSdlRectT = __LoadFunction<SdlGetClipRectSdlSurfaceSdlRectT>("SDL_GetClipRect");
        public static unsafe void SDL_GetClipRect(IntPtr surface, SdlRect* rect) => SSdlGetClipRectSdlSurfaceSdlRectT(surface, rect);

        private delegate int SdlConvertPixelsIntIntUInt32IntPtrIntUInt32IntPtrIntT(int width, int height, UInt32 src_format, IntPtr src, int src_pitch, UInt32 dst_format, IntPtr dst, int dst_pitch);
        private static readonly SdlConvertPixelsIntIntUInt32IntPtrIntUInt32IntPtrIntT SSdlConvertPixelsIntIntUInt32IntPtrIntUInt32IntPtrIntT = __LoadFunction<SdlConvertPixelsIntIntUInt32IntPtrIntUInt32IntPtrIntT>("SDL_ConvertPixels");
        public static int SDL_ConvertPixels(int width, int height, UInt32 src_format, IntPtr src, int src_pitch, UInt32 dst_format, IntPtr dst, int dst_pitch) => SSdlConvertPixelsIntIntUInt32IntPtrIntUInt32IntPtrIntT(width, height, src_format, src, src_pitch, dst_format, dst, dst_pitch);

        private unsafe delegate int SdlSoftStretchSdlSurfaceSdlRectSdlSurfaceSdlRectT(IntPtr src, SdlRect* srcrect, IntPtr dst, SdlRect* dstrect);
        private unsafe delegate int SdlBlitScaledIntPtrSdlRectIntPtrSdlRectT(IntPtr src, SdlRect* srcrect, IntPtr dst, SdlRect* dstrect);
        private static readonly SdlBlitScaledIntPtrSdlRectIntPtrSdlRectT SSdlBlitScaledIntPtrSdlRectIntPtrSdlRectT;
        public static unsafe int SDL_BlitScaled(IntPtr src, SdlRect* srcrect, IntPtr dst, SdlRect* dstrect) => SSdlBlitScaledIntPtrSdlRectIntPtrSdlRectT(src, srcrect, dst, dstrect);

        public static unsafe int SDL_BlitScaled(IntPtr src, IntPtr dst) =>
            SDL_BlitScaled(src, null, dst, null);

        private unsafe delegate int SdlLowerBlitScaledIntPtrSdlRectIntPtrSdlRectT(IntPtr src, SdlRect* srcrect, IntPtr dst, SdlRect* dstrect);
        private static readonly SdlLowerBlitScaledIntPtrSdlRectIntPtrSdlRectT SSdlLowerBlitScaledIntPtrSdlRectIntPtrSdlRectT = __LoadFunction<SdlLowerBlitScaledIntPtrSdlRectIntPtrSdlRectT>("SDL_LowerBlitScaled");
        public static unsafe int SDL_LowerBlitScaled(IntPtr src, SdlRect* srcrect, IntPtr dst, SdlRect* dstrect) => SSdlLowerBlitScaledIntPtrSdlRectIntPtrSdlRectT(src, srcrect, dst, dstrect);

        private unsafe delegate int SdlFillRectIntPtrSdlRectUInt32T(IntPtr dst, SdlRect* rect, UInt32 color);
        private static readonly SdlFillRectIntPtrSdlRectUInt32T SSdlFillRectIntPtrSdlRectUInt32T = __LoadFunction<SdlFillRectIntPtrSdlRectUInt32T>("SDL_FillRect");
        public static unsafe int SDL_FillRect(IntPtr dst, SdlRect* rect, UInt32 color) => SSdlFillRectIntPtrSdlRectUInt32T(dst, rect, color);

        private delegate int SdlFillRectsIntPtrIntPtrIntUInt32T(IntPtr dst, IntPtr rects, int count, UInt32 color);
        private static readonly SdlFillRectsIntPtrIntPtrIntUInt32T SSdlFillRectsIntPtrIntPtrIntUInt32T = __LoadFunction<SdlFillRectsIntPtrIntPtrIntUInt32T>("SDL_FillRects");
        public static int SDL_FillRects(IntPtr dst, IntPtr rects, int count, UInt32 color) => SSdlFillRectsIntPtrIntPtrIntUInt32T(dst, rects, count, color);

        private static readonly SdlSoftStretchSdlSurfaceSdlRectSdlSurfaceSdlRectT SSdlSoftStretchSdlSurfaceSdlRectSdlSurfaceSdlRectT = __LoadFunction<SdlSoftStretchSdlSurfaceSdlRectSdlSurfaceSdlRectT>("SDL_SoftStretch");
        public static unsafe int SDL_SoftStretch(IntPtr src, SdlRect* srcrect, IntPtr dst, SdlRect* dstrect) => SSdlSoftStretchSdlSurfaceSdlRectSdlSurfaceSdlRectT(src, srcrect, dst, dstrect);

        private delegate IntPtr SdlConvertSurfaceIntPtrIntPtrUInt32T(IntPtr src, IntPtr fmt, UInt32 flags);
        private static readonly SdlConvertSurfaceIntPtrIntPtrUInt32T SSdlConvertSurfaceIntPtrIntPtrUInt32T = __LoadFunction<SdlConvertSurfaceIntPtrIntPtrUInt32T>("SDL_ConvertSurface");
        public static IntPtr SDL_ConvertSurface(IntPtr src, IntPtr fmt, UInt32 flags) => SSdlConvertSurfaceIntPtrIntPtrUInt32T(src, fmt, flags);

        private delegate IntPtr SdlConvertSurfaceFormatIntPtrUInt32UInt32T(IntPtr src, UInt32 fmt, UInt32 flags);
        private static readonly SdlConvertSurfaceFormatIntPtrUInt32UInt32T SSdlConvertSurfaceFormatIntPtrUInt32UInt32T = __LoadFunction<SdlConvertSurfaceFormatIntPtrUInt32UInt32T>("SDL_ConvertSurfaceFormat");
        public static IntPtr SDL_ConvertSurfaceFormat(IntPtr src, UInt32 fmt, UInt32 flags) => SSdlConvertSurfaceFormatIntPtrUInt32UInt32T(src, fmt, flags);

        private delegate int SdlUpperBlitIntPtrIntPtr(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);
        private static readonly SdlUpperBlitIntPtrIntPtr SSdlUpperBlit = __LoadFunction<SdlUpperBlitIntPtrIntPtr>("SDL_UpperBlit");
        public static int SdlUpperBlit(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect) => SSdlUpperBlit(src, srcrect, dst, dstrect);
        public static int SdlBlitSurface(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect) => SdlUpperBlit(src, srcrect, dst, dstrect);
        
        private static T __LoadFunction<T>(string name) { return Internal.LoaderSdl2.LoadFunction<T>(name); }

#pragma warning disable
        static SdlSurfaceBlit()
        {
            var blitScaled = new[] { "SDL_BlitScaled", "SDL_UpperBlitScaled" };
            foreach (var name in blitScaled)
            {
                try
                {
                    SSdlBlitScaledIntPtrSdlRectIntPtrSdlRectT =
                        __LoadFunction<SdlBlitScaledIntPtrSdlRectIntPtrSdlRectT>(name);
                    break;
                }
                catch (Exception ex) { }
            }
            if (SSdlBlitScaledIntPtrSdlRectIntPtrSdlRectT == null)
                throw new EntryPointNotFoundException("Could not find SDL_BlitScaled function");
        }
#pragma warning enable
    }
}
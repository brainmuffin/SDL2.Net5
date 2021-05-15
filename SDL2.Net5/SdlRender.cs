using System;
using System.Runtime.InteropServices;
using static SDL2.Net5.SdlSurfaceBlit;
using static SDL2.Net5.SdlPointRect;

using SDL_bool = System.Int32;

namespace SDL2.Net5
{
    public static class SdlRender
    {
        [Flags]
        public enum SDL_RendererFlags : UInt32
        {
            SDL_RENDERER_SOFTWARE = 0x00000001,         /**< The renderer is a software fallback */
            SDL_RENDERER_ACCELERATED = 0x00000002,      /**< The renderer uses hardware acceleration */
            SDL_RENDERER_PRESENTVSYNC = 0x00000004,     /**< Present is synchronized with the refresh rate */
            SDL_RENDERER_TARGETTEXTURE = 0x00000008     /**< The renderer supports rendering to texture */
        }
        public enum SDL_TextureAccess
        {
            SDL_TEXTUREACCESS_STATIC,    /**< Changes rarely, not lockable */
            SDL_TEXTUREACCESS_STREAMING, /**< Changes frequently, lockable */
            SDL_TEXTUREACCESS_TARGET     /**< Texture can be used as a render target */
        }
        public enum SDL_TextureModulate
        {
            SDL_TEXTUREMODULATE_NONE = 0x00000000,     /**< No modulation */
            SDL_TEXTUREMODULATE_COLOR = 0x00000001,    /**< srcC = srcC * color */
            SDL_TEXTUREMODULATE_ALPHA = 0x00000002     /**< srcA = srcA * alpha */
        }
        public enum SDL_RendererFlip
        {
            SDL_FLIP_NONE = 0x00000000,     /**< Do not flip */
            SDL_FLIP_HORIZONTAL = 0x00000001,    /**< flip horizontally */
            SDL_FLIP_VERTICAL = 0x00000002     /**< flip vertically */
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_RendererInfo
        {
            public IntPtr name;           /**< The name of the renderer */
            public SDL_RendererFlags flags;        /**< Supported ::SDL_RendererFlags */
            public UInt32 num_texture_formats; /**< The number of available texture formats */
            public unsafe fixed UInt32 texture_formats[16]; /**< The available texture formats */
            public int max_texture_width;      /**< The maximimum texture width */
            public int max_texture_height;     /**< The maximimum texture height */
        }

        private delegate int SdlGetNumRenderDriversT();
        private static readonly SdlGetNumRenderDriversT SSdlGetNumRenderDriversT = __LoadFunction<SdlGetNumRenderDriversT>("SDL_GetNumRenderDrivers");
        public static int SDL_GetNumRenderDrivers() => SSdlGetNumRenderDriversT();

        private delegate int SdlGetRenderDriverInfoIntIntPtrT(int index, out SDL_RendererInfo info);
        private static readonly SdlGetRenderDriverInfoIntIntPtrT SSdlGetRenderDriverInfoIntIntPtrT = __LoadFunction<SdlGetRenderDriverInfoIntIntPtrT>("SDL_GetRenderDriverInfo");
        public static int SDL_GetRenderDriverInfo(int index, out SDL_RendererInfo info) => SSdlGetRenderDriverInfoIntIntPtrT(index, out info);

        private delegate int SdlCreateWindowAndRendererIntIntUInt32IntPtrIntPtrT(int width, int height, UInt32 window_flags, out IntPtr window, out IntPtr renderer);
        private static readonly SdlCreateWindowAndRendererIntIntUInt32IntPtrIntPtrT SSdlCreateWindowAndRendererIntIntUInt32IntPtrIntPtrT = __LoadFunction<SdlCreateWindowAndRendererIntIntUInt32IntPtrIntPtrT>("SDL_CreateWindowAndRenderer");
        public static int SDL_CreateWindowAndRenderer(int width, int height, UInt32 window_flags, out IntPtr window, out IntPtr renderer) => SSdlCreateWindowAndRendererIntIntUInt32IntPtrIntPtrT(width, height, window_flags, out window, out renderer);

        private delegate IntPtr SdlCreateRendererIntPtrIntUInt32T(IntPtr window, int index, UInt32 flags);
        private static readonly SdlCreateRendererIntPtrIntUInt32T SSdlCreateRendererIntPtrIntUInt32T = __LoadFunction<SdlCreateRendererIntPtrIntUInt32T>("SDL_CreateRenderer");
        public static IntPtr SDL_CreateRenderer(IntPtr window, int index, UInt32 flags) => SSdlCreateRendererIntPtrIntUInt32T(window, index, flags);

        private delegate IntPtr SdlCreateSoftwareRendererSdlSurfaceT(ref SdlSurface surface);
        private static readonly SdlCreateSoftwareRendererSdlSurfaceT SSdlCreateSoftwareRendererSdlSurfaceT = __LoadFunction<SdlCreateSoftwareRendererSdlSurfaceT>("SDL_CreateSoftwareRenderer");
        public static IntPtr SDL_CreateSoftwareRenderer(ref SdlSurface surface) => SSdlCreateSoftwareRendererSdlSurfaceT(ref surface);

        private delegate IntPtr SdlGetRendererIntPtrT(IntPtr window);
        private static readonly SdlGetRendererIntPtrT SSdlGetRendererIntPtrT = __LoadFunction<SdlGetRendererIntPtrT>("SDL_GetRenderer");
        public static IntPtr SDL_GetRenderer(IntPtr window) => SSdlGetRendererIntPtrT(window);

        private delegate int SdlGetRendererInfoIntPtrIntPtrT(IntPtr renderer, ref SDL_RendererInfo info);
        private static readonly SdlGetRendererInfoIntPtrIntPtrT SSdlGetRendererInfoIntPtrIntPtrT = __LoadFunction<SdlGetRendererInfoIntPtrIntPtrT>("SDL_GetRendererInfo");
        public static int SDL_GetRendererInfo(IntPtr renderer, ref SDL_RendererInfo info) => SSdlGetRendererInfoIntPtrIntPtrT(renderer, ref info);

        private delegate int SdlGetRendererOutputSizeIntPtrIntIntT(IntPtr renderer, out int w, out int h);
        private static readonly SdlGetRendererOutputSizeIntPtrIntIntT SSdlGetRendererOutputSizeIntPtrIntIntT = __LoadFunction<SdlGetRendererOutputSizeIntPtrIntIntT>("SDL_GetRendererOutputSize");
        public static int SDL_GetRendererOutputSize(IntPtr renderer, out int w, out int h) => SSdlGetRendererOutputSizeIntPtrIntIntT(renderer, out w, out h);

        private delegate IntPtr SdlCreateTextureIntPtrUInt32IntIntIntT(IntPtr renderer, UInt32 format, int access, int w, int h);
        private static readonly SdlCreateTextureIntPtrUInt32IntIntIntT SSdlCreateTextureIntPtrUInt32IntIntIntT = __LoadFunction<SdlCreateTextureIntPtrUInt32IntIntIntT>("SDL_CreateTexture");
        public static IntPtr SDL_CreateTexture(IntPtr renderer, UInt32 format, int access, int w, int h) => SSdlCreateTextureIntPtrUInt32IntIntIntT(renderer, format, access, w, h);

        private delegate IntPtr SdlCreateTextureFromSurfaceIntPtrSdlSurfaceT(IntPtr renderer, IntPtr surface);
        private static readonly SdlCreateTextureFromSurfaceIntPtrSdlSurfaceT SSdlCreateTextureFromSurfaceIntPtrSdlSurfaceT = __LoadFunction<SdlCreateTextureFromSurfaceIntPtrSdlSurfaceT>("SDL_CreateTextureFromSurface");
        public static IntPtr SDL_CreateTextureFromSurface(IntPtr renderer, IntPtr surface) => SSdlCreateTextureFromSurfaceIntPtrSdlSurfaceT(renderer, surface);

        private delegate int SdlQueryTextureIntPtrUintIntIntIntT(IntPtr texture, ref uint format, ref int access, ref int w, ref int h);
        private static readonly SdlQueryTextureIntPtrUintIntIntIntT SSdlQueryTextureIntPtrUintIntIntIntT = __LoadFunction<SdlQueryTextureIntPtrUintIntIntIntT>("SDL_QueryTexture");
        public static int SDL_QueryTexture(IntPtr texture, ref uint format, ref int access, ref int w, ref int h) => SSdlQueryTextureIntPtrUintIntIntIntT(texture, ref format, ref access, ref w, ref h);

        private delegate int SdlSetTextureColorModIntPtrByteByteByteT(IntPtr texture, byte r, byte g, byte b);
        private static readonly SdlSetTextureColorModIntPtrByteByteByteT SSdlSetTextureColorModIntPtrByteByteByteT = __LoadFunction<SdlSetTextureColorModIntPtrByteByteByteT>("SDL_SetTextureColorMod");
        public static int SDL_SetTextureColorMod(IntPtr texture, byte r, byte g, byte b) => SSdlSetTextureColorModIntPtrByteByteByteT(texture, r, g, b);

        private delegate int SdlGetTextureColorModIntPtrByteByteByteT(IntPtr texture, ref byte r, ref byte g, ref byte b);
        private static readonly SdlGetTextureColorModIntPtrByteByteByteT SSdlGetTextureColorModIntPtrByteByteByteT = __LoadFunction<SdlGetTextureColorModIntPtrByteByteByteT>("SDL_GetTextureColorMod");
        public static int SDL_GetTextureColorMod(IntPtr texture, ref byte r, ref byte g, ref byte b) => SSdlGetTextureColorModIntPtrByteByteByteT(texture, ref r, ref g, ref b);

        private delegate int SdlSetTextureAlphaModIntPtrByteT(IntPtr texture, byte alpha);
        private static readonly SdlSetTextureAlphaModIntPtrByteT SSdlSetTextureAlphaModIntPtrByteT = __LoadFunction<SdlSetTextureAlphaModIntPtrByteT>("SDL_SetTextureAlphaMod");
        public static int SDL_SetTextureAlphaMod(IntPtr texture, byte alpha) => SSdlSetTextureAlphaModIntPtrByteT(texture, alpha);

        private delegate int SdlGetTextureAlphaModIntPtrByteT(IntPtr texture, ref byte alpha);
        private static readonly SdlGetTextureAlphaModIntPtrByteT SSdlGetTextureAlphaModIntPtrByteT = __LoadFunction<SdlGetTextureAlphaModIntPtrByteT>("SDL_GetTextureAlphaMod");
        public static int SDL_GetTextureAlphaMod(IntPtr texture, ref byte alpha) => SSdlGetTextureAlphaModIntPtrByteT(texture, ref alpha);

        private delegate int SdlSetTextureBlendModeIntPtrSdlBlendModeT(IntPtr texture, SdlBlendmode.SdlBlendMode blendMode);
        private static readonly SdlSetTextureBlendModeIntPtrSdlBlendModeT SSdlSetTextureBlendModeIntPtrSdlBlendModeT = __LoadFunction<SdlSetTextureBlendModeIntPtrSdlBlendModeT>("SDL_SetTextureBlendMode");
        public static int SDL_SetTextureBlendMode(IntPtr texture, SdlBlendmode.SdlBlendMode blendMode) => SSdlSetTextureBlendModeIntPtrSdlBlendModeT(texture, blendMode);

        private delegate int SdlGetTextureBlendModeIntPtrSdlBlendModeT(IntPtr texture, ref SdlBlendmode.SdlBlendMode blendMode);
        private static readonly SdlGetTextureBlendModeIntPtrSdlBlendModeT SSdlGetTextureBlendModeIntPtrSdlBlendModeT = __LoadFunction<SdlGetTextureBlendModeIntPtrSdlBlendModeT>("SDL_GetTextureBlendMode");
        public static int SDL_GetTextureBlendMode(IntPtr texture, ref SdlBlendmode.SdlBlendMode blendMode) => SSdlGetTextureBlendModeIntPtrSdlBlendModeT(texture, ref blendMode);

        private delegate int SdlUpdateTextureIntPtrSdlRectIntPtrIntT(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch);
        private static readonly SdlUpdateTextureIntPtrSdlRectIntPtrIntT SSdlUpdateTextureIntPtrSdlRectIntPtrIntT = __LoadFunction<SdlUpdateTextureIntPtrSdlRectIntPtrIntT>("SDL_UpdateTexture");
        public static int SDL_UpdateTexture(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch) => SSdlUpdateTextureIntPtrSdlRectIntPtrIntT(texture, rect, pixels, pitch);

        private unsafe delegate int SdlUpdateYuvTextureIntPtrSdlRectByteIntByteIntByteIntT(IntPtr texture, SdlRect* rect, ref byte Yplane, int Ypitch, ref byte Uplane, int Upitch, ref byte Vplane, int Vpitch);
        private static readonly SdlUpdateYuvTextureIntPtrSdlRectByteIntByteIntByteIntT SSdlUpdateYuvTextureIntPtrSdlRectByteIntByteIntByteIntT = __LoadFunction<SdlUpdateYuvTextureIntPtrSdlRectByteIntByteIntByteIntT>("SDL_UpdateYUVTexture");
        public static unsafe int SDL_UpdateYUVTexture(IntPtr texture, SdlRect* rect, ref byte Yplane, int Ypitch, ref byte Uplane, int Upitch, ref byte Vplane, int Vpitch) => SSdlUpdateYuvTextureIntPtrSdlRectByteIntByteIntByteIntT(texture, rect, ref Yplane, Ypitch, ref Uplane, Upitch, ref Vplane, Vpitch);

        private delegate int SdlLockTextureIntPtrSdlRectIntPtrIntT(IntPtr texture, IntPtr rect, out IntPtr pixels, ref int pitch);
        private static readonly SdlLockTextureIntPtrSdlRectIntPtrIntT SSdlLockTextureIntPtrSdlRectIntPtrIntT = __LoadFunction<SdlLockTextureIntPtrSdlRectIntPtrIntT>("SDL_LockTexture");
        public static int SDL_LockTexture(IntPtr texture, IntPtr rect, out IntPtr pixels, ref int pitch) => SSdlLockTextureIntPtrSdlRectIntPtrIntT(texture, rect, out pixels, ref pitch);

        private delegate void SdlUnlockTextureIntPtrT(IntPtr texture);
        private static readonly SdlUnlockTextureIntPtrT SSdlUnlockTextureIntPtrT = __LoadFunction<SdlUnlockTextureIntPtrT>("SDL_UnlockTexture");
        public static void SDL_UnlockTexture(IntPtr texture) => SSdlUnlockTextureIntPtrT(texture);

        private delegate SDL_bool SdlRenderTargetSupportedIntPtrT(IntPtr renderer);
        private static readonly SdlRenderTargetSupportedIntPtrT SSdlRenderTargetSupportedIntPtrT = __LoadFunction<SdlRenderTargetSupportedIntPtrT>("SDL_RenderTargetSupported");
        public static SDL_bool SDL_RenderTargetSupported(IntPtr renderer) => SSdlRenderTargetSupportedIntPtrT(renderer);

        private delegate int SdlSetRenderTargetIntPtrIntPtrT(IntPtr renderer, IntPtr texture);
        private static readonly SdlSetRenderTargetIntPtrIntPtrT SSdlSetRenderTargetIntPtrIntPtrT = __LoadFunction<SdlSetRenderTargetIntPtrIntPtrT>("SDL_SetRenderTarget");
        public static int SDL_SetRenderTarget(IntPtr renderer, IntPtr texture) => SSdlSetRenderTargetIntPtrIntPtrT(renderer, texture);

        private delegate IntPtr SdlGetRenderTargetIntPtrT(IntPtr renderer);
        private static readonly SdlGetRenderTargetIntPtrT SSdlGetRenderTargetIntPtrT = __LoadFunction<SdlGetRenderTargetIntPtrT>("SDL_GetRenderTarget");
        public static IntPtr SDL_GetRenderTarget(IntPtr renderer) => SSdlGetRenderTargetIntPtrT(renderer);

        private delegate int SdlRenderSetLogicalSizeIntPtrIntIntT(IntPtr renderer, int w, int h);
        private static readonly SdlRenderSetLogicalSizeIntPtrIntIntT SSdlRenderSetLogicalSizeIntPtrIntIntT = __LoadFunction<SdlRenderSetLogicalSizeIntPtrIntIntT>("SDL_RenderSetLogicalSize");
        public static int SDL_RenderSetLogicalSize(IntPtr renderer, int w, int h) => SSdlRenderSetLogicalSizeIntPtrIntIntT(renderer, w, h);

        private delegate void SdlRenderGetLogicalSizeIntPtrIntIntT(IntPtr renderer, ref int w, ref int h);
        private static readonly SdlRenderGetLogicalSizeIntPtrIntIntT SSdlRenderGetLogicalSizeIntPtrIntIntT = __LoadFunction<SdlRenderGetLogicalSizeIntPtrIntIntT>("SDL_RenderGetLogicalSize");
        public static void SDL_RenderGetLogicalSize(IntPtr renderer, ref int w, ref int h) => SSdlRenderGetLogicalSizeIntPtrIntIntT(renderer, ref w, ref h);

        private unsafe delegate int SdlRenderSetViewportIntPtrSdlRectT(IntPtr renderer, SdlRect* rect);
        private static SdlRenderSetViewportIntPtrSdlRectT s_SDL_RenderSetViewport_IntPtr_SDL_Rect_t = __LoadFunction<SdlRenderSetViewportIntPtrSdlRectT>("SDL_RenderSetViewport");
        public static unsafe int SDL_RenderSetViewport(IntPtr renderer, SdlRect* rect) => s_SDL_RenderSetViewport_IntPtr_SDL_Rect_t(renderer, rect);

        private unsafe delegate void SdlRenderGetViewportIntPtrSdlRectT(IntPtr renderer, SdlRect* rect);
        private static readonly SdlRenderGetViewportIntPtrSdlRectT SSdlRenderGetViewportIntPtrSdlRectT = __LoadFunction<SdlRenderGetViewportIntPtrSdlRectT>("SDL_RenderGetViewport");
        public static unsafe void SDL_RenderGetViewport(IntPtr renderer, SdlRect* rect) => SSdlRenderGetViewportIntPtrSdlRectT(renderer, rect);

        private unsafe delegate int SdlRenderSetClipRectIntPtrSdlRectT(IntPtr renderer, SdlRect* rect);
        private static readonly SdlRenderSetClipRectIntPtrSdlRectT SSdlRenderSetClipRectIntPtrSdlRectT = __LoadFunction<SdlRenderSetClipRectIntPtrSdlRectT>("SDL_RenderSetClipRect");
        public static unsafe int SDL_RenderSetClipRect(IntPtr renderer, SdlRect* rect) => SSdlRenderSetClipRectIntPtrSdlRectT(renderer, rect);

        private unsafe delegate void SdlRenderGetClipRectIntPtrSdlRectT(IntPtr renderer, SdlRect* rect);
        private static readonly SdlRenderGetClipRectIntPtrSdlRectT SSdlRenderGetClipRectIntPtrSdlRectT = __LoadFunction<SdlRenderGetClipRectIntPtrSdlRectT>("SDL_RenderGetClipRect");
        public static unsafe void SDL_RenderGetClipRect(IntPtr renderer, SdlRect* rect) => SSdlRenderGetClipRectIntPtrSdlRectT(renderer, rect);

        private delegate int SdlRenderSetScaleIntPtrFloatFloatT(IntPtr renderer, float scaleX, float scaleY);
        private static readonly SdlRenderSetScaleIntPtrFloatFloatT SSdlRenderSetScaleIntPtrFloatFloatT = __LoadFunction<SdlRenderSetScaleIntPtrFloatFloatT>("SDL_RenderSetScale");
        public static int SDL_RenderSetScale(IntPtr renderer, float scaleX, float scaleY) => SSdlRenderSetScaleIntPtrFloatFloatT(renderer, scaleX, scaleY);

        private delegate void SdlRenderGetScaleIntPtrFloatFloatT(IntPtr renderer, ref float scaleX, ref float scaleY);
        private static readonly SdlRenderGetScaleIntPtrFloatFloatT SSdlRenderGetScaleIntPtrFloatFloatT = __LoadFunction<SdlRenderGetScaleIntPtrFloatFloatT>("SDL_RenderGetScale");
        public static void SDL_RenderGetScale(IntPtr renderer, ref float scaleX, ref float scaleY) => SSdlRenderGetScaleIntPtrFloatFloatT(renderer, ref scaleX, ref scaleY);

        private delegate int SdlSetRenderDrawColorIntPtrByteByteByteByteT(IntPtr renderer, byte r, byte g, byte b, byte a);
        private static readonly SdlSetRenderDrawColorIntPtrByteByteByteByteT SSdlSetRenderDrawColorIntPtrByteByteByteByteT = __LoadFunction<SdlSetRenderDrawColorIntPtrByteByteByteByteT>("SDL_SetRenderDrawColor");
        public static int SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a) => SSdlSetRenderDrawColorIntPtrByteByteByteByteT(renderer, r, g, b, a);

        private delegate int SdlGetRenderDrawColorIntPtrByteByteByteByteT(IntPtr renderer, ref byte r, ref byte g, ref byte b, ref byte a);
        private static readonly SdlGetRenderDrawColorIntPtrByteByteByteByteT SSdlGetRenderDrawColorIntPtrByteByteByteByteT = __LoadFunction<SdlGetRenderDrawColorIntPtrByteByteByteByteT>("SDL_GetRenderDrawColor");

        public static int SDL_GetRenderDrawColor(IntPtr renderer, ref byte r, ref byte g, ref byte b, ref byte a) => SSdlGetRenderDrawColorIntPtrByteByteByteByteT(renderer, ref r, ref g, ref b, ref a);

        private delegate int SdlSetRenderDrawBlendModeIntPtrSdlBlendModeT(IntPtr renderer, SdlBlendmode.SdlBlendMode blendMode);
        private static readonly SdlSetRenderDrawBlendModeIntPtrSdlBlendModeT SSdlSetRenderDrawBlendModeIntPtrSdlBlendModeT = __LoadFunction<SdlSetRenderDrawBlendModeIntPtrSdlBlendModeT>("SDL_SetRenderDrawBlendMode");
        public static int SDL_SetRenderDrawBlendMode(IntPtr renderer, SdlBlendmode.SdlBlendMode blendMode) => SSdlSetRenderDrawBlendModeIntPtrSdlBlendModeT(renderer, blendMode);

        private delegate int SdlGetRenderDrawBlendModeIntPtrSdlBlendModeT(IntPtr renderer, ref SdlBlendmode.SdlBlendMode blendMode);
        private static readonly SdlGetRenderDrawBlendModeIntPtrSdlBlendModeT SSdlGetRenderDrawBlendModeIntPtrSdlBlendModeT = __LoadFunction<SdlGetRenderDrawBlendModeIntPtrSdlBlendModeT>("SDL_GetRenderDrawBlendMode");
        public static int SDL_GetRenderDrawBlendMode(IntPtr renderer, ref SdlBlendmode.SdlBlendMode blendMode) => SSdlGetRenderDrawBlendModeIntPtrSdlBlendModeT(renderer, ref blendMode);

        private delegate int SdlRenderClearIntPtrT(IntPtr renderer);
        private static readonly SdlRenderClearIntPtrT SSdlRenderClearIntPtrT = __LoadFunction<SdlRenderClearIntPtrT>("SDL_RenderClear");
        public static int SDL_RenderClear(IntPtr renderer) => SSdlRenderClearIntPtrT(renderer);

        private delegate int SdlRenderDrawPointIntPtrIntIntT(IntPtr renderer, int x, int y);
        private static readonly SdlRenderDrawPointIntPtrIntIntT SSdlRenderDrawPointIntPtrIntIntT = __LoadFunction<SdlRenderDrawPointIntPtrIntIntT>("SDL_RenderDrawPoint");
        public static int SDL_RenderDrawPoint(IntPtr renderer, int x, int y) => SSdlRenderDrawPointIntPtrIntIntT(renderer, x, y);

        private unsafe delegate int SdlRenderDrawPointsIntPtrSdlPointIntT(IntPtr renderer, SdlPoint* points, int count);
        private static readonly SdlRenderDrawPointsIntPtrSdlPointIntT SSdlRenderDrawPointsIntPtrSdlPointIntT = __LoadFunction<SdlRenderDrawPointsIntPtrSdlPointIntT>("SDL_RenderDrawPoints");
        public static unsafe int SDL_RenderDrawPoints(IntPtr renderer, SdlPoint* points, int count) => SSdlRenderDrawPointsIntPtrSdlPointIntT(renderer, points, count);

        private delegate int SdlRenderDrawLineIntPtrIntIntIntIntT(IntPtr renderer, int x1, int y1, int x2, int y2);
        private static readonly SdlRenderDrawLineIntPtrIntIntIntIntT SSdlRenderDrawLineIntPtrIntIntIntIntT = __LoadFunction<SdlRenderDrawLineIntPtrIntIntIntIntT>("SDL_RenderDrawLine");
        public static int SDL_RenderDrawLine(IntPtr renderer, int x1, int y1, int x2, int y2) => SSdlRenderDrawLineIntPtrIntIntIntIntT(renderer, x1, y1, x2, y2);

        private unsafe delegate int SdlRenderDrawLinesIntPtrSdlPointIntT(IntPtr renderer, SdlPoint* points, int count);
        private static readonly SdlRenderDrawLinesIntPtrSdlPointIntT SSdlRenderDrawLinesIntPtrSdlPointIntT = __LoadFunction<SdlRenderDrawLinesIntPtrSdlPointIntT>("SDL_RenderDrawLines");
        public static unsafe int SDL_RenderDrawLines(IntPtr renderer, SdlPoint* points, int count) => SSdlRenderDrawLinesIntPtrSdlPointIntT(renderer, points, count);

        private unsafe delegate int SdlRenderDrawRectIntPtrSdlRectT(IntPtr renderer, SdlRect* rect);
        private static readonly SdlRenderDrawRectIntPtrSdlRectT SSdlRenderDrawRectIntPtrSdlRectT = __LoadFunction<SdlRenderDrawRectIntPtrSdlRectT>("SDL_RenderDrawRect");
        public static unsafe int SDL_RenderDrawRect(IntPtr renderer, SdlRect* rect) => SSdlRenderDrawRectIntPtrSdlRectT(renderer, rect);

        private unsafe delegate int SdlRenderDrawRectsIntPtrSdlRectIntT(IntPtr renderer, SdlRect* rects, int count);
        private static readonly SdlRenderDrawRectsIntPtrSdlRectIntT SSdlRenderDrawRectsIntPtrSdlRectIntT = __LoadFunction<SdlRenderDrawRectsIntPtrSdlRectIntT>("SDL_RenderDrawRects");
        public static unsafe int SDL_RenderDrawRects(IntPtr renderer, SdlRect* rects, int count) => SSdlRenderDrawRectsIntPtrSdlRectIntT(renderer, rects, count);

        private unsafe delegate int SdlRenderFillRectIntPtrSdlRectT(IntPtr renderer, SdlRect* rect);
        private static readonly SdlRenderFillRectIntPtrSdlRectT SSdlRenderFillRectIntPtrSdlRectT = __LoadFunction<SdlRenderFillRectIntPtrSdlRectT>("SDL_RenderFillRect");
        public static unsafe int SDL_RenderFillRect(IntPtr renderer, SdlRect* rect) => SSdlRenderFillRectIntPtrSdlRectT(renderer, rect);

        private unsafe delegate int SdlRenderFillRectsIntPtrSdlRectIntT(IntPtr renderer, SdlRect* rects, int count);
        private static readonly SdlRenderFillRectsIntPtrSdlRectIntT SSdlRenderFillRectsIntPtrSdlRectIntT = __LoadFunction<SdlRenderFillRectsIntPtrSdlRectIntT>("SDL_RenderFillRects");
        public static unsafe int SDL_RenderFillRects(IntPtr renderer, SdlRect* rects, int count) => SSdlRenderFillRectsIntPtrSdlRectIntT(renderer, rects, count);

        private unsafe delegate int SdlRenderCopyIntPtrIntPtrSdlRectSdlRectT(IntPtr renderer, IntPtr texture, SdlRect* srcrect, SdlRect* dstrect);
        private static readonly SdlRenderCopyIntPtrIntPtrSdlRectSdlRectT SSdlRenderCopyIntPtrIntPtrSdlRectSdlRectT = __LoadFunction<SdlRenderCopyIntPtrIntPtrSdlRectSdlRectT>("SDL_RenderCopy");
        public static unsafe int SDL_RenderCopy(IntPtr renderer, IntPtr texture, SdlRect* srcrect, SdlRect* dstrect) => SSdlRenderCopyIntPtrIntPtrSdlRectSdlRectT(renderer, texture, srcrect, dstrect);

        private unsafe delegate int SdlRenderCopyExIntPtrIntPtrSdlRectSdlRectDoubleSdlPointSdlRendererFlipT(IntPtr renderer, IntPtr texture, SdlRect* srcrect, SdlRect* dstrect, double angle, SdlPoint* center, SDL_RendererFlip flip);
        private static readonly SdlRenderCopyExIntPtrIntPtrSdlRectSdlRectDoubleSdlPointSdlRendererFlipT SSdlRenderCopyExIntPtrIntPtrSdlRectSdlRectDoubleSdlPointSdlRendererFlipT = __LoadFunction<SdlRenderCopyExIntPtrIntPtrSdlRectSdlRectDoubleSdlPointSdlRendererFlipT>("SDL_RenderCopyEx");
        public static unsafe int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, SdlRect* srcrect, SdlRect* dstrect, double angle, SdlPoint* center, SDL_RendererFlip flip) => SSdlRenderCopyExIntPtrIntPtrSdlRectSdlRectDoubleSdlPointSdlRendererFlipT(renderer, texture, srcrect, dstrect, angle, center, flip);

        private delegate int SdlRenderReadPixelsIntPtrSdlRectUInt32IntPtrIntT(IntPtr renderer, IntPtr rect, UInt32 format, IntPtr pixels, int pitch);
        private static readonly SdlRenderReadPixelsIntPtrSdlRectUInt32IntPtrIntT SSdlRenderReadPixelsIntPtrSdlRectUInt32IntPtrIntT = __LoadFunction<SdlRenderReadPixelsIntPtrSdlRectUInt32IntPtrIntT>("SDL_RenderReadPixels");
        public static int SDL_RenderReadPixels(IntPtr renderer, IntPtr rect, UInt32 format, IntPtr pixels, int pitch) => SSdlRenderReadPixelsIntPtrSdlRectUInt32IntPtrIntT(renderer, rect, format, pixels, pitch);

        private delegate void SdlRenderPresentIntPtrT(IntPtr renderer);
        private static readonly SdlRenderPresentIntPtrT SSdlRenderPresentIntPtrT = __LoadFunction<SdlRenderPresentIntPtrT>("SDL_RenderPresent");
        public static void SDL_RenderPresent(IntPtr renderer) => SSdlRenderPresentIntPtrT(renderer);

        private delegate void SdlDestroyTextureIntPtrT(IntPtr texture);
        private static readonly SdlDestroyTextureIntPtrT SSdlDestroyTextureIntPtrT = __LoadFunction<SdlDestroyTextureIntPtrT>("SDL_DestroyTexture");
        public static void SDL_DestroyTexture(IntPtr texture) => SSdlDestroyTextureIntPtrT(texture);

        private delegate void SdlDestroyRendererIntPtrT(IntPtr renderer);
        private static readonly SdlDestroyRendererIntPtrT SSdlDestroyRendererIntPtrT = __LoadFunction<SdlDestroyRendererIntPtrT>("SDL_DestroyRenderer");
        public static void SDL_DestroyRenderer(IntPtr renderer) => SSdlDestroyRendererIntPtrT(renderer);

        private delegate int SdlGlBindTextureIntPtrFloatFloatT(IntPtr texture, ref float texw, ref float texh);
        private static readonly SdlGlBindTextureIntPtrFloatFloatT SSdlGlBindTextureIntPtrFloatFloatT = __LoadFunction<SdlGlBindTextureIntPtrFloatFloatT>("SDL_GL_BindTexture");
        public static int SDL_GL_BindTexture(IntPtr texture, ref float texw, ref float texh) => SSdlGlBindTextureIntPtrFloatFloatT(texture, ref texw, ref texh);

        private delegate int SdlGlUnbindTextureIntPtrT(IntPtr texture);
        private static readonly SdlGlUnbindTextureIntPtrT SSdlGlUnbindTextureIntPtrT = __LoadFunction<SdlGlUnbindTextureIntPtrT>("SDL_GL_UnbindTexture");
        public static int SDL_GL_UnbindTexture(IntPtr texture) => SSdlGlUnbindTextureIntPtrT(texture);
        
        private static T __LoadFunction<T>(string name) { return Internal.LoaderSdl2.LoadFunction<T>(name); }
    }
}
using System;
using System.Runtime.InteropServices;
using SDL2.Net5.Internal;

using SDL_bool = System.Int32;
using SDL_GLContext = System.IntPtr;

namespace SDL2.Net5
{
    public static class SdlVideo
    {
        public const int SDL_WINDOWPOS_UNDEFINED_MASK = 0x1FFF0000;
        public const int SDL_WINDOWPOS_CENTERED_MASK = 0x2FFF0000;
        public static int SDL_WINDOWPOS_UNDEFINED_DISPLAY(int displayIndex) =>
            SDL_WINDOWPOS_UNDEFINED_MASK | displayIndex;
        public static int SDL_WINDOWPOS_CENTERED_DISPLAY(int displayIndex) =>
            SDL_WINDOWPOS_CENTERED_MASK | displayIndex;
        
        [Flags]
        public enum SDL_WindowFlags
        {
            SDL_WINDOW_FULLSCREEN = 0x00000001,         /** fullscreen window */
            SDL_WINDOW_OPENGL = 0x00000002,             /** window usable with OpenGL context */
            SDL_WINDOW_SHOWN = 0x00000004,              /** window is visible */
            SDL_WINDOW_HIDDEN = 0x00000008,             /** window is not visible */
            SDL_WINDOW_BORDERLESS = 0x00000010,         /** no window decoration */
            SDL_WINDOW_RESIZABLE = 0x00000020,          /** window can be resized */
            SDL_WINDOW_MINIMIZED = 0x00000040,          /** window is minimized */
            SDL_WINDOW_MAXIMIZED = 0x00000080,          /** window is maximized */
            SDL_WINDOW_INPUT_GRABBED = 0x00000100,      /** window has grabbed input focus */
            SDL_WINDOW_INPUT_FOCUS = 0x00000200,        /** window has input focus */
            SDL_WINDOW_MOUSE_FOCUS = 0x00000400,        /** window has mouse focus */
            SDL_WINDOW_FULLSCREEN_DESKTOP = (SDL_WINDOW_FULLSCREEN | 0x00001000),
            SDL_WINDOW_FOREIGN = 0x00000800,            /** window not created by SDL */
            SDL_WINDOW_ALLOW_HIGHDPI = 0x00002000       /** window should be created in high-DPI mode if supported */
        }
        public enum SDL_WindowEventID
        {
            SDL_WINDOWEVENT_NONE,           /** Never used */
            SDL_WINDOWEVENT_SHOWN,          /** Window has been shown */
            SDL_WINDOWEVENT_HIDDEN,         /** Window has been hidden */
            SDL_WINDOWEVENT_EXPOSED,        /** Window has been exposed and should be redrawn */
            SDL_WINDOWEVENT_MOVED,          /** Window has been moved to data1, data2 */
            SDL_WINDOWEVENT_RESIZED,        /** Window has been resized to data1xdata2 */
            SDL_WINDOWEVENT_SIZE_CHANGED,   /** The window size has changed, either as a result of an API call or through the system or user changing the window size. */
            SDL_WINDOWEVENT_MINIMIZED,      /** Window has been minimized */
            SDL_WINDOWEVENT_MAXIMIZED,      /** Window has been maximized */
            SDL_WINDOWEVENT_RESTORED,       /** Window has been restored to normal size and position */
            SDL_WINDOWEVENT_ENTER,          /** Window has gained mouse focus */
            SDL_WINDOWEVENT_LEAVE,          /** Window has lost mouse focus */
            SDL_WINDOWEVENT_FOCUS_GAINED,   /** Window has gained keyboard focus */
            SDL_WINDOWEVENT_FOCUS_LOST,     /** Window has lost keyboard focus */
            SDL_WINDOWEVENT_CLOSE           /** The window manager requests that the window be closed */
        }
        public enum SDL_GLattr
        {
            SDL_GL_RED_SIZE,
            SDL_GL_GREEN_SIZE,
            SDL_GL_BLUE_SIZE,
            SDL_GL_ALPHA_SIZE,
            SDL_GL_BUFFER_SIZE,
            SDL_GL_DOUBLEBUFFER,
            SDL_GL_DEPTH_SIZE,
            SDL_GL_STENCIL_SIZE,
            SDL_GL_ACCUM_RED_SIZE,
            SDL_GL_ACCUM_GREEN_SIZE,
            SDL_GL_ACCUM_BLUE_SIZE,
            SDL_GL_ACCUM_ALPHA_SIZE,
            SDL_GL_STEREO,
            SDL_GL_MULTISAMPLEBUFFERS,
            SDL_GL_MULTISAMPLESAMPLES,
            SDL_GL_ACCELERATED_VISUAL,
            SDL_GL_RETAINED_BACKING,
            SDL_GL_CONTEXT_MAJOR_VERSION,
            SDL_GL_CONTEXT_MINOR_VERSION,
            SDL_GL_CONTEXT_EGL,
            SDL_GL_CONTEXT_FLAGS,
            SDL_GL_CONTEXT_PROFILE_MASK,
            SDL_GL_SHARE_WITH_CURRENT_CONTEXT,
            SDL_GL_FRAMEBUFFER_SRGB_CAPABLE
        }
        public enum SDL_GLprofile
        {
            SDL_GL_CONTEXT_PROFILE_CORE = 0x0001,
            SDL_GL_CONTEXT_PROFILE_COMPATIBILITY = 0x0002,
            SDL_GL_CONTEXT_PROFILE_ES = 0x0004 /* GLX_CONTEXT_ES2_PROFILE_BIT_EXT */
        }
        public enum SDL_GLcontextFlag
        {
            SDL_GL_CONTEXT_DEBUG_FLAG = 0x0001,
            SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG = 0x0002,
            SDL_GL_CONTEXT_ROBUST_ACCESS_FLAG = 0x0004,
            SDL_GL_CONTEXT_RESET_ISOLATION_FLAG = 0x0008
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_DisplayMode
        {
            public UInt32 format;              /** pixel format */
            public int w;                      /** width */
            public int h;                      /** height */
            public int refreshRate;           /** refresh rate (or zero for unspecified) */
            public IntPtr driverData;           /** driver-specific data, initialize to 0 */

            public override string ToString() => $"{w}x{h} - {refreshRate} Hz";

            public override bool Equals(object obj) => obj is SDL_DisplayMode && this == (SDL_DisplayMode)obj;
            public override int GetHashCode()
            {
                var hash = 17;
                hash = hash * 23 + format.GetHashCode();
                hash = hash * 23 + w.GetHashCode();
                hash = hash * 23 + h.GetHashCode();
                hash = hash * 23 + refreshRate.GetHashCode();
                return hash;
            }
            public static bool operator ==(SDL_DisplayMode one, SDL_DisplayMode two) =>
                one.format == two.format &&
                    one.w == two.w &&
                    one.h == two.h &&
                    one.refreshRate == two.refreshRate;
            public static bool operator !=(SDL_DisplayMode one, SDL_DisplayMode two) => !(one == two);
        }

        private delegate int SdlGetNumVideoDriversT();
        private static readonly SdlGetNumVideoDriversT SSdlGetNumVideoDriversT = __LoadFunction<SdlGetNumVideoDriversT>("SDL_GetNumVideoDrivers");
        public static int SDL_GetNumVideoDrivers() => SSdlGetNumVideoDriversT();

        private delegate IntPtr SdlGetVideoDriverIntT(int index);
        private static readonly SdlGetVideoDriverIntT SSdlGetVideoDriverIntT = __LoadFunction<SdlGetVideoDriverIntT>("SDL_GetVideoDriver");
        public static IntPtr SDL_GetVideoDriver(int index) => SSdlGetVideoDriverIntT(index);

        private delegate int SdlVideoInitIntPtrT(IntPtr driverName);
        private static readonly SdlVideoInitIntPtrT SSdlVideoInitIntPtrT = __LoadFunction<SdlVideoInitIntPtrT>("SDL_VideoInit");
        public static int SDL_VideoInit(IntPtr driverName) => SSdlVideoInitIntPtrT(driverName);

        private delegate void SdlVideoQuitT();
        private static readonly SdlVideoQuitT SSdlVideoQuitT = __LoadFunction<SdlVideoQuitT>("SDL_VideoQuit");
        public static void SDL_VideoQuit() => SSdlVideoQuitT();

        private delegate IntPtr SdlGetCurrentVideoDriverT();
        private static readonly SdlGetCurrentVideoDriverT SSdlGetCurrentVideoDriverT = __LoadFunction<SdlGetCurrentVideoDriverT>("SDL_GetCurrentVideoDriver");
        public static IntPtr SDL_GetCurrentVideoDriver() => SSdlGetCurrentVideoDriverT();

        private delegate int SdlGetNumVideoDisplaysT();
        private static readonly SdlGetNumVideoDisplaysT SSdlGetNumVideoDisplaysT = __LoadFunction<SdlGetNumVideoDisplaysT>("SDL_GetNumVideoDisplays");
        public static int SDL_GetNumVideoDisplays() => SSdlGetNumVideoDisplaysT();

        private delegate IntPtr SdlGetDisplayNameIntT(int displayIndex);
        private static readonly SdlGetDisplayNameIntT SSdlGetDisplayNameIntT = __LoadFunction<SdlGetDisplayNameIntT>("SDL_GetDisplayName");
        private static IntPtr _SDL_GetDisplayName(int displayIndex) => SSdlGetDisplayNameIntT(displayIndex);
        public static string SDL_GetDisplayName(int displayIndex) => Util.PtrToStringUTF8(_SDL_GetDisplayName(displayIndex));

        private delegate int SdlGetDisplayBoundsIntSdlRectT(int displayIndex, ref SdlPointRect.SdlRect rect);
        private static readonly SdlGetDisplayBoundsIntSdlRectT SSdlGetDisplayBoundsIntSdlRectT = __LoadFunction<SdlGetDisplayBoundsIntSdlRectT>("SDL_GetDisplayBounds");
        public static int SDL_GetDisplayBounds(int displayIndex, ref SdlPointRect.SdlRect rect) => SSdlGetDisplayBoundsIntSdlRectT(displayIndex, ref rect);

        private delegate int SdlGetNumDisplayModesIntT(int displayIndex);
        private static readonly SdlGetNumDisplayModesIntT SSdlGetNumDisplayModesIntT = __LoadFunction<SdlGetNumDisplayModesIntT>("SDL_GetNumDisplayModes");
        public static int SDL_GetNumDisplayModes(int displayIndex) => SSdlGetNumDisplayModesIntT(displayIndex);

        private delegate int SdlGetDisplayModeIntIntSdlDisplayModeT(int displayIndex, int modeIndex, ref SDL_DisplayMode mode);
        private static readonly SdlGetDisplayModeIntIntSdlDisplayModeT SSdlGetDisplayModeIntIntSdlDisplayModeT = __LoadFunction<SdlGetDisplayModeIntIntSdlDisplayModeT>("SDL_GetDisplayMode");
        public static int SDL_GetDisplayMode(int displayIndex, int modeIndex, ref SDL_DisplayMode mode) => SSdlGetDisplayModeIntIntSdlDisplayModeT(displayIndex, modeIndex, ref mode);

        private delegate int SdlGetDesktopDisplayModeIntSdlDisplayModeT(int displayIndex, ref SDL_DisplayMode mode);
        private static readonly SdlGetDesktopDisplayModeIntSdlDisplayModeT SSdlGetDesktopDisplayModeIntSdlDisplayModeT = __LoadFunction<SdlGetDesktopDisplayModeIntSdlDisplayModeT>("SDL_GetDesktopDisplayMode");
        public static int SDL_GetDesktopDisplayMode(int displayIndex, ref SDL_DisplayMode mode) => SSdlGetDesktopDisplayModeIntSdlDisplayModeT(displayIndex, ref mode);

        private delegate int SdlGetCurrentDisplayModeIntSdlDisplayModeT(int displayIndex, ref SDL_DisplayMode mode);
        private static readonly SdlGetCurrentDisplayModeIntSdlDisplayModeT SSdlGetCurrentDisplayModeIntSdlDisplayModeT = __LoadFunction<SdlGetCurrentDisplayModeIntSdlDisplayModeT>("SDL_GetCurrentDisplayMode");
        public static int SDL_GetCurrentDisplayMode(int displayIndex, ref SDL_DisplayMode mode) => SSdlGetCurrentDisplayModeIntSdlDisplayModeT(displayIndex, ref mode);

        private delegate IntPtr SdlGetClosestDisplayModeIntSdlDisplayModeSdlDisplayModeT(int displayIndex, ref SDL_DisplayMode mode, ref SDL_DisplayMode closest);
        private static readonly SdlGetClosestDisplayModeIntSdlDisplayModeSdlDisplayModeT SSdlGetClosestDisplayModeIntSdlDisplayModeSdlDisplayModeT = __LoadFunction<SdlGetClosestDisplayModeIntSdlDisplayModeSdlDisplayModeT>("SDL_GetClosestDisplayMode");
        public static IntPtr SDL_GetClosestDisplayMode(int displayIndex, ref SDL_DisplayMode mode, ref SDL_DisplayMode closest) => SSdlGetClosestDisplayModeIntSdlDisplayModeSdlDisplayModeT(displayIndex, ref mode, ref closest);

        private delegate int SdlGetWindowDisplayIndexIntPtrT(IntPtr window);
        private static readonly SdlGetWindowDisplayIndexIntPtrT SSdlGetWindowDisplayIndexIntPtrT = __LoadFunction<SdlGetWindowDisplayIndexIntPtrT>("SDL_GetWindowDisplayIndex");
        public static int SDL_GetWindowDisplayIndex(IntPtr window) => SSdlGetWindowDisplayIndexIntPtrT(window);

        private delegate int SdlSetWindowDisplayModeIntPtrSdlDisplayModeT(IntPtr window, ref SDL_DisplayMode mode);
        private static readonly SdlSetWindowDisplayModeIntPtrSdlDisplayModeT SSdlSetWindowDisplayModeIntPtrSdlDisplayModeT = __LoadFunction<SdlSetWindowDisplayModeIntPtrSdlDisplayModeT>("SDL_SetWindowDisplayMode");
        public static int SDL_SetWindowDisplayMode(IntPtr window, ref SDL_DisplayMode mode) => SSdlSetWindowDisplayModeIntPtrSdlDisplayModeT(window, ref mode);

        private delegate int SdlGetWindowDisplayModeIntPtrSdlDisplayModeT(IntPtr window, ref SDL_DisplayMode mode);
        private static readonly SdlGetWindowDisplayModeIntPtrSdlDisplayModeT SSdlGetWindowDisplayModeIntPtrSdlDisplayModeT = __LoadFunction<SdlGetWindowDisplayModeIntPtrSdlDisplayModeT>("SDL_GetWindowDisplayMode");
        public static int SDL_GetWindowDisplayMode(IntPtr window, ref SDL_DisplayMode mode) => SSdlGetWindowDisplayModeIntPtrSdlDisplayModeT(window, ref mode);

        private delegate uint SdlGetWindowPixelFormatIntPtrT(IntPtr window);
        private static readonly SdlGetWindowPixelFormatIntPtrT SSdlGetWindowPixelFormatIntPtrT = __LoadFunction<SdlGetWindowPixelFormatIntPtrT>("SDL_GetWindowPixelFormat");
        public static uint SDL_GetWindowPixelFormat(IntPtr window) => SSdlGetWindowPixelFormatIntPtrT(window);

        private delegate IntPtr SdlCreateWindow(IntPtr title, int x, int y, int w, int h, UInt32 flags);
        private static readonly SdlCreateWindow SSdlCreateWindowIntPtrIntIntIntIntUInt32T = __LoadFunction<SdlCreateWindow>("SDL_CreateWindow");
        public static IntPtr SDL_CreateWindow(string title, int x, int y, int w, int h, UInt32 flags) => SSdlCreateWindowIntPtrIntIntIntIntUInt32T(Util.StringToHGlobalUTF8(title), x, y, w, h, flags);
        
        private delegate IntPtr SdlCreateWindowFromIntPtrT(IntPtr data);
        private static readonly SdlCreateWindowFromIntPtrT SSdlCreateWindowFromIntPtrT = __LoadFunction<SdlCreateWindowFromIntPtrT>("SDL_CreateWindowFrom");
        public static IntPtr SDL_CreateWindowFrom(IntPtr data) => SSdlCreateWindowFromIntPtrT(data);

        private delegate uint SdlGetWindowId(IntPtr window);
        private static readonly SdlGetWindowId SSdlGetWindowId = __LoadFunction<SdlGetWindowId>("SDL_GetWindowID");
        public static uint SDL_GetWindowID(IntPtr window) => SSdlGetWindowId(window);

        private delegate IntPtr SdlGetWindowFromIdUInt32T(UInt32 id);
        private static readonly SdlGetWindowFromIdUInt32T SSdlGetWindowFromIdUInt32T = __LoadFunction<SdlGetWindowFromIdUInt32T>("SDL_GetWindowFromID");
        public static IntPtr SDL_GetWindowFromID(UInt32 id) => SSdlGetWindowFromIdUInt32T(id);

        private delegate uint SdlGetWindowFlagsIntPtrT(IntPtr window);
        private static readonly SdlGetWindowFlagsIntPtrT SSdlGetWindowFlagsIntPtrT = __LoadFunction<SdlGetWindowFlagsIntPtrT>("SDL_GetWindowFlags");
        public static uint SDL_GetWindowFlags(IntPtr window) => SSdlGetWindowFlagsIntPtrT(window);

        private delegate void SdlSetWindowTitleIntPtrIntPtrT(IntPtr window, IntPtr title);
        private static readonly SdlSetWindowTitleIntPtrIntPtrT SSdlSetWindowTitleIntPtrIntPtrT = __LoadFunction<SdlSetWindowTitleIntPtrIntPtrT>("SDL_SetWindowTitle");
        private static void _SDL_SetWindowTitle(IntPtr window, IntPtr title) => SSdlSetWindowTitleIntPtrIntPtrT(window, title);
        public static void SDL_SetWindowTitle(IntPtr window, string title) => _SDL_SetWindowTitle(window, Util.StringToHGlobalUTF8(title));

        private delegate IntPtr SdlGetWindowTitleIntPtrT(IntPtr window);
        private static readonly SdlGetWindowTitleIntPtrT SSdlGetWindowTitleIntPtrT = __LoadFunction<SdlGetWindowTitleIntPtrT>("SDL_GetWindowTitle");
        private static IntPtr _SDL_GetWindowTitle(IntPtr window) => SSdlGetWindowTitleIntPtrT(window);
        public static string SDL_GetWindowTitle(IntPtr window) => Util.PtrToStringUTF8(_SDL_GetWindowTitle(window));

        private delegate void SdlSetWindowIconIntPtrSdlSurfaceT(IntPtr window, ref SdlSurfaceBlit.SdlSurface icon);
        private static readonly SdlSetWindowIconIntPtrSdlSurfaceT SSdlSetWindowIconIntPtrSdlSurfaceT = __LoadFunction<SdlSetWindowIconIntPtrSdlSurfaceT>("SDL_SetWindowIcon");
        public static void SDL_SetWindowIcon(IntPtr window, ref SdlSurfaceBlit.SdlSurface icon) => SSdlSetWindowIconIntPtrSdlSurfaceT(window, ref icon);

        private delegate IntPtr SdlSetWindowDataIntPtrIntPtrIntPtrT(IntPtr window, IntPtr name, IntPtr userdata);
        private static readonly SdlSetWindowDataIntPtrIntPtrIntPtrT SSdlSetWindowDataIntPtrIntPtrIntPtrT = __LoadFunction<SdlSetWindowDataIntPtrIntPtrIntPtrT>("SDL_SetWindowData");
        public static IntPtr SDL_SetWindowData(IntPtr window, IntPtr name, IntPtr userdata) => SSdlSetWindowDataIntPtrIntPtrIntPtrT(window, name, userdata);

        private delegate IntPtr SdlGetWindowDataIntPtrIntPtrT(IntPtr window, IntPtr name);
        private static readonly SdlGetWindowDataIntPtrIntPtrT SSdlGetWindowDataIntPtrIntPtrT = __LoadFunction<SdlGetWindowDataIntPtrIntPtrT>("SDL_GetWindowData");
        public static IntPtr SDL_GetWindowData(IntPtr window, IntPtr name) => SSdlGetWindowDataIntPtrIntPtrT(window, name);

        private delegate void SdlSetWindowPositionIntPtrIntIntT(IntPtr window, int x, int y);
        private static readonly SdlSetWindowPositionIntPtrIntIntT SSdlSetWindowPositionIntPtrIntIntT = __LoadFunction<SdlSetWindowPositionIntPtrIntIntT>("SDL_SetWindowPosition");
        public static void SDL_SetWindowPosition(IntPtr window, int x, int y) => SSdlSetWindowPositionIntPtrIntIntT(window, x, y);

        private delegate void SdlGetWindowPositionIntPtrIntIntT(IntPtr window, out int x, out int y);
        private static readonly SdlGetWindowPositionIntPtrIntIntT SSdlGetWindowPositionIntPtrIntIntT = __LoadFunction<SdlGetWindowPositionIntPtrIntIntT>("SDL_GetWindowPosition");
        public static void SDL_GetWindowPosition(IntPtr window, out int x, out int y) => SSdlGetWindowPositionIntPtrIntIntT(window, out x, out y);

        private delegate void SdlSetWindowSizeIntPtrIntIntT(IntPtr window, int w, int h);
        private static readonly SdlSetWindowSizeIntPtrIntIntT SSdlSetWindowSizeIntPtrIntIntT = __LoadFunction<SdlSetWindowSizeIntPtrIntIntT>("SDL_SetWindowSize");
        public static void SDL_SetWindowSize(IntPtr window, int w, int h) => SSdlSetWindowSizeIntPtrIntIntT(window, w, h);

        private delegate void SdlGetWindowSizeIntPtrIntIntT(IntPtr window, out int w, out int h);
        private static readonly SdlGetWindowSizeIntPtrIntIntT SSdlGetWindowSizeIntPtrIntIntT = __LoadFunction<SdlGetWindowSizeIntPtrIntIntT>("SDL_GetWindowSize");
        public static void SDL_GetWindowSize(IntPtr window, out int w, out int h) => SSdlGetWindowSizeIntPtrIntIntT(window, out w, out h);

        private delegate void SdlSetWindowMinimumSizeIntPtrIntIntT(IntPtr window, int minW, int minH);
        private static readonly SdlSetWindowMinimumSizeIntPtrIntIntT SSdlSetWindowMinimumSizeIntPtrIntIntT = __LoadFunction<SdlSetWindowMinimumSizeIntPtrIntIntT>("SDL_SetWindowMinimumSize");
        public static void SDL_SetWindowMinimumSize(IntPtr window, int minW, int minH) => SSdlSetWindowMinimumSizeIntPtrIntIntT(window, minW, minH);

        private delegate void SdlGetWindowMinimumSizeIntPtrIntIntT(IntPtr window, out int w, out int h);
        private static readonly SdlGetWindowMinimumSizeIntPtrIntIntT SSdlGetWindowMinimumSizeIntPtrIntIntT = __LoadFunction<SdlGetWindowMinimumSizeIntPtrIntIntT>("SDL_GetWindowMinimumSize");
        public static void SDL_GetWindowMinimumSize(IntPtr window, out int w, out int h) => SSdlGetWindowMinimumSizeIntPtrIntIntT(window, out w, out h);

        private delegate void SdlSetWindowMaximumSizeIntPtrIntIntT(IntPtr window, int maxW, int maxH);
        private static readonly SdlSetWindowMaximumSizeIntPtrIntIntT SSdlSetWindowMaximumSizeIntPtrIntIntT = __LoadFunction<SdlSetWindowMaximumSizeIntPtrIntIntT>("SDL_SetWindowMaximumSize");
        public static void SDL_SetWindowMaximumSize(IntPtr window, int maxW, int maxH) => SSdlSetWindowMaximumSizeIntPtrIntIntT(window, maxW, maxH);

        private delegate void SdlGetWindowMaximumSizeIntPtrIntIntT(IntPtr window, out int w, out int h);
        private static readonly SdlGetWindowMaximumSizeIntPtrIntIntT SSdlGetWindowMaximumSizeIntPtrIntIntT = __LoadFunction<SdlGetWindowMaximumSizeIntPtrIntIntT>("SDL_GetWindowMaximumSize");
        public static void SDL_GetWindowMaximumSize(IntPtr window, out int w, out int h) => SSdlGetWindowMaximumSizeIntPtrIntIntT(window, out w, out h);

        private delegate void SdlSetWindowBorderedIntPtrSdlBoolT(IntPtr window, SDL_bool bordered);
        private static readonly SdlSetWindowBorderedIntPtrSdlBoolT SSdlSetWindowBorderedIntPtrSdlBoolT = __LoadFunction<SdlSetWindowBorderedIntPtrSdlBoolT>("SDL_SetWindowBordered");
        public static void SDL_SetWindowBordered(IntPtr window, SDL_bool bordered) => SSdlSetWindowBorderedIntPtrSdlBoolT(window, bordered);

        private delegate void SdlShowWindowIntPtrT(IntPtr window);
        private static readonly SdlShowWindowIntPtrT SSdlShowWindowIntPtrT = __LoadFunction<SdlShowWindowIntPtrT>("SDL_ShowWindow");
        public static void SDL_ShowWindow(IntPtr window) => SSdlShowWindowIntPtrT(window);

        private delegate void SdlHideWindowIntPtrT(IntPtr window);
        private static readonly SdlHideWindowIntPtrT SSdlHideWindowIntPtrT = __LoadFunction<SdlHideWindowIntPtrT>("SDL_HideWindow");
        public static void SDL_HideWindow(IntPtr window) => SSdlHideWindowIntPtrT(window);

        private delegate void SdlRaiseWindowIntPtrT(IntPtr window);
        private static readonly SdlRaiseWindowIntPtrT SSdlRaiseWindowIntPtrT = __LoadFunction<SdlRaiseWindowIntPtrT>("SDL_RaiseWindow");
        public static void SDL_RaiseWindow(IntPtr window) => SSdlRaiseWindowIntPtrT(window);

        private delegate void SdlMaximizeWindowIntPtrT(IntPtr window);
        private static readonly SdlMaximizeWindowIntPtrT SSdlMaximizeWindowIntPtrT = __LoadFunction<SdlMaximizeWindowIntPtrT>("SDL_MaximizeWindow");
        public static void SDL_MaximizeWindow(IntPtr window) => SSdlMaximizeWindowIntPtrT(window);

        private delegate void SdlMinimizeWindowIntPtrT(IntPtr window);
        private static readonly SdlMinimizeWindowIntPtrT SSdlMinimizeWindowIntPtrT = __LoadFunction<SdlMinimizeWindowIntPtrT>("SDL_MinimizeWindow");
        public static void SDL_MinimizeWindow(IntPtr window) => SSdlMinimizeWindowIntPtrT(window);

        private delegate void SdlRestoreWindowIntPtrT(IntPtr window);
        private static readonly SdlRestoreWindowIntPtrT SSdlRestoreWindowIntPtrT = __LoadFunction<SdlRestoreWindowIntPtrT>("SDL_RestoreWindow");
        public static void SDL_RestoreWindow(IntPtr window) => SSdlRestoreWindowIntPtrT(window);

        private delegate int SdlSetWindowFullscreenIntPtrUInt32T(IntPtr window, UInt32 flags);
        private static readonly SdlSetWindowFullscreenIntPtrUInt32T SSdlSetWindowFullscreenIntPtrUInt32T = __LoadFunction<SdlSetWindowFullscreenIntPtrUInt32T>("SDL_SetWindowFullscreen");
        public static int SDL_SetWindowFullscreen(IntPtr window, UInt32 flags) => SSdlSetWindowFullscreenIntPtrUInt32T(window, flags);

        private delegate IntPtr SdlGetWindowSurfaceIntPtrT(IntPtr window);
        private static readonly SdlGetWindowSurfaceIntPtrT SSdlGetWindowSurfaceIntPtrT = __LoadFunction<SdlGetWindowSurfaceIntPtrT>("SDL_GetWindowSurface");
        public static IntPtr SDL_GetWindowSurface(IntPtr window) => SSdlGetWindowSurfaceIntPtrT(window);

        private delegate int SdlUpdateWindowSurfaceIntPtrT(IntPtr window);
        private static readonly SdlUpdateWindowSurfaceIntPtrT SSdlUpdateWindowSurfaceIntPtrT = __LoadFunction<SdlUpdateWindowSurfaceIntPtrT>("SDL_UpdateWindowSurface");
        public static int SDL_UpdateWindowSurface(IntPtr window) => SSdlUpdateWindowSurfaceIntPtrT(window);

        private delegate int SdlUpdateWindowSurfaceRectsIntPtrSdlRectIntT(IntPtr window, ref SdlPointRect.SdlRect rects, int numrects);
        private static readonly SdlUpdateWindowSurfaceRectsIntPtrSdlRectIntT SSdlUpdateWindowSurfaceRectsIntPtrSdlRectIntT = __LoadFunction<SdlUpdateWindowSurfaceRectsIntPtrSdlRectIntT>("SDL_UpdateWindowSurfaceRects");
        public static int SDL_UpdateWindowSurfaceRects(IntPtr window, ref SdlPointRect.SdlRect rects, int numrects) => SSdlUpdateWindowSurfaceRectsIntPtrSdlRectIntT(window, ref rects, numrects);

        private delegate void SdlSetWindowGrabIntPtrSdlBoolT(IntPtr window, SDL_bool grabbed);
        private static readonly SdlSetWindowGrabIntPtrSdlBoolT SSdlSetWindowGrabIntPtrSdlBoolT = __LoadFunction<SdlSetWindowGrabIntPtrSdlBoolT>("SDL_SetWindowGrab");
        public static void SDL_SetWindowGrab(IntPtr window, SDL_bool grabbed) => SSdlSetWindowGrabIntPtrSdlBoolT(window, grabbed);

        private delegate SDL_bool SdlGetWindowGrabIntPtrT(IntPtr window);
        private static readonly SdlGetWindowGrabIntPtrT SSdlGetWindowGrabIntPtrT = __LoadFunction<SdlGetWindowGrabIntPtrT>("SDL_GetWindowGrab");
        public static SDL_bool SDL_GetWindowGrab(IntPtr window) => SSdlGetWindowGrabIntPtrT(window);

        private delegate int SdlSetWindowBrightnessIntPtrFloatT(IntPtr window, float brightness);
        private static readonly SdlSetWindowBrightnessIntPtrFloatT SSdlSetWindowBrightnessIntPtrFloatT = __LoadFunction<SdlSetWindowBrightnessIntPtrFloatT>("SDL_SetWindowBrightness");
        public static int SDL_SetWindowBrightness(IntPtr window, float brightness) => SSdlSetWindowBrightnessIntPtrFloatT(window, brightness);

        private delegate float SdlGetWindowBrightnessIntPtrT(IntPtr window);
        private static readonly SdlGetWindowBrightnessIntPtrT SSdlGetWindowBrightnessIntPtrT = __LoadFunction<SdlGetWindowBrightnessIntPtrT>("SDL_GetWindowBrightness");
        public static float SDL_GetWindowBrightness(IntPtr window) => SSdlGetWindowBrightnessIntPtrT(window);

        private delegate int SdlSetWindowGammaRampIntPtrUshortUshortUshortT(IntPtr window, ref ushort red, ref ushort green, ref ushort blue);
        private static readonly SdlSetWindowGammaRampIntPtrUshortUshortUshortT SSdlSetWindowGammaRampIntPtrUshortUshortUshortT = __LoadFunction<SdlSetWindowGammaRampIntPtrUshortUshortUshortT>("SDL_SetWindowGammaRamp");
        public static int SDL_SetWindowGammaRamp(IntPtr window, ref ushort red, ref ushort green, ref ushort blue) => SSdlSetWindowGammaRampIntPtrUshortUshortUshortT(window, ref red, ref green, ref blue);

        private delegate int SdlGetWindowGammaRampIntPtrUshortUshortUshortT(IntPtr window, ref ushort red, ref ushort green, ref ushort blue);
        private static readonly SdlGetWindowGammaRampIntPtrUshortUshortUshortT SSdlGetWindowGammaRampIntPtrUshortUshortUshortT = __LoadFunction<SdlGetWindowGammaRampIntPtrUshortUshortUshortT>("SDL_GetWindowGammaRamp");
        public static int SDL_GetWindowGammaRamp(IntPtr window, ref ushort red, ref ushort green, ref ushort blue) => SSdlGetWindowGammaRampIntPtrUshortUshortUshortT(window, ref red, ref green, ref blue);

        private delegate void SdlDestroyWindowIntPtrT(IntPtr window);
        private static readonly SdlDestroyWindowIntPtrT SSdlDestroyWindowIntPtrT = __LoadFunction<SdlDestroyWindowIntPtrT>("SDL_DestroyWindow");
        public static void SDL_DestroyWindow(IntPtr window) => SSdlDestroyWindowIntPtrT(window);

        private delegate SDL_bool SdlIsScreenSaverEnabledT();
        private static readonly SdlIsScreenSaverEnabledT SSdlIsScreenSaverEnabledT = __LoadFunction<SdlIsScreenSaverEnabledT>("SDL_IsScreenSaverEnabled");
        public static SDL_bool SDL_IsScreenSaverEnabled() => SSdlIsScreenSaverEnabledT();

        private delegate void SdlEnableScreenSaverT();
        private static readonly SdlEnableScreenSaverT SSdlEnableScreenSaverT = __LoadFunction<SdlEnableScreenSaverT>("SDL_EnableScreenSaver");
        public static void SDL_EnableScreenSaver() => SSdlEnableScreenSaverT();

        private delegate void SdlDisableScreenSaverT();
        private static readonly SdlDisableScreenSaverT SSdlDisableScreenSaverT = __LoadFunction<SdlDisableScreenSaverT>("SDL_DisableScreenSaver");

        public static void SDL_DisableScreenSaver() => SSdlDisableScreenSaverT();

        private delegate int SdlGlLoadLibraryIntPtrT(IntPtr path);
        private static readonly SdlGlLoadLibraryIntPtrT SSdlGlLoadLibraryIntPtrT = __LoadFunction<SdlGlLoadLibraryIntPtrT>("SDL_GL_LoadLibrary");
        public static int SDL_GL_LoadLibrary(IntPtr path) => SSdlGlLoadLibraryIntPtrT(path);

        private delegate IntPtr SdlGlGetProcAddressIntPtrT(IntPtr proc);
        private static readonly SdlGlGetProcAddressIntPtrT SSdlGlGetProcAddressIntPtrT = __LoadFunction<SdlGlGetProcAddressIntPtrT>("SDL_GL_GetProcAddress");
        public static IntPtr SDL_GL_GetProcAddress(IntPtr proc) => SSdlGlGetProcAddressIntPtrT(proc);

        private delegate void SdlGlUnloadLibraryT();
        private static readonly SdlGlUnloadLibraryT SSdlGlUnloadLibraryT = __LoadFunction<SdlGlUnloadLibraryT>("SDL_GL_UnloadLibrary");
        public static void SDL_GL_UnloadLibrary() => SSdlGlUnloadLibraryT();

        private delegate SDL_bool SdlGlExtensionSupportedIntPtrT(IntPtr extension);
        private static readonly SdlGlExtensionSupportedIntPtrT SSdlGlExtensionSupportedIntPtrT = __LoadFunction<SdlGlExtensionSupportedIntPtrT>("SDL_GL_ExtensionSupported");
        public static SDL_bool SDL_GL_ExtensionSupported(IntPtr extension) => SSdlGlExtensionSupportedIntPtrT(extension);

        private delegate void SdlGlResetAttributesT();
        private static readonly SdlGlResetAttributesT SSdlGlResetAttributesT = __LoadFunction<SdlGlResetAttributesT>("SDL_GL_ResetAttributes");
        public static void SDL_GL_ResetAttributes() => SSdlGlResetAttributesT();

        private delegate int SdlGlSetAttributeSdlGLattrIntT(SDL_GLattr attr, int value);
        private static readonly SdlGlSetAttributeSdlGLattrIntT SSdlGlSetAttributeSdlGLattrIntT = __LoadFunction<SdlGlSetAttributeSdlGLattrIntT>("SDL_GL_SetAttribute");
        public static int SDL_GL_SetAttribute(SDL_GLattr attr, int value) => SSdlGlSetAttributeSdlGLattrIntT(attr, value);

        private delegate int SdlGlGetAttributeSdlGLattrIntT(SDL_GLattr attr, out int value);
        private static readonly SdlGlGetAttributeSdlGLattrIntT SSdlGlGetAttributeSdlGLattrIntT = __LoadFunction<SdlGlGetAttributeSdlGLattrIntT>("SDL_GL_GetAttribute");
        public static int SDL_GL_GetAttribute(SDL_GLattr attr, out int value) => SSdlGlGetAttributeSdlGLattrIntT(attr, out value);

        private delegate SDL_GLContext SdlGlCreateContextIntPtrT(IntPtr window);
        private static readonly SdlGlCreateContextIntPtrT SSSdlGlCreateContextIntPtrT = __LoadFunction<SdlGlCreateContextIntPtrT>("SDL_GL_CreateContext");
        public static SDL_GLContext SDL_GL_CreateContext(IntPtr window) => SSSdlGlCreateContextIntPtrT(window);

        private delegate int SdlGlMakeCurrentIntPtrSdlGlContextT(IntPtr window, SDL_GLContext context);
        private static readonly SdlGlMakeCurrentIntPtrSdlGlContextT SSdlGlMakeCurrentIntPtrSdlGlContextT = __LoadFunction<SdlGlMakeCurrentIntPtrSdlGlContextT>("SDL_GL_MakeCurrent");
        public static int SDL_GL_MakeCurrent(IntPtr window, SDL_GLContext context) => SSdlGlMakeCurrentIntPtrSdlGlContextT(window, context);

        private delegate IntPtr SdlGlGetCurrentWindowT();
        private static readonly SdlGlGetCurrentWindowT SSdlGlGetCurrentWindowT = __LoadFunction<SdlGlGetCurrentWindowT>("SDL_GL_GetCurrentWindow");
        public static IntPtr SDL_GL_GetCurrentWindow() => SSdlGlGetCurrentWindowT();

        private delegate SDL_GLContext SdlGlGetCurrentContextT();
        private static readonly SdlGlGetCurrentContextT SSdlGlGetCurrentContextT = __LoadFunction<SdlGlGetCurrentContextT>("SDL_GL_GetCurrentContext");
        public static SDL_GLContext SDL_GL_GetCurrentContext() => SSdlGlGetCurrentContextT();

        private delegate void SdlGlGetDrawableSizeIntPtrIntIntT(IntPtr window, out int w, out int h);
        private static readonly SdlGlGetDrawableSizeIntPtrIntIntT SSdlGlGetDrawableSizeIntPtrIntIntT = __LoadFunction<SdlGlGetDrawableSizeIntPtrIntIntT>("SDL_GL_GetDrawableSize");
        public static void SDL_GL_GetDrawableSize(IntPtr window, out int w, out int h) => SSdlGlGetDrawableSizeIntPtrIntIntT(window, out w, out h);

        private delegate int SdlGlSetSwapIntervalIntT(int interval);
        private static readonly SdlGlSetSwapIntervalIntT SSdlGlSetSwapIntervalIntT = __LoadFunction<SdlGlSetSwapIntervalIntT>("SDL_GL_SetSwapInterval");
        public static int SDL_GL_SetSwapInterval(int interval) => SSdlGlSetSwapIntervalIntT(interval);

        private delegate int SdlGlGetSwapIntervalT();
        private static readonly SdlGlGetSwapIntervalT SSdlGlGetSwapIntervalT = __LoadFunction<SdlGlGetSwapIntervalT>("SDL_GL_GetSwapInterval");
        public static int SDL_GL_GetSwapInterval() => SSdlGlGetSwapIntervalT();

        private delegate void SdlGlSwapWindowIntPtrT(IntPtr window);
        private static readonly SdlGlSwapWindowIntPtrT SSdlGlSwapWindowIntPtrT = __LoadFunction<SdlGlSwapWindowIntPtrT>("SDL_GL_SwapWindow");
        public static void SDL_GL_SwapWindow(IntPtr window) => SSdlGlSwapWindowIntPtrT(window);

        private delegate void SdlGlDeleteContextSdlGlContextT(SDL_GLContext context);
        private static readonly SdlGlDeleteContextSdlGlContextT SSdlGlDeleteContextSdlGlContextT = __LoadFunction<SdlGlDeleteContextSdlGlContextT>("SDL_GL_DeleteContext");
        public static void SDL_GL_DeleteContext(SDL_GLContext context) => SSdlGlDeleteContextSdlGlContextT(context);

        /* X11 ONLY */
        private delegate int SdlSetWindowInputFocusIntPtrT(IntPtr window);
        private static readonly SdlSetWindowInputFocusIntPtrT SSdlSetWindowInputFocusIntPtrT;
        public static int SDL_SetWindowInputFocus(IntPtr window) => SSdlSetWindowInputFocusIntPtrT(window);

        private static T __LoadFunction<T>(string name) { return LoaderSdl2.LoadFunction<T>(name); }

#pragma warning disable
        static SdlVideo()
        {
            try
            {
                SSdlSetWindowInputFocusIntPtrT = __LoadFunction<SdlSetWindowInputFocusIntPtrT>("SDL_SetWindowInputFocus");
            }
            catch (Exception ex)
            {
                SSdlSetWindowInputFocusIntPtrT = p => { return 0; };
            }
        }
#pragma warning enable
    }
}
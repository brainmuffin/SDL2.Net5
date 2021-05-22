using System;
using System.Runtime.InteropServices;

namespace SDL2.Net5
{
    public static class SdlErrorCode
    {
        public enum SDL_errorcode
        {
            SDL_ENOMEM,
            SDL_EFREAD,
            SDL_EFWRITE,
            SDL_EFSEEK,
            SDL_UNSUPPORTED,
            SDL_LASTERROR
        }

        private delegate IntPtr SdlGetErrorT();
        private static readonly SdlGetErrorT SSdlGetErrorT = __LoadFunction<SdlGetErrorT>("SDL_GetError");
        private static IntPtr _SDL_GetError() => SSdlGetErrorT();
        public static string SdlGetError() => Marshal.PtrToStringAnsi(_SDL_GetError());

        private delegate void SdlClearErrorT();
        private static readonly SdlClearErrorT SSdlClearErrorT = __LoadFunction<SdlClearErrorT>("SDL_ClearError");
        public static void SdlClearError() => SSdlClearErrorT();

        private delegate int SdlErrorSdlErrorCodeT(SDL_errorcode code);
        private static readonly SdlErrorSdlErrorCodeT SSdlErrorSdlErrorCodeT = __LoadFunction<SdlErrorSdlErrorCodeT>("SDL_Error");
        public static int SdlError(SDL_errorcode code) => SSdlErrorSdlErrorCodeT(code);
        
        private static T __LoadFunction<T>(string name) { return Internal.LoaderSdl2.LoadFunction<T>(name); }
    }
}
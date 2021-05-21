using System;
using System.Runtime.InteropServices;

namespace SDL2.Net5
{
    public class SdlVersion
    {
        public const int SDL_MAJOR_VERSION = 2;
        public const int SDL_MINOR_VERSION = 0;
        public const int SDL_PATCHLEVEL = 2;
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Version
        {
            public byte major;        /**< major version */
            public byte minor;        /**< minor version */
            public byte patch;        /**< update version */
        }

        private delegate void GetVersionSdlVersionT(out SDL_Version ver);
        private static readonly GetVersionSdlVersionT SSSdlGetVersionSdlVersionT = __LoadFunction<GetVersionSdlVersionT>("SDL_GetVersion");
        public static void SDL_GetVersion(out SDL_Version ver) => SSSdlGetVersionSdlVersionT(out ver);

        private delegate IntPtr GetRevisionT();
        private static readonly GetRevisionT SSSdlGetRevisionT = __LoadFunction<GetRevisionT>("SDL_GetRevision");
        public static IntPtr SDL_GetRevision() => SSSdlGetRevisionT();

        private delegate int GetRevisionNumberT();
        private static readonly GetRevisionNumberT SSSdlGetRevisionNumberT = __LoadFunction<GetRevisionNumberT>("SDL_GetRevisionNumber");
        public static int SDL_GetRevisionNumber() => SSSdlGetRevisionNumberT();
        
        private static T __LoadFunction<T>(string name) { return Internal.LoaderSdl2.LoadFunction<T>(name); }
    }
}
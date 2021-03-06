using System;

namespace SDL2.Net5
{
    public static class Sdl
    {
        public const int SDL_INIT_TIMER = 0x00000001;
        public const int SDL_INIT_AUDIO = 0x00000010;
        public const int SDL_INIT_VIDEO = 0x00000020;  /**< SDL_INIT_VIDEO implies SDL_INIT_EVENTS */
        public const int SDL_INIT_JOYSTICK = 0x00000200;  /**< SDL_INIT_JOYSTICK implies SDL_INIT_EVENTS */
        public const int SDL_INIT_HAPTIC = 0x00001000;
        public const int SDL_INIT_GAMECONTROLLER = 0x00002000;  /**< SDL_INIT_GAMECONTROLLER implies SDL_INIT_JOYSTICK */
        public const int SDL_INIT_EVENTS = 0x00004000;
        public const int SDL_INIT_NOPARACHUTE = 0x00100000;  /**< Don't catch fatal signals */
        public const int SDL_INIT_EVERYTHING = (
                        SDL_INIT_TIMER | SDL_INIT_AUDIO | SDL_INIT_VIDEO | SDL_INIT_EVENTS |
                        SDL_INIT_JOYSTICK | SDL_INIT_HAPTIC | SDL_INIT_GAMECONTROLLER);

        private delegate int SdlInitUInt32T(UInt32 flags);
        private static readonly SdlInitUInt32T SSdlInitUInt32T = __LoadFunction<SdlInitUInt32T>("SDL_Init");
        public static int SdlInit(uint flags) => SSdlInitUInt32T(flags);

        private delegate int SdlInitSubSystemUInt32T(UInt32 flags);
        private static SdlInitSubSystemUInt32T s_SDL_InitSubSystem_UInt32_t = __LoadFunction<SdlInitSubSystemUInt32T>("SDL_InitSubSystem");
        public static int SDL_InitSubSystem(UInt32 flags) => s_SDL_InitSubSystem_UInt32_t(flags);

        private delegate void SdlQuitSubSystemUInt32T(UInt32 flags);
        private static SdlQuitSubSystemUInt32T s_SDL_QuitSubSystem_UInt32_t = __LoadFunction<SdlQuitSubSystemUInt32T>("SDL_QuitSubSystem");
        public static void SDL_QuitSubSystem(UInt32 flags) => s_SDL_QuitSubSystem_UInt32_t(flags);

        private delegate UInt32 SdlWasInitUInt32T(UInt32 flags);
        private static SdlWasInitUInt32T s_SDL_WasInit_UInt32_t = __LoadFunction<SdlWasInitUInt32T>("SDL_WasInit");
        public static UInt32 SDL_WasInit(UInt32 flags) => s_SDL_WasInit_UInt32_t(flags);

        private delegate void SdlQuitT();
        private static readonly SdlQuitT SSdlQuitT = __LoadFunction<SdlQuitT>("SDL_Quit");
        public static void SdlQuit() => SSdlQuitT();
        
        private static T __LoadFunction<T>(string name)
        {
            return Internal.LoaderSdl2.LoadFunction<T>(name);
        }
    }
}
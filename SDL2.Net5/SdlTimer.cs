using SDL2.Net5.Internal;

namespace SDL2.Net5
{
    public class SdlTimer
    {
        private delegate uint GetTicksT();
        private static readonly GetTicksT SSSdlGetTicksT = __LoadFunction<GetTicksT>("SDL_GetTicks");
        public static uint SDL_GetTicks() => SSSdlGetTicksT();
        
        private delegate uint DelayT(uint ms);
        private static readonly DelayT SSSdlDelayT = __LoadFunction<DelayT>("SDL_Delay");
        public static void SDL_Delay(uint ms) => SSSdlDelayT(ms);
        
        private static T __LoadFunction<T>(string name) { return LoaderSdl2.LoadFunction<T>(name); }
    }
}
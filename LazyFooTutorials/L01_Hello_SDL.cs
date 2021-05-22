using System;
using System.Runtime.InteropServices;
using SDL2.Net5;

using static SDL2.Net5.Sdl;
using static SDL2.Net5.SdlErrorCode;
using static SDL2.Net5.SdlPixels;
using static SDL2.Net5.SdlSurfaceBlit;
using static SDL2.Net5.SdlVideo;
using static SDL2.Net5.SdlTimer;

namespace LazyFooTutorials
{
    public class L01_Hello_SDL
    {
        private const int SDL_SCREEN_WIDTH = 640;
        private const int SDL_SCREEN_HEIGHT = 480;
        
        public static void Show()
        {
            unsafe
            {
                if (SdlInit(SDL_INIT_VIDEO) != 0)
                    throw new Exception($"SDL could not initialize! SDL_Error: {SdlGetError()}");

                var window = SdlCreateWindow("SDL Tutorial", SDL_WINDOWPOS_UNDEFINED_MASK, SDL_WINDOWPOS_UNDEFINED_MASK,
                    SDL_SCREEN_WIDTH, SDL_SCREEN_HEIGHT, (uint) SdlVideo.SDL_WindowFlags.SDL_WINDOW_SHOWN);
                if (window == IntPtr.Zero)
                    throw new Exception($"Window could not be created! SDL_Error: {SdlGetError()}");
            
                var screenSurface = SDL_GetWindowSurface( window );
                var convertedScreen = Marshal.PtrToStructure<SdlSurface>(screenSurface);

                //Fill the surface white
                SDL_FillRect( screenSurface, null, SdlMapRGB( convertedScreen.format, 0xFF, 0xFF, 0xFF ) );
            
                //Update the surface
                SdlUpdateWindowSurface( window );

                //Wait two seconds
                SdlDelay( 4000 );
                
                SDL_DestroyWindow( window );

                //Quit SDL subsystems
                SdlQuit();
            }
        }
    }
}
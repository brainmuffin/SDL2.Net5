using System;
using SDL2.Net5;

using static SDL2.Net5.Sdl;
using static SDL2.Net5.SdlErrorCode;
using static SDL2.Net5.SdlEvents;
using static SDL2.Net5.SdlSurfaceBlit;
using static SDL2.Net5.SdlVideo;

namespace LazyFooTutorials
{
    public class L03_X_Out : ILesson
    {
        private const int SCREEN_WIDTH = 640;
        private const int SCREEN_HEIGHT = 480;

        private IntPtr _window;
        private IntPtr _screenSurface;
        private IntPtr _xOut;

        public void Show()
        {
            if (!Init())
                throw new Exception($"SDL could not initialize! SDL_Error: {SdlGetError()}");
                
            if( !LoadImage() )
                throw new Exception( "Failed to load media!" );

            var quit = false;

            while (!quit)
            {
                while( SdlPollEvent( out var raisedEvent ) != 0 )
                {
                    //User requests quit
                    if( (SdlEvents.SDL_EventType) raisedEvent.type == SdlEvents.SDL_EventType.SDL_QUIT )
                    {
                        quit = true;
                    }
                }

                SdlBlitSurface(_xOut, IntPtr.Zero, _screenSurface, IntPtr.Zero);

                SdlUpdateWindowSurface(_window);
            }

            Close();
        }
        
        private bool Init()
        {
            if (SdlInit(SDL_INIT_VIDEO) != 0)
            { 
                Console.WriteLine($"SDL could not initialize! SDL_Error: {SdlGetError()}");
                return false;
            }

            _window = SdlCreateWindow("SDL Load Image", SDL_WINDOWPOS_UNDEFINED_MASK, SDL_WINDOWPOS_UNDEFINED_MASK,
                SCREEN_WIDTH, SCREEN_HEIGHT, (uint) SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if (_window == IntPtr.Zero)
            {
                Console.WriteLine($"Window could not be created! SDL_Error: {SdlGetError()}");
                return false;
            }
            
            _screenSurface = SdlGetWindowSurface( _window );

            return true;
        }
        
        private bool LoadImage()
        {
            _xOut = SdlLoadBmp( "03_event_driven_programming/x.bmp" );
            if( _xOut == IntPtr.Zero )
            {
                Console.WriteLine($"Unable to load image 03_event_driven_programming/x.bmp! SDL Error: {SdlGetError()}");
                return false;
            }

            return true;
        }

        private void Close()
        {
            SdlFreeSurface( _xOut );
            
            SdlDestroyWindow( _window );
            
            SdlQuit();
        }
    }
}
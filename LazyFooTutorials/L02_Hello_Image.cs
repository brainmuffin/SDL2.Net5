using System;

using static SDL2.Net5.Sdl;
using static SDL2.Net5.SdlErrorCode;
using static SDL2.Net5.SdlSurfaceBlit;
using static SDL2.Net5.SdlTimer;
using static SDL2.Net5.SdlVideo;

namespace LazyFooTutorials
{
    public class L02_Hello_Image
    {
        private const int SCREEN_WIDTH = 640;
        private const int SCREEN_HEIGHT = 480;

        private IntPtr _window;
        private IntPtr _screenSurface;
        private IntPtr _helloWorld;

        public void Show()
        {
            if (!Init())
                throw new Exception($"SDL could not initialize! SDL_Error: {SdlGetError()}");
                
            if( !LoadImage() )
                throw new Exception( "Failed to load media!" );

            SdlBlitSurface(_helloWorld, IntPtr.Zero, _screenSurface, IntPtr.Zero);
            
            SdlUpdateWindowSurface( _window );
            
            SdlDelay( 5000 );

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
            
            _screenSurface = SDL_GetWindowSurface( _window );

            return true;
        }
        
        private bool LoadImage()
        {
            _helloWorld = SDL_LoadBMP( "02_getting_an_image_on_the_screen/hello_world.bmp" );
            if( _helloWorld == IntPtr.Zero )
            {
                Console.WriteLine($"Unable to load image 02_getting_an_image_on_the_screen/hello_world.bmp! SDL Error: {SdlGetError()}");
                return false;
            }

            return true;
        }

        private void Close()
        {
            SdlFreeSurface( _helloWorld );
            
            SDL_DestroyWindow( _window );
            
            SdlQuit();
        }
    }
}
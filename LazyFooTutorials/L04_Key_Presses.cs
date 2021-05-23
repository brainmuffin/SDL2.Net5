using System;
using System.Collections.Generic;
using System.Linq;
using static SDL2.Net5.Sdl;
using static SDL2.Net5.SdlErrorCode;
using static SDL2.Net5.SdlKeycode;
using static SDL2.Net5.SdlSurfaceBlit;
using static SDL2.Net5.SdlVideo;
using static SDL2.Net5.SdlEvents;

namespace LazyFooTutorials
{
    public class L04_Key_Presses : ILesson
    {
        private enum KeyPressSurfaces
        {
            KEY_PRESS_SURFACE_DEFAULT,
            KEY_PRESS_SURFACE_UP,
            KEY_PRESS_SURFACE_DOWN,
            KEY_PRESS_SURFACE_LEFT,
            KEY_PRESS_SURFACE_RIGHT,
            KEY_PRESS_SURFACE_TOTAL
        }

        private struct KeyLoadInformation
        {
            public readonly KeyPressSurfaces Id;
            public readonly string Filename;
            public readonly string Message;

            public KeyLoadInformation( KeyPressSurfaces id, string filename, string message)
            {
                Message = message;
                Filename = filename;
                Id = id;
            }
        }

        private static readonly Dictionary<SdlKeyCode, KeyLoadInformation> SKeysToLoad =
            new ()
            {
                {SdlKeyCode.SDLK_0, new KeyLoadInformation(KeyPressSurfaces.KEY_PRESS_SURFACE_DEFAULT, "04_key_presses/press.bmp", "Failed to load default image!")},
                {SdlKeyCode.SDLK_UP, new KeyLoadInformation(KeyPressSurfaces.KEY_PRESS_SURFACE_UP, "04_key_presses/up.bmp", "Failed to load up image!")},
                {SdlKeyCode.SDLK_DOWN, new KeyLoadInformation(KeyPressSurfaces.KEY_PRESS_SURFACE_DOWN, "04_key_presses/down.bmp", "Failed to load down image!")},
                {SdlKeyCode.SDLK_LEFT, new KeyLoadInformation(KeyPressSurfaces.KEY_PRESS_SURFACE_LEFT, "04_key_presses/left.bmp", "Failed to load left image!")},
                {SdlKeyCode.SDLK_RIGHT, new KeyLoadInformation(KeyPressSurfaces.KEY_PRESS_SURFACE_RIGHT, "04_key_presses/right.bmp", "Failed to load right image!")}
            };
        
        private const int SCREEN_WIDTH = 640;
        private const int SCREEN_HEIGHT = 480;

        private IntPtr _window;
        private IntPtr _screenSurface;
        private readonly IntPtr[] _keyPressSurfaces = new IntPtr[(int) KeyPressSurfaces.KEY_PRESS_SURFACE_TOTAL];

        public void Show()
        {
            if (!Init())
                throw new Exception($"SDL could not initialize! SDL_Error: {SdlGetError()}");
                
            if( !LoadImages() )
                throw new Exception( "Failed to load media!" );

            var quit = false;
            var currentSurface = _keyPressSurfaces[ (int)KeyPressSurfaces.KEY_PRESS_SURFACE_DEFAULT ];

            while (!quit)
            {
                while( SdlPollEvent( out var raisedEvent ) != 0 )
                {
                    var eventType = (SDL_EventType) raisedEvent.type;

                    if( eventType == SDL_EventType.SDL_QUIT )
                    {
                        quit = true;
                    }
                    else if( eventType == SDL_EventType.SDL_KEYDOWN )
                    {
                        currentSurface = MapKeyToSurface(raisedEvent.key.keysym.sym);
                    }
                }

                SdlBlitSurface(currentSurface, IntPtr.Zero, _screenSurface, IntPtr.Zero);

                SdlUpdateWindowSurface(_window);
            }

            Close();
        }
        
        private bool Init()
        {
            if (SdlInit(SDL_INIT_EVERYTHING) != 0)
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
        
        private bool LoadImages()
        {
            try
            {
                SKeysToLoad.Select(x => x.Value).ToList().ForEach(LoadKeyImage);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private void LoadKeyImage(KeyLoadInformation loadInformation)
        {
            var index = (int) loadInformation.Id;
            
            _keyPressSurfaces[index] = LoadSurface( loadInformation.Filename );
            if (_keyPressSurfaces[index] == IntPtr.Zero)
                throw new Exception(loadInformation.Message);
        }
        
        private static IntPtr LoadSurface(string path )
        {
            //Load image at specified path
            var loadedSurface = SdlLoadBmp(path);
            if( loadedSurface == IntPtr.Zero )
            {
                Console.WriteLine( $"Unable to load image %s! SDL Error: {path}, {SdlGetError()}" );
            }

            return loadedSurface;
        }

        private IntPtr MapKeyToSurface(SdlKeyCode keyCode)
        {
            return SKeysToLoad.ContainsKey(keyCode) 
                ? _keyPressSurfaces[(int) SKeysToLoad[keyCode].Id] 
                : _keyPressSurfaces[ (int)KeyPressSurfaces.KEY_PRESS_SURFACE_DEFAULT ];
        }

        private void Close()
        {
            SdlDestroyWindow( _window );
            
            SdlQuit();
        }
    }
}
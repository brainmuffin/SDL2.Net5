using System;
using System.Runtime.InteropServices;

using static SDL2.Net5.SdlKeyboard;

using SDL_bool = System.Int32;
using SDL_JoystickID = System.Int32;
using SDL_TouchID = System.Int64;
using SDL_FingerID = System.Int64;
using SDL_GestureID = System.Int64;
using SDL_EventFilter = System.IntPtr;
using System.Runtime.CompilerServices;

namespace SDL2.Net5
{
    public class SdlEvents
    {
        public const int SDL_RELEASED = 0;
        public const int SDL_PRESSED = 1;
        public const int SDL_TEXTEDITINGEVENT_TEXT_SIZE = 32;
        public const int SDL_TEXTINPUTEVENT_TEXT_SIZE = 32;
        public const int SDL_QUERY = -1;
        public const int SDL_IGNORE = 0;
        public const int SDL_DISABLE = 0;
        public const int SDL_ENABLE = 1;

        // This struct should be cast depending on the event type
        // The field here is to ensure proper struct size to hold all variations
        [StructLayout(LayoutKind.Explicit, Size = 56)]
        public struct SDL_Event
        {
            [FieldOffset(0)]
            public UInt32 type;
            [FieldOffset(0)]
            public SDL_CommonEvent common;
            [FieldOffset(0)]
            public SDL_DisplayEvent display;
            [FieldOffset(0)]
            public SDL_WindowEvent window;
            [FieldOffset(0)]
            public SDL_KeyboardEvent key;
            [FieldOffset(0)]
            public SDL_TextEditingEvent edit;      /**< Text editing event data */
            [FieldOffset(0)]
            public SDL_TextInputEvent text;        /**< Text input event data */
            [FieldOffset(0)]
            public SDL_MouseMotionEvent motion;    /**< Mouse motion event data */
            [FieldOffset(0)]
            public SDL_MouseButtonEvent button;    /**< Mouse button event data */
            [FieldOffset(0)]
            public SDL_MouseWheelEvent wheel;      /**< Mouse wheel event data */
            [FieldOffset(0)]
            public SDL_JoyAxisEvent jaxis;         /**< Joystick axis event data */
            [FieldOffset(0)]
            public SDL_JoyBallEvent jball;         /**< Joystick ball event data */
            [FieldOffset(0)]
            public SDL_JoyHatEvent jhat;           /**< Joystick hat event data */
            [FieldOffset(0)]
            public SDL_JoyButtonEvent jbutton;     /**< Joystick button event data */
            [FieldOffset(0)]
            public SDL_JoyDeviceEvent jdevice;     /**< Joystick device change event data */
            [FieldOffset(0)]
            public SDL_ControllerAxisEvent caxis;      /**< Game Controller axis event data */
            [FieldOffset(0)]
            public SDL_ControllerButtonEvent cbutton;  /**< Game Controller button event data */
            [FieldOffset(0)]
            public SDL_ControllerDeviceEvent cdevice;  /**< Game Controller device event data */
            [FieldOffset(0)]
            public SDL_AudioDeviceEvent adevice;   /**< Audio device event data */
            [FieldOffset(0)]
            public SDL_SensorEvent sensor;         /**< Sensor event data */
            [FieldOffset(0)]
            public SDL_QuitEvent quit;             /**< Quit request event data */
            [FieldOffset(0)]
            public SDL_UserEvent user;             /**< Custom event data */
            [FieldOffset(0)]
            public SDL_SysWMEvent syswm;           /**< System dependent window event data */
            [FieldOffset(0)]
            public SDL_TouchFingerEvent tfinger;   /**< Touch finger event data */
            [FieldOffset(0)]
            public SDL_MultiGestureEvent mgesture; /**< Gesture event data */
            [FieldOffset(0)]
            public SDL_DollarGestureEvent dgesture; /**< Gesture event data */
            [FieldOffset(0)]
            public SDL_DropEvent drop;             /**< Drag and drop event data */
            
            [FieldOffset(4)]
            public unsafe fixed byte data[52];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_Event e) =>
               *((SDL_CommonEvent*)&e);
        }

        public enum SDL_EventType
        {
            SDL_FIRSTEVENT = 0,     /**< Unused (do not remove) */

            /* Application events */
            SDL_QUIT = 0x100, /**< User-requested quit */

            /* These application events have special meaning on iOS, see README-ios.txt for details */
            SDL_APP_TERMINATING,        /**< The application is being terminated by the OS
                                     Called on iOS in applicationWillTerminate()
                                     Called on Android in onDestroy()
                                */
            SDL_APP_LOWMEMORY,          /**< The application is low on memory, free memory if possible.
                                     Called on iOS in applicationDidReceiveMemoryWarning()
                                     Called on Android in onLowMemory()
                                */
            SDL_APP_WILLENTERBACKGROUND, /**< The application is about to enter the background
                                     Called on iOS in applicationWillResignActive()
                                     Called on Android in onPause()
                                */
            SDL_APP_DIDENTERBACKGROUND, /**< The application did enter the background and may not get CPU for some time
                                     Called on iOS in applicationDidEnterBackground()
                                     Called on Android in onPause()
                                */
            SDL_APP_WILLENTERFOREGROUND, /**< The application is about to enter the foreground
                                     Called on iOS in applicationWillEnterForeground()
                                     Called on Android in onResume()
                                */
            SDL_APP_DIDENTERFOREGROUND, /**< The application is now interactive
                                     Called on iOS in applicationDidBecomeActive()
                                     Called on Android in onResume()
                                */

            /* Window events */
            SDL_WINDOWEVENT = 0x200, /**< Window state change */
            SDL_SYSWMEVENT,             /**< System specific event */

            /* Keyboard events */
            SDL_KEYDOWN = 0x300, /**< Key pressed */
            SDL_KEYUP,                  /**< Key released */
            SDL_TEXTEDITING,            /**< Keyboard text editing (composition) */
            SDL_TEXTINPUT,              /**< Keyboard text input */

            /* Mouse events */
            SDL_MOUSEMOTION = 0x400, /**< Mouse moved */
            SDL_MOUSEBUTTONDOWN,        /**< Mouse button pressed */
            SDL_MOUSEBUTTONUP,          /**< Mouse button released */
            SDL_MOUSEWHEEL,             /**< Mouse wheel motion */

            /* Joystick events */
            SDL_JOYAXISMOTION = 0x600, /**< Joystick axis motion */
            SDL_JOYBALLMOTION,          /**< Joystick trackball motion */
            SDL_JOYHATMOTION,           /**< Joystick hat position change */
            SDL_JOYBUTTONDOWN,          /**< Joystick button pressed */
            SDL_JOYBUTTONUP,            /**< Joystick button released */
            SDL_JOYDEVICEADDED,         /**< A new joystick has been inserted into the system */
            SDL_JOYDEVICEREMOVED,       /**< An opened joystick has been removed */

            /* Game controller events */
            SDL_CONTROLLERAXISMOTION = 0x650, /**< Game controller axis motion */
            SDL_CONTROLLERBUTTONDOWN,          /**< Game controller button pressed */
            SDL_CONTROLLERBUTTONUP,            /**< Game controller button released */
            SDL_CONTROLLERDEVICEADDED,         /**< A new Game controller has been inserted into the system */
            SDL_CONTROLLERDEVICEREMOVED,       /**< An opened Game controller has been removed */
            SDL_CONTROLLERDEVICEREMAPPED,      /**< The controller mapping was updated */

            /* Touch events */
            SDL_FINGERDOWN = 0x700,
            SDL_FINGERUP,
            SDL_FINGERMOTION,

            /* Gesture events */
            SDL_DOLLARGESTURE = 0x800,
            SDL_DOLLARRECORD,
            SDL_MULTIGESTURE,

            /* Clipboard events */
            SDL_CLIPBOARDUPDATE = 0x900, /**< The clipboard changed */

            /* Drag and drop events */
            SDL_DROPFILE = 0x1000, /**< The system requests a file open */

            /* Render events */
            SDL_RENDER_TARGETS_RESET = 0x2000, /**< The render targets have been reset */

            /** Events ::SDL_USEREVENT through ::SDL_LASTEVENT are for your use,
             *  and should be allocated with SDL_RegisterEvents()
             */
            SDL_USEREVENT = 0x8000,

            /**
             *  This last event is only for bounding internal arrays
             */
            SDL_LASTEVENT = 0xFFFF
        }
        public enum SDL_eventaction
        {
            SDL_ADDEVENT,
            SDL_PEEKEVENT,
            SDL_GETEVENT
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_CommonEvent
        {
            public UInt32 type;
            public UInt32 timestamp;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_CommonEvent e) =>
               *((SDL_Event*)&e);
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_DisplayEvent
        {
            public UInt32 type;        /**< ::SDL_DISPLAYEVENT */
            public UInt32 timestamp;   /**< In milliseconds, populated using SDL_GetTicks() */
            public UInt32 display;     /**< The associated display index */
            public byte @event;        /**< ::SDL_DisplayEventID */
            public byte padding1;
            public byte padding2;
            public byte padding3;
            public Int32 data1;       /**< event dependent data */
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_WindowEvent
        {
            public UInt32 type;        /**< ::SDL_WINDOWEVENT */
            public UInt32 timestamp;
            public UInt32 windowID;    /**< The associated window */
            public byte @event;        /**< ::SDL_WindowEventID */
            public byte padding1;
            public byte padding2;
            public byte padding3;
            public Int32 data1;       /**< event dependent data */
            public Int32 data2;       /**< event dependent data */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_WindowEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_WindowEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_KeyboardEvent
        {
            public UInt32 type;        /**< ::SDL_KEYDOWN or ::SDL_KEYUP */
            public UInt32 timestamp;
            public UInt32 windowID;    /**< The window with keyboard focus, if any */
            public byte state;        /**< ::SDL_PRESSED or ::SDL_RELEASED */
            public byte repeat;       /**< Non-zero if this is a key repeat */
            public byte padding2;
            public byte padding3;
            public SDL_Keysym keysym;  /**< The key that was pressed or released */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_KeyboardEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_KeyboardEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_TextEditingEvent
        {
            public UInt32 type;                                /**< ::SDL_TEXTEDITING */
            public UInt32 timestamp;
            public UInt32 windowID;                            /**< The window with keyboard focus, if any */
            public unsafe fixed byte text[SDL_TEXTEDITINGEVENT_TEXT_SIZE];  /**< The editing text */
            public Int32 start;                               /**< The start cursor of selected editing text */
            public Int32 length;                              /**< The length of selected editing text */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_TextEditingEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_TextEditingEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_TextInputEvent
        {
            public UInt32 type;                              /**< ::SDL_TEXTINPUT */
            public UInt32 timestamp;
            public UInt32 windowID;                          /**< The window with keyboard focus, if any */
            public unsafe fixed byte text[SDL_TEXTINPUTEVENT_TEXT_SIZE];  /**< The input text */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_TextInputEvent e) =>
                *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_TextInputEvent e) =>
                *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_MouseMotionEvent
        {
            public UInt32 type;        /**< ::SDL_MOUSEMOTION */
            public UInt32 timestamp;
            public UInt32 windowID;    /**< The window with mouse focus, if any */
            public UInt32 which;       /**< The mouse instance id, or SDL_TOUCH_MOUSEID */
            public UInt32 state;       /**< The current button state */
            public Int32 x;           /**< X coordinate, relative to window */
            public Int32 y;           /**< Y coordinate, relative to window */
            public Int32 xrel;        /**< The relative motion in the X direction */
            public Int32 yrel;        /**< The relative motion in the Y direction */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_MouseMotionEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_MouseMotionEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_MouseButtonEvent
        {

            public UInt32 type;        /**< ::SDL_MOUSEBUTTONDOWN or ::SDL_MOUSEBUTTONUP */
            public UInt32 timestamp;
            public UInt32 windowID;    /**< The window with mouse focus, if any */
            public UInt32 which;       /**< The mouse instance id, or SDL_TOUCH_MOUSEID */
            public byte button;       /**< The mouse button index */
            public byte state;        /**< ::SDL_PRESSED or ::SDL_RELEASED */
            public byte clicks;       /**< 1 for single-click, 2 for double-click, etc. */
            public byte padding1;
            public Int32 x;           /**< X coordinate, relative to window */
            public Int32 y;           /**< Y coordinate, relative to window */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_MouseButtonEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_MouseButtonEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_MouseWheelEvent
        {

            public UInt32 type;        /**< ::SDL_MOUSEWHEEL */
            public UInt32 timestamp;
            public UInt32 windowID;    /**< The window with mouse focus, if any */
            public UInt32 which;       /**< The mouse instance id, or SDL_TOUCH_MOUSEID */
            public Int32 x;           /**< The amount scrolled horizontally, positive to the right and negative to the left */
            public Int32 y;           /**< The amount scrolled vertically, positive away from the user and negative toward the user */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_MouseWheelEvent e) =>
                *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_MouseWheelEvent e) =>
                *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_JoyAxisEvent
        {
            public UInt32 type;        /**< ::SDL_JOYAXISMOTION */
            public UInt32 timestamp;
            public SDL_JoystickID which; /**< The joystick instance id */
            public byte axis;         /**< The joystick axis index */
            public byte padding1;
            public byte padding2;
            public byte padding3;
            public Int16 value;       /**< The axis value (range: -32768 to 32767) */
            public UInt16 padding4;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_JoyAxisEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_JoyAxisEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_JoyBallEvent
        {
            public UInt32 type;        /**< ::SDL_JOYBALLMOTION */
            public UInt32 timestamp;
            public SDL_JoystickID which; /**< The joystick instance id */
            public byte ball;         /**< The joystick trackball index */
            public byte padding1;
            public byte padding2;
            public byte padding3;
            public Int16 xrel;        /**< The relative motion in the X direction */
            public Int16 yrel;        /**< The relative motion in the Y direction */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_JoyBallEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_JoyBallEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_JoyHatEvent
        {
            public UInt32 type;        /**< ::SDL_JOYHATMOTION */
            public UInt32 timestamp;
            public SDL_JoystickID which; /**< The joystick instance id */
            public byte hat;          /**< The joystick hat index */
            public byte value;        /**< The hat position value.
                          *   \sa ::SDL_HAT_LEFTUP ::SDL_HAT_UP ::SDL_HAT_RIGHTUP
                          *   \sa ::SDL_HAT_LEFT ::SDL_HAT_CENTERED ::SDL_HAT_RIGHT
                          *   \sa ::SDL_HAT_LEFTDOWN ::SDL_HAT_DOWN ::SDL_HAT_RIGHTDOWN
                          *
                          *   Note that zero means the POV is centered.
                          */
            public byte padding1;
            public byte padding2;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_JoyHatEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_JoyHatEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_JoyButtonEvent
        {
            public UInt32 type;        /**< ::SDL_JOYBUTTONDOWN or ::SDL_JOYBUTTONUP */
            public UInt32 timestamp;
            public SDL_JoystickID which; /**< The joystick instance id */
            public byte button;       /**< The joystick button index */
            public byte state;        /**< ::SDL_PRESSED or ::SDL_RELEASED */
            public byte padding1;
            public byte padding2;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_JoyButtonEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_JoyButtonEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_JoyDeviceEvent
        {
            public UInt32 type;        /**< ::SDL_JOYDEVICEADDED or ::SDL_JOYDEVICEREMOVED */
            public UInt32 timestamp;
            public Int32 which;       /**< The joystick device index for the ADDED event, instance id for the REMOVED event */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_JoyDeviceEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_JoyDeviceEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_ControllerAxisEvent
        {
            public UInt32 type;        /**< ::SDL_CONTROLLERAXISMOTION */
            public UInt32 timestamp;
            public SDL_JoystickID which; /**< The joystick instance id */
            public byte axis;         /**< The controller axis (SDL_GameControllerAxis) */
            public byte padding1;
            public byte padding2;
            public byte padding3;
            public Int16 value;       /**< The axis value (range: -32768 to 32767) */
            public UInt16 padding4;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_ControllerAxisEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_ControllerAxisEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_ControllerButtonEvent
        {
            public UInt32 type;        /**< ::SDL_CONTROLLERBUTTONDOWN or ::SDL_CONTROLLERBUTTONUP */
            public UInt32 timestamp;
            public SDL_JoystickID which; /**< The joystick instance id */
            public byte button;       /**< The controller button (SDL_GameControllerButton) */
            public byte state;        /**< ::SDL_PRESSED or ::SDL_RELEASED */
            public byte padding1;
            public byte padding2;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_ControllerButtonEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_ControllerButtonEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_ControllerDeviceEvent
        {
            public UInt32 type;        /**< ::SDL_CONTROLLERDEVICEADDED, ::SDL_CONTROLLERDEVICEREMOVED, or ::SDL_CONTROLLERDEVICEREMAPPED */
            public UInt32 timestamp;
            public Int32 which;       /**< The joystick device index for the ADDED event, instance id for the REMOVED or REMAPPED event */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_ControllerDeviceEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_ControllerDeviceEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_AudioDeviceEvent
        {
            public UInt32 type;        /**< ::SDL_AUDIODEVICEADDED, or ::SDL_AUDIODEVICEREMOVED */
            public UInt32 timestamp;   /**< In milliseconds, populated using SDL_GetTicks() */
            public UInt32 which;       /**< The audio device index for the ADDED event (valid until next SDL_GetNumAudioDevices() call), SDL_AudioDeviceID for the REMOVED event */
            public byte iscapture;    /**< zero if an output device, non-zero if a capture device. */
            public byte padding1;
            public byte padding2;
            public byte padding3;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_TouchFingerEvent
        {
            public UInt32 type;        /**< ::SDL_FINGERMOTION or ::SDL_FINGERDOWN or ::SDL_FINGERUP */
            public UInt32 timestamp;
            public SDL_TouchID touchId; /**< The touch device id */
            public SDL_FingerID fingerId;
            public float x;            /**< Normalized in the range 0...1 */
            public float y;            /**< Normalized in the range 0...1 */
            public float dx;           /**< Normalized in the range 0...1 */
            public float dy;           /**< Normalized in the range 0...1 */
            public float pressure;     /**< Normalized in the range 0...1 */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_TouchFingerEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_TouchFingerEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_MultiGestureEvent
        {
            public UInt32 type;        /**< ::SDL_MULTIGESTURE */
            public UInt32 timestamp;
            public SDL_TouchID touchId; /**< The touch device index */
            public float dTheta;
            public float dDist;
            public float x;
            public float y;
            public UInt16 numFingers;
            public UInt16 padding;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_MultiGestureEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_MultiGestureEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_DollarGestureEvent
        {
            public UInt32 type;        /**< ::SDL_DOLLARGESTURE */
            public UInt32 timestamp;
            public SDL_TouchID touchId; /**< The touch device id */
            public SDL_GestureID gestureId;
            public UInt32 numFingers;
            public float error;
            public float x;            /**< Normalized center of gesture */
            public float y;            /**< Normalized center of gesture */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_DollarGestureEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_DollarGestureEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_DropEvent
        {
            public UInt32 type;        /**< ::SDL_DROPFILE */
            public UInt32 timestamp;
            public IntPtr file;         /**< The file name, which should be freed with SDL_free() */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_DropEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_DropEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_SensorEvent
        {
            public UInt32 type;        /**< ::SDL_SENSORUPDATE */
            public UInt32 timestamp;   /**< In milliseconds, populated using SDL_GetTicks() */
            public UInt32 which;       /**< The instance ID of the sensor */
            public unsafe fixed float data[6];      /**< Up to 6 values from the sensor - additional values can be queried using SDL_SensorGetData() */
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_QuitEvent
        {
            public UInt32 type;        /**< ::SDL_QUIT */
            public UInt32 timestamp;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_QuitEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_QuitEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_OSEvent
        {
            public UInt32 type;        /**< ::SDL_QUIT */
            public UInt32 timestamp;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_OSEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_OSEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_UserEvent
        {
            public UInt32 type;        /**< ::SDL_USEREVENT through ::SDL_LASTEVENT-1 */
            public UInt32 timestamp;
            public UInt32 windowID;    /**< The associated window if any */
            public Int32 code;        /**< User defined event code */
            public IntPtr data1;        /**< User defined data pointer */
            public IntPtr data2;        /**< User defined data pointer */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_UserEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_UserEvent e) =>
               *((SDL_CommonEvent*)&e);
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_SysWMEvent
        {
            public UInt32 type;        /**< ::SDL_SYSWMEVENT */
            public UInt32 timestamp;
            public IntPtr msg;  /**< driver dependent data, defined in SDL_syswm.h */

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_Event(SDL_SysWMEvent e) =>
               *((SDL_Event*)&e);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static unsafe implicit operator SDL_CommonEvent(SDL_SysWMEvent e) =>
               *((SDL_CommonEvent*)&e);
        }

        private delegate void PumpEventsT();
        private static readonly PumpEventsT SSSdlPumpEventsT = __LoadFunction<PumpEventsT>("SDL_PumpEvents");
        public static void SDL_PumpEvents() => SSSdlPumpEventsT();

        private delegate int PeepEventsSdlEventIntSdlEventactionUInt32UInt32T(ref SDL_Event events, int numevents, SDL_eventaction action, UInt32 minType, UInt32 maxType);
        private static readonly PeepEventsSdlEventIntSdlEventactionUInt32UInt32T SSSdlPeepEventsSdlEventIntSdlEventactionUInt32UInt32T = __LoadFunction<PeepEventsSdlEventIntSdlEventactionUInt32UInt32T>("SDL_PeepEvents");
        public static int SDL_PeepEvents(ref SDL_Event events, int numevents, SDL_eventaction action, UInt32 minType, UInt32 maxType) => SSSdlPeepEventsSdlEventIntSdlEventactionUInt32UInt32T(ref events, numevents, action, minType, maxType);

        private delegate SDL_bool HasEventUInt32T(UInt32 type);
        private static readonly HasEventUInt32T SSSdlHasEventUInt32T = __LoadFunction<HasEventUInt32T>("SDL_HasEvent");
        public static SDL_bool SDL_HasEvent(UInt32 type) => SSSdlHasEventUInt32T(type);

        private delegate SDL_bool HasEventsUInt32UInt32T(UInt32 minType, UInt32 maxType);
        private static readonly HasEventsUInt32UInt32T SSSdlHasEventsUInt32UInt32T = __LoadFunction<HasEventsUInt32UInt32T>("SDL_HasEvents");
        public static SDL_bool SDL_HasEvents(UInt32 minType, UInt32 maxType) => SSSdlHasEventsUInt32UInt32T(minType, maxType);

        private delegate void FlushEventUInt32T(UInt32 type);
        private static readonly FlushEventUInt32T SSSdlFlushEventUInt32T = __LoadFunction<FlushEventUInt32T>("SDL_FlushEvent");
        public static void SDL_FlushEvent(UInt32 type) => SSSdlFlushEventUInt32T(type);

        private delegate void FlushEventsUInt32UInt32T(UInt32 minType, UInt32 maxType);
        private static readonly FlushEventsUInt32UInt32T SSSdlFlushEventsUInt32UInt32T = __LoadFunction<FlushEventsUInt32UInt32T>("SDL_FlushEvents");
        public static void SDL_FlushEvents(UInt32 minType, UInt32 maxType) => SSSdlFlushEventsUInt32UInt32T(minType, maxType);

        private delegate int PollEventSdlEventT(out SDL_Event @event);
        private static readonly PollEventSdlEventT SSSdlPollEventSdlEventT = __LoadFunction<PollEventSdlEventT>("SDL_PollEvent");
        public static int SDL_PollEvent(out SDL_Event @event) => SSSdlPollEventSdlEventT(out @event);

        private delegate int WaitEventSdlEventT(ref SDL_Event @event);
        private static readonly WaitEventSdlEventT SSSdlWaitEventSdlEventT = __LoadFunction<WaitEventSdlEventT>("SDL_WaitEvent");
        public static int SDL_WaitEvent(ref SDL_Event @event) => SSSdlWaitEventSdlEventT(ref @event);

        private delegate int WaitEventTimeoutSdlEventIntT(ref SDL_Event @event, int timeout);
        private static readonly WaitEventTimeoutSdlEventIntT SSSdlWaitEventTimeoutSdlEventIntT = __LoadFunction<WaitEventTimeoutSdlEventIntT>("SDL_WaitEventTimeout");
        public static int SDL_WaitEventTimeout(ref SDL_Event @event, int timeout) => SSSdlWaitEventTimeoutSdlEventIntT(ref @event, timeout);

        private delegate int PushEventSdlEventT(ref SDL_Event @event);
        private static readonly PushEventSdlEventT SSSdlPushEventSdlEventT = __LoadFunction<PushEventSdlEventT>("SDL_PushEvent");
        public static int SDL_PushEvent(ref SDL_Event @event) => SSSdlPushEventSdlEventT(ref @event);

        private delegate void SetEventFilterSdlEventFilterIntPtrT(SDL_EventFilter filter, IntPtr userdata);
        private static readonly SetEventFilterSdlEventFilterIntPtrT SSSdlSetEventFilterSdlEventFilterIntPtrT = __LoadFunction<SetEventFilterSdlEventFilterIntPtrT>("SDL_SetEventFilter");
        public static void SDL_SetEventFilter(SDL_EventFilter filter, IntPtr userdata) => SSSdlSetEventFilterSdlEventFilterIntPtrT(filter, userdata);

        private delegate SDL_bool GetEventFilterSdlEventFilterIntPtrT(ref SDL_EventFilter filter, out IntPtr userdata);
        private static readonly GetEventFilterSdlEventFilterIntPtrT SSSdlGetEventFilterSdlEventFilterIntPtrT = __LoadFunction<GetEventFilterSdlEventFilterIntPtrT>("SDL_GetEventFilter");

        static SDL_bool _SDL_GetEventFilter(ref SDL_EventFilter filter, out IntPtr userdata) => SSSdlGetEventFilterSdlEventFilterIntPtrT(ref filter, out userdata);
        public static SDL_bool SDL_GetEventFilter(ref SDL_EventFilter filter, out IntPtr userdata)
        {
            var result = _SDL_GetEventFilter(ref filter, out IntPtr userdataPtr);
            userdata = result == 0 ? IntPtr.Zero : Marshal.PtrToStructure<IntPtr>(userdataPtr);
            return result;
        }

        private delegate void AddEventWatchSdlEventFilterIntPtrT(SDL_EventFilter filter, IntPtr userdata);
        private static readonly AddEventWatchSdlEventFilterIntPtrT SSSdlAddEventWatchSdlEventFilterIntPtrT = __LoadFunction<AddEventWatchSdlEventFilterIntPtrT>("SDL_AddEventWatch");
        public static void SDL_AddEventWatch(SDL_EventFilter filter, IntPtr userdata) => SSSdlAddEventWatchSdlEventFilterIntPtrT(filter, userdata);

        private delegate void DelEventWatchSdlEventFilterIntPtrT(SDL_EventFilter filter, IntPtr userdata);
        private static readonly DelEventWatchSdlEventFilterIntPtrT SSSdlDelEventWatchSdlEventFilterIntPtrT = __LoadFunction<DelEventWatchSdlEventFilterIntPtrT>("SDL_DelEventWatch");
        public static void SDL_DelEventWatch(SDL_EventFilter filter, IntPtr userdata) => SSSdlDelEventWatchSdlEventFilterIntPtrT(filter, userdata);

        private delegate void FilterEventsSdlEventFilterIntPtrT(SDL_EventFilter filter, IntPtr userdata);
        private static readonly FilterEventsSdlEventFilterIntPtrT SSSdlFilterEventsSdlEventFilterIntPtrT = __LoadFunction<FilterEventsSdlEventFilterIntPtrT>("SDL_FilterEvents");
        public static void SDL_FilterEvents(SDL_EventFilter filter, IntPtr userdata) => SSSdlFilterEventsSdlEventFilterIntPtrT(filter, userdata);

        private delegate byte EventStateUInt32IntT(UInt32 type, int state);
        private static readonly EventStateUInt32IntT SSSdlEventStateUInt32IntT = __LoadFunction<EventStateUInt32IntT>("SDL_EventState");
        public static byte SDL_EventState(UInt32 type, int state) => SSSdlEventStateUInt32IntT(type, state);

        private delegate uint RegisterEventsIntT(int numevents);
        private static readonly RegisterEventsIntT SSSdlRegisterEventsIntT = __LoadFunction<RegisterEventsIntT>("SDL_RegisterEvents");
        public static uint SDL_RegisterEvents(int numEvents) => SSSdlRegisterEventsIntT(numEvents);
        
        private static T __LoadFunction<T>(string name) { return Internal.LoaderSdl2.LoadFunction<T>(name); }
    }
}
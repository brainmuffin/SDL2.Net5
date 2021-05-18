using System;
using System.Runtime.InteropServices;

using static SDL2.Net5.SdlKeycode;
using static SDL2.Net5.SdlPointRect;
using static SDL2.Net5.SdlScancode;

using SDL_bool = System.Int32;

namespace SDL2.Net5
{
    public class SdlKeyboard
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_Keysym
        {
            public SdlScanCode scancode;      /**< SDL physical key code - see ::SdlScanCode for details */
            public SdlKeyCode sym;            /**< SDL virtual key code - see ::SdlKeyCode for details */
            public UInt16 mod;                 /**< current key modifiers */
            public UInt32 unused;
        }

        private delegate IntPtr GetKeyboardFocusT();
        private static readonly GetKeyboardFocusT SSSdlGetKeyboardFocusT = __LoadFunction<GetKeyboardFocusT>("SDL_GetKeyboardFocus");
        public static IntPtr SDL_GetKeyboardFocus() => SSSdlGetKeyboardFocusT();

        private delegate IntPtr GetKeyboardStateIntT(ref int numkeys);
        private static readonly GetKeyboardStateIntT SSSdlGetKeyboardStateIntT = __LoadFunction<GetKeyboardStateIntT>("SDL_GetKeyboardState");
        public static IntPtr SDL_GetKeyboardState(ref int numkeys) => SSSdlGetKeyboardStateIntT(ref numkeys);

        private delegate SdlKeymod GetModStateT();
        private static readonly GetModStateT SSSdlGetModStateT = __LoadFunction<GetModStateT>("SDL_GetModState");
        public static SdlKeymod SDL_GetModState() => SSSdlGetModStateT();

        private delegate void SetModStateSdlKeymodT(SdlKeymod modstate);
        private static readonly SetModStateSdlKeymodT SSSdlSetModStateSdlKeymodT = __LoadFunction<SetModStateSdlKeymodT>("SDL_SetModState");
        public static void SDL_SetModState(SdlKeymod modstate) => SSSdlSetModStateSdlKeymodT(modstate);

        private delegate SdlKeyCode GetKeyFromScancodeSdlScanCodeT(SdlScanCode scancode);
        private static readonly GetKeyFromScancodeSdlScanCodeT SSSdlGetKeyFromScancodeSdlScanCodeT = __LoadFunction<GetKeyFromScancodeSdlScanCodeT>("SDL_GetKeyFromScancode");
        public static SdlKeyCode SDL_GetKeyFromScancode(SdlScanCode scancode) => SSSdlGetKeyFromScancodeSdlScanCodeT(scancode);

        private delegate SdlScanCode GetScancodeFromKeySdlKeyCodeT(SdlKeyCode key);
        private static readonly GetScancodeFromKeySdlKeyCodeT SSSdlGetScancodeFromKeySdlKeyCodeT = __LoadFunction<GetScancodeFromKeySdlKeyCodeT>("SDL_GetScancodeFromKey");
        public static SdlScanCode SDL_GetScancodeFromKey(SdlKeyCode key) => SSSdlGetScancodeFromKeySdlKeyCodeT(key);

        private delegate IntPtr GetScancodeNameSdlScanCodeT(SdlScanCode scancode);
        private static readonly GetScancodeNameSdlScanCodeT SSSdlGetScancodeNameSdlScanCodeT = __LoadFunction<GetScancodeNameSdlScanCodeT>("SDL_GetScancodeName");
        public static IntPtr SDL_GetScancodeName(SdlScanCode scancode) => SSSdlGetScancodeNameSdlScanCodeT(scancode);

        private delegate SdlScanCode GetScancodeFromNameIntPtrT(IntPtr name);
        private static readonly GetScancodeFromNameIntPtrT SSSdlGetScancodeFromNameIntPtrT = __LoadFunction<GetScancodeFromNameIntPtrT>("SDL_GetScancodeFromName");
        public static SdlScanCode SDL_GetScancodeFromName(IntPtr name) => SSSdlGetScancodeFromNameIntPtrT(name);

        private delegate IntPtr GetKeyNameSdlKeyCodeT(SdlKeyCode key);
        private static readonly GetKeyNameSdlKeyCodeT SSSdlGetKeyNameSdlKeyCodeT = __LoadFunction<GetKeyNameSdlKeyCodeT>("SDL_GetKeyName");
        public static IntPtr SDL_GetKeyName(SdlKeyCode key) => SSSdlGetKeyNameSdlKeyCodeT(key);

        private delegate SdlKeyCode GetKeyFromNameIntPtrT(IntPtr name);
        private static readonly GetKeyFromNameIntPtrT SSSdlGetKeyFromNameIntPtrT = __LoadFunction<GetKeyFromNameIntPtrT>("SDL_GetKeyFromName");
        public static SdlKeyCode SDL_GetKeyFromName(IntPtr name) => SSSdlGetKeyFromNameIntPtrT(name);

        private delegate void StartTextInputT();
        private static readonly StartTextInputT SSSdlStartTextInputT = __LoadFunction<StartTextInputT>("SDL_StartTextInput");
        public static void SDL_StartTextInput() => SSSdlStartTextInputT();

        private delegate SDL_bool IsTextInputActiveT();
        private static readonly IsTextInputActiveT SSSdlIsTextInputActiveT = __LoadFunction<IsTextInputActiveT>("SDL_IsTextInputActive");
        public static SDL_bool SDL_IsTextInputActive() => SSSdlIsTextInputActiveT();

        private delegate void StopTextInputT();
        private static readonly StopTextInputT SSSdlStopTextInputT = __LoadFunction<StopTextInputT>("SDL_StopTextInput");
        public static void SDL_StopTextInput() => SSSdlStopTextInputT();

        private delegate void SetTextInputRectSdlRectT(ref SdlRect rect);
        private static readonly SetTextInputRectSdlRectT SSSdlSetTextInputRectSdlRectT = __LoadFunction<SetTextInputRectSdlRectT>("SDL_SetTextInputRect");
        public static void SDL_SetTextInputRect(ref SdlRect rect) => SSSdlSetTextInputRectSdlRectT(ref rect);

        private delegate SDL_bool HasScreenKeyboardSupportT();
        private static readonly HasScreenKeyboardSupportT SSSdlHasScreenKeyboardSupportT = __LoadFunction<HasScreenKeyboardSupportT>("SDL_HasScreenKeyboardSupport");
        public static SDL_bool SDL_HasScreenKeyboardSupport() => SSSdlHasScreenKeyboardSupportT();

        private delegate SDL_bool IsScreenKeyboardShownIntPtrT(IntPtr window);
        private static readonly IsScreenKeyboardShownIntPtrT SSSdlIsScreenKeyboardShownIntPtrT = __LoadFunction<IsScreenKeyboardShownIntPtrT>("SDL_IsScreenKeyboardShown");
        public static SDL_bool SDL_IsScreenKeyboardShown(IntPtr window) => SSSdlIsScreenKeyboardShownIntPtrT(window);
        private static T __LoadFunction<T>(string name) { return Internal.LoaderSdl2.LoadFunction<T>(name); }
    }
}
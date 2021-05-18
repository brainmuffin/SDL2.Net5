using System;

using SDL2.Net5.Internal;

namespace SDL2.Net5.Extensions
{
    public class SdlTtf
    {
        public static string TTF_GetError() => SdlError.SDL_GetError();
        
        private delegate void TtfLinkedVersionT();
        private static readonly TtfLinkedVersionT SSTtfLinkedVersionT = __LoadFunction<TtfLinkedVersionT>("TTF_Linked_Version");
        public static void TTF_Linked_Version() => SSTtfLinkedVersionT();
        
        private delegate void TtfInitT();
        private static readonly TtfInitT SSTtfInitT = __LoadFunction<TtfInitT>("TTF_Init");
        public static void TTF_Init() => SSTtfInitT();
        
        private delegate void TtfQuitT();
        private static readonly TtfQuitT SSTtfQuitT = __LoadFunction<TtfQuitT>("TTF_Quit");
        public static void TTF_Quit() => SSTtfQuitT();
        
        private delegate IntPtr TtfOpenFontIntPtrIntT(IntPtr file, int size);
        private static readonly TtfOpenFontIntPtrIntT SSTtfOpenFontIntPtrIntT = __LoadFunction<TtfOpenFontIntPtrIntT>("TTF_OpenFont");
        public static IntPtr TTF_OpenFont(string file, int size) => SSTtfOpenFontIntPtrIntT(Util.StringToHGlobalUTF8(file), size);

        private delegate void TtfCloseFontIntPtrT(IntPtr font);
        private static readonly TtfCloseFontIntPtrT SSTtfCloseFontIntPtrT = __LoadFunction<TtfCloseFontIntPtrT>("TTF_CloseFont");
        public static void TTF_CloseFont(IntPtr font) => SSTtfCloseFontIntPtrT(font);

        private delegate IntPtr TtfRenderTextSolidIntPtrIntPtrColorT(IntPtr font, IntPtr text,
            SdlPixels.SdlColor color);
        private static readonly TtfRenderTextSolidIntPtrIntPtrColorT SSTtfRenderTextSolidIntPtrIntPtrColorT =
            __LoadFunction<TtfRenderTextSolidIntPtrIntPtrColorT>("TTF_RenderText_Solid");
        public static IntPtr TTF_RenderText_Solid(IntPtr font, string text, SdlPixels.SdlColor foreground) => SSTtfRenderTextSolidIntPtrIntPtrColorT(font, Util.StringToHGlobalUTF8(text), foreground);
        
        private static T __LoadFunction<T>(string name)
            where T : class
        {
            if (LoaderSdl2.SdlTff == null) return null;
            try
            {
                return LoaderSdl2.SdlTff.LoadFunction<T>(name);
            }
#pragma warning disable
            catch (Exception ex)
            {
                return null;
            }
#pragma warning enable
        }
    }
}
using System;
using SDL2.Net5.Internal;

namespace SDL2.Net5.Extensions
{
    public class SdlImage
    {
        public enum IMG_InitFlags
        {
            IMG_INIT_JPG = 0x00000001,
            IMG_INIT_PNG = 0x00000002,
            IMG_INIT_TIF = 0x00000004,
            IMG_INIT_WEBP = 0x00000008
        }
        
        private delegate IntPtr ImgLoadT(IntPtr file);
        private static readonly ImgLoadT SImgLoad = __LoadFunction<ImgLoadT>("IMG_Load");
        public static IntPtr IMG_Load(string file) => SImgLoad(Util.StringToHGlobalUTF8(file));
        
        public static string IMG_GetError() => SdlError.SDL_GetError();
        
        private delegate int ImgInitT();
        private static readonly ImgInitT SImgInitT = __LoadFunction<ImgInitT>("IMG_Init");
        public static int IMG_Init(int imgInitPng) => SImgInitT();
        
        private delegate void ImgQuitT();
        private static readonly ImgQuitT SImgQuit = __LoadFunction<ImgQuitT>("IMG_Quit");
        public static void IMG_Quit() => SImgQuit();

        private static T __LoadFunction<T>(string name)
            where T : class
        {
            if (LoaderSdl2.SdlImage == null) return null;
            try
            {
                return LoaderSdl2.SdlImage.LoadFunction<T>(name);
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
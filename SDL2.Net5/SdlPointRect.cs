using System.Runtime.InteropServices;
using SDL_bool = System.Int32;

namespace SDL2.Net5
{
    public static class SdlPointRect
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SdlPoint
        {
            public int x;
            public int y;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SdlRect
        {
            public int x, y;
            public int w, h;
        }

        private delegate SDL_bool SdlHasIntersectionSdlRectSdlRectT(ref SdlRect A, ref SdlRect B);
        private static readonly SdlHasIntersectionSdlRectSdlRectT SSdlHasIntersectionSdlRectSdlRectT = __LoadFunction<SdlHasIntersectionSdlRectSdlRectT>("SDL_HasIntersection");
        public static SDL_bool SDL_HasIntersection(ref SdlRect A, ref SdlRect B) => SSdlHasIntersectionSdlRectSdlRectT(ref A, ref B);

        private delegate SDL_bool SdlIntersectRectSdlRectSdlRectSdlRectT(ref SdlRect A, ref SdlRect B, ref SdlRect result);
        private static readonly SdlIntersectRectSdlRectSdlRectSdlRectT SSdlIntersectRectSdlRectSdlRectSdlRectT = __LoadFunction<SdlIntersectRectSdlRectSdlRectSdlRectT>("SDL_IntersectRect");
        public static SDL_bool SDL_IntersectRect(ref SdlRect A, ref SdlRect B, ref SdlRect result) => SSdlIntersectRectSdlRectSdlRectSdlRectT(ref A, ref B, ref result);

        private delegate void SdlUnionRectSdlRectSdlRectSdlRectT(ref SdlRect A, ref SdlRect B, ref SdlRect result);
        private static readonly SdlUnionRectSdlRectSdlRectSdlRectT SSdlUnionRectSdlRectSdlRectSdlRectT = __LoadFunction<SdlUnionRectSdlRectSdlRectSdlRectT>("SDL_UnionRect");
        public static void SDL_UnionRect(ref SdlRect A, ref SdlRect B, ref SdlRect result) => SSdlUnionRectSdlRectSdlRectSdlRectT(ref A, ref B, ref result);

        private delegate SDL_bool SdlEnclosePointsSdlPointIntSdlRectSdlRectT(ref SdlPoint points, int count, ref SdlRect clip, ref SdlRect result);
        private static readonly SdlEnclosePointsSdlPointIntSdlRectSdlRectT SSdlEnclosePointsSdlPointIntSdlRectSdlRectT = __LoadFunction<SdlEnclosePointsSdlPointIntSdlRectSdlRectT>("SDL_EnclosePoints");
        public static SDL_bool SDL_EnclosePoints(ref SdlPoint points, int count, ref SdlRect clip, ref SdlRect result) => SSdlEnclosePointsSdlPointIntSdlRectSdlRectT(ref points, count, ref clip, ref result);

        private delegate SDL_bool SdlIntersectRectAndLineSdlRectIntIntIntIntT(ref SdlRect rect, ref int X1, ref int Y1, ref int X2, ref int Y2);
        private static readonly SdlIntersectRectAndLineSdlRectIntIntIntIntT SSdlIntersectRectAndLineSdlRectIntIntIntIntT = __LoadFunction<SdlIntersectRectAndLineSdlRectIntIntIntIntT>("SDL_IntersectRectAndLine");
        public static SDL_bool SDL_IntersectRectAndLine(ref SdlRect rect, ref int X1, ref int Y1, ref int X2, ref int Y2) => SSdlIntersectRectAndLineSdlRectIntIntIntIntT(ref rect, ref X1, ref Y1, ref X2, ref Y2);
        
        private static T __LoadFunction<T>(string name) { return Internal.LoaderSdl2.LoadFunction<T>(name); }
    }
}
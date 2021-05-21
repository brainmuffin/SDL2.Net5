using System;
using System.Runtime.InteropServices;
using SDL_AudioFormat = System.UInt32;
using SDL_AudioCallback = System.IntPtr;
using SDL_AudioFilter = System.IntPtr;
using SDL_AudioDeviceID = System.UInt32;
using SDL_RWops = System.IntPtr;

namespace SDL2.Net5
{
    public class SdlAudio
    {
        public const int SDL_AUDIO_MASK_BITSIZE = 0xFF;
        public const int SDL_AUDIO_MASK_DATATYPE = 1 << 8;
        public const int SDL_AUDIO_MASK_ENDIAN = 1 << 12;
        public const int SDL_AUDIO_MASK_SIGNED = 1 << 15;
        public const int AUDIO_U8 = 0x0008;
        public const int AUDIO_S8 = 0x8008;
        public const int AUDIO_U16LSB = 0x0010;
        public const int AUDIO_S16LSB = 0x8010;
        public const int AUDIO_U16MSB = 0x1010;
        public const int AUDIO_S16MSB = 0x9010;
        public const int AUDIO_U16 = AUDIO_U16LSB;
        public const int AUDIO_S16 = AUDIO_S16LSB;
        public const int AUDIO_S32LSB = 0x8020;
        public const int AUDIO_S32MSB = 0x9020;
        public const int AUDIO_S32 = AUDIO_S32LSB;
        public const int AUDIO_F32LSB = 0x8120;
        public const int AUDIO_F32MSB = 0x9120;
        public const int AUDIO_F32 = AUDIO_F32LSB;
        public const int AUDIO_U16SYS = AUDIO_U16LSB;
        public const int AUDIO_S16SYS = AUDIO_S16LSB;
        public const int AUDIO_S32SYS = AUDIO_S32LSB;
        public const int AUDIO_F32SYS = AUDIO_F32LSB;
        public const int SDL_AUDIO_ALLOW_FREQUENCY_CHANGE = 0x00000001;
        public const int SDL_AUDIO_ALLOW_FORMAT_CHANGE = 0x00000002;
        public const int SDL_AUDIO_ALLOW_CHANNELS_CHANGE = 0x00000004;
        public const int SDL_AUDIO_ALLOW_ANY_CHANGE = SDL_AUDIO_ALLOW_FREQUENCY_CHANGE | SDL_AUDIO_ALLOW_FORMAT_CHANGE | SDL_AUDIO_ALLOW_CHANNELS_CHANGE;
        public const int SDL_MIX_MAXVOLUME = 128;

        public enum SDL_AudioStatus
        {
            SDL_AUDIO_STOPPED = 0,
            SDL_AUDIO_PLAYING,
            SDL_AUDIO_PAUSED
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_AudioSpec
        {
            public int freq;                   /**< DSP frequency -- samples per second */
            public SDL_AudioFormat format;     /**< Audio data format */
            public byte channels;             /**< Number of channels: 1 mono, 2 stereo */
            public byte silence;              /**< Audio buffer silence value (calculated) */
            public UInt16 samples;             /**< Audio buffer size in samples (power of 2) */
            public UInt16 padding;             /**< Necessary for some compile environments */
            public UInt32 size;                /**< Audio buffer size in bytes (calculated) */
            public SDL_AudioCallback callback;
            public IntPtr userdata;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_AudioCVT
        {
            public int needed;                 /**< Set to 1 if conversion possible */
            public SDL_AudioFormat src_format; /**< Source audio format */
            public SDL_AudioFormat dst_format; /**< Target audio format */
            public double rate_incr;           /**< Rate conversion increment */
            public IntPtr buf;                 /**< Buffer to hold entire audio data */
            public int len;                    /**< Length of original audio buffer */
            public int len_cvt;                /**< Length of converted audio buffer */
            public int len_mult;               /**< buffer must be len*len_mult big */
            public double len_ratio;           /**< Given len, final size is len*len_ratio */
            // public unsafe fixed IntPtr filters[10];        /**< Filter list */
            public IntPtr filter_1;
            public IntPtr filter_2;
            public IntPtr filter_3;
            public IntPtr filter_4;
            public IntPtr filter_5;
            public IntPtr filter_6;
            public IntPtr filter_7;
            public IntPtr filter_8;
            public IntPtr filter_9;
            public IntPtr filter_10;
            public int filter_index;           /**< Current audio conversion function */
        }

        private delegate int GetNumAudioDriversT();
        private static readonly GetNumAudioDriversT SSSdlGetNumAudioDriversT = __LoadFunction<GetNumAudioDriversT>("SDL_GetNumAudioDrivers");
        public static int SDL_GetNumAudioDrivers() => SSSdlGetNumAudioDriversT();

        private delegate IntPtr GetAudioDriverIntT(int index);
        private static readonly GetAudioDriverIntT SSSdlGetAudioDriverIntT = __LoadFunction<GetAudioDriverIntT>("SDL_GetAudioDriver");
        private static IntPtr _SDL_GetAudioDriver(int index) => SSSdlGetAudioDriverIntT(index);
        public static string SDL_GetAudioDriver(int index) =>
            Marshal.PtrToStringAnsi(_SDL_GetAudioDriver(index));

        private delegate int AudioInitIntPtrT(IntPtr driverName);
        private static readonly AudioInitIntPtrT SSSdlAudioInitIntPtrT = __LoadFunction<AudioInitIntPtrT>("SDL_AudioInit");
        public static int SDL_AudioInit(IntPtr driverName) => SSSdlAudioInitIntPtrT(driverName);

        private delegate void AudioQuitT();
        private static readonly AudioQuitT SSSdlAudioQuitT = __LoadFunction<AudioQuitT>("SDL_AudioQuit");
        public static void SDL_AudioQuit() => SSSdlAudioQuitT();

        private delegate IntPtr GetCurrentAudioDriverT();
        private static readonly GetCurrentAudioDriverT SSSdlGetCurrentAudioDriverT = __LoadFunction<GetCurrentAudioDriverT>("SDL_GetCurrentAudioDriver");
        private static IntPtr _SDL_GetCurrentAudioDriver() => SSSdlGetCurrentAudioDriverT();
        public static string SDL_GetCurrentAudioDriver() => Marshal.PtrToStringAnsi(_SDL_GetCurrentAudioDriver());

        private delegate int OpenAudioSdlAudioSpecSdlAudioSpecT(ref SDL_AudioSpec desired, ref SDL_AudioSpec obtained);
        private static readonly OpenAudioSdlAudioSpecSdlAudioSpecT SSSdlOpenAudioSdlAudioSpecSdlAudioSpecT = __LoadFunction<OpenAudioSdlAudioSpecSdlAudioSpecT>("SDL_OpenAudio");
        public static int SDL_OpenAudio(ref SDL_AudioSpec desired, ref SDL_AudioSpec obtained) => SSSdlOpenAudioSdlAudioSpecSdlAudioSpecT(ref desired, ref obtained);

        private delegate int GetNumAudioDevicesIntT(int isCapture);
        private static readonly GetNumAudioDevicesIntT SSSdlGetNumAudioDevicesIntT = __LoadFunction<GetNumAudioDevicesIntT>("SDL_GetNumAudioDevices");
        public static int SDL_GetNumAudioDevices(int isCapture) => SSSdlGetNumAudioDevicesIntT(isCapture);

        private delegate IntPtr GetAudioDeviceNameIntIntT(int index, int isCapture);
        private static readonly GetAudioDeviceNameIntIntT SSSdlGetAudioDeviceNameIntIntT = __LoadFunction<GetAudioDeviceNameIntIntT>("SDL_GetAudioDeviceName");
        public static IntPtr SDL_GetAudioDeviceName(int index, int isCapture) => SSSdlGetAudioDeviceNameIntIntT(index, isCapture);

        private delegate SDL_AudioDeviceID OpenAudioDeviceIntPtrIntSdlAudioSpecSdlAudioSpecIntT(IntPtr device, int isCapture, ref SDL_AudioSpec desired, ref SDL_AudioSpec obtained, int allowedChanges);
        private static readonly OpenAudioDeviceIntPtrIntSdlAudioSpecSdlAudioSpecIntT SSSdlOpenAudioDeviceIntPtrIntSdlAudioSpecSdlAudioSpecIntT = __LoadFunction<OpenAudioDeviceIntPtrIntSdlAudioSpecSdlAudioSpecIntT>("SDL_OpenAudioDevice");
        public static SDL_AudioDeviceID SDL_OpenAudioDevice(IntPtr device, int isCapture, ref SDL_AudioSpec desired, ref SDL_AudioSpec obtained, int allowedChanges) => SSSdlOpenAudioDeviceIntPtrIntSdlAudioSpecSdlAudioSpecIntT(device, isCapture, ref desired, ref obtained, allowedChanges);

        private delegate SDL_AudioStatus GetAudioStatusT();
        private static readonly GetAudioStatusT SSSdlGetAudioStatusT = __LoadFunction<GetAudioStatusT>("SDL_GetAudioStatus");
        public static SDL_AudioStatus SDL_GetAudioStatus() => SSSdlGetAudioStatusT();

        private delegate SDL_AudioStatus GetAudioDeviceStatusSdlAudioDeviceIdT(SDL_AudioDeviceID dev);
        private static readonly GetAudioDeviceStatusSdlAudioDeviceIdT SSSdlGetAudioDeviceStatusSdlAudioDeviceIdT = __LoadFunction<GetAudioDeviceStatusSdlAudioDeviceIdT>("SDL_GetAudioDeviceStatus");
        public static SDL_AudioStatus SDL_GetAudioDeviceStatus(SDL_AudioDeviceID dev) => SSSdlGetAudioDeviceStatusSdlAudioDeviceIdT(dev);

        private delegate void PauseAudioIntT(int pauseOn);
        private static readonly PauseAudioIntT SSSdlPauseAudioIntT = __LoadFunction<PauseAudioIntT>("SDL_PauseAudio");
        public static void SDL_PauseAudio(int pauseOn) => SSSdlPauseAudioIntT(pauseOn);

        private delegate void PauseAudioDeviceSdlAudioDeviceIdIntT(SDL_AudioDeviceID dev, int pauseOn);
        private static readonly PauseAudioDeviceSdlAudioDeviceIdIntT SSSdlPauseAudioDeviceSdlAudioDeviceIdIntT = __LoadFunction<PauseAudioDeviceSdlAudioDeviceIdIntT>("SDL_PauseAudioDevice");
        public static void SDL_PauseAudioDevice(SDL_AudioDeviceID dev, int pauseOn) => SSSdlPauseAudioDeviceSdlAudioDeviceIdIntT(dev, pauseOn);

        private delegate IntPtr LoadWavRwSdlRWopsIntSdlAudioSpecIntPtrUintT(IntPtr src, int freeSrc, ref SDL_AudioSpec spec, IntPtr audioBuf, ref uint audioLen);
        private static readonly LoadWavRwSdlRWopsIntSdlAudioSpecIntPtrUintT SSSdlLoadWavRwSdlRWopsIntSdlAudioSpecIntPtrUintT = __LoadFunction<LoadWavRwSdlRWopsIntSdlAudioSpecIntPtrUintT>("SDL_LoadWAV_RW");
        public static IntPtr SDL_LoadWAV_RW(IntPtr src, int freeSrc, ref SDL_AudioSpec spec, IntPtr audioBuf, ref uint audioLen) => SSSdlLoadWavRwSdlRWopsIntSdlAudioSpecIntPtrUintT(src, freeSrc, ref spec, audioBuf, ref audioLen);

        private delegate void FreeWavByteT(ref byte audioBuf);
        private static readonly FreeWavByteT SSSdlFreeWavByteT = __LoadFunction<FreeWavByteT>("SDL_FreeWAV");
        public static void SDL_FreeWAV(ref byte audioBuf) => SSSdlFreeWavByteT(ref audioBuf);

        private delegate int BuildAudioCvtSdlAudioCvtSdlAudioFormatByteIntSdlAudioFormatByteIntT(ref SDL_AudioCVT cvt, SDL_AudioFormat srcFormat, byte srcChannels, int srcRate, SDL_AudioFormat dstFormat, byte dstChannels, int dstRate);
        private static readonly BuildAudioCvtSdlAudioCvtSdlAudioFormatByteIntSdlAudioFormatByteIntT SSSdlBuildAudioCvtSdlAudioCvtSdlAudioFormatByteIntSdlAudioFormatByteIntT = __LoadFunction<BuildAudioCvtSdlAudioCvtSdlAudioFormatByteIntSdlAudioFormatByteIntT>("SDL_BuildAudioCVT");
        public static int SDL_BuildAudioCVT(ref SDL_AudioCVT cvt, SDL_AudioFormat srcFormat, byte srcChannels, int srcRate, SDL_AudioFormat dstFormat, byte dstChannels, int dstRate) => SSSdlBuildAudioCvtSdlAudioCvtSdlAudioFormatByteIntSdlAudioFormatByteIntT(ref cvt, srcFormat, srcChannels, srcRate, dstFormat, dstChannels, dstRate);

        private delegate int ConvertAudioSdlAudioCvtT(ref SDL_AudioCVT cvt);
        private static readonly ConvertAudioSdlAudioCvtT SSSdlConvertAudioSdlAudioCvtT = __LoadFunction<ConvertAudioSdlAudioCvtT>("SDL_ConvertAudio");
        public static int SDL_ConvertAudio(ref SDL_AudioCVT cvt) => SSSdlConvertAudioSdlAudioCvtT(ref cvt);

        private delegate void MixAudioByteByteUInt32IntT(ref byte dst, ref byte src, UInt32 len, int volume);
        private static readonly MixAudioByteByteUInt32IntT SSSdlMixAudioByteByteUInt32IntT = __LoadFunction<MixAudioByteByteUInt32IntT>("SDL_MixAudio");
        public static void SDL_MixAudio(ref byte dst, ref byte src, UInt32 len, int volume) => SSSdlMixAudioByteByteUInt32IntT(ref dst, ref src, len, volume);

        private delegate void MixAudioFormatByteByteSdlAudioFormatUInt32IntT(ref byte dst, ref byte src, SDL_AudioFormat format, UInt32 len, int volume);
        private static readonly MixAudioFormatByteByteSdlAudioFormatUInt32IntT SSSdlMixAudioFormatByteByteSdlAudioFormatUInt32IntT = __LoadFunction<MixAudioFormatByteByteSdlAudioFormatUInt32IntT>("SDL_MixAudioFormat");
        public static void SDL_MixAudioFormat(ref byte dst, ref byte src, SDL_AudioFormat format, UInt32 len, int volume) => SSSdlMixAudioFormatByteByteSdlAudioFormatUInt32IntT(ref dst, ref src, format, len, volume);

        private delegate void LockAudioT();
        private static readonly LockAudioT SSSdlLockAudioT = __LoadFunction<LockAudioT>("SDL_LockAudio");
        public static void SDL_LockAudio() => SSSdlLockAudioT();

        private delegate void LockAudioDeviceSdlAudioDeviceIdT(SDL_AudioDeviceID dev);
        private static readonly LockAudioDeviceSdlAudioDeviceIdT SSSdlLockAudioDeviceSdlAudioDeviceIdT = __LoadFunction<LockAudioDeviceSdlAudioDeviceIdT>("SDL_LockAudioDevice");
        public static void SDL_LockAudioDevice(SDL_AudioDeviceID dev) => SSSdlLockAudioDeviceSdlAudioDeviceIdT(dev);

        private delegate void UnlockAudioT();
        private static readonly UnlockAudioT SSSdlUnlockAudioT = __LoadFunction<UnlockAudioT>("SDL_UnlockAudio");
        public static void SDL_UnlockAudio() => SSSdlUnlockAudioT();

        private delegate void UnlockAudioDeviceSdlAudioDeviceIdT(SDL_AudioDeviceID dev);
        private static readonly UnlockAudioDeviceSdlAudioDeviceIdT SSSdlUnlockAudioDeviceSdlAudioDeviceIdT = __LoadFunction<UnlockAudioDeviceSdlAudioDeviceIdT>("SDL_UnlockAudioDevice");
        public static void SDL_UnlockAudioDevice(SDL_AudioDeviceID dev) => SSSdlUnlockAudioDeviceSdlAudioDeviceIdT(dev);

        private delegate void CloseAudioT();
        private static readonly CloseAudioT SSSdlCloseAudioT = __LoadFunction<CloseAudioT>("SDL_CloseAudio");
        public static void SDL_CloseAudio() => SSSdlCloseAudioT();

        private delegate void CloseAudioDeviceSdlAudioDeviceIdT(SDL_AudioDeviceID dev);
        private static readonly CloseAudioDeviceSdlAudioDeviceIdT SSSdlCloseAudioDeviceSdlAudioDeviceIdT = __LoadFunction<CloseAudioDeviceSdlAudioDeviceIdT>("SDL_CloseAudioDevice");
        public static void SDL_CloseAudioDevice(SDL_AudioDeviceID dev) => SSSdlCloseAudioDeviceSdlAudioDeviceIdT(dev);
        
        private static T __LoadFunction<T>(string name) { return Internal.LoaderSdl2.LoadFunction<T>(name); }
    }
}
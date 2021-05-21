using System;
using System.Runtime.InteropServices;
using static SDL2.Net5.SdlAudio;

using SDL_RWops = System.IntPtr;
using Mix_EffectFunc_t = System.IntPtr;
using Mix_EffectDone_t = System.IntPtr;
using SDL2.Net5.Internal;

namespace SDL2.Net5.Extensions
{
    public class SdlMixer
    {
        public static bool IsSDLMixerLoaded => LoaderSdl2.SdlMixer != null;
        public const int MIX_CHANNELS = 8;
        public const int MIX_DEFAULT_FREQUENCY = 22050;
        public const int MIX_DEFAULT_FORMAT = AUDIO_S16LSB;
        public const int MIX_DEFAULT_CHANNELS = 2;
        public const int MIX_MAX_VOLUME = 128;
        public const int MIX_CHANNEL_POST = -2;
        public const string MIX_EFFECTSMAXSPEED = "MIX_EFFECTSMAXSPEED";

        public enum MIX_InitFlags
        {
            MIX_INIT_FLAC = 0x00000001,
            MIX_INIT_MOD = 0x00000002,
            MIX_INIT_MODPLUG = 0x00000004,
            MIX_INIT_MP3 = 0x00000008,
            MIX_INIT_OGG = 0x00000010,
            MIX_INIT_FLUIDSYNTH = 0x00000020
        }
        public enum Mix_Fading
        {
            MIX_NO_FADING,
            MIX_FADING_OUT,
            MIX_FADING_IN
        }
        
        public enum Mix_MusicType
        {
            MUS_NONE,
            MUS_CMD,
            MUS_WAV,
            MUS_MOD,
            MUS_MID,
            MUS_OGG,
            MUS_MP3,
            MUS_MP3_MAD,
            MUS_FLAC,
            MUS_MODPLUG
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Mix_Chunk
        {
            int allocated;
            IntPtr abuf;
            UInt32 alen;
            byte volume;       /* Per-sample volume, 0-128 */
        }

        private delegate IntPtr MixLinkedVersionT();
        private static readonly MixLinkedVersionT SSMixLinkedVersionT = __LoadFunction<MixLinkedVersionT>("Mix_Linked_Version");
        public static IntPtr Mix_Linked_Version() => SSMixLinkedVersionT();

        private delegate int MixInitIntT(int flags);
        private static readonly MixInitIntT SSMixInitIntT = __LoadFunction<MixInitIntT>("Mix_Init");
        public static int Mix_Init(int flags) => SSMixInitIntT(flags);

        private delegate void MixQuitT();
        private static readonly MixQuitT SSMixQuitT = __LoadFunction<MixQuitT>("Mix_Quit");
        public static void Mix_Quit() => SSMixQuitT();

        private delegate int MixOpenAudioIntUInt16IntIntT(int frequency, UInt16 format, int channels, int chunksize);
        private static readonly MixOpenAudioIntUInt16IntIntT SSMixOpenAudioIntUInt16IntIntT = __LoadFunction<MixOpenAudioIntUInt16IntIntT>("Mix_OpenAudio");
        public static int Mix_OpenAudio(int frequency, UInt16 format, int channels, int chunksize) => SSMixOpenAudioIntUInt16IntIntT(frequency, format, channels, chunksize);

        private delegate int MixAllocateChannelsIntT(int numChans);
        private static readonly MixAllocateChannelsIntT SSMixAllocateChannelsIntT = __LoadFunction<MixAllocateChannelsIntT>("Mix_AllocateChannels");
        public static int Mix_AllocateChannels(int numChans) => SSMixAllocateChannelsIntT(numChans);

        private delegate int MixQuerySpecIntUshortIntT(ref int frequency, ref ushort format, ref int channels);
        private static readonly MixQuerySpecIntUshortIntT SSMixQuerySpecIntUshortIntT = __LoadFunction<MixQuerySpecIntUshortIntT>("Mix_QuerySpec");
        public static int Mix_QuerySpec(ref int frequency, ref ushort format, ref int channels) => SSMixQuerySpecIntUshortIntT(ref frequency, ref format, ref channels);

        private delegate IntPtr MixLoadWavRwSdlRWopsIntT(IntPtr src, int freeSrc);
        private static readonly MixLoadWavRwSdlRWopsIntT SSMixLoadWavRwSdlRWopsIntT = __LoadFunction<MixLoadWavRwSdlRWopsIntT>("Mix_LoadWAV_RW");
        public static IntPtr Mix_LoadWAV_RW(IntPtr src, int freeSrc) => SSMixLoadWavRwSdlRWopsIntT(src, freeSrc);

        private delegate IntPtr MixLoadMusIntPtrT(IntPtr file);
        private static readonly MixLoadMusIntPtrT SSMixLoadMusIntPtrT = __LoadFunction<MixLoadMusIntPtrT>("Mix_LoadMUS");
        public static IntPtr Mix_LoadMUS(IntPtr file) => SSMixLoadMusIntPtrT(file);

        private delegate IntPtr MixLoadMusRwSdlRWopsIntT(IntPtr src, int freeSrc);
        private static readonly MixLoadMusRwSdlRWopsIntT SSMixLoadMusRwSdlRWopsIntT = __LoadFunction<MixLoadMusRwSdlRWopsIntT>("Mix_LoadMUS_RW");
        public static IntPtr Mix_LoadMUS_RW(IntPtr src, int freeSrc) => SSMixLoadMusRwSdlRWopsIntT(src, freeSrc);

        private delegate IntPtr MixLoadMusTypeRwSdlRWopsMixMusicTypeIntT(IntPtr src, Mix_MusicType type, int freeSrc);
        private static readonly MixLoadMusTypeRwSdlRWopsMixMusicTypeIntT SSMixLoadMusTypeRwSdlRWopsMixMusicTypeIntT = __LoadFunction<MixLoadMusTypeRwSdlRWopsMixMusicTypeIntT>("Mix_LoadMUSType_RW");
        public static IntPtr Mix_LoadMUSType_RW(IntPtr src, Mix_MusicType type, int freeSrc) => SSMixLoadMusTypeRwSdlRWopsMixMusicTypeIntT(src, type, freeSrc);

        private delegate IntPtr MixQuickLoadWavByteT(IntPtr mem);
        private static readonly MixQuickLoadWavByteT SSMixQuickLoadWavByteT = __LoadFunction<MixQuickLoadWavByteT>("Mix_QuickLoad_WAV");
        public static IntPtr Mix_QuickLoad_WAV(IntPtr mem) => SSMixQuickLoadWavByteT(mem);

        private delegate IntPtr MixQuickLoadRawByteUInt32T(IntPtr mem, UInt32 len);
        private static readonly MixQuickLoadRawByteUInt32T SSMixQuickLoadRawByteUInt32T = __LoadFunction<MixQuickLoadRawByteUInt32T>("Mix_QuickLoad_RAW");
        public static IntPtr Mix_QuickLoad_RAW(IntPtr mem, UInt32 len) => SSMixQuickLoadRawByteUInt32T(mem, len);

        private delegate void MixFreeChunkMixChunkT(IntPtr chunk);
        private static readonly MixFreeChunkMixChunkT SSMixFreeChunkMixChunkT = __LoadFunction<MixFreeChunkMixChunkT>("Mix_FreeChunk");
        public static void Mix_FreeChunk(IntPtr chunk) => SSMixFreeChunkMixChunkT(chunk);

        private delegate void MixFreeMusicIntPtrT(IntPtr music);
        private static readonly MixFreeMusicIntPtrT SSMixFreeMusicIntPtrT = __LoadFunction<MixFreeMusicIntPtrT>("Mix_FreeMusic");
        public static void Mix_FreeMusic(IntPtr music) => SSMixFreeMusicIntPtrT(music);

        private delegate int MixGetNumChunkDecodersT();
        private static readonly MixGetNumChunkDecodersT SSMixGetNumChunkDecodersT = __LoadFunction<MixGetNumChunkDecodersT>("Mix_GetNumChunkDecoders");
        public static int Mix_GetNumChunkDecoders() => SSMixGetNumChunkDecodersT();

        private delegate IntPtr MixGetChunkDecoderIntT(int index);
        private static readonly MixGetChunkDecoderIntT SSMixGetChunkDecoderIntT = __LoadFunction<MixGetChunkDecoderIntT>("Mix_GetChunkDecoder");
        public static IntPtr Mix_GetChunkDecoder(int index) => SSMixGetChunkDecoderIntT(index);

        private delegate int MixGetNumMusicDecodersT();
        private static readonly MixGetNumMusicDecodersT SSMixGetNumMusicDecodersT = __LoadFunction<MixGetNumMusicDecodersT>("Mix_GetNumMusicDecoders");
        public static int Mix_GetNumMusicDecoders() => SSMixGetNumMusicDecodersT();

        private delegate IntPtr MixGetMusicDecoderIntT(int index);
        private static readonly MixGetMusicDecoderIntT SSMixGetMusicDecoderIntT = __LoadFunction<MixGetMusicDecoderIntT>("Mix_GetMusicDecoder");
        public static IntPtr Mix_GetMusicDecoder(int index) => SSMixGetMusicDecoderIntT(index);

        private delegate Mix_MusicType MixGetMusicTypeIntPtrT(IntPtr music);
        private static readonly MixGetMusicTypeIntPtrT SSMixGetMusicTypeIntPtrT = __LoadFunction<MixGetMusicTypeIntPtrT>("Mix_GetMusicType");
        public static Mix_MusicType Mix_GetMusicType(IntPtr music) => SSMixGetMusicTypeIntPtrT(music);

        private delegate IntPtr MixGetMusicHookDataT();
        private static readonly MixGetMusicHookDataT SSMixGetMusicHookDataT = __LoadFunction<MixGetMusicHookDataT>("Mix_GetMusicHookData");
        public static IntPtr Mix_GetMusicHookData() => SSMixGetMusicHookDataT();

        private delegate int MixRegisterEffectIntMixEffectFuncTMixEffectDoneTIntPtrT(int chan, Mix_EffectFunc_t f, Mix_EffectDone_t d, IntPtr arg);
        private static readonly MixRegisterEffectIntMixEffectFuncTMixEffectDoneTIntPtrT SSMixRegisterEffectIntMixEffectFuncTMixEffectDoneTIntPtrT = __LoadFunction<MixRegisterEffectIntMixEffectFuncTMixEffectDoneTIntPtrT>("Mix_RegisterEffect");
        public static int Mix_RegisterEffect(int chan, Mix_EffectFunc_t f, Mix_EffectDone_t d, IntPtr arg) => SSMixRegisterEffectIntMixEffectFuncTMixEffectDoneTIntPtrT(chan, f, d, arg);

        private delegate int MixUnregisterEffectIntMixEffectFuncTt(int channel, Mix_EffectFunc_t f);
        private static readonly MixUnregisterEffectIntMixEffectFuncTt SSMixUnregisterEffectIntMixEffectFuncTt = __LoadFunction<MixUnregisterEffectIntMixEffectFuncTt>("Mix_UnregisterEffect");
        public static int Mix_UnregisterEffect(int channel, Mix_EffectFunc_t f) => SSMixUnregisterEffectIntMixEffectFuncTt(channel, f);

        private delegate int MixUnregisterAllEffectsIntT(int channel);
        private static readonly MixUnregisterAllEffectsIntT SSMixUnregisterAllEffectsIntT = __LoadFunction<MixUnregisterAllEffectsIntT>("Mix_UnregisterAllEffects");
        public static int Mix_UnregisterAllEffects(int channel) => SSMixUnregisterAllEffectsIntT(channel);

        private delegate int MixSetPanningIntByteByteT(int channel, byte left, byte right);
        private static readonly MixSetPanningIntByteByteT SSMixSetPanningIntByteByteT = __LoadFunction<MixSetPanningIntByteByteT>("Mix_SetPanning");
        public static int Mix_SetPanning(int channel, byte left, byte right) => SSMixSetPanningIntByteByteT(channel, left, right);

        private delegate int MixSetPositionIntInt16ByteT(int channel, Int16 angle, byte distance);
        private static readonly MixSetPositionIntInt16ByteT SSMixSetPositionIntInt16ByteT = __LoadFunction<MixSetPositionIntInt16ByteT>("Mix_SetPosition");
        public static int Mix_SetPosition(int channel, Int16 angle, byte distance) => SSMixSetPositionIntInt16ByteT(channel, angle, distance);

        private delegate int MixSetDistanceIntByteT(int channel, byte distance);
        private static readonly MixSetDistanceIntByteT SSMixSetDistanceIntByteT = __LoadFunction<MixSetDistanceIntByteT>("Mix_SetDistance");
        public static int Mix_SetDistance(int channel, byte distance) => SSMixSetDistanceIntByteT(channel, distance);

        private delegate int MixSetReverseStereoIntIntT(int channel, int flip);
        private static readonly MixSetReverseStereoIntIntT SSMixSetReverseStereoIntIntT = __LoadFunction<MixSetReverseStereoIntIntT>("Mix_SetReverseStereo");
        public static int Mix_SetReverseStereo(int channel, int flip) => SSMixSetReverseStereoIntIntT(channel, flip);

        private delegate int MixReserveChannelsIntT(int num);
        private static readonly MixReserveChannelsIntT SSMixReserveChannelsIntT = __LoadFunction<MixReserveChannelsIntT>("Mix_ReserveChannels");
        public static int Mix_ReserveChannels(int num) => SSMixReserveChannelsIntT(num);

        private delegate int MixGroupChannelIntIntT(int which, int tag);
        private static readonly MixGroupChannelIntIntT SSMixGroupChannelIntIntT = __LoadFunction<MixGroupChannelIntIntT>("Mix_GroupChannel");
        public static int Mix_GroupChannel(int which, int tag) => SSMixGroupChannelIntIntT(which, tag);

        private delegate int MixGroupChannelsIntIntIntT(int from, int to, int tag);
        private static readonly MixGroupChannelsIntIntIntT SSMixGroupChannelsIntIntIntT = __LoadFunction<MixGroupChannelsIntIntIntT>("Mix_GroupChannels");
        public static int Mix_GroupChannels(int from, int to, int tag) => SSMixGroupChannelsIntIntIntT(from, to, tag);

        private delegate int MixGroupAvailableIntT(int tag);
        private static readonly MixGroupAvailableIntT SSMixGroupAvailableIntT = __LoadFunction<MixGroupAvailableIntT>("Mix_GroupAvailable");
        public static int Mix_GroupAvailable(int tag) => SSMixGroupAvailableIntT(tag);

        private delegate int MixGroupCountIntT(int tag);
        private static readonly MixGroupCountIntT SSMixGroupCountIntT = __LoadFunction<MixGroupCountIntT>("Mix_GroupCount");
        public static int Mix_GroupCount(int tag) => SSMixGroupCountIntT(tag);

        private delegate int MixGroupOldestIntT(int tag);
        private static readonly MixGroupOldestIntT SSMixGroupOldestIntT = __LoadFunction<MixGroupOldestIntT>("Mix_GroupOldest");
        public static int Mix_GroupOldest(int tag) => SSMixGroupOldestIntT(tag);

        private delegate int MixGroupNewerIntT(int tag);
        private static readonly MixGroupNewerIntT SSMixGroupNewerIntT = __LoadFunction<MixGroupNewerIntT>("Mix_GroupNewer");
        public static int Mix_GroupNewer(int tag) => SSMixGroupNewerIntT(tag);

        private delegate int MixPlayChannelTimedIntMixChunkIntIntT(int channel, IntPtr chunk, int loops, int ticks);
        private static readonly MixPlayChannelTimedIntMixChunkIntIntT SSMixPlayChannelTimedIntMixChunkIntIntT = __LoadFunction<MixPlayChannelTimedIntMixChunkIntIntT>("Mix_PlayChannelTimed");
        public static int Mix_PlayChannelTimed(int channel, IntPtr chunk, int loops, int ticks) => SSMixPlayChannelTimedIntMixChunkIntIntT(channel, chunk, loops, ticks);

        private delegate int MixPlayMusicIntPtrIntT(IntPtr music, int loops);
        private static readonly MixPlayMusicIntPtrIntT SSMixPlayMusicIntPtrIntT = __LoadFunction<MixPlayMusicIntPtrIntT>("Mix_PlayMusic");
        public static int Mix_PlayMusic(IntPtr music, int loops) => SSMixPlayMusicIntPtrIntT(music, loops);

        private delegate int MixFadeInMusicIntPtrIntIntT(IntPtr music, int loops, int ms);
        private static readonly MixFadeInMusicIntPtrIntIntT SSMixFadeInMusicIntPtrIntIntT = __LoadFunction<MixFadeInMusicIntPtrIntIntT>("Mix_FadeInMusic");
        public static int Mix_FadeInMusic(IntPtr music, int loops, int ms) => SSMixFadeInMusicIntPtrIntIntT(music, loops, ms);

        private delegate int MixFadeInMusicPosIntPtrIntIntDoubleT(IntPtr music, int loops, int ms, double position);
        private static readonly MixFadeInMusicPosIntPtrIntIntDoubleT SSMixFadeInMusicPosIntPtrIntIntDoubleT = __LoadFunction<MixFadeInMusicPosIntPtrIntIntDoubleT>("Mix_FadeInMusicPos");
        public static int Mix_FadeInMusicPos(IntPtr music, int loops, int ms, double position) => SSMixFadeInMusicPosIntPtrIntIntDoubleT(music, loops, ms, position);

        private delegate int MixFadeInChannelTimedIntMixChunkIntIntIntT(int channel, IntPtr chunk, int loops, int ms, int ticks);
        private static readonly MixFadeInChannelTimedIntMixChunkIntIntIntT SSMixFadeInChannelTimedIntMixChunkIntIntIntT = __LoadFunction<MixFadeInChannelTimedIntMixChunkIntIntIntT>("Mix_FadeInChannelTimed");
        public static int Mix_FadeInChannelTimed(int channel, IntPtr chunk, int loops, int ms, int ticks) => SSMixFadeInChannelTimedIntMixChunkIntIntIntT(channel, chunk, loops, ms, ticks);

        private delegate int MixVolumeIntIntT(int channel, int volume);
        private static readonly MixVolumeIntIntT SSMixVolumeIntIntT = __LoadFunction<MixVolumeIntIntT>("Mix_Volume");
        public static int Mix_Volume(int channel, int volume) => SSMixVolumeIntIntT(channel, volume);

        private delegate int MixVolumeChunkMixChunkIntT(IntPtr chunk, int volume);
        private static readonly MixVolumeChunkMixChunkIntT SSMixVolumeChunkMixChunkIntT = __LoadFunction<MixVolumeChunkMixChunkIntT>("Mix_VolumeChunk");
        public static int Mix_VolumeChunk(IntPtr chunk, int volume) => SSMixVolumeChunkMixChunkIntT(chunk, volume);

        private delegate int MixVolumeMusicIntT(int volume);
        private static readonly MixVolumeMusicIntT SSMixVolumeMusicIntT = __LoadFunction<MixVolumeMusicIntT>("Mix_VolumeMusic");
        public static int Mix_VolumeMusic(int volume) => SSMixVolumeMusicIntT(volume);

        private delegate int MixHaltChannelIntT(int channel);
        private static readonly MixHaltChannelIntT SSMixHaltChannelIntT = __LoadFunction<MixHaltChannelIntT>("Mix_HaltChannel");
        public static int Mix_HaltChannel(int channel) => SSMixHaltChannelIntT(channel);

        private delegate int MixHaltGroupIntT(int tag);
        private static readonly MixHaltGroupIntT SSMixHaltGroupIntT = __LoadFunction<MixHaltGroupIntT>("Mix_HaltGroup");
        public static int Mix_HaltGroup(int tag) => SSMixHaltGroupIntT(tag);

        private delegate int MixHaltMusicT();
        private static readonly MixHaltMusicT SSMixHaltMusicT = __LoadFunction<MixHaltMusicT>("Mix_HaltMusic");
        public static int Mix_HaltMusic() => SSMixHaltMusicT();

        private delegate int MixExpireChannelIntIntT(int channel, int ticks);
        private static readonly MixExpireChannelIntIntT SSMixExpireChannelIntIntT = __LoadFunction<MixExpireChannelIntIntT>("Mix_ExpireChannel");
        public static int Mix_ExpireChannel(int channel, int ticks) => SSMixExpireChannelIntIntT(channel, ticks);

        private delegate int MixFadeOutChannelIntIntT(int which, int ms);
        private static readonly MixFadeOutChannelIntIntT SSMixFadeOutChannelIntIntT = __LoadFunction<MixFadeOutChannelIntIntT>("Mix_FadeOutChannel");
        public static int Mix_FadeOutChannel(int which, int ms) => SSMixFadeOutChannelIntIntT(which, ms);

        private delegate int MixFadeOutGroupIntIntT(int tag, int ms);
        private static readonly MixFadeOutGroupIntIntT SSMixFadeOutGroupIntIntT = __LoadFunction<MixFadeOutGroupIntIntT>("Mix_FadeOutGroup");
        public static int Mix_FadeOutGroup(int tag, int ms) => SSMixFadeOutGroupIntIntT(tag, ms);

        private delegate int MixFadeOutMusicIntT(int ms);
        private static readonly MixFadeOutMusicIntT SSMixFadeOutMusicIntT = __LoadFunction<MixFadeOutMusicIntT>("Mix_FadeOutMusic");
        public static int Mix_FadeOutMusic(int ms) => SSMixFadeOutMusicIntT(ms);

        private delegate Mix_Fading MixFadingMusicT();
        private static readonly MixFadingMusicT SSMixFadingMusicT = __LoadFunction<MixFadingMusicT>("Mix_FadingMusic");
        public static Mix_Fading Mix_FadingMusic() => SSMixFadingMusicT();

        private delegate Mix_Fading MixFadingChannelIntT(int which);
        private static readonly MixFadingChannelIntT SSMixFadingChannelIntT = __LoadFunction<MixFadingChannelIntT>("Mix_FadingChannel");
        public static Mix_Fading Mix_FadingChannel(int which) => SSMixFadingChannelIntT(which);

        private delegate void MixPauseIntT(int channel);
        private static readonly MixPauseIntT SSMixPauseIntT = __LoadFunction<MixPauseIntT>("Mix_Pause");
        public static void Mix_Pause(int channel) => SSMixPauseIntT(channel);

        private delegate void MixResumeIntT(int channel);
        private static readonly MixResumeIntT SSMixResumeIntT = __LoadFunction<MixResumeIntT>("Mix_Resume");
        public static void Mix_Resume(int channel) => SSMixResumeIntT(channel);

        private delegate int MixPausedIntT(int channel);
        private static readonly MixPausedIntT SSMixPausedIntT = __LoadFunction<MixPausedIntT>("Mix_Paused");
        public static int Mix_Paused(int channel) => SSMixPausedIntT(channel);

        private delegate void MixPauseMusicT();
        private static readonly MixPauseMusicT SSMixPauseMusicT = __LoadFunction<MixPauseMusicT>("Mix_PauseMusic");
        public static void Mix_PauseMusic() => SSMixPauseMusicT();
        
        private delegate void MixResumeMusicT();
        private static readonly MixResumeMusicT SSMixResumeMusicT = __LoadFunction<MixResumeMusicT>("Mix_ResumeMusic");
        public static void Mix_ResumeMusic() => SSMixResumeMusicT();

        private delegate void MixRewindMusicT();
        private static readonly MixRewindMusicT SSMixRewindMusicT = __LoadFunction<MixRewindMusicT>("Mix_RewindMusic");
        public static void Mix_RewindMusic() => SSMixRewindMusicT();

        private delegate int MixPausedMusicT();
        private static readonly MixPausedMusicT SSMixPausedMusicT = __LoadFunction<MixPausedMusicT>("Mix_PausedMusic");
        public static int Mix_PausedMusic() => SSMixPausedMusicT();

        private delegate int MixSetMusicPositionDoubleT(double position);
        private static readonly MixSetMusicPositionDoubleT SSMixSetMusicPositionDoubleT = __LoadFunction<MixSetMusicPositionDoubleT>("Mix_SetMusicPosition");
        public static int Mix_SetMusicPosition(double position) => SSMixSetMusicPositionDoubleT(position);

        private delegate int MixPlayingIntT(int channel);
        private static readonly MixPlayingIntT SSMixPlayingIntT = __LoadFunction<MixPlayingIntT>("Mix_Playing");
        public static int Mix_Playing(int channel) => SSMixPlayingIntT(channel);

        private delegate int MixPlayingMusicT();
        private static readonly MixPlayingMusicT SSMixPlayingMusicT = __LoadFunction<MixPlayingMusicT>("Mix_PlayingMusic");
        public static int Mix_PlayingMusic() => SSMixPlayingMusicT();

        private delegate int MixSetMusicCmdIntPtrT(IntPtr command);
        private static readonly MixSetMusicCmdIntPtrT SSMixSetMusicCmdIntPtrT = __LoadFunction<MixSetMusicCmdIntPtrT>("Mix_SetMusicCMD");
        public static int Mix_SetMusicCMD(IntPtr command) => SSMixSetMusicCmdIntPtrT(command);

        private delegate int MixSetSynchroValueIntT(int value);
        private static readonly MixSetSynchroValueIntT SSMixSetSynchroValueIntT = __LoadFunction<MixSetSynchroValueIntT>("Mix_SetSynchroValue");
        public static int Mix_SetSynchroValue(int value) => SSMixSetSynchroValueIntT(value);

        private delegate int MixGetSynchroValueT();
        private static readonly MixGetSynchroValueT SSMixGetSynchroValueT = __LoadFunction<MixGetSynchroValueT>("Mix_GetSynchroValue");
        public static int Mix_GetSynchroValue() => SSMixGetSynchroValueT();

        private delegate int MixSetSoundFontsIntPtrT(IntPtr paths);
        private static readonly MixSetSoundFontsIntPtrT SSMixSetSoundFontsIntPtrT = __LoadFunction<MixSetSoundFontsIntPtrT>("Mix_SetSoundFonts");
        public static int Mix_SetSoundFonts(IntPtr paths) => SSMixSetSoundFontsIntPtrT(paths);

        private delegate IntPtr MixGetSoundFontsT();
        private static readonly MixGetSoundFontsT SSMixGetSoundFontsT = __LoadFunction<MixGetSoundFontsT>("Mix_GetSoundFonts");
        public static IntPtr Mix_GetSoundFonts() => SSMixGetSoundFontsT();

        private delegate IntPtr MixGetChunkIntT(int channel);
        private static readonly MixGetChunkIntT SSMixGetChunkIntT = __LoadFunction<MixGetChunkIntT>("Mix_GetChunk");
        public static IntPtr Mix_GetChunk(int channel) => SSMixGetChunkIntT(channel);

        private delegate void MixCloseAudioT();
        private static readonly MixCloseAudioT SSMixCloseAudioT = __LoadFunction<MixCloseAudioT>("Mix_CloseAudio");
        public static void Mix_CloseAudio() => SSMixCloseAudioT();
        
        private static T __LoadFunction<T>(string name)
            where T : class
        {
            if (LoaderSdl2.SdlMixer == null) return null;
            try
            {
                return LoaderSdl2.SdlMixer.LoadFunction<T>(name);
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
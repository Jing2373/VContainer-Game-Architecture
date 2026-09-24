using UnityEngine;

namespace Jing.Feature.Audio
{
    public interface IAudioManager
    {
        void CloseAllAudio();
        void PlayMainMusic(string name);
        void PlaySoundByLoop(string name);
        void PlaySoundByOnce(string name);
        void StopLoopSound(string name);
        void UpdateMainMusicVolume(float volume);
        void UpdateSoundVolume(float value);
        void UpdateVoiceVolume(float value);
    }

}
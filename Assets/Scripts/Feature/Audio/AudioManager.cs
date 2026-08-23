using System.Collections;
using UnityEngine;

using Jing.Tools;
using System.Collections.Generic;

namespace Jing.Feature.Audio
{
    //音樂音效管理
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        #region SerializeField
        [SerializeField] private string audioPath;          //音樂音效路徑
        [SerializeField] private AudioSource mainMusic;     //主要播放音樂
        [SerializeField] private GameObject soundUnit;      //要被複製的音效單位
        #endregion

        private AudioClip[] audioClips = new AudioClip[0];  //音樂音效清單
        private Dictionary<string, AudioSource> audios_dict = new Dictionary<string, AudioSource>();
        private float bgm_value = 1f;   //bgm音量參數（0-1,預設1)
        private float sound_value = 1f; //sound effects音量參數（0-1,預設1)
        private float character_voice_value = 1f; //角色人聲 音量參數（0-1,預設1)

        private PoolManager pool;

        #region ::: Unity :::
        /// <summary> 初始化 </summary>
        void Awake()
        {
            //故事相關的音樂音效不會擺在Resources底下
            audioClips = Resources.LoadAll<AudioClip>(audioPath);
        }

        void Start()
        {
            pool = new PoolManager();
            pool.Init(soundUnit);
        }
        #endregion

        #region ::: Public Methods :::

        #region ::: 音樂 :::
        /// <summary> 播放背景樂 </summary>
        /// <param name="name"> 音樂名稱 </param>
        public void PlayMainMusic(string name)
        {
            AudioClip clip = GetAudioClipByName(name);
            PlayMainMusic(clip);
        }

        /// <summary> 更新背景樂音量 </summary>
        public void UpdateMainMusicVolume(float volume)
        {
            bgm_value = volume;
            mainMusic.volume = bgm_value;
        }

        #endregion

        #region ::: 音效 :::



        /// <summary> 播放音效 一次 </summary>
        /// <param name="name"> 音效名稱 </param>
        public void PlaySoundByOnce(string name)
        {
            AudioClip clip = GetAudioClipByName(name);
            AudioSource audio = PlaySound(clip);
            StartCoroutine(WaitToStop(audio));
        }

        /// <summary> 播放 loop 的音效（目前用在super模式） </summary>
        /// <param name="name"> 音效名稱</param>
        public void PlaySoundByLoop(string name)
        {
            AudioClip clip = GetAudioClipByName(name);
            AudioSource audio = PlaySound(clip);
            audio.loop = true;
            audios_dict[name] = audio;
        }

        /// <summary> 停止播放Loop音效 </summary>
        public void StopLoopSound(string name)
        {
            if (audios_dict.TryGetValue(name, out AudioSource audio))
            {
                StopSound(audio);
                audios_dict.Remove(name);
            }
        }

        /// <summary> 更新音效樂音量 </summary>
        public void UpdateSoundVolume(float value)
        {
            sound_value = value;
        }

        #endregion

        #region ::: 人聲？ ::: 
        /// <summary> 更新角色人聲 </summary>
        public void UpdateVoiceVolume(float value)
        {
            character_voice_value = value;
        }

        #endregion

        /// <summary>
        /// 關閉所有聲音，包含音樂
        /// </summary>
        public void CloseAllAudio()
        {
            mainMusic.Stop();

            foreach (var audio in audios_dict.Values)
            {
                if (audio != null)
                {
                    StopSound(audio);
                }
            }
            audios_dict.Clear();
            pool.AllGoToPool();
        }

        #endregion

        #region :::Privare Methods :::

        /// <summary>
        /// 播放音樂（使用clip)
        /// </summary>
        private void PlayMainMusic(AudioClip clip)
        {
            if (clip == null || mainMusic == null) { return; }
            if (mainMusic.isPlaying) { mainMusic.Stop(); }

            mainMusic.clip = clip;
            mainMusic.volume = bgm_value;
            mainMusic.Play();
        }

        /// <summary> 播放音效，然後停止後初始化 </summary>
        private IEnumerator WaitToStop(AudioSource audio)
        {
            yield return new WaitForSeconds(audio.clip.length);
            StopSound(audio);
        }

        /// <summary>  播放音效 </summary>
        private AudioSource PlaySound(AudioClip clip)
        {

            if (clip == null) { return null; }

            AudioSource audio = pool.Get().GetComponent<AudioSource>();
            audio.transform.parent = soundUnit.transform.parent.transform;

            audio.clip = clip;
            audio.volume = sound_value;
            audio.Play();

            return audio;
        }

        /// <summary> 關閉這個AudioSource </summary>
        private void StopSound(AudioSource audio)
        {
            audio.Stop();
            audio.loop = false;
            audio.volume = 1;
            pool.GoToPool(audio.gameObject);
        }

        /// <summary> 取得音樂音效 </summary>
        /// <param name="name"> 音樂音效名稱</param>
        private AudioClip GetAudioClipByName(string name)
        {
            foreach (AudioClip clip in audioClips)
            {
                if (clip.name == name) { return clip; }
            }
            return null;
        }

        //測試音量的地方（我認為每個遊戲都需要，可以註解不用刪除拉
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //PlayMainMusic("BGM_background", 0.5f);            //背景
                //PlaySoundByOnce("BGM_coin", 1);                //金幣掉落
                //PlaySoundByOnce("BGM_frame_pop", 1);           //按鈕
                // PlaySoundByOnce("BGM_slotmachine");         //勝利金幣掉落
                // PlaySoundByOnce("BGM_slotmachine_zoom");    //開頭
            }
        }

        #endregion
    }
}

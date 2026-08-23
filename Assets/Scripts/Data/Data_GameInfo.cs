using UnityEngine;

namespace Jing.Game.Data
{
    /// <summary>
    /// Game settings 
    /// (Only exists in PlayerPrefs; 
    /// just set it again if you switch computers.)
    /// </summary>
    public class Data_GameInfo
    {
        public Vector2Int Resolution { set; get; } = new Vector2Int(1920, 1080);
        public bool FullScreen { set; get; } = false;

        /// <summary>
        /// Background Music Volume
        /// </summary>
        public float BGMVolume
        {
            set => PlayerPrefs.SetFloat("BGMVolume", value);
            get => PlayerPrefs.GetFloat("BGMVolume", 1f);
        }

        /// <summary>
        /// Sound Volume
        /// </summary>
        public float SEVolume
        {
            set => PlayerPrefs.SetFloat("SEVolume", value);
            get => PlayerPrefs.GetFloat("SEVolume", 1f);
        }

        /// <summary>
        /// Character Voice Volume
        /// </summary>
        public float CVVolume
        {
            set => PlayerPrefs.SetFloat("CVVolume", value);
            get => PlayerPrefs.GetFloat("CVVolume", 1f);
        }

        public int Language
        {
            set => PlayerPrefs.SetInt("Language", value);
            get => PlayerPrefs.GetInt("Language", 1);
        }


    }
}
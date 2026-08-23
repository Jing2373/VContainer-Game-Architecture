using UnityEngine;

namespace Jing.Tools
{
    public class GlobalCamsAndCanvas
    {
        public Camera MainCamera;
        public Canvas MainCanvas;
        public Canvas StoryCanvas;
        public Canvas PopWindowCanvas;

        public void SwitchMainCanvas(bool isDisplay)
        {
            Debug.Log("AAA");
            MainCanvas.gameObject.SetActive(isDisplay);
        }
        public void SwitchStoryCanvas(bool isDisplay)
        {
            StoryCanvas.gameObject.SetActive(isDisplay);
        }
    }
}
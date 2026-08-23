
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jing.Feature.UI
{
    public enum UIBindType
    {
        Auto,
        Transform,
        ButtonAdv,
        Button,
        TMP_Text,
        Image,
        RawImage,
        Slider,
        Dropdown
    }

    public class UIBinder : MonoBehaviour
    {
        public string Name;

        public UIBindType BindType = UIBindType.Auto;

        public Component Target;

        private void OnValidate()
        {
            Name = gameObject.name;

            if (BindType == UIBindType.Auto)
            {
                Target = (Component)GetComponent<Button>() ??
                         (Component)GetComponent<TMP_Text>() ??
                         (Component)GetComponent<Image>() ??
                         (Component)GetComponent<RawImage>() ??
                         (Component)GetComponent<Slider>() ??
                         (Component)GetComponent<TMP_Dropdown>() ??
                         transform;
            }
            else
            {
                switch (BindType)
                {
                    case UIBindType.Transform: Target = transform; break;
                    case UIBindType.Button: Target = GetComponent<Button>(); break;
                    case UIBindType.TMP_Text: Target = GetComponent<TMP_Text>(); break;
                    case UIBindType.Image: Target = GetComponent<Image>(); break;
                    case UIBindType.RawImage: Target = GetComponent<RawImage>(); break;
                    case UIBindType.Slider: Target = GetComponent<Slider>(); break;
                    case UIBindType.Dropdown: Target = GetComponent<TMP_Dropdown>(); break;
                }

                if (Target == null)
                {
                    Target = transform;
                }
            }
        }
    }
}
using UnityEngine;

using Jing.Tools.Localization;


namespace Jing.Setting
{
    public class Setting_ItemBase : ScriptableObject, ISetting_Item
    {
        [SerializeField] private int id;
        [SerializeField] private Sprite icon;


        public virtual string PreviewName => LocalizationManager.GetLocalization("Data", $"Name_{id}");
        public virtual string PreviewIntroduce => LocalizationManager.GetLocalization("Data", $"Introduce_{id}");

        public int Id => id;

        public Sprite Icon => icon;


    }
}

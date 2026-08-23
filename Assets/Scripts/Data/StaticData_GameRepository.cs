using System.Collections.Generic;
using UnityEngine;

using Jing.Setting;


namespace Jing.Game.Data
{
    /// <summary>
    /// Datas From ScriptableObject
    /// </summary>
    public class StaticData_GameRepository
    {
        #region ::: Items :::
        public Dictionary<(System.Type, int), Setting_ItemBase> Dict_Items { get; } = new();

        #endregion

    }
}

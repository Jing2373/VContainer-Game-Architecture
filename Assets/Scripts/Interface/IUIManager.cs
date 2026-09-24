using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Jing.Feature.UI
{
    public interface IUIManager
    {
        UniTask ShowPage(string pageName);
        UniTask ShowPageAndHideCurrent(string pageName);
        UniTask ShowPageAndReleaseCurrent(string pageName);
        void ClosePage();

        void OpenPopup(string popupName);
        void ClosePopup();

        void CloseAll();
    }
}
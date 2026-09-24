using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Jing.Tools;
using UnityEditor;
using UnityEngine;
using VContainer;

namespace Jing.Feature.UI
{
    [RequireComponent(typeof(UICollector))]
    public abstract class BaseView : MonoBehaviour, IBaseView
    {
        protected UICollector collector;

        #region ::: Public Methods :::
        /// <summary>
        /// Shows the UI. Called by UIManager.
        /// </summary>
        public virtual void Show()
        {
            AddAndGetComponent();
            AddListener();
        }
        /// <summary>
        /// Closes the UI. Called by UIManager.
        /// </summary>
        public virtual void Close()
        {
            RemoveListener();
        }
        #endregion

        #region ::: Protected Methods :::
        /// <summary>
        /// All object scripts are attached here.
        /// </summary>
        protected virtual void AddAndGetComponent() { }

        #region ::: Listener :::
        protected virtual void AddListener() { }
        protected virtual void RemoveListener() { }
        #endregion
        #endregion

    }
}
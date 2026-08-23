using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Jing.Tools;
using UnityEditor;
using UnityEngine;
using VContainer;

namespace Jing.Feature.UI
{
    public abstract class BaseView : MonoBehaviour, IBaseView
    {


        #region ::: Bind :::

        [SerializeField] protected Dictionary<string, Component> _uiMap = new Dictionary<string, Component>();

        public void AutoBind()
        {
            _uiMap.Clear();

            foreach (Transform child in transform)
            {
                FindBindersRecursively(child);
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        private void FindBindersRecursively(Transform current)
        {

            if (current.GetComponent<BaseView>() != null)
            {
                return;
            }

            var binder = current.GetComponent<UIBinder>();
            if (binder != null && !string.IsNullOrEmpty(binder.Name))
            {
                _uiMap[binder.Name] = binder.Target;
            }

            foreach (Transform child in current)
            {
                FindBindersRecursively(child);
            }
        }

        protected T GetUI<T>(string key) where T : Component
        {

            if (_uiMap.TryGetValue(key, out var comp)) return comp as T;
            Debug.Log("Nothing was found., key=" + key);
            return null;
        }

        #endregion

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


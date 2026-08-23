using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Jing.Tools
{
    public class AddressableTools : IAddressableTools
    {
        private Dictionary<string, AsyncOperationHandle> _handles = new Dictionary<string, AsyncOperationHandle>();

        public async UniTask<T> LoadAsset<T>(string key) where T : UnityEngine.Object
        {
            if (_handles.TryGetValue(key, out var existingHandle))
            {
                if (existingHandle.Status == AsyncOperationStatus.Failed)
                {
                    _handles.Remove(key);
                }
                else
                {
                    await existingHandle.ToUniTask();
                    return existingHandle.Result as T;
                }
            }

            var handle = Addressables.LoadAssetAsync<T>(key);
            _handles[key] = handle;

            try
            {
                var result = await handle.ToUniTask();
                return result;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AddressableTools] 載入失敗 Key = {key}。錯誤訊息: {ex.Message}");

                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
                _handles.Remove(key);

                return null;
            }
        }

        public void Release(string key)
        {
            if (_handles.TryGetValue(key, out var handle))
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
                _handles.Remove(key);
            }
        }

        public void ReleaseAll()
        {
            foreach (var handle in _handles.Values)
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }
            _handles.Clear();
        }

    }
}
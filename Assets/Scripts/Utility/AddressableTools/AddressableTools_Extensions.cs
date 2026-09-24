using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Jing.Tools.Extensions
{
    public static class AddressableTools_Extensions
    {
        /// <summary>
        /// 下載圖片
        /// </summary>
        public static async UniTask<Sprite> AddressableGetImage(this IAddressableTools tools, string name)
        {
            //如果日後有新增path，直接這裡改比較快
            string spriteKey = name;
            Sprite sp = await tools.LoadAsset<Sprite>(spriteKey);

            if (sp != null)
            {
                return sp;
            }
            Debug.Log("Sprite下載失敗，名稱＝" + spriteKey);
            return null;
        }

        /// <summary>
        /// 下載GameObject
        /// </summary>
        public static async UniTask<GameObject> AddressableGetGameObject(this IAddressableTools tools, string name)
        {
            //如果日後有新增path，直接這裡改比較快
            string gameObjectKey = name;
            GameObject obj = await tools.LoadAsset<GameObject>(gameObjectKey);

            if (obj != null)
            {
                return obj;
            }
            Debug.Log("GameObject下載失敗，名稱＝" + gameObjectKey);
            return null;
        }
    }
}

using VContainer;
using System.Linq;
using UnityEngine;
using Jing.Setting;
using Jing.Game.Data;
using Jing.Tools;
using Cysharp.Threading.Tasks;

namespace Jing.Game
{
    public class LoadGameRepository : ILoadGameRepository
    {
        #region ::: Inject :::
        private IAddressableTools addressableTools;
        private StaticData_GameRepository gameRepository;

        [Inject]
        public void Construct(StaticData_GameRepository gameRepository, IAddressableTools addressableTools)
        {
            this.gameRepository = gameRepository;
            this.addressableTools = addressableTools;
        }
        #endregion

        #region ::: Public Methods ::: 
        public async UniTask Load()
        {
            var (pills, skills, clothes) = await UniTask.WhenAll(
                addressableTools.LoadAsset<ScriptableObject>("Pills"),
                addressableTools.LoadAsset<ScriptableObject>("Skills"),
                addressableTools.LoadAsset<ScriptableObject>("Clothes")
            );
            PopulateDict(pills);
            PopulateDict(skills);
            PopulateDict(clothes);
        }

        /// <summary>
        /// Get Item By ID
        /// </summary>
        public T GetItem<T>(int id) where T : Setting_ItemBase
        {

            if (gameRepository.Dict_Items.TryGetValue((typeof(T), id), out var item))
            {
                return item as T;
            }

            Debug.LogWarning($"Cannot find item of type {typeof(T).Name}, ID is {id}");
            return null;
        }

        /// <summary>
        /// Get All Item
        /// </summary>
        public T[] GetAllItems<T>() where T : Setting_ItemBase
        {
            return gameRepository.Dict_Items.Values.OfType<T>().ToArray();
        }
        #endregion

        #region ::: Private Methods :::

        private void PopulateDict(ScriptableObject items)
        {
            if (items is ISetting_ItemList list)
            {
                foreach (var item in list.GetItems())
                {
                    if (item is Setting_ItemBase baseItem)
                    {
                        gameRepository.Dict_Items.TryAdd((baseItem.GetType(), baseItem.Id), baseItem);
                    }
                }
            }
        }
        #endregion
    }
}
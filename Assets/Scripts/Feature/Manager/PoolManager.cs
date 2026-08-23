using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Jing.Tools
{
    public class PoolManager
    {
        private List<GameObject> usingList = new List<GameObject>();  //using list
        private ObjectPool<GameObject> poolsList;  //waiting list

        private GameObject unit;

        /// <summary> initial </summary>
        public void Init(GameObject unit)
        {
            this.unit = unit;
            poolsList = new ObjectPool<GameObject>(
                createFunc: () => Object.Instantiate(unit),
                actionOnGet: obj =>
                {
                    obj.SetActive(true);
                    usingList.Add(obj);
                },
                actionOnRelease: obj =>
                {
                    obj.SetActive(false);
                    usingList.Remove(obj);
                },
                actionOnDestroy: obj => Object.Destroy(obj),
                collectionCheck: false,
                defaultCapacity: 5,
                maxSize: 20
            );
        }

        public GameObject Get()
        {
            return poolsList.Get();
        }

        public List<GameObject> GetUsingList()
        {
            return usingList;
        }

        public void GoToPool(GameObject obj)
        {
            poolsList.Release(obj);
        }

        public void AllGoToPool()
        {
            foreach (var obj in new List<GameObject>(usingList))
            {
                poolsList.Release(obj);
            }
            usingList.Clear();
        }
    }

}

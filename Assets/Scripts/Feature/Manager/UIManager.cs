using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Jing.Feature.UI
{
    public class UIManager
    {
        private Canvas main_canvas;
        private Canvas pop_canvas;

        private Dictionary<string, GameObject> mainUI_Dic = new Dictionary<string, GameObject>();
        private Dictionary<string, GameObject> popWindowsUI_Dic = new Dictionary<string, GameObject>();

        private Dictionary<string, GameObject> pageDic = new Dictionary<string, GameObject>();
        private Stack<GameObject> pages = new Stack<GameObject>();
        private GameObject currentPopWindows = null;  //預計只會有一個開始彈跳視窗
        private IObjectResolver resolver;

        #region ::: Public Methods :::
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(Canvas main_canvas, Canvas pop_canvas, Dictionary<string, GameObject> mainUI, Dictionary<string, GameObject> popWindows, IObjectResolver resolver)
        {
            this.main_canvas = main_canvas;
            this.pop_canvas = pop_canvas;
            mainUI_Dic = mainUI;
            popWindowsUI_Dic = popWindows;
            this.resolver = resolver;


        }

        /// <summary>
        /// 顯示頁面
        /// </summary> 
        public void ShowPage(string pageName, bool hideCurrent = true)
        {
            if (hideCurrent && pages.Count > 0)
            {
                GameObject currentUI = pages.Peek();
                ClosePage(currentUI);
            }

            GameObject page = GetOrCreatePage(pageName);
            if (page == null) return;

            OpenPage(page);
            pages.Push(page);
        }

        /// <summary>
        /// 打開彈跳視窗（之前頁面不影藏）
        /// </summary>
        public GameObject OpenPopWindows(string popName)
        {
            GameObject page = GetOrCreatePopWindow(popName);
            if (page == null) return null;
            if (currentPopWindows != null)
            {
                ClosePopWindows();
            }
            OpenPage(page);
            page.transform.SetAsLastSibling();

            currentPopWindows = page;
            return currentPopWindows;
        }

        /// <summary>
        /// 關閉彈跳視窗
        /// </summary>
        public void ClosePopWindows()
        {
            if (currentPopWindows == null) return;

            ClosePage(currentPopWindows);
            currentPopWindows = null;

        }

        /// <summary>
        /// 返回上一頁
        /// </summary>
        public void Back()
        {
            if (pages.Count <= 1)
            {
                return;
            }

            GameObject nowPage = pages.Pop();
            ClosePage(nowPage);

            GameObject previous = pages.Peek();
            OpenPage(previous);
        }

        /// <summary>
        /// 返回到初始介面
        /// </summary>
        public void BackHome()
        {
            while (pages.Count > 1)
            {
                GameObject nowPage = pages.Pop();
                ClosePage(nowPage);
            }
            GameObject previous = pages.Peek();
            OpenPage(previous);
        }

        /// <summary>
        /// 關閉所有介面（跳場景用）
        /// </summary>
        public void Close()
        {

            while (pages.Count > 0)
            {
                GameObject nowPage = pages.Pop();
                ClosePage(nowPage);
            }
            pageDic.Clear();
            currentPopWindows = null;
            mainUI_Dic.Clear();
            popWindowsUI_Dic.Clear();
            main_canvas = null;
            pop_canvas = null;
        }

        public bool ConfirmPageIsDisplay(string page_name)
        {
            GameObject obj = pages.FirstOrDefault(page => page.name == page_name);
            return obj != null;
        }

        #endregion

        #region ::: Private Methods :::
        /// <summary>
        /// 取得或創建
        /// </summary>
        private GameObject GetOrCreatePage(string pageName)
        {
            if (pageDic.TryGetValue(pageName, out var existingPage))
            {
                return existingPage;
            }
            mainUI_Dic.TryGetValue(pageName, out GameObject prefab);

            if (prefab == null)
            {
                Debug.LogWarning($"UIManager: Prefab '{pageName}' not found.");
                return null;
            }
            GameObject instance = GameObject.Instantiate(prefab, main_canvas.transform);
            instance.name = pageName;
            instance.SetActive(false);

            pageDic[pageName] = instance;

            resolver.InjectGameObject(instance);
            return instance;
        }

        private GameObject GetOrCreatePopWindow(string popName)
        {
            if (pageDic.TryGetValue(popName, out var existingPage))
            {
                return existingPage;
            }
            popWindowsUI_Dic.TryGetValue(popName, out GameObject pop);
            if (pop == null)
            {
                Debug.LogWarning($"UIManager: PopWindows '{popName}' not found.");
                return null;
            }
            GameObject instance = GameObject.Instantiate(pop, pop_canvas.transform);
            instance.name = popName;
            instance.SetActive(false);

            pageDic[popName] = instance;
            return instance;

        }

        /// <summary>
        /// 打開介面
        /// </summary>
        private void OpenPage(GameObject page)
        {
            if (!page.activeSelf)
            {
                page.SetActive(true);

                IBaseView[] views = page.GetComponents<IBaseView>();

                foreach (var v in views)
                {
                    v.Show();
                }
            }
        }

        /// <summary>
        /// 關閉介面
        /// </summary>
        private void ClosePage(GameObject page)
        {
            Debug.Log("Close=" + page.name);
            page.SetActive(false);

            IBaseView[] views = page.GetComponents<IBaseView>();
            foreach (var v in views)
            {
                v.Close();
            }
        }
        #endregion


    }
}
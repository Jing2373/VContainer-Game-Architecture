using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class LoadingView : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private GameObject lottie;
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        lottie = gameObject.transform.Find("AnimationIcon").gameObject;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        lottie.SetActive(false);
    }
    public async UniTask ShowLoading()
    {
        lottie.SetActive(true);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        await canvasGroup.DOFade(1f, 0.5f).AsyncWaitForCompletion();
    }

    public async UniTask HideLoading()
    {
        await canvasGroup.DOFade(0f, 0.5f).OnComplete(() =>
        {
            lottie.SetActive(false);
        }).AsyncWaitForCompletion();

    }

}

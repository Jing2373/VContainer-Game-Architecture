using Jing.Tools;
using UnityEngine;
using VContainer;


//主要串聯伺服器的地方（DontDestroyOnLoad）
public class AliveForever : MonoBehaviour
{

    public static AliveForever Instance { get; private set; }
    public bool TestFromJson;

    /// <summary> Unity Methods</summary> 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }
        else { Instance = this; }

        DontDestroyOnLoad(gameObject);

    }

}

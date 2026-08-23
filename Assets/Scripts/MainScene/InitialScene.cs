using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("StartScene");
    }

}

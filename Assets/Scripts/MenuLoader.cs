using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
#if UNITY_IOS
    private string gameId = "4214132";
#elif UNITY_ANDROID
    private string gameId = "4214133";
#endif
    
    public void RestartScene()
    {
        AddSytem.IncreaseAddCounter();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadScene(string sceneName)
    {
        AddSytem.IncreaseAddCounter();
        SceneManager.LoadScene(sceneName);
    }

}

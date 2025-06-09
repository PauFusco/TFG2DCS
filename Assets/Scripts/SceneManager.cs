using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void ChangeScene(int sceneIdx)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIdx);
    }
}

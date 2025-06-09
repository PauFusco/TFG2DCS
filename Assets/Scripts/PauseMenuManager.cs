using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject
        pauseCanvasObject,
        baseMenuObject,
        controlsMenuObject;

    private void Awake()
    {
        pauseCanvasObject.SetActive(false);
        baseMenuObject.SetActive(false);
        controlsMenuObject.SetActive(false);
    }

    #region PauseMenu
    public void EnablePauseMenu()
    {
        pauseCanvasObject.SetActive(true);
        baseMenuObject.SetActive(true);
    }
    public void DisablePauseMenu()
    {
        pauseCanvasObject.SetActive(false);
        baseMenuObject.SetActive(false);
    }
    #endregion
    #region ControlsMenu
    public void EnableControlsUI()
    {
        baseMenuObject.SetActive(false);
        controlsMenuObject.SetActive(true);
    }
    public void DisableControlsUI()
    {
        controlsMenuObject.SetActive(false);
        baseMenuObject.SetActive(true);
    }
    #endregion
}

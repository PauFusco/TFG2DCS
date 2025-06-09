using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject
        pauseCanvasObject,
        baseMenuObject,
        controlsMenuObject,
        informationMenuObject;

    private void Awake()
    {
        pauseCanvasObject.SetActive(false);
        baseMenuObject.SetActive(false);
        controlsMenuObject.SetActive(false);
        informationMenuObject.SetActive(false);
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
    #region InformationMenu
    public void EnableInformationUI()
    {
        baseMenuObject.SetActive(false);
        informationMenuObject.SetActive(true);
    }
    public void DisableInformationUI()
    {
        informationMenuObject.SetActive(false);
        baseMenuObject.SetActive(true);
    }
    #endregion
}

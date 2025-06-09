using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject
        mainmenuCanvasObject,
        baseMenuObject,
        controlsMenuObject;
    
    private void Awake()
    {
        mainmenuCanvasObject.SetActive(true);
        baseMenuObject.SetActive(true);
        controlsMenuObject.SetActive(false);
    }

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

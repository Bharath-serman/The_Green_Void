using UnityEngine;

public class MenuPanelsLogic : MonoBehaviour
{
    #region Inputs
    [Header("Inputs")]
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject ControlsPanel;
    private bool IsActive = false;
    #endregion
    void Start()
    {
        //Set the Panels to Disabled other than MainPanel.
        MainPanel.SetActive(!IsActive);
        OptionsPanel.SetActive(IsActive);
        ControlsPanel.SetActive(IsActive);
    }
    #region PanelLogics
    public void OpenOptionsPanel()
    {
        //Disable the Mainpanel and show OptionsPanel.
        MainPanel.SetActive(IsActive);  //False.
        OptionsPanel.SetActive(!IsActive);  //True.
    }

    public void OpenControlsPanel()
    {
        //Disable the MainPanel and show ControlsPanel.
        MainPanel.SetActive(IsActive);  //False.
        ControlsPanel.SetActive(!IsActive);  //True.
    }

    public void OptionsBackButton()
    {
        OptionsPanel.SetActive(IsActive);  //False.
        MainPanel.SetActive(!IsActive);  //True.
    }

    public void ControlsBackButton()
    {
        ControlsPanel.SetActive(IsActive);  //False.
        MainPanel.SetActive(!IsActive);  //True.
    }

    public  void ExitButton()
    {
        //Quit the Application.
        Application.Quit();
        print(Application.productName + "Closed!");
    }
    #endregion
}

using UnityEngine;

public class ObjectivePanelLogic : MonoBehaviour
{

    //Get the Inputs
    [Header("Inputs")]
    [SerializeField] private GameObject ObjectivePanel;
    private bool IsOpen = false;
    private KeyCode OpenPanelCode = KeyCode.Q;
    void Start()
    {
        //Make sure the panel is disabled.
        ObjectivePanel.SetActive(IsOpen);  //False.
    }

    void Update()
    {
        //Listen for Button Click Event
        if (Input.GetKeyDown(OpenPanelCode))
        {
            //Enable the ObjectivePanel.
            OpenPanel();
        }
    }

    void OpenPanel()
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            //Freeze the time.
            Time.timeScale = 0f;
            ObjectivePanel.SetActive(true);  //True.
        }
        else
        {
            //UnFreeze the time.
            Time.timeScale = 1f;
            ObjectivePanel.SetActive(false);
        }
    }
}

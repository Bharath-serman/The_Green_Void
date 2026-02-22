using UnityEngine;

public class PasswordPanelLogic : MonoBehaviour
{
    //Get the UI Components.
    public GameObject PasswordPanel;
    private bool OpenStatus = false;

    private string TagName = "Player";

    private void Start()
    {
        //Disable the Password Panel at initial.
        PasswordPanel.SetActive(OpenStatus);  //False.
    }

    //Check for the Trigger
    private void OnTriggerEnter(Collider other)
    {
        //Check if the entered one is the player.
        if (other.CompareTag(TagName) && OpenStatus == false)
        {
            PasswordPanel.SetActive(true);
            OpenStatus = true;
        }
            
        //OpenStatus = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            PasswordPanel.SetActive(false);
            OpenStatus = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && OpenStatus == true)
        {
            PasswordPanel.SetActive(false);
            OpenStatus = false;
        }
    }
}

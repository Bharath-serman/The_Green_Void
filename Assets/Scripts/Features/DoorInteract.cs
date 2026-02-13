using UnityEngine;
using System.Collections;
public enum DoorType
{
    FrontDoor,
    InteriorDoor
}
public class DoorInteract : MonoBehaviour
{
    #region Inputs
    //Get the Inputs
    public Animator DoorAnimator;
    private bool IsOpened = false;

    [Header("Audio")]
    [SerializeField] AudioSource FrontDoorSource;
    [SerializeField] AudioSource InteriorDoorSource;

    private string FrontDoorTrigger = "FrontDoor";
    private string InteriorDoorTrigger = "InteriorDoor";
    private string TagName = "Player";
    public DoorType type;

    [Header("TutorialUI")]
    [SerializeField] GameObject TutorialUI;
    private bool UIStatus = true;
    //private float duration = 5f;
    private bool TutorialShown = false;
    #endregion

    private void Start()
    {
        TutorialUI.SetActive(!UIStatus);  //False.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(TagName))
            return;

        if (TutorialShown)
            return;

        TutorialShown = true;
        StartCoroutine(DoorTutorialUI(4f));
    }

    //Check for the trigger
    private void OnTriggerStay(Collider other)
    {
        //Check for conditions
        if (!other.CompareTag(TagName))  //If it is not the player.
            return;
        if (IsOpened)
            return;
        if (Input.GetKeyDown(KeyCode.E)) 
            DoorOpen();
    }

    #region OpenLogic
    private void DoorOpen()
    {
        //Check for DoorType
        switch (type)
        {
            case DoorType.FrontDoor:
                //Play the FrontDoorAnimation
                DoorAnimator.SetTrigger(FrontDoorTrigger);
                //Play the Audio
                FrontDoorSource.Play();
                break;
            case DoorType.InteriorDoor:
                //Play the InteriorDoorAnimation
                DoorAnimator.SetTrigger(InteriorDoorTrigger);
                //Play the Audio
                InteriorDoorSource.Play();
                break;
        }
        IsOpened = true;
    }
    #endregion

    #region DoorControlUI

    IEnumerator DoorTutorialUI(float duration)
    {
        //Show the UI.
        TutorialUI.SetActive(UIStatus);  //True.

        //Wait for X seconds.
        yield return new WaitForSeconds(duration);

        //Disable the UI.
        TutorialUI.SetActive(!UIStatus);  //False.

    }

    #endregion
}

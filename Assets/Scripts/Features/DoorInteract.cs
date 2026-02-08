using UnityEngine;

public enum DoorType
{
    FrontDoor,
    InteriorDoor
}

public class DoorInteract : MonoBehaviour
{
    //Get the Inputs
    public Animator DoorAnimator;
    private bool IsOpened = false;

    private string FrontDoorTrigger = "FrontDoor";
    private string InteriorDoorTrigger = "InteriorDoor";
    private string TagName = "Player";
    public DoorType type;

    //Check for the trigger
    private void OnTriggerStay(Collider other)
    {
        //Check for conditions
        if (!other.CompareTag(TagName))
            return;
        if (IsOpened)
            return;
        if (Input.GetKeyDown(KeyCode.E))
            DoorOpen();
    }

    private void DoorOpen()
    {
        //Check for DoorType
        switch (type)
        {
            case DoorType.FrontDoor:
                //Play the FrontDoorAnimation
                DoorAnimator.SetTrigger(FrontDoorTrigger);
                break;
            case DoorType.InteriorDoor:
                //Play the InteriorDoorAnimation
                DoorAnimator.SetTrigger(InteriorDoorTrigger);
                break;
        }
        IsOpened = true;
    }
}

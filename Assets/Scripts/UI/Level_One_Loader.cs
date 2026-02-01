using UnityEngine;
using System.Collections;

public class Level_One_Loader : MonoBehaviour
{
    //Get the Necessary inputs
    [Header("Inputs")]

    //Animation Clip
    public Animator FadeClip;
    public GameObject BeginButton;

    [SerializeField] public Animator StandUpAnimator;

    [Tooltip("The Canvas which has the FadeAnimator")]
    public GameObject StartCanvas;

    private bool ActiveStatus = false;
    private string TriggerName = "Start";

    #region ButtonEvent

    public void StartClip()
    {
        //Disable/Hide the Button.
        BeginButton.SetActive(ActiveStatus);  //False.
        //Start the coroutine
        StartCoroutine(ClipStopLogic(5f));
    }
    #endregion

    //ClipLogic Coroutine
    #region ClipStopLogic Coroutine
    IEnumerator ClipStopLogic(float WaitTime)
    {
        //Play the Animation
        FadeClip.SetTrigger(TriggerName);
        print("Trigger has been fired!");

        //Wait for some time
        yield return new WaitForSeconds(WaitTime);
        print("Exiting Animation");

        //Stop the Animation
        FadeClip.ResetTrigger(TriggerName);

        //Disable the Canvas
        StartCanvas.SetActive(ActiveStatus);  //False.
        print("Canvas Got ridden!");

        yield return null;
    }
    #endregion
}

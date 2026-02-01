using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

public class Level_One_Loader : MonoBehaviour
{
    //Get the Necessary inputs
    [Header("Inputs")]

    //Animation Clip
    public Animator FadeClip;
    public GameObject BeginButton;

    public static Level_One_Loader Instance;

    [SerializeField] public Animator StandUpAnimator;
    public PlayableDirector LevelOneTimeline;

    [Tooltip("Place all the objects that needs to be enabled and disabled in the timeline signal")]
    [SerializeField] private GameObject[] EnableObjects;
    [SerializeField] private GameObject[] DisableObjects;

    [Tooltip("The Canvas which has the FadeAnimator")]
    public GameObject StartCanvas;

    private bool TrueStats = true;
    private bool ActiveStatus = false;
    private string TriggerName = "Start";
    private string StandTriggerName = "IsStanding";
    #region ButtonEvent

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

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
        //Play the Animation (Canvas Loader Animation)
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

        //Wait for some time.
        yield return new WaitForSeconds(WaitTime);

        //Start Playing the Timeline.
        LevelOneTimeline.Play();
        yield return null;
    }
    #endregion

    #region DisableAndEnable

    public void DisableandEnable()
    {
        //Iterate through all elements in the EnableObjects array.
        foreach (GameObject Eobj in EnableObjects)
        {
            //Enable the Objects.
            Eobj.SetActive(TrueStats);  //True.
        }

        //Iterate through all elements in the DisableObjects array.
        foreach (GameObject Dobj in DisableObjects)
        {
            //Disable the Objects.
            Dobj.SetActive(!TrueStats);  //False.
        }

    }

    #endregion
    public void StartStanding()
    {
        //Fire the StandAnimation Trigger.
        StandUpAnimator.SetTrigger(StandTriggerName);
        
    }
}

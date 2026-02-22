using UnityEngine;
using Cinemachine;

public class SeparateEAndD : MonoBehaviour
{
    public static SeparateEAndD instance;
    //Get the Inputs.
    [Header("E And D")]
    [Tooltip("GameObjects that need to be disabled and enabled")]

    public GameObject[] EnabledObjects;
    public GameObject[] DisabledObjects;

    public GameObject DialogueCanvas;
    private bool CanvasStatus = false;
    [SerializeField] GameObject PlayerFollowCamera;
    [SerializeField] GameObject PlayerHerbert;
    [Header("CutsceneVirtualCamera")]
    [SerializeField] GameObject CutsceneCam;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    #region E And D
    public void EnableObj()
    {
        foreach(var Obj in EnabledObjects)
            Obj.SetActive(true);
        print("Objects Enabled");
    }

    public void DisableObj()
    {
        foreach (var Obj in DisabledObjects)
            Obj.SetActive(false);
        print("Objects Disabled");
    }

    #endregion

    #region DialogueCanvasDisable

    public void DisableCanvas()
    {
        //Disable the Whole Canvas.
        DialogueCanvas.SetActive(CanvasStatus);  //False.
    }

    #endregion

    #region EnablePlayerandCamera
    public void CamPlayerEnable()
    {
        //Enable the Player and PlayerFollowCamera.
        PlayerFollowCamera.SetActive(true);
        PlayerHerbert.SetActive(true);
        CutsceneCam.SetActive(false);
    } 
    #endregion
}

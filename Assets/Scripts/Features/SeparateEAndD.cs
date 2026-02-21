using UnityEngine;

public class SeparateEAndD : MonoBehaviour
{
    //Get the Inputs.
    [Header("E And D")]
    [Tooltip("GameObjects that need to be disabled and enabled")]

    public GameObject[] EnabledObjects;
    public GameObject[] DisabledObjects;

    public GameObject DialogueCanvas;
    private bool CanvasStatus = false;

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
}

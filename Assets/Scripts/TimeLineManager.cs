using UnityEngine;

public class TimeLineManager : MonoBehaviour
{
    //Get the GameObjects that need to be disabled and enabled.
    [SerializeField] private GameObject[] ObjectsToEnable;
    [SerializeField] private GameObject[] ObjectsToDisable;

    private bool IsEnable = true;
    private bool IsDisable = false;

    //Method for enabling and disabling
    #region MainMethod
    public void EnableAndDisable()
    {
        //Iterate through each objects in the array.
        foreach(GameObject enable in ObjectsToEnable)
        {
            if(enable != null)
            {
                enable.SetActive(IsEnable);  //True.
            }
        }

        foreach(GameObject disable in ObjectsToDisable)
        {
            if(disable != null)
            {
                disable.SetActive(IsDisable);  //False.
            }
        }
    }
    #endregion
}

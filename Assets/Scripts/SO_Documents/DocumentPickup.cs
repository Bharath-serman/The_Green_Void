using System.Collections;
using UnityEngine;

public class DocumentPickup : MonoBehaviour
{
    #region Inputs
    //public DocumentData Data;
    private bool PlayerInside;

    private KeyCode DocPickCode = KeyCode.E;
    public GameObject DocumentTutorialUI;

    private bool DocumentUIStatus = true;
    private bool ScareStatus = true;
    #endregion
    private void Update()
    {
        if(PlayerInside && Input.GetKeyDown(DocPickCode))
        {
            //Disable the DocumentTutorialUI.
            DocumentTutorialUI.SetActive(!DocumentUIStatus);  //False.
            //Show the JumpScare from JumpScareManager.
            if (ScareStatus)
            {
                StartCoroutine(SyncLogic());
                ScareStatus = false;
            }
            else
            {
                DocumentManager.Instance.OpenDocument();
            }
            //DocumentManager.Instance.OpenDocument();
        }
    }

    private void Start()
    {
        //Disable the DocumentTutorialUI at Initial.
        DocumentTutorialUI.SetActive(!DocumentUIStatus);  //False.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInside = true;

            //Show the DocumentUI.
            DocumentTutorialUI.SetActive(DocumentUIStatus);  //True.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInside = false;
        }
    }

    #region Document With JumpScareSyncLogic.

    IEnumerator SyncLogic()
    {
        //Start the JumpScare Coroutine.
        yield return StartCoroutine(JumpscareManager.Instance.JumpScare());
        DocumentManager.Instance.OpenDocument();
    }

    #endregion
}

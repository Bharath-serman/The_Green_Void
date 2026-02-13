using UnityEngine;

public class DocumentPickup : MonoBehaviour
{
    //public DocumentData Data;
    private bool PlayerInside;

    private KeyCode DocPickCode = KeyCode.E;

    private void Update()
    {
        if(PlayerInside && Input.GetKeyDown(DocPickCode))
        {
            DocumentManager.Instance.OpenDocument();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInside = false;
        }
    }
}

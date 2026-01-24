using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GhostBlinking : MonoBehaviour
{
    //Get the Inputs
    [Header("Ghost Images")]
    [SerializeField] private Image[] GhostImages;
    //Delay rate
    private float mindelay = 0.03f;
    private float maxdelay = 0.3f;

    private bool Status = false;
    private Coroutine BlinkRoutine;

    //Start the Coroutine.
    public void BeginCoroutine()
    {
        if(BlinkRoutine == null)
        {
            BlinkRoutine = StartCoroutine(GhostBlink());
        }
    }

    //Stop the Coroutine.
    public void EndCoroutine()
    {
        if(BlinkRoutine != null)
        {
            StopCoroutine(BlinkRoutine);
            BlinkRoutine = null;
        }


        //Iterate through the Image array.
        foreach(Image image in GhostImages)
        {
            image.enabled = Status;  //False.
        }
    }

    //Coroutine.
    IEnumerator GhostBlink()
    {
        bool truestatus = true;
        while (truestatus)  //True.
        {
            Image image = GhostImages[Random.Range(0, GhostImages.Length)];

            image.enabled = truestatus;  //True.
            yield return new WaitForSeconds(Random.Range(mindelay, maxdelay));

            image.enabled = false;
            yield return new WaitForSeconds(Random.Range(mindelay, maxdelay));
        }
    }
}

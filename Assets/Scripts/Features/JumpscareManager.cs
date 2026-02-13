using UnityEngine;
using System.Collections;

public class JumpscareManager : MonoBehaviour
{
    public static JumpscareManager Instance;
    //Get the JumpScare Image Panel.
    public GameObject JumpscarePanel;
    private float Duration = 1.01f;
    public AudioSource JumpscareSource;

    private bool JumpScareStatus = true;

    private void Awake()
    {
        Instance = this;
    }

    public void JumpScareBegin()
    {
        //Call the Coroutine.
        StartCoroutine(JumpScare());
    }

    #region JumpScareLogic

    public IEnumerator JumpScare()
    {
        //Show the JumpScare Image and Play the Audio.
        JumpscarePanel.SetActive(JumpScareStatus);  //True.
        JumpscareSource.Play();

        //Wait for X Seconds
        yield return new WaitForSeconds(Duration);

        //Disable the JumpScare Image.
        JumpscarePanel.SetActive(!JumpScareStatus);  //False.
    }

    #endregion
}

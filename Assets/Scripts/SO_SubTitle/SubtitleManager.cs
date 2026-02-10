using UnityEngine;
using TMPro;
using Unity.Collections;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    //Make an instance.
    public static SubtitleManager Instance;

    public TMP_Text SubtitleText;
    public GameObject SubtitlePanel;

    private bool ActiveStatus = false;

    private void Awake()
    {
        Instance = this;

        //Disable the Placeholder Initially.
        SubtitlePanel.SetActive(ActiveStatus);  //False.
    }

    public void StartSubtitle(SubtitleData data)
    {
        //Start the Coroutine
        StartCoroutine(PlaySubtitles(data));
    }

    #region PlaySubtitlesLogic

    IEnumerator PlaySubtitles(SubtitleData data)
    {
        SubtitlePanel.SetActive(true);
        SubtitleText.text = data.SubtitleText;

        if(data.Voice != null)
        {
            AudioSource.PlayClipAtPoint(data.Voice, Camera.main.transform.position);
        }
        yield return new WaitForSeconds(data.duration);

        SubtitleText.text = " ";
        SubtitlePanel?.SetActive(false);
    }

    #endregion

}

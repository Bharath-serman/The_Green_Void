using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DocumentManager : MonoBehaviour
{
    public static DocumentManager Instance;

    public GameObject BackgroundPanel;
    public TMP_Text TitleText;
    public TMP_Text DescriptionText;
    public Image DocumentSprite;

    public DocumentData data;
    public Animator FallDownAnimator;
    [Tooltip("The panel that fades to the next scene")]
    public GameObject BlackoutPanel;

    private bool status = false;
    public AudioSource FallBGM;
    public TMP_Text WarningText;

    [Header("Disable Objects")]
    public GameObject[] DisableObjects;
    public GameObject[] EnableObjects;

    private void Awake()
    {
        Instance = this;
        BackgroundPanel.SetActive(status);  //False.
    }

    private void Update()
    {
        if(BackgroundPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDocument();

            //Starting the Falling Down Coroutine.
            StartCoroutine(FallDown(0.4f, 3.9f, "ChaosLevel"));
        }
    }

    #region DocumentOpenLogic
    public void OpenDocument()
    {
        TitleText.text = data.title;
        DescriptionText.text = data.description;
        DocumentSprite.sprite = data.DocImage;

        BackgroundPanel.SetActive(!status);  //True.
        Time.timeScale = 0f;
    }
    #endregion

    #region DocumentCloseLogic
    public void CloseDocument()
    {
        BackgroundPanel.SetActive(status);  //False.
        Time.timeScale = 1f;
    }
    #endregion

    #region FallDownAnimLogic
    
    IEnumerator FallDown(float delayduration, float PanelDuration, string SceneName)
    {
        //Wait for some seconds.
        yield return new WaitForSeconds(delayduration);

        //Play the BGM.
        FallBGM.Play();

        //Disable the Main Player and His Camera.
        foreach(var obj in DisableObjects)
        {
            obj.SetActive(false);
        }
        foreach (var obj in EnableObjects)
        {
            obj.SetActive(true);
        }

        //Trigger the FallAnimation
        FallDownAnimator.SetTrigger("Falling");

        //Wait for some seconds.
        yield return new WaitForSeconds(PanelDuration);

        //Enable the BlackoutPanel.
        BlackoutPanel.SetActive(true);
        BlackoutPanel.GetComponentInChildren<TMP_Text>().enabled = false;

        //Wait for some seconds.
        yield return new WaitForSeconds(1.1f);

        //Show the Text Element.
        BlackoutPanel.GetComponentInChildren<TMP_Text>().enabled = true;

        //Wait for some seconds.
        yield return new WaitForSeconds(3.2f);

        //Load to next Scene.
        SceneManager.LoadScene(SceneName);
    }

    #endregion
}

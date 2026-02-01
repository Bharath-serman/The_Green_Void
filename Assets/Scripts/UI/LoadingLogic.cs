using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingLogic : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private GameObject LoadingLogo;
    public static LoadingLogic Instance;
    private bool LoadingPanelStatus = false;
    public Slider LoadingSlider;
    private string Scene_Name = "Level_one";
    private bool IsBlinking = false;
    private float BlinkInterval = 0.2f;

    private void Start()
    {
        //Hide the LoadingPanel.
        LoadingPanel.SetActive(LoadingPanelStatus);  //False.

        //Hide the LoadingLogo.
        LoadingLogo.SetActive(LoadingPanelStatus);  //False.
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void StopBlinking()
    {
        StopAllCoroutines();
        IsBlinking = false;  //False.
        if(LoadingPanel != null)
        {
            LoadingPanel.SetActive(IsBlinking);
        }
    }

    public void LoadingPanelLogic()
    {
        StartCoroutine(LoadLogic(Scene_Name));
    }

    #region LoadLogic
    IEnumerator LoadLogic(string SceneName)
    {
        //Get the AsyncOpearation.
        AsyncOperation Operation = SceneManager.LoadSceneAsync(SceneName);

        //Show the LoadingPanel.
        LoadingPanel.SetActive(!LoadingPanelStatus);  //True.

        while (!Operation.isDone)
        {
            //Play the Blink Animation.
            if(LoadingLogo != null && !IsBlinking)  //True.
            {
                StartCoroutine(Blink());
            }

            //Clamp the Progress
            float roundoff = Mathf.Clamp01(Operation.progress / 0.9f);

            //Set the Slider Value.
            LoadingSlider.value = roundoff;
            yield return null; 
        }
        
    }
    #endregion

    IEnumerator Blink()
    {
        //Get the LoadingLogo.
        IsBlinking = true;
        while (true)  //True.
        {
            LoadingLogo.SetActive(!LoadingLogo.activeSelf);
            yield return new WaitForSeconds(BlinkInterval);
        }
    }
}

using UnityEngine;
using System.Collections;

public class StartGameLogic : MonoBehaviour
{
    #region Inputs
    //Get the Start Button Component.
    public GameObject MainMenuPrefab;
    [Header("SerializeField Inputs")]
    private bool IsEnabled = false;
    private int Index = 1;
    public Animator MenuFade;
    private string FadeTrigger = "Fade";
    
    #endregion
    public void StartLogic()
    {
        //Call the Coroutine.
        StopAllCoroutines();

        //Call the SceneNavigation Method.
        //SceneNavigate(Index);
        StartCoroutine(MenuFadeCoroutine());
        
    }

    #region FadeCoroutine
    /*IEnumerator FadeLogic()
    {
        //Get the canvasGroup
        CanvasGroup Group = GetComponent<CanvasGroup>();

        float StartAlpha = 1f;
        float TargetAlpha = 0f;
        float ElapsedTime = 0f;

        if(Group != null)
        {
            Group.alpha = StartAlpha;
        }

        while (ElapsedTime < FadeDelay)
        {
            ElapsedTime += Time.deltaTime;
            float blend = Mathf.Clamp01(ElapsedTime / FadeDelay);
            float currentalpha = Mathf.Lerp(StartAlpha, TargetAlpha, blend);

            if(Group != null)
            {
                Group.alpha = currentalpha;
            }

            yield return null;
        }

        if(Group != null)
        {
            Group.alpha = TargetAlpha;
        }
    }*/
    #endregion


    //MenuFade Animation Coroutine.
    #region FadeCoroutine
    IEnumerator MenuFadeCoroutine()
    {
        int LayerIndex = 0;
        //Play the Animation.
        MenuFade.SetTrigger(FadeTrigger);

        //Get the Animator Status.
        AnimatorStateInfo StateInfo = MenuFade.GetCurrentAnimatorStateInfo(LayerIndex);
        float duration = StateInfo.length;

        //Wait for duration seconds.
        yield return new WaitForSeconds(duration);

        //Disable the MainMenuCanvas Object.
        MainMenuPrefab.SetActive(IsEnabled);

        //Call the LoadLogic Coroutine from LoadingLogic.cs
        LoadingLogic.Instance.LoadingPanelLogic();
    }
    #endregion
}

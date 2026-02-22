using UnityEngine;
using System.Collections;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Signals : MonoBehaviour
{
    //Input Section
    [Header("FOV")]
    [Tooltip("Inputs required for Manipulating the FOV Angle")]
    [SerializeField]public CinemachineVirtualCamera VirtualCamera;
    private float ZoomInFOV = 1f;
    private float ZoomOutFOV = 93.90182f;
    private float ZoomInTime = 1.5f;
    private float ZoomOutTime = 2.8f;

    [SerializeField] Animator FadeAnimator;
    //private bool CanFade;
    [Header("Skybox and Scene")]
    [Tooltip("Inputs required for Changing the Skybox")]
    //public Skybox GreenSkyBox;
    [SerializeField]public Material GreenMaterial;
    private int ChaosSceneIndex = 2;

    public void CallZoomInCoroutine()
    {
        StartCoroutine(FOV(ZoomInFOV));
    }

    public void ChangeSkybox()
    {
        //Change the Skybox to GreenVoid.
        RenderSettings.skybox = GreenMaterial;
        DynamicGI.UpdateEnvironment();
    }

    public void CallZoomOutCoroutine()
    {
        StartCoroutine(ZOFOV(ZoomOutFOV));
    }

    #region ZoomInCoroutine
    //Coroutine.
    IEnumerator FOV(float target)
    {
        float initial = VirtualCamera.m_Lens.FieldOfView;
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime / ZoomInTime;
            VirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(initial, target, time);
            yield return null;
        }
    }
    #endregion

    #region ZoomOutCoroutine
    IEnumerator ZOFOV(float end)
    {
        float start = VirtualCamera.m_Lens.FieldOfView;
        float duration = 0f;

        while (duration < 1f)
        {
            duration += Time.deltaTime / ZoomOutTime;
            VirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(start, end, duration);
            yield return null;
        }
    }

    #endregion

    //SceneLoad Signal
    public void NextScene()
    {
        //Load next scene.
        SceneManager.LoadScene(ChaosSceneIndex);
    }

    #region Level_One_Loader_

    //Create a Method to call the Instance.
    public void StandUpAnimation()
    {
        //Trigger the stand animation from the Level_One_Loader Script.
        Level_One_Loader.Instance.StartStanding();
        print("Standing Up");
    }

    public void DAndE()
    {
        Level_One_Loader.Instance.DisableandEnable();
        print("GameObjects Manipulated");
    }

    #endregion

    #region ChaosLevel

    public void BeginAnimation()
    {
        //Trigger the boolean value for the animation.
        if(FadeAnimator != null)
        {
            //Trigger the animation.
            FadeAnimator.SetTrigger("CanFade");
        }
        //Change the Boolean value.
        //CanFade = true;
    }

    #endregion

    public void RevivePlayerandCamera()
    {
        //Invoke the Method from SeparateEAndD Script.
        SeparateEAndD.instance.CamPlayerEnable();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ChaosUI : MonoBehaviour
{
    public Slider chaosSlider;
    public Image fillImage;

    void Update()
    {
        if (ChaosManager.Instance == null) return;

        float chaos = ChaosManager.Instance.CurrentChaos;
        float max = ChaosManager.Instance.maxChaos;

        chaosSlider.value = chaos;

        float t = chaos / max;
        fillImage.color = Color.Lerp(Color.green, Color.red, t);
    }
}

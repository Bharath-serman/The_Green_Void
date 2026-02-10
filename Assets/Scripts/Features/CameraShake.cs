using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class CameraShake : MonoBehaviour
{

    public CinemachineImpulseSource impulseSource;

    public void Shake(float intensity)
    {
        impulseSource.GenerateImpulse(intensity);
    }
}

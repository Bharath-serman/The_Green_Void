using UnityEngine;

public class SubtitleCollider : MonoBehaviour
{
    //Get the ScriptableObject code
    public SubtitleData Subtitledata;
    private bool Isplayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (Isplayed) return;
        if (other.CompareTag("Player"))
        {
            SubtitleManager.Instance.StartSubtitle(Subtitledata);
            //Play the Subtitle.
            Isplayed = true;
        }
    }
}

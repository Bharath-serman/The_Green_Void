using UnityEngine;

[CreateAssetMenu(fileName = "New_Subtitle", menuName = "Subtitles/Create_Subtitle")]
public class SubtitleData : ScriptableObject
{
    //Get the Inputs
    [TextArea]
    public string SubtitleText;
    public float duration = 3f;

    public AudioClip Voice;
}

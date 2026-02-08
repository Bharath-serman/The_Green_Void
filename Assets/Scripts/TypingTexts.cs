using UnityEngine;

[CreateAssetMenu(fileName = "TextType", menuName ="TypingText/TypingDatas")]
public class TypingTexts : ScriptableObject
{
    //Datas
    public string[] Texts;
    public float TypingSpeed = 0.05f;
    public float EraseSpeed = 0.05f;
    public float WaitDelay = 1f;
    //public Color[] textColors;
}

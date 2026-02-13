using UnityEngine;

[CreateAssetMenu(fileName ="NewDoc",menuName ="Documents/Document Data")]
public class DocumentData : ScriptableObject
{
    //Datas
    public string title;
    [TextArea(5,10)]
    public string description;
    public Sprite DocImage;
}

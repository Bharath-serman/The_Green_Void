using UnityEngine;
using TMPro;
using System.Collections;

public class TypeWritingEffect : MonoBehaviour
{
    #region Inputs
    [Header("Inputs")]

    //Get the TextObject
    public TMP_Text DialogueTexts;
    [SerializeField] private float TypingSpeed = 0.05f;
    [SerializeField] private string[] Texts;
    [SerializeField] private float EraseSpeed = 0.05f;
    [SerializeField] private float WaitDelay = 1f;
    
    #endregion
    void Start()
    {
        //Get the Text Component.
        if(DialogueTexts == null)
        {
            DialogueTexts = GetComponent<TMP_Text>();
        }
        //Start the coroutine.
        StartCoroutine(TypeWriting());
    }

    #region Effect
    IEnumerator TypeWriting()
    {
        #region String Iteration
        foreach (string s in Texts)
        {
            DialogueTexts.text = "";
            //Iterate the Characters in it.
            foreach(char c in s)
            {
                DialogueTexts.text += c;
                yield return new WaitForSeconds(TypingSpeed);
            }

            yield return new WaitForSeconds(WaitDelay);

            #endregion

            #region String Erase
            for (int i = DialogueTexts.text.Length - 1; i > 0; i--)
            {
                DialogueTexts.text = DialogueTexts.text.Substring(0, i - 1);
                yield return new WaitForSeconds(EraseSpeed);
            }

            yield return new WaitForSeconds(1.5f);
        }
        #endregion
    }
    #endregion
}

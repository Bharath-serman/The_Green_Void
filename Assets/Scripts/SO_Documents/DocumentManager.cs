using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DocumentManager : MonoBehaviour
{
    public static DocumentManager Instance;

    public GameObject BackgroundPanel;
    public TMP_Text TitleText;
    public TMP_Text DescriptionText;
    public Image DocumentSprite;

    public DocumentData data;

    private bool status = false;

    private void Awake()
    {
        Instance = this;
        BackgroundPanel.SetActive(status);  //False.
    }

    private void Update()
    {
        if(BackgroundPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDocument();
        }
    }

    #region DocumentOpenLogic
    public void OpenDocument()
    {
        TitleText.text = data.title;
        DescriptionText.text = data.description;
        DocumentSprite.sprite = data.DocImage;

        BackgroundPanel.SetActive(!status);  //True.
        Time.timeScale = 0f;
    }
    #endregion

    #region DocumentCloseLogic
    public void CloseDocument()
    {
        BackgroundPanel.SetActive(status);  //False.
        Time.timeScale = 1f;
    }
    #endregion
}

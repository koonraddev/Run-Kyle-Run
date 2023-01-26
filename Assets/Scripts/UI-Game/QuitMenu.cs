using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitMenu : MonoBehaviour
{

    public GameObject backButtonObject;
    private Button backButton;

    public GameObject quitTriggerObject;
    private QuitTrigger quitTrigger;

    public GameObject exitPopup;
    void Start()
    {
        quitTrigger = quitTriggerObject.GetComponent<QuitTrigger>();
        SetButtonActive();
    }

    public void ExitGame()
    {
        exitPopup.SetActive(true);
    }

    public void BackToMenu()
    {
        gameObject.SetActive(false);
        quitTrigger.SetButtonActive();
    }

    public void SetButtonActive()
    {
        backButton = backButtonObject.GetComponent<Button>();
        backButton.Select();
    }
}

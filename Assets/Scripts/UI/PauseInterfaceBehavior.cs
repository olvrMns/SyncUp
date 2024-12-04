using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseInterfaceBehavior : MonoBehaviour
{

    private PauseController pauseController;
    private Button resumeButton;

    void Start()
    {
        pauseController = GameObject.FindGameObjectWithTag("PauseController").GetComponent<PauseController>();
        resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        resumeButton.onClick.AddListener(pauseController.TogglePause);
    }

}
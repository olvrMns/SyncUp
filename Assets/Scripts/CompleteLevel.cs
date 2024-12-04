using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{

    private OverlayController overlayController;
    private PlayerController playerController;

    void Start()
    {
        overlayController = GameObject.Find("Overlays").GetComponent<OverlayController>();
        playerController = ComponentUtils.GetPlayerController();
    }

    public void ActivateCompletionOverlay() 
    {
        Time.timeScale = 0f;
        playerController.PlayerCanMove = false;
        playerController.PlayerHasWon = true;
        Cursor.lockState = CursorLockMode.Confined;
        overlayController.ChangeOverlay(OverlayType.COMPLETION);
        //enemyCanDamage == false?
        //PostProcessing
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ActivateCompletionOverlay();
    }

}

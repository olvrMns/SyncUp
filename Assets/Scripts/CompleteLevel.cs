using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{

    private OverlayController overlayController;

    void Start()
    {
        overlayController = GameObject.Find("Overlays").GetComponent<OverlayController>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            overlayController.ChangeOverlay(OverlayType.COMPLETION);
        }
    }
}

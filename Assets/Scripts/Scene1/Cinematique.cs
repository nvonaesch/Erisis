using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraOnTrigger : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera rigCamera;         
    public Camera cinematicCamera;   

    [Header("Cinematic Settings")]
    public float cinematicDuration = 5f; 

    private bool isCinematicPlaying = false;

    private void Start()
    {

        if (cinematicCamera != null)
        {
            cinematicCamera.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCinematicPlaying) 
        {
            StartCoroutine(PlayCinematic());
        }
    }

    private IEnumerator PlayCinematic()
    {
        isCinematicPlaying = true;

        if (rigCamera != null)
        {
            rigCamera.gameObject.SetActive(false);
        }

        if (cinematicCamera != null)
        {
            cinematicCamera.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(cinematicDuration);

        if (rigCamera != null)
        {
            rigCamera.gameObject.SetActive(true);
        }

        if (cinematicCamera != null)
        {
            cinematicCamera.gameObject.SetActive(false);
        }

        isCinematicPlaying = false;
        Destroy(gameObject);
    }
}

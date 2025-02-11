using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCinematic : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera cinematicCamera;
    public Camera rigCamera;

    [Header("Cinematic Settings")]
    public float cinematicDuration = 5f;

    private bool isCinematicPlaying = false;

    public MonoBehaviour ScriptDeplacement;

    // Start is called before the first frame update
    void Start()
    {
        cinematicCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCinematicPlaying == false)
        {
            StartCoroutine(Cinematic());
        }
    }

    private IEnumerator Cinematic()
    {
        isCinematicPlaying = true;
        rigCamera.gameObject.SetActive(false);
        ScriptDeplacement.enabled = false;

        cinematicCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(cinematicDuration);

        rigCamera.gameObject.SetActive(true);
        cinematicCamera.gameObject.SetActive(false);

        isCinematicPlaying = false;
        ScriptDeplacement.enabled = true;
    }
}

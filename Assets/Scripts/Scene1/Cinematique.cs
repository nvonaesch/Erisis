using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraOnTrigger : MonoBehaviour
{
    [Header("Param Echidna")]
    public Animator Echidna;
    private string Echidlancement = "anim";

    public float explosionForce = 10f;
    public Transform playerRig;
    public Transform explosionOrigin;

    [Header("Camera Settings")]
    public Camera rigCamera;         
    public Camera cinematicCamera;   

    [Header("Cinematic Settings")]
    public float cinematicDuration = 5f; 

    private bool isCinematicPlaying = false;

    public MonoBehaviour ScriptDeplacement;

    public GameObject pomme;
    public GameObject triggerPomme;
    public GameObject slime;

    private void Start()
    {
        cinematicCamera.gameObject.SetActive(false);
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
        Echidna.Play(Echidlancement);
        isCinematicPlaying = true;

        rigCamera.gameObject.SetActive(false);
        ScriptDeplacement.enabled = false;

        cinematicCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(cinematicDuration);

        rigCamera.gameObject.SetActive(true);
        cinematicCamera.gameObject.SetActive(false);        

        isCinematicPlaying = false;
        ScriptDeplacement.enabled = true;

        Vector3 direction = (playerRig.position - explosionOrigin.position).normalized;
        Rigidbody rigRigidbody = playerRig.GetComponent<Rigidbody>();
        
        rigRigidbody.AddForce(direction * explosionForce, ForceMode.Impulse);

        pomme.SetActive(true);
        triggerPomme.SetActive(true);
        slime.SetActive(true);
        
        Debug.Log("BOOM");
        Destroy(gameObject);
        Echidna.Play("idle");
    }
}

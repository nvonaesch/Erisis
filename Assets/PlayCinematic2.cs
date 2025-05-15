using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCinematic2 : MonoBehaviour
{
    [Header("Thanatos (avec Animator)")]
    public Animator thanatosAnimator;

    [Header("Nom de l'animation à lancer")]
    public string triggerToSend = "launchThanatos";

    public Animator Titi;
    public Animator Titi1;
    public Animator Titi2;
    public Animator Titi3;
    private string Titilancement = "Walk";

    public GameObject mouvement;

    private bool alreadyTriggered = false;

    [Header("Camera Settings")]
    public Camera cinematicCamera;
    public Camera rigCamera;

    [Header("Cinematic Settings")]
    public float cinematicDuration = 5f;

    private bool isCinematicPlaying = false;

    public MonoBehaviour ScriptDeplacement;
    public Collider triggerCollider;


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
            alreadyTriggered = true;

            if (thanatosAnimator != null)
            {
                thanatosAnimator.SetTrigger(triggerToSend);
                //Titi.Play(Titilancement);
                //Titi1.Play(Titilancement);
                //Titi2.Play(Titilancement);
                //Titi3.Play(Titilancement);
                StartCoroutine(Cinematique_Marche());
            }
            
        }
    }

    private IEnumerator Cinematique_Marche()
    {
        yield return new WaitForSeconds(3f);
        Titi.Play(Titilancement);
        Titi1.Play(Titilancement);
        Titi2.Play(Titilancement);
        Titi3.Play(Titilancement);
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
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false; // Désactive le collider pour empêcher le trigger
        }

    }
}

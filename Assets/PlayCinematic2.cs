using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayCinematic2 : MonoBehaviour
{
    [Header("Thanatos (avec Animator)")]
    public Animator thanatosAnimator;
    

    [Header("Nom de l'animation à lancer")]
    public string triggerToSend = "launchThanatos";
    public GameObject _Titi;
    public GameObject _Titi1;
    public GameObject _Titi2;
    public GameObject _Titi3;
    public GameObject _Titi4;
    public GameObject _Titi5;
    public Animator Titi;
    public Animator Titi1;
    public Animator Titi2;
    public Animator Titi3;
    public Animator Titi4;
    public Animator Titi5;
    private string Titilancement = "Walk";
    private string Than = "Walk 0";
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
    
    [Header("Pierre Settings")]
    public GameObject pierreParent;
    private List<GameObject> pierres = new List<GameObject>();
    private bool pierresLancees = false;
    // Start is called before the first frame update
    void Start()
    {

        cinematicCamera.gameObject.SetActive(false);
        _Titi.SetActive(false);
        _Titi1.SetActive(false);
        _Titi2.SetActive(false);
        _Titi3.SetActive(false);
        _Titi4.SetActive(false);
        _Titi5.SetActive(false);

        pierreParent.SetActive(false);
        foreach (Transform t in pierreParent.transform)
        {
            pierres.Add(t.gameObject);
            Rigidbody rb = t.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // On désactive la physique au début
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = thanatosAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(Than))
        {
            _Titi.SetActive(true);
            _Titi1.SetActive(true);
            _Titi2.SetActive(true);
            _Titi3.SetActive(true);
            _Titi4.SetActive(true);
            _Titi5.SetActive(true);
            Titi.Play(Titilancement);
            Titi1.Play(Titilancement);
            Titi2.Play(Titilancement);
            Titi3.Play(Titilancement);
            Titi4.Play(Titilancement);
            Titi5.Play(Titilancement);
            pierreParent.SetActive(true);
            if (!pierresLancees)
            {
                LancerPierres();
                pierresLancees = true;
            }
        }
    }
    private void LancerPierres()
    {
        foreach (GameObject pierre in pierres)
        {
            Rigidbody rb = pierre.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            StartCoroutine(FadeOut(pierre));
        }
    }
    private IEnumerator FadeOut(GameObject pierre)
    {
        Renderer rend = pierre.GetComponent<Renderer>();
        Material mat = rend.material;

        // Passage au shader transparent
        mat.SetFloat("_Mode", 2);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;

        Color startColor = mat.color;
        float elapsed = 0f;
        float duration = 2f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            mat.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mat.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }

    private IEnumerator buzz()
    {
        yield return new WaitForSeconds(2);
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

                //StartCoroutine(Cinematique_Marche());
                
            }
            
        }
    }

    /*private IEnumerator Cinematique_Marche()
    {
        
        AnimatorStateInfo stateInfo = thanatosAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(Than))
        {
            Titi.Play(Titilancement);
            Titi1.Play(Titilancement);
            Titi2.Play(Titilancement);
            Titi3.Play(Titilancement);
        }
    }*/

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

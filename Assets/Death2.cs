using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cinematicCamera;
    public float cinematicDuration = 5f;
    private bool isCinematicPlaying = false;
    public Scene Scene1;
    public GameObject player;
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
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scene1"));
            SceneManager.UnloadSceneAsync("Scene2");
        }

    }

    private IEnumerator Cinematic()
    {
        isCinematicPlaying = true;

        cinematicCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(cinematicDuration);

        cinematicCamera.gameObject.SetActive(false);

        DontDestroyOnLoad(player.gameObject);
        DontDestroyOnLoad(player.gameObject);

        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
        
        

        isCinematicPlaying = false;
    }

}

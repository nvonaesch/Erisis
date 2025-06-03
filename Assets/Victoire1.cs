using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoire1 : MonoBehaviour
{
    public GameObject player;
    
   
    private void OnTriggerEnter(Collider other)
    {

        SceneManager.UnloadSceneAsync("Scene1");
        //DontDestroyOnLoad(player.gameObject);

        SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
        

    }
}

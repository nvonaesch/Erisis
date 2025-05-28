using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoire : MonoBehaviour
{
   
    public Scene Scene1;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
           
        SceneManager.UnloadSceneAsync("Scene2");
        DontDestroyOnLoad(player.gameObject);

        SceneManager.LoadScene("Scene3", LoadSceneMode.Single);


    }

   

}

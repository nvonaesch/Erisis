using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject contextGame;
    public GameObject optionsGame;
    public GameObject player;
    public bool isPaused;

    public void Start(){
        mainMenu.SetActive(false);
        contextGame.SetActive(false);
        optionsGame.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
   
    public void PauseGame(){
        mainMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused= true;
        //contextGame.SetActive(true);
    }
    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused= false;
    }
    public void NextButton(){
        DontDestroyOnLoad(player.gameObject);
        SceneManager.LoadScene("Scene1");
    } 
    public void OptionsButton(){
        mainMenu.SetActive(false);
        optionsGame.SetActive(true);
    } 
    public void ReturnButton(){
        mainMenu.SetActive(true);
        optionsGame.SetActive(false);
    } 
    public void QuitButton(){
        DontDestroyOnLoad(player.gameObject);
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    } 
}

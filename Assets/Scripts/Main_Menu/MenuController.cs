using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject contextGame;
    public GameObject optionsGame;

    public void Start(){
        mainMenu.SetActive(true);
        contextGame.SetActive(false);
        optionsGame.SetActive(false);
    }

    public void PlayButton(){
        mainMenu.SetActive(false);
        contextGame.SetActive(true);
    }
    public void NextButton(){
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
        Application.Quit();
    } 
}

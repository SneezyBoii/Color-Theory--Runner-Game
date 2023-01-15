using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager MenuManagerInstance;
    public bool GameState;
    public GameObject[] menuElement = new GameObject[2];

    void Start()
    {
        GameState = false;
        MenuManagerInstance = this;
    }

    void Update()
    {
        
    }

    //The game is not active by default
    public void GameStart(){
        GameState = true;
        menuElement[0].SetActive(false);
    }


}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Menu : MonoBehaviour
{
    public static string resultLastGame;
    [HideInInspector]public Text result;

    private void Start()
    {
        if(!String.IsNullOrEmpty(resultLastGame))
            result.text = resultLastGame;
    }
    private void Update()
    {
        if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("Game");
    }
}

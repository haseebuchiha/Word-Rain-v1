using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundOverScript : MonoBehaviour
{
    public Text scoreDisplay;

    private string score;

    // GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        // gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        score = GameController.score.ToString();
        scoreDisplay.text += score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}

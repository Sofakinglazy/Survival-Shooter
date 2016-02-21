using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;

    Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = "Score : " + score;
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The HUD
/// </summary>
public class HUD : MonoBehaviour
{
    Timer scoreTimer;
    // scoring support
    [SerializeField]
    Text scoreText;
    int score = 0;
    bool alive = true;
    const string ScorePrefix = "Score: ";

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        scoreText.text = ScorePrefix + score.ToString();
        scoreTimer = gameObject.AddComponent<Timer>();
        scoreTimer.Duration = 1;
        scoreTimer.Run();
    }

 
    void Update()
    {
        // check for timer just finished
        if (scoreTimer.Finished)
        {
            GameObject ship = GameObject.FindWithTag("Ship");
            if (ship != null)
            {
                score += 1;
                scoreText.text = ScorePrefix + score.ToString();
                scoreTimer.Run();
            }
            
        }


        
        }
    }

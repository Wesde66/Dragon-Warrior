using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcanvasController : MonoBehaviour
{
    [SerializeField] private PlayerControl playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;
    [SerializeField] private Text score;
    private float scoreAmount;
    // Start is called before the first frame update
    void Start()
    {
        totalHealthbar.fillAmount = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
        currentHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }

    public void ScoreUpdate(float _score)
    {
        scoreAmount += _score;
        score.text = "High Score: " + scoreAmount;
    }

    
}

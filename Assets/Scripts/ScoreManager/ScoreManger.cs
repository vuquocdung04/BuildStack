using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManger : LoadAuto
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] public TMP_Text hightScoreText;
    [SerializeField] public int score;
    [SerializeField] public int maxScore;


    private void Start()
    {
        maxScore = PlayerPrefs.GetInt(Const.highScore,0);
        if (maxScore > 0)
            hightScoreText.text = "HightScore: " + (maxScore - 1).ToString();
        hightScoreText.text = "HightScore: " + maxScore.ToString();
    }

    private void Update()
    {
        if (score - 1 < 0) return;
        scoreText.text = (score-1).ToString();
        if(score > maxScore)
        {
            maxScore =score;
            PlayerPrefs.SetInt(Const.highScore, maxScore);
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
        this.LoadTextHighScore();
    }

    protected virtual void LoadText()
    {
        GameObject objScore = GameObject.Find(Const.Obj_ScoreText);
        if (scoreText != null) return;
        scoreText = objScore.GetComponent<TMP_Text>();
    }

    protected virtual void LoadTextHighScore()
    {
        GameObject objHighScore = GameObject.Find(Const.Obj_HighScore);
        if (hightScoreText != null) return;
        hightScoreText = objHighScore.GetComponent<TMP_Text>();
        
    }
}

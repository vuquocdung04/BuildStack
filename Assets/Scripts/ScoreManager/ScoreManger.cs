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
        hightScoreText.text = "HightScore: "  + (maxScore-1) .ToString();
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
    }

    protected virtual void LoadText()
    {
        if (scoreText != null) return;
        scoreText = GetComponentInChildren<TMP_Text>();
    }
}

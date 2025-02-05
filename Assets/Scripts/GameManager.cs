using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Spawner spawner;
    public AudioManager audioManager;
    public ScoreManger scoreManger;
    public GameOver gameOver;
    public ColorManager ranDomColor;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
        this.LoadAudioManager();
        this.LoadScoreManager();
        this.LoadGameOver();
        this.LoadColorManager();
    }

    protected virtual void LoadSpawner()
    {
        if (spawner != null) return;
        spawner = GetComponentInChildren<Spawner>();
    }
    protected virtual void LoadAudioManager()
    {
        if (audioManager != null) return;
        audioManager = GetComponentInChildren<AudioManager>();
    }
    protected virtual void LoadScoreManager()
    {
        if (scoreManger != null) return;
        scoreManger = GetComponentInChildren<ScoreManger>();
    }
    protected virtual void LoadGameOver()
    {
        if (gameOver != null) return;
        gameOver = GetComponentInChildren<GameOver>();
    }
    protected virtual void LoadColorManager()
    {
        if (ranDomColor != null) return;
        ranDomColor = GetComponentInChildren<ColorManager>();
    }

}

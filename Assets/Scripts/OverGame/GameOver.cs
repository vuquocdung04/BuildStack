using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : LoadAuto
{
    public bool hasGameFinished;

    public GameObject startText;
    public GameObject endText;
    public GameObject Panel;

    private void Start()
    {
        endText.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextStart();
        this.LoadTextEnd();
        this.LoadPanel();
    }

    protected virtual void LoadTextStart()
    {
        if (startText != null) return;
        startText = GameObject.Find(Const.Obj_Start);
    }

    protected virtual void LoadTextEnd()
    {
        if(endText != null) return;
        endText = GameObject.Find(Const.Obj_End);
    }

    protected virtual void LoadPanel()
    {
        if (Panel != null) return;
        Panel = GameObject.Find(Const.Obj_Panel);
    }

    public void EndGame()
    {
        hasGameFinished = true;
        Panel.SetActive(true);
        endText.SetActive(true);
        GameManager.Instance.scoreManger.hightScoreText.gameObject.SetActive(false);
        StartCoroutine(GameManager.Instance.spawner.cam.EndCam());
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}

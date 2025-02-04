using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public bool hasGameFinished;
    public TMP_Text start;
    public TMP_Text end;
    public GameObject Panel;
    public void EndGame()
    {
        hasGameFinished = true;
        Panel.SetActive(true);
        end.gameObject.SetActive(true);
        Spawner.Instance.scoreManger.hightScoreText.gameObject.SetActive(false);
        StartCoroutine(Spawner.Instance.cam.EndCam());
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}

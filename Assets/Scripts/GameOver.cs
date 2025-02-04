using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public bool hasGameStarted, hasGameFinished;
    public void EndGame()
    {
        hasGameFinished = true;
        StartCoroutine(Spawner.Instance.cam.EndCam());
    }
}

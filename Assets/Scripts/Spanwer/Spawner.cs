using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] GameObject tile, bottomTile, startButton;

    TMP_Text scoreText;

    [SerializeField] List<GameObject> stack;

    [SerializeField] bool hasGameStarted, hasGameFinished;


    private void Start()
    {
        stack = new List<GameObject>();
        stack.Add(bottomTile);
        CreateTile();
    }

    private void Update()
    {
        if (hasGameFinished || hasGameStarted) return;
        if (Input.GetMouseButtonDown(0))
        {
            if(stack.Count > 1)
            {
                stack[stack.Count - 1].GetComponent<Movement>().ScaleTile();
            }
            if(hasGameFinished) return;
            CreateTile();
        } 
    }
    protected virtual void CreateTile()
    {
        GameObject previousTile = stack[stack.Count - 1];
        GameObject activeTile;

        Movement moveScript;

        activeTile = Instantiate(tile);
        moveScript = activeTile.GetComponent<Movement>();
        stack.Add(activeTile); 


        if(stack.Count > 2)
            activeTile.transform.localScale = previousTile.transform.localScale;

        activeTile.transform.position = new Vector3(previousTile.transform.position.x,
                                        previousTile.transform.position.y + previousTile.transform.localScale.y, 
                                        previousTile.transform.position.z);

        activeTile.GetComponent<Movement>().moveX = stack.Count % 2 == 0;
    }

    public void GameOver()
    {

    }
}

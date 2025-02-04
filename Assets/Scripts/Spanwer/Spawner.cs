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
        if (hasGameFinished || !hasGameStarted) return;
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

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBottomTile();
        this.LoadTile();
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        this.hasGameFinished = false;
        this.hasGameStarted = true;
        
    }
    protected virtual void LoadBottomTile()
    {
        if (bottomTile != null) return;
        bottomTile = GetComponentInChildren<BottomTile>().gameObject;
    }

    protected virtual void LoadTile()
    {
        if (tile != null) return;
        tile = GetComponentInChildren<Movement>().gameObject;
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

    }

    public void GameOver()
    {

    }
}

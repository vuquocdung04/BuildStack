
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [Header("===Preperties===")]
    [SerializeField] protected int level;
    public int Level => level;
    [SerializeField] bool hasGameStarted, hasGameFinished;

    [Space(10)]
    [Header("====LoadComponent====")]

    [SerializeField] GameObject tile, bottomTile;
    [SerializeField] RanDomColor randomColor;

    [SerializeField] List<GameObject> stack;


    private void Start()
    {
        stack = new List<GameObject>();
        stack.Add(bottomTile);
        SpawnTile();
    }

    private void Update()
    {
        if (hasGameFinished || hasGameStarted) return;
        if (Input.GetMouseButtonDown(0))
        {
            if(stack.Count > 1)
            {
                stack[stack.Count - 1].GetComponent<TileCtrl>().ScaleTile();
            }
            if(hasGameFinished) return;
            SpawnTile();
            level++;
        } 
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadColor();
        this.LoadBottomTile();
        this.LoadTile();
    }
    protected virtual void LoadColor()
    {
        if (randomColor != null) return;
        randomColor = GetComponent<RanDomColor>();
    }

    protected virtual void LoadBottomTile()
    {
        if (bottomTile != null) return;
        bottomTile = GetComponentInChildren<BottomTile>().gameObject;
    }

    protected virtual void LoadTile()
    {
        if (tile != null) return;
        tile = Resources.Load<GameObject>("Prefabs/Tile");
    }


    protected virtual void SpawnTile()
    {
        GameObject LastTile = stack[stack.Count - 1];
        GameObject CurrentTile;
        CurrentTile = Instantiate(tile);
        stack.Add(CurrentTile); 


        if(stack.Count > 2)
            CurrentTile.transform.localScale = LastTile.transform.localScale;

        CurrentTile.transform.position = new Vector3(LastTile.transform.position.x,
                                        LastTile.transform.position.y + LastTile.transform.localScale.y, 
                                        LastTile.transform.position.z);

        CurrentTile.GetComponent<TileCtrl>().moveX = stack.Count % 2 == 0;

        randomColor.RandomColor(CurrentTile,level);
    }

    public void GameOver()
    {

    }
}

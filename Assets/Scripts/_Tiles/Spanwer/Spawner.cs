
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [Space(10)]
    [Header("====LoadComponent====")]

    [SerializeField] GameObject tile;
    [SerializeField] GameObject bottomTile;
    [SerializeField] public GameOver over;
    [SerializeField] public RanDomColor randomColor;
    [SerializeField] public Cam cam;
    [SerializeField] public ScoreManger scoreManger;
    [Space(10)]
    [SerializeField] public List<GameObject> stack;


    private void Start()
    {
        stack = new List<GameObject>();
        stack.Add(bottomTile);
    }

    private void Update()
    {
        if (over.hasGameFinished)
        {
            if (Input.GetMouseButtonDown(0)) over.LoadScene();
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            over.Panel.SetActive(false);
            over.start.gameObject.SetActive(false);
            scoreManger.hightScoreText.gameObject.SetActive(false);


            if (stack.Count > 1)
            {
                stack[stack.Count - 1].GetComponent<TileCtrl>().ScaleTile();
            }
            if(over.hasGameFinished) return;
            StartCoroutine(cam.Move());
            SpawnTile();
        }
        if (Input.GetMouseButtonUp(0)) scoreManger.score++;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadColor();
        this.LoadBottomTile();
        this.LoadTile();
        this.LoadCam();
        this.LoadGameOver();
        this.LoadScoreManager();
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

    protected virtual void LoadCam()
    {
        if (cam != null) return;
        cam = GetComponentInChildren<Cam>();
    }

    protected virtual void LoadGameOver()
    {
        if (over != null) return;
        over = GetComponentInChildren<GameOver>();
    }


    protected virtual void LoadScoreManager()
    {
        if (scoreManger != null) return;
        scoreManger = GetComponentInChildren<ScoreManger>();
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

        randomColor.RandomColor(CurrentTile,scoreManger.score);
    }
}

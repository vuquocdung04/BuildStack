
using System.Collections.Generic;
using UnityEngine;

public class Spawner : LoadAuto
{
    [Space(10)]
    [Header("====LoadComponent====")]

    [SerializeField] GameObject tile;
    [SerializeField] GameObject bottomTile;
    [SerializeField] public Cam cam;
    [Space(10)]
    [SerializeField] public List<GameObject> stack;


    private void Start()
    {
        stack = new List<GameObject>();
        stack.Add(bottomTile);
    }

    private void Update()
    {
        if (GameManager.Instance.gameOver.hasGameFinished)
        {
            if (Input.GetMouseButtonDown(0)) GameManager.Instance.gameOver.LoadScene();
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            SetActiveObj();
            if (stack.Count > 1)
                stack[stack.Count - 1].GetComponent<TileCtrl>().ScaleTile();

            if(GameManager.Instance.gameOver.hasGameFinished) return;
            if (GameManager.Instance.scoreManger.score > 0)
                StartCoroutine(cam.Move());
            SpawnTile();
        }
        if (Input.GetMouseButtonUp(0)) GameManager.Instance.scoreManger.score++;
    }
    #region LoadComponent
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBottomTile();
        this.LoadTile();
        this.LoadCam();
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

    #endregion

    #region SpawnTile
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

        GameManager.Instance.ranDomColor.RandomColor(CurrentTile,GameManager.Instance.scoreManger.score);
    }

    #endregion

    protected virtual void SetActiveObj()
    {
        GameManager.Instance.gameOver.Panel.SetActive(false);
        GameManager.Instance.gameOver.startText.SetActive(false);
        if (!GameManager.Instance.gameOver.startText.activeInHierarchy)
            GameManager.Instance.audioManager.audioBG.gameObject.SetActive(true);


        GameManager.Instance.scoreManger.hightScoreText.gameObject.SetActive(false);
    }
}

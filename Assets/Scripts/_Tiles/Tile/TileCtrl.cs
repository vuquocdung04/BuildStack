using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCtrl : LoadAuto
{

    [Header("====Properties====")]
    [SerializeField] float distance;
    [SerializeField] float maxDistance;
    [SerializeField] float stepLength;
    [SerializeField] float minCutThresHold;

    [SerializeField] bool moveForward;
    [SerializeField] public bool moveX;
    
    [Space(10)]
    [Header("====LoadComponent====")]
    [SerializeField] GameObject lostTile;
    [SerializeField] RanDomColor randomColor;
    private void Start()
    {
        if (moveX)
            transform.Translate(-distance, 0, 0);
        else
            transform.Translate(0, 0, distance);
    }

    private void Update()
    {
        stepLength = Time.deltaTime * 6f;
        if (moveX)
        {
            MoveX();
        }
        else MoveZ();
    }
    #region Load_Reset
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLostTile();
        this.LoadColor();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        maxDistance = 5f;
        distance = maxDistance;
        moveForward = false;
        moveX = false;
        minCutThresHold = 0.2f;
    }

    protected virtual void LoadLostTile()
    {
        if (lostTile != null) return;
        lostTile = Resources.Load<GameObject>("Prefabs/Lost");
    }
    protected virtual void LoadColor()
    {
        if (randomColor != null) return;
        randomColor = GetComponent<RanDomColor>();
    }


    #endregion

    #region moveTile
    void MoveX()
    {
        if (moveForward)
        {
            if (distance < maxDistance)
            {
                transform.Translate(-stepLength, 0, 0);
                distance += stepLength;
            }
            else
                moveForward = false;
        }
        else
        {
            if (distance > -maxDistance) // chay tu 5 den -5
            {
                transform.Translate(stepLength, 0, 0); // di chuyen sang phai
                distance -= stepLength;
            }
            else
                moveForward = true;
        }
    }

    void MoveZ()
    {
        if (moveForward)
        {
            if (distance < maxDistance)
            {
                transform.Translate(0, 0, stepLength);
                distance += stepLength;
            }
            else
                moveForward = false;
        }
        else
        {
            if (distance > -maxDistance)
            {
                transform.Translate(0, 0, -stepLength);
                distance -= stepLength;
            }
            else
                moveForward = true;
        }
    }
    #endregion

    #region scaleTile
    public void ScaleTile()
    {
        if (Mathf.Abs(distance) > minCutThresHold)
        {
            float lostLength = Mathf.Abs(distance);

            if (moveX)
            {
                if (transform.localScale.x < lostLength)
                {
                    gameObject.AddComponent<Rigidbody>();
                    Spawner.Instance.over.EndGame();
                    return;
                }

                GameObject _lostTile = Instantiate(lostTile);
                _lostTile.transform.localScale = new Vector3(lostLength, transform.localScale.y, transform.localScale.z);
                _lostTile.transform.position = new Vector3(transform.position.x + (distance > 0 ? -1 : 1) * (transform.localScale.x - lostLength) / 2,
                                                            transform.position.y, transform.position.z);
                randomColor.RandomColor(_lostTile, Spawner.Instance.scoreManger.score - 1);
                transform.localScale -= new Vector3(lostLength, 0, 0);
                transform.Translate((distance > 0 ? 1 : -1) * lostLength / 2, 0, 0);
            }
            else
            {
                if (transform.localScale.z < lostLength)
                {
                    gameObject.AddComponent<Rigidbody>();
                    Spawner.Instance.over.EndGame();
                    return;
                }

                GameObject _lostTile = Instantiate(lostTile);
                _lostTile.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, lostLength);
                _lostTile.transform.position = new Vector3(transform.position.x, transform.position.y,
                                                        transform.position.z + (distance > 0 ? 1 : -1) * (transform.localScale.z - lostLength) / 2);
                randomColor.RandomColor(_lostTile, Spawner.Instance.scoreManger.score - 1);
                transform.localScale -= new Vector3(0, 0, lostLength);
                transform.Translate(0, 0, (distance > 0 ? -1 : 1) * lostLength / 2);
            }
        }
        else
        {
            if (moveX)
                transform.Translate(distance, 0, 0);
            else
                transform.Translate(0, 0, -distance);
        }

        Destroy(this);
    }
    #endregion

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : LoadAuto
{
    [SerializeField] GameObject lostTile;
    [SerializeField] float distance;
    [SerializeField] float maxDistance;
    [SerializeField] float stepLength;
    [SerializeField] bool moveForward;
    [SerializeField] bool moveX;

    private void Start()
    {
        distance = maxDistance;
        if (moveX)
            transform.Translate(distance, 0, 0);
        else
            transform.Translate(0,0,distance);
    }

    private void Update()
    {
        stepLength = Time.deltaTime * 6f;
        if (moveX) MoveX();
        else MoveZ();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        maxDistance = 5f;
        moveForward = false;
        moveX = false;
    }

    void MoveX()
    {
        if (moveForward)
        {
            if(distance < maxDistance)
            {
                transform.Translate(stepLength,0,0);
                distance += stepLength;
            }
            else
                moveForward = false;
        }
        else
        {
            if (distance > -maxDistance)
            {
                transform.Translate(-stepLength,0,0);
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

    public void ScaleTile()
    {
        if(Mathf.Abs(distance) > 0.1f)
        {
            float lostLength = Mathf.Abs(distance);

            if (moveX)
            {
                if(transform.localScale.x < lostLength)
                {
                    gameObject.AddComponent<Rigidbody>();
                    Spawner.Instance.GameOver();
                    return;
                }

                GameObject _lostTile = Instantiate(lostTile);
                _lostTile.transform.localScale = new Vector3(lostLength, transform.localScale.y, transform.localScale.z);
                _lostTile.transform.position = new Vector3(transform.position.x  + (distance > 0 ? 1 : -1) * (transform.localScale.x - lostLength)/2,
                                                            transform.position.y, transform.position.z);
                transform.localScale -= new Vector3(lostLength, 0, 0);
                transform.Translate((distance > 0 ? -1 : 1) * lostLength / 2, 0, 0);
            }
            else
            {
                if (transform.localScale.z < lostLength)
                {
                    gameObject.AddComponent<Rigidbody>();
                    Spawner.Instance.GameOver();
                    return;
                }

                GameObject _lostTile = Instantiate(lostTile);
                _lostTile.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, lostLength);
                _lostTile.transform.position = new Vector3(transform.position.x, transform.position.y, 
                                                        transform.position.z +(distance > 0 ? 1 : -1) * (transform.localScale.x - lostLength) / 2);
                transform.localScale -= new Vector3(0, 0, lostLength);
                transform.Translate(0, 0, (distance > 0 ? -1 : 1) * lostLength / 2);
            }
        }
        else
        {
            if (moveX)
                transform.Translate(-distance, 0, 0);
            else
                transform.Translate(0, 0, -distance);
        }

        Destroy(this);
    }
}

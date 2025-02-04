using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Main : Singleton<Main>
{
    [SerializeField] GameObject CurrentCube;
    [SerializeField] GameObject LastCube;
    [SerializeField] Text text;
    [SerializeField] int level;
    [SerializeField] bool done;
    [SerializeField] float cubeSpeed;
    [SerializeField] float thresHold;

    private void Start()
    {
        NewBlock();
    }
    void Update()
    {
        if (done)
        {
            Cam.Instance.ZoomCam();
            return;
        }
        Move();

        if (Input.GetMouseButtonDown(0)) NewBlock();
    }

    protected virtual void Move()
    {
        var time = Mathf.Abs(Time.realtimeSinceStartup % 2f - 1f);
        var pos1 = LastCube.transform.position + Vector3.up * 20f;
        var pos2 = pos1 + ((level % 2 == 0) ? Vector3.left : -Vector3.forward) * 100;

        if (level % 2 == 0)
            CurrentCube.transform.position = Vector3.Lerp(pos2, pos1 + new Vector3(100f, 0, 0), time);
        else
            CurrentCube.transform.position = Vector3.Lerp(pos1 + new Vector3(0, 0, 100f), pos2, time);
    }

    protected virtual void RamdomColor()
    {
        CurrentCube.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.HSVToRGB((level / 100f) % 1f, 1f, 1f));
    }

    protected virtual void NewBlock()
    {
        if (LastCube != null)
        {
            float offsetX = Mathf.Abs(CurrentCube.transform.position.x - LastCube.transform.position.x);
            float offsetZ = Mathf.Abs(CurrentCube.transform.position.z - LastCube.transform.position.z);

            if (offsetX <= thresHold)
            {
                CurrentCube.transform.position = new Vector3(LastCube.transform.position.x,
                    CurrentCube.transform.position.y,
                    CurrentCube.transform.position.z);
                offsetX = 0;
            }

            if (offsetZ <= thresHold)
            {
                CurrentCube.transform.position = new Vector3(CurrentCube.transform.position.x,
                    CurrentCube.transform.position.y,
                    LastCube.transform.position.z);
                offsetZ = 0;
            }
            CurrentCube.transform.localScale = new Vector3(
                LastCube.transform.localScale.x - offsetX,
                LastCube.transform.localScale.y,
                LastCube.transform.localScale.z - offsetZ);


            CurrentCube.transform.position = Vector3.Lerp(CurrentCube.transform.position, LastCube.transform.position, 0.5f) + Vector3.up * 10f;

            if (CurrentCube.transform.localScale.x <= 0f || CurrentCube.transform.localScale.z <= 0f)
            {
                done = true;
                text.gameObject.SetActive(true);
                return;
            }
        }

        LastCube = CurrentCube;
        CurrentCube = Instantiate(LastCube);
        RamdomColor();
        level++;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.position + new Vector3(0, 20, 0), 1f);
        Camera.main.fieldOfView -= 0.5f;
    }
}

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
            Camera mainCamera = Camera.main;

            float targetFOV = 90f;
            float currentFOV = mainCamera.fieldOfView;
            mainCamera.fieldOfView = Mathf.Lerp(currentFOV, targetFOV, Time.deltaTime * 5f);
            return;
        }

        var time = Mathf.Abs(Time.realtimeSinceStartup % 2f - 1f);

        var pos1 = LastCube.transform.position + Vector3.up * 10f;

        var pos2 = pos1 + ((level % 2 == 0) ? Vector3.left : -Vector3.forward) * 100;

        if (level % 2 == 0)
            CurrentCube.transform.position = Vector3.Lerp(pos2, pos1, time);
        else
            CurrentCube.transform.position = Vector3.Lerp(pos1, pos2, time);

        if (Input.GetMouseButtonDown(0)) NewBlock();
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

            CurrentCube.transform.position = Vector3.Lerp(CurrentCube.transform.position, LastCube.transform.position, 0.5f) + Vector3.up * 5f;

            if (CurrentCube.transform.localScale.x <= 0f || CurrentCube.transform.localScale.z <= 0f)
            {
                done = true;
                text.gameObject.SetActive(true);
                return;
            }
        }

        LastCube = CurrentCube;
        CurrentCube = Instantiate(LastCube);
        CurrentCube.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.HSVToRGB((level / 100f) % 1f, 1f, 1f));
        level++;
        Camera.main.transform.position = CurrentCube.transform.position + new Vector3(150, 150, 150);
        Camera.main.transform.LookAt(CurrentCube.transform.position);
    }


}

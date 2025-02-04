using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : LoadAuto
{
    [SerializeField] public float moveLength;
    [SerializeField] float stepLength;
    [SerializeField] float timeMove;

    protected override void ResetValue()
    {
        base.ResetValue();
        this.moveLength = 1f;
        this.stepLength = 0.1f;
        this.timeMove = 0.05f;
    }
    public IEnumerator Move()
    {

        var waitSecond = new WaitForSeconds(timeMove);
        while (moveLength > 0)
        {
            stepLength = 0.1f;
            moveLength -= stepLength;
            Camera.main.transform.Translate(0, stepLength,0,Space.World);
            yield return waitSecond;
        }
        moveLength = 1f;
        
    }

    public IEnumerator EndCam()
    {
        Vector3 temp = Camera.main.transform.position;
        Vector3 final = new Vector3(temp.x, temp.y - Spawner.Instance.stack.Count * 0.5f, temp.z);
        float cameraSizeFinal = Spawner.Instance.stack.Count * 0.65f;

        var waitSecond = new WaitForSeconds(0.01f);

        while (Camera.main.GetComponent<Camera>().orthographicSize < cameraSizeFinal)
        {
            Camera.main.orthographicSize += 0.2f;
            temp = Camera.main.transform.position;
            temp = Vector3.Lerp(temp, final, 0.2f);
            Camera.main.transform.position = temp;
            yield return waitSecond;
        }
        Camera.main.transform.position = final;
    }
}

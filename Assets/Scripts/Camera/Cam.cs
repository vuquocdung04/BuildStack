using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : Singleton<Cam>
{

    [Header("==== Properties ====")]
    [SerializeField] float targetFOV;

    protected override void ResetValue()
    {
        base.ResetValue();
        this.targetFOV = 90f;
    }

    public void ZoomCam()
    {
        Camera mainCamera = Camera.main;
        float currentFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = Mathf.Lerp(currentFOV, targetFOV, Time.deltaTime * 5f);
    }


}

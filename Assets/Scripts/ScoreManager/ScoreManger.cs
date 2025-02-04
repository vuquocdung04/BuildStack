using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManger : LoadAuto
{
    [SerializeField] TMP_Text score;

    private void Update()
    {
        score.text = Spawner.Instance.Level.ToString(); 
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    protected virtual void LoadText()
    {
        if (score != null) return;
        score = GetComponentInChildren<TMP_Text>();
    }
}

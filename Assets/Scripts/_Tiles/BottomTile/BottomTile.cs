
using UnityEngine;

public class BottomTile : MonoBehaviour
{
    private void Start()
    {
        int ramdomNum = Random.Range(10,100);
        GameManager.Instance.spawner.randomColor.RandomColor(this.gameObject,ramdomNum);
    }
}

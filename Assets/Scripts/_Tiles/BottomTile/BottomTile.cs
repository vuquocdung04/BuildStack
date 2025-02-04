
using UnityEngine;

public class BottomTile : MonoBehaviour
{
    private void Start()
    {
        int ramdomNum = Random.Range(10,100);
        Spawner.Instance.randomColor.RandomColor(this.gameObject,ramdomNum);
    }
}

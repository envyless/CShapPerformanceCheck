using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void FStart(bool e)
    {
        isFind = e;
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemy((e) =>
        {
            isFind = e;
        });
    }

    bool isFind;
    void FindEnemy(System.Action<bool> comp)
    {
        isFind = Random.Range(0, 10) > 3;
        comp?.Invoke(isFind);
    }
}

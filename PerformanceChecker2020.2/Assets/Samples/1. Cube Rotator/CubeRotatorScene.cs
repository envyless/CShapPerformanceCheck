using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotatorScene : MonoBehaviour
{
    public GameObject prefab;
    public int NumOfObject = 10;

    // Start is called before the first frame update
    void Start()
    {
        int x = (int)Mathf.Sqrt(NumOfObject);
        int y = x;

        const float distance = 3;
        for (int i = 0; i < y; ++i)
        {
            for(int k = 0; k < x; ++k)
            {
                var go = GameObject.Instantiate(prefab);
                go.transform.position = transform.position + new Vector3(i * distance, -k * distance, 0);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class RandomRotator : MonoBehaviour
{
    float getRand => UnityEngine.Random.Range(180f, -180f);

    // Start is called before the first frame update
    void Start()
    {
        float x = 0, y = 0, z = 0;
        StreamMaker.Updater(2000f, 1f)
            .Subscribe(time =>
            {
                x = getRand;
                y = getRand;
                z = getRand;
            }).AddTo(this);

        Observable.EveryUpdate()
            .Subscribe(_=> {
                transform.eulerAngles += new Vector3(x, y, z) * Time.deltaTime;
            }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
    }
}


public static class StreamMaker
{
    public static IObservable<int> CountDownObservable(int countTime)
    {
        return Observable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1)) // �ٷν���(0��) ���� 1�� �������� ����
            .Select(x => (int)(countTime - x)) // x�� �����ϰ� ������ �ð�(��)
            .TakeWhile(x => x > 0); // 0�� �ʰ� ���� OnNext 0�� �Ǹ� OnComplete
    }

    public static IObservable<int> Updater(float duration = 0, float period = 0.1f)
    {
        return Observable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1)) // �ٷν���(0��) ���� 1�� �������� ����
            .Select(x => (int)x) // x�� �����ϰ� ������ �ð�(��)
            .TakeWhile(x => x < duration); // 0�� �ʰ� ���� OnNext 0�� �Ǹ� OnComplete
    }
}
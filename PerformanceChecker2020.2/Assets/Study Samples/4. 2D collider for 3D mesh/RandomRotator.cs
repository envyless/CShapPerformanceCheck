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
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1)) // 바로시작(0초) 이후 1초 간격으로 실행
            .Select(x => (int)(countTime - x)) // x는 시작하고 나서의 시간(초)
            .TakeWhile(x => x > 0); // 0초 초과 동안 OnNext 0이 되면 OnComplete
    }

    public static IObservable<int> Updater(float duration = 0, float period = 0.1f)
    {
        return Observable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1)) // 바로시작(0초) 이후 1초 간격으로 실행
            .Select(x => (int)x) // x는 시작하고 나서의 시간(초)
            .TakeWhile(x => x < duration); // 0초 초과 동안 OnNext 0이 되면 OnComplete
    }
}
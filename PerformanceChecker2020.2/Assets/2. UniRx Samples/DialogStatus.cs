using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // 각 다이얼로그가 갖을 수 있는 상태
    public enum State
    {
        None,
        Inited,
        Opend,
        Closed,
    }

    // 반응형으로 만들어진 다이얼로그의 상태값
    public ReactiveProperty<State> RPState = new ReactiveProperty<State>();

    // Object 형 Param과 State를 같이 구독중
    public ReactiveProperty<(State, Object)> RPStateWithParam = new ReactiveProperty<(State, Object)>();
}

public class DialogStatus : Dialog
{
    [SerializeField]
    [GetComponentInChildren(true, "Atk")]
    Text Atk;

    [SerializeField]
    [GetComponentInChildren(true, "Def")]
    Text Def;

    [SerializeField]
    [GetComponentInChildren(true, "Hp")]
    Text Hp;

    [SerializeField]
    [GetComponentInChildren(true, "Name")]
    Text Name;

    public void Init(Actor.RPDataStat statData)
    {        
        // 초기화와 동시에 행위 정의
        statData.Atk.Subscribe(_v => {
            Atk.text = _v.ToString();
        });

        float beforeDef = 0;
        statData.Def.Subscribe(_v =>
        {
            Def.text = _v.ToString();
        });

        statData.Name.Subscribe(_v =>
        {
            Name.text = _v.ToString();
        });

        statData.Hp.Subscribe(_v =>
        {
            Hp.text = _v.ToString();
        });
    }
}

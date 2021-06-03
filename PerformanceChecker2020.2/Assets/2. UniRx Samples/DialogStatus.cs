using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // �� ���̾�αװ� ���� �� �ִ� ����
    public enum State
    {
        None,
        Inited,
        Opend,
        Closed,
    }

    // ���������� ������� ���̾�α��� ���°�
    public ReactiveProperty<State> RPState = new ReactiveProperty<State>();

    // Object �� Param�� State�� ���� ������
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
        // �ʱ�ȭ�� ���ÿ� ���� ����
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

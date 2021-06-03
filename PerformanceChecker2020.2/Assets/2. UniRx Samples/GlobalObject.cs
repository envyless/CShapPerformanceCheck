using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GlobalObject : MonoBehaviour
{
    [SerializeField]
    [GetComponentInChildren]
    Canvas MainCanvas;

    [SerializeField]
    [GetComponentInChildren(true)]
    public DialogStatus DlgStatus;

    public static ReactiveProperty<bool> IsInitedRP = new ReactiveProperty<bool>();

    public static GlobalObject Instance;
    private void Awake()
    {
        Instance = this;
        IsInitedRP.Value = true;
    }


    // Event Managing
    public ReactiveProperty<int> DamageRecived = new ReactiveProperty<int>();
    public ReactiveProperty<bool> Dead = new ReactiveProperty<bool>();

    // Buff
    public enum Buff
    {
        None,
        AtkIncrease,
        DefIncrease,
    }
    public ReactiveProperty<Buff> BuffReciver = new ReactiveProperty<Buff>();
}

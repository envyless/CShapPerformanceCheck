using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Actor : MonoBehaviour
{
    // Start is called before the first frame update
    public class RPDataStat
    {
        public ReactiveProperty<string> Name = new ReactiveProperty<string>();
        public ReactiveProperty<float> Hp = new ReactiveProperty<float>();
        public ReactiveProperty<int> Def = new ReactiveProperty<int>();
        public ReactiveProperty<int> Atk = new ReactiveProperty<int>();

        public Subject<string> EventName = new Subject<string>();
    }

    public RPDataStat Stat = new RPDataStat();

    private void Awake()
    {
        GlobalObject.IsInitedRP.Subscribe(isInit =>
        {
            if(isInit)
            {
                Stat.Name.Value = "LeeSun";
                Stat.Hp.Value = 1000;
                Stat.Def.Value = 23;
                Stat.Atk.Value = 90;
            }
        }).AddTo(this);
    }

    private void Update()
    {
        // Something Event happened
        if(Input.GetKeyDown(KeyCode.A))
        {
            //Atk Increase
            Stat.Atk.Value += 10;
            GlobalObject.Instance.BuffReciver.Value = GlobalObject.Buff.AtkIncrease;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //Atk Increase
            Stat.Atk.Value = 10;
        }

        // Something Event Name Changed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Stat.EventName.OnNext("FRameCnt : " + Time.frameCount);
        }

        // Something Event Name Changed
        if (Input.GetKeyDown(KeyCode.D))
        {
            GlobalObject.Instance.DlgStatus.Init(Stat);
            GlobalObject.Instance.DlgStatus.gameObject.SetActive(true);
        }

        // Something Event Name Changed
        if (Input.GetKeyDown(KeyCode.C))
        {            
            GlobalObject.Instance.DlgStatus.gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static public class ExtensionGameObject
{
    /// <summary>
    /// GameObject에서 호출시에 내부적으로 갖고있는 컴포넌트가 있을 시 반한합니다. 없을 시 새로 추가합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    static public T AddMissingComponent<T>(this GameObject go) where T : Component
    {
        T comp = go.GetComponent<T>();

        if (comp == null)        
            comp = go.AddComponent<T>();
        
        return comp;
    }

    static public T AddMissingComponent<T>(this Component cp) where T : Component
    {
        T comp = cp.GetComponent<T>();

        if (comp == null)
            comp = cp.gameObject.AddComponent<T>();

        return comp;
    }

    static public List<GameObject> FindContainsGameObject(this Transform tr, string containString, out List<GameObject> childs)
    {
        int count = tr.childCount;
        childs = new List<GameObject>();
        for(int i = 0; i < count; ++i)
        {
            GameObject go = tr.GetChild(i).gameObject;

            if (go.name.Contains(containString))
            {
                childs.Add(go);
            }
        }
        return childs;
    }

    static public GameObject FindContain(this Transform tr, string containString)
    {
        if (tr == null)
            return null;

        int count = tr.childCount;
        for (int i = 0; i < count; ++i)
        {
            GameObject go = tr.GetChild(i).gameObject;

            if (go.name.Contains(containString))
            {
                return go;
            }
            else
            {
                var childGo = go.transform.FindContain(containString);
                if (childGo != null)
                    return childGo;
            }
        }
        return null;
    }

    static public T FindContain<T>(this Transform tr, string containString) where T : Component
    {
        int count = tr.childCount;
        for (int i = 0; i < count; ++i)
        {
            GameObject go = tr.GetChild(i).gameObject;
           

            if (go.name.Contains(containString))
            {
                T cp = go.GetComponent<T>();
                if (cp != null)
                    return cp;
            }
        }
        return null;
    }

    static public List<T> FindContains<T>(this Transform tr, string containString) where T : Component
    {
        int count = tr.childCount;
        List<T> childs = new List<T>();
        for (int i = 0; i < count; ++i)
        {
            GameObject go = tr.GetChild(i).gameObject;

            if (go.name.Contains(containString))
            {
                T a = go.GetComponent<T>();
                if (a != null)
                    childs.Add(a);
            }
        }
        return childs;
    }
    
    static public T FindChild<T>(this Transform tr, string name, bool isDeep = false) where T : Component
    {
        var childs = tr.GetComponentsInChildren<T>(true);
        for (int i = 0; i < childs.Length; i++)
        {
            if (childs[i].transform == tr)
                continue;
            if (!isDeep && childs[i].transform.parent != tr)
                continue;

            if (childs[i].gameObject.name.Contains(name))
            {
                return childs[i];
            }
        }
        return null;
    }

    static public T FindChildMatch<T>(this Transform tr, string name, bool is_ul_allow = true) where T : Component
    {
        if (is_ul_allow)
        {
            name = name.ToLower();
        }

        var childs = tr.GetComponentsInChildren<T>(true);
        for (int i = 0; i < childs.Length; i++)
        {
            if (childs[i].transform == tr)
                continue;

            string s_name = childs[i].gameObject.name;
            if (is_ul_allow)
                s_name = s_name.ToLower();

            if (s_name.Equals(name))
            {
                return childs[i];
            }
        }
        return null;
    }

    static public GameObject FindChildGameObject(this Transform tr, string name)
    {
        var childs = tr.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < childs.Length; i++ )
        {
            if (childs[i].gameObject.name.Contains(name))
            {
                return childs[i].gameObject;
            }
            if (childs[i] == tr)
                continue;
            if (childs[i].parent != tr)
                continue;
        }
        return null;
    }
    static public List<GameObject> FindChildGameObjects(this Transform tr, string name)
    {
        List<GameObject> list = new List<GameObject>();
        var childs = tr.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < childs.Length; i++)
        {
            if (childs[i] == tr)
                continue;
            if (childs[i].parent != tr)
                continue;

            if (childs[i].gameObject.name.Contains(name))
            {
                list.Add( childs[i].gameObject );
            }            
        }
        return list;
    }
    static public T GetContainsInChildren<T>(this Transform tr, string name) where T : Component
    {
        var array = tr.GetComponentsInChildren<T>(true);
        foreach (var t in array)
        {
            if (t.transform == tr)
                continue;
            if (t.gameObject.name.Contains(name))
                return t;
        }
        return null;
    }
    static public List<T> GetContainsInChildrens<T>(this Transform tr, string name) where T : Component
    {
        List<T> list = new List<T>();
        var array = tr.GetComponentsInChildren<T>(true);
        foreach (var t in array)
        {
            if (t.transform == tr)
                continue;
            if (t.gameObject.name.Contains(name))
                list.Add(t);
        }
        return list;
    }

    static public T FindParent<T>(this Transform tr) where T : Component
    {
        Transform p = tr.parent;
        while (p != null)
        {
            T t = p.GetComponent<T>();
            if (t != null)
            {
                return t;
            }
            else
            {
                p = p.parent;
            }
        }
        return null;
    }

    static public GameObject FindParent(this Transform tr, string name)
    {
        Transform p = tr.parent;
        while (p != null)
        {
            if (p.gameObject.name.Equals(name))
            {
                return p.gameObject;
            }
            else
            {
                p = p.parent;
            }
        }
        return null;
    }

    static public Vector3 SetLocalScaleXYZ(this Transform tr, float _value)
    {
        tr.localScale = new Vector3(_value, _value, _value);
        return tr.localScale;
    }


    static public Vector3 SetPositionLocalY(this Transform tr, float y)
    {
        Vector3 pos = tr.localPosition;
        pos.y = y;
        tr.localPosition = pos;
        return pos;
    }

    static public Vector3 SetPositionLocalX(this Transform tr, float x)
    {
        Vector3 pos = tr.localPosition;
        pos.x = x;
        tr.localPosition = pos;
        return pos;
    }

    static public Vector3 SetPositionLocalZ(this Transform tr, float z)
    {
        Vector3 pos = tr.localPosition;
        pos.z = z;
        tr.localPosition = pos;
        return pos;
    }

    static public Vector3 SetPositionX(this Transform tr, float x)
    {
        Vector3 pos = tr.position;
        pos.x = x;
        tr.position = pos;
        return pos;
    }

    static public Vector3 SetPositionZ(this Transform tr, float z)
    {
        Vector3 pos = tr.position;
        pos.z = z;
        tr.position = pos;
        return pos;
    }

    static public Vector3 SetPositionY(this Transform tr, float y)
    {
        Vector3 pos = tr.position;
        pos.y = y;
        tr.position = pos;
        return pos;
    }

    static public Vector3 WorldToRectPos(this Vector3 worldPos)
    {
        if (Camera.main == null)
            return Vector3.zero;

        Vector3 pos = Camera.main.WorldToScreenPoint(worldPos);
        return pos;
    }

    public static List<GameObject> turnOffObjects = new List<GameObject>();
    static public void ReserveTurnOff(this GameObject turnOff)
    {
        turnOffObjects.Add(turnOff);
    }
    static public void ExecuteTurnOffObjects()
    {
        foreach (var obj in turnOffObjects)
        {
            obj.gameObject.SetActive(false);
        }
        turnOffObjects.Clear();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute
{

}


[AttributeUsage(AttributeTargets.Field)]
public class GetComponentInWorldAttribute : ReadOnlyAttribute
{
    public readonly string gameObjName;
    public GetComponentInWorldAttribute(string _gameObjName = null)
    {
        this.gameObjName = _gameObjName;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class GetComponentAttribute : ReadOnlyAttribute
{
}

[AttributeUsage(AttributeTargets.Field)]
public class GetComponentInChildrenAttribute : ReadOnlyAttribute
{
    public readonly bool includeInactive;

    public string gameObjectKey;

    /// <param name="includeInactive">Should Components on inactive GameObjects be included in the found set?</param>
    public GetComponentInChildrenAttribute(bool includeInactive = false, string _gameObjName = null)
    {
        this.includeInactive = includeInactive;
        this.gameObjectKey = _gameObjName;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class GetComponentInParentAttribute : ReadOnlyAttribute
{
    public readonly bool includeInactive;

    /// <param name="includeInactive">Should inactive Components be included in the found set? (Array and List only)</param>
    public GetComponentInParentAttribute(bool includeInactive = false)
    {
        this.includeInactive = includeInactive;
    }
}







// Restrict to methods only
[AttributeUsage(AttributeTargets.Method)]
public class ExposeMethodInEditorAttribute : Attribute
{
}



[System.AttributeUsageAttribute(System.AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
public class SimpleButtonNewAttribute : PropertyAttribute
{
    private string buttonName = "";
    public string ButtonName
    {
        get { return buttonName; }
        set { buttonName = value; }
    }

    private string functionName = "";
    public string FunctionName
    {
        get { return functionName; }
        set { functionName = value; }
    }

    private bool above = false;
    public bool Above
    {
        get { return above; }
        set { above = value; }
    }

    public SimpleButtonNewAttribute(string _FunctionToCall, bool _Above = false)
    {
        ButtonName = "Method: " + _FunctionToCall + "()";
        FunctionName = _FunctionToCall;
        Above = _Above;
    }
}
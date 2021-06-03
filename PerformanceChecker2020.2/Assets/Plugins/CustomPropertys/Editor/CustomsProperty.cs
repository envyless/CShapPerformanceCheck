using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}


[CustomPropertyDrawer(typeof(SimpleButtonNewAttribute))]
public class SimpleButtonNewDrawer : DecoratorDrawer
{
    //Unity GUI function for Property Drawer
    public override void OnGUI(Rect _Position)
    {
        // First get the attribute
        SimpleButtonNewAttribute tAttribute = attribute as SimpleButtonNewAttribute;

        UnityEngine.GameObject theObject = Selection.activeGameObject; //.GetComponent(tAttribute.ClassType) as UnityEngine.Object;

        if (GUI.Button(_Position, tAttribute.ButtonName))
        {
            //Match found get the function in MethodInfo for Invoke
            //Debug.Log("Found match:  " + TargetObjects[i].GetType() + "   " + tFunction.ClassType);

            if (theObject != null)
            {
                //Invoke the method if != null Note: It works only of you dont need special parameters! (null)
                theObject.SendMessage(tAttribute.FunctionName, null);
            }
        }
    }

    public override float GetHeight()
    {
        return base.GetHeight() * 2f;
    }
}




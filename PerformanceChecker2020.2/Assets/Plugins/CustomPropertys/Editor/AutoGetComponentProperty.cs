using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(MonoBehaviour), true)]
public class MonoBehaviourEditor : Editor
{
    static bool GetComponenets(MonoBehaviour behaviour, object customAttribute, Type elementType, out object components)
    {
        if(customAttribute is GetComponentInWorldAttribute)
        {
            components = GameObject.FindObjectsOfType(elementType);
        }
        else if (customAttribute is GetComponentAttribute)
        {
            var getter = typeof(MonoBehaviour)
                    .GetMethod("GetComponents", new Type[0])
                    .MakeGenericMethod(elementType);
            components = getter.Invoke(behaviour, null);
        }
        else if (customAttribute is GetComponentInChildrenAttribute)
        {
            GetComponentInChildrenAttribute ca = customAttribute as GetComponentInChildrenAttribute;

                var getter = typeof(MonoBehaviour)
        .GetMethod("GetComponentsInChildren", new Type[] { typeof(bool) })
        .MakeGenericMethod(elementType);
                components = getter.Invoke(behaviour,
                        new object[] { ((GetComponentInChildrenAttribute)customAttribute).includeInactive });

            if (ca.gameObjectKey != null)
            {

            }
        }
        else if (customAttribute is GetComponentInParentAttribute)
        {
            var getter = typeof(MonoBehaviour)
                    .GetMethod("GetComponentsInParent", new Type[] { typeof(bool) })
                    .MakeGenericMethod(elementType);
            components = getter.Invoke(behaviour,
                    new object[] { ((GetComponentInParentAttribute)customAttribute).includeInactive });
        }
        else
        {
            components = null;
            return false;
        }

        return true;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (Application.isPlaying)
            return;

        MonoBehaviour behaviour = (MonoBehaviour)target;
        var fields = behaviour.GetType().GetFields(BindingFlags.Public |
                              BindingFlags.NonPublic |
                              BindingFlags.Instance);

        foreach (var field in fields)
        {
            var customAttributes = field.GetCustomAttributes(true);

            foreach (var customAttribute in customAttributes)
            {
                var type = field.FieldType;

                if (type.IsArray)
                {
                    object components;
                    if (GetComponenets(behaviour, customAttribute, type.GetElementType(), out components))
                        field.SetValue(behaviour, components);
                }
                else if (type.IsGenericType)
                {
                    if (type.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        object components;
                        if (GetComponenets(behaviour, customAttribute, type.GetGenericArguments()[0], out components))
                            field.SetValue(behaviour, Activator.CreateInstance(type, components));
                    }
                }
                else
                {
                    if (customAttribute is GetComponentAttribute)
                        field.SetValue(behaviour, behaviour.GetComponent(type));
                    else if (customAttribute is GetComponentInChildrenAttribute)
                    {
                        string goName = ((GetComponentInChildrenAttribute)customAttribute).gameObjectKey;
                        if(goName == null)
                        {
                            field.SetValue(behaviour, behaviour.GetComponentInChildren(type,
                                      ((GetComponentInChildrenAttribute)customAttribute).includeInactive));
                        }
                        else
                        {
                            var finded = behaviour.transform.FindChildGameObject(goName);
                            field.SetValue(behaviour, finded.GetComponent(type));
                        }
                    }                        
                    else if (customAttribute is GetComponentInParentAttribute)
                        field.SetValue(behaviour, behaviour.GetComponentInParent(type));
                    else if (customAttribute is GetComponentInWorldAttribute)
                    {

                        string goName = ((GetComponentInWorldAttribute)customAttribute).gameObjName;
                        if(goName == null)
                        {
                            object[] go = GameObject.FindObjectsOfType(type);
                            if (go != null && go.Length > 0)
                            {
                                field.SetValue(behaviour, go[0]);
                            }
                        }
                        else
                        {
                            Transform [] trs = Resources.FindObjectsOfTypeAll<Transform>();
                            foreach(Transform tr in trs)
                            {
                                if(tr.gameObject.name.Contains(goName))
                                {
                                    var obj = tr.GetComponent(type);
                                    if (obj == null)
                                        continue;

                                    field.SetValue(behaviour, obj);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        // Currently this will only work in the Play mode. You'll see why

        // Get the type descriptor for the MonoBehaviour we are drawing
        var _type = target.GetType();

        // Iterate over each private or public instance method (no static methods atm)
        foreach (var method in _type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
        {
            // make sure it is decorated by our custom attribute
            var attributes = method.GetCustomAttributes(typeof(ExposeMethodInEditorAttribute), true);
            if (attributes.Length > 0)
            {

                if (GUILayout.Button("Run: " + method.Name))
                {
                    // If the user clicks the button, invoke the method immediately.
                    // There are many ways to do this but I chose to use Invoke which only works in Play Mode.
                    ((MonoBehaviour)target).Invoke(method.Name, 0f);
                }
                EditorUtility.SetDirty(target);
            }
        }
    }
}
#endif

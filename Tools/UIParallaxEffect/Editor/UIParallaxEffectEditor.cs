using UnityEditorInternal;
using UnityEditor;
using UnityEngine;
using UIParallax;

[CustomEditor(typeof(UIParallaxEffect))]
public class UIParallaxEffectEditor : Editor
{
    private SerializedProperty parallaxLayers;
    private SerializedProperty autoUpdate;
    private ReorderableList parallaxLayersList;

    private void OnEnable()
    {
        autoUpdate = serializedObject.FindProperty("autoUpdate");
        parallaxLayers = serializedObject.FindProperty("parallaxLayers");
        parallaxLayersList = new ReorderableList(serializedObject, parallaxLayers, true, true, true, true);

        parallaxLayersList.drawHeaderCallback = DrawHeaderCallback;
        parallaxLayersList.drawElementCallback = DrawElementCallback;
        parallaxLayersList.elementHeightCallback += ElementHeightCallback;
        parallaxLayersList.onAddCallback += OnAddCallback;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(autoUpdate, new GUIContent("Auto update parallax", "Set 'false' if you want control parallax by yourself."), true, GUILayout.MinWidth(100));
        EditorGUILayout.Space();
        parallaxLayersList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
    {
        SerializedProperty element = parallaxLayersList.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;

        SerializedProperty elementName = element.FindPropertyRelative("ParallaxLayerName");
        string elementTitle = string.IsNullOrEmpty(elementName.stringValue)
            ? "New layer" : elementName.stringValue;

        EditorGUI.PropertyField(position:
            new Rect(rect.x += 10, rect.y, Screen.width * .8f, height: EditorGUIUtility.singleLineHeight), property:
            element, label: new GUIContent(elementTitle), includeChildren: true);
    }

    private float ElementHeightCallback(int index)
    {
        float propertyHeight = EditorGUI.GetPropertyHeight(parallaxLayersList.serializedProperty.GetArrayElementAtIndex(index), true);
        float spacing = EditorGUIUtility.singleLineHeight / 2;
        return propertyHeight + spacing;
    }

    private void OnAddCallback(ReorderableList list)
    {
        var index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = index;
    }

    private void DrawHeaderCallback(Rect rect)
    {
        EditorGUI.LabelField(rect, "Parallax layers");
    }
}
using DG.DOTweenEditor;
using DG.Tweening.Core;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Hangar))]
public class HangarScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Hangar hangar = (Hangar)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Test hit"))
        {
            DOTweenEditorPreview.Stop();
            DOTweenEditorPreview.PrepareTweenForPreview(hangar.CreateTween());
            DOTweenEditorPreview.Start();
        }
    }
}


[CustomEditor(typeof(GameplaySystem))]
public class GameplaySystemScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameplaySystem hangar = (GameplaySystem)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Test hit"))
        {
            DOTweenEditorPreview.Stop();
            DOTweenEditorPreview.PrepareTweenForPreview(hangar.CreateHitEffectTween());
            DOTweenEditorPreview.Start();
        }
    }
}
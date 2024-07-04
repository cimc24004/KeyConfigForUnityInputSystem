using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Samples.RebindUI;
using UnityEditor;
[CustomEditor(typeof(KeyIconsSetting))]
public class KeyIconsSettingEditor : Editor
{
    public KeyIconsSetting _target;
    private void Awake()
    {
        _target = target as KeyIconsSetting;
    }
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        _target.key_or_pad = (KeyIconsSetting.GamePadOrKeyboard)EditorGUILayout.EnumPopup("‚ ‚ ", _target.key_or_pad);

        if (_target.key_or_pad == KeyIconsSetting.GamePadOrKeyboard.gamepad)
        {
            //_target.controller_setting = EditorGUILayout.
        }else if(_target.key_or_pad == KeyIconsSetting.GamePadOrKeyboard.keyboard)
        {

        }
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(_target);
        }
    }
}

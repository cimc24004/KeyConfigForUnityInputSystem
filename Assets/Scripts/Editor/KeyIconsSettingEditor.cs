using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Samples.RebindUI;
using UnityEditor;
[CustomEditor(typeof(KeyIconsSetting))]
public class KeyIconsSettingEditor : Editor
{
    public KeyIconsSetting _target;
    private SerializedProperty key_setting_property;
    private SerializedProperty controller_setting_property;
    private SerializedProperty m_xbox_property;
    private SerializedProperty m_ps4_property;
    private SerializedProperty key_translate_text_list_property;
    private SerializedProperty key_translate_sprites_list_property;
    private SerializedProperty controller_translate_text_list_property;
    private void OnEnable()
    {
        _target = target as KeyIconsSetting;
        key_setting_property = serializedObject.FindProperty("key_setting");
        controller_setting_property = serializedObject.FindProperty("controller_setting");


        m_xbox_property = controller_setting_property.FindPropertyRelative("xbox");
        m_ps4_property = controller_setting_property.FindPropertyRelative("ps4");

        key_translate_text_list_property = key_setting_property.FindPropertyRelative("translate_text_keys_list");
        key_translate_sprites_list_property = key_setting_property.FindPropertyRelative("translate_sprite_keys_list");

        controller_translate_text_list_property = controller_setting_property.FindPropertyRelative("translate_text_keys_list");

    }
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        _target.key_or_pad = (KeyIconsSetting.GamePadOrKeyboard)EditorGUILayout.EnumPopup("", _target.key_or_pad);
        if (_target.key_or_pad == KeyIconsSetting.GamePadOrKeyboard.gamepad)
        {
            _target.controller_setting.translate_mode_by_sprite = EditorGUILayout.Toggle("画像で指定。", _target.controller_setting.translate_mode_by_sprite);

            if (_target.controller_setting.translate_mode_by_sprite == true)
            {
                //Sprite Translateの場合
                EditorGUILayout.LabelField("Spriteを指定してください gamepad");
                EditorGUILayout.PropertyField(m_xbox_property);
                EditorGUILayout.PropertyField(m_ps4_property);
            }
            else
            {
                //Text Translateの場合
                EditorGUILayout.LabelField("Textを指定してください gamepad");
                EditorGUILayout.PropertyField(controller_translate_text_list_property);
            }

        }
        else if(_target.key_or_pad == KeyIconsSetting.GamePadOrKeyboard.keyboard)
        {
            _target.key_setting.translate_mode_by_sprite = EditorGUILayout.Toggle("画像で指定。", _target.key_setting.translate_mode_by_sprite);
            if (_target.key_setting.translate_mode_by_sprite == true)
            {
                //Sprite Translateの場合
                EditorGUILayout.LabelField("Spriteを指定してください。 keyboard");
                EditorGUILayout.PropertyField(key_translate_sprites_list_property);
            }
            else
            {
                //Text Translateの場合
                EditorGUILayout.LabelField("Textを指定してください keyboard");
                EditorGUILayout.PropertyField(key_translate_text_list_property);
            }
        }
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(_target);
        }
    }
}

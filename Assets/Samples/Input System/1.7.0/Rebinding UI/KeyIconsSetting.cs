using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;
////TODO: have updateBindingUIEvent receive a control path string, too (in addition to the device layout name)

    /// <summary>
    /// This is an example for how to override the default display behavior of bindings. The component
    /// hooks into <see cref="RebindActionUI.updateBindingUIEvent"/> which is triggered when UI display
    /// of a binding should be refreshed. It then checks whether we have an icon for the current binding
    /// and if so, replaces the default text display with an icon.
    /// </summary>
public class KeyIconsSetting : MonoBehaviour
{
    public enum GamePadOrKeyboard {
        keyboard,
        gamepad
    };
    public GamePadOrKeyboard key_or_pad;
    public string key;
    public ControllerSetting controller_setting;
    public KeySetting key_setting;
    protected void OnEnable()
    {
        // Hook into all updateBindingUIEvents on all RebindActionUI components in our hierarchy.
        var rebindUIComponents = transform.GetComponentsInChildren<RebindActionUI>();
        foreach (var component in rebindUIComponents)
        {
            component.updateBindingUIEvent.AddListener(OnUpdateBindingDisplay);
            component.UpdateBindingDisplay();
        }
    }

    protected void OnUpdateBindingDisplay(RebindActionUI component, string bindingDisplayString, string deviceLayoutName, string controlPath)
    {
        if (string.IsNullOrEmpty(deviceLayoutName) || string.IsNullOrEmpty(controlPath))
            return;

        var icon = default(Sprite);
        if (controller_setting.translate_mode_by_sprite)
        {
            if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "DualShockGamepad"))
                icon = controller_setting.ps4.GetSprite(controlPath);
            else if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "Gamepad"))
                icon = controller_setting.xbox.GetSprite(controlPath);
            else if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "Keyboard"))
            {

                //icon = key_setting.translate_sprite_keys_list[Key]
            }
        }


        var textComponent = component.bindingText;
        Debug.Log("OnUpdateBindingDisplayFromKeyIconsSetting.cs");

        var bindingId = new Guid(component.bindingId);
        InputAction action;
        action = component.actionReference.action;
        int bindingIndex = action.bindings.IndexOf(x => x.id == bindingId);
        var overridePath = action.bindings[bindingIndex].overridePath;

        // Grab Image component.
        var imageGO = component.transform.Find("ActionBindingIcon");
        var imageComponent = imageGO.GetComponent<Image>();

        //ここはKeyConfigText翻訳の設定をするところです。
        if (key_or_pad == KeyIconsSetting.GamePadOrKeyboard.gamepad)
        {
            if (!controller_setting.translate_mode_by_sprite)
            {
                KeyIconsSetting.translate_text_lists translate_text_list = controller_setting.translate_text_keys_list.Find(x => x.key_path == overridePath);
                if (translate_text_list != null)
                {
                    textComponent.text = translate_text_list.key_name;
                }
            }

        }
        else if(key_or_pad == KeyIconsSetting.GamePadOrKeyboard.keyboard && !key_setting.translate_mode_by_sprite)
        {
            if (!key_setting.translate_mode_by_sprite)
            {
                KeyIconsSetting.translate_text_lists translate_text_list = key_setting.translate_text_keys_list.Find(x => x.key_path == overridePath);
                if (translate_text_list != null)
                {
                    textComponent.text = translate_text_list.key_name;
                }
            }
            else
            {
                KeyIconsSetting.translate_sprite_lists translate_sprite_keys_list = key_setting.translate_sprite_keys_list.Find(x => x.key_path == overridePath);
                icon = translate_sprite_keys_list.key_sprite;
            }

        }


        if (icon != null)
        {
            textComponent.gameObject.SetActive(false);
            imageComponent.sprite = icon;
            imageComponent.gameObject.SetActive(true);
        }
        else
        {
            textComponent.gameObject.SetActive(true);
            imageComponent.gameObject.SetActive(false);
        }
    }

    [Serializable]
    public struct GamepadIcons
    {
        public Sprite buttonSouth;
        public Sprite buttonNorth;
        public Sprite buttonEast;
        public Sprite buttonWest;
        public Sprite startButton;
        public Sprite selectButton;
        public Sprite leftTrigger;
        public Sprite rightTrigger;
        public Sprite leftShoulder;
        public Sprite rightShoulder;
        public Sprite dpad;
        public Sprite dpadUp;
        public Sprite dpadDown;
        public Sprite dpadLeft;
        public Sprite dpadRight;
        public Sprite leftStick;
        public Sprite rightStick;
        public Sprite leftStickPress;
        public Sprite rightStickPress;

        public Sprite GetSprite(string controlPath)
        {
            // From the input system, we get the path of the control on device. So we can just
            // map from that to the sprites we have for gamepads.
            switch (controlPath)
            {
                case "buttonSouth": return buttonSouth;
                case "buttonNorth": return buttonNorth;
                case "buttonEast": return buttonEast;
                case "buttonWest": return buttonWest;
                case "start": return startButton;
                case "select": return selectButton;
                case "leftTrigger": return leftTrigger;
                case "rightTrigger": return rightTrigger;
                case "leftShoulder": return leftShoulder;
                case "rightShoulder": return rightShoulder;
                case "dpad": return dpad;
                case "dpad/up": return dpadUp;
                case "dpad/down": return dpadDown;
                case "dpad/left": return dpadLeft;
                case "dpad/right": return dpadRight;
                case "leftStick": return leftStick;
                case "rightStick": return rightStick;
                case "leftStickPress": return leftStickPress;
                case "rightStickPress": return rightStickPress;
            }
            return null;
        }
    }
    [Serializable]
    public class KeySetting
    {
        public bool translate_mode_by_sprite;
        [Header("key_pathにはキーのpathを。 key_nameには翻訳先文字を。")]
        public List<translate_text_lists> translate_text_keys_list;
        [Header("key_pathにはキーのpathを。 key_spriteには翻訳先画像を。")]
        public List<translate_sprite_lists> translate_sprite_keys_list;
    }
    [Serializable]
    public class translate_text_lists
    {
        public string key_path;
        public string key_name;
    }
    [Serializable]
    public class translate_sprite_lists
    {
        public string key_path;
        public Sprite key_sprite;
        //public void
    }
    [Serializable]
    public class ControllerSetting
    {
        public bool translate_mode_by_sprite;
        public GamepadIcons xbox;
        public GamepadIcons ps4;
        [Header("key_pathにはpathを。 key_nameには翻訳先を。")]
        public List<translate_text_lists> translate_text_keys_list;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyConfigReset : MonoBehaviour
{
    public InputActionAsset input_action_asset;
    public string targetControlScheme;
    public KeyConfigUI key_config_ui;
    //Reset機能
    public void AllResetRebinding()
    {
        foreach (InputActionMap input_action_map in input_action_asset.actionMaps)
        {
            input_action_map.RemoveAllBindingOverrides();
        }
        key_config_ui.rebind_save_load.Save();
    }
    public void AllResetControlSchemeRebinding()
    {
        //指定したスキーマのみリセット。
        foreach (InputActionMap input_action_map in input_action_asset.actionMaps)
        {
            foreach (InputAction action in input_action_map.actions)
            {
                action.RemoveBindingOverride(InputBinding.MaskByGroup(targetControlScheme));
            }
        }
        key_config_ui.rebind_save_load.Save();
    }
}

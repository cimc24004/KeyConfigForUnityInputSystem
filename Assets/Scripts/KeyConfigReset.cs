using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyConfigReset : MonoBehaviour
{
    public InputActionAsset input_action_asset;
    public string targetControlScheme;
    //Reset機能
    public void AllResetRebinding()
    {
        foreach (InputActionMap input_action_map in input_action_asset.actionMaps)
        {
            input_action_map.RemoveAllBindingOverrides();
        }
    }
    public void AllResetControlSchemeRebinding()
    {
        //ここでコントローラーコンフィグを行ったコントローラーキーをすべてリセットする。
        foreach (InputActionMap input_action_map in input_action_asset.actionMaps)
        {
            foreach (InputAction action in input_action_map.actions)
            {
                action.RemoveBindingOverride(InputBinding.MaskByGroup(targetControlScheme));
            }
        }
    }
}

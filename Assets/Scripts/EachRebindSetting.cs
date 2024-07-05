using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;
public class EachRebindSetting : MonoBehaviour
{
    private RebindActionUI rebind_action_ui;
    public KeyConfigUI key_config_ui;
    private PlayerInput player_input;
    private bool display_once;
    // Start is called before the first frame update
    void Start()
    {
        rebind_action_ui = this.gameObject.GetComponent<RebindActionUI>();
        //key_config_ui.Display.AddListener(rebind_action_ui.CancelOperation());
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnRebindStart(RebindActionUI rebind_action_ui, InputActionRebindingExtensions.RebindingOperation operation)
    {
        Debug.Log("RebindStarttttt");
    }
    public void OnRebindStop(RebindActionUI _rebind_action_ui, InputActionRebindingExtensions.RebindingOperation operation)
    {
        Debug.Log("RebindStoppp");
        if(_rebind_action_ui != null)
        {
            //_rebind_action_ui.actionReference.action.Enable();
        }
    }
    public void OnRebindComplete(RebindActionUI _rebind_action_ui, InputActionRebindingExtensions.RebindingOperation operation)
    {
        Debug.Log("RebindCompleteeee");
        if (_rebind_action_ui != null)
        {
            key_config_ui.rebind_save_load.Save();
        }
    }
    public void OnUpdateUIEvent(RebindActionUI rebind_action_ui, string a, string b, string c, bool start_mode)
    {
        Debug.Log("RebindUpdateUIEvent");
    }
    public void OnClick()
    {
        if(rebind_action_ui != null)
        {
            //rebind_action_ui.actionReference.action.Disable();
        }
    }
}

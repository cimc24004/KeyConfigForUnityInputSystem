using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;
public class KeyConfigUI : MonoBehaviour
{
    private CanvasGroup canvas_group;
    private GameObject GameController;
    private PlayerInput player_input;
    public bool DisplayNow;
    public GameObject Controller;
    public GameObject Key_and_Mouse;
    public Dictionary<GameObject, RebindActionUI> obj_with_action_uis = new Dictionary<GameObject, RebindActionUI>();
    public RebindSaveLoad rebind_save_load;
    // Start is called before the first frame update
    void Start()
    {
        canvas_group = this.gameObject.GetComponent<CanvasGroup>();
        GameController = GameObject.FindWithTag("GameController");
        player_input = GameController.GetComponent<PlayerInput>();
        rebind_save_load = this.gameObject.GetComponent<RebindSaveLoad>();
        //Ç∑Ç◊ÇƒÇÃRebindActionUIÇéÊìæÇµÇƒCancelã@î\ÇçÏÇÈÇÊÅB
        foreach (Transform child in Controller.transform)
        {
            RebindActionUI rebind_action_ui = child.gameObject.GetComponent<RebindActionUI>();
            obj_with_action_uis.Add(child.gameObject, rebind_action_ui);
        }
        foreach (Transform child in Key_and_Mouse.transform)
        {
            RebindActionUI rebind_action_ui = child.gameObject.GetComponent<RebindActionUI>();
            obj_with_action_uis.Add(child.gameObject, rebind_action_ui);
        }
        rebind_save_load.Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (player_input.actions["Menu"].triggered)
        {
            if (!DisplayNow)
            {
                Display();
            }
            else
            {
                Undisplay();
            }
        }
    }
    public void Display()
    {
        canvas_group.alpha = 1;
        DisplayNow = true;
    }
    public void Undisplay()
    {
        CancelRebind();
        DisplayNow = false;
        canvas_group.alpha = 0;
    }
    private void CancelRebind()
    {
        foreach(KeyValuePair<GameObject, RebindActionUI> obj_rebind_ui in obj_with_action_uis)
        {
            obj_rebind_ui.Value.CancelOperation();
        }
    }


}

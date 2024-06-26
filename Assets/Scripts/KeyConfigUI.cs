using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class KeyConfigUI : MonoBehaviour
{
    private CanvasGroup canvas_group;
    private GameObject GameController;
    private PlayerInput player_input;
    public bool DisplayNow;
    // Start is called before the first frame update
    void Start()
    {
        canvas_group = this.gameObject.GetComponent<CanvasGroup>();
        GameController = GameObject.FindWithTag("GameController");
        player_input = GameController.GetComponent<PlayerInput>();
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
        DisplayNow = false;
        canvas_group.alpha = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput player_input;
    private Rigidbody rigid;
    private Vector2 Move;
    public float speed;
    public float JumpAmount = 5;
    private bool JumpMode = false;
    private bool JumpPushed = false;
    public GroundCheck ground_check;
    private GameObject GameController;
    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.FindWithTag("GameController");
        player_input = GameController.GetComponent<PlayerInput>();
        rigid = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move = player_input.actions["Move"].ReadValue<Vector2>();
        JumpMode = player_input.actions["Jump"].triggered;
        if (JumpMode && ground_check.Col)
        {
            JumpPushed = true;
        }
        else
        {
        }
    }
    void FixedUpdate()
    {
        if(Move.magnitude != 0)
        {
            rigid.velocity = new Vector3(Move.x * speed, rigid.velocity.y, Move.y * speed);
        }
        else
        {
            rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
        }
        if (JumpPushed)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, JumpAmount, rigid.velocity.z);
            JumpPushed = false;
        }

    }
}

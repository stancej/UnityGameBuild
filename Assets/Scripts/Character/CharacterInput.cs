using System;
using UnityEngine;  
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class CharacterInput : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    [SerializeField] private Button doButton;
    [SerializeField] private Button dropButton;
    
    [SerializeField] private Joystick movementJoystick;
    [Range(0,1f)][SerializeField] private float movementTrashold;
    [SerializeField]private float _ch_speed = 3.0f;

    public float ch_speed { get; set; }

    private CharacterInventory inventory;
    private CharacterFunctionality functionality;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inventory = GetComponent<CharacterInventory>();
        functionality = GetComponent<CharacterFunctionality>();

        doButton.onClick.AddListener(Do);
        dropButton.onClick.AddListener(DropItem);
        
        ch_speed = _ch_speed;
    }


    private void Update()
    {
        #region JOYSTICK MOVEMENT
        
        
        if (movementJoystick.Horizontal > movementTrashold)
            movement.x = 1f;
        else if (movementJoystick.Horizontal < -movementTrashold)
            movement.x = -1f;
        else
            movement.x = 0;
        
        if (movementJoystick.Vertical > movementTrashold)
            movement.y = 1f;
        else if (movementJoystick.Vertical < -movementTrashold)
            movement.y = -1f;
        else
            movement.y = 0;
        
        #endregion

        if (functionality.animating == true)
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    public void Do()
    {
        if (functionality != null)
        {
            functionality.Do();
        }
    }

    public void DropItem()
    {
        if (inventory != null)
            inventory.DropItem();
    }
    
    private void FixedUpdate()
    {
        if (functionality?.animating == false)
            rb.MovePosition(rb.position + movement * ch_speed * Time.fixedDeltaTime);
        else
            rb.velocity = Vector2.zero;
    }
}

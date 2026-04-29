using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Ply_Soul_Move : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float MoveSpeed; public float getSpeed (){return MoveSpeed;} 
    private Rigidbody2D rb2D;
    private Vector2 Move;
    public InputAction MoveInput;
    [Header("Automatic Move")]
    [SerializeField] private Transform CenterPos;
    [SerializeField] private float AutoMoveSpeed;
    private SpriteRenderer sr;
    private Vector2 CharPos; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CharPos = transform.localPosition;
        rb2D = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        MoveInput.Enable();
    }
    void Update()
    {
        Move = MoveInput.ReadValue<Vector2>();
    } 

    // Update is called once per frame
    void FixedUpdate()
    {
        Moving();
    }
    void Moving()
    {
        rb2D.linearVelocity = Move.normalized*getSpeed();
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float MoveSpeed; public float getSpeed (){return MoveSpeed;} 
    private Rigidbody2D rb2D;
    private Vector2 Move;
    public InputAction MoveInput;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
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

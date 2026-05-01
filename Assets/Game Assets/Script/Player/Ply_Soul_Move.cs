using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

public class Ply_Soul_Move : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float MoveSpeed; public float getSpeed (){return MoveSpeed;} 
    private Rigidbody2D rb2D;
    private Vector2 Move;
    public InputAction MoveInput;
    private bool canMove = true;
    [Header("Automatic Move")]
    [SerializeField] private Vector2 CenterPos;
    [SerializeField] private Vector2 originPos; 
    [SerializeField] private float AutoMoveSpeed = 1f;
    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originPos = transform.position;
        CenterPos = Vector2.zero;
        rb2D = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        MoveInput.Enable();
        hideHeart();
    }
    void Update()
    {
        Move = MoveInput.ReadValue<Vector2>();
    } 
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canMove) {
            rb2D.linearVelocity = Vector2.zero;
            return;
        }
        Moving();
    }
    void Moving()
    {
        rb2D.linearVelocity = Move.normalized*getSpeed();
    }
    //Ini teh supaya setiap masuk enemy turn, si hatinya gerak dari player ke tengah layar, abistu begitu selesai dia balik lagi
    public void doMoveToCenter() => StartCoroutine (MoveToCenter());
    public IEnumerator MoveToCenter()
    {
        canMove = false;
        while (Vector2.Distance(transform.position, CenterPos)>2f)
        {
            transform.position = Vector2.Lerp(transform.position, CenterPos, AutoMoveSpeed*Time.deltaTime);
            yield return null;
        }
        transform.position = CenterPos;
        yield return new WaitForSecondsRealtime(.5f);
        canMove = true;
    }
    public void doMoveToChar() => StartCoroutine (MoveToChar());
    public IEnumerator MoveToChar()
    {
        canMove = false;
        while (Vector2.Distance(transform.position, originPos)>2f)
        {
            transform.position = Vector2.Lerp(transform.position, originPos, AutoMoveSpeed*Time.deltaTime);
            yield return null;
        }
        transform.position = originPos;
        yield return new WaitForSecondsRealtime(1f);
        // canMove = true;
    }
    //Invisible
    public void hideHeart() => sr.color = new Color (1f,1f,1f,0f);
    public void showheart() => sr.color = new Color (1f,1f,1f,1f);
}

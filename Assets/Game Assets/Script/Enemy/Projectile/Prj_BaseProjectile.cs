using UnityEngine;

public class Prj_BaseProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private BoxCollider2D bCol;
    [SerializeField] private float Speed; 
    public void Update()
    {
        Moving();
    }
    public virtual void Moving()
    {
        this.transform.position = Vector2.left*Speed*Time.deltaTime;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit");
        }
    }
}

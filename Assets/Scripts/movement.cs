using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    [SerializeField] private float force;
    [SerializeField] private float cooldown;
    [SerializeField] private Rigidbody2D[] rigidbodies2D = new Rigidbody2D[5];
    private Vector2 dirVec;
    private float nextJump;
    private SpriteRenderer pointerSr;
    private void Awake()
    {
        nextJump = 0f;
        pointerSr = pointer.GetComponent<SpriteRenderer>();
        pointerSr.enabled = false;
    }
    private void FixedUpdate()
    {
        if(isGrounded() && Time.time > nextJump)
        {
            GetDirection();
            if(!pointerSr.enabled)
            {
                pointerSr.enabled = true;
            }
            if(Input.GetButtonDown("Fire1"))
            {
                foreach(Rigidbody2D rb in rigidbodies2D)
                {
                    rb.AddForce(dirVec * force);
                }
                nextJump = Time.time + cooldown;
            }
        }
        else if(pointerSr.enabled)
        {
            pointerSr.enabled = false;
        }
        if(Input.GetButtonDown("reload"))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void GetDirection()
    {
        dirVec = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        dirVec = dirVec.normalized;
        float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        if(angle > 0f)
        {
            pointer.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            pointer.transform.position =  rigidbodies2D[0].position + dirVec;
        }
        else if(angle > -90f)
        {
            dirVec = Vector2.right;
            pointer.transform.rotation = Quaternion.AngleAxis(-90f, Vector3.forward);
            pointer.transform.position =  rigidbodies2D[0].position + dirVec;
        }
        else
        {
            dirVec = Vector2.left;
            pointer.transform.rotation = Quaternion.AngleAxis(90f, Vector3.forward);
            pointer.transform.position =  rigidbodies2D[0].position + dirVec;
        }
    }
    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("obstacles"));
    }
}

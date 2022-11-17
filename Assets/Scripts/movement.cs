using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    [SerializeField] private Animator animator;
    [SerializeField] private float force;
    [SerializeField] private float cooldown;
    private float nextJump;
    private Vector2 dirVec;
    private SpriteRenderer pointerSr;
    private bool isCooldownSet;
    private void Awake()
    {
        nextJump = 0f;
        isCooldownSet = false;
        pointerSr = pointer.GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if(isGrounded() && !isCooldownSet)
        {
            nextJump = Time.time + cooldown;
            isCooldownSet = true;
        }
        if(isGrounded() && Time.time > nextJump)
        {
            GetDirection();
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("arrow_disabled"))
            {
                animator.SetTrigger("dToETrigger");
            }
            if(Input.GetButtonDown("Fire1"))
            {
                GetComponent<Rigidbody2D>().AddForce(dirVec * force);
                isCooldownSet = false;
                animator.SetTrigger("arrowFadeInTrigger");       
            }
        }
        else if(!isGrounded())
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("arrow_enabled"))
            {
                animator.SetTrigger("eToDTrigger");
            }
            if(isCooldownSet)
            {
                isCooldownSet = false;
            }
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
            pointer.transform.position = GetComponent<Rigidbody2D>().position + dirVec;
        }
        else if(angle > -90f)
        {
            dirVec = Vector2.right;
            pointer.transform.rotation = Quaternion.AngleAxis(-90f, Vector3.forward);
            pointer.transform.position = GetComponent<Rigidbody2D>().position + dirVec;
        }
        else
        {
            dirVec = Vector2.left;
            pointer.transform.rotation = Quaternion.AngleAxis(90f, Vector3.forward);
            pointer.transform.position = GetComponent<Rigidbody2D>().position + dirVec;
        }
    }
    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("obstacles"));
    }
}

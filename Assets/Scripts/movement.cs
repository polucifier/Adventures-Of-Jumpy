using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    [SerializeField] private Animator animator;
    [SerializeField] private float force;
    private float cooldown;
    private float nextJump;
    private Vector2 dirVec;
    private bool isCooldownSet;
    private void Awake()
    {
        cooldown = 0.2f;
        nextJump = 0f;
        isCooldownSet = false;
    }
    private void Update()
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
        Debug.DrawLine(transform.position, transform.position+Vector3.down*0.6f, Color.green);
        Debug.DrawLine(transform.position, transform.position+Vector3.left*0.8f, Color.green);
        Debug.DrawLine(transform.position, transform.position+Vector3.right*0.8f, Color.green);
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
        if(Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("obstacles")))
        {
            return true;
        }
        else if(Physics2D.Raycast(transform.position, Vector2.right, 0.8f, LayerMask.GetMask("obstacles")))
        {
            return true;
        }
        else if(Physics2D.Raycast(transform.position, Vector2.left, 0.8f, LayerMask.GetMask("obstacles")))
        {
            return true;
        }
        return false;
    }
}

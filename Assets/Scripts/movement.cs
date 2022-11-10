using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class movement : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject partSys;
    [SerializeField] private float force;
    [SerializeField] private float cooldown;
    private Vector2 dirVec;
    private float nextJump;
    private SpriteRenderer pointerSr;
    private void Awake()
    {
        nextJump = 0f;
        pointerSr = pointer.GetComponent<SpriteRenderer>();
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
                /*foreach(Rigidbody2D rb in rigidbodies2D)
                {
                    rb.AddForce(dirVec * force);
                }*/
                GetComponent<Rigidbody2D>().AddForce(dirVec * force);
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
            //pointer.transform.position =  rigidbodies2D[0].position + dirVec;
            pointer.transform.position = GetComponent<Rigidbody2D>().position + dirVec;
        }
        else if(angle > -90f)
        {
            dirVec = Vector2.right;
            pointer.transform.rotation = Quaternion.AngleAxis(-90f, Vector3.forward);
            //pointer.transform.position =  rigidbodies2D[0].position + dirVec;
            pointer.transform.position = GetComponent<Rigidbody2D>().position + dirVec;
        }
        else
        {
            dirVec = Vector2.left;
            pointer.transform.rotation = Quaternion.AngleAxis(90f, Vector3.forward);
            //pointer.transform.position =  rigidbodies2D[0].position + dirVec;
            pointer.transform.position = GetComponent<Rigidbody2D>().position + dirVec;
        }
    }
    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("obstacles"));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "exit")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            partSys.transform.position = transform.position;
            partSys.GetComponent<ParticleSystem>().Play();
            StartCoroutine(LoadLevelAfterDelay(4));
        }
    }
    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

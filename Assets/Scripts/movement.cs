using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Transform pointer;
    [SerializeField] private float force;
    private Vector2 dirVec;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        GetDirection();
        if(Input.GetButtonDown("reload"))
        {
            SceneManager.LoadScene(0);
        }
        if(Input.GetButtonDown("Fire1"))
        {
            rb.AddForce(dirVec * force);
        }
    }
    private void GetDirection()
    {
        dirVec = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        dirVec = dirVec.normalized;
        float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        if(angle > 0f)
        {
            pointer.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            pointer.position = rb.position + dirVec;
        }
        else if(angle > -90f)
        {
            pointer.rotation = Quaternion.AngleAxis(-90f, Vector3.forward);
            pointer.position = rb.position + new Vector2(1f, 0f);
        }
        else
        {
            pointer.rotation = Quaternion.AngleAxis(90f, Vector3.forward);
            pointer.position = rb.position + new Vector2(-1f, 0f);
        }
    }
}

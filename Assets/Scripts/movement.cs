using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private float force;
    [SerializeField] private Rigidbody2D[] rigidbodies2D = new Rigidbody2D[5];
    private Vector2 dirVec;
    private void FixedUpdate()
    {
        GetDirection();
        if(Input.GetButtonDown("reload"))
        {
            SceneManager.LoadScene(0);
        }
        if(Input.GetButtonDown("Fire1") && isGrounded())
        {
            //rigidbodies2D[0].AddForce(dirVec * force);
            foreach(Rigidbody2D rb in rigidbodies2D)
            {
                rb.AddForce(dirVec * force);
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
            pointer.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            pointer.position =  rigidbodies2D[0].position + dirVec;
        }
        else if(angle > -90f)
        {
            pointer.rotation = Quaternion.AngleAxis(-90f, Vector3.forward);
            pointer.position =  rigidbodies2D[0].position + new Vector2(1f, 0f);
        }
        else
        {
            pointer.rotation = Quaternion.AngleAxis(90f, Vector3.forward);
            pointer.position =  rigidbodies2D[0].position + new Vector2(-1f, 0f);
        }
    }
    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.5f, LayerMask.GetMask("obstacles"));
    }
}

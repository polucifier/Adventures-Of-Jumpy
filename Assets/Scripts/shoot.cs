//using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shoot : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private float force;
    private float angle = 0f;
    private bool direction;
    private Vector3[] positions = new Vector3[2];
    private Vector2 dirVec;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lr.enabled = false;
    }
    private void FixedUpdate()
    {
        CalcAngle(angle);
        dirVec = new Vector2(Mathf.Cos(angle*Mathf.PI/180), Mathf.Sin(angle*Mathf.PI/180));
        positions[0] = rb.position;
        positions[1] = new Vector3(dirVec.x, dirVec.y, 0f)*1.5f + positions[0];
        lr.SetPositions(positions);
        lr.enabled = true;

        if(Input.GetButtonDown("reload"))
        {
            SceneManager.LoadScene(0);
        }
        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(dirVec * force);
        }
    }
    private void CalcAngle(float prevAngle)
    {
        if(prevAngle == 0f)
        {
            direction = true; // true == up
        }
        if(prevAngle == 180f)
        {
            direction = false;
        }
        if(direction)
        {
            angle += 2f;
        }
        else
        {
            angle -= 2f;
        }
    }
}

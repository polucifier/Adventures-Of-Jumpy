using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shoot : MonoBehaviour
{
    private float delay = 0.1f;
    private float maxDragDistance = 2f;
    private Rigidbody2D rb;
    private SpringJoint2D sj;
    private Rigidbody2D slingRb;
    private LineRenderer lr;
    private bool isPressed = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        lr = GetComponent<LineRenderer>();
        slingRb = sj.connectedBody;
        lr.enabled = false;
    }
    private void FixedUpdate()
    {
        if(isPressed)
        {
            Drag();
        }
        if(Input.GetButtonDown("reload"))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void Drag()
    {
        Vector3[] positions = new Vector3[2]; // passing positions to the lr
        positions[0] = rb.position;
        positions[1] = slingRb.position;
        lr.SetPositions(positions);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //drag behavior
        float distance = Vector2.Distance(mousePos, slingRb.position);
        if(distance > maxDragDistance)
        {
            Vector2 direction = (mousePos - slingRb.position).normalized;
            rb.position = slingRb.position + direction * maxDragDistance;
        }
        else
        {
            rb.position = mousePos;
        }
    }
    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
        lr.enabled = true;
    }
    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());
        lr.enabled = false;
    }
    private IEnumerator Release()
    {
        yield return new WaitForSeconds(delay);
        sj.enabled = false;
    }
}

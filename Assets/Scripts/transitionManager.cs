using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionManager : MonoBehaviour
{
    [SerializeField] private animationEventReceiver AER;
    private void Update()
    {
        if(Input.GetButtonDown("reload"))
        {
            AER.FadeToLevel(0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "exit")
        {
            AER.FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

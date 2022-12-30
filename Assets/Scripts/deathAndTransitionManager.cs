using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class deathAndTransitionManager : MonoBehaviour
{
    [SerializeField] private GameObject partSys;
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
        else
        {
            partSys.transform.position = transform.position;
            partSys.GetComponent<ParticleSystem>().Play();
            StartCoroutine(LoadLevelAfterDelay(1.5f));
        }
    }
    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        AER.FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }
}

//this script is level 1 specific

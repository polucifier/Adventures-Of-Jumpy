using UnityEngine;
using UnityEngine.SceneManagement;
public class animationEventReceiver : MonoBehaviour
{
    private int levelIndex;
    [SerializeField] private Animator animator;
    public void FadeToLevel(int index)
    {
        levelIndex = index;
        animator.SetTrigger("fadeOutTrigger");
    }
    private void OnFadeComplete()
    {
        SceneManager.LoadScene(levelIndex);
    }
}

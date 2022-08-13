using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    private Animator animator;


    private void OnMouseEnter() { animator.SetTrigger("Hover"); }

    private void OnMouseExit() { animator.SetTrigger("Exit"); }

    private void OnMouseDown()
    {
        animator.SetTrigger("Pressed");
        StartCoroutine(WaitBeforeAction());
    }

    IEnumerator WaitBeforeAction()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene("MainMenu");
    }
}

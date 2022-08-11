using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
public class BackButton : MonoBehaviour
{
    public Animator animator;

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

    private void OnMouseEnter()
    {
        animator.SetTrigger("Hover");
    }

    private void OnMouseExit()
    {
        animator.SetTrigger("Exit");
    }
}

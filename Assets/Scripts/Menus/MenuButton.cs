using System.Collections;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private MainMenu mainMenu;
    [SerializeField]
    private Animator animator;

    private void Start()
    {
        mainMenu = MainMenu.instance;
    }

    private void OnMouseDown()
    {
        animator.SetTrigger("Pressed");

        StartCoroutine(WaitBeforeAction());
    }

    IEnumerator WaitBeforeAction()
    {
        yield return new WaitForSeconds(0.25f);

        switch (gameObject.name)
        {
            case "PlayButton":
                mainMenu.Play();
                break;

            case "OptionsButton":
                mainMenu.Options();
                break;

            case "ExitButton":
                mainMenu.Exit();
                break;

            default:
                Debug.LogError("Button not found");
                break;
        }
    }

    private void OnMouseEnter()
    {
        animator.SetTrigger("Hover");
    }

    private void OnMouseExit() { animator.SetTrigger("Exit"); }
}
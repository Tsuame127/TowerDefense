using System.Collections;
using System.Net.Sockets;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private MainMenu mainMenu;

    private float startY;
    [SerializeField]
    private float hoverY = 3.5f;
    [SerializeField]
    private float clickedY = 2f;


    [SerializeField]
    private float animSpeed = 15f;


    private void Start()
    {
        mainMenu = MainMenu.instance;
        startY = transform.position.y;
    }

    private void OnMouseDown()
    {
        GoToZPos(clickedY);

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
        GoToZPos(hoverY);
    }



    private void OnMouseExit()
    {
        GoToZPos(startY);
    }


    private void GoToZPos(float targetYPos)
    {
        StopCoroutine("GoToZPosCour");

        StartCoroutine(GoToZPosCour(targetYPos));

    }


    IEnumerator GoToZPosCour(float targetYPos)
    {
        Debug.Log ("CurrentPos: " + transform.position);
        Debug.Log("\nTargetPos: "+ targetYPos);

        float speed = animSpeed;

        if (transform.position.y > targetYPos)
        {
            while (transform.position.y > targetYPos)
            {
                yield return new WaitForSeconds(0.005f);
                transform.position = new Vector3(transform.position.x, transform.position.y - animSpeed, transform.position.z);
            }
        }
        else
        {
            while (transform.position.y < targetYPos)
            {
                yield return new WaitForSeconds(0.005f);
                transform.position = new Vector3(transform.position.x, transform.position.y + animSpeed, transform.position.z);
            }
        }
        

    }

}
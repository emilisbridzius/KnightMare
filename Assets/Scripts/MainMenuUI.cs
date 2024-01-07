using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainMenuState
{
    START,
    MENU
}

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuButtonsObj;
    [SerializeField] private Animator logoAnim;

    private MainMenuState menuState;

    [SerializeField] private GameObject startTransition;

    // Start is called before the first frame update
    void Start()
    {
        menuState = MainMenuState.START;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (menuState == MainMenuState.START)
            {
                // sfx here
                logoAnim.SetBool("Confirm", true);
                menuButtonsObj.SetActive(true);
            }
        }
    }

    public void StartButton()
    {
        StartCoroutine(StartTransition());
    }

    private IEnumerator StartTransition()
    {
        startTransition.SetActive(true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("BedroomScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

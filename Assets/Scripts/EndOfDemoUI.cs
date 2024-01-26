using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndOfDemoUI : MonoBehaviour
{
    [SerializeField] private GameObject fadeIn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(ReturnToMenu());
        }
    }

    private IEnumerator ReturnToMenu()
    {
        fadeIn.SetActive(true);

        yield return new WaitForSeconds(2.2f);

        SceneManager.LoadScene("MainMenu");
    }
}

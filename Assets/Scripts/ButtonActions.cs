using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    // ui cutscene thingymabob
    [SerializeField] private GameObject sleepyTime;

    // Script for button activations, set through the canvas button editor

    // yes button
    public void YesButton()
    {
        StartCoroutine(GoToSleep());
    }

    private IEnumerator GoToSleep()
    {
        sleepyTime.SetActive(true);

        // temp
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Forest");
    }
}

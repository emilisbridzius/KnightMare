using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomManager : MonoBehaviour
{
    [SerializeField] private GameObject keyObj;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        StartCoroutine(SpawnKeyDelayed());
    }


    // taking a precaution cus if data loads on the same frame you check the data it might not work?
    private IEnumerator SpawnKeyDelayed()
    {
        yield return new WaitForSeconds(0.5f);

        if (player.ArtifactCount >= 5)
        {
            keyObj.SetActive(true);
        }
    }
}

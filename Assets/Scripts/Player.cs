using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // makin it public cus i cant be arsed
    public List<PlayerFlags> playerFlags;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // look through list of flags to get the correct bool
    public bool GetFlagBool(string flagName)
    {
        for (int i = 0; i < playerFlags.Count; i++)
        {
            if (flagName.ToLower() == playerFlags[i].FlagName.ToLower())
            {
                //Debug.Log(playerFlags[i].FlagName + " is " + playerFlags[i].IsActive);
                return playerFlags[i].IsActive;
            }
        }

        // default to false if the flag name doesn't exist
        Debug.LogWarning(flagName + " could not be found, returning false as a fallback");
        return false;
    }

    // activate a specific flag
    public void SetFlagBool(string flagName)
    {
        for (int i = 0; i < playerFlags.Count; i++)
        {
            if (flagName.ToLower() == playerFlags[i].FlagName.ToLower())
            {
                Debug.Log("Set " + playerFlags[i].FlagName + " to true");
                playerFlags[i].IsActive = true;
            }
        }
    }

    // deactivate a specific flag
    public void DeactivateFlagBool(string flagName)
    {
        for (int i = 0; i < playerFlags.Count; i++)
        {
            if (flagName.ToLower() == playerFlags[i].FlagName.ToLower())
            {
                Debug.Log("Set " + playerFlags[i].FlagName + " to false");
                playerFlags[i].IsActive = false;
            }
        }
    }

    public void SaveData()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        playerFlags = data.playerFlags;
    }
}

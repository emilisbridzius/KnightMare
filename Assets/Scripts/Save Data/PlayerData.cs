using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public List<PlayerFlags> playerFlags;

    public PlayerData(Player player)
    {
        playerFlags = player.playerFlags;
    }
}

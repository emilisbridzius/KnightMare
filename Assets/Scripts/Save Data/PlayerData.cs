using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public List<PlayerFlags> playerFlags;

    public int artifactCount;

    public PlayerData(Player player)
    {
        playerFlags = player.playerFlags;
        artifactCount = player.ArtifactCount;
    }
}

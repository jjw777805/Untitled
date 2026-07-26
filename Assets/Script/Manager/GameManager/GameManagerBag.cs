

using MyObject;
using UnityEngine;

public partial class GameManager 
{
    public void PlayerBagAdd(string key,CollectionCommonData sc)
    {
        if (!IsInstance())
        {
            instance.PlayerBagAdd(key,sc);
        }

        if (playerData.HasCollected(key))
        {
            Debug.LogError("Error!");
        }

        playerData.BagAdd(key,sc);
    }

    public bool HasCollected(string key)
    {
        if(!IsInstance())return instance.HasCollected(key);
        return playerData.HasCollected(key);
    }   
}

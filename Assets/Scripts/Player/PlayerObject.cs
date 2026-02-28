using UnityEngine;
using System.Collections;

[DefaultExecutionOrder(-100)]
public class PlayerObject : MonoBehaviour
{
    // Script that provides static tools associated with the player GameObject
    // Is a Singleton design attached to the player gameobject

    private static GameObject player;

    private void Start()
    {
        if (player == null)
        {
            player = gameObject;
        }
        else
        {
            Debug.LogError("Multiple PlayerObject Scripts Detected");
        }
    }

    public static GameObject getPlayer()
    {
        if (player == null)
        {
            Debug.LogError("Player Object not defined yet");
        }

        return player;
    }
}

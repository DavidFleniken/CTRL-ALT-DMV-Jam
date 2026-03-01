using UnityEngine;

public class RenderOrderDecider : MonoBehaviour
{
    // placed on parent empty object
    SpriteRenderer sr;
    GameObject player;
    BoxCollider2D playerBC;
    BoxCollider2D objectBC;
    float objectPivot = 0;
    int abovePlayerOrder = 11;
    int belowPlayerOrder = 9;

    private void Start()
    {
        if (TryGetComponent<BoxCollider2D>(out objectBC))
        {
            objectPivot = objectBC.bounds.center.y;
        }
        else
        {
            objectPivot = transform.position.y;
        }

        sr = GetComponentInChildren<SpriteRenderer>();
        player = PlayerObject.getPlayer();
        playerBC = player.GetComponent<BoxCollider2D>();
        if (player == null)
        {
            Debug.LogError("No Player Found");
        }

    }

    private void Update()
    {
        if (playerBC.bounds.center.y > objectPivot)
        {
            sr.sortingOrder = abovePlayerOrder;
        }
        else
        {
            sr.sortingOrder = belowPlayerOrder;
        }
    }
}

using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform; //Not recommended if game has more than 1 player, if so, set public Transform player, then drag player in in Unity Editor

    }

    private void LateUpdate() //get called in the very end of each frame, in this case to make sure the camera gets updated after Mario's position has been updated
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }
}

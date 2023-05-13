using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

    public float height = 7f; //6.5f in tutorial video (part 5)
    public float undergroundHeight = -9f;

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

    //passed variable is called 'underground' in tutorial video (part 5)
    public void SetUnderground(bool isUnderground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = isUnderground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}

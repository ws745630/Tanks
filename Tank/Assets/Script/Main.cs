using UnityEngine;

public class Main : MonoBehaviour
{
    private int centerX = Screen.width / 2;
    private int centerY = Screen.height / 2;
    private int y = 0;
    void Update()
    {
        if (y >= centerY)
        {
            return;
        }
        y++;
        transform.position = new Vector3(centerX, y, 0);
    }
}
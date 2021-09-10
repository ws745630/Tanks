
using UnityEngine;

public class Bullect : MonoBehaviour
{
    public float moveSpeed = 10; //移动速度
    public bool isPlayerBullect;// 是否是玩家发射子弹
    private void Start()
    {

    }
    private void Update()
    {
        // 移动当前子弹  沿着自身的y轴移动  如果沿着自身移动，则必须以世界轴为准则
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }
    // 碰撞检测 触发器 进入 - 接触 - 退出
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Tank":
                if (!isPlayerBullect)
                {
                    other.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                other.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullect)
                {
                    other.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Destroy(other.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                if (isPlayerBullect)
                {   // 只有玩家的子弹才会播放音效
                    other.SendMessage("PlayerAudio");
                }
                Destroy(gameObject);
                break;
            default: break;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {

    }
    private void OnTriggerExit2D(Collider2D other)
    {

    }
}
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 0.2f);// 0.2秒后销毁爆炸效果
    }
}
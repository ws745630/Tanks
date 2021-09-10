using UnityEngine;
public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;//精灵渲染器 改变图片
    public Sprite BrokenSprite; //老窝被消灭时显示的图片
    public GameObject explosionPrefab;// 爆炸特效
    public AudioClip dieAudio;//老家被消灭的声音
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Die()
    {
        sr.sprite = BrokenSprite;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        /// 老家被毁 更新状态
        PlayerManager.Instance.isDefead = true;
        // 播放声音
        AudioSource.PlayClipAtPoint(dieAudio,transform.position);
    }
}
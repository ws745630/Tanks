using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3; // 坦克移动速度
    private Vector3 bullectEulerAngles; // 子弹的角度
    private float v = -1; //默认坦克朝下
    private float h;
    public Sprite[] tankSprite;// 保存坦克不同方向的图片 上右下左
    private SpriteRenderer sr; //精灵渲染器,修改对象的属性；修改坦克不同方向的图片 达到控制坦克方向；
    public GameObject bullectPrefab; // 子弹的预制体
    public GameObject explosionPrefab; // 爆炸特效

    private float timeVal; // 发射子弹间隔
    private float timeValChangeDirection = 0;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>(); //获取精灵渲染器
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 攻击间隔
        if (timeVal >= 3f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    // 坦克攻击方法
    private void Attack()
    {
        // 子弹的角度：坦克的角度(方向)+子弹的角度(方向)
        Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
        timeVal = 0;
    }
    //坦克的移动方法 监听键盘方向键,移动坦克
    private void Move()
    {
        if (timeValChangeDirection >= 4)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                v = 0;
                h = -1;
            }
            else if (num >= 2 && num <= 4)
            {
                v = 0;
                h = 1;
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0) //下边
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, 180);
        }
        if (v > 0) //上边
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }
        if (v != 0)
        {
            return;
        }

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0) //左边
        {
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }
        if (h > 0) //右边
        {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }

    }
    // 坦克死亡
    private void Die()
    {
        PlayerManager.Instance.playerScore++;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        // 死亡方法
        Destroy(gameObject);
    }
    /// 碰撞检测 坦克碰撞在一起就调整方向 防止坦克不动
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            timeValChangeDirection = 4;
        }
    }
}
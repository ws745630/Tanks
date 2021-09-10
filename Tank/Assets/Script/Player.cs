using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 3; // 坦克移动速度
    private Vector3 bullectEulerAngles; // 子弹的角度
    private float timeVal; // 发射子弹间隔
    private float defendedTime = 3; // 出生时；无敌时间
    private bool isDefended = true; // 是否处于无敌
    public Sprite[] tankSprite;// 保存坦克不同方向的图片 上右下左
    private SpriteRenderer sr; //精灵渲染器 可以修改对象的属性 这里修改坦克的图片；
    public GameObject bullectPrefab; // 子弹的预制体
    public GameObject explosionPrefab; // 爆炸特效
    public GameObject defendEffectPrefab; //无敌特效 
    public AudioSource moveAudio; //移动音效对象
    public AudioClip[] audioClips; //音效素材
    private void Awake(){
        sr = GetComponent<SpriteRenderer>(); //获取精灵渲染器
    }
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        // 是否处于无敌
        if (isDefended){
            defendEffectPrefab.SetActive(true);
            defendedTime -= Time.deltaTime;
            if (defendedTime <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
        
    }
    private void FixedUpdate()
    {
        // 如果游戏失败  则游戏停止移动
        if (PlayerManager.Instance.isDefead){
            return;
        }
        Move();
        // 攻击间隔
        if (timeVal >= 0.4f){
            Attack();
        }else{
            timeVal += Time.fixedDeltaTime;
        }
    }
    // 坦克攻击方法
    private void Attack(){
        if (Input.GetKeyDown(KeyCode.Space)){
            // 子弹的角度：坦克的角度+子弹的角度
            Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
            timeVal = 0;
        }
    }
    //坦克的移动方法 监听键盘方向键,移动坦克
    private void Move(){
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        //下边
        if (v < 0) {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, 180);
        }
         //上边
        if (v > 0){
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }
        
        if (Math.Abs(v) > 0.05f)
        {
            moveAudio.clip = audioClips[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }else{
            moveAudio.clip = audioClips[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        if (v != 0){
            return;
        }
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        //左边
        if (h < 0) {
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }
        //右边
        if (h > 0) {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }

        if (Math.Abs(h) > 0.05f)
        {
            moveAudio.clip = audioClips[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }else{
            moveAudio.clip = audioClips[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }

    }
    // 坦克死亡
    private void Die()
    {
        if (isDefended){
           return;   
        }
        PlayerManager.Instance.isDead = true;
        // 暴躁效果
        Instantiate(explosionPrefab,transform.position,transform.rotation);
        // 死亡方法
        Destroy(gameObject);
    }

}

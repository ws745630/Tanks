using System;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    public int lifeValue = 3;//生命值
    public int playerScore = 0;// 分数
    public bool isDead; //是否死亡
    public bool isDefead;//游戏是否失败
    private int checkpointLevel = 1; //关卡等级 等级越高消灭敌人数量就越多

    public GameObject born;// 出生特效，里面会创建坦克
    public Text playerScoreText; // 分数对象
    public Text playerLifeValueText;// 玩家对象
    public GameObject defeatImage; // 游戏失败对象

    private static PlayerManager instance; //单例
    public static PlayerManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

    }
    private void Update()
    {
        //游戏失败显示失败UI{
        if (isDefead)
        {
            defeatImage.SetActive(true);
            Invoke("ReturenToTheMainMenu", 2f);
            return;
        }
        if (isDead)
        {
            Recover();
        }
        
        playerScoreText.text = $"x {playerScore}";
        playerLifeValueText.text = $"x {lifeValue}";
    }
    // 玩家被杀死  复活
    private void Recover()
    {
        // 生命值归零 返回主页面
        if (lifeValue <= 0) 
        {
            isDefead = true;
            Invoke("ReturenToTheMainMenu", 2f);
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }
    /// 每个关卡消灭敌人的个数
    public int MaxDestroyEnemy(){
       return 5 * checkpointLevel;
    }
    // 返回开始页面
    private void ReturenToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
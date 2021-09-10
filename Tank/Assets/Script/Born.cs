using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab; //玩家预制体
    public GameObject[] enemyPrefabList;//敌人数组
    public bool createPlayer; //是否为玩家
    private void Start()
    {
        Invoke("BornTank", 1f);  //延时调用创建坦克出生方法
        Destroy(gameObject, 1f); //延时1秒后销毁
    }

    private void BornTank()
    {
        if (createPlayer)
        { // 创建一个出生特效  位置  无旋转
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {   // 随机创建敌人
            int num = Random.Range(0, 3);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }
    }
}
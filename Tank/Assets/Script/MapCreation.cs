using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] items;//游戏物体初始化数组 0：老家，1.墙 2.障碍 3.出生效果 4.河流 5.草 6.空气墙
    private List<Vector3> itemPositionList = new List<Vector3>();//已经有对象的位置列表
    private void Awake()
    {
        InitMap();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // 初始化地图
    private void InitMap()
    {
        InitHeart();
        InitExternalBarrier();
        InitPlyer();
        InitEnemy();
        InitBarrier();
    }
    /// 创建地图
    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotion)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotion);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }
    // 产生随机位置
    private Vector3 CreateRandomPosition()
    {
        //不生成x = -10,10的2列  y= -8,8正2行的位置
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
        }
    }
    // 判断位置列表是否有生成的位置
    private bool HasThePosition(Vector3 createPos)
    {
        return itemPositionList.Contains(createPos);
    }
    /// 初始化障碍物
    private void InitBarrier()
    {
        // 创建地图 墙 
        for (int i = 0; i < 30; i++)
        {
            CreateItem(items[1], CreateRandomPosition(), Quaternion.identity);
        }
        // 创建地图 障碍  
        for (int i = 0; i < 30; i++)
        {
            CreateItem(items[2], CreateRandomPosition(), Quaternion.identity);
        }
        // 创建地图 河流 
        for (int i = 0; i < 15; i++)
        {
            CreateItem(items[4], CreateRandomPosition(), Quaternion.identity);
        }
        // 创建地图 草 
        for (int i = 0; i < 10; i++)
        {
            CreateItem(items[5], CreateRandomPosition(), Quaternion.identity);
        }
    }
    private void InitPlyer()
    {
        GameObject go = Instantiate(items[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;
    }
    // 初始化敌人
    private void InitEnemy()
    {
        CreateItem(items[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(items[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(items[3], new Vector3(10, 8, 0), Quaternion.identity);
        // 创建敌人 每4秒执行5次方法
        InvokeRepeating("CreateEnemy", 4, 5);
    }
    // 创建敌人
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if (num == 0)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }
        else if (num == 1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else
        {
            EnemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(items[3], EnemyPos, Quaternion.identity);
    }
    // 初始化老家 
    private void InitHeart()
    {
        CreateItem(items[0], new Vector3(0, -8, 0), Quaternion.identity);
        // 创建老家的墙
        CreateItem(items[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(items[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(items[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
    }
    // 初始化外部障碍
    private void InitExternalBarrier()
    {
        // 创建空气墙
        for (int i = -11; i < 12; i++)
        {
            CreateItem(items[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            CreateItem(items[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(items[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(items[6], new Vector3(11, i, 0), Quaternion.identity);
        }
    }
}


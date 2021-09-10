using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Option : MonoBehaviour
{

    private int choice = 1;
    public Transform posOne;
    public Transform posTow;
    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            choice = 1;
            transform.position = posOne.position;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            choice = 2;
            transform.position = posTow.position;
        }
        if (choice == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }
}
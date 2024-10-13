using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq; // สำหรับใช้ LINQ
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class JosephusController : MonoBehaviour
{
    public ResultJosephus resultJosephus;
    public List<int> ResultList;
    public int currentIndex = 0;
    public NavMeshAgent agent;
    public string choose;
    public Text resultText;
    public bool isWin = false;
    public bool isGameOver = false;
    public GameObject explosionEffect, spriteEnemy;
    Animator animator;
    private const string HIGH_SCORE_KEY = "HighScore";
    public int surviveDay = 0;
    public bool isSeek = false;
    void Start()
    {
        surviveDay = PlayerPrefs.GetInt(HIGH_SCORE_KEY);
        if(surviveDay == null){
            surviveDay = 0;
        }
        choose = PlayerPrefs.GetString("choose");
        spriteEnemy.SetActive(true);
        animator = spriteEnemy.GetComponent<Animator>();
        //StartCoroutine(DisplayResult());
        StartCoroutine(waitForStart());
    }

    void Update()
    {
        if (isWin)
        {
            StartCoroutine(flyingAway());
            
        }

        if (isGameOver)
        {
            /*Time.timeScale = 0;
            GameObject instance = Instantiate(Resources.Load("prefabs/CanvasGameOver", typeof(GameObject))) as GameObject;
            isGameOver = false;*/
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }

    IEnumerator flyingAway()
    {
        yield return new WaitForSeconds(0.9f);
        if (explosionEffect != null)
        {
            animator.SetTrigger("isGone");
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        StartCoroutine(Winning());
    }

    IEnumerator Winning()
    {
        yield return new WaitForSeconds(1.0f);
        surviveDay = surviveDay+1;
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, surviveDay);
        Time.timeScale = 0;
        SceneManager.LoadScene("Win", LoadSceneMode.Single);
        /*GameObject instance = Instantiate(Resources.Load("prefabs/CanvasWin", typeof(GameObject))) as GameObject;
        Time.timeScale = 0;
        isWin = false;*/

    }

    IEnumerator waitForStart()
    {
        yield return new WaitForSeconds(2.0f);
        ResultList = resultJosephus.result;
        StartCoroutine(DisplayResult());
    }

    IEnumerator DisplayResult()
    {

        int showCount = ResultList.Count;
        int final = showCount - 1;
        while (currentIndex < final)
        {
            // หา GameObject เป้าหมาย
            GameObject target = FindTargetGameObject(ResultList[currentIndex]);

            if (target != null)
            {
                // ตั้งค่า destination ให้ NavMeshAgent
                resultText.text = "I'm looking for " + ResultList[currentIndex];
                agent.destination = target.transform.position;
                if (ResultList[currentIndex] == int.Parse(choose))
                {
                    isSeek = true;
                }
                else
                {
                    isSeek = false;
                }

            }
            else
            {
                Debug.LogError("Target not found: " + ResultList[currentIndex]);
            }
            currentIndex++;
            yield return new WaitForSeconds(2.0f);
        }
        isWin = true;
        resultText.text = "It's so boring. I'm out of here.";
    }

    // ฟังก์ชันหา GameObject เป้าหมายตามชื่อ
    private GameObject FindTargetGameObject(int targetNumber)
    {
        string targetName = "" + targetNumber;
        return GameObject.Find(targetName);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isSeek)
        {
            if (other.gameObject.name == choose)
            {
                isGameOver = true;
            }
        }

    }
}
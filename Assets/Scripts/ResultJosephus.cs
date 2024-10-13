using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using System.Linq; // สำหรับใช้ LINQ

public class ResultJosephus : MonoBehaviour
{
    public GameObject cubePrefab; // Prefab ของ Cube ที่จะสร้าง
    public int numberCubes;
    public float radius = 5f; // รัศมีของวงกลม
    public float cubeScale = 1f; // ขนาดของ Cube
    public string answer, choose;
    public int n;
    public int k = 2;
    public List<int> result { get; set; }
    private int currentIndex = 0;
    public string skill;
    void Start()
    {
        skill = PlayerPrefs.GetString("level");
        if(skill == "easy"){
            radius = 5f;
            Camera.main.transform.position = new Vector3(0f, 9f, -9f);
        }
        if(skill == "normal"){
            radius = 7f;
            Camera.main.transform.position = new Vector3(0f, 10f, -10f);
        }
        if(skill == "hard"){
            radius = 10f;
            Camera.main.transform.position = new Vector3(0f, 13f, -13f);
        }

        numberCubes = (int)PlayerPrefs.GetInt("total");
        n = numberCubes;
        answer = PlayerPrefs.GetString("answer");
        choose = PlayerPrefs.GetString("choose");
        CreateCircleOfCubes(numberCubes);
        result = JosephusProblem.Solve(n, k);
        //Debug.Log("ลำดับการกำจัด: " + string.Join(", ", result));
        StartCoroutine(DisplayResult());
    }

    IEnumerator DisplayResult()
    {
        while (currentIndex < result.Count)
        {
            currentIndex++;
            yield return new WaitForSeconds(1f);
        }
    }

    public List<int> GetResultList()
    {
        return result; // สมมติว่ามีตัวแปร resultList เก็บค่า
    }

    void Update()
    {

    }

    void CreateCircleOfCubes(int numberOfCubes)
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfCubes;
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, 0.5f, Mathf.Sin(angle) * radius);
            GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
            cube.transform.localScale = Vector3.one * cubeScale;
            cube.name = (i + 1).ToString();
        }
    }
}

public class JosephusProblem
{
    public static List<int> Solve(int n, int k)
    {
        List<int> people = new List<int>();
        for (int i = 1; i <= n; i++)
        {
            people.Add(i);
        }

        List<int> eliminationOrder = new List<int>();
        int index = 0;
        while (people.Count > 0)
        {
            index = (index + k - 1) % people.Count;
            eliminationOrder.Add(people[index]); // เพิ่มหมายเลขคนที่ถูกกำจัดเข้าไปใน list
            people.RemoveAt(index);
            // ปรับ index ให้ถูกต้องหลังจากลบสมาชิกออกไป
            if (index >= people.Count)
            {
                index = 0;
            }
        }

        return eliminationOrder;
    }
}

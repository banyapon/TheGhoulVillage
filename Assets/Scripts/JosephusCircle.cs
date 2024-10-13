using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JosephusCircle : MonoBehaviour
{
    public GameObject cubePrefab; // Prefab ของ Cube ที่จะสร้าง
    public int numberOfCubes = 7; // จำนวน Cube ที่ต้องการสร้าง
    public float radius = 5f; // รัศมีของวงกลม
    public float cubeScale = 1f; // ขนาดของ Cube
    public float z_camera = -6f;
    public float cameraZoomSpeed = 1f; // ความเร็วในการซูมของกล้อง
    public int survivor;
    public float timeLeft = 60.0f;
    public Text startText; 
    public bool isCal = false;
    public bool vaseOn = false;
    public Text resultText;
    public string skill;
    void Start()
    {
        skill = PlayerPrefs.GetString("level");
        if(skill == "easy"){
            numberOfCubes = Random.Range(2,12);
            radius = 5f;
            Camera.main.transform.position = new Vector3(0f, 9f, -9f);
        }
        if(skill == "normal"){
            numberOfCubes = Random.Range(13,20);
            radius = 7f;
            Camera.main.transform.position = new Vector3(0f, 10f, -10f);
        }
        if(skill == "hard"){
            numberOfCubes = Random.Range(21,40);
            radius = 10f;
            Camera.main.transform.position = new Vector3(0f, 13f, -13f);
        }
        // สร้าง Cube เป็นวงกลม
        
        //Camera.main.transform.position = new Vector3(0f, 8f, z_camera);
        //ZoomCamera();
        CreateCircleOfCubes();
        int totalPeople = numberOfCubes; // จำนวนคนทั้งหมด
        int killEvery = 2; // กำจัดทุกๆ 3 คน

        survivor = JosephusProblem(totalPeople, killEvery);
        //Debug.Log("ผู้รอดชีวิตคือคนที่: " + survivor);
        PlayerPrefs.SetString("answer",survivor.ToString());
        
    }

    void Update(){
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            timeLeft = 0;
            isCal = true;
        }

        if(isCal){
            
            isCal = false;
            startText.text = " ";
            resultText.text = "Choose the vase to jump into.";
            vaseOn = true;
        }
    }

    void CreateCircleOfCubes()
    {
        PlayerPrefs.SetInt("total",numberOfCubes);
        // สร้าง Cube และกำหนดตำแหน่งเป็นวงกลม
        for (int i = 0; i < numberOfCubes; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfCubes;
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, 0.5f, Mathf.Sin(angle) * radius);

            GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
            cube.transform.localScale = Vector3.one * cubeScale;

            // กำหนดชื่อ Cube ให้เป็นตัวเลขเรียงกัน
            cube.name = (i + 1).ToString();
            if (int.TryParse(cube.name, out int cubeNumber) && cubeNumber == survivor)
            {
                cube.tag = "Save";
            }
        }
    }

    void ZoomCamera()
    {
        // คำนวณระยะห่างที่เหมาะสมสำหรับการซูม
        float distance = radius * cameraZoomSpeed * Mathf.Log(numberOfCubes, 2f);
        Camera.main.transform.position = new Vector3(0f, 0f, -distance);
    }

    public int JosephusProblem(int n, int k)
    {
        // n คือ จำนวนคนทั้งหมดในวงกลม
        // k คือ ทุกๆ k คน จะถูกกำจัดออกไป

        // สร้าง List เพื่อเก็บข้อมูลผู้เล่น
        List<int> people = new List<int>();
        for (int i = 1; i <= n; i++)
        {
            people.Add(i);
        }

        int index = 0;
        while (people.Count > 1)
        {
            // กำจัดคนที่ index
            index = (index + k - 1) % people.Count;
            people.RemoveAt(index);
        }

        // คืนค่า index ของผู้รอดชีวิตคนสุดท้าย
        return people[0];
    }
}
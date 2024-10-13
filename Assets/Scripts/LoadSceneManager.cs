using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    string currentSceneName;
    public string audioPath = "sounds/sfx/click";
    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneObject(sceneName));
    }

    public void ForceLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (currentSceneName == "Launcher")
            {
                Application.Quit();
            }
        }
    }

    public void QuitGameNow(){
        Application.Quit();
    }

    public IEnumerator LoadSceneObject(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        async.allowSceneActivation = false;

        // Loop เพื่อตรวจสอบว่าโหลด Object เสร็จหรือยัง
        while (!async.isDone)
        {
            // ทำการคำนวณ progress
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            GameObject instance = Instantiate(Resources.Load("prefabs/canvasloading", typeof(GameObject))) as GameObject;
            Debug.Log("Loading progress: " + (progress * 100).ToString("n0") + "%");

            // Loading completed
            if (progress == 1f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        // โหลด AudioClip จาก Resources
        AudioClip clip = Resources.Load<AudioClip>(audioPath);

        // ตรวจสอบว่าโหลด AudioClip สำเร็จหรือไม่
        if (clip != null)
        {
            // เล่นเสียงแบบ One-shot
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
        else
        {
            Debug.LogError("ไม่พบไฟล์เสียง: " + audioPath);
        }
    }
}
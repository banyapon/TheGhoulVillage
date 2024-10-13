using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextChapter : MonoBehaviour
{
    public float delayTime = 70f; // ระยะเวลาหน่วง (วินาที)

    void Start()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(delayTime);

        // ทำให้หน้าจอดำ (ปรับค่าสีตามต้องการ)
        Color color = Color.black;
        float fadeDuration = 2f; // ระยะเวลาที่ใช้ในการ fade (วินาที)
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            Color.Lerp(Color.clear, color, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // เปลี่ยน Scene
        SceneManager.LoadScene("Launcher");
    }
}

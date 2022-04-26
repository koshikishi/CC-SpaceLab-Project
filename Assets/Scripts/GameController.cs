using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Volume postProcessingLocalVolume;
    public VolumeProfile postProcessingProfile;

    Camera m_MainCamera;
    float m_FadeInOutMultiplier = 5f;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;

        OnGameStartHandler();
    }

    // Update is called once per frame
    void Update()
    {
        postProcessingLocalVolume.gameObject.transform.position = m_MainCamera.transform.position;
    }

    void OnGameStartHandler() {
        StartCoroutine(nameof(FadeIn));
    }

    public void GameOver()
    {
        if (postProcessingProfile != null)
        {
            postProcessingLocalVolume.profile = postProcessingProfile;
        }

        StartCoroutine(nameof(FadeOut));
    }

    IEnumerator FadeIn()
    {
        while (postProcessingLocalVolume.weight > 0)
        {
            postProcessingLocalVolume.weight -= Time.deltaTime / m_FadeInOutMultiplier;

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeOut()
    {
        while (postProcessingLocalVolume.weight < 1)
        {
            postProcessingLocalVolume.weight += Time.deltaTime / m_FadeInOutMultiplier;

            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

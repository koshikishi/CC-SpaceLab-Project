using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StarterAssets;

public class GameController : MonoBehaviour
{
    [Header("Post Processing")]
    public Volume postProcessingLocalVolume;
    public VolumeProfile postProcessingProfile;

    [Space]
    [Header("User Interface")]
    public GameObject gameStartScreen;
    public GameObject gameEndScreen;
    public StarterAssetsInputs inputsScript;

    Camera m_MainCamera;
    float m_FadeInOutMultiplier = 5f;
    CursorLockMode m_InitialCursorLockMode;

    void Start()
    {
        m_MainCamera = Camera.main;
        m_InitialCursorLockMode = Cursor.lockState;

        OnGameStartHandler();
    }

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

    public void GameRestart()
    {
        Cursor.lockState = m_InitialCursorLockMode;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator FadeIn()
    {
        while (postProcessingLocalVolume.weight > 0)
        {
            postProcessingLocalVolume.weight -= Time.deltaTime / m_FadeInOutMultiplier;

            yield return new WaitForEndOfFrame();
        }

        gameStartScreen.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        while (postProcessingLocalVolume.weight < 1)
        {
            postProcessingLocalVolume.weight += Time.deltaTime / m_FadeInOutMultiplier;

            yield return new WaitForEndOfFrame();
        }

        GameObject.Find("/UIRoot(Clone)/Canvas/CenterPoint").GetComponent<Image>().enabled = false;
        gameEndScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

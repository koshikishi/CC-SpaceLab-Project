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
    public GameObject gameMenuScreen;
    public GameObject gameEndScreen;
    public StarterAssetsInputs inputsScript;
    public AudioSource audioSource;
    public AudioClip menuOnAudioFX;
    public AudioClip menuOffAudioFX;

    Camera m_MainCamera;
    float m_FadeInOutMultiplier = 5f;
    bool m_IsMenuOpened = false;

    void Start()
    {
        m_MainCamera = Camera.main;

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
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuToggle()
    {
        if (m_IsMenuOpened)
        {
            LockCursor();
            audioSource.PlayOneShot(menuOffAudioFX);
            gameMenuScreen.SetActive(false);
            m_IsMenuOpened = false;
        }
        else
        {
            UnlockCursor();
            audioSource.PlayOneShot(menuOnAudioFX);
            gameMenuScreen.SetActive(true);
            m_IsMenuOpened = true;
        }
    }

    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inputsScript.cursorInputForLook = true;
        GameObject.Find("/UIRoot(Clone)/Canvas/CenterPoint").GetComponent<Image>().enabled = true;
    }

    void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        inputsScript.cursorInputForLook = false;
        GameObject.Find("/UIRoot(Clone)/Canvas/CenterPoint").GetComponent<Image>().enabled = false;
    }

    IEnumerator FadeIn()
    {
        while (postProcessingLocalVolume.weight > 0)
        {
            postProcessingLocalVolume.weight -= Time.deltaTime / m_FadeInOutMultiplier;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2f);

        GameObject.Find("/UIRoot(Clone)/Canvas/CenterPoint").GetComponent<Image>().enabled = true;
        gameStartScreen.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        while (postProcessingLocalVolume.weight < 1)
        {
            postProcessingLocalVolume.weight += Time.deltaTime / m_FadeInOutMultiplier;

            yield return new WaitForEndOfFrame();
        }

        UnlockCursor();
        gameEndScreen.SetActive(true);
    }
}

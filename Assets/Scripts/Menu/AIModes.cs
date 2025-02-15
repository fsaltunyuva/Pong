using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIModes : MonoBehaviour
{
    public void PlayGameWithEasyAI() {
        SceneManager.LoadSceneAsync("Main Game with Easy AI", LoadSceneMode.Single);
    }

    public void PlayGameWithMediumAI() {
        SceneManager.LoadSceneAsync("Main Game with Medium AI", LoadSceneMode.Single);
    }

    public void PlayGameWithHardAI() {
        SceneManager.LoadSceneAsync("Main Game with Hard AI", LoadSceneMode.Single);
    }

    public void PlayGameWithUltraHardAI() {
        SceneManager.LoadSceneAsync("Main Game with Ultra Hard AI", LoadSceneMode.Single);
    }
}

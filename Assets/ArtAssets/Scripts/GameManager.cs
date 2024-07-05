using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CharacterMovement character;
    public PlatformManager platformManager;

    public Animator winAnimator;
    public CameraController cameraController;
    public bool win, lose;
    public UiManager uiManager;
    private void Awake()
    {
        var buttonClick = uiManager.nextLevelButton.onClick;
        buttonClick.AddListener(platformManager.SetNextLevelPlatforms);
        buttonClick.AddListener(cameraController.SetCameraSettings);
        buttonClick.AddListener(StopDance);
        uiManager.restartButton.onClick.AddListener(RestartScene);
    }

    void Update()
    {
        if (platformManager.platformScale < 0.05f && !lose)
        {
            LoseGame();
        }
        if (!character.isMoving && !win)
        {
            CheckWinCondition();
        }
        uiManager.scoreText.text = platformManager.platformIndex.ToString();
    }

    public void CheckWinCondition()
    {
        if (character.transform.position.z == platformManager.endPlatform.position.z)
        {
            WinGame();
        }
    }
    [ContextMenu("WinGame")]
    private void WinGame()
    {
        win = true;
        winAnimator.SetBool("Win", true);
        cameraController.RotateAroundCharacter(character.transform);
        uiManager.WinScreen();
    }

    private void LoseGame()
    {
        lose = true;
        uiManager.LoseScreen();
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StopDance()
    {
        win = false;
        winAnimator.SetBool("Win", false);
        var charTransform = character.animator.gameObject.transform;
        charTransform.localPosition = Vector3.zero;
        charTransform.localRotation = Quaternion.identity;
    }
}

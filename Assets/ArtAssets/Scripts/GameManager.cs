using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharecterMovement character;
    public PlatformManager platformManager;
    public Transform endPlatform;
    public Animator winAnimator;
    public CameraController cameraController;

    void Update()
    {
        if (platformManager.platformScale < 0.1f)
        {
            LoseGame();
        }
        if (!character.isMoving)
        {
            CheckWinCondition();
        }
    }

    public void CheckWinCondition()
    {
        if (character.transform.position.z == endPlatform.position.z)
        {
            WinGame();
        }
    }
    [ContextMenu("WinGame")]
    private void WinGame()
    {
        winAnimator.SetTrigger("Win");
        cameraController.RotateAroundCharacter(character.transform);
    }

    private void LoseGame()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterMovement character;
    public PlatformManager platformManager;
    public Transform endPlatform;
    public Animator winAnimator;
    public CameraController cameraController;
    public bool win;

    void Update()
    {
        if (platformManager.platformScale < 0.1f)
        {
            LoseGame();
        }
        if (!character.isMoving && !win)
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
        win = true;
        winAnimator.SetTrigger("Win");
        cameraController.RotateAroundCharacter(character.transform);
    }

    private void LoseGame()
    {

    }
}

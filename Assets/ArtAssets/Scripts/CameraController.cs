using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera;

    public void RotateAroundCharacter(Transform character)
    {
        StartCoroutine(RotateCamera(character));
    }

    private IEnumerator RotateCamera(Transform character)
    {
        float rotationSpeed = 50f;
        float totalRotation = 0f;

        while (totalRotation < 360f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            cinemachineCamera.transform.RotateAround(character.position, Vector3.up, rotationStep);
            totalRotation += rotationStep;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject currentPlatform;
    public GameObject lastPlatform;
    public Transform endPlatform;
    public CharacterMovement characterMovement;
    public ObjectPool objectPool;

    // PlatformSettings
    public float speed = 2f;
    public float moveDistance = 3f;
    private bool movingRight = true;
    public List<Material> platformMaterials = new List<Material>();
    public float tolerance;
    public int platformIndex;
    public float platformScale = 3f;

    // Audio Settings
    public int comboCount = 0;
    public AudioSource audioSource;
    public AudioClip comboClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GenerateNextPlatform();
        characterMovement.SetTargetPosition(lastPlatform.transform.position);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsFinalPlatform())
        {
            OnPlatformClick();
        }
        if (currentPlatform != null && platformScale > 0 && !IsFinalPlatform())
        {
            Movement(currentPlatform.transform);
        }
    }

    public void OnPlatformClick()
    {
        if (CheckOverlap() <= tolerance)
        {
            comboCount++;
            PlayNote();
        }
        else
        {
            comboCount = 0;
        }
        if (platformScale > 0)
        {
            GenerateNextPlatform();
        }
    }

    private void PlayNote()
    {
        if (comboClip != null)
        {
            audioSource.clip = comboClip;
            audioSource.pitch = 0.3f + (comboCount * 0.1f);
            audioSource.Play();
        }
        if (comboCount == 10)
        {
            platformScale += 0.35f;
            currentPlatform.transform.localScale = new Vector3(platformScale, 1, 3);
        }
    }

    private void GenerateNextPlatform()
    {
        var platform = objectPool.GetObject();
        platform.transform.position += new Vector3(-3, 0, 3 * platformIndex);
        platform.transform.localScale = new Vector3(platformScale, 1, 3);
        platform.GetComponent<MeshRenderer>().material = platformMaterials[platformIndex % platformMaterials.Count];
        platformIndex++;
        currentPlatform = platform;
        characterMovement.SetTargetPosition(lastPlatform.transform.position);
    }

    public void Movement(Transform currentPlatformTransform)
    {
        if (movingRight)
        {
            currentPlatformTransform.position += Vector3.right * speed * Time.deltaTime;
            if (currentPlatformTransform.position.x >= moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            currentPlatformTransform.position += Vector3.left * speed * Time.deltaTime;
            if (currentPlatformTransform.position.x <= -moveDistance)
            {
                movingRight = true;
            }
        }
    }

    public float CheckOverlap()
    {
        float overlap = currentPlatform.transform.position.x - lastPlatform.transform.position.x;
        float overlapAbs = Mathf.Abs(overlap);
        float currentPlatformWidth = currentPlatform.transform.localScale.x;
        float lastPlatformWidth = lastPlatform.transform.localScale.x;

        float remainingWidth = currentPlatformWidth - overlapAbs;

        if (remainingWidth >= tolerance)
        {
            platformScale = remainingWidth;

            float cutPosition = (currentPlatform.transform.position.x + lastPlatform.transform.position.x) / 2;
            Vector3 newScale = new Vector3(remainingWidth, currentPlatform.transform.localScale.y, currentPlatform.transform.localScale.z);
            Vector3 newPosition = new Vector3(cutPosition, currentPlatform.transform.position.y, currentPlatform.transform.position.z);

            Vector3 fallingPartPosition = currentPlatform.transform.position + new Vector3((overlap > 0 ? 1 : -1) * (remainingWidth / 2 + overlapAbs / 2), 0, 0);
            GameObject fallingPart = Instantiate(currentPlatform, fallingPartPosition, currentPlatform.transform.rotation);
            fallingPart.transform.localScale = new Vector3(overlapAbs, currentPlatform.transform.localScale.y, currentPlatform.transform.localScale.z);
            fallingPart.AddComponent<Rigidbody>();

            currentPlatform.transform.localScale = newScale;
            currentPlatform.transform.position = newPosition;

            lastPlatform = currentPlatform;
        }
        else
        {
            platformScale = 0;
        }

        return overlapAbs;
    }

    private bool IsFinalPlatform()
    {
        return currentPlatform.transform.position.z > endPlatform.position.z;
    }
    public void SetNextLevelPlatforms()
    {
        endPlatform.position += new Vector3(0, 0, 45);
        //platformScale = 3f;
        //currentPlatform.transform.localScale = new Vector3(platformScale, 1, 3);
        speed += 0.2f;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController: MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float ControlSpeed = 40f;
    [Tooltip("In m")] [SerializeField] float xRange = 20f;
    [Tooltip("In m")] [SerializeField] float yRange = 15f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 1.5f;

    [Header("Control-throw Based")]
    [SerializeField] float controlRollFactor = -10f;
    [SerializeField] float controlPitchFactor = -9f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if(isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }

    }





    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;


        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * ControlSpeed * Time.deltaTime;
        float yOffset = yThrow * ControlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
    }
    void OnPlayerDeath() //called by string message!
    {
        isControlEnabled = false;
    }


    private void ProcessFiring()
    {
        if (Input.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
            SetGunsActive(false);
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
   
   

}

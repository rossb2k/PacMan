using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private bool canEatEnemies = false;
    private float powerUpDuration = 15f;
    private float powerUpTimer = 0f;


    public bool CanEatEnemies()
    {
        return canEatEnemies;
    }

    public void EnablePowerUp()
    {
        canEatEnemies = true;
        powerUpTimer = powerUpDuration;


        StartCoroutine(DisablePowerUp());
    }

    private IEnumerator DisablePowerUp()
    {
        yield return new WaitForSeconds(powerUpDuration);

        canEatEnemies = false;


        // Notify the power-up has ended.
        OnPowerUpEnded();
    }

    private void Update1()
    {
        if (canEatEnemies)
        {
            powerUpTimer -= Time.deltaTime;

            // Check if the power-up timer has expired.
            if (powerUpTimer <= 0f)
            {
                canEatEnemies = false;
                OnPowerUpEnded();


            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canEatEnemies && other.CompareTag("Enemy"))
        {
            Debug.Log("Player collided with enemy while power-up is active.");
            Destroy(other.gameObject); // Destroy the enemy
        }
    }
    public void OnPowerUpActivated()
    {
        
        canEatEnemies = true;
    }
    public void OnPowerUpEnded()

    {
        canEatEnemies = false;
    }

}


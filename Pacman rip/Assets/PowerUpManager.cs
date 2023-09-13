using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool canEatEnemies = false;
    private float powerUpDuration = 15f;
    private float powerUpTimer = 0f;

    // Reference to your player controller script
    private PlayerController playerController;

    private void Start()
    {
        // Assuming you have a reference to your PlayerController script here
        playerController = GetComponent<PlayerController>();
    }

    // Handle power-up collection
    public void CollectPowerUp()
    {
        canEatEnemies = true;
        powerUpTimer = powerUpDuration;


        playerController.OnPowerUpActivated();
    }

    // Update is called once per frame
    private void Update()
    {
        if (canEatEnemies)
        {
            powerUpTimer -= Time.deltaTime;

            if (powerUpTimer <= 0f)
            {
                canEatEnemies = false;

             

                playerController.OnPowerUpEnded();
            }
        }
    }
}

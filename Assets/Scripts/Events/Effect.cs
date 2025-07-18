using System.Collections;
using UnityEngine;

public class Effect : Event
{
    public bool playerChange;
    public bool gunChange;
    private float damangeMultiplierIncrease = 2f;
    private float movementMultiplierIncrease = 2f;
    [Tooltip("Player: 0: damage increase, 1: increase movement speed, 2: lose half hp, 3: heal half hp \n Gun: 0: Magazine size increase, 1: Ammo refill, 2: Gun damage increase")]
    public int index;

    private void Start()
    {
        if ((playerChange && gunChange) || (!playerChange && !gunChange))
        {
            Debug.LogError("Effect event cannot change gun and player!");
        }
        if (playerChange)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                Damagee damagee = player.GetComponent<Damagee>();
                switch (index)
                {
                    case 0:
                        FirstPersonPlayer shooter = player.GetComponent<FirstPersonPlayer>();
                        shooter.damageMultiplier = damangeMultiplierIncrease;
                        Debug.Log("damage increase");
                        break;
                    case 1:
                        FirstPersonPlayer playerController = player.GetComponent<FirstPersonPlayer>();
                        playerController.moveMultiplier = movementMultiplierIncrease;
                        Debug.Log("movement increase");
                        break;
                    case 2:
                        damagee.health -= damagee.health / 2;
                        Debug.Log("lose half");
                        break;
                    case 3:
                        damagee.health += damagee.maxHealth / 2;
                        Debug.Log("gain half");
                        break;
                }

            }
            else
            {
                Debug.LogError("Player not found! - Effect event");
            }
        }
        if (gunChange)
        {
            Gun gun = LevelManager.Instance.gun;
            if (gun != null)
            {
                switch (index)
                {
                    case 0:
                        gun.magSize += 4;
                        return;
                    case 1:
                        gun.numBullets = gun.maxBullets;
                        return;
                    case 2:
                        gun.damageMultiplier *= 2;
                        return;
                }
            }

        }
        LevelManager.Instance.OnBoxOpen(eventName);
        Destroy(gameObject);
    }
}

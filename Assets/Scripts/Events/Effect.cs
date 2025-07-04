using System.Collections;
using UnityEngine;

public class Effect : Event
{
    public bool playerChange;
    public bool gunChange;
    private float damangeMultiplierIncrease = 2f;
    private float movementMultiplierIncrease = 2f;

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
                // 0: damage increase
                // 1: increase movement speed
                // 2: lose half hp
                // 3: heal half hp
                switch (Random.Range(0, 4))
                {
                    case 0:
                        Shooter shooter = player.GetComponent<Shooter>();
                        shooter.damageMultiplier = damangeMultiplierIncrease;
                        StartCoroutine(RemoveDamageIncrease(shooter));
                        Debug.Log("damage increase");
                        break;
                    case 1:
                        FirstPersonPlayer playerController = player.GetComponent<FirstPersonPlayer>();
                        playerController.moveMultiplier = movementMultiplierIncrease;
                        StartCoroutine(RemoveMovementIncrease(playerController));
                        Debug.Log("movement increase");
                        break;
                    case 2:
                        damagee.health -= damagee.maxHealth / 2;
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
    }

    IEnumerator RemoveDamageIncrease(Shooter shooter)
    {
        yield return new WaitForSeconds(30);
        shooter.damageMultiplier = 1;
    }

    IEnumerator RemoveMovementIncrease(FirstPersonPlayer playerController)
    {
        yield return new WaitForSeconds(30);
        playerController.moveMultiplier = 1;
    }
}

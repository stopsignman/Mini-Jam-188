using System.Collections;
using UnityEngine;

public class Effect : Event
{
    public bool playerChange;
    public bool gunChange;
    private float damangeMultiplierIncrease = 2f;
    private float movementMultiplierIncrease = 2f;
    [Tooltip("Player: 0: damage increase, 1: increase movement speed, 2: lose half hp, 3: heal half hp \n Gun: 0: Magazine size increase, 1: Ammo refill")]
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

    IEnumerator RemoveDamageIncrease(FirstPersonPlayer shooter)
    {
        yield return new WaitForSeconds(60);
        shooter.damageMultiplier = 1;
    }

    IEnumerator RemoveMovementIncrease(FirstPersonPlayer playerController)
    {
        yield return new WaitForSeconds(60);
        playerController.moveMultiplier = 1;
    }
}

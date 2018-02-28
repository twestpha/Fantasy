using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleComponent : MonoBehaviour {

    // just duplicate mouseguard for now
    public enum Move : uint {
        None,
        Attack,  // Uses strength, add weapon attack bonus. Finesse weapons use agility
        Recover, // Uses constitution, add armor bonus. Shields use strength
        Dodge,   // Uses agility, subtract armor bonus, add Finesse weapons
        Magic,   // Uses magic, add weapon magic bonus
    }

    public GameObject[] players;
    public GameObject[] enemies;

    public GameObject selector;

    private bool started;
    private int turnCount;

	void Start(){
        started = false;
	}

	void Update(){
        if(!started){
            return;
        }

        GameObject currentPlayer = players[turnCount];
        BattleCharacterComponent playerCharComponent = currentPlayer.GetComponent<BattleCharacterComponent>();

        if(playerCharComponent.currentHealth == 0){
            int repeats = 0;
            for(; repeats < players.Length; ++repeats){
                turnCount++;
                turnCount = turnCount % players.Length;

                currentPlayer = players[turnCount];
                playerCharComponent = currentPlayer.GetComponent<BattleCharacterComponent>();

                if(playerCharComponent.currentHealth > 0){
                    break;
                }
            }
            if(repeats == players.Length){
                started = false;
                Debug.Log("LOST COMBAT");
                return;
            }
        }

        selector.transform.position = new Vector3(currentPlayer.transform.position.x, 2.2f, currentPlayer.transform.position.z);

        // Temporary input
        Move playerMove = Move.None;

        if(Input.GetKeyDown(KeyCode.JoystickButton0 /* A button */)){
            playerMove = Move.Attack;
        } else if(Input.GetKeyDown(KeyCode.JoystickButton1 /* B button */)){
            playerMove = Move.Recover;
        } else if(Input.GetKeyDown(KeyCode.JoystickButton2 /* X button */)){
            playerMove = Move.Dodge;
        } else if(Input.GetKeyDown(KeyCode.JoystickButton3 /* Y button */)){
            playerMove = Move.Magic;
        }

        if(playerMove != Move.None){
            turnCount++;
            turnCount = turnCount % players.Length;

            // Player Turn

            CharacterData currentCharacterData = playerCharComponent.charData;
            int playerAttackValue  = 0;
            int playerRecoverValue = 0;
            int playerDodgeValue   = 0;
            int playerMagicValue   = 0;

            switch(playerMove){
            case Move.Attack:
                playerAttackValue = (int)((Random.value + 0.5f) * currentCharacterData.strength);
            break;
            case Move.Recover:
                playerRecoverValue = (int)((Random.value + 0.5f) * currentCharacterData.constitution);
            break;
            case Move.Dodge:
                playerDodgeValue = (int)((Random.value + 0.5f) * currentCharacterData.agility);
            break;
            case Move.Magic:
                playerMagicValue = (int)((Random.value + 0.5f) * currentCharacterData.magic);
            break;
            }

            // Enemy Turn
            // real dumb AI for now
            Move enemyMove = (Move)((Random.value * 4.0f) + 1.0f);

            BattleCharacterComponent enemyCharComponent = enemies[0].GetComponent<BattleCharacterComponent>();
            CharacterData enemCharacterData = enemyCharComponent.charData;
            int enemyAttackValue  = 0;
            int enemyRecoverValue = 0;
            int enemyDodgeValue   = 0;
            int enemyMagicValue   = 0;

            switch(enemyMove){
            case Move.Attack:
                enemyAttackValue = (int)((Random.value + 0.5f) * enemCharacterData.strength);
            break;
            case Move.Recover:
                enemyRecoverValue = (int)((Random.value + 0.5f) * enemCharacterData.constitution);
            break;
            case Move.Dodge:
                enemyDodgeValue = (int)((Random.value + 0.5f) * enemCharacterData.agility);
            break;
            case Move.Magic:
                enemyMagicValue = (int)((Random.value + 0.5f) * enemCharacterData.magic);
            break;
            }

            Debug.Log("MOVE #######################################################");

            // Move resolution
            switch(playerMove){
            case Move.Attack:
                switch(enemyMove){
                case Move.Attack:
                    // Attack versus Attack: each party deals damage to the other
                    Debug.Log("Player Attacks, Enemy Attack");
                    playerCharComponent.Damage(enemyAttackValue);
                    enemyCharComponent.Damage(playerAttackValue);
                break;
                case Move.Recover:
                    // Attack versus Recover: The winner of the contest deals damage or heals respectively, equal to the difference in scores. if they are equal, nothing happens.
                    Debug.Log("Player Attacks, Enemy Recovers");
                    if(playerAttackValue > enemyRecoverValue){
                        enemyCharComponent.Damage(playerAttackValue - enemyRecoverValue);
                    } else if(playerAttackValue < enemyRecoverValue){
                        enemyCharComponent.Heal(enemyRecoverValue - playerAttackValue);
                    }
                break;
                case Move.Dodge:
                    // Attack versus Dodge: The dodger automatically scores a hit
                    Debug.Log("Player Attacks, Enemy Dodges");
                    playerCharComponent.Damage(enemyDodgeValue);
                break;
                case Move.Magic:
                    // Attack versus Magic: The attack hits automatically
                    Debug.Log("Player Attacks, Enemy Magics");
                    enemyCharComponent.Damage(playerAttackValue);
                break;
                }
            break;
            case Move.Recover:
                switch(enemyMove){
                case Move.Attack:
                    // Attack versus Recover: The winner of the contest deals damage or heals respectively, equal to the difference in scores. if they are equal, nothing happens.
                    Debug.Log("Player Recovers, Enemy Attacks");
                    if(playerRecoverValue > enemyAttackValue){
                        playerCharComponent.Heal(playerRecoverValue - enemyAttackValue);
                    } else if(playerRecoverValue < enemyAttackValue){
                        playerCharComponent.Damage(enemyAttackValue - playerRecoverValue);
                    }
                break;
                case Move.Recover:
                    // Recover versus Recover: Both parties heal
                    Debug.Log("Player Recovers, Enemy Recovers");
                    playerCharComponent.Heal(playerRecoverValue);
                    enemyCharComponent.Heal(enemyRecoverValue);
                break;
                case Move.Dodge:
                    // Recover versus Dodge: Recover wins automatically, dodge does nothing.
                    Debug.Log("Player Recovers, Enemy Dodges");
                    playerCharComponent.Heal(playerRecoverValue);
                break;
                case Move.Magic:
                    // Recover versus Magic: Magic hits automatically
                    Debug.Log("Player Recovers, Enemy Magics");
                    playerCharComponent.Damage(enemyMagicValue);
                break;
                }
            break;
            case Move.Dodge:
                switch(enemyMove){
                case Move.Attack:
                    // Dodge versus Attack: The dodger automatically scores a hit
                    Debug.Log("Player Dodges, Enemy Attacks");
                    enemyCharComponent.Damage(playerDodgeValue);
                break;
                case Move.Recover:
                    // Dodge versus Recover: Recover wins automatically, dodge does nothing.
                    Debug.Log("Player Dodges, Enemy Recovers");
                    enemyCharComponent.Heal(enemyRecoverValue);
                break;
                case Move.Dodge:
                    // Dodge versus Dodge: Nothing Happens
                    Debug.Log("Player Dodges, Enemy Dodges");
                break;
                case Move.Magic:
                    // Dodge versus Magic: Dodge hits
                    Debug.Log("Player Dodges, Enemy Magics");
                    enemyCharComponent.Damage(playerDodgeValue);
                break;
                }
            break;
            case Move.Magic:
                switch(enemyMove){
                case Move.Attack:
                    // Magic versus Attack: The attack hits automatically
                    Debug.Log("Player Magics, Enemy Attacks");
                    playerCharComponent.Damage(enemyAttackValue);
                break;
                case Move.Recover:
                    // Magic versus Recover: Magic hits
                    Debug.Log("Player Magics, Enemy Recovers");
                    enemyCharComponent.Damage(playerMagicValue);
                break;
                case Move.Dodge:
                    // Magic versus Dodge: deals dodge damage
                    Debug.Log("Player Magics, Enemy Dodges");
                    playerCharComponent.Damage(enemyDodgeValue);
                break;
                case Move.Magic:
                    // Magic versus Magic: the party with the highest value deals all value damage
                    Debug.Log("Player Magics, Enemy Magics");
                    if(playerMagicValue > enemyMagicValue){
                        enemyCharComponent.Damage(playerMagicValue);
                    } else if(playerMagicValue < enemyMagicValue){
                        playerCharComponent.Damage(enemyMagicValue);
                    }
                break;
                }
            break;
            }

        }
	}

    public void BeginBattle(){
        turnCount = 0;
        started = true;
    }
}

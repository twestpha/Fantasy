using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterComponent : MonoBehaviour {

    public CharacterData charData;

    public int currentHealth;

    void Start(){
        currentHealth = charData.maxHealth;
    }

    void Update(){

    }

    public void Damage(int amount){
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, charData.maxHealth);

        Debug.Log("Player " + gameObject + " takes " + amount + " damage. Health: " + currentHealth);

        if(currentHealth == 0){
            transform.rotation = Quaternion.Euler(-180.0f, 90.0f, 0.0f);
        }
    }

    public void Heal(int amount){
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, charData.maxHealth);

        Debug.Log("Player " + gameObject + " heals " + amount + " health. Health: " + currentHealth);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
[System.Serializable]
public class CharacterData : ScriptableObject {
    public int maxHealth;
    public int currentHealth;

    public int strength;
    public int constitution;
    public int agility;
    public int magic;
}

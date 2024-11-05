using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// CharacterEvents holds static UnityActions for character damage and healing events, allowing other scripts
/// to listen and respond when a character takes damage or receives healing.
/// </summary>
public class CharacterEvents
{
    // Event triggered when a character is damaged, passing the character's GameObject and the damage amount
    public static UnityAction<GameObject, int> characterDamaged;

    // Event triggered when a character is healed, passing the character's GameObject and the health restored
    public static UnityAction<GameObject, int> characterHealed;
}

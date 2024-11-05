using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// AnimationStrings provides a centralized collection of string constants for Animator parameter names.
/// This reduces the risk of errors from hardcoded strings and makes parameter management easier across scripts.
/// </summary>
internal class AnimationStrings
{
    internal static string isMoving = "isMoving";                 // Indicates if the character is moving
    internal static string isRunning = "isRunning";               // Indicates if the character is running
    internal static string isGrounded = "isGrounded";             // Indicates if the character is grounded
    internal static string yVelocity = "yVelocity";               // Tracks the character's vertical velocity
    internal static string jump = "jump";                         // Triggers the jump animation
    internal static string isOnWall = "isOnWall";                 // Indicates if the character is on a wall
    internal static string isOnCeiling = "isOnCeiling";           // Indicates if the character is on the ceiling
    internal static string attack = "attack";                     // Triggers an attack animation
    internal static string canMove = "canMove";                   // Indicates if the character can move
    internal static string hasTarget = "hasTarget";               // Indicates if the character has a target in range
    internal static string isAlive = "isAlive";                   // Indicates if the character is alive
    internal static string isHit = "isHit";                       // Indicates if the character is hit
    internal static string hitTrigger = "hit";                    // Triggers a hit reaction animation
    internal static string lockVelocity = "lockVelocity";         // Indicates if the character's velocity is locked
    internal static string attackCooldown = "attackCooldown";     // Sets the cooldown period for attack animations
    internal static string rangedAttack = "rangedAttack";         // Triggers a ranged attack animation
}

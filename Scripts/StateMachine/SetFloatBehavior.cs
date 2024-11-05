using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SetFloatBehavior sets a specified float parameter in the Animator at various points
/// (on state enter, on state exit, on state machine enter, and on state machine exit),
/// allowing for flexible control over float-based animations and transitions.
/// </summary>
public class SetFloatBehavior : StateMachineBehaviour
{
    // Name of the float parameter to be set in the Animator
    public string floatName;

    // Flags to control when to update the float value
    public bool updateOnStateEnter, updateOnStateExit;
    public bool updateOnStateMachineEnter, updateOnStateMachineExit;

    // Values to set the float parameter to on enter and exit
    public float valueOnEnter, valueOnExit;

    // Called when a transition starts and the animator begins evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateEnter)
        {
            animator.SetFloat(floatName, valueOnEnter); // Set the float to the specified value on entering the state
        }
    }

    // Called when the animator stops evaluating this state due to a transition ending
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateExit)
        {
            animator.SetFloat(floatName, valueOnExit); // Set the float to the specified value on exiting the state
        }
    }

    // Called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineEnter)
        {
            animator.SetFloat(floatName, valueOnEnter); // Set the float to the specified value on entering the state machine
        }
    }

    // Called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachineExit)
        {
            animator.SetFloat(floatName, valueOnExit); // Set the float to the specified value on exiting the state machine
        }
    }
}

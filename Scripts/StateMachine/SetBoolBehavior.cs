using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SetBoolBehavior is used to set a specific boolean parameter in the Animator
/// on entering, updating, or exiting states within a state machine, allowing
/// for flexible control over animation transitions.
/// </summary>
public class SetBoolBehavior : StateMachineBehaviour
{
    // Name of the boolean parameter to be set in the Animator
    public string boolName;

    // Flags to control when to update the boolean value
    public bool updateOnState;          // Update on state enter/exit
    public bool updateOnStateMachine;    // Update on state machine enter/exit

    // Values to set the boolean parameter on enter and exit
    public bool valueOnEnter, valueOnExit;

    // Called when a transition starts and the animator begins evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnEnter); // Set the boolean to the specified value on enter
        }
    }

    // Called when the animator stops evaluating this state due to a transition ending
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(boolName, valueOnExit); // Set the boolean to the specified value on exit
        }
    }

    // Called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnEnter); // Set the boolean to the specified value on entering the state machine
        }
    }

    // Called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnExit); // Set the boolean to the specified value on exiting the state machine
        }
    }
}

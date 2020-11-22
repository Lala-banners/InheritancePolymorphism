using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : Life
{
    

    #region Prey AI Requirements
    /*
     * Flock, make flock point as goal, make function for completion of prey going to goal (print message)
     * Wander, need prey waypoints for wander until/if predator wanders into prey wander range
     * Evade, go out of predator seek range
     * Hide, hide behind obstacle wall
    */
    #endregion

    #region Variables
    public GameObject[] PreyWaypoints;
    public PreyState state;
    public bool ChangeState = false;
    public float preySpeed = 5f;
    public int index = 0;
    public float minDist = 0.5f;
    #endregion

    public void MovePrey(Vector2 targetPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, preySpeed * Time.deltaTime);
    }

    public void PreyWander()
    {
        float distance = Vector2.Distance(transform.position, PreyWaypoints[index].transform.position);

        if (distance < minDist)
        {
            index++;
        }
        if (index >= PreyWaypoints.Length)
        {
            index = 0;
        }
        MovePrey(PreyWaypoints[index].transform.position); //go to waypoints 1-3
    }

    #region States
    public enum PreyState
    {
        Flock,
        Wander,
        Evade,
        Hide
    }

    IEnumerator FlockState() 
    {
        print("Flock: Enter");
        while (state == PreyState.Flock)
        {
            
            
            yield return 0;
        }
        print("Flock: Exit");
        NextState();
    }

    IEnumerator WanderState() // wander around the waypoints
    {
        print("Wander: Enter");
        while (state == PreyState.Wander)
        {
            PreyWander();
            if(ChangeState == false)
            {
                state = PreyState.Wander;
            }
            MovePrey(PreyWaypoints[index].transform.position);
            yield return 0;
        }
        print("Wander: Exit");
        NextState();
    }

    IEnumerator EvadeState() 
    {
        print("Evade: Enter");
        while (state == PreyState.Evade)
        {
            
            
            yield return 0;
        }
        print("Evade: Exit");
        NextState();
    }

    IEnumerator HideState() 
    {
        print("Evade: Enter");
        while (state == PreyState.Hide)
        {
            
            yield return 0;
        }
        print("Hide: Exit");
        //NextState();
    }
    #endregion

    #region Functions
    private void Start()
    {
        NextState();
    }

    void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                   System.Reflection.BindingFlags.NonPublic |
                                   System.Reflection.BindingFlags.Instance);

        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
    #endregion
}


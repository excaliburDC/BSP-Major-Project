using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using UnityEngine.Video;

public class Enemy : NPC
{
    [SerializeField] private float nextWaypointDist = 1f;

    private Transform target;
    private Path enemyPath;
    private Seeker targetSeeker;

    private int currentWayPoint = 0;

    private bool reachedPathEnd = false;

    public Transform Target 
    { 
        get => target; 
        set => target = value; 
    }

    protected override void Start()
    {
        base.Start();
        Debug.Log(myRB.gameObject.name);

        targetSeeker = GetComponent<Seeker>();

        
        InvokeRepeating("UpdatePath", 0f, 0.5f);
       

        
        
    }

    private void UpdatePath()
    {
        if(targetSeeker.IsDone())
        {
            targetSeeker.StartPath(myRB.position, Target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            enemyPath = p;
            currentWayPoint = 0;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Follow();
    }

    private void Follow()
    {
        if (enemyPath == null)
            return;

        if(currentWayPoint >= enemyPath.vectorPath.Count)
        {
            reachedPathEnd = true;
            return;
        }

        else
        {
            reachedPathEnd = false;
        }

        CalculatePathDirection();
    }

    private void CalculatePathDirection()
    {
        Vector2 direction = ((Vector2)enemyPath.vectorPath[currentWayPoint] - myRB.position).normalized;

        Vector2 force =  direction * speed * Time.deltaTime;

        myRB.AddForce(force,ForceMode2D.Impulse);

        float distance = Vector2.Distance(myRB.position, enemyPath.vectorPath[currentWayPoint]);

        if (distance < nextWaypointDist)
        {
            currentWayPoint++;
        }
    }
}

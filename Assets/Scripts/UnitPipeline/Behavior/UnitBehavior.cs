using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class UnitBehavior : MonoBehaviour
{
    Vector3 goal;
    private Animator anim;
    public NavMeshAgent nav;

    NavNode[] nodePath;
    int currentNode = 0;
    float epsilon = 1f;

    Healthbar healthbar;

    float maxHealth = 100;
    float health = 100;
    public float moveSpeed = 1.0f;

    public static List<UnitBehavior> allEnemies = new List<UnitBehavior>();

    private void OnEnable()
    {
        allEnemies.Add(this);
    }

    private void OnDisable()
    {
        allEnemies.Remove(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        // setting below as placeholder until it's clear if unitData or the variables inside this class
        // will be used for things like the units health

        anim = GetComponentInChildren<Animator>();

        nodePath = FindObjectOfType<NavHolder>().navNodes;
        nav = GetComponent<NavMeshAgent>();

        goal = nodePath[currentNode].transform.position;
        nav.destination = goal;
        healthbar = GetComponentInChildren<Healthbar>();
        nav.speed = moveSpeed;

    }

    // Update is called once per frame
    public void Update()
    {
        if (Vector3.Distance(transform.position, goal) > epsilon)
        {
        }
        else
        {
            currentNode++;
            GetNewGoal();
        }
    }

    void GetNewGoal()
    {
        if (currentNode < nodePath.Length)
        {
            goal = nodePath[currentNode].transform.position;
            nav.destination = goal;
        }
    }


    public float GetDistanceFromEnd()
    {
        return Vector3.Distance(transform.position, goal);
    }

    public void TakeDamage(float delta)
    {
        health -= delta;
        healthbar.value = health / maxHealth;
        isDead();
      
    }
    public void GainHealth(int delta)
    {
        health = Mathf.Max(health + delta, maxHealth);
        float healthpercent = ((float)health / maxHealth);
        healthbar.value = healthpercent;
    }

    public void Stunned(float duration)
    {
        anim.SetBool("isStunned", true);
        Invoke("ResetStun", duration);
    }

    void ResetStun()
    {
        anim.SetBool("isStunned", false);
    }

    bool isDead()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            //nav.speed = 0;
            //anim.SetBool("isDead", true);
            return true;
        }
        return false;
    }
}

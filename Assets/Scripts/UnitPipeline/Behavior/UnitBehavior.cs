using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class UnitBehavior : MonoBehaviour
{
    public Vector3 goal;
    private SpawningManager.UnitData unitData;
    private Animator anim;
    public NavMeshAgent nav;

    NavNode[] nodePath;
    int currentNode = 0;
    float epsilon = 1f;

    Slider healthbar;


    public float health;
    public float moveSpeed;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        // setting below as placeholder until it's clear if unitData or the variables inside this class
        // will be used for things like the units health
        unitData.unitHealth = 100;

        anim = GetComponentInChildren<Animator>();

        nodePath = FindObjectOfType<NavHolder>().navNodes;
        nav = GetComponent<NavMeshAgent>();

        goal = nodePath[currentNode].transform.position;
        nav.destination = goal;
        healthbar = GetComponentInChildren<Slider>();
        nav.speed = moveSpeed;

    }

    // Update is called once per frame
    public void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            ResourceManager.instance.UpdateResources(unitData.goldValue, 0);
            Destroy(gameObject);
        }
        if (Vector3.Distance(transform.position, goal) > epsilon)
        {
        }
        else
        {
            currentNode++;
            GetNewGoal();
        }
        healthbar.transform.LookAt(healthbar.transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
    }

    void GetNewGoal()
    {
        if (currentNode < nodePath.Length)
        {
            goal = nodePath[currentNode].transform.position;
            nav.destination = goal;
        }
    }


    public void LoadData(SpawningManager.UnitData data) 
    {
        unitData = data;
        health = data.unitHealth;
        nav = GetComponent<NavMeshAgent>();
        moveSpeed = data.unitSpeed;
        nav.speed = moveSpeed;
        damage = data.damage;
    }

    void SetupNav()
    {

    }

    public float GetDistanceFromEnd()
    {
        return Vector3.Distance(transform.position, goal);
    }

    public void TakeDamage(float delta)
    {
        health -= delta;
        healthbar.value = health / unitData.unitHealth;
        //Debug.Log("unithealth value: " + unitData.unitHealth);
        isDead();
      
    }
    public void GainHealth(int delta)
    {
        health = Mathf.Max(health + delta, unitData.unitHealth);
        float healthpercent = ((float)health / (float)unitData.unitHealth);
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
            nav.speed = 0;
            anim.SetBool("isDead", true);
            return true;
        }
        return false;
    }
}

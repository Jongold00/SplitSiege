using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class UnitBehavior : MonoBehaviour
{
    public UnitDataSO data;

    private Animator anim;

    UnitNavigation nav;


    Healthbar healthbar;

    float maxHealth;
    float health;

    public static List<UnitBehavior> allEnemies = new List<UnitBehavior>();
    
    // baseOfUnit is intended to be used for things like AoE which should appear to target
    // the ground under a unit rather than the unit itself
    [SerializeField] Transform baseOfUnit;
    public Transform BaseOfUnit { get => baseOfUnit; set => baseOfUnit = value; }

    private void OnEnable()
    {
        allEnemies.Add(this);
        nav = GetComponent<UnitNavigation>();
    }

    private void OnDisable()
    {
        allEnemies.Remove(this);
    }

    [SerializeField]
    List<StatusEffect> activeEffects = new List<StatusEffect>();


    // Start is called before the first frame update
    void Start()
    {
        // setting below as placeholder until it's clear if unitData or the variables inside this class
        // will be used for things like the units health

        anim = GetComponentInChildren<Animator>();


        maxHealth = data.unitHealth;
        health = maxHealth;

        healthbar = GetComponentInChildren<Healthbar>();


    }

    // Update is called once per frame
    public void Update()
    {
        TickStatusEffects(Time.deltaTime);
        anim.SetFloat("MoveSpeed", nav.GetSpeed());
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
            anim.SetTrigger("Death");
            nav.SetSpeed(0);
            return true;
        }
        return false;
    }



    #region StatusEffectMethods

    public void AttachStatusEffect(StatusEffect effect)
    {
        foreach (StatusEffect curr in activeEffects)
        {
            if (curr.compareID(effect.id))
            {

                switch (curr.stackType)
                {
                    case StatusEffect.StackType.Refreshing:
                        curr.Refresh();
                        return;
                    case StatusEffect.StackType.Additive:
                        activeEffects.Add(effect);
                        effect.OnApply(this);
                        break;
                    case StatusEffect.StackType.Multiplicative:
                        activeEffects.Add(effect);
                        effect.OnApply(this);
                        break;

                }
                return;
            }

        }
        activeEffects.Add(effect);
        effect.OnApply(this);

    }

    public void StatusEffectExpired(StatusEffect effect)
    {
        activeEffects.Remove(effect);
    }

    void TickStatusEffects(float deltaT)
    {
        if (activeEffects == null)
        {
            return;
        }

        for (int i = 0; i < activeEffects.Count; i++)
        {
            activeEffects[i].Tick(this, deltaT);
        }
    }

    public float GetTotalSpeedMultiplier()
    {
        float total = 1;
        foreach (StatusEffect curr in activeEffects)
        {
            if (curr is Slow)
            {
                Slow asSlow = (Slow)curr;
                total *= 1 - asSlow.GetSlowMultiplier();
            }
        }
        return total;
    }

    #endregion
}

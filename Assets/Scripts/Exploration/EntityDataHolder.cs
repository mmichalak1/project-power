using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces;
using System;

public class EntityDataHolder : MonoBehaviour, IProvideStatistics
{
    public const int MAXDEFENCE = 70;


    [SerializeField]
    private EntityData _sheepData;
    public void LoadSheepData(EntityData data)
    {
        SheepData = data;
        gameObject.name = data.Name;
        HealthController comp = gameObject.GetComponent<HealthController>();
        data.ResetStats();
        ApplyItemsChange(data);
        comp.MaxHealth = data.TotalHealth;
        comp.HealToFull();

        foreach (var x in SheepData.SheepSkills.Skills)
            if (x != null)
                x.Initialize(gameObject);
    }
    public static void ApplyItemsChange(EntityData SheepData)
    {
        if (SheepData.DefensiveItem != null)
            SheepData.DefensiveItem.ApplyItemChanges(SheepData);
        if (SheepData.OffensiveItem != null)
            SheepData.OffensiveItem.ApplyItemChanges(SheepData);
    }

    public static void RevertItemsChange(EntityData SheepData)
    {
        if (SheepData.DefensiveItem != null)
            SheepData.DefensiveItem.ReverseItemChanges(SheepData);
        if (SheepData.OffensiveItem != null)
            SheepData.OffensiveItem.ReverseItemChanges(SheepData);
    }

    /// <summary>
    /// Get total damage with buffs/debuffs 
    /// </summary>
    /// <returns>Damage</returns>
    public int GetDamage()
    {
        var status = gameObject.GetComponent<EntityStatus>();

        int buffValue = 0;
        int debuffValue = 0;

        if (status.IsDamageBuffed)
        {
            //TODO: Calculate DMG buff value
        }

        if (status.IsDamageDebuffed)
        {
            //TODO: Calculate DMG debuff value
        }

        return _sheepData.TotalAttack + buffValue - debuffValue;
    }

    /// <summary>
    /// Returns defence value with buffs/debuffs
    /// </summary>
    /// <returns>Defence</returns>
    public int GetDefence()
    {
        var status = gameObject.GetComponent<EntityStatus>();

        int buffValue = 0;
        int debuffValue = 0;
        int basicAttack = _sheepData.TotalAttack;

        if (status.IsDefenceBuffed)
        {
            //TODO: Calculate DMG buff value
        }

        if (status.IsDamageDebuffed)
        {
            var debuff = gameObject.GetComponent<DamageDebuff>();
            if (debuff == null)
            {
                Debug.LogError("Damage debuff not found");
            }
            else
            {
                debuffValue = (basicAttack * debuff.DebuffValue) / 100;
            }

        }

        int result = basicAttack + buffValue - debuffValue;
        if (result < 0)
            return 0;
        if (result > MAXDEFENCE)
            return MAXDEFENCE;

        return result;
    }

    public int GetMaxHealth()
    {
        return _sheepData.TotalHealth;
    }

    public float GetDamageMultiplicator()
    {
        throw new NotImplementedException();
    }

    public EntityData SheepData
    {
        get { return _sheepData; }
        private set { _sheepData = value; }
    }
}

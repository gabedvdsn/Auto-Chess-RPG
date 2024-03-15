using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoChessRPG.Entity;
using AYellowpaper.SerializedCollections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability/Base")]
    public class BaseAbilityData : EntityBaseData
    {
        [Header("Base Ability Information")] 
        [SerializeField] protected Affiliation canTarget;
        [SerializeField] protected AbilityActivation activation;
        [SerializeField] protected BasePowerPacket powerPacket;
        
        [Header("Specific Ability Information")]
        [SerializeField] protected SerializedDictionary<Affiliation, BaseEffectData[]> effectsOfAbility;
        [SerializeField] protected float abilityRange;
        
        [Header("Base Level Up Information")]
        [SerializeField] protected float levelUpAbilityRange;
        
        [Header("Other")]
        [SerializeField] protected bool hideInUI;
        

        // Base Ability Information Getters
        public Affiliation GetTargetableAffiliation() => canTarget;

        public AbilityActivation GetAbilityActivation() => activation;

        public BasePowerPacket GetPowerPacket() => powerPacket;

        public BaseEffectData[] GetAllEffects()
        {
            var effects = new List<BaseEffectData>();
            foreach (var affiliation in effectsOfAbility.Keys)
            {
                effects.AddRange(effectsOfAbility[affiliation]);
            }

            return effects.ToArray();
        }

        public RealEffectData[] AllToRealEffects(int level = 0)
        {
            var realEffects = new List<RealEffectData>();
            foreach (BaseEffectData effect in GetAllEffects())
            {
                realEffects.Add(effect.ToRealEffect(level));
            }

            return realEffects.ToArray();
        }
        
        public Dictionary<Affiliation, BaseEffectData[]> GetEffectsByAffiliation() => effectsOfAbility;

        public Dictionary<Affiliation, RealEffectData[]> EffectsToRealByAffiliation(bool separate = true)
        {
            
            Dictionary<Affiliation, RealEffectData[]> realEffects = new Dictionary<Affiliation, RealEffectData[]>();

            foreach (Affiliation aff in effectsOfAbility.Keys)
            {
                realEffects[aff] = new RealEffectData[effectsOfAbility[aff].Length];
                for (int i = 0; i < effectsOfAbility[aff].Length; i++)
                {
                    realEffects[aff][i] = effectsOfAbility[aff][i].ToRealEffect();
                }
            }

            return realEffects;
        }

        public float GetRange() => abilityRange;

        public bool GetHideInUI() => hideInUI;
        
        // Level Up Information Getters
        public float GetLevelUpRange() => levelUpAbilityRange;

        public RealAbilityData ToRealAbilityData(int level = 0, RealItemData _attachedItem = null)
        {
            RealAbilityData real = new RealAbilityData(this, _attachedItem);

            real.OnLevelsUp(level);

            return real;
        }
    }

    
}

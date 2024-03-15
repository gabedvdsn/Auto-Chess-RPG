using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using Unity.PlasticSCM.Editor.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Entity/Item/Item Base", fileName = "Item")]
    public class BaseItemData : EntityBaseData
    {
        // This data includes items under the following categories: resource, spirit, construction, knowledge
        [Header("Base Item Data Information")] 
        [SerializeField] private ItemType itemType;
        [SerializeField] private BasePowerPacket power;
        [SerializeField] private BaseAttributePacket attachedAttributes;
        [SerializeField] private StatPacket attachedStats;
        [SerializeField] private BaseAbilityData[] attachedAbilities;
        
        [Header("Level Up Information")]
        [SerializeField] private BaseAttributePacket onLevelBaseAttributes;
        [SerializeField] private StatPacket onLevelStats;


        // Base Item Data Information Getters
        public ItemType GetItemType() => itemType;

        public BasePowerPacket GetPowerPacket() => power;
        
        public BaseAttributePacket GetAttachedAttributes() => attachedAttributes;

        public StatPacket GetAttachedStats() => attachedStats;

        public BaseAbilityData[] GetAttachedAbilities() => attachedAbilities;
        
        // Level Up Information Getters
        public BaseAttributePacket GetLevelUpAttachedAttributes() => onLevelBaseAttributes;
        public StatPacket GetLevelUpAttachedStats() => onLevelStats;
    }

    public class RealItemData
    {
        private BaseItemData baseData;

        private RealPowerPacket power;

        private RealAbilityData[] attachedAbilities;

        private RealAttributePacket attachedBaseAttributes;
        private StatPacket attachedStats;

        private float cooldown = -1f;

        public RealItemData(BaseItemData _baseData, RealPowerPacket _power, RealAbilityData[] _attachedAbilities, RealAttributePacket _attachedBaseAttributes, StatPacket _attachedStats)
        {
            baseData = _baseData;
            power = _power;

            attachedAbilities = _attachedAbilities;
            
            attachedBaseAttributes = _attachedBaseAttributes;
            attachedStats = _attachedStats;

            foreach (RealAbilityData ability in attachedAbilities)
            {
                if (ability.GetCooldown() > cooldown) cooldown = ability.GetCooldown();
            }
        }

        public void SendCooldown(float _cooldown) => cooldown = _cooldown;

        public virtual bool LevelUp()
        {
            if (!power.LevelUp()) return false;

            // Level up attached attributes
            attachedBaseAttributes.MergeOtherAttributePacket(baseData.GetLevelUpAttachedAttributes());
            
            // Level up attached stats
            attachedStats.MergeOtherStatPacket(baseData.GetLevelUpAttachedStats());
            
            // Level up attached abilities
            foreach (RealAbilityData ability in attachedAbilities) ability.LevelUp();

            return true;
        }

        public BaseItemData GetBaseData() => baseData;

        public RealAbilityData[] GetAttachedAbilities() => attachedAbilities;

        public RealAttributePacket GetAttachedAttributes() => attachedBaseAttributes;

        public StatPacket GetAttachedStats() => attachedStats;

        public RealPowerPacket GetPowerPacket() => power;

        public BasePowerPacket GetBasePowerPacket() => baseData.GetPowerPacket();

        public float GetCooldown() => cooldown;
    }
}
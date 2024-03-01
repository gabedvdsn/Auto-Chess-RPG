using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
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
        [SerializeField] private AttributePacket attachedAttributes;
        [SerializeField] private StatPacket attachedStats;
        [SerializeField] private BaseAbilityData[] attachedAbilities;
        [SerializeField] private BasePowerPacket power;
        
        [Header("Level Up Information")]
        [SerializeField] private AttributePacket onLevelAttributes;
        [SerializeField] private StatPacket onLevelStats;


        // Base Item Data Information Getters
        public ItemType GetItemType() => itemType;

        public BasePowerPacket GetPowerPacket() => power;
        
        public AttributePacket GetAttachedAttributes() => attachedAttributes;

        public StatPacket GetAttachedStats() => attachedStats;

        public BaseAbilityData[] GetAttachedAbilities() => attachedAbilities;
        
        // Level Up Information Getters
        public AttributePacket GetLevelUpAttachedAttributes() => onLevelAttributes;
        public StatPacket GetLevelUpAttachedStats() => onLevelStats;
    }

    public class RealItemData
    {
        private BaseItemData baseData;

        private RealPowerPacket power;

        private RealAbilityData[] attachedAbilities;

        private AttributePacket attachedAttributes;
        private StatPacket attachedStats;

        public RealItemData(BaseItemData _baseData, RealPowerPacket _power, RealAbilityData[] _attachedAbilities, AttributePacket _attachedAttributes, StatPacket _attachedStats)
        {
            baseData = _baseData;
            power = _power;

            attachedAbilities = _attachedAbilities;
            
            attachedAttributes = _attachedAttributes;
            attachedStats = _attachedStats;
        }

        public virtual bool LevelUp()
        {
            if (!power.LevelUp()) return false;

            // Level up attached attributes
            attachedAttributes.MergeOtherAttributePacket(baseData.GetLevelUpAttachedAttributes());
            
            // Level up attached stats
            attachedStats.MergeOtherStatPacket(baseData.GetLevelUpAttachedStats());
            
            // Level up attached abilities
            foreach (RealAbilityData ability in attachedAbilities) ability.LevelUp();

            return true;
        }

        public BaseItemData GetBaseData() => baseData;

        public RealAbilityData[] GetAttachedAbilities() => attachedAbilities;

        public AttributePacket GetAttachedAttributes() => attachedAttributes;

        public StatPacket GetAttachedStats() => attachedStats;

        public RealPowerPacket GetPowerPacket() => power;

        public BasePowerPacket GetBasePowerPacket() => baseData.GetPowerPacket();
    }
}
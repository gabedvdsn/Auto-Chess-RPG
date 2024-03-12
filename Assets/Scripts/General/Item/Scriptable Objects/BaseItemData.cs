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
        [FormerlySerializedAs("attachedAttributes")] [SerializeField] private BaseAttributePacket attachedBaseAttributes;
        [SerializeField] private StatPacket attachedStats;
        [SerializeField] private BaseAbilityData[] attachedAbilities;
        [SerializeField] private BasePowerPacket power;
        
        [FormerlySerializedAs("onLevelAttributes")]
        [Header("Level Up Information")]
        [SerializeField] private BaseAttributePacket onLevelBaseAttributes;
        [SerializeField] private StatPacket onLevelStats;


        // Base Item Data Information Getters
        public ItemType GetItemType() => itemType;

        public BasePowerPacket GetPowerPacket() => power;
        
        public BaseAttributePacket GetAttachedAttributes() => attachedBaseAttributes;

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

        private RealAttributePacket _attachedBaseAttributes;
        private StatPacket attachedStats;

        

        public RealItemData(BaseItemData _baseData, RealPowerPacket _power, RealAbilityData[] _attachedAbilities, RealAttributePacket attachedBaseAttributes, StatPacket _attachedStats, RealItemData _attachedItem = null)
        {
            baseData = _baseData;
            power = _power;

            attachedAbilities = _attachedAbilities;
            
            _attachedBaseAttributes = attachedBaseAttributes;
            attachedStats = _attachedStats;
        }

        public virtual bool LevelUp()
        {
            if (!power.LevelUp()) return false;

            // Level up attached attributes
            _attachedBaseAttributes.MergeOtherAttributePacket(baseData.GetLevelUpAttachedAttributes());
            
            // Level up attached stats
            attachedStats.MergeOtherStatPacket(baseData.GetLevelUpAttachedStats());
            
            // Level up attached abilities
            foreach (RealAbilityData ability in attachedAbilities) ability.LevelUp();

            return true;
        }

        public BaseItemData GetBaseData() => baseData;

        public RealAbilityData[] GetAttachedAbilities() => attachedAbilities;

        public RealAttributePacket GetAttachedAttributes() => _attachedBaseAttributes;

        public StatPacket GetAttachedStats() => attachedStats;

        public RealPowerPacket GetPowerPacket() => power;

        public BasePowerPacket GetBasePowerPacket() => baseData.GetPowerPacket();
        
    }
}
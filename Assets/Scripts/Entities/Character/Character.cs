using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG.Entity.Character
{
    public class Character : ObservableSubject, ICharacterEntity
    {
        // Base Information
        private CharacterEntityData characterData;

        private Affiliation affiliation;

        private EffectShelf effectShelf;
        private AbilityShelf abilityShelf;

        private StatPacket stats;
        
        // Status Information
        private bool isDead;
        private bool isPermanentlyDead;
        
        // Start is called before the first frame update
        void Start()
        {
            effectShelf = GetComponentInChildren<EffectShelf>();
            abilityShelf = GetComponentInChildren<AbilityShelf>();

            effectShelf.Initialize(this);

            stats = AttributesManager.ComputeStatPacketFromAttributePacket(characterData.GetAttributes());
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public CharacterEntityData GetData() => characterData;

        public Affiliation GetAffiliation() => affiliation;

        public bool AttachEffect(EntityBaseData source, EffectBaseData effect)
        {
            return effectShelf.AddEffect(source, effect);
        }

        public bool ApplyModifierToStats(CharacterModifierTag modifier, float amount)
        {
            return stats.ApplyModifier(modifier, amount);
        }

        public EntityBaseData GetBaseData()
        {
            return GetData();
        }
    }
}

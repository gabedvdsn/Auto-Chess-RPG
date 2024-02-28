using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG
{
    public class Character : ObservableSubject, ICharacterEntity
    {
        // Base Information
        [SerializeField] private CharacterEntityData characterData;

        private Affiliation affiliation;

        private EffectShelf effectShelf;
        private AbilityShelf abilityShelf;
        private ItemShelf itemShelf;

        private StatPacket stats;
        private PowerPacket power;

        private EncounterPreferencesPacket encounterPreferences;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool Initialize(Affiliation _affiliation)
        {
            if (effectShelf is null || abilityShelf is null) return false;

            affiliation = _affiliation;
            
            effectShelf = GetComponentInChildren<EffectShelf>();
            abilityShelf = GetComponentInChildren<AbilityShelf>();

            effectShelf.Initialize(this);

            stats = AttributesManager.ComputeStatPacketFromAttributePacket(characterData.GetAttributes());

            return true;
        }

        #region Getters
        
        public StatPacket GetCharacterStatPacket() => stats;

        public PowerPacket GetCharacterPowerPacket() => power;

        public AbilityShelf GetCharacterAbilityShelf() => abilityShelf;
        
        public ItemShelf GetCharacterItemShelf() => itemShelf;
        
        public EncounterPreferencesPacket GetCharacterEncounterPreferencesPacket() => encounterPreferences;
        
        public CharacterEntityData GetCharacterData() => characterData;

        public EntityBaseData GetBaseData() => characterData;

        public Affiliation GetAffiliation() => affiliation;
        
        #endregion
        
        #region Effects

        public bool AttachEffect(ICharacterEntity source, EffectBaseData effect) => effectShelf.AddEffect(source, effect);

        public bool RemoveEffect(EffectBaseData effect) => effectShelf.RemoveEffect(effect);
        
        #endregion
        
        #region Applications

        public bool ApplyModifierToStats(CharacterModifierTag modifier, float amount)
        {
            return stats.ApplyModifier(modifier, amount);
        }
        
        #endregion

        
    }
}

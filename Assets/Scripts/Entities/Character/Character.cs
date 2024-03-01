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
        private PowerPacket _power;

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

            stats = AttributesParameters.ComputeStatPacketFromAttributePacket(characterData.GetAttributes());

            return true;
        }

        #region Getters
        
        public StatPacket GetCharacterStatPacket() => stats;

        public PowerPacket GetCharacterPowerPacket() => _power;

        public AbilityShelf GetCharacterAbilityShelf() => abilityShelf;
        
        public ItemShelf GetCharacterItemShelf() => itemShelf;
        
        public EncounterPreferencesPacket GetCharacterEncounterPreferencesPacket() => encounterPreferences;
        
        public CharacterEntityData GetCharacterData() => characterData;

        public EntityBaseData GetBaseData() => characterData;

        public Affiliation GetAffiliation() => affiliation;
        
        #endregion
        
        #region Effects

        public bool AttachEffect(ICharacterEntity source, BaseEffectData baseEffect) => effectShelf.AddEffect(source, baseEffect);

        public bool RemoveEffect(BaseEffectData baseEffect) => effectShelf.RemoveEffect(baseEffect);
        
        #endregion
        
        #region Applications

        public bool ApplyModifierToStats(CharacterModifierTag modifier, float amount)
        {
            return stats.ApplyModifier(modifier, amount);
        }
        
        #endregion

        
    }
}

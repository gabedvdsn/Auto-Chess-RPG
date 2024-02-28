using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AutoChessRPG
{
    public class CharacterUIContainer : LowerUIContainer
    {
        [Header("UI Text Components")] 
        [SerializeField] private TMP_Text characterName;
        
        [SerializeField] private TMP_Text characterHealth;
        [SerializeField] private TMP_Text characterMana;
        
        [SerializeField] private TMP_Text attackText;
        [SerializeField] private TMP_Text attackSpeedText;
        [SerializeField] private TMP_Text moveSpeedText;
        [SerializeField] private TMP_Text armorText;
        [SerializeField] private TMP_Text negationText;
        [SerializeField] private TMP_Text healthRegenText;
        [SerializeField] private TMP_Text manaRegenText;
        
        [SerializeField] private TMP_Text strengthText;
        [SerializeField] private TMP_Text agilityText;
        [SerializeField] private TMP_Text intelligenceText;
        
        [Header("UI Slider Components")]
        [SerializeField] private Slider characterHealthSlider;
        [SerializeField] private Slider characterManaSlider;
        
        [FormerlySerializedAs("effecShelfUIContainer")]
        [Header("Effect Shelf Components")]
        [SerializeField] private EffectShelfUIContainer effectShelfUIContainer;
        
        [Header("Ability Shelf Components")]
        [SerializeField] private AbilityShelfUIContainer abilityShelfUIContainer;
        
        // Specified character information
        private Character character;
        
        private StatPacket stats;
        private AttributePacket attributes;
        
        private EffectShelf effectShelf;
        private AbilityShelf abilityShelf;

        public void SendCharacter(Character _character)
        {
            if (_character != null && _character != character) character = _character;
        }

        private void UpdateUI()
        {
            
        }
    }
}

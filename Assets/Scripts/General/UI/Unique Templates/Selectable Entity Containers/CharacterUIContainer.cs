using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AutoChessRPG
{
    public class CharacterUIContainer : LowerUIContainer
    {
        [Header("UI Text Components")] 
        [SerializeField] private TMP_Text currHealth;
        [SerializeField] private TMP_Text currMana;
        [SerializeField] private TMP_Text healthRegenText;
        [SerializeField] private TMP_Text manaRegenText;
        
        [Space]
        
        [Header("UI Slider Components")]
        [SerializeField] private Slider characterHealthSlider;
        [SerializeField] private Slider characterManaSlider;
        
        [Header("Effect Shelf Components")]
        [SerializeField] private EffectShelfUIContainer effectShelfUIContainer;
        
        [Header("Ability Shelf Components")]
        [SerializeField] private AbilityShelfUIContainer abilityShelfUIContainer;
        
        // Specified character information
        private Character character;
        
        private StatPacket stats;
        private BaseAttributePacket _baseAttributes;
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG.Entity.Character
{
    public interface ICharacterEntity : IEntity
    {
        public CharacterEntityData GetCharacterData();
        
        public bool AttachEffect(ICharacterEntity source, EffectBaseData effect);

        public bool RemoveEffect(EffectBaseData effect);
    }
}

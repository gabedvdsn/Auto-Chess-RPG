using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG
{
    public interface ICharacterEntity : IEntity
    {
        public CharacterEntityData GetCharacterData();
        
        public bool AttachEffect(ICharacterEntity source, BaseEffectData baseEffect);

        public bool RemoveEffect(BaseEffectData baseEffect);
    }
}

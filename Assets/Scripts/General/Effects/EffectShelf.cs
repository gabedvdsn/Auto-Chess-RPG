using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoChessRPG.Entity;
using AutoChessRPG.Entity.Character;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace AutoChessRPG
{
    public class EffectShelf : ObservableSubject
    {
        private Character owner;
        private Dictionary<EffectBaseData, EffectRecord> shelf;
        private List<EffectRecord> records;

        private bool doDispell;
        private List<EffectBaseData> dispellQueue;

        private void Start()
        {
            shelf = new Dictionary<EffectBaseData, EffectRecord>();
        }

        private void Update()
        {
            TickEffects();

            if (doDispell) EmptyDispellQueue();
        }

        public bool Initialize(Character _owner) => owner = _owner;

        private void TickEffects()
        {
            foreach (EffectBaseData effect in shelf.Keys.Where(effect => shelf[effect].GetResultOfTick() && !effect.GetApplyOnce()))
            {
                ApplyEffect(effect);
            }
        }

        private void ApplyConstantEffect(EffectBaseData effect)
        {
            if (effect.GetApplyOnce()) ApplyEffect(effect);
        }

        private void ApplyEffect(EffectBaseData effect)
        {
            owner.ApplyModifierToStats(effect.GetModifier(), effect.GetEffectAmount());
        }
        
        public bool AddEffect(ICharacterEntity source, EffectBaseData effect)
        {
            shelf[effect] = new EffectRecord(source, owner, effect, Time.time);
            
            ApplyConstantEffect(effect);

            return true;
        }

        public bool RemoveEffect(EffectBaseData effect)
        {
            EffectRecord record = shelf[effect];

            records.Add(record);
            shelf.Remove(effect);

            if (effect.GetReverseEffectsAtTermination()) owner.ApplyModifierToStats(effect.GetModifier(), -record.statDelta);

            return true;
        }

        public void ApplyDispell(Dispell dispell)
        {
            foreach (EffectBaseData effect in shelf.Keys.Where(effect => DispellManager.DispellIsEffective(dispell, effect.GetDispellRequirement())))
            {
                dispellQueue.Add(effect);
                doDispell = true;
            }
        }

        private bool EmptyDispellQueue()
        {
            foreach (EffectBaseData effect in dispellQueue)
            {
                RemoveEffect(effect);
            }

            doDispell = false;

            return true;
        }

        public EffectRecord[] EmptyRecords()
        {
            EffectRecord[] recordsCopy = new EffectRecord[] { };
            records.CopyTo(recordsCopy);
            
            records.Clear();
            
            return recordsCopy;
        }
    }

    public struct EffectRecord : IObservableData
    {
        public ICharacterEntity target;
        public ICharacterEntity source;
        public EffectBaseData baseData;  // the source effect
        
        public float timeInitial;
        public float timeRemaining;

        public float tickDelta;

        public float statDelta;
                                    
        public EffectRecord(ICharacterEntity _source, ICharacterEntity _target, EffectBaseData _baseData, float _timeInitial)
        {
            source = _source;
            target = _target;
            
            baseData = _baseData;
            
            timeInitial = _timeInitial;
            timeRemaining = _baseData.GetEffectDuration();
            
            tickDelta = 0f;
            statDelta = 0f;
        }

        public bool GetResultOfTick()
        {
            tickDelta += Time.deltaTime;

            if (!(tickDelta > baseData.GetTickRate())) return false;
            
            tickDelta -= baseData.GetTickRate();
            return true;

        }

        public IObservableData OnObserve()
        {
            return this;
        }

        public Dictionary<string, string> FormatForObservance()
        {
            return new Dictionary<string, string>()
            {
                {ObservableDataFormatting.ACTION_SOURCE_CHARACTER, source.GetBaseData().GetEntityName()},
                {ObservableDataFormatting.ACTION_TARGET_CHARACTER, target.GetBaseData().GetEntityName()},
                {ObservableDataFormatting.IMPACT_CHARACTER_EFFECT_SOURCE, baseData.GetEntityName()},
                {ObservableDataFormatting.ACTION_TIME_INITIAL, timeInitial.ToString()},
                {ObservableDataFormatting.ACTION_TIME_FINAL, (timeInitial + (baseData.GetEffectDuration() - timeRemaining)).ToString()},
                {ObservableDataFormatting.IMPACT_CHARACTER_EFFECT_MODIFIER, baseData.GetModifier().ToString()}
            };
        }
    }
    
}
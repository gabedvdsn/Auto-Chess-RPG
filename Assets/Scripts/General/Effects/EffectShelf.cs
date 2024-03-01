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
        private Dictionary<BaseEffectData, EffectRecord> shelf;
        private List<EffectRecord> records;

        private bool doDispell;
        private List<BaseEffectData> dispellQueue;

        private void Start()
        {
            shelf = new Dictionary<BaseEffectData, EffectRecord>();
        }

        private void Update()
        {
            TickEffects();

            if (doDispell) EmptyDispellQueue();
        }

        public bool Initialize(Character _owner) => owner = _owner;

        private void TickEffects()
        {
            foreach (BaseEffectData effect in shelf.Keys.Where(effect => shelf[effect].GetResultOfTick() && !effect.GetApplyOnce()))
            {
                ApplyEffect(effect);
            }
        }

        private void ApplyConstantEffect(BaseEffectData baseEffect)
        {
            if (baseEffect.GetApplyOnce()) ApplyEffect(baseEffect);
        }

        private void ApplyEffect(BaseEffectData baseEffect)
        {
            owner.ApplyModifierToStats(baseEffect.GetModifier(), baseEffect.GetEffectAmount());
        }
        
        public bool AddEffect(ICharacterEntity source, BaseEffectData baseEffect)
        {
            shelf[baseEffect] = new EffectRecord(source, owner, baseEffect, Time.time);
            
            ApplyConstantEffect(baseEffect);

            return true;
        }

        public bool RemoveEffect(BaseEffectData baseEffect)
        {
            EffectRecord record = shelf[baseEffect];

            records.Add(record);
            shelf.Remove(baseEffect);

            if (baseEffect.GetReverseEffectsAtTermination()) owner.ApplyModifierToStats(baseEffect.GetModifier(), -record.statDelta);

            return true;
        }

        public void ApplyDispell(Dispell dispell)
        {
            foreach (BaseEffectData effect in shelf.Keys.Where(effect => DispellManager.DispellIsEffective(dispell, effect.GetDispellRequirement())))
            {
                dispellQueue.Add(effect);
                doDispell = true;
            }
        }

        private bool EmptyDispellQueue()
        {
            foreach (BaseEffectData effect in dispellQueue)
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
        public BaseEffectData data;  // the source baseEffect
        
        public float timeInitial;
        public float timeRemaining;

        public float tickDelta;

        public float statDelta;
                                    
        public EffectRecord(ICharacterEntity _source, ICharacterEntity _target, BaseEffectData data, float _timeInitial)
        {
            source = _source;
            target = _target;
            
            this.data = data;
            
            timeInitial = _timeInitial;
            timeRemaining = data.GetDuration();
            
            tickDelta = 0f;
            statDelta = 0f;
        }

        public bool GetResultOfTick()
        {
            tickDelta += Time.deltaTime;

            if (!(tickDelta > data.GetTickRate())) return false;
            
            tickDelta -= data.GetTickRate();
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
                {ObservableDataFormatting.IMPACT_CHARACTER_EFFECT_SOURCE, data.GetEntityName()},
                {ObservableDataFormatting.ACTION_TIME_INITIAL, timeInitial.ToString()},
                {ObservableDataFormatting.ACTION_TIME_FINAL, (timeInitial + (data.GetDuration() - timeRemaining)).ToString()},
                {ObservableDataFormatting.IMPACT_CHARACTER_EFFECT_MODIFIER, data.GetModifier().ToString()}
            };
        }
    }
    
}
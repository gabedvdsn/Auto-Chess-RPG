using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
        private Dictionary<RealEffectData, EffectRecord> shelf;
        private List<EffectRecord> records;

        private bool doDispell;
        private List<RealEffectData> dispellQueue;

        private int dispellValue;

        private void Start()
        {
            shelf = new Dictionary<RealEffectData, EffectRecord>();
        }

        private void Update()
        {
            TickEffects();

            if (doDispell) EmptyDispellQueue();
        }

        public bool Initialize(Character _owner) => owner = _owner;

        private void TickEffects()
        {
            foreach (RealEffectData effect in shelf.Keys.Where(effect => shelf[effect].GetResultOfTick() && !effect.GetBaseData().GetApplyOnce()))
            {
                ApplyEffect(effect);
            }
        }

        private void ApplyConstantEffect(RealEffectData effect)
        {
            if (effect.GetBaseData().GetApplyOnce()) ApplyEffect(effect);
        }

        private void ApplyEffect(RealEffectData baseEffect)
        {
            owner.ApplyModifierToStats(baseEffect.GetBaseData().GetModifier(), baseEffect.GetAmount());
        }
        
        public bool AttachEffect(ICharacterEntity source, RealEffectData effect)
        {
            shelf[effect] = new EffectRecord(source, owner, effect, Time.time);
            
            ApplyConstantEffect(effect);

            dispellValue += (int)effect.GetBaseData().GetDispellRequirement();

            return true;
        }

        public bool RemoveEffect(RealEffectData effect)
        {
            EffectRecord record = shelf[effect];

            records.Add(record);
            shelf.Remove(effect);

            if (effect.GetBaseData().GetReverseEffectsAtTermination()) owner.ApplyModifierToStats(effect.GetBaseData().GetModifier(), -record.statDelta);

            return true;
        }

        public void ApplyDispell(Dispell dispell)
        {
            foreach (RealEffectData effect in shelf.Keys.Where(effect => DispellManager.DispellIsEffective(dispell, effect.GetBaseData().GetDispellRequirement())))
            {
                dispellQueue.Add(effect);
                doDispell = true;
            }
        }

        private bool EmptyDispellQueue()
        {
            foreach (RealEffectData effect in dispellQueue)
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

        public int GetDispellValue() => dispellValue;
    }

    public struct EffectRecord : IObservableData
    {
        public ICharacterEntity target;
        public ICharacterEntity source;
        public RealEffectData data;  // the source effect
        
        public float timeInitial;
        public float timeRemaining;

        public float tickDelta;

        public float statDelta;
                                    
        public EffectRecord(ICharacterEntity _source, ICharacterEntity _target, RealEffectData data, float _timeInitial)
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
                {ObservableDataFormatting.IMPACT_CHARACTER_EFFECT_SOURCE, data.GetBaseData().GetEntityName()},
                {ObservableDataFormatting.ACTION_TIME_INITIAL, timeInitial.ToString(CultureInfo.InvariantCulture)},
                {ObservableDataFormatting.ACTION_TIME_FINAL, (timeInitial + (data.GetDuration() - timeRemaining)).ToString(CultureInfo.InvariantCulture)},
                {ObservableDataFormatting.IMPACT_CHARACTER_EFFECT_MODIFIER, data.GetBaseData().GetModifier().ToString()}
            };
        }
    }
    
}
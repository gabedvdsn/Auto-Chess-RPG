namespace AutoChessRPG
{
    public class RealPassiveAbilityData : RealAbilityData
    {
        private BasePassiveAbilityData baseData;

        public RealPassiveAbilityData(BasePassiveAbilityData _baseData, RealItemData _attachedItem = null) : base(_baseData, _attachedItem)
        {
            baseData = _baseData;
        }

        public BasePassiveAbilityData GetBasePassiveData() => baseData;
    }
}

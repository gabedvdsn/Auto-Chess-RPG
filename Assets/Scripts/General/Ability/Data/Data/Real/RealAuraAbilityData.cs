namespace AutoChessRPG
{
    public class RealAuraAbilityData : RealAbilityData
    {
        private BaseAuraAbilityData baseData;

        public RealAuraAbilityData(BaseAuraAbilityData _baseData, RealItemData _attachedItem = null) : base(_baseData, _attachedItem)
        {
            baseData = _baseData;
        }

        public BaseAuraAbilityData GetBaseAuraData() => baseData;
    }
}

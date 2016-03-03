public class TestUpgrade : Upgrade
{
    public TestUpgrade(string parName, float parCost, float parValue, float parAddByLevel) : base(parName, parCost, parValue, UpgradeType.TestUpgrade, parAddByLevel) { }

    public override float Apply()
    {
        return base.Apply();
    }
}

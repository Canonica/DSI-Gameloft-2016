public class TestUpgrade : Upgrade
{
    public TestUpgrade(string parName, int parCost, float parValue) : base(parName, parCost, parValue) { }

    public override float Apply()
    {
        float res = base.Apply();

        //modifications

        return res;
    }
}

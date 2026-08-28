namespace MyObject
{
    public class RestTransport : RestObject
    {
        public string portName;

        public override void Rest()
        {
            base.Rest();
            var pd = GameManager.instance.playerData;
            string queryKey = portName+"_is_active";
            if(pd.BoolGet(queryKey)==true)return ;
            pd.BoolSet(queryKey,true);
            pd.HPLimitChange(1);
        }
    }
}
namespace Necnat.Abp.NnLibCommon.Blazor.Helpers
{
    public sealed class EventCallbackReturnParameter<TInput, TOutput>
    {
        public TInput? Value { get; set; }
        public TOutput? OutputValue { get; set; }

        public EventCallbackReturnParameter()
        {
        }

        public EventCallbackReturnParameter(TOutput outputValue)
        {
            OutputValue = outputValue;
        }

        public EventCallbackReturnParameter(TInput value, TOutput outputValue)
        {
            Value = value;
            OutputValue = outputValue;
        }
    }
}

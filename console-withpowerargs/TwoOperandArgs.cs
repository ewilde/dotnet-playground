using PowerArgs;

namespace console_withpowerargs
{
    public class TwoOperandArgs
    {
        [ArgRequired, ArgDescription("The first operand to process"), ArgPosition(1)]
        public double Value1 { get; set; }
        [ArgRequired, ArgDescription("The second operand to process"), ArgPosition(2)]
        public double Value2 { get; set; }
    }
}
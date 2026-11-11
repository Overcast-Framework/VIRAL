using System;
using System.Collections.Generic;
using System.Text;

namespace Veyr.VIRAL
{
    public abstract record IRInstruction;

    public record ConstStringInstruction(
            IRValue Result,
            string Value
        )
        : IRInstruction;

    public record CallStaticInstruction(
            IRValue? Result,
            IRFunction Method,
            List<IRValue> Arguments
        )
        : IRInstruction;
}

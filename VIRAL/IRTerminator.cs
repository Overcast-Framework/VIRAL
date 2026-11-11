using System;
using System.Collections.Generic;
using System.Text;

namespace Veyr.VIRAL
{
    public abstract record IRTerminator;

    public record ReturnTerminator(
        IRValue? Value
    ) : IRTerminator;

    public record BranchTerminator(
        BasicBlock Target
    ) : IRTerminator;

    public record ConditionalBranchTerminator(
        IRValue Condition,
        BasicBlock TrueTarget,
        BasicBlock FalseTarget
    ) : IRTerminator;
}

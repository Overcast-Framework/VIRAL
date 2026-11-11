using System;
using System.Collections.Generic;
using System.Text;

namespace Veyr.VIRAL
{
    public abstract record IRType;

    public record VoidIRType() : IRType;
    public record IntegerIRType(int Bits) : IRType;
    public record StringIRType() : IRType;
    public record ArrayIRType(IRType ElementType) : IRType;
    public record ClassIRType(IRClass Class) : IRType;
}

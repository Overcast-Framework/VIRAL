using Pathogen;
using Veyr.VIRAL;

namespace VIRAL.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRModule module = new("ExampleProgram");

            module.InsertImport("System");

            IRClass consoleClass = new("System.Console", [], true);
            IRFunction printLnFunction = new("Println", new VoidIRType(), true, false, consoleClass);
            printLnFunction.Parameters.Add(new IRValue(new StringIRType(), 0));
            consoleClass.InsertMethod(printLnFunction);

            module.InsertClass(consoleClass);

            IRClass programClass = new("Program", []);
            IRFunction mainMethod = new("main", new VoidIRType(), true, false, programClass);
            mainMethod.Parameters.Add(new IRValue(new ArrayIRType(new StringIRType()), 0));

            BasicBlock _entryBlock = new("entry");
            var messageValue = new IRValue(new StringIRType(), 1);
            _entryBlock.Instructions.Add(new ConstStringInstruction(messageValue, "Hello, world!"));
            _entryBlock.Instructions.Add(new CallStaticInstruction(null, printLnFunction, [messageValue]));
            _entryBlock.Terminator = new ReturnTerminator(null);

            mainMethod.Blocks.Add(_entryBlock);

            programClass.InsertMethod(mainMethod);
            module.InsertClass(programClass);

            VIRALPrinter printer = new();
            Console.WriteLine(printer.Print(module));

            Runtime _rt = new();

            _rt.LoadModule(module);
            _rt.Execute(mainMethod);

            Console.ReadLine();
        }
    }
}

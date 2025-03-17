using System;
using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

public class CodeCompiler
{
    public static Func<int[], int[]> CompileUserSort(string userCode)
    {
        string sourceCode = @"
            using System;
            public class UserSorter
            {
                public static int[] Sort(int[] input)
                {
                    " + userCode + @"
                    return input;
                }
            }";

        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

        // Ссылки на стандартные сборки
        MetadataReference[] references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
        };

        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: Guid.NewGuid().ToString(),
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using (var ms = new System.IO.MemoryStream())
        {
            EmitResult result = compilation.Emit(ms);

            if (!result.Success)
            {
                var errors = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);

                string errorMessage = string.Join(Environment.NewLine, errors.Select(diag => diag.GetMessage()));
                throw new CompilerException("Ошибка компиляции: " + errorMessage);
            }

            ms.Seek(0, System.IO.SeekOrigin.Begin);
            Assembly assembly = Assembly.Load(ms.ToArray());

            Type type = assembly.GetType("UserSorter");
            MethodInfo method = type.GetMethod("Sort");

            return (Func<int[], int[]>)Delegate.CreateDelegate(typeof(Func<int[], int[]>), method);
        }
    }
}

// Исключение для ошибок компиляции
public class CompilerException : Exception
{
    public CompilerException(string message) : base(message) { }
}
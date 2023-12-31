public string WriteCode()
{
    CodeBuilder.Clear();

    CodeBuilder.AppendLine("using System;");
    CodeBuilder.AppendLine("using System.Collections.Generic;");
    CodeBuilder.AppendLine("using System.Text.Json.Serialization;");
    CodeBuilder.AppendLine("using MediatR.CommandQuery.Queries;");
    CodeBuilder.AppendLine("using Microsoft.AspNetCore.JsonPatch;");

    var names = EntityContext.Entities
        .SelectMany(e => e.Models.Select(m => m.ModelNamespace))
        .ToHashSet();

    foreach (var name in names)
        CodeBuilder.AppendLine($"using {name};");

    CodeBuilder.AppendLine();
    CodeBuilder.AppendLine("// ReSharper disable once CheckNamespace");
    CodeBuilder.AppendLine($"namespace {TemplateOptions.Namespace};");
    CodeBuilder.AppendLine();

    GenerateClass();

    CodeBuilder.AppendLine();

    return CodeBuilder.ToString();
}

private void GenerateClass()
{
    CodeBuilder.AppendLine($"[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]");
    CodeBuilder.AppendLine("#region Generated Attributes");

    foreach (var entity in EntityContext.Entities.OrderBy(e => e.ContextProperty))
    {
        foreach (var model in entity.Models.OrderBy(e => e.ModelType))
        {
            switch (model.ModelType)
            {
                case ModelType.Read:
                    var readModel = model.ModelClass.ToSafeName();

                    CodeBuilder.AppendLine($"[JsonSerializable(typeof({readModel}))]");
                    CodeBuilder.AppendLine($"[JsonSerializable(typeof(IReadOnlyCollection<{readModel}>))]");
                    CodeBuilder.AppendLine($"[JsonSerializable(typeof(EntityPagedResult<{readModel}>))]");

                    break;
                case ModelType.Create:
                    var createModel = model.ModelClass.ToSafeName();

                    CodeBuilder.AppendLine($"[JsonSerializable(typeof({createModel}))]");

                    break;
                case ModelType.Update:
                    var updateModel = model.ModelClass.ToSafeName();

                    CodeBuilder.AppendLine($"[JsonSerializable(typeof({updateModel}))]");

                    break;
            }
        }
    }

    // query types
    CodeBuilder.AppendLine($"[JsonSerializable(typeof(EntityQuery))]");
    CodeBuilder.AppendLine($"[JsonSerializable(typeof(EntitySelect))]");
    CodeBuilder.AppendLine($"[JsonSerializable(typeof(IJsonPatchDocument))]");

    CodeBuilder.AppendLine("#endregion");

    string className = System.IO.Path.GetFileNameWithoutExtension(TemplateOptions.FileName);

    CodeBuilder.AppendLine($"public partial class {className} : JsonSerializerContext");
    CodeBuilder.AppendLine("{");
    CodeBuilder.AppendLine();
    CodeBuilder.AppendLine("}");
}


// run script
WriteCode()

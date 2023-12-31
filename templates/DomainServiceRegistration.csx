public string WriteCode()
{
    if (Entity.Models.Count == 0)
        return string.Empty;

    var modelNamespace = Entity.Models.Select(m => m.ModelNamespace).FirstOrDefault();
    var readModel = string.Empty;
    var createModel = string.Empty;
    var updateModel = string.Empty;

    foreach (var model in Entity.Models)
    {
        switch (model.ModelType)
        {
            case ModelType.Read:
                readModel = model.ModelClass.ToSafeName();
                break;
            case ModelType.Create:
                createModel = model.ModelClass.ToSafeName();
                break;
            case ModelType.Update:
                updateModel = model.ModelClass.ToSafeName();
                break;
        }
    }

    if (string.IsNullOrEmpty(readModel))
        return string.Empty;

    CodeBuilder.Clear();

    CodeBuilder.AppendLine("using System;");
    CodeBuilder.AppendLine("using System.Collections.Generic;");
    CodeBuilder.AppendLine("using MediatR.CommandQuery.EntityFrameworkCore;");
    CodeBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");

    if (!string.IsNullOrEmpty(modelNamespace))
        CodeBuilder.AppendLine($"using {modelNamespace};");

    CodeBuilder.AppendLine();
    CodeBuilder.AppendLine("// ReSharper disable once CheckNamespace");
    CodeBuilder.AppendLine($"namespace {TemplateOptions.Namespace};");
    CodeBuilder.AppendLine();

    GenerateClass(readModel, createModel, updateModel);

    CodeBuilder.AppendLine();

    return CodeBuilder.ToString();
}

private void GenerateClass(string readModel, string createModel, string updateModel)
{
    string className = System.IO.Path.GetFileNameWithoutExtension(TemplateOptions.FileName);

    CodeBuilder.AppendLine($"public class {className}");
    CodeBuilder.AppendLine("{");

    using (CodeBuilder.Indent())
    {
        GenerateRegister(readModel, createModel, updateModel);
    }

    CodeBuilder.AppendLine("}");

}

private void GenerateRegister(string readModel, string createModel, string updateModel)
{
    var contextClass = Entity.Context.ContextClass.ToSafeName();
    var contextNamespace = Entity.Context.ContextNamespace;

    var entityNamespace = Entity.EntityNamespace;
    var entityClass = Entity.EntityClass.ToSafeName();

    var keyType = TemplateOptions.Parameters["keyType"];

    CodeBuilder.AppendLine($"[RegisterServices]");
    CodeBuilder.AppendLine($"public void Register(IServiceCollection services)");
    CodeBuilder.AppendLine("{");

    using (CodeBuilder.Indent())
    {
        CodeBuilder.AppendLine("#region Generated Register");

        CodeBuilder.AppendLine($"services.AddEntityQueries<{contextNamespace}.{contextClass}, {entityNamespace}.{entityClass}, {keyType}, {readModel}>();");
        CodeBuilder.AppendLine();

        if (!string.IsNullOrEmpty(updateModel))
        {
            CodeBuilder.AppendLine($"services.AddEntityCommands<{contextNamespace}.{contextClass}, {entityNamespace}.{entityClass}, {keyType}, {readModel}, {createModel}, {updateModel}>();");
            CodeBuilder.AppendLine();
        }

        CodeBuilder.AppendLine("#endregion");
    }

    CodeBuilder.AppendLine("}");
    CodeBuilder.AppendLine();
}


// run script
WriteCode()

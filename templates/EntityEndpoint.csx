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
    CodeBuilder.AppendLine("using MediatR;");
    CodeBuilder.AppendLine("using MediatR.CommandQuery.Endpoints;");

    if (!string.IsNullOrEmpty(modelNamespace))
        CodeBuilder.AppendLine($"using {modelNamespace};");

    CodeBuilder.AppendLine();

    CodeBuilder.AppendLine($"namespace {TemplateOptions.Namespace};");
    CodeBuilder.AppendLine();

    GenerateClass(readModel, createModel, updateModel);

    CodeBuilder.AppendLine();

    return CodeBuilder.ToString();
}

private void GenerateClass(string readModel, string createModel, string updateModel)
{
    var className = System.IO.Path.GetFileNameWithoutExtension(TemplateOptions.FileName);
    var keyType = TemplateOptions.Parameters["keyType"];

    CodeBuilder.AppendLine("[RegisterTransient<IFeatureEndpoint>(Duplicate = DuplicateStrategy.Append)]");

    if (string.IsNullOrEmpty(updateModel))
        CodeBuilder.AppendLine($"public class {className} : EntityQueryEndpointBase<{keyType}, {readModel}, {readModel}>");
    else
        CodeBuilder.AppendLine($"public class {className} : EntityCommandEndpointBase<{keyType}, {readModel}, {readModel}, {createModel}, {updateModel}>");

    CodeBuilder.AppendLine("{");

    using (CodeBuilder.Indent())
    {
        GenerateConstructor(className);
    }

    CodeBuilder.AppendLine("}");

}

private void GenerateConstructor(string className)
{
    CodeBuilder.AppendLine($"public {className}(IMediator mediator) : base(mediator, \"{Entity.EntityClass}\")");
    CodeBuilder.AppendLine("{");
    CodeBuilder.AppendLine();
    CodeBuilder.AppendLine("}");
    CodeBuilder.AppendLine();
}

// run script
WriteCode()

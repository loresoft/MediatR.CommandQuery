public string WriteCode()
{
    if (Entity.Models.Count == 0)
        return string.Empty;
    
    var modelNamespace = Entity.Models.Select(m => m.ModelNamespace).FirstOrDefault();
    var readModel = string.Empty;
    var createModel= string.Empty;
    var updateModel= string.Empty;

    foreach(var model in Entity.Models)
    {
        switch(model.ModelType)
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
    CodeBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
    
    if (!string.IsNullOrEmpty(modelNamespace))
        CodeBuilder.AppendLine($"using {modelNamespace};");
    
    CodeBuilder.AppendLine();
    CodeBuilder.AppendLine("// ReSharper disable once CheckNamespace");
    CodeBuilder.AppendLine($"namespace {TemplateOptions.Namespace}");
    CodeBuilder.AppendLine("{");

    using (CodeBuilder.Indent())
    {
        GenerateClass(readModel, createModel, updateModel);
    }

    CodeBuilder.AppendLine("}");

    return CodeBuilder.ToString();
}

private void GenerateClass(string readModel, string createModel, string updateModel)
{
    string className = System.IO.Path.GetFileNameWithoutExtension(TemplateOptions.FileName);
    
    CodeBuilder.AppendLine($"public class {className} : DomainServiceRegistrationBase");
    CodeBuilder.AppendLine("{");

    using (CodeBuilder.Indent())
    {
        GenerateRegister(readModel, createModel, updateModel);
    }

    CodeBuilder.AppendLine("}");

}

private void GenerateRegister(string readModel, string createModel, string updateModel)
{           
    var entityNamespace = Entity.EntityNamespace;
    var entityClass = Entity.EntityClass.ToSafeName();

    CodeBuilder.AppendLine($" public override void Register(IServiceCollection services, IDictionary<string, object> data)");
    CodeBuilder.AppendLine("{");

    using (CodeBuilder.Indent())
    {
        CodeBuilder.AppendLine($"RegisterEntityQuery<Guid, {entityNamespace}.{entityClass}, {readModel}>(services);");
        CodeBuilder.AppendLine();

        if (!string.IsNullOrEmpty(updateModel))
        {
            CodeBuilder.AppendLine($"RegisterEntityCommand<Guid, {entityNamespace}.{entityClass}, {readModel}, {createModel}, {updateModel}>(services);");
            CodeBuilder.AppendLine();
        }
    }

    CodeBuilder.AppendLine("}");
    CodeBuilder.AppendLine();
}


// run script
WriteCode()
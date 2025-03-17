using System.Text;

namespace MediatR.CommandQuery.Queries;

/// <summary>
/// Build a <c>string</c> based Linq expression from a <see cref="EntityFilter"/> instance
/// </summary>
public class LinqExpressionBuilder
{
    private static readonly Dictionary<string, string> _operatorMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { EntityFilterOperators.Equal, "==" },
        { EntityFilterOperators.NotEqual, "!=" },
        { EntityFilterOperators.LessThan, "<" },
        { EntityFilterOperators.LessThanOrEqual, "<=" },
        { EntityFilterOperators.GreaterThan, ">" },
        { EntityFilterOperators.GreaterThanOrEqual, ">=" },
        { "equals", "==" },
        { "not equals", "!=" },
        { "starts with", EntityFilterOperators.StartsWith },
        { "ends with", EntityFilterOperators.EndsWith },
        { "is null", EntityFilterOperators.IsNull },
        { "is empty", EntityFilterOperators.IsNull },
        { "is not null", EntityFilterOperators.IsNotNull },
        { "is not empty", EntityFilterOperators.IsNotNull },
    };

    private readonly StringBuilder _expression = new();
    private readonly List<object?> _values = [];

    /// <summary>
    /// Gets the expression parameters.
    /// </summary>
    /// <value>
    /// The expression parameters.
    /// </value>
    public IReadOnlyList<object?> Parameters => _values;

    /// <summary>
    /// Gets the Linq expression string.
    /// </summary>
    /// <value>
    /// The Linq expression string.
    /// </value>
    public string Expression => _expression.ToString();


    /// <summary>
    /// Builds a string base Linq expression from the specified <see cref="EntityFilter"/>.
    /// </summary>
    /// <param name="entityFilter">The entity filter to build expression from.</param>
    public void Build(EntityFilter? entityFilter)
    {
        _expression.Length = 0;
        _values.Clear();

        if (entityFilter == null)
            return;

        Visit(entityFilter);
    }

    private void Visit(EntityFilter entityFilter)
    {
        if (WriteGroup(entityFilter))
            return;

        WriteExpression(entityFilter);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "MA0051:Method is too long", Justification = "Expression Logic")]
    private void WriteExpression(EntityFilter entityFilter)
    {
        // name require for expression
        if (string.IsNullOrWhiteSpace(entityFilter.Name))
            return;

        // parameter index number
        int index = _values.Count;

        var name = entityFilter.Name;
        var value = entityFilter.Value;

        // translate operator
        var operatorValue = string.IsNullOrWhiteSpace(entityFilter.Operator) ? "==" : entityFilter.Operator;
        _operatorMap.TryGetValue(operatorValue, out var comparison);
        if (string.IsNullOrEmpty(comparison))
            comparison = operatorValue.Trim();

        // use function call
        var negation = comparison.StartsWith('!') || comparison.StartsWith("not", StringComparison.OrdinalIgnoreCase) ? "!" : string.Empty;

        if (comparison.EndsWith(EntityFilterOperators.StartsWith, StringComparison.OrdinalIgnoreCase))
        {
            _expression.Append(negation).Append(name).Append(".StartsWith(@").Append(index).Append(')');
            _values.Add(value);
        }
        else if (comparison.EndsWith(EntityFilterOperators.EndsWith, StringComparison.OrdinalIgnoreCase))
        {
            _expression.Append(negation).Append(name).Append(".EndsWith(@").Append(index).Append(')');
            _values.Add(value);
        }
        else if (comparison.EndsWith(EntityFilterOperators.Contains, StringComparison.OrdinalIgnoreCase))
        {
            _expression.Append(negation).Append(name).Append(".Contains(@").Append(index).Append(')');
            _values.Add(value);
        }
        else if (comparison.EndsWith(EntityFilterOperators.IsNull, StringComparison.OrdinalIgnoreCase))
        {
            _expression.Append(name).Append(" == NULL");
        }
        else if (comparison.EndsWith(EntityFilterOperators.IsNotNull, StringComparison.OrdinalIgnoreCase))
        {
            _expression.Append(name).Append(" != NULL");
        }
        else if (comparison.EndsWith(EntityFilterOperators.In, StringComparison.OrdinalIgnoreCase))
        {
            _expression.Append(negation).Append("it.").Append(name).Append(" in @").Append(index);
            _values.Add(value);
        }
        else if (comparison.EndsWith(EntityFilterOperators.Expression, StringComparison.OrdinalIgnoreCase))
        {
            // clean up parameter
            var expression = index == 0 ? name : name.Replace("@0", $"@{index}", StringComparison.OrdinalIgnoreCase);

            // use raw expression
            _expression.Append(negation).Append(expression);
            _values.Add(value);
        }
        else
        {
            _expression.Append(name).Append(' ').Append(comparison).Append(" @").Append(index);
            _values.Add(value);
        }

    }

    private bool WriteGroup(EntityFilter entityFilter)
    {
        // check for group start
        var filters = entityFilter.Filters?.Where(f => f.IsValid());
        if (filters?.Any() != true)
            return false;

        var logic = string.IsNullOrWhiteSpace(entityFilter.Logic)
            ? EntityFilterLogic.And
            : entityFilter.Logic;

        var wroteFirst = false;

        _expression.Append('(');
        foreach (var filter in filters)
        {
            if (wroteFirst)
                _expression.Append(' ').Append(logic).Append(' ');

            Visit(filter);
            wroteFirst = true;
        }
        _expression.Append(')');

        return true;
    }
}

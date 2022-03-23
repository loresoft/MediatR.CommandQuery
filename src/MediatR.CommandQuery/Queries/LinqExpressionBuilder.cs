using System;
using System.Collections.Generic;
using System.Linq;
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
        { EntityFilterOperators.GreaterThanOrEqual, ">=" }
    };

    private readonly StringBuilder _expression = new();
    private readonly List<object?> _values = new();

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
    public void Build(EntityFilter entityFilter)
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
        var o = string.IsNullOrWhiteSpace(entityFilter.Operator) ? "==" : entityFilter.Operator;
        _operatorMap.TryGetValue(o, out var comparison);
        if (string.IsNullOrEmpty(comparison))
            comparison = o.Trim();

        // use function call
        var negation = comparison.StartsWith("!") || comparison.StartsWith("not", StringComparison.OrdinalIgnoreCase) ? "!" : string.Empty;

        if (comparison.EndsWith(EntityFilterOperators.StartsWith, StringComparison.OrdinalIgnoreCase))
            _expression.Append(negation).Append(name).Append(".StartsWith(@").Append(index).Append(')');
        else if (comparison.EndsWith(EntityFilterOperators.EndsWith, StringComparison.OrdinalIgnoreCase))
            _expression.Append(negation).Append(name).Append(".EndsWith(@").Append(index).Append(')');
        else if (comparison.EndsWith(EntityFilterOperators.Contains, StringComparison.OrdinalIgnoreCase))
            _expression.Append(negation).Append(name).Append(".Contains(@").Append(index).Append(')');
        else if (comparison.EndsWith(EntityFilterOperators.In, StringComparison.OrdinalIgnoreCase))
            _expression.Append(negation).Append("it.").Append(name).Append(" in @").Append(index);
        else
            _expression.Append(name).Append(' ').Append(comparison).Append(" @").Append(index);

        _values.Add(value);
    }

    private bool WriteGroup(EntityFilter entityFilter)
    {
        // check for group start
        var filters = entityFilter.Filters;

        var hasGroup = filters?.Count > 0;
        if (!hasGroup)
            return false;

        var logic = string.IsNullOrWhiteSpace(entityFilter.Logic)
            ? EntityFilterLogic.And
            : entityFilter.Logic;

        var wroteFirst = false;

        if (filters == null)
            return false;

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

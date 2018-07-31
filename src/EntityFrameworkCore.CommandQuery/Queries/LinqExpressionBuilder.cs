using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameworkCore.CommandQuery.Queries
{
    /// <summary>
    /// Build a <c>string</c> based Linq expression from a <see cref="EntityFilter"/> instance
    /// </summary>
    public class LinqExpressionBuilder
    {
        private static readonly IDictionary<string, string> _operatorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"eq", "=="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="}
        };

        private readonly StringBuilder _expression = new StringBuilder();
        private readonly List<object> _values = new List<object>();

        /// <summary>
        /// Gets the expression parameters.
        /// </summary>
        /// <value>
        /// The expression parameters.
        /// </value>
        public IReadOnlyList<object> Parameters => _values;

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
            _operatorMap.TryGetValue(o, out string comparison);
            if (string.IsNullOrEmpty(comparison))
                comparison = o.Trim();

            // use function call
            var negation = comparison.StartsWith("!") || comparison.StartsWith("not", StringComparison.OrdinalIgnoreCase) ? "!" : string.Empty;
            if (comparison.EndsWith("StartsWith", StringComparison.OrdinalIgnoreCase))
                _expression.Append($"{negation}{name}.StartsWith(@{index})");
            else if (comparison.EndsWith("EndsWith", StringComparison.OrdinalIgnoreCase))
                _expression.Append($"{negation}{name}.EndsWith(@{index})");
            else if (comparison.EndsWith("Contains", StringComparison.OrdinalIgnoreCase))
                _expression.Append($"{negation}{name}.Contains(@{index})");
            else
                _expression.Append($"{name} {comparison} @{index}");

            _values.Add(value);
        }

        private bool WriteGroup(EntityFilter entityFilter)
        {
            // check for group start
            var hasGroup = entityFilter.Filters != null && entityFilter.Filters.Any();
            if (!hasGroup)
                return false;

            var logic = string.IsNullOrWhiteSpace(entityFilter.Logic) ? "and" : entityFilter.Logic;
            var wroteFirst = false;

            _expression.Append("(");
            foreach (var filter in entityFilter.Filters)
            {
                if (wroteFirst)
                    _expression.Append($" {logic} ");

                Visit(filter);
                wroteFirst = true;
            }
            _expression.Append(")");

            return true;
        }
    }
}
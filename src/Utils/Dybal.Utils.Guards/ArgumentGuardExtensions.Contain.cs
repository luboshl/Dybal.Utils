﻿using System.Runtime.CompilerServices;

namespace Dybal.Utils.Guards;

public static partial class ArgumentGuardExtensions
{
    public static IEnumerable<TArgument> Contain<TArgument>(this IArgumentGuard<IEnumerable<TArgument>> guard, TArgument? value, string? message = null)
    {
        return guard.Contain(item => Equals(item, value), message ?? $"Collection has to contain '{value}'.");
    }

    public static IEnumerable<TArgument> Contain<TArgument>(this IArgumentGuard<IEnumerable<TArgument>> guard, Func<TArgument, bool> filter, string? message = null)
    {
        if (guard.IsActive)
        {
            if (!guard.Argument.Value.Any(filter))
            {
                ThrowHelper.Throw<ArgumentException>(guard.Argument.Name, message ?? "Collection does not contain required item.");
            }
        }

        return guard.Argument.Value;
    }
}
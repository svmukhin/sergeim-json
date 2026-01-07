// SPDX-FileCopyrightText: Copyright (c) [2025-2026] [Sergei Mukhin]
// SPDX-License-Identifier: MIT

using System.Globalization;
using System.Numerics;

namespace SergeiM.Json;

/// <summary>
/// Represents an immutable JSON number value.
/// </summary>
public sealed class JsonNumber : JsonValue
{
    private readonly decimal _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonNumber"/> class.
    /// </summary>
    /// <param name="value">The numeric value.</param>
    public JsonNumber(decimal value)
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonNumber"/> class.
    /// </summary>
    /// <param name="value">The numeric value.</param>
    public JsonNumber(double value)
    {
        _value = (decimal)value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonNumber"/> class.
    /// </summary>
    /// <param name="value">The numeric value.</param>
    public JsonNumber(long value)
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonNumber"/> class.
    /// </summary>
    /// <param name="value">The numeric value.</param>
    public JsonNumber(int value)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the type of this JSON value.
    /// </summary>
    public override JsonValueType ValueType => JsonValueType.Number;

    /// <summary>
    /// Returns the int value of this JSON number.
    /// </summary>
    /// <returns>An int representation of the JSON number.</returns>
    /// <exception cref="OverflowException">If the value is outside the range of int.</exception>
    public int IntValue() => decimal.ToInt32(_value);

    /// <summary>
    /// Returns the long value of this JSON number.
    /// </summary>
    /// <returns>A long representation of the JSON number.</returns>
    /// <exception cref="OverflowException">If the value is outside the range of long.</exception>
    public long LongValue() => decimal.ToInt64(_value);

    /// <summary>
    /// Returns the double value of this JSON number.
    /// </summary>
    /// <returns>A double representation of the JSON number.</returns>
    public double DoubleValue() => decimal.ToDouble(_value);

    /// <summary>
    /// Returns the decimal value of this JSON number.
    /// </summary>
    /// <returns>The decimal value.</returns>
    public decimal DecimalValue() => _value;

    /// <summary>
    /// Returns the BigInteger value of this JSON number.
    /// </summary>
    /// <returns>A BigInteger representation of the JSON number.</returns>
    public BigInteger BigIntegerValue() => (BigInteger)_value;

    /// <summary>
    /// Indicates whether this JSON number is an integral value.
    /// </summary>
    /// <returns>true if the number is integral; otherwise, false.</returns>
    public bool IsIntegral() => _value == decimal.Truncate(_value);

    /// <summary>
    /// Returns a JSON representation of this value.
    /// </summary>
    /// <returns>A JSON string representation of the number.</returns>
    public override string ToString() => _value.ToString(CultureInfo.InvariantCulture);

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is a JsonNumber with the same value; otherwise, false.</returns>
    public override bool Equals(object? obj) =>
        obj is JsonNumber other && _value == other._value;

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Implicitly converts a JsonNumber to an int.
    /// </summary>
    /// <param name="jsonNumber">The JSON number to convert.</param>
    public static implicit operator int(JsonNumber jsonNumber) => jsonNumber.IntValue();

    /// <summary>
    /// Implicitly converts a JsonNumber to a long.
    /// </summary>
    /// <param name="jsonNumber">The JSON number to convert.</param>
    public static implicit operator long(JsonNumber jsonNumber) => jsonNumber.LongValue();

    /// <summary>
    /// Implicitly converts a JsonNumber to a double.
    /// </summary>
    /// <param name="jsonNumber">The JSON number to convert.</param>
    public static implicit operator double(JsonNumber jsonNumber) => jsonNumber.DoubleValue();

    /// <summary>
    /// Implicitly converts a JsonNumber to a decimal.
    /// </summary>
    /// <param name="jsonNumber">The JSON number to convert.</param>
    public static implicit operator decimal(JsonNumber jsonNumber) => jsonNumber._value;
}

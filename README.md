# SergeiM.Json

![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/svmukhin/sergeim-json/build.yml)
![NuGet](https://img.shields.io/nuget/v/SergeiM.Json?color=%230000FF)
[![Hits-of-Code](https://hitsofcode.com/github/svmukhin/sergeim-json)](https://hitsofcode.com/github/svmukhin/sergeim-json/view)
![GitHub License](https://img.shields.io/github/license/svmukhin/sergeim-json)

Immutable JSON library for .NET inspired by Jakarta JSON API (JSR 374).
This library provides a clean, fluent API for working with JSON data
structures using immutable types.

## Features

- **Immutable JSON Types**: All JSON values (objects, arrays, strings, numbers,
booleans, null) are immutable
- **Builder Pattern**: Fluent API for constructing JSON objects and arrays
- **Type-Safe Accessors**: Strongly-typed methods for accessing JSON values with
optional defaults
- **JSON Pointer (RFC 6901)**: Navigate JSON structures using JSON Pointer syntax
- **JSON Patch (RFC 6902)**: Apply modifications to JSON documents with Add,
Remove, Replace, Move, Copy, and Test operations
- **Stream-Based I/O**: Efficient reading and writing of JSON from/to streams
- **Modern .NET**: Built for .NET 8+ with full nullable reference type support

## Installation

```bash
dotnet add package SergeiM.Json
```

## Quick Start

### Creating JSON Objects

```csharp
using SergeiM.Json;

// Using builder pattern
var person = new JsonObjectBuilder()
    .Add("name", "Alice")
    .Add("age", 30)
    .Add("isActive", true)
    .Add("address", new JsonObjectBuilder()
        .Add("city", "New York")
        .Add("zip", "10001"))
    .Build();

// Accessing values
string name = person.GetString("name");           // "Alice"
int age = person.GetInt("age");                   // 30
bool active = person.GetBoolean("isActive");      // true

// With defaults for missing keys
int score = person.GetInt("score", 0);            // 0 (key doesn't exist)
```

### Creating JSON Arrays

```csharp
// Using builder pattern
var numbers = new JsonArrayBuilder()
    .Add(1)
    .Add(2)
    .Add(3)
    .Build();

// Accessing elements
int first = numbers.GetInt(0);                    // 1
int count = numbers.Count;                        // 3

// Iteration
foreach (JsonValue value in numbers)
{
    Console.WriteLine(value);
}
```

### Reading and Writing JSON

```csharp
// Reading from string
var reader = JsonReader.Create(new StringReader(jsonString));
JsonValue value = reader.Read();
JsonObject obj = reader.ReadObject();
JsonArray arr = reader.ReadArray();

// Reading from stream
using var fileStream = File.OpenRead("data.json");
var streamReader = JsonReader.Create(fileStream);
var data = streamReader.ReadObject();

// Writing to string
var writer = JsonWriter.Create(new StringWriter());
writer.Write(person);
string json = stringWriter.ToString();

// Writing with formatting
var options = new JsonWriterOptions { IndentOutput = true };
var prettyWriter = JsonWriter.Create(new StringWriter(), options);
prettyWriter.WriteObject(person);

// Writing Unicode characters without escaping (Cyrillic, Chinese, etc.)
var unicodeWriter = JsonWriter.Create(new StringWriter(), JsonWriterOptions.UnicodeUnescaped);
unicodeWriter.Write(new JsonString("Привет мир")); // Writes: "Привет мир" (not "\u041F...")

// Pretty print with Unicode
var prettyUnicode = JsonWriter.Create(new StringWriter(), JsonWriterOptions.PrettyPrintUnicodeUnescaped);
```

### JSON Pointer (RFC 6901)

```csharp
var doc = new JsonObjectBuilder()
    .Add("users", new JsonArrayBuilder()
        .Add(new JsonObjectBuilder()
            .Add("name", "Alice")
            .Add("email", "alice@example.com")))
    .Build();

// Navigate using JSON Pointer
var pointer = new JsonPointer("/users/0/email");
JsonValue email = pointer.GetValue(doc);          // "alice@example.com"

// Check existence
bool exists = pointer.Contains(doc);              // true

// Safe access
if (pointer.TryGetValue(doc, out var value))
{
    Console.WriteLine(value);
}

// Building pointers
var newPointer = JsonPointer.Empty
    .Append("users")
    .Append("0")
    .Append("name");                              // "/users/0/name"
```

### JSON Patch (RFC 6902)

```csharp
var original = new JsonObjectBuilder()
    .Add("name", "Alice")
    .Add("age", 30)
    .Build();

// Apply patch operations
var patch = new JsonPatch(
    JsonPatchOperation.Add("/email", new JsonString("alice@example.com")),
    JsonPatchOperation.Replace("/age", new JsonNumber(31)),
    JsonPatchOperation.Remove("/name")
);

JsonValue modified = patch.Apply(original);

// Test operation (throws if value doesn't match)
var testPatch = new JsonPatch(
    JsonPatchOperation.Test("/age", new JsonNumber(30)),
    JsonPatchOperation.Add("/verified", JsonValue.True)
);

// Move operation
var movePatch = new JsonPatch(
    JsonPatchOperation.Move("/oldName", "/name")
);

// Copy operation
var copyPatch = new JsonPatch(
    JsonPatchOperation.Copy("/template", "/instance")
);
```

## API Overview

### Core Types

- **`JsonValue`**: Base class for all JSON values
- **`JsonObject`**: Immutable JSON object (key-value pairs)
- **`JsonArray`**: Immutable JSON array (ordered list)
- **`JsonString`**: JSON string value
- **`JsonNumber`**: JSON number value (supports int, long, double, decimal)
- **`JsonBoolean`**: JSON boolean value (true/false)
- **`JsonNull`**: JSON null value (singleton)

### Builders

- **`JsonObjectBuilder`**: Fluent builder for creating JSON objects
- **`JsonArrayBuilder`**: Fluent builder for creating JSON arrays

### I/O

- **`JsonReader`**: Read JSON from streams or TextReader
- **`JsonWriter`**: Write JSON to streams or TextWriter
- **`JsonReaderOptions`**: Configuration for JSON parsing (comments, trailing commas)
- **`JsonWriterOptions`**: Configuration for JSON output (indentation, formatting, Unicode escaping)
  - `Default`: Compact output with Unicode escaping
  - `PrettyPrint`: Indented output with Unicode escaping
  - `UnicodeUnescaped`: Compact output without Unicode escaping (for Cyrillic, Chinese, etc.)
  - `PrettyPrintUnicodeUnescaped`: Indented output without Unicode escaping

### JSON Pointer & Patch

- **`JsonPointer`**: Navigate JSON structures using RFC 6901 pointer syntax
- **`JsonPatch`**: Apply modifications using RFC 6902 patch operations
- **`JsonPatchOperation`**: Individual patch operations (Add, Remove, Replace,
Move, Copy, Test)

## Type Conversions

```csharp
var number = new JsonNumber(42);

int intValue = number.IntValue();
long longValue = number.LongValue();
double doubleValue = number.DoubleValue();
decimal decimalValue = number.DecimalValue();
```

## Immutability

All JSON types are immutable. Operations that appear to modify a JSON structure
actually return a new instance:

```csharp
var original = new JsonObjectBuilder()
    .Add("x", 10)
    .Build();

// This creates a NEW JsonObject, original is unchanged
var modified = Json.CreateObjectBuilder(original)
    .Add("y", 20)
    .Build();

Console.WriteLine(original.ContainsKey("y"));     // false
Console.WriteLine(modified.ContainsKey("y"));     // true
```

## Error Handling

```csharp
// Accessing non-existent keys
try 
{
    int value = obj.GetInt("missing");            // Throws KeyNotFoundException
}
catch (KeyNotFoundException) { }

// Use defaults to avoid exceptions
int value = obj.GetInt("missing", 0);             // Returns 0

// Invalid JSON
try
{
    var reader = JsonReader.Create(new StringReader("invalid"));
    var value = reader.Read();                    // Throws JsonException
}
catch (JsonException ex) { }

// Failed patch operations
try
{
    var patch = new JsonPatch(
        JsonPatchOperation.Test("/value", new JsonNumber(99))
    );
    patch.Apply(doc);               // Throws JsonException if test fails
}
catch (JsonException ex) { }
```

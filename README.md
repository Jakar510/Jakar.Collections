# Jakar.Collections

Observable, concurrent and pooled collections: ObservableCollection, ConcurrentObservableCollection, ObservableDictionary, ObservableHashSet, ArrayBuffer, PooledArray and the buffer types.

```sh
dotnet add package Jakar.Collections
```

It depends on [Jakar.Core](https://www.nuget.org/packages/Jakar.Core/) and [Jakar.Logging](https://www.nuget.org/packages/Jakar.Logging/); the types are in the `Jakar.Extensions` namespace.

## What's in it

Everything that was under `Jakar.Extensions/Collections`: the observable and concurrent collections, `CollectionAlerts`, the pooled buffers (`ArrayBuffer<T>`, `Buffer<T>`) and collection helpers. The types keep the `Jakar.Extensions` namespace.


## Versioning

Versions are `<.NET major>.<library major>.<library minor>`: a breaking change bumps the middle number (`10.1.0` → `10.2.0`), anything else the last one.

## Repository

| Path | What it is |
|---|---|
| `Jakar.Collections/` | The package. |
| `Jakar.Collections.Tests/` | NUnit tests. |
| `.github/workflows/ci.yml` | Build and test on Ubuntu and Windows, for every push and pull request. |
| `.github/workflows/publish.yml` | On `master`: CI, then pack and push to nuget.org (Trusted Publishing). |
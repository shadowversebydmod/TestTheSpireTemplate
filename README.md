A template for a Slay the Spire 2 character mod with BaseLib as a dependency.

See the [wiki](https://github.com/Alchyr/ModTemplate-StS2/wiki) to get started.

## License

This template is licensed under the GNU Affero General Public License v3.0. See [LICENSE](LICENSE).

## Testing

`CharMod.Tests` uses the `TestTheSpire` NuGet package. Run the MSBuild target directly; no `build.sh`, `install.sh`, or `run.sh` wrapper is needed.

Before running tests, keep the STS2 `mods/` directory free of other `TestTheSpire` test mods. STS2 loads every enabled test mod from that directory, and the last initialized test entry can take over the run.

```bash
dotnet msbuild CharMod.Tests/CharMod.Tests.csproj \
  -restore \
  -t:RunSts2Tests \
  -p:Sts2Path=/path/to/SlayTheSpire2
```

Use `CopySts2TestPayload` when you only want to build the test mod payload:

```bash
dotnet msbuild CharMod.Tests/CharMod.Tests.csproj \
  -restore \
  -t:CopySts2TestPayload \
  -p:Sts2Path=/path/to/SlayTheSpire2
```

For a focused run, pass a TestTheSpire filter:

```bash
dotnet msbuild CharMod.Tests/CharMod.Tests.csproj \
  -restore \
  -t:RunSts2Tests \
  -p:Sts2Path=/path/to/SlayTheSpire2 \
  -p:Sts2TestArgs=--sts2-test-filter=Sample_enhance
```

## Local Reference Sources

The `local/` directory keeps source material that helps an AI or developer inspect real APIs before changing cards:

- `local/BaseLib-StS2/`: submodule of `https://github.com/Alchyr/BaseLib-StS2` checked out at the tag matching `Alchyr.Sts2.BaseLib`.
- `local/MinionLib/`: submodule of `https://github.com/FuYnAloft/MinionLib` checked out at the tag matching `FuYnAloft.Sts2.MinionLib`.
- `local/sts2-decompiled/`: ignored output from decompiling `sts2.dll` with `ilspycmd`.

`CharMod.csproj` excludes `local/**` from compile, content, resource, and none items. Commit the submodule pointers; keep generated decompiled sources out of git.

Regenerate the STS2 decompiled sources through MSBuild. The target uses `Sts2PathDiscovery.props` to infer the OS-specific data directory, then decompiles `$(Sts2DataDir)/sts2.dll`.

```bash
dotnet msbuild CharMod.csproj -t:DecompileSts2
```

If STS2 is not in a standard Steam location, pass the game root explicitly:

```bash
dotnet msbuild CharMod.csproj -t:DecompileSts2 -p:Sts2Path=/path/to/SlayTheSpire2
```

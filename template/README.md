# [[[project_name]]]

[[[mod_description]]]

This repository was generated from `Gluck-House/7dtd-mod-template` and currently mirrors the same layout, build scripts, and CI conventions used by `7dtdTimeLoop`.

## Repository layout

- `[[[project_name]]]/`: solution, project file, source code, and packaged mod resources
- `deps/`: local-only 7 Days to Die assemblies used for compilation
- `scripts/`: helpers for checking and downloading pinned 7DTD dependencies
- `.github/7dtd-version.env`: the pinned 7DTD build used by CI
- `build.sh`: the standard local build entry point

## Building

Requirements:

- .NET SDK 8.0 or greater
- a copy of the 7 Days to Die dedicated server assemblies

The project expects these local game references in `deps/`:

- `0Harmony.dll`
- `Assembly-CSharp.dll`
- `LogLibrary.dll`
- `UnityEngine.dll`
- `UnityEngine.CoreModule.dll`

See [deps/README.md](deps/README.md) for the supported ways to populate that folder.

From the repo root:

```bash
./build.sh
```

If you want to invoke MSBuild directly instead:

```bash
dotnet build [[[project_name]]]/[[[project_name]]].sln
```

The local build output is written to `[[[project_name]]]/build/[[[project_name]]]/`.

## CI behavior

CI pins the expected game dependency set using `.github/7dtd-version.env`.

- `build.yml` restores or downloads the matching dedicated server assemblies, builds the project, and uploads a ready-to-install artifact containing a top-level `[[[command_prefix_long]]]/` folder.
- `update-7dtd-build.yml` checks whether the pinned `BUILD_ID` is stale and opens a PR with the updated version pin when Steam publishes a new build for the configured branch.

To check the current pin manually, run:

```bash
./scripts/check_7dtd_build.sh
```

## Installation

1. Build the mod locally or download a CI/release artifact.
2. Copy the `[[[command_prefix_long]]]/` folder into your server `Mods/` directory.
3. Start the server once so the default config file can be created.
4. Edit `[[[config_stem]]].xml` inside the mod folder if you need to change defaults.

## License

This repository is licensed under the terms in [LICENSE](LICENSE).

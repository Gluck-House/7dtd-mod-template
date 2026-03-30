# [[[project_name]]]

[[[mod_description]]]

This repository was generated from `Gluck-House/7dtd-mod-template`.

## Repository layout

- `[[[project_name]]]/`: solution, project file, source code, and packaged mod resources
- `[[[project_name]]]/src/`: the mod source tree
- `deps/`: local-only 7 Days to Die assemblies used for compilation
- `scripts/`: local developer helpers such as downloading pinned 7DTD dependencies
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

- `build.yml` downloads the matching shared dependency bundle for the pinned build, builds the project, and uploads a ready-to-install artifact containing a top-level `[[[artifact_folder]]]/` folder.
- the normal pinned-build update loop runs centrally from `7dtd-mod-infra`, which publishes the matching dependency bundle and opens a PR when `.github/7dtd-version.env` should move forward

## Installation

1. Build the mod locally or download a CI/release artifact.
2. Copy the `[[[artifact_folder]]]/` folder into your server `Mods/` directory.
3. Add whatever additional source, resources, and runtime configuration your mod needs.

## License

This repository is licensed under the terms in [LICENSE](LICENSE).

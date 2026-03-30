# Contributing

## Local setup

1. Populate `deps/` with the required 7 Days to Die assemblies.
2. Run `./build.sh` from the repository root.
3. Test the resulting mod from `[[[project_name]]]/build/[[[project_name]]]/`.

To download the required assemblies automatically:

```bash
STEAMCMD_MODE=docker ./scripts/download_7dtd_server.sh
```

## Notes

- `deps/`, `.cache/`, `.tools/`, and `[[[project_name]]]/build/` are local-only and ignored by git.
- CI uploads an artifact with a top-level `[[[command_prefix_long]]]/` folder for direct use in `Mods/`.
- Keep changes portable. Avoid absolute filesystem paths and machine-specific assumptions in docs or scripts.
- The project currently builds with .NET SDK 8 while targeting `netstandard2.1` for the game-facing assembly.
- This repository was generated from `Gluck-House/7dtd-mod-template` and currently mirrors the `7dtd-timeloop` repository layout.

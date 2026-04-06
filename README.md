# 7DTD Mod Template

This repository contains the Copier template used to create and maintain Gluck House 7 Days to Die mod repositories.

The goal is to keep each mod in its own standalone repository while still giving every repo the same baseline structure, build conventions, and CI integration points.

## Purpose

Each generated mod repository should:

- remain independent, so forks and upstream tracking stay simple
- follow the same layout for source, packaging, and build assets
- use the same conventions for acquiring 7DTD build dependencies
- consume shared CI workflows without copying large amounts of workflow logic into every repo
- stay updateable over time by reapplying template changes with Copier

This repository is not the place where shared build logic should live long term. The template should define the shape of a mod repository and its default files, while reusable GitHub workflows and actions should live in a separate infrastructure repository.

## Current State

The template now aims to be generic at the repository level.

That means the generated repository currently includes:

- thin repo-local workflow wrappers that call reusable workflows in `7dtd-mod-infra`
- a repo-local script for downloading local developer dependencies
- a minimal C# mod starter rather than a prebuilt gameplay architecture

The next phase is to keep moving more shared automation into `7dtd-mod-infra` while preserving a stable, Copier-managed downstream repo shape.

## Usage

Use UV to run Copier:

```bash
uvx copier copy . ../my-mod
```

To generate a repository using the current defaults without interactive prompts:

```bash
uvx copier copy --defaults . ../my-mod
```

## Planned Template Output

The template currently generates a repository with a structure close to this:

```text
.
├── .github/
│   ├── workflows/
│   │   └── build.yml
│   └── 7dtd-version.env
├── deps/
│   └── README.md
├── scripts/
│   └── download_7dtd_server.sh
├── <ModName>/
│   ├── <ModName>.csproj
│   ├── <ModName>.sln
│   ├── resources/
│   └── src/
├── .gitignore
├── build.sh
├── CONTRIBUTING.md
├── LICENSE
└── README.md
```

The exact contents will vary by mod, but the high-level layout should stay consistent across repositories.

## Template Structure

This repository now contains:

- `copier.yml` for template questions and defaults
- `template/` for the files copied into each generated mod repository
- a generic repo scaffold with workflow wrappers pointing at `7dtd-mod-infra`
- `.github/workflows/pr-title.yml` to enforce Conventional Commit PR titles
- `.github/workflows/release-please.yml` to manage template release PRs and tags
- `.github/release-please-manifest.json` as the template release version source of truth

The generated repositories are expected to separate responsibilities like this:

- `.github/7dtd-version.env`: the repo-local pin for the target 7DTD build
- `.github/workflows/`: thin workflow wrappers that call shared reusable workflows
- `deps/`: local-only game DLL references, not committed to git
- `scripts/`: local developer helpers for downloading server assemblies
- `<ModName>/`: the actual mod project, source code, and packaged resources
- `build.sh`: the standard local build entry point
- `<ModName>/resources/ModInfo.xml`: bootstrapped by the template, then owned by the consuming repo

## Design Principles

- One mod, one repository.
- Shared shape comes from Copier.
- Shared CI behavior comes from reusable workflows, not template duplication.
- The template should stay generic at the mod-source level and avoid imposing one gameplay architecture.
- The pinned 7DTD version stays in each mod repository so upgrades can be rolled out per mod.
- Copier bootstraps `.github/7dtd-version.env`, but future `copier update` runs should not overwrite that file.
- Copier bootstraps `ModInfo.xml`, but future `copier update` runs should not overwrite it, so mod versioning stays repo-owned.
- Game DLLs should be downloaded or cached for builds rather than committed into source control unless redistribution has been explicitly cleared.

## Template Releases

Template releases are managed in this repository with `release-please`.

- Pull requests into `main` should use Conventional Commit titles because squash merges drive the release notes and version bump.
- Each merge to `main` updates a long-running release PR for the template.
- Merging that release PR creates the next `v*` tag and GitHub release for the template repository.
- `7dtd-mod-infra` can continue pinning downstream Copier updates to those released template tags.

## Status

The template now provides a generic 7DTD mod repository scaffold with a minimal source starter and shared CI wrappers. Future iterations should continue extracting shared workflow logic into `7dtd-mod-infra` while keeping the generated repo itself lightweight and reusable.

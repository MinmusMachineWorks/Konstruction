> **Minmus Machine Works fork.** This is a fork of [UmbraSpaceIndustries/Konstruction](https://github.com/UmbraSpaceIndustries/Konstruction)
> with community fixes. Install it through CKAN as `Konstruction-MM`, after adding the
> [Minmus Machine Works repository](https://github.com/MinmusMachineWorks/KSP-Mods-CKAN). It replaces the
> official `Konstruction` package. Releases are built by `.github/workflows/release.yml` from
> `v*.*.*` tags. CKAN metadata lives in `ckan/metadata.json`.
>
> **Cargo volumes.** Upstream's `Patches/EVAConstructionTweaks.cfg` gave every part without a stock
> cargo module one, sized by mass (1 t = 3600 L). This fork drops it and depends on
> [KSP_PartVolume](https://github.com/linuxgurugamer/KSP_PartVolume) instead, which sizes parts by their model's
> bounds and makes the ones it skips movable in EVA construction but not storable. CKAN installs it; for a
> manual install, add it yourself.
>
> **Building.** The projects use [KSPBuildTools](https://github.com/KSPModdingLibs/KSPBuildTools) and
> build with `dotnet build Source/Konstruction.sln`. Both DLLs land in
> `FOR_RELEASE/GameData/UmbraSpaceIndustries/Konstruction/` and aren't committed. KSPBuildTools
> finds a Steam KSP install by itself. It also needs USITools in `GameData/000_USITools/` of that
> install. If your mods live elsewhere, create a gitignored `Directory.Build.props.user` that sets
> `KSPBT_GameRoot` (the KSP folder with USITools installed) and, if needed, `KSPBT_ManagedPath`.

Konstruction
===

This is the repository for the USI Konstruction mod for [Kerbal Space Program](http://kerbalspaceprogram.com)

You can find the official repo for this mod on [Github](https://github.com/UmbraSpaceIndustries/Konstruction).  Releases, along with other USI products, can be found on [the USI Catalog Page](https://umbraspaceindustries.github.io/UmbraSpaceIndustries/).

### Download Link

You can download the current version of MKS from [the USI Catalog Page](https://umbraspaceindustries.github.io/UmbraSpaceIndustries/).  Note that for the sake of my own sanity, only the current release version is supported.

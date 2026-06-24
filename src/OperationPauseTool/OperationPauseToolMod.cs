using System;
using Mafi;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Game;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;

namespace OperationPauseTool;

/// <summary>
/// Mod entry point. Registers the operation-only pause cursor tool via DI.
/// </summary>
public sealed class OperationPauseToolMod : IMod, IDisposable
{
	private readonly ModManifest m_manifest;
	private readonly ModJsonConfig m_jsonConfig;

	public OperationPauseToolMod(ModManifest manifest)
	{
		m_manifest = manifest;
		m_jsonConfig = new ModJsonConfig(this);
		Log.Info("OperationPauseTool: loaded");
	}

	public ModManifest Manifest => m_manifest;

	public bool IsUiOnly => false;

	public Option<IConfig> ModConfig { get; set; } = Option<IConfig>.None;

	public ModJsonConfig JsonConfig => m_jsonConfig;

	public void RegisterPrototypes(ProtoRegistrator registrator) { }

	public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool wasLoaded)
	{
		depBuilder.RegisterAllGlobalDependencies(typeof(OperationPauseToolMod).Assembly);
	}

	public void EarlyInit(DependencyResolver resolver) { }

	public void Initialize(DependencyResolver resolver, bool gameWasLoaded) { }

	public void MigrateJsonConfig(VersionSlim savedVersion, Dict<string, object> savedValues) { }

	public void Dispose() { }
}

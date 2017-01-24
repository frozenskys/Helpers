

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var artifacts = MakeAbsolute(Directory(Argument("artifactPath", "./artifacts")));
var buildFolder = MakeAbsolute(Directory(Argument("buildFolder", "./build"))).ToString();

///////////////////////////////////////////////////////////////////////////////
// USER TASKS
// PUT ALL YOUR BUILD GOODNESS IN HERE
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
	{
		CleanDirectory(artifacts);
		CleanDirectory(buildFolder);
	});

Task("Default")
    .IsDependentOn("Package")
    .Does(() => 
	{
	});

	Task("Restore")
	.IsDependentOn("Clean")
	.Does(() =>
	{
		NuGetRestore("./Frozenskys.Helpers.sln");
	});

Task("Build")
	.IsDependentOn("Restore")
	.Does(() =>
	{
		MSBuild("./Frozenskys.Helpers.sln", settings => settings
				.WithProperty("TreatWarningsAsErrors","true")
				.WithProperty("UseSharedCompilation", "false")
				.WithProperty("AutoParameterizationWebConfigConnectionStrings", "false")
				.WithProperty("OutDir", buildFolder)
				.SetVerbosity(Verbosity.Minimal)
				.SetConfiguration(configuration)
				.WithTarget("Rebuild"));
	});

Task("Package")
	.IsDependentOn("Build")
	.Does(() =>
	{
		var nuGetPackSettings   = new NuGetPackSettings {
                                     Id                      = "Frozenskys.Helpers",
                                     Version                 = "0.0.2.0",
                                     Title                   = "Helpers for .NET Applications",
                                     Authors                 = new[] {"Richard Cooper"},
                                     Description             = "Contains helpers to make writing .NET applications easier",
                                     ProjectUrl              = new Uri("https://github.com/frozenskys/helpers/"),
                                     LicenseUrl              = new Uri("https://github.com/frozenskys/helpers/blob/master/LICENSE.md"),
                                     Copyright               = "Frozenskys Software Ltd. 2017",
                                     RequireLicenseAcceptance= false,
                                     Symbols                 = false,
                                     NoPackageAnalysis       = true,
									 Files                   = new [] {
                                                                          new NuSpecContent {Source = "Frozenskys.Helpers.dll", Target = "bin"},
                                                                       },
                                     BasePath                = buildFolder,
                                     OutputDirectory         = artifacts
                                 };

     NuGetPack(nuGetPackSettings);
	});

RunTarget(target);
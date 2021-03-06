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

Task("Test")
	.IsDependentOn("Build")
	.Does(() => 
	{
		//MSTest("./build/*.UnitTests.dll", new MSTestSettings(){ToolPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\IDE\MSTest.exe" });
	});

Task("Package")
	.IsDependentOn("Test")
	.Does(() =>
	{
		var nuGetPackSettings   = new NuGetPackSettings {
                                     Id                      = "Frozenskys.Helpers",
                                     Version                 = "0.3.1.0",
                                     Title                   = "Helpers for .NET Applications",
                                     Authors                 = new[] {"Richard Cooper"},
                                     Description             = "Contains helpers to make writing .NET applications easier",
                                     ProjectUrl              = new Uri("https://github.com/frozenskys/helpers/"),
                                     LicenseUrl              = new Uri("https://frozenskys.mit-license.org/"),
                                     Copyright               = "Frozenskys Software Ltd. 2017",
                                     RequireLicenseAcceptance= false,
                                     Symbols                 = false,
                                     NoPackageAnalysis       = true,
									 Dependencies            = new [] {
                                                                          new NuSpecDependency {Id="System.IO.Abstractions", Version="2.0.0.140", TargetFramework="net452"},
                                                                       },
									 Files                   = new [] {
                                                                          new NuSpecContent {Source = "Frozenskys.Helpers.dll", Target = "lib"},
                                                                       },
                                     BasePath                = buildFolder,
                                     OutputDirectory         = artifacts
                                 };

     NuGetPack(nuGetPackSettings);
	});

RunTarget(target);
#addin "Cake.Compression"

var target = Argument("target", "Default");

Task("Restore")
  .Does(() =>
{
    DotNetCoreRestore("src/");
});

Task("Build")
    .IsDependentOn("Restore")
  .Does(() =>
{
    DotNetCoreBuild("src/**/project.json");
});

Task("Publish")
    .IsDependentOn("Build")
  .Does(() =>
{
    var settings = new DotNetCorePublishSettings
    {
        Framework = "netcoreapp1.0",
        Configuration = "Release",
        OutputDirectory = "./publish/"
    };
                
    DotNetCorePublish("src/Narochno.Lambda.Slack", settings);

    Zip("./publish", "publish.zip");
});

Task("Default")
    .IsDependentOn("Restore")
    .IsDependentOn("Build");

RunTarget(target);
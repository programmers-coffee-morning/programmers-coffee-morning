#r "_lib/Fornax.Core.dll"

open Config
open System.IO

let postPredicate (projectRoot: string, page: string) =
    let fileName = Path.Combine(projectRoot,page)
    let ext = Path.GetExtension page
    if ext = ".md" then
        let ctn = File.ReadAllText fileName
        ctn.Contains("layout: post")
    else
        false

let isStaticPredicate (projectRoot: string, page: string) =
    let ext = Path.GetExtension page
    let pagesIsNotStatic =
           page.Contains "_public" ||
           page.Contains "_bin" ||
           page.Contains "_lib" ||
           page.Contains "_data" ||
           page.Contains "_settings" ||
           page.Contains "_config.yml" ||
           page.Contains ".sass-cache" ||
           page.Contains ".git" ||
           page.Contains ".ionide" ||
           page.Contains "README.md" ||
           ext = ".fsx"
    pagesIsNotStatic |> not

let config = {
    Generators = [
        {Script = "less.fsx"; Trigger = OnFileExt ".less"; OutputFile = ChangeExtension "css" }
        {Script = "sass.fsx"; Trigger = OnFileExt ".scss"; OutputFile = ChangeExtension "css" }
        {Script = "post.fsx"; Trigger = OnFilePredicate postPredicate; OutputFile = ChangeExtension "html" }
        {Script = "staticfile.fsx"; Trigger = OnFilePredicate isStaticPredicate; OutputFile = SameFileName }
        {Script = "index.fsx"; Trigger = Once; OutputFile = NewFileName "index.html" }
    ]
}

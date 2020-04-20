#r "../_lib/Fornax.Core.dll"

type SiteInfo = {
    title: string
    description: string
}

let loader (projectRoot: string) (siteContent: SiteContents) =
    let desc = "A virtual coffee morning for software developers - does exactly what it says on the tin."
    siteContent.Add({title = "Programmer's Coffee Morning"; description = desc })

    siteContent

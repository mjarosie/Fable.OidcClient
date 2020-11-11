# Fable.OidcClient [![Nuget](https://img.shields.io/nuget/v/Fable.OidcClient?style=plastic)](https://www.nuget.org/packages/Fable.OidcClient/)

Library to provide OpenID Connect (OIDC) and OAuth2 protocol support for client applications written in F#. It's a Fable binding for [oidc-client](https://github.com/IdentityModel/oidc-client-js) javascript library.

# How to start using this package

In a directory where your project's `.fsproj` file is:

```
dotnet add package Fable.OidcClient
```

In a directory where your project's `package.json` is:

```
npm install oidc-client 
```

# Examples

Repositories showing examples of using `Fable.OidcClient` with:

- Fable application: [FableBrowserClientOpenIdConnect](https://github.com/mjarosie/FableBrowserClientOpenIdConnect) (based on IdentityServer "[Adding a JavaScript client](https://identityserver4.readthedocs.io/en/latest/quickstarts/4_javascript_client.html#add-your-html-and-javascript-files)" tutorial)
- Elmish single page application: [ElmishSpaOpenIdConnect](https://github.com/mjarosie/ElmishSpaOpenIdConnect)

## Initialise the User Manager

```F#
open Fable.OidcClient
open Fable.Core.JsInterop // Required for the "!!" operator.

let settings: UserManagerSettings = 
    !!{| 
        authority = Some "https://localhost:5001"
        client_id = Some "js"
        redirect_uri = Some "https://localhost:5003/callback.html"
        response_type = Some "code"
        scope = Some "openid profile scope1"
        post_logout_redirect_uri = Some "https://localhost:5003/index.html"
        
        filterProtocolClaims = Some true
        loadUserInfo = Some true
    |}

let mgr: UserManager = Oidc.UserManager.Create settings

// From here you can call
// mgr.signinRedirect() to redirect to login page,
// mgr.signoutRedirect() to redirect to logout page,
// mgr.getUser() to retrieve user's details,
// etc...
```

## Handle the OpenId Connect redirect protocol

```F#
open Fable.OidcClient
open Fable.Core.JsInterop

let mgr: UserManager = Oidc.UserManager.Create !!{| response_mode = Some "query" |}

promise {
    console.log "mgr.signinRedirectCallback()"
    let! user = mgr.signinRedirectCallback()
    console.log (sprintf "%A" user)
    window.location.href <- "index.html"
} |> ignore
```
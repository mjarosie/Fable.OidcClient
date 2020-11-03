namespace Fable.OidcClient

open System
open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop

type Error = Exception

type [<AllowNullLiteral>] Log =
    interface end

type [<AllowNullLiteral>] Logger =
    abstract error: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract info: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract debug: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract warn: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit

type [<AllowNullLiteral>] LogStatic =
    [<Emit "new $0($1...)">] abstract Create: unit -> Log
    abstract NONE: obj
    abstract ERROR: obj
    abstract WARN: obj
    abstract INFO: obj
    abstract DEBUG: obj
    abstract reset: unit -> unit
    abstract level: float with get, set
    abstract logger: Logger with get, set
    abstract debug: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract info: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract warn: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit
    abstract error: ?message: obj * [<ParamArray>] optionalParams: ResizeArray<obj option> -> unit

type [<AllowNullLiteral>] InMemoryWebStorage =
    abstract getItem: key: string -> obj option
    abstract setItem: key: string * value: obj option -> obj option
    abstract removeItem: key: string -> obj option
    abstract key: index: float -> obj option
    abstract length: float option with get, set

type [<AllowNullLiteral>] InMemoryWebStorageStatic =
    [<Emit "new $0($1...)">] abstract Create: unit -> InMemoryWebStorage

type [<AllowNullLiteral>] OidcMetadata =
    abstract issuer: string with get, set
    abstract authorization_endpoint: string with get, set
    abstract token_endpoint: string with get, set
    abstract token_endpoint_auth_methods_supported: ResizeArray<string> with get, set
    abstract token_endpoint_auth_signing_alg_values_supported: ResizeArray<string> with get, set
    abstract userinfo_endpoint: string with get, set
    abstract check_session_iframe: string with get, set
    abstract end_session_endpoint: string with get, set
    abstract jwks_uri: string with get, set
    abstract registration_endpoint: string with get, set
    abstract scopes_supported: ResizeArray<string> with get, set
    abstract response_types_supported: ResizeArray<string> with get, set
    abstract acr_values_supported: ResizeArray<string> with get, set
    abstract subject_types_supported: ResizeArray<string> with get, set
    abstract userinfo_signing_alg_values_supported: ResizeArray<string> with get, set
    abstract userinfo_encryption_alg_values_supported: ResizeArray<string> with get, set
    abstract userinfo_encryption_enc_values_supported: ResizeArray<string> with get, set
    abstract id_token_signing_alg_values_supported: ResizeArray<string> with get, set
    abstract id_token_encryption_alg_values_supported: ResizeArray<string> with get, set
    abstract id_token_encryption_enc_values_supported: ResizeArray<string> with get, set
    abstract request_object_signing_alg_values_supported: ResizeArray<string> with get, set
    abstract display_values_supported: ResizeArray<string> with get, set
    abstract claim_types_supported: ResizeArray<string> with get, set
    abstract claims_supported: ResizeArray<string> with get, set
    abstract claims_parameter_supported: bool with get, set
    abstract service_documentation: string with get, set
    abstract ui_locales_supported: ResizeArray<string> with get, set
    abstract revocation_endpoint: string with get, set
    abstract introspection_endpoint: string with get, set
    abstract frontchannel_logout_supported: bool with get, set
    abstract frontchannel_logout_session_supported: bool with get, set
    abstract backchannel_logout_supported: bool with get, set
    abstract backchannel_logout_session_supported: bool with get, set
    abstract grant_types_supported: ResizeArray<string> with get, set
    abstract response_modes_supported: ResizeArray<string> with get, set
    abstract code_challenge_methods_supported: ResizeArray<string> with get, set

type [<AllowNullLiteral>] MetadataService =
    abstract metadataUrl: string option with get, set
    abstract getMetadata: unit -> Promise<OidcMetadata>
    abstract getIssuer: unit -> Promise<string>
    abstract getAuthorizationEndpoint: unit -> Promise<string>
    abstract getUserInfoEndpoint: unit -> Promise<string>
    abstract getTokenEndpoint: unit -> Promise<string option>
    abstract getCheckSessionIframe: unit -> Promise<string option>
    abstract getEndSessionEndpoint: unit -> Promise<string option>
    abstract getRevocationEndpoint: unit -> Promise<string option>
    abstract getKeysEndpoint: unit -> Promise<string option>
    abstract getSigningKeys: unit -> Promise<ResizeArray<obj option>>

type [<AllowNullLiteral>] SigninRequest =
    abstract url: string with get, set
    abstract state: obj option with get, set

type [<AllowNullLiteral>] SignoutRequest =
    abstract url: string with get, set
    abstract state: obj option with get, set

type [<AllowNullLiteral>] StateStore =
    abstract set: key: string * value: obj option -> Promise<unit>
    abstract get: key: string -> Promise<obj option>
    abstract remove: key: string -> Promise<obj option>
    abstract getAllKeys: unit -> Promise<ResizeArray<string>>

type [<AllowNullLiteral>] OidcClientSettings =
    /// The URL of the OIDC/OAuth2 provider
    abstract authority: string option with get, set
    abstract metadataUrl: string option
    /// Provide metadata when authority server does not allow CORS on the metadata endpoint
    abstract metadata: obj option with get, set
    /// Provide signingKeys when authority server does not allow CORS on the jwks uri
    abstract signingKeys: ResizeArray<obj option> option with get, set
    /// Your client application's identifier as registered with the OIDC/OAuth2
    abstract client_id: string option with get, set
    abstract client_secret: string option with get, set
    /// The type of response desired from the OIDC/OAuth2 provider (default: 'id_token')
    abstract response_type: string option
    abstract response_mode: string option
    /// The scope being requested from the OIDC/OAuth2 provider (default: 'openid')
    abstract scope: string option
    /// The redirect URI of your client application to receive a response from the OIDC/OAuth2 provider
    abstract redirect_uri: string option
    /// The OIDC/OAuth2 post-logout redirect URI
    abstract post_logout_redirect_uri: string option
    /// The OIDC/OAuth2 post-logout redirect URI when using popup
    abstract popup_post_logout_redirect_uri: string option
    abstract prompt: string option
    abstract display: string option
    abstract max_age: float option
    abstract ui_locales: string option
    abstract acr_values: string option
    /// Should OIDC protocol claims be removed from profile (default: true)
    abstract filterProtocolClaims: bool option
    /// Flag to control if additional identity data is loaded from the user info endpoint in order to populate the user's profile (default: true)
    abstract loadUserInfo: bool option
    /// Number (in seconds) indicating the age of state entries in storage for authorize requests that are considered abandoned and thus can be cleaned up (default: 300)
    abstract staleStateAge: float option
    /// The window of time (in seconds) to allow the current time to deviate when validating id_token's iat, nbf, and exp values (default: 300)
    abstract clockSkew: float option
    abstract stateStore: StateStore option
    abstract userInfoJwtIssuer: U2<string, string> option
    // abstract ResponseValidatorCtor: ResponseValidatorCtor option with get, set
    // abstract MetadataServiceCtor: MetadataServiceCtor option with get, set
    /// An object containing additional query string parameters to be including in the authorization request
    abstract extraQueryParams: Map<string, obj option> option with get, set

type [<AllowNullLiteral>] SigninResponse =
    abstract access_token: string with get, set
    abstract code: string with get, set
    abstract error: string with get, set
    abstract error_description: string with get, set
    abstract error_uri: string with get, set
    abstract id_token: string with get, set
    abstract profile: obj option with get, set
    abstract scope: string with get, set
    abstract session_state: string with get, set
    abstract state: obj option with get, set
    abstract token_type: string with get, set
    abstract expired: bool option
    abstract expires_in: float option
    abstract isOpenIdConnect: bool
    abstract scopes: ResizeArray<string>

type [<AllowNullLiteral>] SigninResponseStatic =
    [<Emit "new $0($1...)">] abstract Create: url: string * ?delimiter: string -> SigninResponse

type [<AllowNullLiteral>] SignoutResponse =
    abstract error: string option with get, set
    abstract error_description: string option with get, set
    abstract error_uri: string option with get, set
    abstract state: obj option with get, set

type [<AllowNullLiteral>] SignoutResponseStatic =
    [<Emit "new $0($1...)">] abstract Create: url: string -> SignoutResponse

type [<AllowNullLiteral>] OidcClient =
    abstract settings: OidcClientSettings
    abstract createSigninRequest: ?args: obj -> Promise<SigninRequest>
    abstract processSigninResponse: ?url: string * ?stateStore: StateStore -> Promise<SigninResponse>
    abstract createSignoutRequest: ?args: obj -> Promise<SignoutRequest>
    abstract processSignoutResponse: ?url: string * ?stateStore: StateStore -> Promise<SignoutResponse>
    abstract clearStaleState: stateStore: StateStore -> Promise<obj option>
    abstract metadataService: MetadataService

type [<AllowNullLiteral>] OidcClientStatic =
    [<Emit "new $0($1...)">] abstract Create: settings: OidcClientSettings -> OidcClient

type [<AllowNullLiteral>] MetadataServiceStatic =
    [<Emit "new $0($1...)">] abstract Create: settings: OidcClientSettings -> MetadataService

type [<AllowNullLiteral>] MetadataServiceCtor =
    [<Emit "$0($1...)">] abstract Invoke: settings: OidcClientSettings * ?jsonServiceCtor: obj -> MetadataService

type [<AllowNullLiteral>] ResponseValidator =
    abstract validateSigninResponse: state: obj option * response: obj option -> Promise<SigninResponse>
    abstract validateSignoutResponse: state: obj option * response: obj option -> Promise<SignoutResponse>

type [<AllowNullLiteral>] ResponseValidatorCtor =
    [<Emit "$0($1...)">] abstract Invoke: settings: OidcClientSettings * ?metadataServiceCtor: MetadataServiceCtor * ?userInfoServiceCtor: obj -> ResponseValidator

type [<AllowNullLiteral>] OidcAddress =
    /// Full mailing address, formatted for display or use on a mailing label
    abstract formatted: string option with get, set
    /// Full street address component, which MAY include house number, street name, Post Office Box, and multi-line extended street address information
    abstract street_address: string option with get, set
    /// City or locality component
    abstract locality: string option with get, set
    /// State, province, prefecture, or region component
    abstract region: string option with get, set
    /// Zip code or postal code component
    abstract postal_code: string option with get, set
    /// Country name component
    abstract country: string option with get, set

type [<AllowNullLiteral>] Profile =
    abstract iss: string with get, set
    abstract sub: string with get, set
    abstract aud: string with get, set
    abstract exp: int with get, set
    abstract iat: int with get, set
    abstract auth_time: int option with get, set
    abstract nonce: int option with get, set
    abstract at_hash: string option with get, set
    abstract acr: string option with get, set
    abstract amr: string list option with get, set
    abstract azp: string option with get, set
    abstract sid: string option with get, set
    abstract name: string option with get, set
    abstract given_name: string option with get, set
    abstract family_name: string option with get, set
    abstract middle_name: string option with get, set
    abstract nickname: string option with get, set
    abstract preferred_username: string option with get, set
    abstract profile: string option with get, set
    abstract picture: string option with get, set
    abstract website: string option with get, set
    abstract email: string option with get, set
    abstract email_verified: bool option with get, set
    abstract gender: string option with get, set
    abstract birthdate: string option with get, set
    abstract zoneinfo: string option with get, set
    abstract locale: string option with get, set
    abstract phone_number: string option with get, set
    abstract phone_number_verified: bool option with get, set
    abstract address: OidcAddress option with get, set
    abstract updated_at: int option with get, set

type [<AllowNullLiteral>] UserSettings =
    abstract id_token: string with get, set
    abstract session_state: string with get, set
    abstract access_token: string with get, set
    abstract refresh_token: string with get, set
    abstract token_type: string with get, set
    abstract scope: string with get, set
    abstract profile: Profile with get, set
    abstract expires_at: float with get, set
    abstract state: obj option with get, set

type [<AllowNullLiteral>] User =
    /// The id_token returned from the OIDC provider
    abstract id_token: string with get, set
    /// The session state value returned from the OIDC provider (opaque)
    abstract session_state: string option with get, set
    /// The access token returned from the OIDC provider.
    abstract access_token: string with get, set
    /// Refresh token returned from the OIDC provider (if requested)
    abstract refresh_token: string option with get, set
    /// The token_type returned from the OIDC provider
    abstract token_type: string with get, set
    /// The scope returned from the OIDC provider
    abstract scope: string with get, set
    /// The claims represented by a combination of the id_token and the user info endpoint
    abstract profile: Profile with get, set
    /// The expires at returned from the OIDC provider
    abstract expires_at: float with get, set
    /// The custom state transferred in the last signin
    abstract state: obj option with get, set
    abstract toStorageString: unit -> string
    /// Calculated number of seconds the access token has remaining
    abstract expires_in: float
    /// Calculated value indicating if the access token is expired
    abstract expired: bool
    /// Array representing the parsed values from the scope
    abstract scopes: ResizeArray<string>

type [<AllowNullLiteral>] UserStatic =
    [<Emit "new $0($1...)">] abstract Create: settings: UserSettings -> User
    abstract fromStorageString: storageString: string -> User

type [<AllowNullLiteral>] AccessTokenEvents =
    abstract load: container: User -> unit
    abstract unload: unit -> unit
    /// Subscribe to events raised prior to the access token expiring
    abstract addAccessTokenExpiring: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract removeAccessTokenExpiring: callback: (ResizeArray<obj option> -> unit) -> unit
    /// Subscribe to events raised after the access token has expired
    abstract addAccessTokenExpired: callback: (ResizeArray<obj option> -> unit) -> unit
    abstract removeAccessTokenExpired: callback: (ResizeArray<obj option> -> unit) -> unit

type [<AllowNullLiteral>] WebStorageStateStoreSettings =
    abstract prefix: string option with get, set
    abstract store: obj option with get, set

type [<AllowNullLiteral>] WebStorageStateStore =
    inherit StateStore
    abstract set: key: string * value: obj option -> Promise<unit>
    abstract get: key: string -> Promise<obj option>
    abstract remove: key: string -> Promise<obj option>
    abstract getAllKeys: unit -> Promise<ResizeArray<string>>

type [<AllowNullLiteral>] WebStorageStateStoreStatic =
    [<Emit "new $0($1...)">] abstract Create: settings: WebStorageStateStoreSettings -> WebStorageStateStore

module UserManagerEvents =

    type [<AllowNullLiteral>] UserLoadedCallback =
        [<Emit "$0($1...)">] abstract Invoke: user: User -> unit

    type [<AllowNullLiteral>] UserUnloadedCallback =
        [<Emit "$0($1...)">] abstract Invoke: unit -> unit

    type [<AllowNullLiteral>] SilentRenewErrorCallback =
        [<Emit "$0($1...)">] abstract Invoke: error: Error -> unit

    type [<AllowNullLiteral>] UserSignedOutCallback =
        [<Emit "$0($1...)">] abstract Invoke: unit -> unit

    type [<AllowNullLiteral>] UserSessionChangedCallback =
        [<Emit "$0($1...)">] abstract Invoke: unit -> unit

type [<AllowNullLiteral>] UserManagerEvents =
    inherit AccessTokenEvents
    abstract load: user: User -> obj option
    abstract unload: unit -> obj option
    /// Subscribe to events raised when user session has been established (or re-established)
    abstract addUserLoaded: callback: UserManagerEvents.UserLoadedCallback -> unit
    abstract removeUserLoaded: callback: UserManagerEvents.UserLoadedCallback -> unit
    /// Subscribe to events raised when a user session has been terminated
    abstract addUserUnloaded: callback: UserManagerEvents.UserUnloadedCallback -> unit
    abstract removeUserUnloaded: callback: UserManagerEvents.UserUnloadedCallback -> unit
    /// Subscribe to events raised when the automatic silent renew has failed
    abstract addSilentRenewError: callback: UserManagerEvents.SilentRenewErrorCallback -> unit
    abstract removeSilentRenewError: callback: UserManagerEvents.SilentRenewErrorCallback -> unit
    /// Subscribe to events raised when the user's sign-in status at the OP has changed
    abstract addUserSignedOut: callback: UserManagerEvents.UserSignedOutCallback -> unit
    abstract removeUserSignedOut: callback: UserManagerEvents.UserSignedOutCallback -> unit
    /// When `monitorSession` subscribe to events raised when the user session changed
    abstract addUserSessionChanged: callback: UserManagerEvents.UserSessionChangedCallback -> unit
    abstract removeUserSessionChanged: callback: UserManagerEvents.UserSessionChangedCallback -> unit

type [<AllowNullLiteral>] UserManagerSettings =
    inherit OidcClientSettings
    /// The URL for the page containing the call to signinPopupCallback to handle the callback from the OIDC/OAuth2
    abstract popup_redirect_uri: string option
    /// The features parameter to window.open for the popup signin window.
    /// default: 'location=no,toolbar=no,width=500,height=500,left=100,top=100'
    abstract popupWindowFeatures: string option
    /// The target parameter to window.open for the popup signin window (default: '_blank')
    abstract popupWindowTarget: obj option
    /// The URL for the page containing the code handling the silent renew
    abstract silent_redirect_uri: obj option
    /// Number of milliseconds to wait for the silent renew to return before assuming it has failed or timed out (default: 10000)
    abstract silentRequestTimeout: obj option
    /// Flag to indicate if there should be an automatic attempt to renew the access token prior to its expiration (default: false)
    abstract automaticSilentRenew: bool option
    abstract validateSubOnSilentRenew: bool option
    /// Flag to control if id_token is included as id_token_hint in silent renew calls (default: true)
    abstract includeIdTokenInSilentRenew: bool option
    /// Will raise events for when user has performed a signout at the OP (default: true)
    abstract monitorSession: bool option
    /// Interval, in ms, to check the user's session (default: 2000)
    abstract checkSessionInterval: float option
    abstract query_status_response_type: string option
    abstract stopCheckSessionOnError: bool option
    /// Will invoke the revocation endpoint on signout if there is an access token for the user (default: false)
    abstract revokeAccessTokenOnSignout: bool option
    /// The number of seconds before an access token is to expire to raise the accessTokenExpiring event (default: 60)
    abstract accessTokenExpiringNotificationTime: float option
    abstract redirectNavigator: obj option
    abstract popupNavigator: obj option
    abstract iframeNavigator: obj option
    /// Storage object used to persist User for currently authenticated user (default: session storage)
    abstract userStore: WebStorageStateStore option
    

type [<AllowNullLiteral>] SessionStatus =
    /// Opaque session state used to validate if session changed (monitorSession)
    abstract session_state: string with get, set
    /// Subject identifier
    abstract sub: string with get, set
    /// Session ID
    abstract sid: string option with get, set

type [<AllowNullLiteral>] UserManager =
    inherit OidcClient
    abstract settings: UserManagerSettings
    /// Removes stale state entries in storage for incomplete authorize requests
    abstract clearStaleState: unit -> Promise<unit>
    /// Load the User object for the currently authenticated user
    abstract getUser: unit -> Promise<User option>
    abstract storeUser: user: User -> Promise<unit>
    /// Remove from any storage the currently authenticated user
    abstract removeUser: unit -> Promise<unit>
    /// Trigger a request (via a popup window) to the authorization endpoint. The result of the promise is the authenticated User
    abstract signinPopup: ?args: obj -> Promise<User>
    /// Notify the opening window of response from the authorization endpoint
    abstract signinPopupCallback: ?url: string -> Promise<User option>
    /// Trigger a silent request (via an iframe or refreshtoken if available) to the authorization endpoint
    abstract signinSilent: ?args: obj -> Promise<User>
    /// Notify the parent window of response from the authorization endpoint
    abstract signinSilentCallback: ?url: string -> Promise<User option>
    /// Trigger a redirect of the current window to the authorization endpoint
    abstract signinRedirect: ?args: obj -> Promise<unit>
    /// Process response from the authorization endpoint.
    abstract signinRedirectCallback: ?url: string -> Promise<User>
    /// Trigger a redirect of the current window to the end session endpoint
    abstract signoutRedirect: ?args: obj -> Promise<unit>
    /// Process response from the end session endpoint
    abstract signoutRedirectCallback: ?url: string -> Promise<SignoutResponse>
    /// Trigger a redirect of a popup window window to the end session endpoint
    abstract signoutPopup: ?args: obj -> Promise<unit>
    /// Process response from the end session endpoint from a popup window
    abstract signoutPopupCallback: ?url: string * ?keepOpen: bool -> Promise<unit>
    abstract signoutPopupCallback: ?keepOpen: bool -> Promise<unit>
    /// Proxy to Popup, Redirect and Silent callbacks
    abstract signinCallback: ?url: string -> Promise<User>
    /// Proxy to Popup and Redirect callbacks
    abstract signoutCallback: ?url: string * ?keepWindowOpen: bool -> Promise<U2<SignoutResponse, unit>>
    /// Query OP for user's current signin status
    abstract querySessionStatus: ?args: obj -> Promise<SessionStatus>
    abstract revokeAccessToken: unit -> Promise<unit>
    /// Enables silent renew
    abstract startSilentRenew: unit -> unit
    /// Disables silent renew
    abstract stopSilentRenew: unit -> unit
    abstract events: UserManagerEvents with get, set

type [<AllowNullLiteral>] UserManagerSettingsStatic =
    [<Emit "new $0($1...)">] abstract Create: settings: UserManagerSettings -> UserManager
    
type [<AllowNullLiteral>] UserManagerStatic =
    [<Emit "new $0($1...)">] abstract Create: settings: UserManagerSettings -> UserManager

type [<AllowNullLiteral>] IDTokenClaims =
    /// Issuer Identifier
    abstract iss: string with get, set
    /// Subject identifier
    abstract sub: string with get, set
    /// Audience(s): client_id ...
    abstract aud: string with get, set
    /// Expiration time
    abstract exp: float with get, set
    /// Issued at
    abstract iat: float with get, set
    /// Time when the End-User authentication occurred
    abstract auth_time: float option with get, set
    /// Time when the End-User authentication occurred
    abstract nonce: float option with get, set
    /// Access Token hash value
    abstract at_hash: string option with get, set
    /// Authentication Context Class Reference
    abstract acr: string option with get, set
    /// Authentication Methods References
    abstract amr: ResizeArray<string> option with get, set
    /// Authorized Party - the party to which the ID Token was issued
    abstract azp: string option with get, set
    /// Session ID - String identifier for a Session
    abstract sid: string option with get, set
    /// Other custom claims
    [<Emit "$0[$1]{{=$2}}">] abstract Item: claimKey: string -> obj option with get, set

type [<AllowNullLiteral>] ProfileStandardClaims =
    /// End-User's full name
    abstract name: string option with get, set
    /// Given name(s) or first name(s) of the End-User
    abstract given_name: string option with get, set
    /// Surname(s) or last name(s) of the End-User
    abstract family_name: string option with get, set
    /// Middle name(s) of the End-User
    abstract middle_name: string option with get, set
    /// Casual name of the End-User that may or may not be the same as the given_name.
    abstract nickname: string option with get, set
    /// Shorthand name that the End-User wishes to be referred to at the RP, such as janedoe or j.doe.
    abstract preferred_username: string option with get, set
    /// URL of the End-User's profile page
    abstract profile: string option with get, set
    /// URL of the End-User's profile picture
    abstract picture: string option with get, set
    /// URL of the End-User's Web page or blog
    abstract website: string option with get, set
    /// End-User's preferred e-mail address
    abstract email: string option with get, set
    /// True if the End-User's e-mail address has been verified; otherwise false.
    abstract email_verified: bool option with get, set
    /// End-User's gender. Values defined by this specification are female and male.
    abstract gender: string option with get, set
    /// End-User's birthday, represented as an ISO 8601:2004 [ISO8601‑2004] YYYY-MM-DD format
    abstract birthdate: string option with get, set
    /// String from zoneinfo [zoneinfo] time zone database representing the End-User's time zone.
    abstract zoneinfo: string option with get, set
    /// End-User's locale, represented as a BCP47 [RFC5646] language tag.
    abstract locale: string option with get, set
    /// End-User's preferred telephone number.
    abstract phone_number: string option with get, set
    /// True if the End-User's phone number has been verified; otherwise false.
    abstract phone_number_verified: bool option with get, set
    /// object 	End-User's preferred address in JSON [RFC4627]
    abstract address: OidcAddress option with get, set
    /// Time the End-User's information was last updated.
    abstract updated_at: float option with get, set

type [<AllowNullLiteral>] CordovaPopupWindow =
    abstract navigate: ``params``: obj option -> Promise<obj option>
    abstract promise: Promise<obj option> with get, set

type [<AllowNullLiteral>] CordovaPopupWindowStatic =
    [<Emit "new $0($1...)">] abstract Create: ``params``: obj option -> CordovaPopupWindow

type [<AllowNullLiteral>] CordovaPopupNavigator =
    abstract prepare: ``params``: obj option -> Promise<CordovaPopupWindow>

type [<AllowNullLiteral>] CordovaPopupNavigatorStatic =
    [<Emit "new $0($1...)">] abstract Create: unit -> CordovaPopupNavigator

type [<AllowNullLiteral>] CordovaIFrameNavigator =
    abstract prepare: ``params``: obj option -> Promise<CordovaPopupWindow>

type [<AllowNullLiteral>] CordovaIFrameNavigatorStatic =
    [<Emit "new $0($1...)">] abstract Create: unit -> CordovaIFrameNavigator

type [<AllowNullLiteral>] CheckSessionIFrame =
    abstract load: unit -> Promise<unit>
    abstract start: session_state: string -> unit
    abstract stop: unit -> unit

type [<AllowNullLiteral>] CheckSessionIFrameStatic =
    [<Emit "new $0($1...)">] abstract Create: callback: (unit -> unit) * client_id: string * url: string * ?interval: float * ?stopOnError: bool -> CheckSessionIFrame

type [<AllowNullLiteral>] CheckSessionIFrameCtor =
    [<Emit "$0($1...)">] abstract Invoke: callback: (unit -> unit) * client_id: string * url: string * ?interval: float * ?stopOnError: bool -> CheckSessionIFrame

type [<AllowNullLiteral>] SessionMonitor =
    interface end

type [<AllowNullLiteral>] SessionMonitorStatic =
    [<Emit "new $0($1...)">] abstract Create: userManager: UserManager * CheckSessionIFrameCtor: CheckSessionIFrameCtor -> SessionMonitor

module Oidc =
    let InMemoryWebStorage: InMemoryWebStorageStatic = importMember "oidc-client"
    let Log: LogStatic = importMember "oidc-client"
    let MetadataService: MetadataServiceStatic = importMember "oidc-client"
    let OidcClient: OidcClientStatic = importMember "oidc-client"
    let UserManager: UserManagerStatic = importMember "oidc-client"
    let WebStorageStateStore: WebStorageStateStoreStatic = importMember "oidc-client"
    let SigninResponse: SigninResponseStatic = importMember "oidc-client"
    let SignoutResponse: SignoutResponseStatic = importMember "oidc-client"
    let User: UserStatic = importMember "oidc-client"
    let CordovaPopupWindow: CordovaPopupWindowStatic = importMember "oidc-client"
    let CordovaPopupNavigator: CordovaPopupNavigatorStatic = importMember "oidc-client"
    let CordovaIFrameNavigator: CordovaIFrameNavigatorStatic = importMember "oidc-client"
    let CheckSessionIFrame: CheckSessionIFrameStatic = importMember "oidc-client"
    let SessionMonitor: SessionMonitorStatic = importMember "oidc-client"
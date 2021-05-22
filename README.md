<a name='assembly'></a>
# BarbezDotEu.Provider
An HTTP(S) client that supports rate limiting so that a polite integration with a third-party data provider can be implemented.

## Contents

- [EdgeMockingRequestHeaderCollection](#T-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection')
  - [#ctor(referrer)](#M-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-#ctor-System-String- 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection.#ctor(System.String)')
  - [AcceptHeaders](#P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-AcceptHeaders 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection.AcceptHeaders')
  - [AcceptLanguage](#P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-AcceptLanguage 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection.AcceptLanguage')
  - [CacheControl](#P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-CacheControl 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection.CacheControl')
  - [Connection](#P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-Connection 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection.Connection')
  - [Others](#P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-Others 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection.Others')
  - [Pragma](#P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-Pragma 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection.Pragma')
  - [Referrer](#P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-Referrer 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection.Referrer')
  - [UserAgent](#P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-UserAgent 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection.UserAgent')
  - [Prep(httpRequestMessage)](#M-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-Prep-System-Net-Http-HttpRequestMessage- 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection.Prep(System.Net.Http.HttpRequestMessage)')
- [ICanFail](#T-BarbezDotEu-Provider-Interfaces-ICanFail 'BarbezDotEu.Provider.Interfaces.ICanFail')
  - [FailedResponse](#P-BarbezDotEu-Provider-Interfaces-ICanFail-FailedResponse 'BarbezDotEu.Provider.Interfaces.ICanFail.FailedResponse')
  - [HasFailed](#P-BarbezDotEu-Provider-Interfaces-ICanFail-HasFailed 'BarbezDotEu.Provider.Interfaces.ICanFail.HasFailed')
- [IPoliteProvider](#T-BarbezDotEu-Provider-Interfaces-IPoliteProvider 'BarbezDotEu.Provider.Interfaces.IPoliteProvider')
  - [IsPolite()](#M-BarbezDotEu-Provider-Interfaces-IPoliteProvider-IsPolite 'BarbezDotEu.Provider.Interfaces.IPoliteProvider.IsPolite')
  - [SetMultiplier()](#M-BarbezDotEu-Provider-Interfaces-IPoliteProvider-SetMultiplier-System-Int64- 'BarbezDotEu.Provider.Interfaces.IPoliteProvider.SetMultiplier(System.Int64)')
- [PoliteProvider](#T-BarbezDotEu-Provider-PoliteProvider 'BarbezDotEu.Provider.PoliteProvider')
  - [#ctor(logger,httpClientFactory)](#M-BarbezDotEu-Provider-PoliteProvider-#ctor-Microsoft-Extensions-Logging-ILogger,System-Net-Http-IHttpClientFactory- 'BarbezDotEu.Provider.PoliteProvider.#ctor(Microsoft.Extensions.Logging.ILogger,System.Net.Http.IHttpClientFactory)')
  - [IsPolite()](#M-BarbezDotEu-Provider-PoliteProvider-IsPolite 'BarbezDotEu.Provider.PoliteProvider.IsPolite')
  - [Request\`\`1()](#M-BarbezDotEu-Provider-PoliteProvider-Request``1-System-Net-Http-HttpRequestMessage,System-Boolean,System-Double- 'BarbezDotEu.Provider.PoliteProvider.Request``1(System.Net.Http.HttpRequestMessage,System.Boolean,System.Double)')
  - [SetMultiplier()](#M-BarbezDotEu-Provider-PoliteProvider-SetMultiplier-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetMultiplier(System.Int64)')
  - [SetRateLimitPerDay(callsPerDayString)](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerDay-System-String- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerDay(System.String)')
  - [SetRateLimitPerMinute(callsPerDayString)](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerMinute-System-String- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerMinute(System.String)')
  - [UpdateTimeOfLastCall(lastQueryTime)](#M-BarbezDotEu-Provider-PoliteProvider-UpdateTimeOfLastCall-System-DateTime- 'BarbezDotEu.Provider.PoliteProvider.UpdateTimeOfLastCall(System.DateTime)')
- [PostClientAuthorizeResponse](#T-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse 'BarbezDotEu.Provider.DTO.PostClientAuthorizeResponse')
  - [AccessToken](#P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-AccessToken 'BarbezDotEu.Provider.DTO.PostClientAuthorizeResponse.AccessToken')
  - [FailedResponse](#P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-FailedResponse 'BarbezDotEu.Provider.DTO.PostClientAuthorizeResponse.FailedResponse')
  - [HasFailed](#P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-HasFailed 'BarbezDotEu.Provider.DTO.PostClientAuthorizeResponse.HasFailed')
  - [Scope](#P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-Scope 'BarbezDotEu.Provider.DTO.PostClientAuthorizeResponse.Scope')
  - [TokenType](#P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-TokenType 'BarbezDotEu.Provider.DTO.PostClientAuthorizeResponse.TokenType')

<a name='T-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection'></a>
## EdgeMockingRequestHeaderCollection `type`

##### Namespace

BarbezDotEu.Provider.DTO

##### Summary

Mocks headers that would've been sent typically by Microsoft Edge during the first half of 2021.

##### Remarks

The purpose of this class is to be used by a provider when said provider does not want to look too much like a bot.
How can a bot not look like a bot? By, for example, looking like Edge instead.

Note: The correct MO is to use your application's actual name in the HTTP headers.

<a name='M-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-#ctor-System-String-'></a>
### #ctor(referrer) `constructor`

##### Summary

Constructs a new [EdgeMockingRequestHeaderCollection](#T-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection 'BarbezDotEu.Provider.DTO.EdgeMockingRequestHeaderCollection').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| referrer | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The referrer to set. |

<a name='P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-AcceptHeaders'></a>
### AcceptHeaders `property`

##### Summary

Gets Edge style accept headers.

<a name='P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-AcceptLanguage'></a>
### AcceptLanguage `property`

##### Summary

Gets an Edge style accept header.

<a name='P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-CacheControl'></a>
### CacheControl `property`

##### Summary

Gets an Edge style cache-control header.

<a name='P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-Connection'></a>
### Connection `property`

##### Summary

Gets an Edge style connection header.

<a name='P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-Others'></a>
### Others `property`

##### Summary

Gets a collection of non-standard headers, including:
- Edge style do-not-track header;
- Edge style sec-fetch-site header;
- Edge style sec-fetch-mode header;
- Edge style sec-fetch-dest header.

<a name='P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-Pragma'></a>
### Pragma `property`

##### Summary

Gets an Edge style pragma header.

<a name='P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-Referrer'></a>
### Referrer `property`

##### Summary

Gets an Edge style referrer header.

<a name='P-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-UserAgent'></a>
### UserAgent `property`

##### Summary

Gets an Edge style user agent header.

<a name='M-BarbezDotEu-Provider-DTO-EdgeMockingRequestHeaderCollection-Prep-System-Net-Http-HttpRequestMessage-'></a>
### Prep(httpRequestMessage) `method`

##### Summary

Prepares a given [HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') with headers sent typically by Microsoft Edge during the first half of 2021.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| httpRequestMessage | [System.Net.Http.HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') | The [HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') to adjust. |

<a name='T-BarbezDotEu-Provider-Interfaces-ICanFail'></a>
## ICanFail `type`

##### Namespace

BarbezDotEu.Provider.Interfaces

<a name='P-BarbezDotEu-Provider-Interfaces-ICanFail-FailedResponse'></a>
### FailedResponse `property`

##### Summary

Gets or sets the [HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') that indicates a fault that has occurred.

<a name='P-BarbezDotEu-Provider-Interfaces-ICanFail-HasFailed'></a>
### HasFailed `property`

##### Summary

Gets a value indicating whether the [HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') indicates a fault.

<a name='T-BarbezDotEu-Provider-Interfaces-IPoliteProvider'></a>
## IPoliteProvider `type`

##### Namespace

BarbezDotEu.Provider.Interfaces

##### Summary

Defines an HTTP(S) client that supports rate limiting so that a polite integration
with a third-party data provider can be implemented.

<a name='M-BarbezDotEu-Provider-Interfaces-IPoliteProvider-IsPolite'></a>
### IsPolite() `method`

##### Summary

Returns true if querying using this [IPoliteProvider](#T-BarbezDotEu-Provider-Interfaces-IPoliteProvider 'BarbezDotEu.Provider.Interfaces.IPoliteProvider') will respects the limit set forth by the third-party resource.

##### Parameters

This method has no parameters.

##### Remarks

Any other methods of this [IPoliteProvider](#T-BarbezDotEu-Provider-Interfaces-IPoliteProvider 'BarbezDotEu.Provider.Interfaces.IPoliteProvider') should be called only after this method was called and returned true first, ensuring this application will not be blacklisted or will start receiving errors.

<a name='M-BarbezDotEu-Provider-Interfaces-IPoliteProvider-SetMultiplier-System-Int64-'></a>
### SetMultiplier() `method`

##### Summary

Sets a multiplier intended for cases where multiple calls to the third-party resource have to be performed in rapid succession (a batch).
In such cases, the multiplier should be set to the number of calls that were performed in batch.
The multiplier is reset to 1 after the next call to [IsPolite](#M-BarbezDotEu-Provider-Interfaces-IPoliteProvider-IsPolite 'BarbezDotEu.Provider.Interfaces.IPoliteProvider.IsPolite') returns true.

##### Parameters

This method has no parameters.

##### Remarks

E.g. if ordinary 1 call per minute can be made, but 2 are made in batch, set the multiplier to 2.
This way, the next batch will only be allowed to run in 2 minutes' time, thus still respecting the average rate limit of 1 call per minute.

<a name='T-BarbezDotEu-Provider-PoliteProvider'></a>
## PoliteProvider `type`

##### Namespace

BarbezDotEu.Provider

##### Summary

Implements an HTTP(S) client that supports rate limiting so that a polite integration
with a third-party data provider can be implemented.

<a name='M-BarbezDotEu-Provider-PoliteProvider-#ctor-Microsoft-Extensions-Logging-ILogger,System-Net-Http-IHttpClientFactory-'></a>
### #ctor(logger,httpClientFactory) `constructor`

##### Summary

Constructs a new [PoliteProvider](#T-BarbezDotEu-Provider-PoliteProvider 'BarbezDotEu.Provider.PoliteProvider').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| logger | [Microsoft.Extensions.Logging.ILogger](#T-Microsoft-Extensions-Logging-ILogger 'Microsoft.Extensions.Logging.ILogger') | A [ILogger](#T-Microsoft-Extensions-Logging-ILogger 'Microsoft.Extensions.Logging.ILogger') to use for logging. |
| httpClientFactory | [System.Net.Http.IHttpClientFactory](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.IHttpClientFactory 'System.Net.Http.IHttpClientFactory') | The [IHttpClientFactory](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.IHttpClientFactory 'System.Net.Http.IHttpClientFactory') to use. |

<a name='M-BarbezDotEu-Provider-PoliteProvider-IsPolite'></a>
### IsPolite() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-BarbezDotEu-Provider-PoliteProvider-Request``1-System-Net-Http-HttpRequestMessage,System-Boolean,System-Double-'></a>
### Request\`\`1() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-BarbezDotEu-Provider-PoliteProvider-SetMultiplier-System-Int64-'></a>
### SetMultiplier() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerDay-System-String-'></a>
### SetRateLimitPerDay(callsPerDayString) `method`

##### Summary

Sets the number of calls per day as allowed to the provider, i.e. third-party resource.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| callsPerDayString | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The max. number of allowed calls per day. |

##### Remarks

The parameter string is expected to hold numeric values only.
[SetRateLimitPerDay](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerDay-System-String- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerDay(System.String)') and [SetRateLimitPerMinute](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerMinute-System-String- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerMinute(System.String)') are mutually exclusive, and the one last calls determines the rate limiter.

<a name='M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerMinute-System-String-'></a>
### SetRateLimitPerMinute(callsPerDayString) `method`

##### Summary

Sets the number of calls per minute as allowed to the provider, i.e. third-party resource.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| callsPerDayString | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The max. number of allowed calls per minute. |

##### Remarks

The parameter string is expected to hold numeric values only.
[SetRateLimitPerDay](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerDay-System-String- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerDay(System.String)') and [SetRateLimitPerMinute](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerMinute-System-String- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerMinute(System.String)') are mutually exclusive, and the one last calls determines the rate limiter.

<a name='M-BarbezDotEu-Provider-PoliteProvider-UpdateTimeOfLastCall-System-DateTime-'></a>
### UpdateTimeOfLastCall(lastQueryTime) `method`

##### Summary

Updates the date and time of when the last call to the provider, i.e. third-party resource, was made.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| lastQueryTime | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |

##### Remarks

It is important to keep updating this number in order to continuously receive a correct answer from the [IsPolite](#M-BarbezDotEu-Provider-PoliteProvider-IsPolite 'BarbezDotEu.Provider.PoliteProvider.IsPolite') method.

<a name='T-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse'></a>
## PostClientAuthorizeResponse `type`

##### Namespace

BarbezDotEu.Provider.DTO

##### Summary

Implements a client authorization response DTO in accordance to the interface as defined and shared by Vimeo, Twitter, and others.

<a name='P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-AccessToken'></a>
### AccessToken `property`

##### Summary

Gets or sets the access token.

<a name='P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-FailedResponse'></a>
### FailedResponse `property`

##### Summary

*Inherit from parent.*

<a name='P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-HasFailed'></a>
### HasFailed `property`

##### Summary

*Inherit from parent.*

<a name='P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-Scope'></a>
### Scope `property`

##### Summary

Gets or sets the authorization scope.

<a name='P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-TokenType'></a>
### TokenType `property`

##### Summary

Gets or sets the token type.

# BarbezDotEu.Provider
A non-exhaustive, yet useful HTTP(S) JSON client that supports rate limiting, enabling you to implement a polite integration with a third-party data provider.

## Contents

- [IPoliteProvider](#T-BarbezDotEu-Provider-Interfaces-IPoliteProvider 'BarbezDotEu.Provider.Interfaces.IPoliteProvider')
  - [IsPolite()](#M-BarbezDotEu-Provider-Interfaces-IPoliteProvider-IsPolite 'BarbezDotEu.Provider.Interfaces.IPoliteProvider.IsPolite')
  - [SetMultiplier()](#M-BarbezDotEu-Provider-Interfaces-IPoliteProvider-SetMultiplier-System-Int64- 'BarbezDotEu.Provider.Interfaces.IPoliteProvider.SetMultiplier(System.Int64)')
- [IPoliteResponse\`1](#T-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1 'BarbezDotEu.Provider.Interfaces.IPoliteResponse`1')
  - [Content](#P-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1-Content 'BarbezDotEu.Provider.Interfaces.IPoliteResponse`1.Content')
  - [HasFailed](#P-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1-HasFailed 'BarbezDotEu.Provider.Interfaces.IPoliteResponse`1.HasFailed')
  - [HttpResponseMessage](#P-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1-HttpResponseMessage 'BarbezDotEu.Provider.Interfaces.IPoliteResponse`1.HttpResponseMessage')
  - [SetContent(content)](#M-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1-SetContent-`0- 'BarbezDotEu.Provider.Interfaces.IPoliteResponse`1.SetContent(`0)')
- [PoliteProvider](#T-BarbezDotEu-Provider-PoliteProvider 'BarbezDotEu.Provider.PoliteProvider')
  - [#ctor(logger,httpClientFactory)](#M-BarbezDotEu-Provider-PoliteProvider-#ctor-Microsoft-Extensions-Logging-ILogger,System-Net-Http-IHttpClientFactory- 'BarbezDotEu.Provider.PoliteProvider.#ctor(Microsoft.Extensions.Logging.ILogger,System.Net.Http.IHttpClientFactory)')
  - [Logger](#P-BarbezDotEu-Provider-PoliteProvider-Logger 'BarbezDotEu.Provider.PoliteProvider.Logger')
  - [RequiredSecondsInBetweenCalls](#P-BarbezDotEu-Provider-PoliteProvider-RequiredSecondsInBetweenCalls 'BarbezDotEu.Provider.PoliteProvider.RequiredSecondsInBetweenCalls')
  - [GetPoliteResponse\`\`1(response)](#M-BarbezDotEu-Provider-PoliteProvider-GetPoliteResponse``1-System-Net-Http-HttpResponseMessage- 'BarbezDotEu.Provider.PoliteProvider.GetPoliteResponse``1(System.Net.Http.HttpResponseMessage)')
  - [IsPolite()](#M-BarbezDotEu-Provider-PoliteProvider-IsPolite 'BarbezDotEu.Provider.PoliteProvider.IsPolite')
  - [Request\`\`1(request,retryOnError,waitingMinutesBeforeRetry)](#M-BarbezDotEu-Provider-PoliteProvider-Request``1-System-Net-Http-HttpRequestMessage,System-Boolean,System-Double- 'BarbezDotEu.Provider.PoliteProvider.Request``1(System.Net.Http.HttpRequestMessage,System.Boolean,System.Double)')
  - [SetMultiplier()](#M-BarbezDotEu-Provider-PoliteProvider-SetMultiplier-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetMultiplier(System.Int64)')
  - [SetRateLimitPerDay(callsPerDay)](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerDay-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerDay(System.Int64)')
  - [SetRateLimitPerHour(callsPerHour)](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerHour-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerHour(System.Int64)')
  - [SetRateLimitPerMinute(callsPerMinute)](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerMinute-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerMinute(System.Int64)')
  - [UpdateTimeOfLastCall(lastQueryTime)](#M-BarbezDotEu-Provider-PoliteProvider-UpdateTimeOfLastCall-System-DateTime- 'BarbezDotEu.Provider.PoliteProvider.UpdateTimeOfLastCall(System.DateTime)')
- [PoliteReponse\`1](#T-BarbezDotEu-Provider-PoliteReponse`1 'BarbezDotEu.Provider.PoliteReponse`1')
  - [#ctor(httpResponseMessage)](#M-BarbezDotEu-Provider-PoliteReponse`1-#ctor-System-Net-Http-HttpResponseMessage- 'BarbezDotEu.Provider.PoliteReponse`1.#ctor(System.Net.Http.HttpResponseMessage)')
  - [Content](#P-BarbezDotEu-Provider-PoliteReponse`1-Content 'BarbezDotEu.Provider.PoliteReponse`1.Content')
  - [HasFailed](#P-BarbezDotEu-Provider-PoliteReponse`1-HasFailed 'BarbezDotEu.Provider.PoliteReponse`1.HasFailed')
  - [HttpResponseMessage](#P-BarbezDotEu-Provider-PoliteReponse`1-HttpResponseMessage 'BarbezDotEu.Provider.PoliteReponse`1.HttpResponseMessage')
  - [SetContent()](#M-BarbezDotEu-Provider-PoliteReponse`1-SetContent-`0- 'BarbezDotEu.Provider.PoliteReponse`1.SetContent(`0)')
- [PostClientAuthorizeResponse](#T-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse 'BarbezDotEu.Provider.DTO.PostClientAuthorizeResponse')
  - [AccessToken](#P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-AccessToken 'BarbezDotEu.Provider.DTO.PostClientAuthorizeResponse.AccessToken')
  - [Scope](#P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-Scope 'BarbezDotEu.Provider.DTO.PostClientAuthorizeResponse.Scope')
  - [TokenType](#P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-TokenType 'BarbezDotEu.Provider.DTO.PostClientAuthorizeResponse.TokenType')

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

<a name='T-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1'></a>
## IPoliteResponse\`1 `type`

##### Namespace

BarbezDotEu.Provider.Interfaces

##### Summary

Defines a [IPoliteResponse\`1](#T-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1 'BarbezDotEu.Provider.Interfaces.IPoliteResponse`1') to a [HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') requested by an [IPoliteProvider](#T-BarbezDotEu-Provider-Interfaces-IPoliteProvider 'BarbezDotEu.Provider.Interfaces.IPoliteProvider').

<a name='P-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1-Content'></a>
### Content `property`

##### Summary

Gets the actual content of a successful response to a [HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage').

<a name='P-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1-HasFailed'></a>
### HasFailed `property`

##### Summary

Gets a value indicating whether the [HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') indicates a fault.

<a name='P-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1-HttpResponseMessage'></a>
### HttpResponseMessage `property`

##### Summary

Gets the [HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') that indicates a fault that has occurred.

<a name='M-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1-SetContent-`0-'></a>
### SetContent(content) `method`

##### Summary

Sets the the actual response content.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| content | [\`0](#T-`0 '`0') | The response content to set. |

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

<a name='P-BarbezDotEu-Provider-PoliteProvider-Logger'></a>
### Logger `property`

##### Summary

Gets or sets the [ILogger](#T-Microsoft-Extensions-Logging-ILogger 'Microsoft.Extensions.Logging.ILogger').

<a name='P-BarbezDotEu-Provider-PoliteProvider-RequiredSecondsInBetweenCalls'></a>
### RequiredSecondsInBetweenCalls `property`

##### Summary

Gets the number of seconds required to lapse before a next call to the data provider is considered polite.

<a name='M-BarbezDotEu-Provider-PoliteProvider-GetPoliteResponse``1-System-Net-Http-HttpResponseMessage-'></a>
### GetPoliteResponse\`\`1(response) `method`

##### Summary

Converts a [HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') into a [PoliteReponse\`1](#T-BarbezDotEu-Provider-PoliteReponse`1 'BarbezDotEu.Provider.PoliteReponse`1').

##### Returns

The [HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') as [PoliteReponse\`1](#T-BarbezDotEu-Provider-PoliteReponse`1 'BarbezDotEu.Provider.PoliteReponse`1').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| response | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The response to convert. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The view model to deserialize a successful response into. |

<a name='M-BarbezDotEu-Provider-PoliteProvider-IsPolite'></a>
### IsPolite() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-BarbezDotEu-Provider-PoliteProvider-Request``1-System-Net-Http-HttpRequestMessage,System-Boolean,System-Double-'></a>
### Request\`\`1(request,retryOnError,waitingMinutesBeforeRetry) `method`

##### Summary

Sends a [HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') to the third-party provider, expecting a certain response.

##### Returns

The expected response content type, as well as other metadata, in case of an exception.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| request | [System.Net.Http.HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') | The [HttpRequestMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpRequestMessage 'System.Net.Http.HttpRequestMessage') to send to the third-party provider. |
| retryOnError | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |
| waitingMinutesBeforeRetry | [System.Double](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Double 'System.Double') | The number of minutes to wait before automatically retrying re-sending the request, if the intention is to retry again upon error. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T | The expected response content type. |

<a name='M-BarbezDotEu-Provider-PoliteProvider-SetMultiplier-System-Int64-'></a>
### SetMultiplier() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerDay-System-Int64-'></a>
### SetRateLimitPerDay(callsPerDay) `method`

##### Summary

Sets the number of calls per day as allowed to the provider, i.e. third-party resource.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| callsPerDay | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | The max. number of allowed calls per day. |

##### Remarks

The parameter string is expected to hold numeric values only.
[SetRateLimitPerDay](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerDay-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerDay(System.Int64)'), [SetRateLimitPerHour](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerHour-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerHour(System.Int64)'), and [SetRateLimitPerMinute](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerMinute-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerMinute(System.Int64)') are mutually exclusive, hence the last called method determines the rate limiter.

<a name='M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerHour-System-Int64-'></a>
### SetRateLimitPerHour(callsPerHour) `method`

##### Summary

Sets the number of calls per hour as allowed to the provider, i.e. third-party resource.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| callsPerHour | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | The max. number of allowed calls per hour. |

##### Remarks

The parameter string is expected to hold numeric values only.
[SetRateLimitPerDay](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerDay-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerDay(System.Int64)'), [SetRateLimitPerHour](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerHour-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerHour(System.Int64)'), and [SetRateLimitPerMinute](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerMinute-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerMinute(System.Int64)') are mutually exclusive, hence the last called method determines the rate limiter.

<a name='M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerMinute-System-Int64-'></a>
### SetRateLimitPerMinute(callsPerMinute) `method`

##### Summary

Sets the number of calls per minute as allowed to the provider, i.e. third-party resource.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| callsPerMinute | [System.Int64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int64 'System.Int64') | The max. number of allowed calls per minute. |

##### Remarks

The parameter string is expected to hold numeric values only.
[SetRateLimitPerDay](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerDay-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerDay(System.Int64)'), [SetRateLimitPerHour](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerHour-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerHour(System.Int64)'), and [SetRateLimitPerMinute](#M-BarbezDotEu-Provider-PoliteProvider-SetRateLimitPerMinute-System-Int64- 'BarbezDotEu.Provider.PoliteProvider.SetRateLimitPerMinute(System.Int64)') are mutually exclusive, hence the last called method determines the rate limiter.

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

<a name='T-BarbezDotEu-Provider-PoliteReponse`1'></a>
## PoliteReponse\`1 `type`

##### Namespace

BarbezDotEu.Provider

##### Summary

*Inherit from parent.*

<a name='M-BarbezDotEu-Provider-PoliteReponse`1-#ctor-System-Net-Http-HttpResponseMessage-'></a>
### #ctor(httpResponseMessage) `constructor`

##### Summary

Constructs a new [PoliteReponse\`1](#T-BarbezDotEu-Provider-PoliteReponse`1 'BarbezDotEu.Provider.PoliteReponse`1') from a given [HttpResponseMessage](#P-BarbezDotEu-Provider-PoliteReponse`1-HttpResponseMessage 'BarbezDotEu.Provider.PoliteReponse`1.HttpResponseMessage').

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| httpResponseMessage | [System.Net.Http.HttpResponseMessage](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Net.Http.HttpResponseMessage 'System.Net.Http.HttpResponseMessage') | The [HttpResponseMessage](#P-BarbezDotEu-Provider-PoliteReponse`1-HttpResponseMessage 'BarbezDotEu.Provider.PoliteReponse`1.HttpResponseMessage') to construct this [IPoliteResponse\`1](#T-BarbezDotEu-Provider-Interfaces-IPoliteResponse`1 'BarbezDotEu.Provider.Interfaces.IPoliteResponse`1') from. |

<a name='P-BarbezDotEu-Provider-PoliteReponse`1-Content'></a>
### Content `property`

##### Summary

*Inherit from parent.*

<a name='P-BarbezDotEu-Provider-PoliteReponse`1-HasFailed'></a>
### HasFailed `property`

##### Summary

*Inherit from parent.*

<a name='P-BarbezDotEu-Provider-PoliteReponse`1-HttpResponseMessage'></a>
### HttpResponseMessage `property`

##### Summary

*Inherit from parent.*

<a name='M-BarbezDotEu-Provider-PoliteReponse`1-SetContent-`0-'></a>
### SetContent() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

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

<a name='P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-Scope'></a>
### Scope `property`

##### Summary

Gets or sets the authorization scope.

<a name='P-BarbezDotEu-Provider-DTO-PostClientAuthorizeResponse-TokenType'></a>
### TokenType `property`

##### Summary

Gets or sets the token type.

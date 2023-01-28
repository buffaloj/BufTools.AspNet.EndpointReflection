# Endpoint Reflection
Combines reflection and XML comment parsing to put endpoint details at your fingertips.

```cs
var endpoints = assembly.GetEndpoints();
```

Provides:
- Route & Verb
- Useable example route with route params filled in XML examples
- Endpoint method details
- XML comments
- Validation of XML comments with suggestions on how to fix errors


# Getting Started

1. Grab the assembly that contains endpoints you want to reflect:

```cs
var assembly = typeof(SomeClassInYourProject).Assembly;
```
* There are many standard ways to grab the Assembly. This is just one example

2. Get the endpoints:

```cs
IEnumerable<HttpEndpoint> endpoints = assembly.GetEndpoints();
```

# Endpoint Info

GetEndpoints returns a collection of HttpEndpoint instances with info describing the endpoint.

HttpEndpoint includes:

## Route Details
- Route - The route to the endpoint without the base URL
- ExampleRoute - A useable route that has the route params replaced from examples in the XML comments
* an XML example value must be present for the ExampleRoute to be constructed, otherwise it will be empty
- Verb - The HTTP verb the endpoint acts on

## XML Comments & Example Values
- XmlSummary - the <summary> field from XML comments on an endpoint method
- XmlReturns - the XML <returns> field
- XmlRemarks - a collection of <remarks> fields
- XmlExceptions - a collection of <exception> details, including exception full type name and description
- EndpointParam.XMLDescription - description in a <param> field
- EndpointParam.XmlExample - an example value in a <param example="5"> field

## Validation
- XmlRouteValidationErrors - a collection of errors in the XML comments that prevent building an example route
- AllXmlValidationErrors - a collection of all endpoint errors like missing XML comments

## Request/Response Types
- BodyPayloadType - the type of payload expected in the body of a request
- ResponseTypes - The reponse types listed using the [ProducesResponseType] attributes on the endpoint

## Method Info
- ReturnType - the return type in the method signature
- MethodName - The name of the endpoint method
- MethodParams - info on the params the endpoint method accepts.  Includes param Type, XML comments, and useage type

## Controller Details
- ControllerType - the type of the controller that contains the endpoint
- Assembly - the assembly that contains the controller & endpoint

# Endpoint Reflection
Provides a simple method that returns all endpoints in an assembly.

```cs
var endpoints = assembly.GetEndpoints();
```

# Getting Started

1. Grab the assembly that contains endpoints you want to reflect

```cs
var assembly = typeof(SomeClassInYourProject).Assembly;
```
* There are many standard ways to grab the Assembly. This is just one example

2. Get the endpoints

```cs
var endpoints = assembly.GetEndpoints();
```

# Endpoint Info

GetEndpoints returns a collection of HttpEndpoint instances with info describing the endpoint.

- Route - The route to the endpoint without the base URL
- Verb - The HTTP verb the endpoint acts on
- Parameters - Reflection info for the parameters that the endpoint method accepts
- MethodInfo - Reflection info for the method that implements the endpoint 
- ReturnType - The type the method that handles the endpoint returns
- ControllerName - The name of the controller class that contains the endpoint
- Assembly - The assembly that contains the endpoint method
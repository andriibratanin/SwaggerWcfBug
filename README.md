# SwaggerWcfBug
SwaggerWcf has a bug: it fails to generate "swagger.json" file if service's contract contains lists of complex types. It only reproduces if parameter name was not included into an uri's templete (binding does not matter).

This is a ready-to-test solution with service logs tracing enabled ("Traces.svclog" will be automatically created near the "csproj" file)

Try [http://localhost:58590/DemoService.svc/json/ping](http://localhost:58590/DemoService.svc/json/ping) to make sure that service is running  
Try [http://localhost:58590/api-docs/](http://localhost:58590/api-docs/) to see Swagger ui  
Please, note that port number may change on your local machine because of IIS Express configuration.

## Stacktrace
Swagger throws **NullReferenceException** with the following stacktrace:
```
SwaggerWcf.Support.Mapper.GetParameter(TypeFormat typeFormat, ParameterInfo parameter, SwaggerWcfParameterAttribute settings, String uriTemplate, IList`1 definitionsTypesList)
SwaggerWcf.Support.Mapper.&lt;GetActions&gt;d__4.MoveNext()
System.Collections.Generic.List`1.InsertRange(Int32 index, IEnumerable`1 collection)
System.Collections.Generic.List`1.AddRange(IEnumerable`1 collection)
SwaggerWcf.Support.Mapper.FindMethods(String basePath, Type serviceType, IList`1 definitionsTypesList)
SwaggerWcf.Support.ServiceBuilder.BuildPaths(Service service, IList`1 hiddenTags, IList`1 definitionsTypesList)
SwaggerWcf.Support.ServiceBuilder.BuildService()
SwaggerWcf.Support.ServiceBuilder.Build()
SwaggerWcf.SwaggerWcfEndpoint.Init()
CreateSwaggerWcf.SwaggerWcfEndpoint()
System.ServiceModel.Dispatcher.InstanceProvider.GetInstance(InstanceContext instanceContext, Message message)
System.ServiceModel.Dispatcher.InstanceBehavior.GetInstance(InstanceContext instanceContext, Message request)
System.ServiceModel.InstanceContext.GetServiceInstance(Message message)
System.ServiceModel.Dispatcher.InstanceBehavior.EnsureServiceInstance(MessageRpc&amp; rpc)
System.ServiceModel.Dispatcher.ImmutableDispatchRuntime.ProcessMessage41(MessageRpc&amp; rpc)
System.ServiceModel.Dispatcher.ImmutableDispatchRuntime.ProcessMessage4(MessageRpc&amp; rpc)
System.ServiceModel.Dispatcher.ImmutableDispatchRuntime.ProcessMessage31(MessageRpc&amp; rpc)
System.ServiceModel.Dispatcher.ImmutableDispatchRuntime.ProcessMessage3(MessageRpc&amp; rpc)
System.ServiceModel.Dispatcher.ImmutableDispatchRuntime.ProcessMessage2(MessageRpc&amp; rpc)
System.ServiceModel.Dispatcher.ImmutableDispatchRuntime.ProcessMessage11(MessageRpc&amp; rpc)
System.ServiceModel.Dispatcher.ImmutableDispatchRuntime.ProcessMessage1(MessageRpc&amp; rpc)
System.ServiceModel.Dispatcher.MessageRpc.Process(Boolean isOperationContextSet)
```

## Some examples
### Contract with list of strings (fails because parameter name is not present in the uri template)
```cs
[OperationContract]
[WebInvoke(Method = "POST", UriTemplate = "/fail")]
void SwaggerFail(List<string> notok);
```
but will work in case of the uri change to '/notok':
```cs
[OperationContract]
[WebInvoke(Method = "POST", UriTemplate = "/notok")]
void SwaggerFail(List<string> notok);
```

### Contract with parameter name present in the uri (will work ok)
```cs
[OperationContract]
[WebInvoke(Method = "POST", UriTemplate = "/ok")]
void SwaggerOk(List<string> ok);
```

### Arrays are ok
```cs
[OperationContract]
[WebInvoke(Method = "POST", UriTemplate = "/arrayOk")]
void SwaggerArrayOk(string[] parameter);
```

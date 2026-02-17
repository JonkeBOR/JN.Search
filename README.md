# JN.Search

#Reflections

Given more more time I would:

* implemenent more granular exceptions types (application, domain, infrastructure) to further discern what to show the user and what to simply log
* implement chaching with IMemoryCache
* implement logging (extending the exceptionhandler and perhaps adding a Behaviour in the MediatR pipeline)
* Add service registration extention methods to make the Dependency injection of each layer a one liner
* Add the usage dapper to make the queries faster
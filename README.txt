
Application Name:  EvolentHealth

Solution Architecture : Onion Architecture based on DDD

Solution Folder Structure :  
 1. Folder Name : Data has two projects :- Core Project, It holds all application domain objects. If an application is developed with ORM entity framework then this layer holds POCO classes (Code First) 
	a) EvolentHealth.Entities
	b) EvolentHealth.Repository
 2. Folder Name : Library has three projects : The layer holds interfaces which are used to communicate between the UI layer and repository layer. It holds business logic for an entity so it’s called business logic layer as well.	
	a) EvolentHealth.Models
	b) EvolentHealth.Service
	c) EvolentHealth.ServiceInterface
 3. Folder Name : CrossCutting has three projects :- Cross cutting projects that will cross layered from library to API
	a) EvolentHealth.CrossCutting.DependencyResolution
	b) EvolentHealth.CrossCutting.Logging
	c) EvolentHealth.Utilities
 4. Project EvolentHealth.API is end point port to expose RestApi to client 
 5. Folder Name : UnitTest has right now one project EvolentHealth.UnitTest.API.Test for API unit testing but we can extend it to add unit test projects for Repository unit testing as well as Service 
 
 Project Dependency:-
   1. EvolentHealth.Entities is not depend on any other project. It is core to the Onion Architecture 
   2. EvolentHealth.Repository is only depend on  EvolentHealth.Entities. it is also part of core to the Onion Architecture.
   3. EvolentHealth.Models is dto model and is not depend on any other.
   4. EvolentHealth.ServiceInterface is only depend on EvolentHealth.Models and will act as adapter to create object.
   5. EvolentHealth.Service is depend on EvolentHealth.Entities, EvolentHealth.Repository, EvolentHealth.Models and EvolentHealth.ServiceInterface.
   6. EvolentHealth.CrossCutting.Logging; i have not written fully code for logging but made it part of project arch to display how we can handle logging. 
									It will depend on core i.e. EvolentHealth.Entities as well as EvolentHealth.Repository if we want to log exception in database
   7. EvolentHealth.Utilities; i have not written any code for it but it will be helper project and other project will depend on it.
   8. EvolentHealth.CrossCutting.DependencyResolution is depend on EvolentHealth.Repository, EvolentHealth.Models and EvolentHealth.ServiceInterface. it will resolve dependency of object creations.
   9. EvolentHealth.API is depend on EvolentHealth.Models, EvolentHealth.ServiceInterface, EvolentHealth.CrossCutting.Logging and EvolentHealth.CrossCutting.DependencyResolution.
  10. EvolentHealth.UnitTest.API.Test is depend on  EvolentHealth.API.
  

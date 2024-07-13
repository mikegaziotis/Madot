IF NOT EXISTS (SELECT TOP 1 NULL FROM dbo.AppCommonPage)
BEGIN
    INSERT INTO dbo.[AppCommonPage]
    (
        [Title]
        ,[Data]
        ,[OrderId]
        ,[IsDeleted]
        ,[CreatedBy]
        ,[CreatedDate]
        ,[LastModifiedBy]
        ,[LastModifiedDate]
    )
    VALUES 
    (
        'Welcome to Madot',
        '# Welcome to Madot!
---
Hi! Welcome to Madot, the **M**ulti-**A**PI **Do**cumentation **T**oolset. It is a FOSS (free and open source software) that helps you manage your API documentation in the world of microservices. Boost the discoverability, readability and integration of your APIs in your organisation!

Madot came about as both a learning/hobby project of its creator, but also to address the need for a decent, self-hosted, API documentation solution. Most solutions on the market seem to include hosting which comes with both high costs and added risks of 3rd party dependence, vendor lock etc. 

Madot is here to address all those concerns. All you need it download the code, make any modifications that suit your organisation (such as Auth) and deploy it on your own infrastructure. Your documentation never leaves the domain of your organisation and is fully owned by you. All at the minimal cost of running 3 docker containers, a DB, an API and a Web App.

In the next few pages we will explain how the app holds together and how to start using it.',
        1,
        0,
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    (
        'Functionality',
        '# Functionality
---
This is a placeholder for the basic functionality covered by Madot.',
        2,
        0,
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    (
        'Tech Stack & Architecture',
        '# Tech Stack & Architecture
---
This is a placeholder for the tech stack and architecture of Madot.',
        3,
        0,
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    );

END

IF NOT EXISTS (SELECT TOP 1 NULL FROM dbo.Api)
BEGIN
    INSERT INTO dbo.[Api]
    (
        [Id]
        ,[DisplayName]
        ,[BaseUrlPath]
        ,[IsInternal]
        ,[IsPreview]
        ,[IsHidden]
        ,[OrderId]
        ,[CreatedBy]
        ,[CreatedDate]
        ,[LastModifiedBy]
        ,[LastModifiedDate]
    )
    VALUES
    (
        'petstore',
        'Pet Store',
        'https://petstore.com',
        0,
        0,
        0,
        1,
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    (
        'todo',
        'ToDo',
        'https://todo.com',
        0,
        1,
        0,
        2,
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    )

    
END

IF NOT EXISTS (SELECT TOP 1 NULL FROM dbo.VersionedDocument)
BEGIN
    --Open Api Spec
    INSERT INTO [dbo].[VersionedDocument] 
    (
       [Id]
       ,[ApiId]
       ,[DocumentType]
       ,[Iteration]
       ,[Data]
       ,[CreatedBy]
       ,[CreatedDate]
       ,[LastModifiedBy]
       ,[LastModifiedDate] 
    )
    VALUES
    (
        'SGH46712DF',
        'petstore',
        'OpenApiSpec',
        1,
        '{"swagger":"2.0","info":{"description":"This is a sample server Petstore server.  You can find out more about Swagger at [http://swagger.io](http://swagger.io) or on [irc.freenode.net, #swagger](http://swagger.io/irc/).  For this sample, you can use the api key `special-key` to test the authorization filters.","version":"1.0.7","title":"Swagger Petstore","termsOfService":"http://swagger.io/terms/","contact":{"email":"apiteam@swagger.io"},"license":{"name":"Apache 2.0","url":"http://www.apache.org/licenses/LICENSE-2.0.html"}},"host":"petstore.swagger.io","basePath":"/v2","tags":[{"name":"pet","description":"Everything about your Pets","externalDocs":{"description":"Find out more","url":"http://swagger.io"}},{"name":"store","description":"Access to Petstore orders"},{"name":"user","description":"Operations about user","externalDocs":{"description":"Find out more about our store","url":"http://swagger.io"}}],"schemes":["https","http"],"paths":{"/pet/{petId}/uploadImage":{"post":{"tags":["pet"],"summary":"uploads an image","description":"","operationId":"uploadFile","consumes":["multipart/form-data"],"produces":["application/json"],"parameters":[{"name":"petId","in":"path","description":"ID of pet to update","required":true,"type":"integer","format":"int64"},{"name":"additionalMetadata","in":"formData","description":"Additional data to pass to server","required":false,"type":"string"},{"name":"file","in":"formData","description":"file to upload","required":false,"type":"file"}],"responses":{"200":{"description":"successful operation","schema":{"$ref":"#/definitions/ApiResponse"}}},"security":[{"petstore_auth":["write:pets","read:pets"]}]}},"/pet":{"post":{"tags":["pet"],"summary":"Add a new pet to the store","description":"","operationId":"addPet","consumes":["application/json","application/xml"],"produces":["application/json","application/xml"],"parameters":[{"in":"body","name":"body","description":"Pet object that needs to be added to the store","required":true,"schema":{"$ref":"#/definitions/Pet"}}],"responses":{"405":{"description":"Invalid input"}},"security":[{"petstore_auth":["write:pets","read:pets"]}]},"put":{"tags":["pet"],"summary":"Update an existing pet","description":"","operationId":"updatePet","consumes":["application/json","application/xml"],"produces":["application/json","application/xml"],"parameters":[{"in":"body","name":"body","description":"Pet object that needs to be added to the store","required":true,"schema":{"$ref":"#/definitions/Pet"}}],"responses":{"400":{"description":"Invalid ID supplied"},"404":{"description":"Pet not found"},"405":{"description":"Validation exception"}},"security":[{"petstore_auth":["write:pets","read:pets"]}]}},"/pet/findByStatus":{"get":{"tags":["pet"],"summary":"Finds Pets by status","description":"Multiple status values can be provided with comma separated strings","operationId":"findPetsByStatus","produces":["application/json","application/xml"],"parameters":[{"name":"status","in":"query","description":"Status values that need to be considered for filter","required":true,"type":"array","items":{"type":"string","enum":["available","pending","sold"],"default":"available"},"collectionFormat":"multi"}],"responses":{"200":{"description":"successful operation","schema":{"type":"array","items":{"$ref":"#/definitions/Pet"}}},"400":{"description":"Invalid status value"}},"security":[{"petstore_auth":["write:pets","read:pets"]}]}},"/pet/findByTags":{"get":{"tags":["pet"],"summary":"Finds Pets by tags","description":"Multiple tags can be provided with comma separated strings. Use tag1, tag2, tag3 for testing.","operationId":"findPetsByTags","produces":["application/json","application/xml"],"parameters":[{"name":"tags","in":"query","description":"Tags to filter by","required":true,"type":"array","items":{"type":"string"},"collectionFormat":"multi"}],"responses":{"200":{"description":"successful operation","schema":{"type":"array","items":{"$ref":"#/definitions/Pet"}}},"400":{"description":"Invalid tag value"}},"security":[{"petstore_auth":["write:pets","read:pets"]}],"deprecated":true}},"/pet/{petId}":{"get":{"tags":["pet"],"summary":"Find pet by ID","description":"Returns a single pet","operationId":"getPetById","produces":["application/json","application/xml"],"parameters":[{"name":"petId","in":"path","description":"ID of pet to return","required":true,"type":"integer","format":"int64"}],"responses":{"200":{"description":"successful operation","schema":{"$ref":"#/definitions/Pet"}},"400":{"description":"Invalid ID supplied"},"404":{"description":"Pet not found"}},"security":[{"api_key":[]}]},"post":{"tags":["pet"],"summary":"Updates a pet in the store with form data","description":"","operationId":"updatePetWithForm","consumes":["application/x-www-form-urlencoded"],"produces":["application/json","application/xml"],"parameters":[{"name":"petId","in":"path","description":"ID of pet that needs to be updated","required":true,"type":"integer","format":"int64"},{"name":"name","in":"formData","description":"Updated name of the pet","required":false,"type":"string"},{"name":"status","in":"formData","description":"Updated status of the pet","required":false,"type":"string"}],"responses":{"405":{"description":"Invalid input"}},"security":[{"petstore_auth":["write:pets","read:pets"]}]},"delete":{"tags":["pet"],"summary":"Deletes a pet","description":"","operationId":"deletePet","produces":["application/json","application/xml"],"parameters":[{"name":"api_key","in":"header","required":false,"type":"string"},{"name":"petId","in":"path","description":"Pet id to delete","required":true,"type":"integer","format":"int64"}],"responses":{"400":{"description":"Invalid ID supplied"},"404":{"description":"Pet not found"}},"security":[{"petstore_auth":["write:pets","read:pets"]}]}},"/store/inventory":{"get":{"tags":["store"],"summary":"Returns pet inventories by status","description":"Returns a map of status codes to quantities","operationId":"getInventory","produces":["application/json"],"parameters":[],"responses":{"200":{"description":"successful operation","schema":{"type":"object","additionalProperties":{"type":"integer","format":"int32"}}}},"security":[{"api_key":[]}]}},"/store/order":{"post":{"tags":["store"],"summary":"Place an order for a pet","description":"","operationId":"placeOrder","consumes":["application/json"],"produces":["application/json","application/xml"],"parameters":[{"in":"body","name":"body","description":"order placed for purchasing the pet","required":true,"schema":{"$ref":"#/definitions/Order"}}],"responses":{"200":{"description":"successful operation","schema":{"$ref":"#/definitions/Order"}},"400":{"description":"Invalid Order"}}}},"/store/order/{orderId}":{"get":{"tags":["store"],"summary":"Find purchase order by ID","description":"For valid response try integer IDs with value >= 1 and <= 10. Other values will generated exceptions","operationId":"getOrderById","produces":["application/json","application/xml"],"parameters":[{"name":"orderId","in":"path","description":"ID of pet that needs to be fetched","required":true,"type":"integer","maximum":10,"minimum":1,"format":"int64"}],"responses":{"200":{"description":"successful operation","schema":{"$ref":"#/definitions/Order"}},"400":{"description":"Invalid ID supplied"},"404":{"description":"Order not found"}}},"delete":{"tags":["store"],"summary":"Delete purchase order by ID","description":"For valid response try integer IDs with positive integer value. Negative or non-integer values will generate API errors","operationId":"deleteOrder","produces":["application/json","application/xml"],"parameters":[{"name":"orderId","in":"path","description":"ID of the order that needs to be deleted","required":true,"type":"integer","minimum":1,"format":"int64"}],"responses":{"400":{"description":"Invalid ID supplied"},"404":{"description":"Order not found"}}}},"/user/createWithList":{"post":{"tags":["user"],"summary":"Creates list of users with given input array","description":"","operationId":"createUsersWithListInput","consumes":["application/json"],"produces":["application/json","application/xml"],"parameters":[{"in":"body","name":"body","description":"List of user object","required":true,"schema":{"type":"array","items":{"$ref":"#/definitions/User"}}}],"responses":{"default":{"description":"successful operation"}}}},"/user/{username}":{"get":{"tags":["user"],"summary":"Get user by user name","description":"","operationId":"getUserByName","produces":["application/json","application/xml"],"parameters":[{"name":"username","in":"path","description":"The name that needs to be fetched. Use user1 for testing. ","required":true,"type":"string"}],"responses":{"200":{"description":"successful operation","schema":{"$ref":"#/definitions/User"}},"400":{"description":"Invalid username supplied"},"404":{"description":"User not found"}}},"put":{"tags":["user"],"summary":"Updated user","description":"This can only be done by the logged in user.","operationId":"updateUser","consumes":["application/json"],"produces":["application/json","application/xml"],"parameters":[{"name":"username","in":"path","description":"name that need to be updated","required":true,"type":"string"},{"in":"body","name":"body","description":"Updated user object","required":true,"schema":{"$ref":"#/definitions/User"}}],"responses":{"400":{"description":"Invalid user supplied"},"404":{"description":"User not found"}}},"delete":{"tags":["user"],"summary":"Delete user","description":"This can only be done by the logged in user.","operationId":"deleteUser","produces":["application/json","application/xml"],"parameters":[{"name":"username","in":"path","description":"The name that needs to be deleted","required":true,"type":"string"}],"responses":{"400":{"description":"Invalid username supplied"},"404":{"description":"User not found"}}}},"/user/login":{"get":{"tags":["user"],"summary":"Logs user into the system","description":"","operationId":"loginUser","produces":["application/json","application/xml"],"parameters":[{"name":"username","in":"query","description":"The user name for login","required":true,"type":"string"},{"name":"password","in":"query","description":"The password for login in clear text","required":true,"type":"string"}],"responses":{"200":{"description":"successful operation","headers":{"X-Expires-After":{"type":"string","format":"date-time","description":"date in UTC when token expires"},"X-Rate-Limit":{"type":"integer","format":"int32","description":"calls per hour allowed by the user"}},"schema":{"type":"string"}},"400":{"description":"Invalid username/password supplied"}}}},"/user/logout":{"get":{"tags":["user"],"summary":"Logs out current logged in user session","description":"","operationId":"logoutUser","produces":["application/json","application/xml"],"parameters":[],"responses":{"default":{"description":"successful operation"}}}},"/user/createWithArray":{"post":{"tags":["user"],"summary":"Creates list of users with given input array","description":"","operationId":"createUsersWithArrayInput","consumes":["application/json"],"produces":["application/json","application/xml"],"parameters":[{"in":"body","name":"body","description":"List of user object","required":true,"schema":{"type":"array","items":{"$ref":"#/definitions/User"}}}],"responses":{"default":{"description":"successful operation"}}}},"/user":{"post":{"tags":["user"],"summary":"Create user","description":"This can only be done by the logged in user.","operationId":"createUser","consumes":["application/json"],"produces":["application/json","application/xml"],"parameters":[{"in":"body","name":"body","description":"Created user object","required":true,"schema":{"$ref":"#/definitions/User"}}],"responses":{"default":{"description":"successful operation"}}}}},"securityDefinitions":{"api_key":{"type":"apiKey","name":"api_key","in":"header"},"petstore_auth":{"type":"oauth2","authorizationUrl":"https://petstore.swagger.io/oauth/authorize","flow":"implicit","scopes":{"read:pets":"read your pets","write:pets":"modify pets in your account"}}},"definitions":{"ApiResponse":{"type":"object","properties":{"code":{"type":"integer","format":"int32"},"type":{"type":"string"},"message":{"type":"string"}}},"Category":{"type":"object","properties":{"id":{"type":"integer","format":"int64"},"name":{"type":"string"}},"xml":{"name":"Category"}},"Pet":{"type":"object","required":["name","photoUrls"],"properties":{"id":{"type":"integer","format":"int64"},"category":{"$ref":"#/definitions/Category"},"name":{"type":"string","example":"doggie"},"photoUrls":{"type":"array","xml":{"wrapped":true},"items":{"type":"string","xml":{"name":"photoUrl"}}},"tags":{"type":"array","xml":{"wrapped":true},"items":{"xml":{"name":"tag"},"$ref":"#/definitions/Tag"}},"status":{"type":"string","description":"pet status in the store","enum":["available","pending","sold"]}},"xml":{"name":"Pet"}},"Tag":{"type":"object","properties":{"id":{"type":"integer","format":"int64"},"name":{"type":"string"}},"xml":{"name":"Tag"}},"Order":{"type":"object","properties":{"id":{"type":"integer","format":"int64"},"petId":{"type":"integer","format":"int64"},"quantity":{"type":"integer","format":"int32"},"shipDate":{"type":"string","format":"date-time"},"status":{"type":"string","description":"Order Status","enum":["placed","approved","delivered"]},"complete":{"type":"boolean"}},"xml":{"name":"Order"}},"User":{"type":"object","properties":{"id":{"type":"integer","format":"int64"},"username":{"type":"string"},"firstName":{"type":"string"},"lastName":{"type":"string"},"email":{"type":"string"},"password":{"type":"string"},"phone":{"type":"string"},"userStatus":{"type":"integer","format":"int32","description":"User Status"}},"xml":{"name":"User"}}},"externalDocs":{"description":"Find out more about Swagger","url":"http://swagger.io"}}',
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    --Homepage
    (
        'G54SH63BNE',
        'petstore',
        'Homepage',
        1,
        '# Welcome to Pet Store!

## Intro 
Let''s talk about our *favourite* pet store

> "Pet Store is my ***favourite*** pet store" - Mark Anonymous

Every one loves it, for the following reasons:
- It
- Is
- The 
- Best',
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    --Homepage 1.1
    (
        'WNTG2674JG',
        'petstore',
        'Homepage',
        2,
        '# Welcome to Pet Store!

## Version 1.1 is here

This is what every has been waiting for! 

> "Pet Store changed my *life*" - Joe Nameless

It''s...

![Magic](https://64.media.tumblr.com/0b51b9ac044e062857f4d4a26effca1a/tumblr_mkxkvhe54v1rc113po1_250.gif "Magic")
',
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    --Changelog
    (
        'HS63NMEQ65',
        'petstore',
        'Changelog',
        1,
        '# Changelog

### Change 3 - Meerkats! 
 
>**Date: 2024-06-01** > Developer comment:
> 
> "We now sell meerkat food"


### Change 2 - Opening hours

>**Date: 2024-05-21** > Developer comment:
> 
> "Shop now open till 11pm on Saturdays, from previous 8pm"

### Change 1 - Auth

>**Date: 2024-05-21** > Developer comment:
>
> "Fixed auth. All my homies hate auth"',
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    (
    'SJ312VBN2W'
    ,'Todo'--[ApiId]
    ,'OpenApiSpec'
    ,1
    ,'{"openapi":"3.0.1","info":{"title":"A TODO-Task list application","description":"A simple application to handle tasks.","version":"1.0.0","license":{"name":"Eclipse Public License 2.0","url":"https://www.eclipse.org/org/documents/epl-2.0/EPL-2.0.html"}},"servers":[{"url":"http://localhost:8080/todoapp"}],"tags":[{"name":"task","description":"Task management"}],"paths":{"/task":{"get":{"tags":["task"],"summary":"Get the list of all tasks","operationId":"tasksGetAll","responses":{"200":{"description":"List of all tasks","content":{"application/json":{"schema":{"type":"array","items":{"$ref":"#/components/schemas/Task"}}}}},"default":{"description":"Generic error response","content":{"application/json":{"schema":{"$ref":"#/components/schemas/Error"}}}}}},"post":{"tags":["task"],"summary":"Create a new task","operationId":"tasksCreate","requestBody":{"content":{"application/json":{"schema":{"$ref":"#/components/schemas/Task"}}}},"responses":{"200":{"description":"The created task","content":{"application/json":{"schema":{"$ref":"#/components/schemas/Task"}}}},"default":{"description":"Generic error response","content":{"application/json":{"schema":{"$ref":"#/components/schemas/Error"}}}}}}},"/task/{taskId}":{"parameters":[{"name":"taskId","in":"path","description":"The id of the task","required":true,"schema":{"type":"string"},"example":"e1cb23d0-6cbe-4a29-b586-bfa424bc93fd"}],"get":{"tags":["task"],"summary":"Get a single task based on its id","operationId":"tasksRead","responses":{"200":{"description":"Ok","content":{"application/json":{"schema":{"$ref":"#/components/schemas/Task"}}}},"default":{"description":"Generic error response","content":{"application/json":{"schema":{"$ref":"#/components/schemas/Error"}}}}}},"put":{"tags":["task"],"summary":"Update an existing task","operationId":"tasksUpdate","requestBody":{"content":{"application/json":{"schema":{"$ref":"#/components/schemas/Task"}}}},"responses":{"200":{"description":"The updated task","content":{"application/json":{"schema":{"$ref":"#/components/schemas/Task"}}}},"default":{"description":"Generic error response","content":{"application/json":{"schema":{"$ref":"#/components/schemas/Error"}}}}}},"delete":{"tags":["task"],"summary":"Delete an existing task","operationId":"tasksDelete","responses":{"204":{"description":"Task deleted response"},"default":{"description":"Generic error response","content":{"application/json":{"schema":{"$ref":"#/components/schemas/Error"}}}}}}}},"components":{"schemas":{"Task":{"description":"Object representing a Task","required":["description"],"type":"object","properties":{"completed":{"description":"indicates if a taks is completed or not","type":"boolean"},"description":{"description":"description of the task","type":"string","example":"My important task"},"id":{"description":"id of the taks","type":"string","readOnly":true,"example":"e1cb23d0-6cbe-4a29-b586-bfa424bc93fd"}}},"Error":{"description":"Object representing an error","type":"object","properties":{"code":{"format":"int32","description":"Error code that identify of the error","type":"integer","example":"1000"},"message":{"description":"Short description of the error","type":"string","example":"Could not perform the task"}}}}}}'
    ,'mig'
    ,GETUTCDATE()
    ,'mig'
    ,GETUTCDATE()
    ),
    (
        'TVBASN3718',
        'todo',
        'Homepage',
        1,
        '# Welcome to the ToDo API!

This API is still in in preview mode. Documentation is not complete',
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    )
END


IF NOT EXISTS (SELECT TOP 1 NULL FROM dbo.Guide)
BEGIN
    INSERT INTO [dbo].[Guide] 
    (
       [Id]
       ,[ApiId]
       ,[Title]
       ,[ProvisionalOrderId]
       ,[IsDeleted]
       ,[CreatedBy]
       ,[CreatedDate]
       ,[LastModifiedBy]
       ,[LastModifiedDate] 
    )
    VALUES
    (
        'FMBVN528JS',
        'petstore',
        'Authentication',
        1,
        0,    
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    )
    ,
    (
        '2LSJ7DM63M',
        'petstore',
        'Using the SDK',
        2,
        0,    
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    )
    ,
    (
        'ODFKV4535F',
        'petstore',
        'Rethinking Life Choices?',
        3,
        0,    
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    )

    INSERT INTO [dbo].[GuideVersion] 
    (
       [Id]
       ,[GuideId]
       ,[Iteration]
       ,[Data]
       ,[IsDeleted]
       ,[CreatedBy]
       ,[CreatedDate]
       ,[LastModifiedBy]
       ,[LastModifiedDate]
    )
    VALUES
    (
        'FIJK345JD7',
        'FMBVN528JS',
        1,
        '# Authentication

Having trouble logging in? 

Why don''t you...

![Git Gud](https://raw.githubusercontent.com/binaryben/dotfiles/docs/public/git%20gud%20light.png "Gid gud")',
        0,
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    )
    ,
    (
        'MC23CX74MC',
        '2LSJ7DM63M',
        1,
        '# Using the SDK

Why not download the SDK from [here](https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstley).

Check out these amazing code samples

    if ($code == "good")
        set $life = Mode.Easy;
    else
        disaster.Initiate();
    end
',
        0,    
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    )
    ,
    (
        '0JF56SUQMZ',
        'ODFKV4535F',
        1,
        '# Rethinking Life Choices

Is our code so bad, that it''s making you rethink life choices?

### Give up your job, become a cowboy today!
![BecomingCowboy](https://i.redd.it/zk5df61gu96b1.gif "Becoming Cowboy")
',
        0,    
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    )
END

IF NOT EXISTS (SELECT TOP 1 NULL FROM dbo.[File])
BEGIN
    INSERT INTO dbo.[File]
    (
        [Id]
        ,[ApiId]
        ,[DisplayName]
        ,[Description]
        ,[ImageURL]
        ,[IsDeleted]
        ,[CreatedBy]
        ,[CreatedDate]
        ,[LastModifiedBy]
        ,[LastModifiedDate]
    )
    VALUES
    (
        'TFOSM231MS',
        'petstore',
        'Postman',
        'You can download the Postman client HTTP tool from here',
        'https://voyager.postman.com/logo/postman-logo-icon-orange.svg',
        0,
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    )
    INSERT INTO dbo.[FileLink]
    (
        [FileId]
        ,[OperatingSystem]
        ,[ChipArchitecture]
        ,[DownloadURL]
        ,[CreatedBy]
        ,[CreatedDate]
        ,[LastModifiedBy]
        ,[LastModifiedDate]
    )
    VALUES
    (
        'TFOSM231MS',
        'Windows',
        'X64',
        'https://dl.pstmn.io/download/latest/win64',
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    (
        'TFOSM231MS',
        'MacOs',
        'X64',
        'https://dl.pstmn.io/download/latest/osx_64',
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    (
        'TFOSM231MS',
        'MacOs',
        'Arm64',
        'https://dl.pstmn.io/download/latest/osx_arm64',
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    (
        'TFOSM231MS',
        'Linux',
        'x64',
        'https://dl.pstmn.io/download/latest/linux_64',
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    (
        'TFOSM231MS',
        'Linux',
        'Arm64',
        'https://dl.pstmn.io/download/latest/linux_arm64',
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    )
    
END

IF NOT EXISTS (SELECT TOP 1 NULL FROM dbo.[ApiVersion])
BEGIN
    INSERT INTO dbo.[ApiVersion]
    (
        [Id]
        ,[ApiId]
        ,[MajorVersion]
        ,[MinorVersion]
        ,[BuildOrReleaseTag]
        ,[OpenApiSpecId]
        ,[HomepageId]
        ,[ChangelogId]
        ,[IsBeta]
        ,[IsHidden]
        ,[CreatedBy]
        ,[CreatedDate]
        ,[LastModifiedBy]
        ,[LastModifiedDate]  
    )
    VALUES
    (
        'SUDB9DH27W',
        'petstore',
        1,
        0,
        '1.7.213',
        'SGH46712DF',
        'G54SH63BNE',
        'HS63NMEQ65',
        1,
        0,
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    (
        'PLME826GH6',
        'petstore',
        1,
        1,
        '1.7.214',
        'SGH46712DF',
        'WNTG2674JG',
        'HS63NMEQ65',
        0,
        0,
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    ),
    (
        '8ANX29KNMS',
        'todo',
        1,
        0,
        '3.27.31',
        'SJ312VBN2W',
        'TVBASN3718',
        NULL,
        1,
        0,
        'mig',
        GETUTCDATE(),
        'mig',
        GETUTCDATE()
    )

    INSERT INTO dbo.[ApiVersionGuideVersion]
    (
        [ApiVersionId]
        ,[GuideVersionId]
        ,[OrderId]
        ,[CreatedBy]
        ,[CreatedDate]     
    )
    VALUES
    (
        'SUDB9DH27W',
        'FIJK345JD7',
        1,
        'mig',
        GETUTCDATE()
    ),
    (
        'SUDB9DH27W',
        'MC23CX74MC',
        2,
        'mig',
        GETUTCDATE()
    ),
    (
        'SUDB9DH27W',
        '0JF56SUQMZ',
        3,
        'mig',
        GETUTCDATE()
    ),
    (
        'PLME826GH6',
        'FIJK345JD7',
        1,
        'mig',
        GETUTCDATE()
    ),
    (
        'PLME826GH6',
        'MC23CX74MC',
        2,
        'mig',
        GETUTCDATE()
    ),
    (
        'PLME826GH6',
        '0JF56SUQMZ',
        3,
        'mig',
        GETUTCDATE()
    )

    INSERT INTO dbo.[ApiVersionFile]
    (
        [ApiVersionId]
        ,[FileId]
        ,[OrderId]
        ,[CreatedBy]
        ,[CreatedDate]     
    )
    VALUES
    (
        'SUDB9DH27W',
        'TFOSM231MS',
        1,
        'mig',
        GETUTCDATE()
    ),
    (
        'PLME826GH6',
        'TFOSM231MS',
        1,
        'mig',
        GETUTCDATE()
    )
    
END

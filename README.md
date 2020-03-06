# WebServiceMetrics
This is a simple POST endpoint that will record the response time of the desired web service endpoint.

Currently only basic auth is supported. It accepts a request body, # of times to make the call, and the target URL. The response times are returned in the response from the POST endpoint, and also recorded in an EF Code First generated database.

Upcoming Changes:
1. Add a GET endpoint to return all MetricRuns
2. Add a GET/ID endpoint to return a specific MetricRun
3. Refactor RepositoryBase to contain Add and Update
4. Add a front end using knockout.js or a vue.js APP (TBD)
5. Add identity management
6. Add the ability to tag each request in the POST endpoint so results can be aggregated

Diff API
This API is to check 2 binary arrays data and return the result which can be either Equals, SizeDoNotMatch or ContentDoNotMatch and spotting the differences.

Correct steps to test the API:
1-	Call the left endpoint (PUT) with the binary array data to either create a new DiffObject and assign the leftData property or if the object already exits update leftData property with the data in the request. Endpoint url is (/v1/1/left)
2-	Call the right endpoint (PUT) with the binary array data to either create a new DiffObject and assign the rightData property or if the object already exits update righttData property with the data in the request. Endpoint url is (/v1/1/right)
3-	Call the getting difference endpoint (GET) to start the comparison process and returning the result.

Testing:
There are 2 testing projects are included in the solution (Unit testing, Integration testing).

Unit Testing (DiffControllerTests & BinaryDiffServiceTests)
DiffControllerTests:
It tests the API endpoitns against the response type it has 3 tests
1-	AddLeftData_Returns_BadRequest_If_Null_Data to test if the left endpoint will return BadResuest if called with null data.
2-	GetDiffData_Returns_NotFound_With_Missing_Diff_Details to test if the get endpoint will retuen NotFound if called with Id and there is missing information which includes object doesn’t exist or left or right data is null.
3-	GetDiffData_Returns_Ok_With_Diff_Details to test if the get endpoint will return Ok if called with id and the object exists with proper data in both left and right.

BinaryDiffServiceTests
 It tests the service that do the difference logic and have 3 test
GetDataDifferences_Is_Equal_Data, GetDataDifferences_Is_ContentDoNotMatch_Data, GetDataDifferences_Is_SizeDoNotMatch_Data

Integration Testing (DiffAPIIntegrationTests)
It has 4 tests
1-	PutLeftAsync
2-	PutRightAsync
3-	GetDiffDataAsync
4-	GetDiffDataWithNullDataAsync

Assumptions:
1-	I assumed that there is no need to data persistence so I used Entityframework InMemory to save the DiffObjects in memory.
2-	I assumed that there is no need for logging the difference operations, so I didn’t implement it.
3-	I assumed that there is no need for perfect differentiation algorithm so I just shows differences offsets and lengths.

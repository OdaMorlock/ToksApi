using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToksApi.Data;
using ToksApi.Models;
using ToksApi.Models.ServiceModels;
using ToksApi.Services;

namespace ToksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustromIssueController : ControllerBase
    {

        private readonly IControllerServices _services;

        public CustromIssueController(IControllerServices services)
        {
            _services = services;

        }

        #region Get
        //Get

        [HttpGet("GetAllIssue")]
        public async Task<IActionResult> GetAllIssue()
        {
            var result = (await _services.GetAllIssuesAsync());
            return new OkObjectResult(result.issueModels);
        }


        [HttpGet("GetIssueById/{Id}")]
        public async Task<IActionResult> GetIssueById(int Id)
        {
            var result = (await _services.SearchIssueByIdAsync((uint)Id));
            if (result.Result)
            {
                return new OkObjectResult(result.issueModels);
            }
            return new BadRequestObjectResult($"{result.Message}");
        }

        [HttpGet("SearchIssueByTitle/{SearchWord}")]
        public async Task<IActionResult> GetIssueByTitle(string SearchWord)
        {

            //return new JsonResult(result.issueModels);  

            var result = (await _services.SearchIssueByTitleAsync(SearchWord));
            if (result.Result)
            {
                return new OkObjectResult(result.issueModels);
            }
            return new BadRequestObjectResult($"{result.Message}");
        }

        [HttpGet("SearchIssueByDescription/{SearchWord}")]
        public async Task<IActionResult> GetIssueByDescription(string SearchWord)
        {
            var result = (await _services.SearchIssueByDescriptionAsync(SearchWord));
            if (result.Result)
            {
                return new OkObjectResult(result.issueModels);
            }
            return new BadRequestObjectResult($"{result.Message}");
        }

        [HttpGet("SearchIssueByIssueType/{issueType}")]
        public async Task<IActionResult> GetIssueByIssueType(IssueType issueType)
        {
            var result = (await _services.SearchIssueByIssueTypeAsync("IssueType",issueType));
            if (result.Result)
            {
                return new OkObjectResult(result.issueModels);
            }
            return new BadRequestObjectResult($"{result.Message}");
        }

        [HttpGet("SearchIssueByPriority/{priority}")]
        public async Task<IActionResult> GetIssueByPriority(Priority priority)
        {
            var result = (await _services.SearchIssueByPriorityAsync("Priority", priority));
            if (result.Result)
            {
                return new OkObjectResult(result.issueModels);
            }
            return new BadRequestObjectResult($"{result.Message}");
        }
        #endregion

        #region Post
        //Post

        [HttpPost("CreateIssue")]
        public async Task<IActionResult> CreateIssue(IssueModifedModel issueModel)
        {
            var result = (await _services.CreateIssueAsync(issueModel));
            if (result.Bool)
            {
                return new OkObjectResult($"{result.Value}");
            }
            return new BadRequestObjectResult($"{result.Value}");
        }
        #endregion

        #region Update

        //Update

        [HttpPut("Update/{id}")]
        public async Task<IActionResult>UpdateIssue(uint id, IssueModifiedModelWithCompleted issueModel)
        {
            var result = (await _services.UpdateIssueAsync(id,issueModel));
            if (result.Bool)
            {
                return new OkObjectResult($"{result.Value}");
            }
            return new BadRequestObjectResult($"{result.Value}");

        }

        [HttpPut("UpdateCompleted/{id}/{Complted}")]
        public async Task<IActionResult> UpdateCompltedIssue(uint id, bool Complted)
        {
            var result = (await _services.UpdateIssueCompltedAsync(id, Complted));
            if (result.Bool)
            {
                return new OkObjectResult($"{result.Value}");
            }
            return new BadRequestObjectResult($"{result.Value}");

        }

        #endregion

        #region Delete

        //Delete
        [HttpDelete("DeleteIssue/{id}")]
        public async Task<IActionResult> DeleteIssue(uint id)
        {
            var result = (await _services.DeleteIssueAsync(id));
            if (result.Bool)
            {
                return new OkObjectResult($"{result.Value}");
            }
            return new BadRequestObjectResult($"{result.Value}");
        }

        #endregion
    }
}

using ToksApi.Models;
using ToksApi.Models.ServiceModels;
using ToksApi.Models.UtilityClasses;

namespace ToksApi.Services
{
    public interface IControllerServices
    {

        #region Create

        // Create 
        Task<BoolWithStringAndInt> CreateIssueAsync(IssueModifedModel issueModel);

        #endregion

        #region Search/Get

        //Search
        Task<IssueResultWithEnumerable> GetAllIssuesAsync();

        Task<IssueResultWithEnumerable> SearchIssueByIdAsync(uint Id);

        Task<IssueResultWithEnumerable> SearchIssueByTitleAsync(string SearchWord);
        Task<IssueResultWithEnumerable> SearchIssueByDescriptionAsync(string SearchWord);

        Task<IssueResultWithEnumerable> SearchIssueByIssueTypeAsync(string? Target, IssueType? issueType);
        Task<IssueResultWithEnumerable> SearchIssueByPriorityAsync(string? Target, Priority? priority);

        #endregion

        #region Update

        //Update
        Task<BoolWithStringAndInt> UpdateIssueAsync(uint Id,IssueModifiedModelWithCompleted issueModel);
        Task<BoolWithStringAndInt> UpdateIssueCompltedAsync(uint Id,bool Completed);

        #endregion

        #region Delete

        //Delete

        Task<BoolWithStringAndInt> DeleteIssueAsync(uint Id);

        #endregion


    }
}

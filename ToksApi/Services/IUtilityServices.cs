using ToksApi.Models;
using ToksApi.Models.UtilityClasses;

namespace ToksApi.Services
{
    public interface IUtilityServices
    {

        #region Search

        Task<BoolWithStringAndInt> SearchLongStringForSingleWordAsync(string StringBeingSearched, string SearchWord, uint? Id);
        Task<IssueResultWithEnumerable> SearchIssueListStringForWordAsync(string SearchWord, List<IssueModel> issueModelList);

        #endregion

    }
}

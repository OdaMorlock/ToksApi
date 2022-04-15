using ToksApi.Models;
using ToksApi.Models.UtilityClasses;

namespace ToksApi.Services
{
    public class UtilityServices : IUtilityServices
    {

        #region Search
        public async Task<IssueResultWithEnumerable> SearchIssueListStringForWordAsync(string SearchWord, List<IssueModel> issueModelList)
        {
            var result = new IssueResultWithEnumerable();

            var newIssueList = new List<IssueModel>();
            try
            {
                foreach (var List in issueModelList)
                {

                    var Searched = (await SearchLongStringForSingleWordAsync(List.Title, SearchWord, List.Id));

                    if (Searched.Bool)
                    {
                        newIssueList.Add(new IssueModel
                        {
                            Id = List.Id,
                            Title = List.Title,
                            Description = List.Description,
                            IssueType = List.IssueType,
                            Priority = List.Priority,
                            Created = List.Created,
                            Completed = List.Completed
                        });
                    }
                    result.Id = (uint?)Searched.Int;
                    result.Result = true;
                    result.issueModels = newIssueList;
                    result.Message = "Found Issue With Title With a Word Matching The Searched";
                    return result;
                }
                result.Result = false;
                result.issueModels = newIssueList;
                result.Message = "Did Not Find Issue With Title With a Word Matching The Searched";
                return result;
            }
            catch (Exception e)
            {
                result.Result = false;
                result.Message = $"In Catch : {e.Message}";
                return result;

            }

            result.Result = false;
            result.Message = "Failed Try Catch";
            return result;

        }

        public async Task<BoolWithStringAndInt> SearchLongStringForSingleWordAsync(string StringBeingSearched, string SearchWord, uint? Id)
        {
            var result = new BoolWithStringAndInt();

            string[] beingSearched = StringBeingSearched.Split(' ');
            foreach (var being in beingSearched)
            {
                if (SearchWord.ToLower() == being.ToLower())
                {
                    result.Bool = true;
                    result.Value = being;
                    result.Int = (int?)Id;
                    return result;
                }
            }
            result.Bool = false;
            return result;

        }

       

        #endregion

    }
}
